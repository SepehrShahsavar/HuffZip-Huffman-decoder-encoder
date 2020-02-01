using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HuffZip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FilePath.Text = openFile.FileName;
            }
        }

        private void archive_btn_Click(object sender, EventArgs e)
        {
            if (FilePath.Text.Equals(String.Empty) || FilePath.Text.Equals(String.Empty)) 
            {
                MessageBox.Show("File Path or Output File Path is Empty !" , "Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
                return;
            }
            var fs = new FileStream(FilePath.Text, FileMode.Open, FileAccess.Read);
            var sr = new StreamReader(fs, Encoding.UTF8);

            //string line = String.Empty;

            string[] input = File.ReadAllLines(FilePath.Text, Encoding.UTF8);
            HuffmanTree huffmanTree = new HuffmanTree();
            string archived = huffmanTree.encoder(input);
            
            string treeFrq = "";
            string treeChar = "";
            List<int> frq = huffmanTree.get_CharFreq();
            List<char> chars = huffmanTree.get_CharArray();
            for (int i = 0; i < frq.Count; i++)
            {
                treeFrq += frq[i].ToString() + '-';
                treeChar += chars[i].ToString() + '-';
            }
            
            fs.Close();
            using (StreamWriter writer = new StreamWriter(output_txt.Text))
            {
                writer.WriteLine(treeFrq);
                writer.WriteLine(treeChar);
                writer.WriteLine(archived);
            }

            label3.Text = " NOICE !";
            FileInfo f = new FileInfo(FilePath.Text);
            long s1 = f.Length;
            FileInfo f2 = new FileInfo(output_txt.Text);
            long s2 = f2.Length;
            double cmprte = (double)s1 / (double)s2;
            rate_lbl.Text = cmprte.ToString();
        }

        private void extract_btn_Click(object sender, EventArgs e)
        {
            if (FilePath.Text.Equals(String.Empty))
            {
                MessageBox.Show("File Path is Empty !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var fs = new FileStream(FilePath.Text, FileMode.Open, FileAccess.Read);
            var sr = new StreamReader(fs, Encoding.UTF8);

            //string line = String.Empty;

            string[] input = File.ReadAllLines(FilePath.Text, Encoding.UTF8);
            HuffmanTree huffmanTree = new HuffmanTree();
            string extracted = huffmanTree.decoder(input);
            fs.Close();
            using (StreamWriter writer = new StreamWriter(output_txt.Text))
            {
                writer.WriteLine(extracted);
            }
        }

        private void OpenDialogOutPut_click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                output_txt.Text = openFile.FileName;
            }
            
        }
    }
}
