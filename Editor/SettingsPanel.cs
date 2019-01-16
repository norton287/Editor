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
    public partial class SettingsPanel : Form
    {
        Editor ths;

        public SettingsPanel(Editor frm)
        {
            InitializeComponent();

            ths = frm;
        }

        private void SettingsPanel_Load(object sender, EventArgs e)
        {
            label1.Text = ths.richTextBox1.Font.FontFamily.Name;
            label2.Text = ths.Location.X.ToString();
            label3.Text = ths.Size.Width.ToString();
            label4.Text = ths.Size.Height.ToString();
            label5.Text = ths.richTextBox1.Font.Size.ToString();
        }
    }
}
