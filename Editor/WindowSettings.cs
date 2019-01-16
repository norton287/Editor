using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFEditor
{
    public class WindowSettings : IWindowSettings
    {
        public string fontFamily { get; set; }
        public float fontSize { get; set; }
        public GraphicsUnit GraphicsUnit { get; set; }
        public FontStyle Style { get; set; }
        public int windowX { get; set; }
        public int windowY { get; set; }
        public int windowHeight { get; set; }
        public int windowWidth { get; set; }
    }
}
