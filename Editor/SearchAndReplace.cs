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
    public partial class Form2 : Form
    {
        Editor ths;

        public Form2(Editor frm)
        {
            InitializeComponent();

            ths = frm;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim().Length > 0)
            {
                int start = ths.richTextBox1.Find(txtSearch.Text);

                ths.richTextBox1.Select(start, txtSearch.Text.Length);
            }

            if (txtReplace.Text != "")
            {
                ths.richTextBox1.Text = ths.richTextBox1.Text.Replace(txtSearch.Text, txtReplace.Text);
            }
        }
    }
}
