using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private Conditions conditions;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Size = new Size(318, 366);
            listBox1.Size = new Size(315, 303);
            comboBox1.Size = new Size(310, 21);
            //panel1.Location = new Point(611, 44);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            textBox2.ForeColor = textBox3.ForeColor = textBox4.ForeColor = textBox5.ForeColor = textBox6.ForeColor = Color.Gray;
            textBox2.Text = textBox3.Text = textBox4.Text = "0";
            textBox5.Text = "1";
            textBox6.Text = "0,1";
            textBox2.GotFocus += removePlaceHolder; textBox2.LostFocus += addPlaceHolder;
            textBox3.GotFocus += removePlaceHolder; textBox3.LostFocus += addPlaceHolder;
            textBox4.GotFocus += removePlaceHolder; textBox4.LostFocus += addPlaceHolder;
            textBox5.GotFocus += removePlaceHolder; textBox5.LostFocus += addPlaceHolder;
            textBox6.GotFocus += removePlaceHolder; textBox6.LostFocus += addPlaceHolder;

            textBox2.GotFocus += textBoxAutoSizeOn; textBox2.LostFocus += textBoxAutoSizeOff; textBox2.TextChanged += textBoxAutoSizeOn;
            textBox3.GotFocus += textBoxAutoSizeOn; textBox3.LostFocus += textBoxAutoSizeOff; textBox3.TextChanged += textBoxAutoSizeOn;
            textBox4.GotFocus += textBoxAutoSizeOn; textBox4.LostFocus += textBoxAutoSizeOff; textBox4.TextChanged += textBoxAutoSizeOn;
            textBox5.GotFocus += textBoxAutoSizeOn; textBox5.LostFocus += textBoxAutoSizeOff; textBox5.TextChanged += textBoxAutoSizeOn;
            textBox6.GotFocus += textBoxAutoSizeOn; textBox6.LostFocus += textBoxAutoSizeOff; textBox6.TextChanged += textBoxAutoSizeOn;

            chart1.ChartAreas[0].CursorX.Interval = 0;
            chart1.ChartAreas[0].CursorY.Interval = 0;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = false;
            chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = false;

            chart1.ChartAreas[0].CursorX.LineColor = Color.Black;
            chart1.ChartAreas[0].CursorX.LineWidth = 1;
            chart1.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.Dot;
            chart1.ChartAreas[0].CursorY.LineColor = Color.Black;
            chart1.ChartAreas[0].CursorY.LineWidth = 1;
            chart1.ChartAreas[0].CursorY.LineDashStyle = ChartDashStyle.Dot;
        }

        private class Conditions
        {
            public string equation;
            public double xmin, xmax, y0, x0, h;
            public bool isSecondary;
            public bool onlyChart;

            public Conditions(string equation, double x0, double y0, double xmin, double xmax, double h, bool isSecondary, bool onlyChart = false)
            {
                this.equation = equation;
                this.xmin = xmin;
                this.xmax = xmax;
                this.y0 = y0;
                this.x0 = x0;
                this.h = h;
                this.isSecondary = isSecondary;
                this.onlyChart = onlyChart;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double xmin;
            double xmax;
            double y0;
            double x0;
            double h;

            if (!String.IsNullOrEmpty(textBox2.Text)) x0 = double.Parse(textBox2.Text.Replace('.', ',')); else x0 = 0;
            if (!String.IsNullOrEmpty(textBox3.Text)) y0 = double.Parse(textBox3.Text.Replace('.', ',')); else y0 = 0;
            if (!String.IsNullOrEmpty(textBox4.Text)) xmin = double.Parse(textBox4.Text.Replace('.', ',')); else xmin = 0;
            if (!String.IsNullOrEmpty(textBox5.Text)) xmax = double.Parse(textBox5.Text.Replace('.', ',')); else xmax = 1;
            if (!String.IsNullOrEmpty(textBox6.Text)) h = double.Parse(textBox6.Text.Replace('.', ',')); else h = 0.1;

            string equation = textBox1.Text;//x+cos(y/sqrt(0.7))

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Legends[0].Enabled = true;
            chart1.Series[0].IsVisibleInLegend = true;
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            if (showSecondSolveToolStripMenuItem.Checked) chart1.Series[1].IsVisibleInLegend = true;
            else chart1.Series[1].IsVisibleInLegend = false;
            foreach (Form f in Application.OpenForms)
                if (f.Name == "FormChart")
                {
                    FormChart fc = (FormChart)f;
                    fc.chartF1.Series[0].Points.Clear();
                    fc.chartF1.Series[1].Points.Clear();
                    fc.chartF1.Legends[0].Enabled = true;
                    fc.chartF1.Series[0].IsVisibleInLegend = true;
                    if (showSecondSolveToolStripMenuItem.Checked) fc.chartF1.Series[1].IsVisibleInLegend = true;
                    else fc.chartF1.Series[1].IsVisibleInLegend = false;
                }
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    chart1.Series[0].LegendText = "Ейлера";
                    foreach (Form f in Application.OpenForms)
                        if (f.Name == "FormChart")
                        {
                            FormChart fc = (FormChart)f;
                            fc.chartF1.Series[0].LegendText = "Ейлера";
                        }
                    conditions = new Conditions(equation, x0, y0, xmin, xmax, h, false);
                    ParameterizedThreadStart pThread = new ParameterizedThreadStart(euler);
                    Thread thread = new Thread(pThread);
                    thread.Start(conditions);

                    break;
                case 1:
                    chart1.Series[0].LegendText = "Рунге-Кута";
                    foreach (Form f in Application.OpenForms)
                        if (f.Name == "FormChart")
                        {
                            FormChart fc = (FormChart)f;
                            fc.chartF1.Series[0].LegendText = "Рунге-Кута";
                        }
                    conditions = new Conditions(equation, x0, y0, xmin, xmax, h, false);
                    ParameterizedThreadStart pThreadRk = new ParameterizedThreadStart(rk);
                    Thread threadRk = new Thread(pThreadRk);
                    threadRk.Start(conditions);

                    break;
            }
            if (showSecondSolveToolStripMenuItem.Checked)
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        chart1.Series[1].LegendText = "Ейлера";
                        foreach (Form f in Application.OpenForms)
                            if (f.Name == "FormChart")
                            {
                                FormChart fc = (FormChart)f;
                                fc.chartF1.Series[1].LegendText = "Ейлера";
                            }
                        conditions = new Conditions(equation, x0, y0, xmin, xmax, h, true);
                        ParameterizedThreadStart pThread = new ParameterizedThreadStart(euler);
                        Thread thread = new Thread(pThread);
                        thread.Start(conditions);

                        break;
                    case 1:
                        chart1.Series[1].LegendText = "Рунге-Кута";
                        foreach (Form f in Application.OpenForms)
                            if (f.Name == "FormChart")
                            {
                                FormChart fc = (FormChart)f;
                                fc.chartF1.Series[1].LegendText = "Рунге-Кута";
                            }
                        conditions = new Conditions(equation, x0, y0, xmin, xmax, h, true);
                        ParameterizedThreadStart pThreadRk = new ParameterizedThreadStart(rk);
                        Thread threadRk = new Thread(pThreadRk);
                        threadRk.Start(conditions);

                        break;
                }
        }

        string calculate(string equation)//When expression contains "()", "sin()", "sqrt()" etc.
        {
            equation = equation.ToLower()
                .Replace('.', ',')
                .Replace(" ", "")
                .Replace("sin", "s")
                .Replace("cos", "c")
                .Replace("tg", "t")
                .Replace("ctg", "1/t")
                .Replace("sqrt", "q")
                .Replace("cbrt", "b")
                .Replace("rt", "r")
                .Replace("log", "l")
                .Replace("ln", "n");

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
                                default:
                                    equation = equation.Replace('(' + toBeReplace + ')', calculate(toBeReplace));
                                    break;
                            }
                            break;
                        }
                    }
                }
            }

            var dict = new Dictionary<char, List<int>>()
            {
                { '^', new List<int>() },
                { '*', new List<int>() },
                { '+', new List<int>() }
            };
            for (int i = 0; i < equation.Length; i++)
            {
                if (!char.IsDigit(equation[i]) && equation[i] != ',' && i != 0 && equation[i - 1] != 'E' && equation[i] != 'E')
                {
                    if (equation[i] == '^')
                        dict['^'].Add(i);
                }
            }
            foreach (int i in dict['^'].Reverse<int>())
            {
                equation = calculateSimple(equation, i);
            }
            for (int i = 0; i < equation.Length; i++)
            {
                if (!char.IsDigit(equation[i]) && equation[i] != ',' && i != 0 && equation[i - 1] != 'E' && equation[i] != 'E')
                {
                    if (equation[i] == '*' || equation[i] == '/')
                        dict['*'].Add(i);
                }
            }
            foreach (int i in dict['*'].Reverse<int>())
            {
                equation = calculateSimple(equation, i);
            }
            for (int i = 0; i < equation.Length; i++)
            {
                if (!char.IsDigit(equation[i]) && equation[i] != ',' && i != 0 && equation[i - 1] != 'E' && equation[i] != 'E')
                {
                    if (equation[i] == '+' || equation[i] == '-')
                        dict['+'].Add(i);
                }
            }
            foreach (int i in dict['+'].Reverse<int>())
            {
                equation = calculateSimple(equation, i);
            }
            /*for (int i = 0; i < equation.Length; i++)
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
                if (!char.IsDigit(equation[i]) && equation[i] != ',' && i != 0 && equation[i - 1].ToString().ToLower() != "e" && equation[i].ToString().ToLower() != "e")
                {
                    equation = calculateSimple(equation, i);
                    i = 0;
                }
            }*/

            return equation;
        }

        string calculateSimple(string expression, int operatorPos)//When expression doesn`t contain "()", "sin()", "sqrt()" etc.
        {
            double numLeft;
            double numRight;
            string numLeftStr = "";
            string numRightStr = "";
            bool numRightIsNegative = false;

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
            return double.Parse(calculate(f.Replace("x", Convert.ToString(x)).Replace("y", Convert.ToString(y))));
        }

        void euler(string f, double x0, double y0, double xmin, double xmax, double h, bool isSecondary, bool onlyChart = false)
        {
            if (isSecondary)
            {
                if (!onlyChart)
                {
                    Action action0 = () => listBox2.Items.Add(Convert.ToString(x0) + ") " + y0);
                    listBox2.Invoke(action0);
                }
            }
            else
            {
                if (!onlyChart)
                {
                    Action action0 = () => listBox1.Items.Add(Convert.ToString(x0) + ") " + y0);
                    listBox2.Invoke(action0);
                }
            }
            Action actionP0 = () => addPointToChart(x0, y0, isSecondary);
            Invoke(actionP0);

            while (xmin + h < xmax)
            {
                double df = der(f, x0, y0);
                double dfn = der(f, x0 + h, df * h + y0);
                double y = y0 + (der(f, x0, y0) + dfn) * h / 2; //x*y+y^5-r3(x)

                x0 += h;
                y0 = y;
                xmin += h;

                if (isSecondary)
                {
                    if (!onlyChart)
                    {
                        Action action1 = () => listBox2.Items.Add(Convert.ToString(x0) + ") " + y0);
                        listBox2.Invoke(action1);
                    }
                }
                else
                {
                    if (!onlyChart)
                    {
                        Action action1 = () => listBox1.Items.Add(Convert.ToString(x0) + ") " + y0);
                        listBox2.Invoke(action1);
                    }
                }
                Action actionP1 = () => addPointToChart(x0, y0, isSecondary);
                Invoke(actionP1);
            }
        }
        void euler(Object obj)
        {
            Conditions cond = obj as Conditions;

            euler(cond.equation, cond.x0, cond.y0, cond.xmin, cond.xmax, cond.h, cond.isSecondary, cond.onlyChart);
        }

        void rk(string f, double x0, double y0, double xmin, double xmax, double h, bool isSecondary, bool onlyChart = false)
        {
            if (isSecondary)
            {
                if (!onlyChart)
                {
                    Action action0 = () => listBox2.Items.Add(Convert.ToString(x0) + ") " + y0);
                    listBox2.Invoke(action0);
                }
            }
            else
            {
                if (!onlyChart)
                {
                    Action action0 = () => listBox1.Items.Add(Convert.ToString(x0) + ") " + y0);
                    listBox2.Invoke(action0);
                }
            }
            Action actionP0 = () => addPointToChart(x0, y0, isSecondary);
            Invoke(actionP0);

            while (xmin + h < xmax)
            {
                double y = y0 + h / 6 * (der(f, x0, y0) + 2 * k2(x0, y0, h) + 2 * k3(x0, y0, h) + k4(x0, y0, h));

                x0 += h;
                y0 = y;
                xmin += h;

                if (isSecondary)
                {
                    if (!onlyChart)
                    {
                        Action action1 = () => listBox2.Items.Add(Convert.ToString(x0) + ") " + y0);
                        listBox2.Invoke(action1);
                    }
                }
                else
                {
                    if (!onlyChart)
                    {
                        Action action1 = () => listBox1.Items.Add(Convert.ToString(x0) + ") " + y0);
                        listBox2.Invoke(action1);
                    }
                }
                Action actionP1 = () => addPointToChart(x0, y0, isSecondary);
                Invoke(actionP1);
            }

            double k2(double _x0, double _y0, double _h)
            {
                return der(f, _x0 + _h / 2, _y0 + _h / 2 * der(f, _x0, _y0));
            }
            double k3(double _x0, double _y0, double _h)
            {
                return der(f, _x0 + _h / 2, _y0 + _h / 2 * k2(_x0, _y0, _h));
            }
            double k4(double _x0, double _y0, double _h)
            {
                return der(f, _x0 + _h, _y0 + _h * k3(_x0, _y0, _h));
            }
        }
        void rk(Object obj)
        {
            Conditions cond = obj as Conditions;

            rk(cond.equation, cond.x0, cond.y0, cond.xmin, cond.xmax, cond.h, cond.isSecondary, cond.onlyChart);
        }

        void addPointToChart(double x, double y, bool secondary)
        {
            foreach (Form f in Application.OpenForms)
                if (f.Name == "FormChart")
                {
                    FormChart fc = (FormChart)f;
                    fc.chartF1.Series[Convert.ToInt32(secondary)].Points.AddXY(x, y);
                }
            this.chart1.Series[Convert.ToInt32(secondary)].Points.AddXY(x, y);
        }

        private void showSecondSolveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showSecondSolveToolStripMenuItem.Checked)
            {
                showSecondSolveToolStripMenuItem.Checked = false;
                panel2.Visible = false;

                panel1.Size = new Size(318, 366);
                listBox1.Size = new Size(315, 303);
                comboBox1.Size = new Size(310, 21);

                //panel1.Location = new Point(611, 44);
            }
            else
            {
                showSecondSolveToolStripMenuItem.Checked = true;

                panel1.Size = new Size(159, 366);
                listBox1.Size = new Size(156, 303);
                comboBox1.Size = new Size(151, 21);

                //panel1.Location = new Point(423, 44);
                panel2.Visible = true;
            }
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSecondSolveToolStripMenuItem_Click(sender, e);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                listBox2.SelectedIndex = listBox1.SelectedIndex;
            }
            catch { }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                listBox1.SelectedIndex = listBox2.SelectedIndex;
            }
            catch { }
        }

        private void showChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showChartToolStripMenuItem.Checked)
            {
                floatableChartToolStripMenuItem.Enabled = false;
                showChartToolStripMenuItem.Checked = false;
                button2.Visible = false;
                chart1.Visible = false;
                bool isFormChartExist = false;
                Form fc = null;
                foreach (Form f in Application.OpenForms)
                    if (f.Name == "FormChart")
                    {
                        isFormChartExist = true;
                        fc = f;
                    }
                if (fc != null) fc.Close();
            }
            else
            {
                showChartToolStripMenuItem.Checked = true;
                //chart1.Series[0].Points.Clear();
                //chart1.Series[1].Points.Clear();
                button2.Visible = true;
                chart1.Visible = true;
                floatableChartToolStripMenuItem.Enabled = true;
            }
        }

        private void floatableChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (floatableChartToolStripMenuItem.Checked)
            {
                bool isFormChartExist = false;
                Form fc = null;
                foreach (Form f in Application.OpenForms)
                    if (f.Name == "FormChart")
                    {
                        isFormChartExist = true;
                        fc = f;
                    }
                if (fc != null) fc.Close(); //floatableChartToolStripMenuItem.Checked and hideFloatableChartToolStripMenuItem.Checked = false
                button2.Visible = true;
                chart1.Visible = true;
            }
            else
            {
                floatableChartToolStripMenuItem.Checked = true;
                hideFloatableChartToolStripMenuItem.Checked = true;
                FormChart formChart = new FormChart(chart1.Series);
                formChart.Show();
                chart1.Visible = false;
                button2.Visible = false;
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
            if ((textBox.Name == "textBox2" || textBox.Name == "textBox3" || textBox.Name == "textBox4") && textBox.Text == "0")
                textBox.Text = "";
            else if (textBox.Name == "textBox5" && textBox.Text == "1")
                textBox.Text = "";
            else if (textBox.Name == "textBox6" && textBox.Text == "0,1")
                textBox.Text = "";
            textBox.ForeColor = TextBox.DefaultForeColor;
        }

        private void addPlaceHolder(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox.Name == "textBox2" || textBox.Name == "textBox3" || textBox.Name == "textBox4")
                    textBox.Text = "0";
                else if (textBox.Name == "textBox5")
                    textBox.Text = "1";
                else if (textBox.Name == "textBox6")
                    textBox.Text = "0,1";
                textBox.ForeColor = Color.Gray;
            }
        }

        private void textBoxAutoSizeOn(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.BringToFront();
            int textWidth = TextRenderer.MeasureText(textBox.Text, textBox.Font).Width;
            if (textWidth < 22)
                textBox.Width = 22;
            else
                textBox.Width = textWidth + 4;
        }
        private void textBoxAutoSizeOff(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Width = 22;
        }

        private void zoomChart(Conditions cond, double left, double right)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            foreach (Form f in Application.OpenForms)
                if (f.Name == "FormChart")
                {
                    FormChart fc = (FormChart)f;
                    fc.chartF1.Series[0].Points.Clear();
                    fc.chartF1.Series[1].Points.Clear();
                }

            double new_h = (right - left) * cond.h * (cond.xmax - cond.xmin);
            double new_xmin = cond.xmin;
            if (new_xmin < left) new_xmin = left;
            double new_xmax = cond.xmax;
            if (new_xmax > right) new_xmax = right;
            double new_x0 = cond.x0;
            if (cond.x0 < left) cond.x0 = left;

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    //cond.isSecondary = false;
                    cond = new Conditions(cond.equation, new_x0, cond.y0, new_xmin, new_xmax, new_h, false, true);
                    ParameterizedThreadStart pThread = new ParameterizedThreadStart(euler);
                    Thread thread = new Thread(pThread);
                    thread.Start(cond);

                    break;
                case 1:
                    //cond.isSecondary = false;
                    cond = new Conditions(cond.equation, new_x0, cond.y0, new_xmin, new_xmax, new_h, false, true);
                    ParameterizedThreadStart pThreadRk = new ParameterizedThreadStart(rk);
                    Thread threadRk = new Thread(pThreadRk);
                    threadRk.Start(cond);

                    break;
            }
            if (showSecondSolveToolStripMenuItem.Checked)
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        //cond.isSecondary = true;
                        cond = new Conditions(cond.equation, cond.x0, cond.y0, cond.xmin, cond.xmax, cond.h, true, true);
                        ParameterizedThreadStart pThread = new ParameterizedThreadStart(euler);
                        Thread thread = new Thread(pThread);
                        thread.Start(cond);

                        break;
                    case 1:
                        //cond.isSecondary = true;
                        cond = new Conditions(cond.equation, cond.x0, cond.y0, cond.xmin, cond.xmax, cond.h, true, true);
                        ParameterizedThreadStart pThreadRk = new ParameterizedThreadStart(rk);
                        Thread threadRk = new Thread(pThreadRk);
                        threadRk.Start(cond);

                        break;
                }
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePoint = new Point(e.X, e.Y);
            chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, true);
            chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, true);
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
    }
}
