using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Search : Form
    {
        Editor ths;

        public Search(Editor frm)
        {
            InitializeComponent();
            ths = frm;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim().Length > 0)
            {
                int start = ths.richTextBox1.Find(txtSearch.Text);

                ths.richTextBox1.Select(start, txtSearch.Text.Length);

                ths.richTextBox1.SelectionBackColor = Color.Yellow;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            ths.richTextBox1.SelectionBackColor = Color.White;
        }
    }
}
