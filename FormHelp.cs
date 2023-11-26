using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсова
{
    public partial class FormHelp : Form
    {
        int searchNumber = 1;
        int scrollTop;
        int scrollBottom;

        public FormHelp()
        {
            InitializeComponent();
            this.richTextBox1.HideSelection = false;
        }

        private void FormHelp_Load(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionIndent += 15;
            richTextBox1.SelectionRightIndent += 15;
            richTextBox1.SelectionFont = new Font("Segoe UI", 10, FontStyle.Regular);
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                if (richTextBox1.Lines[i] == richTextBox1.Lines[i].ToUpper())
                {
                    int start = richTextBox1.Find(richTextBox1.Lines[i]);
                    if (start != -1)
                    {
                        richTextBox1.Select(start, richTextBox1.Lines[i].Length);
                        richTextBox1.SelectionFont = new Font("Segoe UI", 13, (FontStyle)1);
                        richTextBox1.SelectionColor = Color.DarkRed;
                    }
                }
                if (i == 0 || i == 49 || i == 69)
                {
                    int start = richTextBox1.Find(richTextBox1.Lines[i]);
                    if (start != -1)
                    {
                        richTextBox1.Select(start, richTextBox1.Lines[i].Length);
                        richTextBox1.SelectionFont = new Font("Segoe UI", 16, (FontStyle)7);
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                label2.Text = 1.ToString();
                int index = richTextBox1.Text.ToLower().IndexOf(textBox1.Text.ToLower());
                if (index >= 0)
                {
                    richTextBox1.Select(richTextBox1.Text.ToLower().IndexOf(textBox1.Text.ToLower()), textBox1.Text.Length);
                    if (scrollTop > richTextBox1.SelectionStart || richTextBox1.SelectionStart > scrollBottom)
                        richTextBox1.ScrollToCaret();
                }
                else
                {
                    richTextBox1.SelectionLength = 0;
                }
                string[] splitted = richTextBox1.Text.ToLower().Split(new string[] { textBox1.Text.ToLower() }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch { }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            try
            {
                string[] splitted = richTextBox1.Text.ToLower().Split(new string[] { textBox1.Text.ToLower() }, StringSplitOptions.None);
                searchNumber = splitted.Length == 2 ? 1 : searchNumber == splitted.Length - 1 ? 1 : searchNumber + 1;
                label2.Text = searchNumber.ToString();
                int index = 0;
                for (int i = 0; i < searchNumber; i++)
                {
                    index += splitted[i].Length;
                    if (i > 0) index += textBox1.Text.Length;
                }
                if (index > 0 || searchNumber == 1)
                {
                    if (richTextBox1.Text.Remove(0, textBox1.Text.Length) == textBox1.Text) index += 2;
                    richTextBox1.Select(index, textBox1.Text.Length);
                    if (scrollTop > richTextBox1.SelectionStart || richTextBox1.SelectionStart > scrollBottom)
                        richTextBox1.ScrollToCaret();
                }
                else
                {
                    richTextBox1.SelectionLength = 0;
                }
            }
            catch { }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (treeView1.SelectedNode.FullPath)
            {
                case "Синтаксис":
                case "Синтаксис\\Базовий синтаксис":
                    richTextBox1.SelectionStart = 0;
                    break;
                case "Синтаксис\\Математичні функції":
                    richTextBox1.SelectionStart = richTextBox1.Find("СИНТАКСИС МАТЕМАТИЧНИХ ФУНКЦІЙ");
                    break;
                case "Синтаксис\\Константи":
                    richTextBox1.SelectionStart = richTextBox1.Find("КОНСТАНТИ");
                    break;

                case "Базове керування":
                case "Базове керування\\Методи":
                    richTextBox1.SelectionStart = richTextBox1.Find("МЕТОДИ РОЗВ'ЯЗКУ");
                    break;
                case "Базове керування\\Способи експорта даних":
                    richTextBox1.SelectionStart = richTextBox1.Find("СПОСОБИ ЕКСПОРТУ ФУНКЦІЙ");
                    break;

                case "Просунуте керування":
                case "Просунуте керування\\Умови":
                    richTextBox1.SelectionStart = richTextBox1.Find("КЕРУВАННЯ ПОЧАТКОВИМИ УМОВАМИ");
                    break;
                case "Просунуте керування\\Розрахунок":
                    richTextBox1.SelectionStart = richTextBox1.Find("ПАРАМЕТРИ РОЗРАХУНКУ");
                    break;
                case "Просунуте керування\\Графік":
                    richTextBox1.SelectionStart = richTextBox1.Find("НАЛАШТУВАННЯ ГРАФІКІВ");
                    break;
                case "Просунуте керування\\Експорт":
                    richTextBox1.SelectionStart = richTextBox1.Find("ПАРАМЕТРИ ЗБЕРЕЖЕННЯ ДАНИХ");
                    break;
            }
            richTextBox1.SelectionLength = 0;
            richTextBox1.ScrollToCaret();
        }

        [DllImport("user32.dll")]
        private static extern bool GetScrollInfo(IntPtr hWnd, int nBar, ref SCROLLINFO ScrollInfo);

        [StructLayout(LayoutKind.Sequential)]
        public struct SCROLLINFO
        {
            public int cbSize;
            public int fMask;
            public int nMin;
            public int nMax;
            public int nPage;
            public int nPos;
            public int nTrackPos;
        }

        private const int SB_VERT = 0x1;
        private const int SB_HORZ = 0x0;
        private const int SIF_PAGE = 0x2;
        private const int SIF_POS = 0x4;
        private const int SIF_RANGE = 0x1;
        private const int SIF_TRACKPOS = 0x10;
        private const int SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS);

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            SCROLLINFO si = new SCROLLINFO();
            si.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(si);
            si.fMask = SIF_ALL;

            GetScrollInfo(richTextBox1.Handle, 1, ref si);
            scrollTop = si.nPos;
            scrollBottom = si.nMax;
        }
    }
}
