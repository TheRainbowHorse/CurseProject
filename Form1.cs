using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Math;

namespace Курсова
{
    public partial class Form1 : Form
    {
        int finished = 1;
        bool stopFlag = false;
        bool limitStopFlag = false;
        bool ignoreLimitFlag = false;
        ManualResetEvent _event = new ManualResetEvent(false);
        List<List<double[]>> points = new List<List<double[]>>();
        MySettings settings = new MySettings();

        bool limitReachedExeption = false;
        bool initConditionsError = false;

        private Conditions conditions;
        public Form1()
        {
            InitializeComponent();
            settings.init(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            settings.apply();

            //panel1.Location = new Point(611, 44);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            searchToolStripComboBox.SelectedIndex = 0;

            textBox2.GotFocus += textBoxAutoSizeOn; textBox2.LostFocus += textBoxAutoSizeOff; textBox2.TextChanged += textBoxAutoSizeOn;
            textBox3.GotFocus += textBoxAutoSizeOn; textBox3.LostFocus += textBoxAutoSizeOff; textBox3.TextChanged += textBoxAutoSizeOn;
            textBox4.GotFocus += textBoxAutoSizeOn; textBox4.LostFocus += textBoxAutoSizeOff; textBox4.TextChanged += textBoxAutoSizeOn;
            textBox5.GotFocus += textBoxAutoSizeOn; textBox5.LostFocus += textBoxAutoSizeOff; textBox5.TextChanged += textBoxAutoSizeOn;
            textBox6.GotFocus += textBoxAutoSizeOn; textBox6.LostFocus += textBoxAutoSizeOff; textBox6.TextChanged += textBoxAutoSizeOn;

            textBox2.GotFocus += removePlaceHolder;
            textBox3.GotFocus += removePlaceHolder;
            textBox4.GotFocus += removePlaceHolder;
            textBox5.GotFocus += removePlaceHolder;
            textBox6.GotFocus += removePlaceHolder;
            textBox2.LostFocus += addPlaceHolder;
            textBox3.LostFocus += addPlaceHolder;
            textBox4.LostFocus += addPlaceHolder;
            textBox5.LostFocus += addPlaceHolder;
            textBox6.LostFocus += addPlaceHolder;

            chart1.ChartAreas[0].CursorX.Interval = 0;
            chart1.ChartAreas[0].CursorY.Interval = 0;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = false;
            chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = false;

            chart1.ChartAreas[0].CursorX.LineColor = Color.Black;
            chart1.ChartAreas[0].CursorX.LineWidth = 1;
            chart1.ChartAreas[0].CursorY.LineColor = Color.Black;
            chart1.ChartAreas[0].CursorY.LineWidth = 1;

            saveFileDialog1.RestoreDirectory = true;

            listSearchToolStripTextBox.TextChanged += listSearch;
            listSearchToolStripTextBox.LostFocus += (sndr, ev) => { listSearchToolStripTextBox.Text = ""; };
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.None;
            if (!Properties.Settings.Default.hideHelpWindowOnStart)
                result = MessageBox.Show("Перед початком роботи з програмою рекомендуємо прочитати довідку з використання. " +
                    "Перейти до довідки?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                FormHelp formHelp = new FormHelp();
                formHelp.ShowDialog();
            }
        }

        private class Conditions
        {
            public string equation;
            public double xmin, xmax, y0, x0, h;
            public int methodNumber;

            public Conditions(string equation, double x0, double y0, double xmin, double xmax, double h, int methodNumber)
            {
                this.equation = equation;
                this.xmin = xmin;
                this.xmax = xmax;
                this.y0 = y0;
                this.x0 = x0;
                this.h = h;
                this.methodNumber = methodNumber;
            }
        }

        public class MySettings
        {
            Form1 f;

            public void init(Form1 form)
            {
                f = form;
            }
            public void apply()
            {
                f.allowCopyByClick();

                f.saveFileDialog1.InitialDirectory = Path.GetFullPath(Properties.Settings.Default.exportPath);

                if (Properties.Settings.Default.exportPath.Contains("./"))
                    Properties.Settings.Default.exportPath = Path.GetFullPath(Properties.Settings.Default.exportPath);

                if (Properties.Settings.Default.showMarker)
                    foreach (var series in f.chart1.Series)
                        series.MarkerStyle = MarkerStyle.Circle;
                else
                    foreach (var series in f.chart1.Series)
                        series.MarkerStyle = MarkerStyle.None;

                f.chart1.ChartAreas[0].AxisY.LabelStyle.Format = "0.";
                for (int i = 0; i < Properties.Settings.Default.decimalPlacesChartY; i++)
                    f.chart1.ChartAreas[0].AxisY.LabelStyle.Format = f.chart1.ChartAreas[0].AxisY.LabelStyle.Format + "0";

                if (f.chart1.Series[0].Points.Count != 0)
                    f.chart1.Legends[0].Enabled = !Properties.Settings.Default.hideLegend;

                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name == "FormChart")
                    {
                        FormChart fc = (FormChart)f;
                        if (Properties.Settings.Default.showMarker)
                            foreach (var series in fc.chartF1.Series)
                                series.MarkerStyle = MarkerStyle.Circle;
                        else
                            foreach (var series in fc.chartF1.Series)
                                series.MarkerStyle = MarkerStyle.None;

                        fc.chartF1.ChartAreas[0].AxisY.LabelStyle.Format = "0.";
                        for (int i = 0; i < Properties.Settings.Default.decimalPlacesChartY; i++)
                            fc.chartF1.ChartAreas[0].AxisY.LabelStyle.Format = fc.chartF1.ChartAreas[0].AxisY.LabelStyle.Format + "0";

                        if (fc.chartF1.Series[0].Points.Count != 0)
                            fc.chartF1.Legends[0].Enabled = !Properties.Settings.Default.hideLegend;
                    }
                }

                f.showMethods();

                f.initConditions();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Focus();
                MessageBox.Show("Введіть рівняння", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox6.Text.Contains('-'))
            {
                textBox6.Focus();
                MessageBox.Show("Крок не може бути від'ємним", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double xmin;
            double xmax;
            double y0;
            double x0;
            double h;

            button3.Enabled = true;
            button3.Focus();
            panelConditions.Enabled = false;
            showManyMethodsToolStripMenuItem.Enabled = false;
            settingsToolStripMenuItem.Enabled = false;
            listContextMenuStrip.Enabled = false;
            finished = 2 - Properties.Settings.Default.countOfMethods;
            _event.Reset();
            limitStopFlag = false;
            ignoreLimitFlag = false;

            try
            {
                if (Properties.Settings.Default.useDefaultConditions)
                {
                    if (Properties.Settings.Default.defaultX0 != "" && String.IsNullOrEmpty(textBox2.Text))
                        x0 = double.Parse(Properties.Settings.Default.defaultX0.Replace('.', ','));
                    else
                        x0 = double.Parse(textBox2.Text.Replace('.', ','));
                    if (Properties.Settings.Default.defaultY0 != "" && String.IsNullOrEmpty(textBox3.Text))
                        y0 = double.Parse(Properties.Settings.Default.defaultY0.Replace('.', ','));
                    else
                        y0 = double.Parse(textBox3.Text.Replace('.', ','));
                    if (Properties.Settings.Default.defaultXmin != "" && String.IsNullOrEmpty(textBox4.Text))
                        xmin = double.Parse(Properties.Settings.Default.defaultXmin.Replace('.', ','));
                    else
                        xmin = double.Parse(textBox4.Text.Replace('.', ','));
                    if (Properties.Settings.Default.defaultXmax != "" && String.IsNullOrEmpty(textBox5.Text))
                        xmax = double.Parse(Properties.Settings.Default.defaultXmax.Replace('.', ','));
                    else
                        xmax = double.Parse(textBox5.Text.Replace('.', ','));
                    if (Properties.Settings.Default.defaultH != "" && String.IsNullOrEmpty(textBox6.Text))
                        h = double.Parse(Properties.Settings.Default.defaultH.Replace('.', ','));
                    else
                        h = double.Parse(textBox6.Text.Replace('.', ','));
                }
                else
                {
                    x0 = double.Parse(textBox2.Text.Replace('.', ','));
                    y0 = double.Parse(textBox3.Text.Replace('.', ','));
                    xmin = double.Parse(textBox4.Text.Replace('.', ','));
                    xmax = double.Parse(textBox5.Text.Replace('.', ','));
                    h = double.Parse(textBox6.Text.Replace('.', ','));
                }

            }
            catch
            {
                MessageBox.Show("Помилка в початкових умовах", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button3.Enabled = false;
                panelConditions.Enabled = true;
                showManyMethodsToolStripMenuItem.Enabled = true;
                settingsToolStripMenuItem.Enabled = true;
                listContextMenuStrip.Enabled = true;
                return;
            }
            string equation = textBox1.Text;//x+cos(y/sqrt(0.7))

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Legends[0].Enabled = !Properties.Settings.Default.hideLegend;
            chart1.Series[0].IsVisibleInLegend = true;
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

            int dpx;
            if (GetDecimalDigitsCount(h) < GetDecimalDigitsCount(x0))
                dpx = GetDecimalDigitsCount(x0);
            else
                dpx = GetDecimalDigitsCount(h);
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "0.";
            for (int i = 0; i < dpx; i++)
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = chart1.ChartAreas[0].AxisX.LabelStyle.Format + "0";

            for (int i = 0; i < 3; i++)
            {
                if (i < Properties.Settings.Default.countOfMethods) chart1.Series[i].IsVisibleInLegend = true;
                else chart1.Series[i].IsVisibleInLegend = false;
            }
            foreach (Form f in Application.OpenForms)
                if (f.Name == "FormChart")
                {
                    FormChart fc = (FormChart)f;
                    fc.chartF1.Series[0].Points.Clear();
                    fc.chartF1.Series[1].Points.Clear();
                    fc.chartF1.Series[2].Points.Clear();
                    fc.chartF1.Legends[0].Enabled = true;
                    fc.chartF1.Series[0].IsVisibleInLegend = true;
                    for (int i = 0; i < 3; i++)
                    {
                        if (i < Properties.Settings.Default.countOfMethods) fc.chartF1.Series[i].IsVisibleInLegend = true;
                        else fc.chartF1.Series[i].IsVisibleInLegend = false;
                    }
                }
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox1.Items.Add("x\t y");
            listBox2.Items.Add("x\t y");
            listBox3.Items.Add("x\t y");
            points.Clear();
            for (int i = 0; i < Properties.Settings.Default.countOfMethods; i++)
                points.Add(new List<double[]>());

            for (int i = 0; i < Properties.Settings.Default.countOfMethods; i++)
            {
                ComboBox comboBox = comboBox1;
                switch (i)
                {
                    case 0: comboBox = comboBox1; break;
                    case 1: comboBox = comboBox2; break;
                    case 2: comboBox = comboBox3; break;
                }
                string method = "";
                ParameterizedThreadStart pThread = new ParameterizedThreadStart(euler);
                switch (comboBox.SelectedIndex)
                {
                    case 0:
                        method = "Ейлера";
                        pThread = new ParameterizedThreadStart(euler);
                        break;
                    case 1:
                        method = "Рунге-Кута";
                        pThread = new ParameterizedThreadStart(rk);
                        break;
                    case 2:
                        method = "Адамса-Бешфорта";
                        pThread = new ParameterizedThreadStart(adams);
                        break;
                }
                chart1.Series[i].LegendText = method;
                foreach (Form f in Application.OpenForms)
                    if (f.Name == "FormChart")
                    {
                        FormChart fc = (FormChart)f;
                        fc.chartF1.Series[i].LegendText = method;
                    }
                conditions = new Conditions(equation, x0, y0, xmin, xmax, h, i + 1);
                Thread thread = new Thread(pThread);
                thread.IsBackground = true;
                thread.Start(conditions);
            }
        }

        string calculate(string equation)//When expression contains "()", "sin()", "sqrt()" etc.
        {
            equation = equation.Replace('.', ',')
                .Replace(" ", "")
                .Replace("abs", "a")
                .Replace("sin", "s")
                .Replace("cos", "c")
                .Replace("tg", "t")
                .Replace("ctg", "1/t")
                .Replace("sqrt", "q")
                .Replace("cbrt", "b")
                .Replace("rt", "r")
                .Replace("log", "l")
                .Replace("ln", "n")
                .Replace("e", E.ToString())
                .Replace("pi", PI.ToString());

            for (int i = 0; i < equation.Length; i++)
            {
                if (equation[i] == '(')
                {
                    string toBeReplace = "";
                    int bracketsCount = 0;
                    for (int j = i + 1; j < equation.Length; j++)
                    {
                        if (equation[j] == '(')
                        {
                            bracketsCount++;
                            toBeReplace += equation[j];
                        }
                        else if (equation[j] == ')' && bracketsCount > 0)
                        {
                            toBeReplace += equation[j];
                            bracketsCount--;
                        }
                        else if (equation[j] != ')')
                        {
                            toBeReplace += equation[j];
                        }
                        else break;
                    }

                    if (i == 0)
                    {
                        equation = equation.Replace('(' + toBeReplace + ')', calculate(toBeReplace));
                        continue;
                    }
                    string newBase = "";
                    for (int j = i - 1; j >= 0; j--)//l10( //r10(
                    {
                        if (equation[j] == 'l')
                        {
                            newBase = new string(newBase.Reverse().ToArray());
                            equation = equation.Replace("l" + newBase + "(" + toBeReplace + ")", Log(double.Parse(calculate(toBeReplace)), double.Parse(newBase)).ToString());
                            break;
                        }
                        else if (equation[j] == 'r')
                        {
                            newBase = new string(newBase.Reverse().ToArray());
                            equation = equation.Replace("r" + newBase + "(" + toBeReplace + ")", Pow(double.Parse(calculate(toBeReplace)), 1.0 / double.Parse(newBase)).ToString());
                            break;
                        }
                        else if (char.IsDigit(equation[j]))
                        {
                            newBase += equation[j];
                        }
                        else
                        {
                            switch (equation[i - 1])
                            {
                                case 's':
                                    equation = equation.Replace("s(" + toBeReplace + ")", Sin(double.Parse(calculate(toBeReplace))).ToString());
                                    break;
                                case 'c':
                                    equation = equation.Replace("c(" + toBeReplace + ")", Cos(double.Parse(calculate(toBeReplace))).ToString());
                                    break;
                                case 't':
                                    equation = equation.Replace("t(" + toBeReplace + ")", Tan(double.Parse(calculate(toBeReplace))).ToString());
                                    break;
                                case 'q':
                                    equation = equation.Replace("q(" + toBeReplace + ")", Sqrt(double.Parse(calculate(toBeReplace))).ToString());
                                    break;
                                case 'b':
                                    equation = equation.Replace("b(" + toBeReplace + ")", Pow(double.Parse(calculate(toBeReplace)), 1.0 / 3.0).ToString());
                                    break;
                                case 'n':
                                    equation = equation.Replace("n(" + toBeReplace + ")", Log(double.Parse(calculate(toBeReplace))).ToString());
                                    break;
                                case 'a':
                                    equation = equation.Replace("a(" + toBeReplace + ")", Abs(double.Parse(calculate(toBeReplace))).ToString());
                                    break;
                                default:
                                    equation = equation.Replace('(' + toBeReplace + ')', calculate(toBeReplace));
                                    break;
                            }
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < equation.Length; i++)
            {
                if (equation[i] == '^')
                {
                    equation = calculateSimple(equation, i);
                    i = 0;
                }
            }
            for (int i = 0; i < equation.Length; i++)
            {
                if (equation[i] == '*' || equation[i] == '/')
                {
                    equation = calculateSimple(equation, i);
                    i = 0;
                }
            }
            for (int i = 0; i < equation.Length; i++)
            {
                if (!char.IsDigit(equation[i]) && equation[i] != ',' && i != 0 && equation[i - 1].ToString().ToLower() != "e" && equation[i].ToString().ToLower() != "e" && equation[i + 1].ToString() != Convert.ToString(Double.NegativeInfinity))
                {
                    equation = calculateSimple(equation, i);
                    i = 0;
                }
            }

            return equation;
        }

        string calculateSimple(string expression, int operatorPos)//When expression doesn`t contain "()", "sin()", "sqrt()" etc.
        {
            double numLeft;
            double numRight;
            string numLeftStr = "";
            string numRightStr = "";
            bool numRightIsNegative = false;

            if (expression.Contains(Convert.ToString(Double.NegativeInfinity)))
                return Convert.ToString(Double.NegativeInfinity);
            else if (expression.Contains(Convert.ToString(Double.PositiveInfinity)))
                return Convert.ToString(Double.PositiveInfinity);

            for (int i = operatorPos - 1; i >= 0; i--)//Finds left number around the symbol
            {
                if (char.IsDigit(expression[i]) || expression[i] == ',')
                {
                    numLeftStr += expression[i];
                }
                else if ((expression[i] == '-' || expression[i] == '+') && i != 0 && expression[i - 1].ToString().ToLower() == "e")
                {
                    numLeftStr += expression[i] + "E";
                    i--;
                    continue;
                }
                else if (expression[i] == '-')
                {
                    numLeftStr += "-";
                    break;
                }
                else break;
            }
            numLeftStr = new string(numLeftStr.Reverse().ToArray());

            if (expression[operatorPos + 1] == '-')
            {
                numRightStr = "-";
                numRightIsNegative = true;
            }
            for (int i = operatorPos + 1 + Convert.ToInt32(numRightIsNegative); i < expression.Length; i++)//Finds right number around the symbol
            {
                if (char.IsDigit(expression[i]) || expression[i] == ',')
                {
                    numRightStr += expression[i];
                }
                else if (i != expression.Length - 1 && (expression[i + 1] == '-' || expression[i + 1] == '+') && (expression[i] == 'E' || expression[i] == 'e'))
                {
                    numRightStr += "E" + expression[i + 1];
                    i++;
                    continue;
                }
                else break;
            }
            double res = 0;
            numRight = double.Parse(numRightStr.ToUpper());
            if (numLeftStr.Length != 0)
            {
                numLeft = double.Parse(numLeftStr.ToUpper());
                switch (expression[operatorPos])
                {
                    case '+':
                        res = numLeft + numRight;
                        break;
                    case '-':
                        res = numLeft - numRight;
                        break;
                    case '*':
                        res = numLeft * numRight;
                        break;
                    case '/':
                        res = numLeft / numRight;
                        break;
                    case '^':
                        res = Pow(numLeft, numRight);
                        break;
                }
            }
            else
            {
                res = -numRight;
            }
            expression = expression.ToUpper().Replace(numLeftStr.ToUpper() + expression[operatorPos] + numRightStr.ToUpper(), res.ToString());
            return expression;
        }

        double der(string f, double x, double y)
        {
            return double.Parse(calculate(f.ToLower().Replace("x", Convert.ToString(x)).Replace("y", Convert.ToString(y))));
        }

        void euler(string f, double x0, double y0, double xmin, double xmax, double h, int methodNumber)
        {
            List<double[]> points = new List<double[]>();
            ListBox listBox = listBox1;
            switch (methodNumber)
            {
                case 1: listBox = listBox1; break;
                case 2: listBox = listBox2; break;
                case 3: listBox = listBox3; break;
            }
            int dpx;
            if (GetDecimalDigitsCount(h) < GetDecimalDigitsCount(x0))
                dpx = GetDecimalDigitsCount(x0);
            else
                dpx = GetDecimalDigitsCount(h);
            int dpy = Properties.Settings.Default.decimalPlacesListY;
            int xminDC = GetDecimalDigitsCount(xmin);
            if (xminDC < GetDecimalDigitsCount(h))
                xminDC = GetDecimalDigitsCount(h);
            if (xminDC < GetDecimalDigitsCount(x0))
                xminDC = GetDecimalDigitsCount(x0);
            if (Round(x0, xminDC) >= Round(xmin, xminDC))
            {
                Action action0 = () => listBox.Items.Add(Convert.ToString(Round(x0, dpx).ToString("G6")) + "\t " + Round(y0, dpy).ToString("G6"));
                listBox.Invoke(action0);
                points.Add(new double[] { x0, y0 });
            }
            if ((Abs(x0) > 1.0E-28 || x0 == 0) && (Abs(y0) > 1.0E-28 || y0 == 0) && Abs(x0) < 1.0E+28 && Abs(y0) < 1.0E+28)
            {
                Action actionP0 = () => addPointToChart(x0, y0, methodNumber);
                Invoke(actionP0);
            }
            else
                if (!limitReachedExeption)
                {
                    limitReachedExeption = true;
                    MessageBox.Show("Деякі значення точок занадто великі або малі. Ці значення не будуть відображені на графіку",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            int xmaxDC = GetDecimalDigitsCount(xmax);
            if (xmaxDC < GetDecimalDigitsCount(h))
                xmaxDC = GetDecimalDigitsCount(h);
            try
            {
                int i = 1; //iteration number
                int j = 0;
                double k = 0;
                double pointsCount = (xmax - xmin) / h;
                double newStep = pointsCount / (double)Properties.Settings.Default.maxPointsAtChart;
                while (Round(x0, xminDC) + h <= Round(xmax, xmaxDC))
                {
                    Thread.Sleep(new TimeSpan(10000));
                    double df = der(f, x0, y0);
                    double dfn = der(f, x0 + h, df * h + y0);
                    double y = y0 + (df + dfn) * h / 2; //x*y+y^5-r3(x)

                    x0 += h;
                    x0 = Round(x0, 9);
                    y0 = y;
                    if (Round(x0, xminDC) >= Round(xmin, xminDC))
                    {
                        Action action1 = () =>
                        {
                            listBox.Items.Add(Convert.ToString(Round(x0, dpx).ToString("G6")) + "\t " + Round(y0, dpy).ToString("G6"));
                            listBox.TopIndex = i - 23;
                        };
                        listBox.Invoke(action1);
                        points.Add(new double[] { x0, y0 });
                        if ((Abs(x0) > 1.0E-28 || x0 == 0) && (Abs(y0) > 1.0E-28 || y0 == 0) && Abs(x0) < 1.0E+28 && Abs(y0) < 1.0E+28)
                        {
                            if (pointsCount > (double)Properties.Settings.Default.maxPointsAtChart)
                            {
                                j++;
                                if (j > k || j == pointsCount)
                                {
                                    k += newStep;
                                    Action actionP1 = () => addPointToChart(x0, y0, methodNumber);
                                    Invoke(actionP1);
                                }
                            }
                            else
                            {
                                Action actionP1 = () => addPointToChart(x0, y0, methodNumber);
                                Invoke(actionP1);
                            }
                        }
                        else
                        {
                            if (!limitReachedExeption)
                            {
                                limitReachedExeption = true;
                                MessageBox.Show("Деякі значення точок занадто великі або малі. Ці значення не будуть відображені на графіку",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    if (Properties.Settings.Default.isIterationLimited && i == Properties.Settings.Default.iterationLimit)
                    {
                        DialogResult result = DialogResult.None;
                        if (methodNumber != 1)
                            _event.WaitOne();
                        if (limitStopFlag)
                            break;
                        else if (!ignoreLimitFlag)
                        {
                            result = MessageBox.Show("Ліміт ітерацій досягнено. Продовжити розрахунок?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        }
                        if (result == DialogResult.No)
                        {
                            limitStopFlag = true;
                            _event.Set();
                            break;
                        }
                        else if (result == DialogResult.Yes)
                        {
                            ignoreLimitFlag = true;
                            _event.Set();
                        }
                    }
                    if (Double.IsInfinity(y0))
                        break;
                    if (stopFlag) break;
                    i++;
                }
            }
            catch
            {
                if (!initConditionsError)
                {
                    initConditionsError = true;
                    MessageBox.Show("Помилка в початкових умовах", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Action actionEnd = () => listBox.TopIndex = listBox.Items.Count - 1;
            listBox.Invoke(actionEnd);

            this.points[methodNumber - 1].AddRange(points);
            if (++finished == 2)
            {
                Action actionFinish = () =>
                {
                    panelConditions.Enabled = true;
                    button1.Focus();
                    button3.Enabled = false;
                    panelConditions.Enabled = true;
                    showManyMethodsToolStripMenuItem.Enabled = true;
                    settingsToolStripMenuItem.Enabled = true;
                    listContextMenuStrip.Enabled = true;
                };
                Invoke(actionFinish);

                limitReachedExeption = false;
                initConditionsError = false;
                stopFlag = false;
            }
        }
        
        void euler(Object obj)
        {
            Conditions cond = obj as Conditions;

            euler(cond.equation, cond.x0, cond.y0, cond.xmin, cond.xmax, cond.h, cond.methodNumber);
        }

        void rk(string f, double x0, double y0, double xmin, double xmax, double h, int methodNumber)
        {
            List<double[]> points = new List<double[]>();
            ListBox listBox = listBox1;
            switch (methodNumber)
            {
                case 1: listBox = listBox1; break;
                case 2: listBox = listBox2; break;
                case 3: listBox = listBox3; break;
            }
            int dpx;
            if (GetDecimalDigitsCount(h) < GetDecimalDigitsCount(x0))
                dpx = GetDecimalDigitsCount(x0);
            else
                dpx = GetDecimalDigitsCount(h);
            int dpy = Properties.Settings.Default.decimalPlacesListY;
            int xminDC = GetDecimalDigitsCount(xmin);
            if (xminDC < GetDecimalDigitsCount(h))
                xminDC = GetDecimalDigitsCount(h);
            if (xminDC < GetDecimalDigitsCount(x0))
                xminDC = GetDecimalDigitsCount(x0);
            if (Round(x0, xminDC) >= Round(xmin, xminDC))
            {
                Action action0 = () => listBox.Items.Add(Convert.ToString(Round(x0, dpx).ToString("G6")) + "\t " + Round(y0, dpy).ToString("G6"));
                listBox.Invoke(action0);
                points.Add(new double[] { x0, y0 });
                if ((Abs(x0) > 1.0E-28 || x0 == 0) && (Abs(y0) > 1.0E-28 || y0 == 0) && Abs(x0) < 1.0E+28 && Abs(y0) < 1.0E+28)
                {
                    Action actionP0 = () => addPointToChart(x0, y0, methodNumber);
                    Invoke(actionP0);
                }
                else
                    if (!limitReachedExeption)
                    {
                        limitReachedExeption = true;
                        MessageBox.Show("Деякі значення точок занадто великі або малі. Ці значення не будуть відображені на графіку",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
            }
            int xmaxDC = GetDecimalDigitsCount(xmax);
            if (xmaxDC < GetDecimalDigitsCount(h))
                xmaxDC = GetDecimalDigitsCount(h);
            try
            {
                int i = 1; //iteration number
                int j = 0;
                double k = 0;
                double pointsCount = (xmax - xmin) / h;
                double newStep = pointsCount / (double)Properties.Settings.Default.maxPointsAtChart;
                while (Round(x0, xminDC) + h <= Round(xmax, xmaxDC))
                {
                    Thread.Sleep(new TimeSpan(10000));
                    double df = der(f, x0, y0);
                    double dk2 = der(f, x0 + h / 2, y0 + h / 2 * df);
                    double dk3 = der(f, x0 + h / 2, y0 + h / 2 * dk2);
                    double dk4 = der(f, x0 + h, y0 + h * dk3);
                    double y = y0 + h / 6 * (df + 2 * dk2 + 2 * dk3 + dk4);

                    x0 += h;
                    y0 = y;

                    if (Round(x0, xminDC) >= Round(xmin, xminDC))
                    {
                        Action action1 = () =>
                        {
                            listBox.Items.Add(Convert.ToString(Round(x0, dpx).ToString("G6")) + "\t " + Round(y0, dpy).ToString("G6"));
                            listBox.TopIndex = i - 23;
                        };
                        listBox.Invoke(action1);
                        points.Add(new double[] { x0, y0 });
                        if ((Abs(x0) > 1.0E-28 || x0 == 0) && (Abs(y0) > 1.0E-28 || y0 == 0) && Abs(x0) < 1.0E+28 && Abs(y0) < 1.0E+28)
                        {
                            if (pointsCount > (double)Properties.Settings.Default.maxPointsAtChart)
                            {
                                j++;
                                if (j > k || j == pointsCount)
                                {
                                    k += newStep;
                                    Action actionP1 = () => addPointToChart(x0, y0, methodNumber);
                                    Invoke(actionP1);
                                }
                            }
                            else
                            {
                                Action actionP1 = () => addPointToChart(x0, y0, methodNumber);
                                Invoke(actionP1);
                            }
                        }
                        else
                        {
                            if (!limitReachedExeption)
                            {
                                limitReachedExeption = true;
                                MessageBox.Show("Деякі значення точок занадто великі або малі. Ці значення не будуть відображені на графіку",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    if (Properties.Settings.Default.isIterationLimited && i == Properties.Settings.Default.iterationLimit)
                    {
                        DialogResult result = DialogResult.None;
                        if (methodNumber != 1)
                            _event.WaitOne();
                        if (limitStopFlag)
                            break;
                        else if (!ignoreLimitFlag)
                        {
                            result = MessageBox.Show("Ліміт ітерацій досягнено. Продовжити розрахунок?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        }
                        if (result == DialogResult.No)
                        {
                            limitStopFlag = true;
                            _event.Set();
                            break;
                        }
                        else if (result == DialogResult.Yes)
                        {
                            ignoreLimitFlag = true;
                            _event.Set();
                        }
                    }
                    if (Double.IsInfinity(y0))
                        break;
                    if (stopFlag) break;
                    i++;
                }
            }
            catch
            {
                if (!initConditionsError)
                {
                    initConditionsError = true;
                    MessageBox.Show("Помилка в початкових умовах", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Action actionEnd = () => listBox.TopIndex = listBox.Items.Count - 1;
            listBox.Invoke(actionEnd);

            this.points[methodNumber - 1].AddRange(points);
            if (++finished == 2)
            {
                Action actionFinish = () =>
                {
                    panelConditions.Enabled = true;
                    button1.Focus();
                    button3.Enabled = false;
                    panelConditions.Enabled = true;
                    showManyMethodsToolStripMenuItem.Enabled = true;
                    settingsToolStripMenuItem.Enabled = true;
                    listContextMenuStrip.Enabled = true;
                };
                Invoke(actionFinish);

                initConditionsError = false;
                limitReachedExeption = false;
                stopFlag = false;
            }
        }
        (double, bool) rk(string f, double x0, double y0, double xmin, double h, int methodNumber)
        {
            double y = 0;
            bool error = false;
            try
            {
                Thread.Sleep(new TimeSpan(10000));
                int i = 1; //iteration number
                double df = der(f, x0, y0);
                double dk2 = der(f, x0 + h / 2, y0 + h / 2 * df);
                double dk3 = der(f, x0 + h / 2, y0 + h / 2 * dk2);
                double dk4 = der(f, x0 + h, y0 + h * dk3);
                y = y0 + h / 6 * (df + 2 * dk2 + 2 * dk3 + dk4);

                x0 += h;
                y0 = y;

                ListBox listBox = listBox1;
                switch (methodNumber)
                {
                    case 1: listBox = listBox1; break;
                    case 2: listBox = listBox2; break;
                    case 3: listBox = listBox3; break;
                }
                int dpx;
                if (GetDecimalDigitsCount(h) < GetDecimalDigitsCount(x0))
                    dpx = GetDecimalDigitsCount(x0);
                else
                    dpx = GetDecimalDigitsCount(h);
                int dpy = Properties.Settings.Default.decimalPlacesListY;
                int xminDC = GetDecimalDigitsCount(xmin);
                if (xminDC < GetDecimalDigitsCount(h))
                    xminDC = GetDecimalDigitsCount(h);
                if (xminDC < GetDecimalDigitsCount(x0))
                    xminDC = GetDecimalDigitsCount(x0);
                if (Round(x0, xminDC) >= Round(xmin, xminDC))
                {
                    Action action1 = () => listBox.Items.Add(Convert.ToString(Round(x0, dpx).ToString("G6")) + "\t " + Round(y0, dpy).ToString("G6"));
                    listBox.Invoke(action1);
                    if ((Abs(x0) > 1.0E-28 || x0 == 0) && (Abs(y0) > 1.0E-28 || y0 == 0) && Abs(x0) < 1.0E+28 && Abs(y0) < 1.0E+28)
                    {
                        Action actionP0 = () => addPointToChart(x0, y0, methodNumber);
                        Invoke(actionP0);
                    }
                    else
                    if (!limitReachedExeption)
                    {
                        limitReachedExeption = true;
                        MessageBox.Show("Деякі значення точок занадто великі або малі. Ці значення не будуть відображені на графіку",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                if (Properties.Settings.Default.isIterationLimited && i == Properties.Settings.Default.iterationLimit)
                {
                        DialogResult result = DialogResult.None;
                    if (methodNumber != 1)
                        _event.WaitOne();
                    if (limitStopFlag)
                        stopFlag = true;
                    else if (!ignoreLimitFlag)
                    {
                        result = MessageBox.Show("Ліміт ітерацій досягнено. Продовжити розрахунок?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    }
                    if (result == DialogResult.No)
                    {
                        limitStopFlag = true;
                        _event.Set();
                        stopFlag = true;
                    }
                    else if (result == DialogResult.Yes)
                    {
                        ignoreLimitFlag = true;
                        _event.Set();
                    }
                }
                if (Double.IsInfinity(y0))
                    error = true;
            }
            catch
            {
                error = true;
                if (!initConditionsError)
                {
                    initConditionsError = true;
                    MessageBox.Show("Помилка в початкових умовах", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return (y, error);
        }
        void rk(Object obj)
        {
            Conditions cond = obj as Conditions;

            rk(cond.equation, cond.x0, cond.y0, cond.xmin, cond.xmax, cond.h, cond.methodNumber);
        }

        void adams(string f, double x0, double y0, double xmin, double xmax, double h, int methodNumber)
        {
            List<double[]> points = new List<double[]>();
            ListBox listBox = listBox1;
            switch (methodNumber)
            {
                case 1: listBox = listBox1; break;
                case 2: listBox = listBox2; break;
                case 3: listBox = listBox3; break;
            }
            int dpx;
            if (GetDecimalDigitsCount(h) < GetDecimalDigitsCount(x0))
                dpx = GetDecimalDigitsCount(x0);
            else
                dpx = GetDecimalDigitsCount(h);
            int dpy = Properties.Settings.Default.decimalPlacesListY;
            int xminDC = GetDecimalDigitsCount(xmin);
            if (xminDC < GetDecimalDigitsCount(h))
                xminDC = GetDecimalDigitsCount(h);
            if (xminDC < GetDecimalDigitsCount(x0))
                xminDC = GetDecimalDigitsCount(x0);
            if (Round(x0, xminDC) >= Round(xmin, xminDC))
            {
                Action action0 = () => listBox.Items.Add(Convert.ToString(Round(x0, dpx).ToString("G6")) + "\t " + Round(y0, dpy).ToString("G6"));
                listBox.Invoke(action0);
                points.Add(new double[] { x0, y0 });
                if ((Abs(x0) > 1.0E-28 || x0 == 0) && (Abs(y0) > 1.0E-28 || y0 == 0) && Abs(x0) < 1.0E+28 && Abs(y0) < 1.0E+28)
                {
                    Action actionP0 = () => addPointToChart(x0, y0, methodNumber);
                    Invoke(actionP0);
                }
                else
                    if (!limitReachedExeption)
                    {
                        limitReachedExeption = true;
                        MessageBox.Show("Деякі значення точок занадто великі або малі. Ці значення не будуть відображені на графіку",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
            }
            int xmaxDC = GetDecimalDigitsCount(xmax);
            if (xmaxDC < GetDecimalDigitsCount(h))
                xmaxDC = GetDecimalDigitsCount(h);
            try
            {

                int i = 2; //iteration number
                int j = 0;
                double k = 0;
                double pointsCount = (xmax - xmin) / h;
                double newStep = pointsCount / (double)Properties.Settings.Default.maxPointsAtChart;

                (double y1, bool error) = rk(f, x0, y0, xmin, h, methodNumber);
                if (Round(x0, xminDC) >= Round(xmin, xminDC))
                    points.Add(new double[] { x0 + h, y1 });
                //x0 += h;
                double x1 = x0 + h;
                if (!error && !stopFlag)
                    while (Round(x1, xminDC) + h <= Round(xmax, xmaxDC))
                    {
                        Thread.Sleep(new TimeSpan(10000));
                        double dp = der(f, x0, y0);
                        double dc = der(f, x1, y1);
                        double y = y1 + 3.0 / 2.0 * h * dc - 1.0 / 2.0 * h * dp;

                        x0 += h;
                        x1 += h;//?
                        y0 = y1;
                        y1 = y;
                        if (Round(x1, xminDC) >= Round(xmin, xminDC))
                        {
                            Action action1 = () =>
                            {
                                listBox.Items.Add(Convert.ToString(Round(x1, dpx).ToString("G6")) + "\t " + Round(y1, dpy).ToString("G6"));
                                listBox.TopIndex = i - 23;
                            };
                            listBox.Invoke(action1);
                            points.Add(new double[] { x1, y1 });
                            if ((Abs(x1) > 1.0E-28 || x1 == 0) && (Abs(y1) > 1.0E-28 || y1 == 0) && Abs(x1) < 1.0E+28 && Abs(y1) < 1.0E+28)
                            {
                                if (pointsCount > (double)Properties.Settings.Default.maxPointsAtChart)
                                {
                                    j++;
                                    if (j > k || j == pointsCount)
                                    {
                                        k += newStep;
                                        Action actionP1 = () => addPointToChart(Round(x1, dpx), y1, methodNumber);
                                        Invoke(actionP1);
                                    }
                                }
                                else
                                {
                                    Action actionP1 = () => addPointToChart(Round(x1, dpx), y1, methodNumber);
                                    Invoke(actionP1);
                                }
                            }
                            else
                            {
                                if (!limitReachedExeption)
                                {
                                    limitReachedExeption = true;
                                    MessageBox.Show("Деякі значення точок занадто великі або малі. Ці значення не будуть відображені на графіку",
                                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        if (Properties.Settings.Default.isIterationLimited && i == Properties.Settings.Default.iterationLimit)
                        {
                            DialogResult result = DialogResult.None;
                            if (methodNumber != 1)
                                _event.WaitOne();
                            if (limitStopFlag)
                                break;
                            else if (!ignoreLimitFlag)
                            {
                                result = MessageBox.Show("Ліміт ітерацій досягнено. Продовжити розрахунок?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            }
                            if (result == DialogResult.No)
                            {
                                limitStopFlag = true;
                                _event.Set();
                                break;
                            }
                            else if (result == DialogResult.Yes)
                            {
                                ignoreLimitFlag = true;
                                _event.Set();
                            }
                        }
                        if (Double.IsInfinity(y0))
                            break;
                        if (stopFlag) break;
                        i++;
                    }
            }
            catch
            {
                if (!initConditionsError)
                {
                    initConditionsError = true;
                    MessageBox.Show("Помилка в початкових умовах", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Action actionEnd = () => listBox.TopIndex = listBox.Items.Count - 1;
            listBox.Invoke(actionEnd);

            this.points[methodNumber - 1].AddRange(points);
            if (++finished == 2)
            {
                Action actionFinish = () =>
                {
                    panelConditions.Enabled = true;
                    button1.Focus();
                    button3.Enabled = false;
                    panelConditions.Enabled = true;
                    showManyMethodsToolStripMenuItem.Enabled = true;
                    settingsToolStripMenuItem.Enabled = true;
                    listContextMenuStrip.Enabled = true;
                };
                Invoke(actionFinish);

                initConditionsError = false;
                limitReachedExeption = false;
                stopFlag = false;
            }
        }
        void adams(Object obj)
        {
            Conditions cond = obj as Conditions;

            adams(cond.equation, cond.x0, cond.y0, cond.xmin, cond.xmax, cond.h, cond.methodNumber);
        }

        static int GetDecimalDigitsCount(double number)
        {
            bool containsE = false;
            string numberStr = Convert.ToString(number);
            string countStr = "";
            for (int i = 0; i < numberStr.Length; i++)
            {
                if (containsE)
                {
                    countStr += numberStr[i];
                    if (i == numberStr.Length - 1)
                        return Convert.ToInt32(countStr);
                }
                if (numberStr[i] == 'E')
                {
                    if (numberStr[++i] == '+') return 0;
                    containsE = true;
                }
            }
            string str = number.ToString(new System.Globalization.NumberFormatInfo() { NumberDecimalSeparator = "." });
            return str.Contains(".") ? str.Remove(0, Math.Truncate(number).ToString().Length + 1).Length : 0;
        }

        void addPointToChart(double x, double y, int methodNumber)
        {
            foreach (Form f in Application.OpenForms)
                if (f.Name == "FormChart")
                {
                    FormChart fc = (FormChart)f;
                    fc.chartF1.Series[methodNumber - 1].Points.AddXY(x, y);
                }
            this.chart1.Series[methodNumber - 1].Points.AddXY(x, y);
        }

        private void showManyMethodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormModalMethods modal = new FormModalMethods();
            modal.ShowDialog();
            showMethods();
        }

        private void showMethods()
        {
            switch (Properties.Settings.Default.countOfMethods)
            {
                case 3:
                    panel3.Visible = true;
                    panel2.Visible = true;
                    panel1.Visible = true;
                    this.Width = 666 + panel3.Width;
                    panel1.Width = 159;
                    adjustSizes();
                    break;
                case 2:
                    panel3.Visible = false;
                    panel2.Visible = true;
                    panel1.Visible = true;
                    this.Width = 666;
                    panel1.Width = 159;
                    adjustSizes();
                    break;
                case 1:
                    panel3.Visible = false;
                    panel2.Visible = false;
                    panel1.Visible = true;
                    this.Width = 666;
                    panel1.Width = 159 * 2;
                    adjustSizes();
                    break;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            adjustSizes();
        }

        private void adjustSizes()
        {
            int additionalWidth = 0;
            if (panel2.Visible) additionalWidth += panel2.Width;
            if (panel3.Visible) additionalWidth += panel3.Width;
            panelChart.Width = this.Width - panel1.Width - additionalWidth - 12 - 20;
            panelChart.Height = this.Height - panelConditions.Height - menuStrip1.Height - 12 - 40;
        }

        private void listBoxSelectedItem(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.multiSelect)
            {
                try
                {
                    ListBox listBox = sender as ListBox;
                    switch (listBox.Name)
                    {
                        case "listBox1":
                            listBox2.SelectedIndex = listBox1.SelectedIndex;
                            listBox3.SelectedIndex = listBox1.SelectedIndex;
                            break;
                        case "listBox2":
                            listBox1.SelectedIndex = listBox2.SelectedIndex;
                            listBox3.SelectedIndex = listBox2.SelectedIndex;
                            break;
                        case "listBox3":
                            listBox1.SelectedIndex = listBox3.SelectedIndex;
                            listBox2.SelectedIndex = listBox3.SelectedIndex;
                            break;
                    }
                }
                catch { }
            }
        }


        private void showChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showChartToolStripMenuItem.Checked)
            {
                floatableChartToolStripMenuItem.Enabled = false;
                showChartToolStripMenuItem.Checked = false;
                panelChart.Visible = false;
                Form fc = null;
                foreach (Form f in Application.OpenForms)
                    if (f.Name == "FormChart")
                    {
                        fc = f;
                    }
                if (fc != null) fc.Close();
            }
            else
            {
                showChartToolStripMenuItem.Checked = true;
                //chart1.Series[0].Points.Clear();
                //chart1.Series[1].Points.Clear();
                panelChart.Visible = true;
                floatableChartToolStripMenuItem.Enabled = true;
            }
        }

        private void floatableChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (floatableChartToolStripMenuItem.Checked)
            {
                Form fc = null;
                foreach (Form f in Application.OpenForms)
                    if (f.Name == "FormChart")
                    {
                        fc = f;
                    }
                if (fc != null) fc.Close(); //floatableChartToolStripMenuItem.Checked and hideFloatableChartToolStripMenuItem.Checked = false
                panelChart.Visible = true;
            }
            else
            {
                floatableChartToolStripMenuItem.Checked = true;
                hideFloatableChartToolStripMenuItem.Checked = true;
                FormChart formChart = new FormChart(chart1.Series);
                formChart.Show();
                panelChart.Visible = false;
            }
        }

        private void hideChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showChartToolStripMenuItem_Click(sender, e);
        }

        private void hideFloatableChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            floatableChartToolStripMenuItem_Click(sender, e);
        }

        private void removePlaceHolder(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.ForeColor != TextBox.DefaultForeColor)
            {
                if (textBox.Name == "textBox2")
                    textBox.Text = "";
                else if (textBox.Name == "textBox3")
                    textBox.Text = "";
                else if (textBox.Name == "textBox4")
                    textBox.Text = "";
                else if (textBox.Name == "textBox5")
                    textBox.Text = "";
                else if (textBox.Name == "textBox6")
                    textBox.Text = "";
            }
            textBox.ForeColor = TextBox.DefaultForeColor;
        }

        private void addPlaceHolder(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text) && Properties.Settings.Default.useDefaultConditions)
            {
                if (textBox.Name == "textBox2")
                    textBox.Text = Properties.Settings.Default.defaultX0;
                else if (textBox.Name == "textBox3")
                    textBox.Text = Properties.Settings.Default.defaultY0;
                else if (textBox.Name == "textBox4")
                    textBox.Text = Properties.Settings.Default.defaultXmin;
                else if (textBox.Name == "textBox5")
                    textBox.Text = Properties.Settings.Default.defaultXmax;
                else if (textBox.Name == "textBox6")
                    textBox.Text = Properties.Settings.Default.defaultH;
                textBox.ForeColor = Color.Gray;
            }
        }

        private void initConditions()
        {
            if (Properties.Settings.Default.useDefaultConditions)
            {
                if ((textBox2.Text == "" || textBox2.ForeColor == Color.Gray) && !textBox2.ContainsFocus)
                {
                    textBox2.Text = Properties.Settings.Default.defaultX0;
                    textBox2.ForeColor = Color.Gray;
                }
                if ((textBox3.Text == "" || textBox3.ForeColor == Color.Gray) && !textBox3.ContainsFocus)
                {
                    textBox3.Text = Properties.Settings.Default.defaultY0;
                    textBox3.ForeColor = Color.Gray;
                }
                if ((textBox4.Text == "" || textBox4.ForeColor == Color.Gray) && !textBox4.ContainsFocus)
                {
                    textBox4.Text = Properties.Settings.Default.defaultXmin;
                    textBox4.ForeColor = Color.Gray;
                }
                if ((textBox5.Text == "" || textBox5.ForeColor == Color.Gray) && !textBox5.ContainsFocus)
                {
                    textBox5.Text = Properties.Settings.Default.defaultXmax;
                    textBox5.ForeColor = Color.Gray;
                }
                if ((textBox6.Text == "" || textBox6.ForeColor == Color.Gray) && !textBox6.ContainsFocus)
                {
                    textBox6.Text = Properties.Settings.Default.defaultH;
                    textBox6.ForeColor = Color.Gray;
                }
                textBox2.Focus();
            }
            else
            {
                if (textBox2.ForeColor != TextBox.DefaultForeColor) textBox2.Text = "";
                if (textBox3.ForeColor != TextBox.DefaultForeColor) textBox3.Text = "";
                if (textBox4.ForeColor != TextBox.DefaultForeColor) textBox4.Text = "";
                if (textBox5.ForeColor != TextBox.DefaultForeColor) textBox5.Text = "";
                if (textBox6.ForeColor != TextBox.DefaultForeColor) textBox6.Text = "";
                textBox2.ForeColor = textBox3.ForeColor = textBox4.ForeColor = textBox5.ForeColor = textBox6.ForeColor = TextBox.DefaultForeColor;
            }
        }

        private void textBoxAutoSizeOn(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.BringToFront();
            int textWidth = TextRenderer.MeasureText(textBox.Text, textBox.Font).Width;
            if (textWidth < 43)
                textBox.Width = 43;
            else
                textBox.Width = textWidth + 4;
            textBox.BringToFront();

            System.Drawing.Drawing2D.GraphicsPath gr = new System.Drawing.Drawing2D.GraphicsPath();
            foreach (Control c in panelConditions.Controls)
            {
                gr.AddRectangle(new Rectangle(c.Location, c.Size));
            }
            panelConditions.Region = new Region(gr);
        }

        private void textBoxAutoSizeOff(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Width = 43;
            textBox.BringToFront();
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (finished == 2)
            {
                chart1.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.Dot;
                chart1.ChartAreas[0].CursorY.LineDashStyle = ChartDashStyle.Dot;
                Point mousePoint = new Point(e.X, e.Y);
                chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, true);
                chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, true);
            }
        }

        private void chart1_MouseLeave(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.NotSet;
            chart1.ChartAreas[0].CursorY.LineDashStyle = ChartDashStyle.NotSet;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
        }

        private void chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (chart1.ChartAreas[0].AxisX.ScaleView.Size <= 1E-05 || chart1.ChartAreas[0].AxisY.ScaleView.Size <= 1E-05)
            {
                chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _event.Set();
            stopFlag = true;
        }

        private void exportChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = Properties.Settings.Default.chartFileName;
            saveFileDialog1.Filter = "JPEG |*.jpeg|PNG |*.png|Точковий малюнок |*.bmp|TIFF |*.tiff|GIF |*.gif";
            if (finished == 2)
                try
                {
                    DialogResult result = saveFileDialog1.ShowDialog();
                    if (result == DialogResult.OK && floatableChartToolStripMenuItem.Checked)
                    {
                        foreach (Form f in Application.OpenForms)
                        {
                            if (f.Name == "FormChart")
                            {
                                FormChart fc = (FormChart)f;
                                fc.chartF1.SaveImage(saveFileDialog1.FileName, (ChartImageFormat)saveFileDialog1.FilterIndex);
                            }
                        }
                    }
                    else if (result == DialogResult.OK)
                    {
                        chart1.SaveImage(saveFileDialog1.FileName, (ChartImageFormat)saveFileDialog1.FilterIndex);
                    }
                }
                catch
                {
                    MessageBox.Show("Помилка збереження", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            else MessageBox.Show("Неможливо експортувати до розрахунку", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void exportListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = Properties.Settings.Default.pointsFileName;
            saveFileDialog1.Filter = "Книга Excel |*.xlsx|Книга Excel з підтримкою макросів |*.xlsm|Шаблон Excel |*.xltx|Шаблон Excel з підтримкою макросів |*.xltm|Текстовий файл |*.txt";
            if (finished == 2)
                try
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if (saveFileDialog1.FilterIndex == 5)
                        {
                            var sw = new StreamWriter(saveFileDialog1.FileName);
                            int j = 0; // Number of method
                            sw.WriteLine("Методом " + comboBox1.Text);
                            sw.WriteLine("x\ty");
                            for (int i = 0; ; i++)
                            {
                                if (i == points[j].Count)
                                {
                                    i = 0;
                                    if (++j == Properties.Settings.Default.countOfMethods) break;
                                    switch (j)
                                    {
                                        case 1:
                                            sw.WriteLine("Методом " + comboBox2.Text);
                                            sw.WriteLine("x\ty");
                                            break;
                                        case 2:
                                            sw.WriteLine("Методом " + comboBox3.Text);
                                            sw.WriteLine("x\ty");
                                            break;
                                    }
                                }
                                sw.WriteLine(points[j][i][0] + "\t" + points[j][i][1]);
                            }
                            sw.Close();
                        }
                        else
                        {
                            var workbook = new XLWorkbook();
                            var sheet = workbook.Worksheets.Add("Результат рішення Диф.рівнянь");

                            sheet.Cell(1, 1).SetValue(comboBox1.Text);
                            sheet.Cell(2, 1).SetValue("x");
                            sheet.Cell(2, 2).SetValue("y");
                            int j = 0; // Number of method
                            for (int i = 0; ; i++)
                            {
                                if (i == points[j].Count)
                                {
                                    i = 0;
                                    if (++j == Properties.Settings.Default.countOfMethods) break;
                                    switch (j)
                                    {
                                        case 1:
                                            sheet.Cell(1, j * 2 + 1).SetValue(comboBox2.Text);
                                            sheet.Cell(2, j * 2 + 1).SetValue("x");
                                            sheet.Cell(2, j * 2 + 2).SetValue("y");
                                            break;
                                        case 2:
                                            sheet.Cell(1, j * 2 + 1).SetValue(comboBox3.Text);
                                            sheet.Cell(2, j * 2 + 1).SetValue("x");
                                            sheet.Cell(2, j * 2 + 2).SetValue("y");
                                            break;
                                    }
                                }
                                sheet.Cell(i + 3, j * 2 + 1).SetValue(points[j][i][0]);
                                sheet.Cell(i + 3, j * 2 + 2).SetValue(points[j][i][1]);
                            }
                            workbook.SaveAs(saveFileDialog1.FileName);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Помилка збереження", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            else MessageBox.Show("Неможливо експортувати до розрахунку", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void listBox_Click(object sender, EventArgs e)
        {
            ListBox listBox = sender as ListBox;
            try
            {
                Clipboard.SetText(listBox.Text, TextDataFormat.UnicodeText);
            }
            catch { }
        }

        private void copyPointToolStripContextMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
                ContextMenuStrip contextMenu = menuItem.Owner as ContextMenuStrip;
                ListBox listBox = contextMenu.SourceControl.Controls[1] as ListBox;
                Clipboard.SetText(listBox.Text, TextDataFormat.UnicodeText);
            }
            catch { }
        }

        private void copyAllPointsToolStripContextMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            ContextMenuStrip contextMenu = menuItem.Owner as ContextMenuStrip;
            ListBox listBox = contextMenu.SourceControl.Controls[1] as ListBox;
            StringBuilder text = new StringBuilder();
            try
            {
                foreach (var item in listBox.Items)
                {
                    text.AppendLine(item.ToString());
                }
                Clipboard.SetText(text.ToString(), TextDataFormat.UnicodeText);
            }
            catch { }
        }

        private void allowCopyByClick()
        {
            if (Properties.Settings.Default.copyByClick)
            {
                listBox1.Click += listBox_Click;
                listBox2.Click += listBox_Click;
                listBox3.Click += listBox_Click;
            }
            else
            {
                listBox1.Click -= listBox_Click;
                listBox2.Click -= listBox_Click;
                listBox3.Click -= listBox_Click;
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings f = new FormSettings();
            if (f.ShowDialog() == DialogResult.OK)
                settings.apply();
        }

        private void listSearch(object sender, EventArgs e)
        {
            try
            {
                ToolStripTextBox menuItem = sender as ToolStripTextBox;
                if (menuItem.Text != "")
                {
                    int searchInd = searchToolStripComboBox.SelectedIndex;
                    foreach (var listBox in new ListBox[] { listBox1, listBox2, listBox3 })
                    {
                        try
                        {
                            for (int i = 0; i < listBox.Items.Count; i++)
                            {
                                string itemText;
                                string searchText;
                                if (menuItem.Text.Replace('.', ',').Contains(','))
                                {
                                    itemText = listBox.Items[i].ToString().Split('\t')[searchInd];
                                    searchText = menuItem.Text.Replace('.', ',');
                                }
                                else
                                {
                                    itemText = listBox.Items[i].ToString().Split('\t')[searchInd].Split(',')[0];
                                    searchText = menuItem.Text;
                                }
                                if (itemText.Contains(searchText))
                                {
                                    listBox.SelectedItem = listBox.Items[i];
                                    break;
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void helpF1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHelp formHelp = new FormHelp();
            formHelp.ShowDialog();
        }
    }
}
