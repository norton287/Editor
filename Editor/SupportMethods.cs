using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace RTFEditor
{
    public class SupportMethods
    {
        #region Properties
        Editor frm;
        public string workingDirectory = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
        public string fileName = @"\settings.xml";
        #endregion
        #region Collections
        public List<IWindowSettings> windowSettings = new List<IWindowSettings>();
        #endregion
        #region Methods
        public void BuildSettings()
        {
            windowSettings = new List<IWindowSettings>();

            windowSettings.Add(new WindowSettings
            {
                fontFamily = frm.richTextBox1.Font.FontFamily.Name,
                fontSize = frm.richTextBox1.Font.Size,
                GraphicsUnit = frm.richTextBox1.Font.Unit,
                Style = frm.richTextBox1.Font.Style,
                windowX = frm.Location.X,
                windowY = frm.Location.Y,
                windowHeight = frm.Height,
                windowWidth = frm.Width
            });
        }

        public void saveSettings()
        {
            BuildSettings(); //Get the form window settings and apply add them to the List
            // Insert code to set properties and fields of the object.  
            XmlSerializer mySerializer = new
            XmlSerializer(typeof(List<IWindowSettings>));
            // To write to a file, create a StreamWriter object.  
            StreamWriter myWriter = new StreamWriter(workingDirectory + fileName);
            mySerializer.Serialize(myWriter, windowSettings);
            myWriter.Close();
        }

        public void LoadSettings()
        {
            windowSettings = new List<IWindowSettings>();
            // Construct an instance of the XmlSerializer with the type  
            // of object that is being deserialized.  
            XmlSerializer mySerializer =
            new XmlSerializer(typeof(List<IWindowSettings>));
            // To read the file, create a FileStream.  
            FileStream myFileStream =
            new FileStream(workingDirectory + fileName, FileMode.Open);
            // Call the Deserialize method and cast to the object type.  
            windowSettings = (List<IWindowSettings>)
            mySerializer.Deserialize(myFileStream);

            //Set window attributes from List
            foreach (var setting in windowSettings)
            {
                frm.richTextBox1.Font = new System.Drawing.Font(setting.fontFamily, setting.fontSize);
                frm.richTextBox1.Location = new System.Drawing.Point(setting.windowX, setting.windowY);
                frm.richTextBox1.Height = setting.windowHeight;
                frm.richTextBox1.Width = setting.windowWidth;
            }
        }

        public IEnumerable<IWindowSettings> GetList()
        {
            return windowSettings;
        }
        #endregion
 
    }
}
