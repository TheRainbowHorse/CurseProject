using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Курсова.Form1;

namespace Курсова
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {   
            Graphics g = e.Graphics;
            Brush _textBrush = new SolidBrush(Color.FromArgb(64,64,64));

            // Get the item from the collection.
            TabPage _tabPage = tabControl1.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                //_textBrush = new SolidBrush(Color.Red);
                g.FillRectangle(new SolidBrush(SystemColors.MenuHighlight), e.Bounds);
                g.FillRectangle(new SolidBrush(SystemColors.GradientActiveCaption), 
                    new Rectangle(e.Bounds.Location.X + 3, e.Bounds.Location.Y + 3, e.Bounds.Size.Width - 5, e.Bounds.Size.Height - 6));
            }
            else
            {
                //g.FillRectangle(new SolidBrush(SystemColors.GradientInactiveCaption), e.Bounds);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Segoe UI", 11.0f, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            //tabControl1.Height = 450 - panel1.Height - 100;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            DialogResult = DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                Properties.Settings.Default.Save();
                DialogResult = DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Ця дія приведе ВСІ значення до значень за замовчуванням. Ця дія є незворотною. \nПродовжити?", "Exclamation", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) 
                == DialogResult.Yes)
            {
                Properties.Settings.Default.Reset();
            }
        }

        private bool validate()
        {
            DirectoryInfo directory = new DirectoryInfo(Properties.Settings.Default.exportPath);
            if (!directory.Exists)
            {
                tabControl1.SelectTab(3);
                textBoxExportPath.Focus();
                MessageBox.Show("Папка не існує", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            FileInfo file;
            try
            {
                file = new FileInfo(Properties.Settings.Default.chartFileName);
            }
            catch
            {
                tabControl1.SelectTab(3);
                textBoxChartFileName.Focus();
                MessageBox.Show("Неприпустиме ім'я файлу", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                file = new FileInfo(Properties.Settings.Default.pointsFileName);
            }
            catch
            {
                tabControl1.SelectTab(3);
                textBoxPointsFileName.Focus();
                MessageBox.Show("Неприпустиме ім'я файлу", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void buttonExportFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog1.SelectedPath = Properties.Settings.Default.exportPath;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxExportPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
