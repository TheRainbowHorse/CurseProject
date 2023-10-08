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
using static System.Math;

namespace Курсова
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Size = new Size(318, 366);
            listBox1.Size = new Size(315, 290);
            comboBox1.Size = new Size(310, 21);
            //panel1.Location = new Point(611, 44);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private class Conditions
        {
            public string equation;
            public double xmin, xmax, y0, x0, h;
            public bool isSecondary;

            public Conditions(string equation, double x0, double y0, double xmin, double xmax, double h, bool isSecondary)
            {
                this.equation = equation;
                this.xmin = xmin;
                this.xmax = xmax;
                this.y0 = y0;
                this.x0 = x0;
                this.h = h;
                this.isSecondary = isSecondary;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double xmin;
            double xmax;
            double y0;
            double x0;
            double h;

            if (!String.IsNullOrEmpty(textBox2.Text)) x0 = double.Parse(textBox2.Text.Replace('.',',')); else x0 = 0;
            if (!String.IsNullOrEmpty(textBox3.Text)) y0 = double.Parse(textBox3.Text.Replace('.', ',')); else y0 = 0;
            if (!String.IsNullOrEmpty(textBox4.Text)) xmin = double.Parse(textBox4.Text.Replace('.', ',')); else xmin = 0;
            if (!String.IsNullOrEmpty(textBox5.Text)) xmax = double.Parse(textBox5.Text.Replace('.', ',')); else xmax = 1;
            if (!String.IsNullOrEmpty(textBox6.Text)) h = double.Parse(textBox6.Text.Replace('.', ',')); else h = 0.1;

            string equation = textBox1.Text;//x+cos(y/sqrt(0.7))

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            foreach (Form f in Application.OpenForms)
                if (f.Name == "FormChart")
                {
                    FormChart fc = (FormChart)f;
                    fc.chartF1.Series[0].Points.Clear();
                    fc.chartF1.Series[1].Points.Clear();
                }
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Conditions conditions = new Conditions(equation, x0, y0, xmin, xmax, h, false);
                    ParameterizedThreadStart pThread = new ParameterizedThreadStart(euler);
                    Thread thread = new Thread(pThread);
                    thread.Start(conditions);

                    break;
                case 1:
                    Conditions conditionsRk = new Conditions(equation, x0, y0, xmin, xmax, h, false);
                    ParameterizedThreadStart pThreadRk = new ParameterizedThreadStart(rk);
                    Thread threadRk = new Thread(pThreadRk);
                    threadRk.Start(conditionsRk);

                    break;
            }
            if (showSecondSolveToolStripMenuItem.Checked)
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        Conditions conditions = new Conditions(equation, x0, y0, xmin, xmax, h, true);
                        ParameterizedThreadStart pThread = new ParameterizedThreadStart(euler);
                        Thread thread = new Thread(pThread);
                        thread.Start(conditions);

                        break;
                    case 1:
                        Conditions conditionsRk = new Conditions(equation, x0, y0, xmin, xmax, h, true);
                        ParameterizedThreadStart pThreadRk = new ParameterizedThreadStart(rk);
                        Thread threadRk = new Thread(pThreadRk);
                        threadRk.Start(conditionsRk);

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
                            equation = equation.Replace("r" + newBase + "(" + toBeReplace + ")", Pow(double.Parse(calculate(toBeReplace)), 1.0/double.Parse(newBase)).ToString());
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

            for (int i = 0; i < equation.Length; i++)
            {
                if (!char.IsDigit(equation[i]) && equation[i] != ',')
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

            for (int i = operatorPos - 1; i >= 0; i--)//Finds left number around the symbol
            {
                if (char.IsDigit(expression[i]) || expression[i] == ',')
                {
                    numLeftStr += expression[i];
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
                else break;
            }
            double res = 0;
            numRight = double.Parse(numRightStr);
            if (numLeftStr.Length != 0)
            {
                numLeft = double.Parse(numLeftStr);
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
            expression = expression.Replace(numLeftStr + expression[operatorPos] + numRightStr, res.ToString());
            return expression;
        }

        double der(string f, double x, double y)
        {
            return double.Parse(calculate(f.Replace("x", Convert.ToString(x)).Replace("y", Convert.ToString(y))));
        }

        void euler(string f, double x0, double y0, double xmin, double xmax, double h, bool isSecondary)
        {
            if (isSecondary)
            {
                Action action0 = () => listBox2.Items.Add(Convert.ToString(x0) + ") " + y0);
                listBox2.Invoke(action0);
            }
            else
            {
                Action action0 = () => listBox1.Items.Add(Convert.ToString(x0) + ") " + y0);
                listBox1.Invoke(action0);
            }

            int iteration = 0;
            double addition = 0;
            while (xmin + h < xmax)
            {
                double y = y0 + (der(f, x0, y0) + der(f, x0 + h, der(f, x0, y0) * h + y0)) * h / 2;

                x0 += h;
                y0 = y;
                xmin += h;

                if (isSecondary)
                {
                    Action action1 = () => listBox2.Items.Add(Convert.ToString(Round(x0, 4)) + ") " + y);
                    listBox2.Invoke(action1);
                }
                else
                {
                    Action action1 = () => listBox1.Items.Add(Convert.ToString(Round(x0, 4)) + ") " + y);
                    listBox1.Invoke(action1);
                }
                if (iteration < 5)
                {
                    Action actionP = () => addPointToChart(x0, y * 100 - Round(y * 100) + addition, isSecondary);
                    Invoke(actionP);
                    addition += 0.9;
                }
                iteration++;
            }
        }
        void euler(Object obj)
        {
            Conditions cond = obj as Conditions;
            string f = cond.equation;
            double x0 = cond.x0;
            double y0 = cond.y0;
            double xmin = cond.xmin;
            double xmax = cond.xmax;
            double h = cond.h;
            bool isSecondary = cond.isSecondary;

            euler(f, x0, y0, xmin, xmax, h, isSecondary);
        }

        void rk(string f, double x0, double y0, double xmin, double xmax, double h, bool isSecondary)
        {
            if (isSecondary)
            {
                Action action0 = () => listBox2.Items.Add(Convert.ToString(x0) + ") " + y0);
                listBox2.Invoke(action0);
            }
            else
            {
                Action action0 = () => listBox1.Items.Add(Convert.ToString(x0) + ") " + y0);
                listBox1.Invoke(action0);
            }

            int iteration = 0;
            double addition = 0;
            while (xmin + h < xmax)
            {
                double y = y0 + h / 6 * (der(f, x0, y0) + 2 * k2(x0,y0,h) + 2 * k3(x0, y0, h) + k4(x0,y0,h));

                x0 += h;
                y0 = y;
                xmin += h;

                if (isSecondary)
                {
                    Action action1 = () => listBox2.Items.Add(Convert.ToString(Round(x0, 4)) + ") " + y);
                    listBox2.Invoke(action1);
                }
                else
                {
                    Action action1 = () => listBox1.Items.Add(Convert.ToString(Round(x0, 4)) + ") " + y);
                    listBox1.Invoke(action1);
                }
                if (iteration < 5)
                {
                    Action actionP = () => addPointToChart(x0, y * 100 - Round(y * 100) + addition, isSecondary);
                    Invoke(actionP);
                    addition += 0.9;
                }
                iteration++;
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
            string f = cond.equation;
            double x0 = cond.x0;
            double y0 = cond.y0;
            double xmin = cond.xmin;
            double xmax = cond.xmax;
            double h = cond.h;
            bool isSecondary = cond.isSecondary;

            rk(f, x0, y0, xmin, xmax, h, isSecondary);
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
                listBox1.Size = new Size(315, 290);
                comboBox1.Size = new Size(310, 21);

                //panel1.Location = new Point(611, 44);
            }
            else
            {
                showSecondSolveToolStripMenuItem.Checked = true;

                panel1.Size = new Size(159, 366);
                listBox1.Size = new Size(156, 290);
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
                chart1.Visible = true;
            }
            else
            {
                floatableChartToolStripMenuItem.Checked = true;
                hideFloatableChartToolStripMenuItem.Checked = true;
                FormChart formChart = new FormChart(chart1.Series);
                formChart.Show();
                chart1.Visible = false;
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
    }
}
