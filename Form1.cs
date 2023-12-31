﻿using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Math;

namespace Курсова
{
    public partial class Form1 : Form
    {
        byte finished = 1;
        bool stopFlag = false;

        private Conditions conditions;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Width = 318;
            listBox1.Width = 315;
            comboBox1.Width = 310;
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

            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.InitialDirectory = Path.GetFullPath("./Export");
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
            if (textBox1.Text == "")
            {
                textBox1.Focus();
                MessageBox.Show("Введіть рівняння", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            button1.Enabled = false;
            button3.Enabled = true;
            finished = 1;

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
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "0.";
            for (int i = 0; i < GetDecimalDigitsCount(h); i++)
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = chart1.ChartAreas[0].AxisX.LabelStyle.Format + "0";
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
            listBox1.Items.Add("x\t y");
            listBox2.Items.Add("x\t y");
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
                case 2:
                    chart1.Series[0].LegendText = "Адамса-Бешфорта";
                    foreach (Form f in Application.OpenForms)
                        if (f.Name == "FormChart")
                        {
                            FormChart fc = (FormChart)f;
                            fc.chartF1.Series[1].LegendText = "Адамса-Бешфорта";
                        }
                    conditions = new Conditions(equation, x0, y0, xmin, xmax, h, false);
                    ParameterizedThreadStart pThreadAd = new ParameterizedThreadStart(adams);
                    Thread ThreadAd = new Thread(pThreadAd);
                    ThreadAd.Start(conditions);

                    break;
            }
            if (showSecondSolveToolStripMenuItem.Checked)
            {
                finished--;
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
                    case 2:
                        chart1.Series[1].LegendText = "Адамса-Бешфорта";
                        foreach (Form f in Application.OpenForms)
                            if (f.Name == "FormChart")
                            {
                                FormChart fc = (FormChart)f;
                                fc.chartF1.Series[1].LegendText = "Адамса-Бешфорта";
                            }
                        conditions = new Conditions(equation, x0, y0, xmin, xmax, h, true);
                        ParameterizedThreadStart pThreadAd = new ParameterizedThreadStart(adams);
                        Thread ThreadAd = new Thread(pThreadAd);
                        ThreadAd.Start(conditions);

                        break;
                }
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
                if (!char.IsDigit(equation[i]) && equation[i] != ',' && i != 0 && equation[i - 1].ToString().ToLower() != "e" && equation[i].ToString().ToLower() != "e")
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
                if (!char.IsDigit(equation[i]) && equation[i] != ',' && i != 0 && equation[i - 1].ToString().ToLower() != "e" && equation[i].ToString().ToLower() != "e")
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
                if (!char.IsDigit(equation[i]) && equation[i] != ',' && i != 0 && equation[i - 1].ToString().ToLower() != "e" && equation[i].ToString().ToLower() != "e")
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
                    Action action0 = () => listBox2.Items.Add(Convert.ToString(Round(x0, GetDecimalDigitsCount(h))) + "\t " + Round(y0, 9));
                    listBox2.Invoke(action0);
                }
            }
            else
            {
                if (!onlyChart)
                {
                    Action action0 = () => listBox1.Items.Add(Convert.ToString(Round(x0, GetDecimalDigitsCount(h))) + "\t " + Round(y0, 9));
                    listBox1.Invoke(action0);
                }
            }
            Action actionP0 = () => addPointToChart(x0, y0, isSecondary);
            Invoke(actionP0);

            int xminDC = GetDecimalDigitsCount(xmin);
            if (xminDC < GetDecimalDigitsCount(h))
                xminDC = GetDecimalDigitsCount(h);
            int xmaxDC = GetDecimalDigitsCount(xmax);
            if (xmaxDC < GetDecimalDigitsCount(h))
                xmaxDC = GetDecimalDigitsCount(h);
            try
            {
                while (Round(xmin, xminDC) + h <= Round(xmax, xmaxDC))
                {
                    double df = der(f, x0, y0);
                    double dfn = der(f, x0 + h, df * h + y0);
                    double y = y0 + (df + dfn) * h / 2; //x*y+y^5-r3(x)

                    x0 += h;
                    y0 = y;
                    xmin += h;

                    if (isSecondary)
                    {
                        if (!onlyChart)
                        {
                            Action action1 = () => listBox2.Items.Add(Convert.ToString(Round(x0, GetDecimalDigitsCount(h))) + "\t " + Round(y0, 9));
                            listBox2.Invoke(action1);
                        }
                    }
                    else
                    {
                        if (!onlyChart)
                        {
                            Action action1 = () => listBox1.Items.Add(Convert.ToString(Round(x0, GetDecimalDigitsCount(h))) + "\t " + Round(y0, 9));
                            listBox1.Invoke(action1);
                        }
                    }
                    Action actionP1 = () => addPointToChart(Round(x0, GetDecimalDigitsCount(h)), y0, isSecondary);
                    Invoke(actionP1);
                    if (stopFlag) break;
                }
            }
            catch
            {
                MessageBox.Show("Помилка в початкових умовах", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (++finished == 2)
            {
                Action actionB1 = () => button1.Enabled = true;
                button1.Invoke(actionB1);
                Action actionB3 = () => button3.Enabled = false;
                button1.Invoke(actionB3);

                stopFlag = false;
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
                    Action action0 = () => listBox2.Items.Add(Convert.ToString(Round(x0, GetDecimalDigitsCount(h))) + "\t " + Round(y0, 9));
                    listBox2.Invoke(action0);
                }
            }
            else
            {
                if (!onlyChart)
                {
                    Action action0 = () => listBox1.Items.Add(Convert.ToString(Round(x0, GetDecimalDigitsCount(h))) + "\t " + Round(y0, 9));
                    listBox1.Invoke(action0);
                }
            }
            Action actionP0 = () => addPointToChart(x0, y0, isSecondary);
            Invoke(actionP0);
            int xminDC = GetDecimalDigitsCount(xmin);
            if (xminDC < GetDecimalDigitsCount(h))
                xminDC = GetDecimalDigitsCount(h);
            int xmaxDC = GetDecimalDigitsCount(xmax);
            if (xmaxDC < GetDecimalDigitsCount(h))
                xmaxDC = GetDecimalDigitsCount(h);
            try
            {
                while (Round(xmin, xminDC) + h <= Round(xmax, xmaxDC))
                {
                    double df = der(f, x0, y0);
                    double dk2 = der(f, x0 + h / 2, y0 + h / 2 * df);
                    double dk3 = der(f, x0 + h / 2, y0 + h / 2 * dk2);
                    double dk4 = der(f, x0 + h, y0 + h * dk3);
                    double y = y0 + h / 6 * (df + 2 * dk2 + 2 * dk3 + dk4);

                    x0 += h;
                    y0 = y;
                    xmin += h;

                    if (isSecondary)
                    {
                        if (!onlyChart)
                        {
                            Action action1 = () => listBox2.Items.Add(Convert.ToString(Round(x0, GetDecimalDigitsCount(h))) + "\t " + Round(y0, 9));
                            listBox2.Invoke(action1);
                        }
                    }
                    else
                    {
                        if (!onlyChart)
                        {
                            Action action1 = () => listBox1.Items.Add(Convert.ToString(Round(x0, GetDecimalDigitsCount(h))) + "\t " + Round(y0, 9));
                            listBox1.Invoke(action1);
                        }
                    }
                    Action actionP1 = () => addPointToChart(x0, y0, isSecondary);
                    Invoke(actionP1);
                    if (stopFlag) break;
                }
            }
            catch
            {
                MessageBox.Show("Помилка в початкових умовах", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (++finished == 2)
            {
                Action actionB1 = () => button1.Enabled = true;
                button1.Invoke(actionB1);
                Action actionB3 = () => button3.Enabled = false;
                button1.Invoke(actionB3);

                stopFlag = false;
            }
        }
        (double, bool) rk(string f, double x0, double y0, double h, bool isSecondary, bool onlyChart = false)
        {
            double y = 0;
            bool error = false;
            try
            {
                double df = der(f, x0, y0);
                double dk2 = der(f, x0 + h / 2, y0 + h / 2 * df);
                double dk3 = der(f, x0 + h / 2, y0 + h / 2 * dk2);
                double dk4 = der(f, x0 + h, y0 + h * dk3);
                y = y0 + h / 6 * (df + 2 * dk2 + 2 * dk3 + dk4);

                x0 += h;
                y0 = y;

                if (isSecondary)
                {
                    if (!onlyChart)
                    {
                        Action action1 = () => listBox2.Items.Add(Convert.ToString(Round(x0, GetDecimalDigitsCount(h))) + "\t " + Round(y0, 9));
                        listBox2.Invoke(action1);
                    }
                }
                else
                {
                    if (!onlyChart)
                    {
                        Action action1 = () => listBox1.Items.Add(Convert.ToString(Round(x0, GetDecimalDigitsCount(h))) + "\t " + Round(y0, 9));
                        listBox1.Invoke(action1);
                    }
                }
                Action actionP1 = () => addPointToChart(x0, y0, isSecondary);
                Invoke(actionP1);
            }
            catch
            {
                error = true;
                MessageBox.Show("Помилка в початкових умовах", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return (y, error);
        }
        void rk(Object obj)
        {
            Conditions cond = obj as Conditions;

            rk(cond.equation, cond.x0, cond.y0, cond.xmin, cond.xmax, cond.h, cond.isSecondary, cond.onlyChart);
        }

        void adams(string f, double x0, double y0, double xmin, double xmax, double h, bool isSecondary, bool onlyChart = false)
        {
            if (isSecondary)
            {
                if (!onlyChart)
                {
                    Action action0 = () => listBox2.Items.Add(Convert.ToString(Round(x0, GetDecimalDigitsCount(h))) + "\t " + Round(y0, 9));
                    listBox2.Invoke(action0);
                }
            }
            else
            {
                if (!onlyChart)
                {
                    Action action0 = () => listBox1.Items.Add(Convert.ToString(Round(x0, GetDecimalDigitsCount(h))) + "\t " + Round(y0, 9));
                    listBox1.Invoke(action0);
                }
            }
            Action actionP0 = () => addPointToChart(x0, y0, isSecondary);
            Invoke(actionP0);
            int xminDC = GetDecimalDigitsCount(xmin);
            if (xminDC < GetDecimalDigitsCount(h))
                xminDC = GetDecimalDigitsCount(h);
            int xmaxDC = GetDecimalDigitsCount(xmax);
            if (xmaxDC < GetDecimalDigitsCount(h))
                xmaxDC = GetDecimalDigitsCount(h);
            try
            {
                (double y1, bool error) = rk(f, x0, y0, h, isSecondary, onlyChart);
                xmin += h;
                if (!error)
                    while (Round(xmin, xminDC) + h <= Round(xmax, xmaxDC))
                    {
                        double x1 = x0 + h;
                        double dp = der(f, x0, y0);
                        double dc = der(f, x1, y1);
                        double y = y1 + 3.0 / 2.0 * h * dc - 1.0 / 2.0 * h * dp;

                        x0 += h;
                        x1 += h;
                        y0 = y1;
                        y1 = y;
                        xmin += h;

                        if (isSecondary)
                        {
                            if (!onlyChart)
                            {
                                Action action1 = () => listBox2.Items.Add(Convert.ToString(Round(x1, GetDecimalDigitsCount(h))) + "\t " + Round(y1, 9));
                                listBox2.Invoke(action1);
                            }
                        }
                        else
                        {
                            if (!onlyChart)
                            {
                                Action action1 = () => listBox1.Items.Add(Convert.ToString(Round(x1, GetDecimalDigitsCount(h))) + "\t " + Round(y1, 9));
                                listBox1.Invoke(action1);
                            }
                        }
                        Action actionP1 = () => addPointToChart(x0, y0, isSecondary);
                        Invoke(actionP1);
                        if (stopFlag) break;
                    }
            }
            catch
            {
                MessageBox.Show("Помилка в початкових умовах", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (++finished == 2)
            {
                Action actionB1 = () => button1.Enabled = true;
                button1.Invoke(actionB1);
                Action actionB3 = () => button3.Enabled = false;
                button1.Invoke(actionB3);

                stopFlag = false;
            }
        }
        void adams(Object obj)
        {
            Conditions cond = obj as Conditions;

            adams(cond.equation, cond.x0, cond.y0, cond.xmin, cond.xmax, cond.h, cond.isSecondary, cond.onlyChart);
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
                showSecondSolveToolStripContextMenuItem.Checked = false;
                panel2.Visible = false;

                panel1.Width = 318;
                listBox1.Width = 315;
                comboBox1.Width = 310;

                //panel1.Location = new Point(611, 44);
            }
            else
            {
                showSecondSolveToolStripMenuItem.Checked = true;
                showSecondSolveToolStripContextMenuItem.Checked = true;

                panel1.Width = 159;
                listBox1.Width = 156;
                comboBox1.Width = 151;

                //panel1.Location = new Point(423, 44);
                panel2.Visible = true;
            }
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
                panel3.Visible = false;
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
                panel3.Visible = true;
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
                panel3.Visible = true;
            }
            else
            {
                floatableChartToolStripMenuItem.Checked = true;
                hideFloatableChartToolStripMenuItem.Checked = true;
                FormChart formChart = new FormChart(chart1.Series);
                formChart.Show();
                panel3.Visible = false;
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
                if ((textBox.Name == "textBox2" || textBox.Name == "textBox3" || textBox.Name == "textBox4") && textBox.Text == "0")
                    textBox.Text = "";
                else if (textBox.Name == "textBox5" && textBox.Text == "1")
                    textBox.Text = "";
                else if (textBox.Name == "textBox6" && textBox.Text == "0,1")
                    textBox.Text = "";
            }
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

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (finished == 2)
            {
                Point mousePoint = new Point(e.X, e.Y);
                chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, true);
                chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, true);
            }
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
            stopFlag = true;
        }

        private void exportChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "chart";
            saveFileDialog1.Filter = "JPEG |*.jpeg|PNG |*.png|Точковий малюнок |*.bmp|TIFF |*.tiff|GIF |*.gif";
            if (finished == 2)
                try
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        chart1.SaveImage(saveFileDialog1.FileName, (ChartImageFormat)saveFileDialog1.FilterIndex);
                }
                catch
                {
                    MessageBox.Show("Помилка збереження", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            else MessageBox.Show("Неможливо експортувати до розрахунку", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void exportListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "points";
            saveFileDialog1.Filter = "Книга Excel |*.xlsx|Книга Excel з підтримкою макросів |*.xlsm|Шаблон Excel |*.xltx|Шаблон Excel з підтримкою макросів |*.xltm|Текстовий файл |*.txt";
            if (finished == 2)
                try
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if (saveFileDialog1.FilterIndex == 5)
                        {
                            var sw = new StreamWriter(saveFileDialog1.FileName);
                            sw.WriteLine("Методом " + comboBox1.Text);
                            for (int i = 0; i < listBox1.Items.Count; i++)
                            {
                                sw.WriteLine(listBox1.Items[i].ToString());
                            }
                            if (showSecondSolveToolStripMenuItem.Checked)
                            {
                                sw.WriteLine("Методом " + comboBox2.Text);
                                for (int i = 0; i < listBox2.Items.Count; i++)
                                {
                                    sw.WriteLine(listBox2.Items[i].ToString());
                                }
                            }
                            sw.Close();
                        }
                        else
                        {
                            var workbook = new XLWorkbook();
                            var sheet = workbook.Worksheets.Add("Результат рішення Диф.рівнянь");

                            sheet.Cell(1, 1).SetValue("Методом " + comboBox1.Text);
                            for (int i = 0; i < listBox1.Items.Count; i++)
                            {
                                string[] row = listBox1.Items[i].ToString().Split('\t');
                                sheet.Cell(i + 2, 1).SetValue(row[0]);
                                sheet.Cell(i + 2, 2).SetValue(row[1]);
                            }
                            if (showSecondSolveToolStripMenuItem.Checked)
                            {
                                sheet.Cell(1, 4).SetValue("Методом " + comboBox2.Text);
                                for (int i = 0; i < listBox2.Items.Count; i++)
                                {
                                    string[] row2 = listBox2.Items[i].ToString().Split('\t');
                                    sheet.Cell(i + 2, 4).SetValue(row2[0]);
                                    sheet.Cell(i + 2, 5).SetValue(row2[1]);
                                }
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
        private void copyPoint(bool isSecondary)
        {
            ListBox listBox = isSecondary ? listBox2 : listBox1;
            try
            {
                Clipboard.SetText(listBox.Text, TextDataFormat.UnicodeText);
            }
            catch {}
        }

        private void copyPointToolStripContextMenuItem_Click(object sender, EventArgs e)
        {
            copyPoint(false);
        }

        private void copyPointToolStripContextMenuItem1_Click(object sender, EventArgs e)
        {
            copyPoint(true);
        }

        private void copyAllPoints(bool isSecondary)
        {
            ListBox listBox = isSecondary ? listBox2 : listBox1;
            StringBuilder text = new StringBuilder();
            foreach (var item in listBox.Items)
            {
                text.AppendLine(item.ToString());
            }
            try
            {
                Clipboard.SetText(text.ToString(), TextDataFormat.UnicodeText);
            }
            catch { }
        }

        private void copyAllPointsToolStripContextMenuItem_Click(object sender, EventArgs e)
        {
            copyAllPoints(false);
        }

        private void copyAllPointsToolStripContextMenuItem1_Click(object sender, EventArgs e)
        {
            copyAllPoints(true);
        }

        private void allowCopyByClickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (allowCopyByClickToolStripMenuItem.Checked)
            {
                allowCopyByClickToolStripMenuItem.Checked = false;
                listBox1.Click -= listBox_Click;
                listBox2.Click -= listBox_Click;
            }
            else
            {
                allowCopyByClickToolStripMenuItem.Checked = true;
                listBox1.Click += listBox_Click;
                listBox2.Click += listBox_Click;
            }
        }
    }
}
