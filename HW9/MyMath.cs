using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HW9
{
    public static class MyMath
    {
        public static void Main()
        {
            List<int> arrList = new List<int>();
            arrList.Exists(x =>
            {
                return x > 10;
            });
            arrList.ForEach(x =>
            {
                Console.WriteLine(x);
                Console.WriteLine(x * x);
            });
            int i = 0;
            arrList.RemoveAll(x => i++ % 2 == 0);
            LinkedList<int> lList = new LinkedList<int>();
        }

        public static void PowerTwo(this ref int a)
        {
            a *= a;
        }

        public static bool MoreThanN(int x)
        {
            return x > 10;
        }

        public static string ToString<T, J>(T a, J b)
        {
            return $"{a} {b}";
        }

    }
}
