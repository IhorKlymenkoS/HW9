using HW9;
using System;
using System.Collections;
using IList = HW9.IList;

namespace TestConcole
{
    class Program
    {
        static void Main(string[] args)
        {
            MyLinkedList ml = new MyLinkedList();
            for (int i = 0; i < 5; i++)
            {
                ml.AddBack(i);
            }
            //Console.WriteLine(ml._count);

            

        }
    }
}
