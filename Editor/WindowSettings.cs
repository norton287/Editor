using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFEditor
{
    public class WindowSettings
    {
        public string _fontFamily { get; set; }
        public float _fontSize { get; set; }
        public GraphicsUnit _graphicsUnit { get; set; }
        public FontStyle _style { get; set; }
        public int _windowX { get; set; }
        public int _windowY { get; set; }
        public int _windowHeight { get; set; }
        public int _windowWidth { get; set; }
    }
}
