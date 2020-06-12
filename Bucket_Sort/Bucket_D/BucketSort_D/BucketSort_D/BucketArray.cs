using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketSort_D
{
    class MyBucketArray : BucketArray
    {
        public MyBucketArray(string filename)
        {
            string text = System.IO.File.ReadAllText(filename);
            byteLenght = Encoding.ASCII.GetBytes(text).Count();
            length = Math.Round((byteLenght / 8));
        }

        public FileStream fs { get; set; }
        public override double this[int index]
        {
            get
            {
                Byte[] data = new Byte[8];
                fs.Seek(8 * index, SeekOrigin.Begin);
                fs.Read(data, 0, 8);
                double result = BitConverter.ToDouble(data, 0);
                return result;
            }
        }

        public override void Swap(int index, double a, double b)
        {
            Byte[] data = new Byte[16];
            BitConverter.GetBytes(a).CopyTo(data, 0);
            BitConverter.GetBytes(b).CopyTo(data, 8);
            fs.Seek(8 * (index - 1), SeekOrigin.Begin);
            fs.Write(data, 0, 16);
        }

        public override bool NotEmpty()
        {

            if (byteLenght > 0)
            {
                return true;
            }
            else return false;
        }
    }
}