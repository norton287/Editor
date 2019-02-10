using System;
using System.Drawing;
using System.Windows.Forms;

namespace RTFEditor
{
    public partial class Search : Form
    {
        private readonly Editor _ths;

        public Search(Editor frm)
        {
            InitializeComponent();
            _ths = frm;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim().Length > 0)
            {
                int start = _ths.richTextBox1.Find(txtSearch.Text);

                _ths.richTextBox1.Select(start, txtSearch.Text.Length);

                _ths.richTextBox1.SelectionBackColor = Color.Yellow;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _ths.richTextBox1.SelectionBackColor = Color.White;

            Close();
        }

        private void Search_Load(object sender, EventArgs e)
        {

        }
    }
}
