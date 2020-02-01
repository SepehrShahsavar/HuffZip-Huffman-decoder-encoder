using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffZip
{
    public class HuffmanNode : IComparable<HuffmanNode>
    {
        public char data { get; set; }
        public int frq { get; set; }
        public HuffmanNode left { get; set; }
        public HuffmanNode right { get; set; }

        public int CompareTo(HuffmanNode other)
        {
            return this.frq - other.frq;
        }
    }
}
