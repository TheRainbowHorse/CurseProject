using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсова
{
    public partial class FormModalShowManyMethods : Form
    {
        public FormModalShowManyMethods()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
                if (f.Name == "Form1")
                {
                    Form1 fm = (Form1)f;
                    fm.countOfMethods = Convert.ToInt32(numericUpDown1.Value);
                }
            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
