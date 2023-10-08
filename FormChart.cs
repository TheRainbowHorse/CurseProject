using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Курсова
{
    public partial class FormChart : Form
    {
        public FormChart(SeriesCollection series)
        {
            InitializeComponent();

            //Form1 f1 = new Form1();
            //chartF1.ContextMenuStrip = f1.chartContextMenuStrip1;
            foreach (Form f in Application.OpenForms)
                if (f.Name == "Form1")
                {
                    Form1 fm = (Form1)f;
                    chartF1.ContextMenuStrip = fm.chartContextMenuStrip1;
                }

            chartF1.Series[0].Points.Clear();
            foreach (DataPoint dp in series[0].Points.ToArray<DataPoint>())
                chartF1.Series[0].Points.Add(dp);
            chartF1.Series[1].Points.Clear();
            foreach (DataPoint dp in series[1].Points.ToArray<DataPoint>())
                chartF1.Series[1].Points.Add(dp);
        }

        private void FormChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form f in Application.OpenForms)
                if (f.Name == "Form1")
                {
                    Form1 fm = (Form1)f;
                    if (fm.showChartToolStripMenuItem.Checked == true)
                        fm.chart1.Visible = true;
                    fm.floatableChartToolStripMenuItem.Checked = false;
                    fm.hideFloatableChartToolStripMenuItem.Checked = false;

                    fm.chart1.Series[0].Points.Clear();
                    foreach (DataPoint dp in chartF1.Series[0].Points.ToArray<DataPoint>())
                        fm.chart1.Series[0].Points.Add(dp);
                    fm.chart1.Series[1].Points.Clear();
                    foreach (DataPoint dp in chartF1.Series[1].Points.ToArray<DataPoint>())
                        fm.chart1.Series[1].Points.Add(dp);
                }
            chartF1.Series.Clear();
        }
    }
}
