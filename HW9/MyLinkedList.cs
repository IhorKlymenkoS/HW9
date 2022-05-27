using System;
using System.Collections.Generic;
using System.Text;

namespace HW9
{
    public class MyLinkedList
    {
        private class Node
        {
            public int Value { get; set; }
            public int NodeIndex { get; private set; }
            public Node Next { get; set; }
            public Node(int element, int index)
            {
                Value = element;
                NodeIndex = index;
            }
        }

        private Node _root;
        private int _nextIndex;
        private
        public int _count { get; private set; }

        public MyLinkedList(int element)
        {
            _count = 1;
            _nextIndex = 0;
            _root = new Node(element, _nextIndex);
            _nextIndex++;
        }

        public MyLinkedList()
        {
            _count = 0;
            _nextIndex = 0;
            _root = null;
        }

        public void AddBack(int element)
        {
            if (_root == null)
            {
                _root = new Node(element, _nextIndex);
                _count++;
                _nextIndex++;
            }
            else
            {
                Node temp = _root;
                while (temp.Next != null)
                {
                    temp = temp.Next;
                }

                temp.Next = new Node(element, _nextIndex);
                _count++;
                _nextIndex++;
            }
        }
        }
    }
}
