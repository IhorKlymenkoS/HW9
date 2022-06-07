using System;
using System.Collections.Generic;
using System.Text;

namespace HW9
{
    public class MyLinkedList<T> : IList<T>
        where T : IComparable<T>
    {
        private int _count;
        private Node _head;

        private class Node
        {
            public T Value { get; set; }
            public Node Next { get; set; }
            public Node(T element)
            {
                Value = element;
            }
        }

        public int Capacity => _count;

        public int Length => _count;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }

                Node temp = _head;
                
                for(int i = 0; i != index; ++i)
                {
                    temp = temp.Next;
                }

                return temp.Value;
            }
            set
            {
                if (index < 0 || index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }

                Node temp = _head;

                for (int i = 0; i != index; ++i)
                {
                    temp = temp.Next;
                }

                temp.Value=value;
            }
        }

        public MyLinkedList()
        {
            _count = 0;
        }

        public MyLinkedList(T element)
        {
            _count = 1;
            _head = new Node(element);
        }

        public MyLinkedList(IEnumerable<T> elements)
        {
            foreach (var item in elements)
            {
                AddBack(item);
            }
        }

        private (int index, T value) GetMaxValueIndexAndValue()
        {
            if (_head == null)
            {
                throw new InvalidOperationException();
            }

            Node temp = _head;
            T maxValue = _head.Value;
            int currentIndex = 0;
            int maxIndex = currentIndex;
            do
            {
                if (temp.Value.CompareTo(maxValue) == 1)
                {
                    maxValue = temp.Value;
                    maxIndex = currentIndex;
                }

                ++currentIndex;
                temp = temp.Next;
            } while (temp != null);

            return (maxIndex, maxValue);
        }
        private (int index, T value) GetMinValueIndexAndValue()
        {
            if (_head == null)
            {
                throw new InvalidOperationException();
            }

            Node temp = _head;
            T maxValue = _head.Value;
            int currentIndex = 0;
            int maxIndex = currentIndex;
            do
            {
                if (temp.Value.CompareTo(maxValue) == -1)
                {
                    maxValue = temp.Value;
                    maxIndex = currentIndex;
                }

                ++currentIndex;
                temp = temp.Next;
            } while (temp != null);

            return (maxIndex, maxValue);
        }

        public void AddBack(T value)
        {
            AddByIndex(Length, value);
        }

        public void AddFront(T value)
        {
            AddByIndex(0, value);
        }

        public void AddByIndex(int index, T value)
        {
            if(index > Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            Node current = _head;
            Node previous = null;
            for(int i = 0; i < index; ++i)
            {
                previous = current;
                current = current.Next;
            }

            var insertable = new Node(value)
            {
                Next = current
            };
            if (previous != null)
            {
                previous.Next = insertable;
            }
            else
            {
                _head = insertable;
            }

            ++_count;
        }

        public T RemoveFrontElement()
        {
            if (_count == 0)
            {
                throw new IndexOutOfRangeException();
            }

            T result = RemoveByIndexElement(0);

            return result;
        }

        public T RemoveBackElement()
        {
            if (_count == 0)
            {
                throw new IndexOutOfRangeException();
            }

            T result = RemoveByIndexElement(Length - 1);

            return result;
        }

        public T RemoveByIndexElement(int index)
        {
            if (index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            Node current = _head;
            Node previous = null;
            for (int i = 0; i < index; ++i)
            {
                previous = current;
                current = current.Next;
            }

            T result;
            if (previous != null)
            {
                result = previous.Next.Value;
                previous.Next = current.Next;
            }
            else
            {
                result = _head.Value;
                _head = current.Next;
            }

            --_count;

            return result;
        }

        public IEnumerable<T> RemoveFrontNElements(int value)
        {
            if (value > Length || value <= 0)
            {
                throw new ArgumentException();
            }

            return RemoveByIndexNElements(0,value);
        }

        public IEnumerable<T> RemoveBackNElements(int count)
        {
            if (count > Length || count <= 0)
            {
                throw new ArgumentException();
            }

            return RemoveByIndexNElements(
                Length - count, count);
        }

        public IEnumerable<T> RemoveByIndexNElements(int index, int count)
        {
            if (index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            if ((index + count) > Length || count <= 0)
            {
                throw new ArgumentException();
            }

            var result = new T[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = RemoveByIndexElement(index);
            }

            return result;
        }

        public int FirstIndexByValue(T value)
        {
            var index = 0;
            foreach (var item in this)
            {
                if(item.CompareTo(value) == 0)
                {
                    return index;
                }

                ++index;
            }

            return -1;
        }

        public void Reverse()
        {
            Node tempI = _head;
            var reverse = new MyLinkedList<T>();
            while(tempI != null)
            {
                reverse.AddFront(tempI.Value);
                tempI = tempI.Next;
            }

            _head = reverse._head;
        }

        public T GetMaxElementValue()
        {
            var (_, value) = GetMaxValueIndexAndValue();
            return value;
        }

        public T GetMinElementValue()
        {
            var (_, value) = GetMinValueIndexAndValue();
            return value;
        }

        public int GetMaxElementIndex()
        {
            var (index, _) = GetMaxValueIndexAndValue();
            return index;
        }

        public int GetMinElementIndex()
        {
            var (index, _) = GetMinValueIndexAndValue();
            return index;
        }

        public void Sort(bool ascending = true)
        {
            Node tempI = _head;
            int coef = ascending ? 1 : -1;
            while(tempI != null)
            {
                Node tempJ = tempI?.Next;
                while (tempJ != null)
                {
                    if(tempI.Value.CompareTo(tempJ.Value) == coef)
                    {
                        T temp = tempI.Value;
                        tempI.Value = tempJ.Value;
                        tempJ.Value = temp;
                    }

                    tempJ = tempJ.Next;
                }

                tempI = tempI.Next;
            }
        }

        public int DeleteByValueFirst(T value)
        {
            int index = FirstIndexByValue(value);
            if (index>=0)
            {
                RemoveByIndexElement(index);
            }

            return index;
        }

        public int DeleteByValueAll(T value)
        {
            var tempList = new MyLinkedList<T>();
            foreach (var item in this)
            {
                if (item.CompareTo(value) != 0)
                {
                    tempList.AddBack(item);
                }
            }

            var removedCount = Length - tempList.Length;
            _head = tempList._head;

            return removedCount;
        }

        public void AddFrontItems(IEnumerable<T> array)
        {
            AddByIndexItems(0, array);
        }

        public void AddBackItems(IEnumerable<T> array)
        {
            AddByIndexItems(Length, array);
        }

        public void AddByIndexItems(int index, IEnumerable<T> array)
        {
            if (index > Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            if (array == null)
            {
                throw new NullReferenceException();
            }

            Node current = _head;
            Node previous = null;
            for (int i = 0; i < index; ++i)
            {
                previous = current;
                current = current.Next;
            }

            var tempArray = new MyLinkedList<T>(array);
            Node tempTail = tempArray._head;
            while (tempTail.Next!=null)
            {
                tempTail = tempTail.Next;
            }

            tempTail.Next = current;
            if (previous != null)
            {
                previous.Next = tempArray._head;
            }
            else
            {
                _head = tempArray._head;
            }

            _count +=tempArray.Length;
                }

        public IEnumerator<T> GetEnumerator()
        {
            Node temp = _head;
            while(temp != null)
            {
                yield return temp.Value;
                temp = temp.Next;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IList<T> Initialize()
        {
            return new MyLinkedList<T>();
        }

        public IList<T> Initialize(IEnumerable<T> items)
        {
            return new MyLinkedList<T>(items);
        }
    }
}
