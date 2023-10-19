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
        SeriesCollection ownerSeries;
        public FormChart()
        {
            InitializeComponent();
        }
        public FormChart(SeriesCollection series)
        {
            InitializeComponent();

            ownerSeries = series;

            Form1 f1 = new Form1();
            this.Size = new Size(f1.chart1.Width + 16, f1.chart1.Height + 39);
            this.StartPosition = FormStartPosition.Manual;
            foreach (Form f in Application.OpenForms)
                if (f.Name == "Form1")
                {
                    Form1 fm = (Form1)f;
                    chartF1.ContextMenuStrip = fm.chartContextMenuStrip1;
                    if (fm.chart1.Legends[0].Enabled) chartF1.Legends[0].Enabled = true;
                    this.Owner = fm;
                }
            this.Location = new Point(this.Owner.Location.X + f1.chart1.Location.X, this.Owner.Location.Y + f1.chart1.Location.Y);
        }

        private void FormChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form f in Application.OpenForms)
                if (f.Name == "Form1")
                {
                    Form1 fm = (Form1)f;
                    if (fm.showChartToolStripMenuItem.Checked == true)
                    {
                        fm.panel3.Visible = true;
                    }
                    fm.floatableChartToolStripMenuItem.Checked = false;
                    fm.hideFloatableChartToolStripMenuItem.Checked = false;

                    fm.chart1.Series[0].Points.Clear();
                    foreach (DataPoint dp in chartF1.Series[0].Points.ToArray<DataPoint>())
                        fm.chart1.Series[0].Points.Add(dp);
                    fm.chart1.Series[1].Points.Clear();
                    foreach (DataPoint dp in chartF1.Series[1].Points.ToArray<DataPoint>())
                        fm.chart1.Series[1].Points.Add(dp);
                    fm.chart1.ChartAreas[0].AxisX.ScaleView.Position = chartF1.ChartAreas[0].AxisX.ScaleView.Position;
                    fm.chart1.ChartAreas[0].AxisX.ScaleView.Size = chartF1.ChartAreas[0].AxisX.ScaleView.Size;
                    fm.chart1.ChartAreas[0].AxisY.ScaleView.Position = chartF1.ChartAreas[0].AxisY.ScaleView.Position;
                    fm.chart1.ChartAreas[0].AxisY.ScaleView.Size = chartF1.ChartAreas[0].AxisY.ScaleView.Size;
                }
            chartF1.Series.Clear();
        }

        private void FormChart_Load(object sender, EventArgs e)
        {
            chartF1.Series[0].Points.Clear();
            foreach (DataPoint dp in ownerSeries[0].Points.ToArray<DataPoint>())
                chartF1.Series[0].Points.Add(dp);
            chartF1.Series[1].Points.Clear();
            foreach (DataPoint dp in ownerSeries[1].Points.ToArray<DataPoint>())
                chartF1.Series[1].Points.Add(dp);
            if (ownerSeries[1].IsVisibleInLegend) chartF1.Series[1].IsVisibleInLegend = true;
            chartF1.Series[0].LegendText = ownerSeries[0].LegendText;
            chartF1.Series[1].LegendText = ownerSeries[1].LegendText;

            foreach (Form f in Application.OpenForms)
                if (f.Name == "Form1")
                {
                    Form1 fm = (Form1)f;
                    chartF1.ChartAreas[0].AxisX.ScaleView.Position = fm.chart1.ChartAreas[0].AxisX.ScaleView.Position;
                    chartF1.ChartAreas[0].AxisX.ScaleView.Size = fm.chart1.ChartAreas[0].AxisX.ScaleView.Size;
                    chartF1.ChartAreas[0].AxisY.ScaleView.Position = fm.chart1.ChartAreas[0].AxisY.ScaleView.Position;
                    chartF1.ChartAreas[0].AxisY.ScaleView.Size = fm.chart1.ChartAreas[0].AxisY.ScaleView.Size;
                }

            chartF1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartF1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartF1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chartF1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

            chartF1.ChartAreas[0].CursorX.Interval = 0;
            chartF1.ChartAreas[0].CursorY.Interval = 0;
            chartF1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartF1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chartF1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = false;
            chartF1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = false;

            chartF1.ChartAreas[0].CursorX.LineColor = Color.Black;
            chartF1.ChartAreas[0].CursorX.LineWidth = 1;
            chartF1.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.Dot;
            chartF1.ChartAreas[0].CursorY.LineColor = Color.Black;
            chartF1.ChartAreas[0].CursorY.LineWidth = 1;
            chartF1.ChartAreas[0].CursorY.LineDashStyle = ChartDashStyle.Dot;
        }

        private void chartF1_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePoint = new Point(e.X, e.Y);
            chartF1.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, true);
            chartF1.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chartF1.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            chartF1.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
        }

        private void chartF1_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (chartF1.ChartAreas[0].AxisX.ScaleView.Size <= 1E-05 || chartF1.ChartAreas[0].AxisY.ScaleView.Size <= 1E-05)
            {
                chartF1.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                chartF1.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
            }
        }
    }
}
