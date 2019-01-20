using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RTFEditor
{
    public partial class Editor : Form
    {
        private readonly string _workingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RTFEditor";
        private readonly string _fileName = @"\settings.xml";

        public Editor()
        {
            InitializeComponent();

        }

        private void Editor_Load(object sender, EventArgs e)
        {
            //Settings for toolStripStatusLabel
            toolStripStatusLabel1.BorderSides = (((ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top)
            | ToolStripStatusLabelBorderSides.Right)
            | ToolStripStatusLabelBorderSides.Bottom);
            toolStripStatusLabel1.BorderStyle = Border3DStyle.Sunken;
            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel1.TextAlign = ContentAlignment.MiddleLeft;
            toolStripStatusLabel2.BorderSides = (((ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top)
            | ToolStripStatusLabelBorderSides.Right)
            | ToolStripStatusLabelBorderSides.Bottom);
            toolStripStatusLabel2.BorderStyle = Border3DStyle.Sunken;
            toolStripStatusLabel2.Text = DateTime.Today.ToLongDateString();
            toolStripStatusLabel2.TextAlign = ContentAlignment.MiddleRight;

            //Create an new SupportMethods object
            var support = new SupportMethods(this);
            
            //Check for settings.xml in App Data folder
            if (File.Exists(_workingDirectory + _fileName))
                support.LoadSettings(); //load saved settings from xml
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
                if (fontDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                else
                    richTextBox1.Font = fontDialog1.Font;
            }
            else
            {
                if (fontDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                else
                    richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont == null)
            {
                return;
            }

            var style = richTextBox1.SelectionFont.Style;

            if (richTextBox1.SelectionFont.Bold)
            {
                style &= ~FontStyle.Bold;
            }
            else
            {
                style |= FontStyle.Bold;

            }
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, style);
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont == null)
            {
                return;
            }
            var style = richTextBox1.SelectionFont.Style;

            if (richTextBox1.SelectionFont.Italic)
            {
                style &= ~FontStyle.Italic;
            }
            else
            {
                style |= FontStyle.Italic;
            }
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, style);
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont == null)
            {
                return;
            }

            var style = richTextBox1.SelectionFont.Style;

            if (richTextBox1.SelectionFont.Underline)
            {
                style &= ~FontStyle.Underline;
            }
            else
            {
                style |= FontStyle.Underline;
            }
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, style);
        }

        //Show a word count in the status bar
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //Look for words in the richtextbox
            var words = richTextBox1.Text.Split(new char[] { ' ', ',', '.', '!', '\n', '?', ':', ';' });
            //Count words longer than or equal to one character
            var count = words.Count(word => word.Length >= 1);
            //Update the toolstripstatuslabel with the count
            toolStripStatusLabel1.Text = $@"Number of words: { count }";
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
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
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
            else
            {
                return;
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
            if (Clipboard.ContainsText(TextDataFormat.Rtf))
            {
                richTextBox1.Paste();
            }
            else if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                richTextBox1.Paste();
            }
        }

        private void tsbUndo_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void tsbInsertImage_Click(object sender, EventArgs e)
        {
            try
            {
                var open = new OpenFileDialog
                {
                    Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
                };
                if (open.ShowDialog() != DialogResult.OK) return;
                var img = Image.FromFile(open.FileName);
                Clipboard.SetImage(img);

                richTextBox1.SelectionStart = 0;
                richTextBox1.Paste();

                Clipboard.Clear();
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed loading image");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 1)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SaveFile(saveFileDialog1.FileName);
                }
                else
                {
                    return;
                }
            }

            var support = new SupportMethods();
            //Save settings to settings.xml before exit
            support.SaveSettings();
            Close();
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (richTextBox1.Text.Length > 1)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SaveFile(saveFileDialog1.FileName);
                }
                else
                {
                    return;
                }
            }

            var support = new SupportMethods();
            //Save settings to settings.xml before exit
            support.SaveSettings();
        }
        #endregion
    }
}
