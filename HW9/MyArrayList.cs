using System;
using System.Collections.Generic;
using System.Text;

namespace HW9
{
    public class MyArrayList : IList
    {
        private const double IncreaseCoefficient = 1.33;
        private const int DefaultSize = 4;
        private int[] _array;
        private int _currentCount;

        public int Capacity => _array.Length;
        public int Length => _currentCount;//orient on it while going through elements

        public int this[int index]//protection from index out of range
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

        public MyArrayList(int capacity)
        {
            capacity = Math.Max(capacity, DefaultSize);
            _array = new int[capacity];
        }

        public MyArrayList(int[] array)
        {
            if (array==null)
            {
                throw new ArgumentException();
            }

            _array = new int[Math.Max(array.Length, DefaultSize)];//array == null??
            for (int i = 0; i < array.Length; i++)
            {
                _array[i] = array[i];
            }

            _currentCount = array.Length;
        }

        public void AddFront(int value)
        {
            AddByIndex(0, value);
        }

        public void AddBack(int value)
        {
            AddByIndex(_currentCount, value);
        }

        public void AddByIndex(int index, int value)
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
            int[] arrayTemp = new int[newLength];
            for (int i = 0; i < Length; i++)
            {
                arrayTemp[i] = this[i];
            }

            _array = arrayTemp;
        }

        public void RemoveFrontElement()
        {
            RemoveByIndexNElements(0, 1);
        }

        public void RemoveBackElement()
        {
            RemoveByIndexNElements(Length-1, 1);
        }

        public void RemoveByIndexElement(int index)
        {
            RemoveByIndexNElements(index, 1);
        }

        public void RemoveFrontNElements(int value)
        {
            RemoveByIndexNElements(0, value);
        }

        public void RemoveBackNElements(int value)
        {
            RemoveByIndexNElements(Length - value, value);
        }

        public void RemoveByIndexNElements(int index, int count)
        {
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
                _array[j] = _array[i];
            }

            _currentCount -= count;
        }

        public int FirstIndexByValue(int value)
        {
            int index = -1;
            for (int i = 0; i < Length; i++)
            {
                if (_array[i] == value)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        public void Reverse()
        {
            int[] arrayTemp = new int[Length];
            int j = arrayTemp.Length - 1;

            for (int i = 0; i < Length; i++)
            {
                arrayTemp[j--] = _array[i];
            }

            _array = arrayTemp;
        }

        public int GetMaxElementValue()
        {
            if (Length<1)
            {
                throw new ArgumentException();
            }

            int max = _array[0];
            for (int i = 0; i < Length; i++)
            {
                if (max < _array[i])
                {
                    max = _array[i];
                }
            }

            return max;
        }

        public int GetMinElementValue()
        {
            if (Length < 1)
            {
                throw new ArgumentException();
            }

            int min = _array[0];
            for (int i = 0; i < Length; i++)
            {
                if (min > _array[i])
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

        private void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        public int DeleteByValueFirst(int value)
        {
            int index = FirstIndexByValue(value);
            RemoveByIndexElement(index);

            return index;
        }

        public int DeleteByValueAll(int value)
        {
            int j = 0;
            int i;
            int[] arrayTemp = new int[Length];
            for (i = 0; i < arrayTemp.Length; i++)
            {
                if (_array[i] != value)
                {
                    arrayTemp[j++] = _array[i];
                }
            }

            _array = arrayTemp;

            int count = i - j;
            _currentCount = -count;
            return count;
        }

        public void AddFrontArray(int[] array)
        {
            AddByIndex(0, array);
        }

        public void AddBackArray(int[] array)
        {
            AddByIndex(Length+1, array);
        }

        public void AddByIndex(int index, int[] array)
        {
            if (Length<1)
            {
                throw new ArgumentNullException(nameof(array), "Array is null");
            }
            int[] arrayTemp = new int[_array.Length + array.Length];
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

        public IEnumerator<int> GetEnumerator()
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
    }
}
