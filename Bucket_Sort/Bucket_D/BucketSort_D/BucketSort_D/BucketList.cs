using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketSort_D
{
    class MyBucketList : BucketList
    {
        public MyBucketList(string filename)
        {
            string text = System.IO.File.ReadAllText(filename);
            byteLength = Encoding.ASCII.GetBytes(text).Count();
            length = Math.Round((byteLength / 8));
        }

        int prevNode;
        int currentNode;
        int nextNode;

        public FileStream fs { get; set; }

        public override double Head()
        {
            Byte[] data = new Byte[8];
            currentNode = 0;
            prevNode = -1;
            fs.Seek(currentNode, SeekOrigin.Begin);
            fs.Read(data, 0, 8);
            double result = BitConverter.ToDouble(data, 0);
            nextNode = 8;
            return result;
        }
        public override double Next()
        {
            Byte[] data = new Byte[8];
            fs.Seek(nextNode, SeekOrigin.Begin);
            fs.Read(data, 0, 8);
            prevNode = currentNode;
            currentNode = nextNode;
            double result = BitConverter.ToDouble(data, 0);
            nextNode = nextNode + 8;
            return result;
        }

        public override double getData(int index)
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

        public override void Swap(double a, double b)
        {
            Byte[] data;
            fs.Seek(prevNode, SeekOrigin.Begin);
            data = BitConverter.GetBytes(a);
            fs.Write(data, 0, 8);
            fs.Seek(currentNode, SeekOrigin.Begin);
            data = BitConverter.GetBytes(b);
            fs.Write(data, 0, 8);
        }

        public override bool NotEmpty()
        {

            if (byteLength > 0)
            {
                return true;
            }
            else return false;
        }
    }
}