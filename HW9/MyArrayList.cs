using System;
using System.Collections.Generic;
using System.Linq;

namespace HW9
{
    public class MyArrayList<T> : IList<T>
        where T : IComparable<T>
    {
        private const double IncreaseCoefficient = 1.33;
        private const int DefaultSize = 4;
        private T[] _array;
        private int _currentCount;

        public int Capacity => _array.Length;
        public int Length => _currentCount;

        public T this[int index]
        {
            get
            {
                if(index < 0 || index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }

                return _array[index];
            }
            set
            {
                if (index < 0 || index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }

                _array[index] = value;
            }
        }

        public MyArrayList() : this(DefaultSize)
        {
        }

        public MyArrayList(int capacity)
        {
            if (capacity.CompareTo(DefaultSize) == -1)
            {
                capacity = DefaultSize;
            }

            _array = new T[capacity];
        }

        public MyArrayList(IEnumerable<T> elements)
        {
            if (elements == null)
            {
                throw new ArgumentException();
            }

            int count = elements.Count();
            int index = 0;
            _array = new T[Math.Max(count, DefaultSize)];
            foreach (var item in elements)
            {
                _array[index++] = item;
            }

            _currentCount = count;
        }

        public void AddFront(T value)
        {
            AddByIndex(0, value);
        }

        public void AddBack(T value)
        {
            AddByIndex(_currentCount, value);
        }

        public void AddByIndex(int index, T value)
        {
            if(Length == Capacity)
            {
                Resize();
            }

            for (int i = Length - 1; i >= index; i--)
            {
                _array[i + 1] = _array[i];
            }

            _array[index] = value;
            ++_currentCount;
        }

        public T RemoveFrontElement()
        {
            T result = RemoveByIndexElement(0);

            return result;
        }

        public T RemoveBackElement()
        {
            T result = RemoveByIndexElement(Length - 1);

            return result;
        }

        public T RemoveByIndexElement(int index)
        {
            if (Length == 0)
            {
                throw new InvalidOperationException();
            }

            IEnumerable<T> temp = RemoveByIndexNElements(index, 1);
            T result = temp.First();

            return result;
        }

        public IEnumerable<T> RemoveFrontNElements(int count)
        {
            IEnumerable<T> result = RemoveByIndexNElements(0, count);

            return result;
        }

        public IEnumerable<T> RemoveBackNElements(int count)
        {
            if (Length==0)
            {
                throw new InvalidOperationException();
            }
            if (count > Length || count <= 0)
            {
                throw new ArgumentException();
            }

            return RemoveByIndexNElements(Length - count, count);
        }

        public IEnumerable<T> RemoveByIndexNElements(int index, int count)
        {
            if (Length == 0)
            {
                throw new InvalidOperationException();
            }
            if (index > Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            if ((index + count) > Length || count <= 0)
            {
                throw new ArgumentException();
            }
            
            T[] result = new T[count];
            int resultIndex = 0;
            var endPosition = index + count;

            for (int i = index; i < endPosition; i++)
            {
                result[resultIndex++] = _array[i];
            }
            for (int i = endPosition, j = index;
                i < Length;
                i++, j++)
            {
                _array[j] = _array[i];
            }

            _currentCount -= count;

            return result;
        }

        public int FirstIndexByValue(T value)
        {
            int index = -1;
            for (int i = 0; i < Length; i++)
            {
                if (_array[i].CompareTo(value)==0)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        public void Reverse()
        {
            int j = Length-1;
            for (int i = 0; i < Length/2; i++)
            {
                Swap(ref _array[j--], ref _array[i]);
            }
        }

        public T GetMaxElementValue()
        {
            if (Length == 0)
            {
                throw new InvalidOperationException();
            }

            T max = _array[0];
            for (int i = 0; i < Length; i++)
            {
                if (max.CompareTo(_array[i]) == -1)
                {
                    max = _array[i];
                }
            }

            return max;
        }

        public T GetMinElementValue()
        {
            if (Length == 0)
            {
                throw new InvalidOperationException();
            }

            T min = _array[0];
            for (int i = 0; i < Length; i++)
            {
                if (min.CompareTo(_array[i]) == 1)
                {
                    min = _array[i];
                }
            }

            return min;
        }

        public int GetMaxElementIndex()
        {
            if (Length == 0)
            {
                throw new InvalidOperationException();
            }

            return FirstIndexByValue(GetMaxElementValue());
        }

        public int GetMinElementIndex()
        {
            if (Length == 0)
            {
                throw new InvalidOperationException();
            }

            return FirstIndexByValue(GetMinElementValue());
        }

        public void Sort(bool ascending = true)
        {
            int coef = ascending ? 1 : -1;
            for (int i = 0; i < Length - 1; i++)
            {
                for (int j = i + 1; j < Length; j++)
                {
                    if(_array[i].CompareTo(_array[j]) == coef)
                    {
                        Swap(ref _array[i], ref _array[j]);
                    }
                }
            }
        }

        public int DeleteByValueFirst(T value)
        {
            if (Length == 0)
            {
                throw new InvalidOperationException();
            }

            int index = FirstIndexByValue(value);
            if (index!=-1)
            {
                RemoveByIndexElement(index);
            }

            return index;
        }

        public int DeleteByValueAll(T value)
        {
            if (Length == 0)
            {
                throw new InvalidOperationException();
            }

            int countAfterRemove = 0;
            T[] arrayTemp = new T[Length];
            for (int i = 0; i < arrayTemp.Length; i++)
            {
                if (_array[i].CompareTo(value)==1 || _array[i].CompareTo(value) == -1)
                {
                    arrayTemp[countAfterRemove++] = _array[i];
                }
            }

            _array = arrayTemp;
            int result = _currentCount - countAfterRemove;
            _currentCount = countAfterRemove;

            return result;
        }

        public void AddFrontItems(IEnumerable<T> items)
        {
            T[] array = items.ToArray();
            AddByIndexItems(0, array);
        }

        public void AddBackItems(IEnumerable<T> items)
        {
            T[] array = items.ToArray();
            AddByIndexItems(Length, array);
        }

        public void AddByIndexItems(int index, IEnumerable<T> items)
        {
            int length = items.Count();

            if (index > Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            if (length == 0)
            {
                throw new ArgumentException();
            }

            int tempLength = Length + length;
            T[] arrayTemp = new T[tempLength];
            int addArrayEnd = index + length;
            int oldArrayCount = 0;
            int i;
            for (i = 0; i < index; i++)
            {
                arrayTemp[i] = _array[oldArrayCount++];
            }
            foreach (var item in items)
            {
                arrayTemp[i++] = item;
            }
            for (i = addArrayEnd; i < tempLength; i++)
            {
                arrayTemp[i] = _array[oldArrayCount++];
            }

            _currentCount += length;
            _array = arrayTemp;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
            {
                yield return _array[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IList<T> Initialize()
        {
            return new MyArrayList<T>();
        }

        public IList<T> Initialize(IEnumerable<T> items)
        {
            return new MyArrayList<T>(items);
        }

        private void Resize()
        {
            var newLength = (int)(IncreaseCoefficient * Capacity);
            T[] arrayTemp = new T[newLength];
            for (int i = 0; i < Length; i++)
            {
                arrayTemp[i] = this[i];
            }

            _array = arrayTemp;
        }
        private void Swap(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}
