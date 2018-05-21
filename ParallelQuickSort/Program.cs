using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParallelQuickSort.Properties;

namespace ParallelQuickSort
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Resources.Welcome);

            Start:
            Console.Write("\n" + Resources.InputNumberCount);
            int countOfNumbers = Int32.Parse(Console.ReadLine());

            //Console.WriteLine("Count of processors\tTime for sort");

            // generate some random source data
            Random rnd = new Random();
            int[] sourceData = new int[countOfNumbers];
            for (int i = 0; i < sourceData.Length; i++)
            {
                sourceData[i] = rnd.Next(1, 100);
            }

            for (var countOfProcessors = 1; countOfProcessors <= 128; countOfProcessors *= 2)
            {
                int[] sourceDataToSort = new int[countOfNumbers];
                for (int i = 0; i < countOfNumbers; i++)
                {
                    sourceDataToSort[i] = sourceData[i];
                }

                Console.WriteLine("\nInputed array:");
                foreach (var element in sourceDataToSort)
                {
                    Console.Write(element + " ");
                }
                Console.WriteLine("\n");

                Stopwatch t = new Stopwatch();
                t.Start();

                // perform the parallel sort
                ParallelkSort<int>.ParallelQuickSort(sourceDataToSort, new IntComparer(), countOfProcessors);

                t.Stop();
                var timeForSort = t.Elapsed;

                Console.WriteLine("Sorted array:");
                foreach (var element in sourceDataToSort)
                {
                    Console.Write(element + " ");
                }
                Console.WriteLine("\n");

                Console.WriteLine("Count of processors\t"+countOfProcessors + "\tTime for sort\t" + timeForSort.TotalMilliseconds + Resources.MilliSeconds+"\n--------------------");
            }

            goto Start;
        }

        public class IntComparer : IComparer<int>
        {
            public int Compare(int first, int second)
            {
                return first.CompareTo(second);
            }
        }
    }
}

