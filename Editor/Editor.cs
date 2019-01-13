using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();

            //Settings for toolStripStatusLabel
            toolStripStatusLabel1.BorderSides = ((ToolStripStatusLabelBorderSides)((((ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top)
            | ToolStripStatusLabelBorderSides.Right)
            | ToolStripStatusLabelBorderSides.Bottom)));
            toolStripStatusLabel1.BorderStyle = Border3DStyle.Sunken;
            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel1.TextAlign = ContentAlignment.MiddleLeft;
            toolStripStatusLabel2.BorderSides = ((ToolStripStatusLabelBorderSides)((((ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top)
            | ToolStripStatusLabelBorderSides.Right)
            | ToolStripStatusLabelBorderSides.Bottom)));
            toolStripStatusLabel2.BorderStyle = Border3DStyle.Sunken;
            toolStripStatusLabel2.Text = DateTime.Today.ToLongDateString();
            toolStripStatusLabel2.TextAlign = ContentAlignment.MiddleRight;
        }

        private void Editor_Load(object sender, EventArgs e)
        {

        }
        #region Events

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
   
                }
            }
        }

        private void printPreviewPrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, fontDialog1.Font, Brushes.Black, e.MarginBounds, StringFormat.GenericTypographic);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void searchAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SearchAndReplace(this).Show();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Search(this).Show();
        }

        private void fontToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length == 0)
            {
                fontDialog1.ShowDialog();
                richTextBox1.Font = fontDialog1.Font;
            }
            else
            {
                fontDialog1.ShowDialog();
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle;

                if (richTextBox1.SelectionFont.Bold == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Bold;
                }

                richTextBox1.SelectionFont = new Font(
                   currentFont.FontFamily,
                   currentFont.Size,
                   newFontStyle
                );
            }
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle;

                if (richTextBox1.SelectionFont.Italic == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Italic;
                }

                richTextBox1.SelectionFont = new Font(
                   currentFont.FontFamily,
                   currentFont.Size,
                   newFontStyle
                );
            }
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle;

                if (richTextBox1.SelectionFont.Underline == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Underline;
                }

                richTextBox1.SelectionFont = new Font(
                   currentFont.FontFamily,
                   currentFont.Size,
                   newFontStyle
                );
            }
        }

        //Show a word count in the status bar
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //Look for words in the richtextbox
            var words = richTextBox1.Text.Split(new char[] { ' ', ',', '.', '!', '\n', '?', ':', ';' });
            //Count words longer than or equal to one character
            var count = words.Count(word => word.Length >= 1);
            //Update the toolstripstatuslabel with the count
            toolStripStatusLabel1.Text = $"Number of words: { count }";
        }
        //Capture Link clicks in the richTextBox1 object
        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            //Open default browser and go to link in document
            Process.Start(e.LinkText);
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 1)
            {
                saveFileDialog1.ShowDialog();
            }

            richTextBox1.Text = "";
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName);
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            DataFormats.Format format = DataFormats.GetFormat(DataFormats.Bitmap);

            if (Clipboard.ContainsText(TextDataFormat.Rtf))
            {
                richTextBox1.Paste();
            }
            else if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                richTextBox1.Paste();
            }
            else if (this.richTextBox1.CanPaste(format))
            {
                richTextBox1.Paste(format);
            }
        }

        private void tsbUndo_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        #endregion
    }
}
