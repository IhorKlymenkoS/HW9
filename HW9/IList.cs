using System;
using System.Collections.Generic;

namespace HW9
{
    public interface IList<T>
        : IEnumerable<T>
        where T : IComparable<T>
    {

        IList<T> Initialize();
        //IList<T> Initialize(int item);
        IList<T> Initialize(IEnumerable<T> items);
        T this[int index]
        {
            get;
            set;
        }

        void AddFront(T value);

        void AddBack(T value);

        void AddByIndex(int index, T value);

        T RemoveFrontElement();

        T RemoveBackElement();

        T RemoveByIndexElement(int index);

        IEnumerable<T> RemoveFrontNElements(int count);

        IEnumerable<T> RemoveBackNElements(int count);

        IEnumerable<T> RemoveByIndexNElements(int index, int count);

        int Capacity { get; }
        int Length { get; }

        int FirstIndexByValue(T value);

        void Reverse();

        T GetMaxElementValue();

        T GetMinElementValue();

        int GetMaxElementIndex();

        int GetMinElementIndex();

        void Sort(bool ascending = true);

        int DeleteByValueFirst(T value);

        /// <summary>
        /// Remove all items by value
        /// </summary>
        /// <param name="value">Value to remove</param>
        /// <returns>Count of removed items</returns>
        int DeleteByValueAll(T value);

        void AddFrontItems(IEnumerable<T> items);

        void AddBackItems(IEnumerable<T> items);

        void AddByIndexItems(int index, IEnumerable<T> items);
    }
}
