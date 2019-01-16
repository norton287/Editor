using System;
using System.Collections.Generic;
using System.Drawing;
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
        public string workingDirectory = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
        public string fileName = @"\settings.xml";
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
            // Insert code to set properties and fields of the object.  
            XmlSerializer mySerializer = new
            XmlSerializer(typeof(List<WindowSettings>));
            // To write to a file, create a StreamWriter object.  
            StreamWriter myWriter = new StreamWriter(workingDirectory + fileName);
            mySerializer.Serialize(myWriter, WindowSettings);
            myWriter.Close();
        }

        public void LoadSettings()
        {
            // Construct an instance of the XmlSerializer with the type  
            // of object that is being deserialized.  
            XmlSerializer mySerializer =
            new XmlSerializer(typeof(List<WindowSettings>));
            // To read the file, create a FileStream.  
            FileStream myFileStream =
            new FileStream(workingDirectory + fileName, FileMode.Open);
            // Call the Deserialize method and cast to the object type.  
            WindowSettings = (List<WindowSettings>)
            mySerializer.Deserialize(myFileStream);

            //Set window attributes from List
            foreach (var setting in WindowSettings)
            {
                ths.richTextBox1.Font = new System.Drawing.Font(setting.fontFamily, setting.fontSize);
                MessageBox.Show($"{ths.richTextBox1.Font}");
                ths.Location = new System.Drawing.Point(setting.windowX, setting.windowY);
                ths.Size = new Size(setting.windowWidth, setting.windowWidth);
            }
        }

        public IEnumerable<WindowSettings> GetList()
        {
            return WindowSettings;
        }
        #endregion
 
    }
}
