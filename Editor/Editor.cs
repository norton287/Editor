using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RTFEditor
{
    public partial class Editor : Form
    {
        private readonly string _workingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RTFEditor";
        private readonly string _fileName = @"\settings.txt";

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
                support.LoadSettings(); //load saved settings from json

            Text = @"RTFEditor - New Document";
            Update();
        }
        #region Events

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            try
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName);
                Text = @"RTFEditor - " + saveFileDialog1.FileName;
                Update();
            }
            catch (Exception myException)
            {
                MessageBox.Show(myException.Message);
                throw;
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            try
            {
                richTextBox1.LoadFile(openFileDialog1.FileName);
                Text = @"RTFEditor - " + openFileDialog1.FileName;
                Update();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void PrintPreviewPrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();

        }

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, fontDialog1.Font, Brushes.Black, e.MarginBounds, StringFormat.GenericTypographic);
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void SearchAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SearchAndReplace(this).Show();
        }

        private void SearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Search(this).Show();
        }

        private void FontToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length == 0)
            {
                if (fontDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                richTextBox1.Font = fontDialog1.Font;
                richTextBox1.ForeColor = fontDialog1.Color;
            }
            else
            {
                if (fontDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                richTextBox1.SelectionFont = fontDialog1.Font;
                richTextBox1.ForeColor = fontDialog1.Color;
            }
        }

        private void BoldToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void ItalicToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void HnderlineToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            //Look for words in the richtextbox
            var words = richTextBox1.Text.Split(' ', ',', '.', '!', '\n', '?', ':', ';');
            //Count words longer than or equal to one character
            var count = words.Count(word => word.Length >= 1);
            //Update the toolstripstatuslabel with the count
            toolStripStatusLabel1.Text = $@"Number of words: { count }";
        }
        //Capture Link clicks in the richTextBox1 object
        private void RichTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            //Open default browser and go to link in document
            Process.Start(e.LinkText);
        }

        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 1)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    richTextBox1.Text = "";
                    Text = @"RTFEditor - New Document";
                    Update();
                }
                else if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SaveFile(saveFileDialog1.FileName);
                    richTextBox1.Text = "";
                    Text = @"RTFEditor - New Document";
                    Update();
                }
            }
            else
            {
                Text = @"RTFEditor - New Document";
                Update();
            }
        }

        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName);
                    Text = @"RTFEditor - " + openFileDialog1.FileName;
                    Update();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName);
                Text = @"RTFEditor - " + saveFileDialog1.FileName;
                Update();

            }
            else
            {
            }
        }

        private void PrintToolStripButton_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void CutToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void CopyToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void PasteToolStripButton_Click(object sender, EventArgs e)
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

        private void TsbUndo_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void TsbInsertImage_Click(object sender, EventArgs e)
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

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
                Close();
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            var support = new SupportMethods();

            if (richTextBox1.Text.Length > 1)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SaveFile(saveFileDialog1.FileName);
                    //Save settings to settings.txt before exit
                    support.SaveSettings();
                }
                else
                {
                    //Save settings to settings.txt before exit
                    support.SaveSettings();
                }
            }
            else
            {
                //Save settings to settings.txt before exit
                support.SaveSettings();
            }
        }
        #endregion
    }
}
