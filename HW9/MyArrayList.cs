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
        public int Length => _currentCount;//orient on it while going through elements

        public T this[int index]//protection from index out of range
        {
            get
            {
                if(index < 0 || index >= Length)
                {
                    throw new ArgumentException();
                }

                return _array[index];
            }
            set
            {
                if (index < 0 || index >= Length)//Add in Capacity?
                {
                    throw new ArgumentException();
                }

                _array[index] = value;
            }
        }

        public MyArrayList() : this(DefaultSize)
        {
        }

        public MyArrayList(T capacity)
        {
            if (capacity.CompareTo(DefaultSize) == -1)
            {
                capacity = DefaultSize;
            }

            _array = new T[capacity];
        }

        public MyArrayList(IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentException();
            }

            T[] array = source.ToArray();

            _array = new T[Math.Max(array.Length, DefaultSize)];//array == null??
            for (int i = 0; i < array.Length; i++)
            {
                _array[i] = array[i];
            }

            _currentCount = array.Length;
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

        public T RemoveFrontElement()
        {
            if (Length<1)
            {
                throw new IndexOutOfRangeException();
            }
            IEnumerable<T> tempI = RemoveByIndexNElements(0, 1);
            T[] tempJ = tempI.ToArray();
            T result = tempJ[0];

            return result;
        }

        public T RemoveBackElement()
        {
            if (Length < 1)
            {
                throw new IndexOutOfRangeException();
            }
            IEnumerable<T> tempI = RemoveByIndexNElements(Length - 1, 1);
            T[] tempJ = tempI.ToArray();
            T result = tempJ[0];

            return result;
        }

        public T RemoveByIndexElement(int index)
        {
            if (Length < 1 || index<0)
            {
                throw new IndexOutOfRangeException();
            }
            IEnumerable<T> tempI = RemoveByIndexNElements(index, 1);
            T[] tempJ = tempI.ToArray();
            T result = tempJ[0];

            return result;
        }

        public IEnumerable<T> RemoveFrontNElements(int value)
        {
            if (value > Length || value <= 0)
            {
                throw new ArgumentException();
            }
            IEnumerable<T> result = RemoveByIndexNElements(0, value);

            return result;
        }

        public IEnumerable<T> RemoveBackNElements(int count)
        {
            if (count > Length || count <= 0)
            {
                throw new ArgumentException();
            }
            IEnumerable<T> result = RemoveByIndexNElements(Length - count, count);

            return result;
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
            T[] result = new T[count];
            int resultIndex = 0;
            var endPosition = index + count;
            if (endPosition > Length)
            {
                throw new ArgumentException();
            }
            if (index < 0)
            {
                throw new ArgumentException();
            }
            if (count < 0)
            {
                throw new ArgumentException();
            }

            for (int i = endPosition, j = index;
                i < Length;
                i++, j++)
            {
                result[resultIndex] = _array[j];
                _array[j] = _array[i];
                resultIndex++;
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
            T[] arrayTemp = new T[Length];
            int j = arrayTemp.Length - 1;

            for (int i = 0; i < Length; i++)
            {
                arrayTemp[j--] = _array[i];
            }

            _array = arrayTemp;
        }

        public T GetMaxElementValue()
        {
            if (Length<1)
            {
                throw new ArgumentException();
            }

            T max = _array[0];
            for (int i = 0; i < Length; i++)
            {
                if (max.CompareTo(_array[i])==-1)
                {
                    max = _array[i];
                }
            }

            return max;
        }

        public T GetMinElementValue()
        {
            if (Length < 1)
            {
                throw new ArgumentException();
            }

            T min = _array[0];
            for (int i = 0; i < Length; i++)
            {
                if (min.CompareTo(_array[i])==1)
                {
                    min = _array[i];
                }
            }

            return min;
        }

        public int GetMaxElementIndex()
        {
            return FirstIndexByValue(GetMaxElementValue());
        }

        public int GetMinElementIndex()
        {
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

        private void Swap(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public int DeleteByValueFirst(T value)
        {
            int index = FirstIndexByValue(value);
            RemoveByIndexElement(index);

            return index;
        }

        public int DeleteByValueAll(T value)
        {
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

        public void AddFrontItems(IEnumerable<T> source)
        {
            T[] array = source.ToArray();
            AddByIndexItems(0, array);
        }

        public void AddBackItems(IEnumerable<T> source)
        {
            T[] array = source.ToArray();
            AddByIndexItems(Length+1, array);
        }

        public void AddByIndexItems(int index, IEnumerable<T> source)
        {
            T[] array = source.ToArray();
            if (index > Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            if (array == null)
            {
                throw new NullReferenceException();
            }

            int tempLength = Length + array.Length;
            T[] arrayTemp = new T[tempLength];
            int addArrayEnd = index + array.Length - 1;
            int oldArrayCount = 0;
            int addArrayCount = 0;
            int i;

            for (i = 0; i < index; i++)
            {
                arrayTemp[i] = _array[oldArrayCount++];
            }
            for (i = index; i <= addArrayEnd; i++)
            {
                arrayTemp[i] = array[addArrayCount];
                addArrayCount++;
            }
            for (i = addArrayEnd + 1; i < arrayTemp.Length; i++)
            {
                arrayTemp[i] = _array[oldArrayCount++];
            }

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
    }
}
