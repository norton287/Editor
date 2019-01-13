using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTFEditor
{
    public partial class SearchAndReplace : Form
    {
        Editor ths;

        public SearchAndReplace(Editor frm)
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

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
