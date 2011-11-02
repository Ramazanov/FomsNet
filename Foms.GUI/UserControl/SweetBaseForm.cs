using System.Drawing;
using System.Windows.Forms;

namespace Foms.GUI.UserControl
{
    public partial class SweetBaseForm : Form
    {
        public SweetBaseForm()
        {
            InitializeComponent();
        }

        public string GetString(string key)
        {
            return GetString(Res, key);
        }

        public string GetString(string res, string key)
        {
            return   key;
        }

        protected virtual string Res
        {
            get
            {
                return GetType().Name;
            }
        }

        private static Color LabelColor
        {
            get
            {
                return Color.FromArgb(0, 88, 56);
            }
        }

        #region Notifications
        public void Notify(string key)
        {
            string caption = GetString("SweetBaseForm", "notification");
            string message = GetString(key) ?? key;
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Warn(string key)
        {
            string caption = GetString("SweetBaseForm", "warning");
            string message = GetString(key) ?? key;
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public bool Confirm(string key)
        {
            string caption = GetString("SweetBaseForm", "confirmation");
            string message = GetString(key) ?? key;
            return DialogResult.Yes == MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public void Fail(string key)
        {
            string caption = GetString("SweetBaseForm", "error");
            string message = GetString(key) ?? key;
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion Notifications
    }
    public class MultiLanguageStrings
    {

        public static string GetString(string s1, string s2)
        {
            return s2;
        }
    }
}