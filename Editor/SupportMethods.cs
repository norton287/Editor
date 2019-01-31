using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace RTFEditor
{
    public class SupportMethods
    {
        #region Fields
        private readonly string _workingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RTFEditor";
        private readonly string _programPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86) + @"\RTFEditor";
        private readonly string _fileName = @"\settings.txt";
        #endregion
        #region Objects
        //Create Editor object on open instance of it
        private readonly Editor _ths = (Editor)Application.OpenForms["Editor"];
        #endregion
        #region Collections
        private WindowSettings _windowSettings = new WindowSettings();
        #endregion
        #region Constructors
        public SupportMethods(Editor frm)
        {
            this._ths = frm;
        }

        public SupportMethods()
        {
        }
        #endregion

        #region Methods
        private void BuildSettings()
        {
            _windowSettings._fontDialogColor = _ths.richTextBox1.ForeColor.Name;
            _windowSettings._foreColor = _ths.richTextBox1.ForeColor.Name;
            _windowSettings._fontFamily = _ths.richTextBox1.Font.FontFamily.Name;
            _windowSettings._fontSize = _ths.richTextBox1.Font.Size;
            _windowSettings._graphicsUnit = _ths.richTextBox1.Font.Unit;
            _windowSettings._style = _ths.richTextBox1.Font.Style;
            _windowSettings._windowX = _ths.Location.X;
            _windowSettings._windowY = _ths.Location.Y;
            _windowSettings._windowHeight = _ths.Height;
            _windowSettings._windowWidth = _ths.Width;

        }

        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <exception cref="T:System.IO.IOException">The specified file is in use. -or-There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public void SaveSettings()
        {
            //Create settings.xml folder in AppData/RTFEditor
            if (!Directory.Exists(_workingDirectory))
                 try
                 {
                     Directory.CreateDirectory(_workingDirectory);
                 }
                 catch (IOException myIoException)
                 {
                     MessageBox.Show(@"Exception {0} could not access directory", myIoException.GetType().Name);
                 }
       
            //Temporary to remove settings.xml when changes are made.
            try
            {
                File.Delete(_workingDirectory + _fileName);
            }
            catch (UnauthorizedAccessException myUnauthorizedAccessException)
            {
                MessageBox.Show(@"Exception {0} could not lock file!",myUnauthorizedAccessException.GetType().Name);
            }

            BuildSettings(); //Get the form window settings and apply add them to the List
            // opening serializer on the WindowSettings object 
            JsonSerializer mySerializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            // Create a StreamWriter object and output to settings.txt in app data folder  
            try
            {
                var myWriter = new StreamWriter(_workingDirectory + _fileName);
                //Serialize settings to settings.txt
                using (JsonWriter writer = new JsonTextWriter(myWriter))
                {
                    writer.Formatting = Formatting.Indented;
                    mySerializer.Serialize(writer, _windowSettings);
                }

                //Close settings.xml
                myWriter.Close();
            }
            catch (DirectoryNotFoundException myDirectoryNotFoundException)
            {
                MessageBox.Show(@"Exception {0} Directory not found!", myDirectoryNotFoundException.GetType().Name);
            }
        }

        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public void LoadSettings()
        {
            // New instance of JSON serializer on the WindowSettings object
            JsonSerializer mySerializer = new JsonSerializer();
            // New FileStream to open and read settings.txt
            try
            {
                StreamReader myStream = File.OpenText(_workingDirectory + _fileName);
                WindowSettings _windowSettings = (WindowSettings) mySerializer.Deserialize(myStream, typeof(WindowSettings));

                //Close the settings.txt file
                myStream.Close();
                //Apply settings read to the Form Controls
                if (_ths != null)
                {
                    _ths.fontDialog1.Font = new Font(_windowSettings._fontFamily, _windowSettings._fontSize, _windowSettings._style, _windowSettings._graphicsUnit);
                    _ths.fontDialog1.Color = Color.FromName(_windowSettings._fontDialogColor);
                    _ths.richTextBox1.ForeColor = Color.FromName(_windowSettings._foreColor);
                    _ths.richTextBox1.Font = new Font(_windowSettings._fontFamily, _windowSettings._fontSize, _windowSettings._style, _windowSettings._graphicsUnit);
                    _ths.Location = new Point(_windowSettings._windowX, _windowSettings._windowY);
                    _ths.Size = new Size(_windowSettings._windowWidth, _windowSettings._windowHeight);
                }
            }
            catch (FileNotFoundException myFileNotFoundException)
            {
                MessageBox.Show(@"Exception {0} File not found exception!", myFileNotFoundException.GetType().Name);
            }
        }
        #endregion
    }
}
