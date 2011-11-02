using System;

namespace Foms.GUI.UserControl
{
    public partial class SweetForm : SweetBaseForm
    {
        public SweetForm()
        {
            InitializeComponent();
        }

        public new string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = value;
                lblTitle.Text = value;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
   }
}