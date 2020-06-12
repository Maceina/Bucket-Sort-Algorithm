using System;
using System.Collections.Generic;
using System.IO;
namespace BucketSort_OP
{
    class MyDataArray : DataArray
    {
        double[] data;
        public MyDataArray(int n, int seed)
        {
            data = new double[n];
            length = n;
            Random rand = new Random(seed);
            for (int i = 0; i < length; i++)
            {
                data[i] = rand.NextDouble();
            }
        }

        public override double this[int index]
        {
            get { return data[index]; }
            set { data[index] = value; }
        }

        public override void AddRange(List<double>[] buckets)
        {
            int k = 0;
            for (int i = 0; i < buckets.Length; i++)
            {
                foreach (double element in buckets[i])
                {
                    data[k++] = element;
                }
            }
        }
    }
}