using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFEditor
{
    public interface IWindowSettings
    {
        string fontFamily { get; set; }
        float fontSize { get; set; }
        GraphicsUnit GraphicsUnit { get; set; }
        FontStyle Style { get; set; }
        int windowX { get; set; }
        int windowY { get; set; }
        int windowHeight { get; set; }
        int windowWidth { get; set; }
    }
}
