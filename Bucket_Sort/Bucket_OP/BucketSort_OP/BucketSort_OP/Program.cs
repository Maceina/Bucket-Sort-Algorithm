using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace BucketSort_OP
{
    class Program
    {
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            Test_Array_List(seed);
        }

        public static void BucketSort(DataArray items)
        {
            
            List<double>[] buckets = new List<double>[items.Length];
            InitializeBuckets(buckets, items);

            for (int i = 0; i < items.Length; i++)
            {
                int bucketNr = (int)(items[i] * buckets.Length);
                buckets[bucketNr].Add(items[i]);
                //Console.WriteLine(items[i]);
            }

            for (int i = 0; i < buckets.Length; i++)
            {
                BubbleSort(buckets[i]);
            }

            items.AddRange(buckets);
        }

        private static void BubbleSort(List<double> bucket)
        {
            if (bucket.Count > 1)
            {
                for (int i = 0; i < bucket.Count; i++)
                {
                    for (int j = 0; j < bucket.Count; j++)
                    {
                        if (bucket[i] < bucket[j])
                        {
                            double temp = bucket[i];
                            bucket[i] = bucket[j];
                            bucket[j] = temp;
                        }
                    }
                }
            }  
        }

        private static void BubbleSort(LinkedList<double> bucket)
        {
            if (bucket.Count > 1)
            {
                LinkedListNode<double> prev, current;
                for (int i = bucket.Count - 1; i >= 0; i--)
                {
                    current = bucket.First;
                    for (int j = 1; j <= i; j++)
                    {
                        prev = current;
                        current = current.Next;
                        if (prev.Value > current.Value)
                        {
                            double temp = prev.Value;
                            prev.Value = current.Value;
                            current.Value = temp;
                        }
                    }
                }
            }
        }
      

        public static void BucketSort(DataList items)
        {
            LinkedList<double>[] buckets = new LinkedList<double>[items.Length];
            InitializeBuckets(buckets, items);

            int bucketNmr = ((int)(items.Head() * buckets.Length));
            buckets[bucketNmr].AddLast(items.Head());
            while (items.Current())
            {
                double next = items.Next();
                if (next > 0)
                {
                    int bucketNr = ((int)(next * buckets.Length));
                    buckets[bucketNr].AddLast(next);
                }
                else break;
            }

            for (int i = 0; i < buckets.Length; i++)
            {
                BubbleSort(buckets[i]);
            }

            items.AddRange(buckets);
        }

        private static void InitializeBuckets(LinkedList<double>[] buckets, DataList items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                buckets[i] = new LinkedList<double>();
            }
        }

        private static void InitializeBuckets(List<double>[] buckets, DataArray items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                buckets[i] = new List<double>();
            }
        }
       

        public static void Test_Array_List(int seed)
        {
            int n = 10000;
            Console.WriteLine("Duomenu masyvas operatyvioje atmintyje:");
            Console.WriteLine("_______________________________________");
            Console.WriteLine("| Elementu kiekis |  Rikiavimo laikas |");
            Console.WriteLine("|-----------------|-------------------|");
            for (int i = 0; i < 7; i++)
            {
                MyDataArray myarray = new MyDataArray(n, seed);
                var watch = System.Diagnostics.Stopwatch.StartNew();
                BucketSort(myarray);
                watch.Stop();
                Console.WriteLine("| {0,15} | {1,17} |", n, watch.Elapsed);
                n = n * 2;
                watch.Reset();
            }
            Console.WriteLine("|-----------------|-------------------|");

            Console.WriteLine("\nDuomenu susietas sarasas operatyvioje atmintyje:");
            Console.WriteLine("_______________________________________");
            Console.WriteLine("| Elementu kiekis |  Rikiavimo laikas |");
            Console.WriteLine("|-----------------|-------------------|");
            n = 10000;
            for (int i = 0; i < 7; i++)
            {
                MyDataList mylist = new MyDataList(n, seed);
                var watch = System.Diagnostics.Stopwatch.StartNew();
                BucketSort(mylist);
                watch.Stop();
                Console.WriteLine("| {0,15} | {1,17} |", n, watch.Elapsed);
                n = n * 2;
                watch.Reset();
            }
            Console.WriteLine("|-----------------|-------------------|");
            
        }
    }

    abstract class DataArray
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract double this[int index] { get; set; }
        public abstract void AddRange(List<double>[] items);
        public void Print(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(" {0:F5} ", this[i]);
            }
            Console.WriteLine();
        }
    }

    abstract class DataList
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract double Head();
        public abstract double Next();
        public abstract bool Current();
        public abstract void AddRange(LinkedList<double>[] items);
        public void Print(int n)
        {
            Console.WriteLine(" {0:F5} ", Head());
            for (int i = 1; i < n; i++)
            {
                Console.WriteLine(" {0:F5} ", Next());
            }
            Console.WriteLine();
        }
    }
}