using System;
using System.Windows.Forms;

namespace RTFEditor
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
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Search_Load(object sender, EventArgs e)
        {

        }
    }
}
