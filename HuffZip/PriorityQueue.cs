using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffZip
{
    class PriorityQueue
    {
        public List<HuffmanNode> list;
        public int Count { get { return list.Count; } }

        public PriorityQueue()
        {
            list = new List<HuffmanNode>();
        }

        public PriorityQueue(int count)
        {
            list = new List<HuffmanNode>(count);
        }


        public void Enqueue(HuffmanNode x)
        {
            list.Add(x);
            int i = Count - 1;

            while (i > 0)
            {
                int p = (i - 1) / 2;
                if (list[p].frq <= x.frq) break;

                list[i] = list[p];
                i = p;
            }

            if (Count > 0) list[i] = x;
        }

        public HuffmanNode Dequeue()
        {
            HuffmanNode min = Peek();
            HuffmanNode root = list[Count - 1];
            list.RemoveAt(Count - 1);

            int i = 0;
            while (i * 2 + 1 < Count)
            {
                int a = i * 2 + 1;
                int b = i * 2 + 2;
                int c = b < Count && list[b].frq < list[a].frq ? b : a;

                if (list[c].frq >= root.frq) break;
                list[i] = list[c];
                i = c;
            }

            if (Count > 0) list[i] = root;
            return min;
        }

        public HuffmanNode Peek()
        {
            if (Count == 0) throw new InvalidOperationException("Queue is empty.");
            return list[0];
        }

        public void Poll()
        {
            if (Count == 0) throw new InvalidOperationException("Queue is empty.");
            list.RemoveAt(0);
        }

        public void Clear()
        {
            list.Clear();
        }
    }
}
