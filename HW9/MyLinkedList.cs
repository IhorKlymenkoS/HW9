using System;
using System.Collections.Generic;
using System.Text;

namespace HW9
{
    public class MyLinkedList : IList
    {
        private int _count;
        private class Node
        {
            public int Value { get; set; }
            public Node Next { get; set; }
            public Node(int element)
            {
                Value = element;
                NodeIndex = index;
            }
        }

        private Node _root;
        public int _count { get; private set; }

        public int Capacity => _count;

        public int Length => _count;

        public int this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        public void ReplaceLast(int newValue)
        {
            Node temp = _head;
            while (temp.Next != null)
            {
                temp = temp.Next;
            }

            temp.Value = newValue;
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

        public void RemoveByIndexElement(int index)
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

            if (previous != null)
            {
                previous.Next = current.Next;
            }
            else
            {
                _head = current.Next;
            }

            --_count;
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
            Node temp = _head;
            int index = 0;
            while (temp.Value == value || temp.Next!=null)
            {
                temp = temp.Next;
                index++;

                if (temp.Next==null && temp.Value!=value)
                {
                    index = -1;
                }
            }

            return index;
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
            Node temp = _head;
            int value = _head.Value;
                while (temp.Next != null)
                {
                if (temp.Value > value)
                {
                    value = temp.Value;
                }
            }

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

        public int GetMaxElementIndex()
        {
            int value = GetMaxElementValue();
            int index = FirstIndexByValue(value);

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
            Node tempY = _head;
            int value;

            while (tempI.Next!=null)
            {
                if (ascending)
                {
                    value = GetMaxElementValue();
                }
                else
                {
                    value = GetMinElementValue();
                }
               
                tempY.Value = value;
                tempY = tempY.Next;
                DeleteByValueFirst(value);
            }

            _head = tempY;
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
