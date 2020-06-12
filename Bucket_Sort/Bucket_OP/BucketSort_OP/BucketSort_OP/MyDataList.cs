using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketSort_OP
{
    class MyDataList : DataList 
    {
        public class MyLinkedListNode
        {
            public MyLinkedListNode nextNode { get; set; }
            public double data { get; set; }
            public MyLinkedListNode(double data)
            {
                this.data = data;
            }
        }

        MyLinkedListNode headNode;
        MyLinkedListNode prevNode;
        MyLinkedListNode currentNode;

        public MyDataList(int n, int seed)
        {
            length = n;
            Random rand = new Random(seed);
            headNode = new MyLinkedListNode(rand.NextDouble());
            currentNode = headNode;
            for (int i = 1; i < length; i++)
            {
                prevNode = currentNode;
                currentNode.nextNode = new MyLinkedListNode(rand.NextDouble());
                currentNode = currentNode.nextNode;
            }
            currentNode.nextNode = null;
        }
        public override double Head()
        {
            currentNode = headNode;
            prevNode = null;
            return currentNode.data;
        }

        public override double Next()
        {
            prevNode = currentNode;
            if (currentNode.nextNode != null)
            {
                currentNode = currentNode.nextNode;
                return currentNode.data;
            }
            else return -1;
        }

        public override bool Current()
        {
            if (currentNode != null)
            {
                return true;
            }
            else return false;
        }

        public override void AddRange(LinkedList<double>[] buckets)
        {
            bool temp = false;
            for (int i = 0; i < buckets.Length; i++)
            {
                foreach (double element in buckets[i])
                {
                    if (temp)
                    {
                        prevNode = currentNode;
                        currentNode.nextNode = new MyLinkedListNode(element);
                        currentNode = currentNode.nextNode;
                    }
                    else
                    {
                        headNode = new MyLinkedListNode(element);
                        currentNode = headNode;
                        temp = true;
                    }
                }
            }
            currentNode.nextNode = null;
        }
    }
}