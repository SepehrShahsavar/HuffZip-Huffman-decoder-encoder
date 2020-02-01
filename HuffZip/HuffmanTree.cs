using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffZip
{
    public class HuffmanTree
    {
        public List<char> CharArray = new List<char>();
        public List<int> CharFreq = new List<int>();
        private List<string> HuffCodes = new List<string>();
        private string output = "";
        HuffmanNode root = new HuffmanNode();

        public List<int> get_CharFreq() { return CharFreq; }

        public List<char> get_CharArray() { return CharArray; }
        public void setCharFrq(string[] lines)
        {
            foreach (string line in lines)
            {
                
                char[] tmp = line.ToCharArray();
                for (int i = 0; i < tmp.Length; i++)
                {
                    if (CharArray.Contains(tmp[i]))
                    {
                        int index = CharArray.IndexOf(tmp[i]);
                        CharFreq[index]++;
                    }
                    else
                    {
                        CharArray.Add(tmp[i]);
                        CharFreq.Add(1);
                    }
                }
            }
        }
        public void createTree()
        {
            PriorityQueue q = new PriorityQueue();
            for (int i = 0; i < CharArray.Count; i++)
            {
                HuffmanNode hn = new HuffmanNode();

                hn.data = CharArray[i];
                hn.frq = CharFreq[i];

                hn.left = null;
                hn.right = null;

                q.Enqueue(hn);
            }

            while (q.Count > 1)
            {

                // first min extract. 
                HuffmanNode x = q.Peek();

                q.Poll();
                // second min extarct. 
                HuffmanNode y = q.Peek();
                q.Poll();

                // new node f which is equal 
                HuffmanNode f = new HuffmanNode();

                // to the sum of the frequency of the two nodes 
                // assigning values to the f node. 
                f.frq = x.frq + y.frq;
                f.data = '-';

                // first extracted node as left child. 
                f.left = x;

                // second extracted node as the right child. 
                f.right = y;

                // marking the f node as the root node. 
                root = f;

                // add this node to the priority-queue. 
                q.Enqueue(f);
            }
        }

        public string encoder(string[] lines)
        {
            setCharFrq(lines);
            createTree();
            CharArray.ForEach(x => { HuffCodes.Add(""); });
            generate_code(root, "");
            foreach (string line in lines)
            {
                char[] orginal = line.ToCharArray();
                foreach (char c in orginal)
                {
                    int index = CharArray.IndexOf(c);
                    output += HuffCodes[index];
                }
                output += "\n";
            }
            return output;

        }

        private void generate_code(HuffmanNode node , string s)
        {
            if (node != null)
            {
                if (node.left != null)
                {
                    generate_code(node.left, s + "0");
                }

                if(node.right != null)
                {
                    generate_code(node.right, s + "1");
                }

                if(node.left == null && node.right == null)
                {
                    char tmp = node.data;
                    int index = CharArray.IndexOf(tmp);
                    HuffCodes[index] = s;
                }
            }
        }

        public string decoder(string[] lines)
        {
            string output = "";
            string[] s = lines[1].Split('-');
            string t = "";
            foreach (string character in s)
            {
                t += character;
            }
            retrive_tree(lines[0].Split('-') ,t.ToCharArray());

            HuffmanNode tmp = root;
            if (root == null)
            {
                return "";
            }
            string S = lines[2];
            int index = 0;

            HuffmanNode node = root;
            while (S.Length > 0)
            {
                if (S[0] == '1')
                {
                    tmp = tmp.right;
                    S = S.Substring(1);
                }
                else
                {
                    tmp = tmp.left;
                    S = S.Substring(1);
                }
                if (tmp.left == null && tmp.right == null)
                {
                    output += tmp.data;
                    tmp = root;
                }

            }
            return output;
        }

        private void retrive_tree(string[] frqs , char[] chars)
        {
            
            for (int i = 0; i < frqs.Length - 1; i++)
            {
                CharFreq.Add(Int32.Parse(frqs[i]));
                CharArray.Add(chars[i]); 
            }
            createTree();
        }
    }
}
