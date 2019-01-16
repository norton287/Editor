using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace RTFEditor
{
    public class SupportMethods
    {
        #region Properties
        public string workingDirectory = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
        public string fileName = @"\settings.xml";
        //Create Editor object on open instance of it
        Editor ths = (Editor)Application.OpenForms["Editor"];
        #endregion
        #region Collections
        public List<WindowSettings> WindowSettings = new List<WindowSettings>();
        #endregion
        #region Methods
        public SupportMethods(Editor frm)
        {
            this.ths = frm;
        }

        public SupportMethods()
        {
        }

        public void BuildSettings()
        {
            WindowSettings = new List<WindowSettings>();

            WindowSettings.Add(new WindowSettings
            {
                fontFamily = ths.richTextBox1.Font.FontFamily.Name,
                fontSize = ths.richTextBox1.Font.Size,
                GraphicsUnit = ths.richTextBox1.Font.Unit,
                Style = ths.richTextBox1.Font.Style,
                windowX = ths.Location.X,
                windowY = ths.Location.Y,
                windowHeight = ths.Height,
                windowWidth = ths.Width
            });
        }

        public void SaveSettings()
        {
            WindowSettings.Clear();
            BuildSettings(); //Get the form window settings and apply add them to the List
            // opening serializer on the List<WindowsSettings> object 
            XmlSerializer mySerializer = new
            XmlSerializer(typeof(List<WindowSettings>));
            // Create a StreamWriter object and output to settings.xml in app data folder  
            StreamWriter myWriter = new StreamWriter(workingDirectory + fileName);
            //Serialize settings to settings.xml
            mySerializer.Serialize(myWriter, WindowSettings);
            //Close settings.xml
            myWriter.Close();
        }

        public void LoadSettings()
        {
            // New instance of xmlserializer on the List<WindowSettings> object
            XmlSerializer mySerializer =
            new XmlSerializer(typeof(List<WindowSettings>));
            // New FileStream to open and read settings.xml
            FileStream myFileStream =
            new FileStream(workingDirectory + fileName, FileMode.Open);
            // Call the Deserialize method and cast to the object type List<WindowSettings>  
            WindowSettings = (List<WindowSettings>)
            mySerializer.Deserialize(myFileStream);

            //Set window attributes from List
            foreach (var setting in WindowSettings)
            {
                ths.richTextBox1.Font = new System.Drawing.Font(setting.fontFamily, setting.fontSize);
                ths.Location = new System.Drawing.Point(setting.windowX, setting.windowY);
                ths.Size = new Size(setting.windowWidth, setting.windowHeight);
            }
        }

        public IEnumerable<WindowSettings> GetList()
        {
            return WindowSettings;
        }
        #endregion
 
    }
}
