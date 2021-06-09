using System;
using System.Collections.Generic;

namespace Parallel_MergeSort
{
    public class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>
            { 1, 4342, 123, 5, 34, 98, 12, 489, 14, 67, 32, 764, 1023, 4, 12678, 19 };

            Console.WriteLine(MergeSort(list));
        }
        private static List<int> MergeSort(List<int> m)
        {
            if (m.Count <= 1)
            {
                return m;
            }
            var left = new List<int>();
            var right = new List<int>();

            for (int i = 0; i < m.Count; i++)
            {
                if (m[i] % 2 != 0)
                {
                    left.Add(m[i]);
                }
                else
                {
                    right.Add(m[i]);
                }
            }
            left = MergeSort(left);
            right = MergeSort(right);


            return Merge(left, right);
        }

        private static List<int> Merge(List<int> left, List<int> right)
        {
            var result = new List<int>();
            int index = 0;
            while (left.Count != 0 && right.Count != 0)
            {
                if (left[index] <= right[index])
                {
                    result.Add(left[index]);
                    left.Remove(left[index]);
                }
                else
                {
                    result.Add(right[index]);
                    right.Remove(right[index]);
                }
                index++;
            }
            return result;
        }
    }
}
