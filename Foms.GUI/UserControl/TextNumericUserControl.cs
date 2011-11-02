using System;
using System.Windows.Forms;

namespace Foms.GUI.UserControl
{
    public partial class TextNumericUserControl : System.Windows.Forms.UserControl
    {
        public event EventHandler NumberChanged;

        public TextNumericUserControl()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get { return textBoxNumeric.Text; }
            set { textBoxNumeric.Text = value; }
        }

        private void textBoxNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                e.Handled = (e.KeyChar != 8);
            }
        }

        private void textBoxNumeric_TextChanged(object sender, EventArgs e)
        {
            if (NumberChanged != null)
                NumberChanged(this, e);
        }
    }
}
