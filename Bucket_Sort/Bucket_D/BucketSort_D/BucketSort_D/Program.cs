using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketSort_D
{
    class Program
    {
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            Test_File_Array_List(seed);
        }

        public static void BucketSort(DataArray items)
        {
            string[] buckets = new string[items.Length];
            InitializeBuckets(buckets);
            for (int i = 0; i < items.Length; i++)
            {
                int bucketNr = (int)(items[i] * items.Length);
                string fileName = String.Format(@"bucket{0}.dat", bucketNr);    
                try
                {
                    using (BinaryWriter writer = new BinaryWriter(File.Open(fileName,
                    FileMode.Append)))
                    {
                            writer.Write(items[i]);
                            writer.Close();
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            foreach (string bucket in buckets)
            {
                MyBucketArray bucketArray = new MyBucketArray(bucket);
                using (bucketArray.fs = new FileStream(bucket, FileMode.Open,
                FileAccess.ReadWrite))
                {
                    BubbleSort(bucketArray);
                }
            }

            int k = 0;
            foreach (string bucket in buckets)
            {
                MyBucketArray bucketArray = new MyBucketArray(bucket);
                using (bucketArray.fs = new FileStream(bucket, FileMode.Open,
                    FileAccess.ReadWrite))
                {
                    if (bucketArray.NotEmpty())
                    {
                        for (int j = 0; j < bucketArray.Length; j++)
                        {
                            items[k++] = bucketArray[j];
                            
                        }
                    }
                }
                File.Delete(bucket);
            }
        }



        public static void BucketSort(DataList items)
        {
            string[] buckets = new string[items.Length];
            InitializeBuckets(buckets);

            for (int i = 0; i < items.Length; i++)
            {
                int bucketNr = (int)(items[i] * items.Length);
                string fileName = String.Format(@"bucket{0}.dat", bucketNr);
                try
                {
                    using (BinaryWriter writer = new BinaryWriter(File.Open(fileName,
                    FileMode.Append)))
                    {
                        writer.Write(items[i]);
                        writer.Close();
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            foreach (string bucket in buckets)
            {
                MyBucketList bucketList = new MyBucketList(bucket);
                using (bucketList.fs = new FileStream(bucket, FileMode.Open,
                FileAccess.ReadWrite))
                {
                    BubbleSort(bucketList);
                }
            }

            int k = 0;
            foreach (string bucket in buckets)
            {
                MyBucketArray bucketList = new MyBucketArray(bucket);
                using (bucketList.fs = new FileStream(bucket, FileMode.Open,
                    FileAccess.ReadWrite))
                {
                    if (bucketList.NotEmpty())
                    {
                        for (int j = 0; j < bucketList.Length; j++)
                        {
                            items[k++] = bucketList[j];
                        }
                    }
                }
                File.Delete(bucket);
            }
        }

        private static void BubbleSort(BucketArray bucketArray)
        { 
            if (bucketArray.Length > 1)
            {
                double prevdata, currentdata;
                int temp = Convert.ToInt32(bucketArray.Length);
                for (int i = temp - 1; i >= 0; i--)
                {
                    currentdata = bucketArray[0];
                    for (int j = 1; j <= i; j++)
                    {
                        prevdata = currentdata;
                        currentdata = bucketArray[j];
                        if (prevdata > currentdata)
                        {
                            bucketArray.Swap(j, currentdata, prevdata);
                            currentdata = prevdata;
                        }
                    }
                }
            }
        }

        private static void BubbleSort(BucketList bucketList)
        {
            if (bucketList.Length > 1)
            {
                double prevdata, currentdata;
                int length = Convert.ToInt32(bucketList.Length);
                for (int i = length - 1; i >= 0; i--)
                {
                    currentdata = bucketList.Head();
                    for (int j = 1; j <= i; j++)
                    {
                        prevdata = currentdata;
                        currentdata = bucketList.Next();
                        if (prevdata > currentdata)
                        {
                            bucketList.Swap(currentdata, prevdata);
                            currentdata = prevdata;
                        }
                    }
                }
            }
        }

        private static void InitializeBuckets(string[] buckets)
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                string fileName = String.Format(@"bucket{0}.dat", i);
                buckets[i] = fileName;
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                try
                {
                    using (BinaryWriter writer = new BinaryWriter(File.Open(fileName,
                    FileMode.Create))) { }
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public static void Test_File_Array_List(int seed)
        {
            int n = 100;
            string filename;
            filename = @"mydataarray.dat";
            Console.WriteLine("Bucket sort isorineje atmintyje:");
            Console.WriteLine("Duomenu masyvas:");
            Console.WriteLine("_______________________________________");
            Console.WriteLine("| Elementu kiekis |  Rikiavimo laikas |");
            Console.WriteLine("|-----------------|-------------------|");
            for (int i = 0; i < 7; i++)
            {
                MyFileArray myfileArray = new MyFileArray(filename, n, seed);
                using (myfileArray.fs = new FileStream(filename, FileMode.Open,
                    FileAccess.ReadWrite))
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    BucketSort(myfileArray);
                    watch.Stop();
                    Console.WriteLine("| {0,15} | {1,17} |", n, watch.Elapsed);
                    n = n * 2;
                    watch.Reset();
                }
            }
            Console.WriteLine("|-----------------|-------------------|");

            n = 100;
            filename = @"mydatalist.dat";
            Console.WriteLine("Duomenu susietas sarasas:");
            Console.WriteLine("_______________________________________");
            Console.WriteLine("| Elementu kiekis |  Rikiavimo laikas |");
            Console.WriteLine("|-----------------|-------------------|");
            for (int i = 0; i < 7; i++)
            {
                MyFileList myfileList = new MyFileList(filename, n, seed);
                using (myfileList.fs = new FileStream(filename, FileMode.Open,
                    FileAccess.ReadWrite))
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    BucketSort(myfileList);
                    watch.Stop();
                    Console.WriteLine("| {0,15} | {1,17} |", n, watch.Elapsed);
                    n = n * 2;
                    watch.Reset();
                }
            }
            Console.WriteLine("|-----------------|-------------------|");
        }
    }

    abstract class DataArray
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract double this[int index] { get; set; }
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
        public abstract double this[int index] { get; set; }
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

    abstract class BucketArray
    {
        protected double length;
        protected double byteLenght;
        public double Length { get { return length; } }
        public double ByteLength { get { return byteLenght; } }
        public abstract double this[int index] { get; }
        public abstract void Swap(int index, double a, double b);
        public abstract bool NotEmpty();
    }

    abstract class BucketList
    {
        protected double length;
        protected double byteLength;
        public double Length { get { return length; } }
        public double ByteLength { get { return byteLength; } }
        public abstract double getData(int index);
        public abstract double Head();
        public abstract double Next();
        public abstract void Swap(double a, double b);
        public abstract bool NotEmpty();
    }
}