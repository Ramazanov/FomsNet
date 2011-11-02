using System.Windows.Forms;
using Foms.GUI.UserControl;

namespace Foms.GUI
{
    public class MultiLanguageForm : Form
    {
        public string GetString(string key)
        {
            return MultiLanguageStrings.GetString(Res, key);
        }

        protected virtual string Res
        {
            get
            {
                return GetType().Name;
            }
        }
    }
}