using System;

namespace SolverCore
{
    public static class Sorter
    {
        public static void QuickSort<TKey, TArray>(TKey[] keys, int start, int end, params TArray[][] arrays)
            where TArray : IComparable
            where TKey : IComparable
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (arrays == null) throw new ArgumentNullException(nameof(arrays));

            foreach(var array in arrays)
            {
                if(array == null)
                {
                    throw new ArgumentException("One of the lines is null", nameof(arrays));
                }
            }

            if (start >= end)
            {
                return;
            }

            Sort(keys, start, end, arrays);
        }

        private static void Sort<TKey, TArray>(TKey[] keys, int start, int end, params TArray[][] arrays)
            where TKey : IComparable
            where TArray : IComparable
        {
            int pivot = Partition(keys, start, end, arrays);
            QuickSort(keys, start, pivot - 1, arrays);
            QuickSort(keys, pivot + 1, end, arrays);
        }

        private static (T, T) Swap<T>(T first, T second) => (second, first);

        private static int Partition<TKey, TArray>(TKey[] keys, int start, int end, params TArray[][] arrays)
            where TArray : IComparable
            where TKey : IComparable
        {
            int marker = start;

            for (int i = start; i <= end; i++)
            {
                if (keys[i].CompareTo(keys[end]) < 0)
                {
                    for (int j = 0; j < arrays.Length; j++)
                    {
                        (arrays[j][marker], arrays[j][i]) = Swap(arrays[j][marker], arrays[j][i]);
                    }

                    (keys[marker], keys[i]) = Swap(keys[marker], keys[i]);

                    marker++;
                }
            }

            for (int j = 0; j < arrays.Length; j++)
            {
                (arrays[j][marker], arrays[j][end]) = Swap(arrays[j][marker], arrays[j][end]);
            }

            (keys[marker], keys[end]) = Swap(keys[marker], keys[end]);
            return marker;
        }
    }
}
