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

        private void FormModalShowManyMethods_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            
            foreach (Form f in Application.OpenForms)
                if (f.Name == "Form1")
                {
                    Form1 f1 = (Form1)f;
                    foreach (var item in f1.comboBox1.Items)
                    {
                        comboBox1.Items.Add(item);
                        comboBox2.Items.Add(item);
                        comboBox3.Items.Add(item);
                    }
                    comboBox1.SelectedItem = f1.comboBox1.SelectedItem;
                    comboBox2.SelectedItem = f1.comboBox2.SelectedItem;
                    comboBox3.SelectedItem = f1.comboBox3.SelectedItem;
                }

            numericUpDown1_ValueChanged(sender, e);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
                if (f.Name == "Form1")
                {
                    Form1 f1 = (Form1)f;
                    f1.comboBox1.SelectedItem = comboBox1.SelectedItem;
                    if (comboBox2.Enabled)
                        f1.comboBox2.SelectedItem = comboBox2.SelectedItem;
                    if (comboBox3.Enabled)
                        f1.comboBox3.SelectedItem = comboBox3.SelectedItem;
                }
            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(numericUpDown1.Value))
            {
                case 1:
                    comboBox2.Enabled = false;
                    comboBox3.Enabled = false;
                    break;
                case 2:
                    comboBox2.Enabled = true;
                    comboBox3.Enabled = false;
                    break;
                case 3:
                    comboBox2.Enabled = true;
                    comboBox3.Enabled = true;
                    break;
            }
        }
    }
}
