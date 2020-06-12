﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketSort_D
{
    class MyFileList : DataList
    {
        int prevNode;
        int currentNode;
        int nextNode;

        public MyFileList(string filename, int n, int seed)
        {
            length = n;
            Random rand = new Random(seed);
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename,
                FileMode.Create)))
                {
                    writer.Write(4);
                    for (int j = 0; j < length; j++)
                    {
                        writer.Write(rand.NextDouble());
                        writer.Write((j + 1) * 12 + 4);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public FileStream fs { get; set; }
        public override double Head()
        {
            Byte[] data = new Byte[12];
            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            currentNode = BitConverter.ToInt32(data, 0);
            prevNode = -1;
            fs.Seek(currentNode, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            double result = BitConverter.ToDouble(data, 0);
            nextNode = BitConverter.ToInt32(data, 8);
            return result;
        }
        public override double Next()
        {
            Byte[] data = new Byte[12];
            fs.Seek(nextNode, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            prevNode = currentNode;
            currentNode = nextNode;
            double result = BitConverter.ToDouble(data, 0);
            nextNode = BitConverter.ToInt32(data, 8);
            return result;
        }

        public override double this[int index]
        {
            get
            {
                Byte[] data = new Byte[8];
                Head();
                while (currentNode != ((index * 12) + 4))
                {
                    Next();
                }
                fs.Seek(currentNode, SeekOrigin.Begin);
                fs.Read(data, 0, 8);
                return BitConverter.ToDouble(data, 0);
            }
            set
            {
                Byte[] data = new Byte[8];
                BitConverter.GetBytes(value).CopyTo(data, 0);
                Head();
                while (currentNode != ((index * 12) + 4))
                {
                    Next();
                }
                fs.Seek(currentNode, SeekOrigin.Begin);
                fs.Write(data, 0, 8);
            }
        }
    }
}