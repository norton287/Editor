using System.Collections.Generic;

namespace RTFEditor
{
    public class SupportMethods
    {
        public static List<Settings> settings = new List<Settings>();

        #region methods
        public void BuildSettings()
        {
            settings.Add(new Settings() { Id = 1, txtFont = System.Windows.Forms.Form.ActiveForm.Font.ToString(), windowSize = System.Windows.Forms.Form.ActiveForm.Size.ToString(), windowLocation = System.Windows.Forms.Form.ActiveForm.Location.ToString() });
        }

        #endregion
    }
}