﻿using System.Collections.Generic;

namespace HW9
{
    public interface IList : IEnumerable<int>
    {
        int this[int index]
        {
            get;
            set;
        }

        void AddFront(int value);

        void AddBack(int value);

        void AddByIndex(int index, int value);

        void RemoveFrontElement();

        void RemoveBackElement();

        int RemoveByIndexElement(int index);

        void RemoveFrontNElements(int value);

        void RemoveBackNElements(int value);

        void RemoveByIndexNElements(int index, int value);

        int Capacity { get; }
        int Length { get; }

        int FirstIndexByValue(int value);

        void Reverse();

        int GetMaxElementValue();

        int GetMinElementValue();

        int GetMaxElementIndex();

        int GetMinElementIndex();

        void Sort(bool ascending = true);

        int DeleteByValueFirst(int value);

        int DeleteByValueAll(int value);

        void AddFrontArray(int[] array);

        void AddBackArray(int[] array);

        void AddByIndex(int index, int[] array);
    }
}
