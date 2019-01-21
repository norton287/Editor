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
        private readonly string _fileName = @"\settings.xml";
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

        public void SaveSettings()
        {
            //Create settings.xml folder in AppData/RTFEditor
            if (!Directory.Exists(_workingDirectory))
                Directory.CreateDirectory(_workingDirectory);

            //Temporary to remove settings.xml when changes are made.
            File.Delete(_workingDirectory + _fileName);

            BuildSettings(); //Get the form window settings and apply add them to the List
            // opening serializer on the WindowSettings object 
            XmlSerializer mySerializer = new
            XmlSerializer(typeof(WindowSettings));
            // Create a StreamWriter object and output to settings.xml in app data folder  
            StreamWriter myWriter = new StreamWriter(_workingDirectory + _fileName);
            //Serialize settings to settings.xml
            mySerializer.Serialize(myWriter, _windowSettings);
            //Close settings.xml
            myWriter.Close();
        }

        public void LoadSettings()
        {
            // New instance of xml serializer on the WindowSettings object
            XmlSerializer mySerializer =
            new XmlSerializer(typeof(WindowSettings));
            // New FileStream to open and read settings.xml
            FileStream myFileStream =
            new FileStream(_workingDirectory + _fileName, FileMode.Open);
            // Call the Deserialize method and cast to the object type WindowSettings 
            _windowSettings = (WindowSettings)
            mySerializer.Deserialize(myFileStream);
            //Apply settings read to the Form Controls
            if (_ths != null)
            {
                _ths.fontDialog1.Color = Color.FromName(_windowSettings._fontDialogColor);
                _ths.richTextBox1.ForeColor = Color.FromName(_windowSettings._foreColor); 
                _ths.richTextBox1.Font = new Font(_windowSettings._fontFamily, _windowSettings._fontSize, _windowSettings._style, _windowSettings._graphicsUnit);
                _ths.Location = new Point(_windowSettings._windowX, _windowSettings._windowY);
                _ths.Size = new Size(_windowSettings._windowWidth, _windowSettings._windowHeight);
            }

            //Close the settings.xml file
            myFileStream.Close();
        }

        public WindowSettings GetList()
        {
            return _windowSettings;
        }
        #endregion
 
    }
}
