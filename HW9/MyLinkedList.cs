using System;
using System.Collections.Generic;
using System.Text;

namespace HW9
{
    public class MyLinkedList : IList
    {
        private int _count;
        private Node _head;

        private class Node
        {
            public int Value { get; set; }
            public Node Next { get; set; }
            public Node(int element)
            {
                Value = element;
            }
        }

        public int Capacity => _count;

        public int Length => _count;

        public int this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public MyLinkedList()
        {

        }

        public MyLinkedList(int element)
        {
            _head = new Node(element);
        }

        public MyLinkedList(IEnumerable<int> elements)
        {
            foreach (var item in elements)
            {
                AddBack(item);
            }
        }

        public void AddBack(int value)
        {
            AddByIndex(Length, value);
        }

        public void AddFront(int value)
        {
            AddByIndex(0, value);
        }

        public void AddByIndex(int index, int value)
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

            Node insertable = new Node(value);
            insertable.Next = current;
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

        public void RemoveFrontElement()
        {
            RemoveByIndexElement(0);
        }

        public void RemoveBackElement()
        {
            RemoveByIndexElement(Length);
        }

        public int RemoveByIndexElement(int index)
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

            int result;
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

        public void RemoveFrontNElements(int value)
        {
            RemoveByIndexNElements(0,value);
        }

        public void RemoveBackNElements(int value)
        {
            RemoveByIndexNElements((Length - value), value);
        }

        public void RemoveByIndexNElements(int index, int value)
        {
            for (int i = 0; i <= value; i++)
            {
                RemoveByIndexElement(index);
            }
        }

        public int FirstIndexByValue(int value)
        {
            var index = 0;
            foreach (var item in this)
            {
                if(item == value)
                {
                    return index;
                }

                ++index;
            }

            return -1;
        }

        public void Reverse()
        {
            Node temp = _head;
            MyLinkedList reverse = null;
            while (temp.Next != null)
            {
                reverse.AddBack(temp.Value);
            }
        }

        public int GetMaxElementValue()
        {
            var (_, value) = GetMaxValueIndexAndValue();
            return value;
        }

        public int GetMinElementValue()
        {
            Node temp = _head;
            int value = _head.Value;
            while (temp.Next != null)
            {
                if (temp.Value < value)
                {
                    value = temp.Value;
                }
            }

            return value;
        }

        private (int index, int value) GetMaxValueIndexAndValue()
        {
            if (_head == null)
            {
                throw new InvalidOperationException();
            }

            Node temp = _head;
            int maxValue = _head.Value;
            int currentIndex = 0;
            int maxIndex = currentIndex;
            do
            {
                if (temp.Value > maxValue)
                {
                    maxValue = temp.Value;
                    maxIndex = currentIndex;
                }

                ++currentIndex;
                temp = temp.Next;
            } while (temp != null);

            return (maxIndex, maxValue);
        }

        public int GetMaxElementIndex()
        {
            var (index, _) = GetMaxValueIndexAndValue();
            return index;
        }

        public int GetMinElementIndex()
        {
            int value = GetMinElementValue();
            int index = FirstIndexByValue(value);

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
                        int temp = tempI.Value;
                        tempI.Value = tempJ.Value;
                        tempJ.Value = temp;
                    }

                    tempJ = tempJ.Next;
                }

                tempI = tempI.Next;
            }

        }

        public int DeleteByValueFirst(int value)
        {
            int index = FirstIndexByValue(value);

            if (index!=-1)
            {
                RemoveByIndexElement(index);
                _count -= 1;
            }

            return index;
        }

        public int DeleteByValueAll(int value)
        {
            int index = 0;
            int count = 0;
            while(index!=-1)
            {
                index = FirstIndexByValue(value);
                RemoveByIndexElement(index);
                count++;
            }

            _count -= count;
            return count;
        }

        public void AddFrontArray(int[] array)
        {
            AddByIndex(0, array);
        }

        public void AddBackArray(int[] array)
        {
            AddByIndex(Length, array);
        }

        public void AddByIndex(int index, int[] array)
        {
            if (index > Length || index < 0)
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

            MyLinkedList tempArray = new MyLinkedList(array);
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

        public IEnumerator<int> GetEnumerator()
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
    }
}
