
namespace Курсова
{
    partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBoxHelpWindowOnStart = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxUseDefaultConditions = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxH = new System.Windows.Forms.TextBox();
            this.textBoxXmax = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxXmin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxY0 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxX0 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBoxMultiSelect = new System.Windows.Forms.CheckBox();
            this.checkBoxIsIterationLimited = new System.Windows.Forms.CheckBox();
            this.numericUpDownIterationLimit = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownDecimalPlacesListY = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonShowManyMethods = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkBoxHideLegend = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDownDecimalPlacesChartY = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.checkBoxShowMarkersOnChart = new System.Windows.Forms.CheckBox();
            this.numericUpDownMaxPointsAtChart = new System.Windows.Forms.NumericUpDown();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxChartFileName = new System.Windows.Forms.TextBox();
            this.textBoxPointsFileName = new System.Windows.Forms.TextBox();
            this.buttonExportFolder = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxExportPath = new System.Windows.Forms.TextBox();
            this.checkBoxCopyByClick = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIterationLimit)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDecimalPlacesListY)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDecimalPlacesChartY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxPointsAtChart)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ItemSize = new System.Drawing.Size(25, 100);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(704, 339);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.checkBoxHelpWindowOnStart);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(104, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(596, 331);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Умови";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBoxHelpWindowOnStart
            // 
            this.checkBoxHelpWindowOnStart.AutoSize = true;
            this.checkBoxHelpWindowOnStart.Checked = global::Курсова.Properties.Settings.Default.hideHelpWindowOnStart;
            this.checkBoxHelpWindowOnStart.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Курсова.Properties.Settings.Default, "hideHelpWindowOnStart", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxHelpWindowOnStart.Location = new System.Drawing.Point(24, 262);
            this.checkBoxHelpWindowOnStart.Name = "checkBoxHelpWindowOnStart";
            this.checkBoxHelpWindowOnStart.Size = new System.Drawing.Size(342, 17);
            this.checkBoxHelpWindowOnStart.TabIndex = 2;
            this.checkBoxHelpWindowOnStart.Text = "Приховати cповіщення про довідку при запуску програми";
            this.checkBoxHelpWindowOnStart.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.checkBoxUseDefaultConditions);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxH);
            this.groupBox1.Controls.Add(this.textBoxXmax);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxXmin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxY0);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxX0);
            this.groupBox1.Location = new System.Drawing.Point(6, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(583, 204);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Значення за замовчуванням";
            // 
            // checkBoxUseDefaultConditions
            // 
            this.checkBoxUseDefaultConditions.AutoSize = true;
            this.checkBoxUseDefaultConditions.Checked = global::Курсова.Properties.Settings.Default.useDefaultConditions;
            this.checkBoxUseDefaultConditions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseDefaultConditions.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Курсова.Properties.Settings.Default, "useDefaultConditions", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxUseDefaultConditions.Location = new System.Drawing.Point(33, 32);
            this.checkBoxUseDefaultConditions.Name = "checkBoxUseDefaultConditions";
            this.checkBoxUseDefaultConditions.Size = new System.Drawing.Size(466, 17);
            this.checkBoxUseDefaultConditions.TabIndex = 7;
            this.checkBoxUseDefaultConditions.Text = "Використовувати значення за замовчуванням, якщо не вказано початкових умов";
            this.checkBoxUseDefaultConditions.UseVisualStyleBackColor = true;
            this.checkBoxUseDefaultConditions.CheckedChanged += new System.EventHandler(this.checkBoxUseDefaultConditions_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Максимальне значення x (xmax):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Крок (h):";
            // 
            // textBoxH
            // 
            this.textBoxH.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Курсова.Properties.Settings.Default, "defaultH", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxH.Location = new System.Drawing.Point(220, 167);
            this.textBoxH.Name = "textBoxH";
            this.textBoxH.Size = new System.Drawing.Size(100, 22);
            this.textBoxH.TabIndex = 4;
            this.textBoxH.Text = global::Курсова.Properties.Settings.Default.defaultH;
            // 
            // textBoxXmax
            // 
            this.textBoxXmax.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Курсова.Properties.Settings.Default, "defaultXmax", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxXmax.Location = new System.Drawing.Point(220, 139);
            this.textBoxXmax.Name = "textBoxXmax";
            this.textBoxXmax.Size = new System.Drawing.Size(100, 22);
            this.textBoxXmax.TabIndex = 3;
            this.textBoxXmax.Text = global::Курсова.Properties.Settings.Default.defaultXmax;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Мінімальне значення x (xmin):";
            // 
            // textBoxXmin
            // 
            this.textBoxXmin.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Курсова.Properties.Settings.Default, "defaultXmin", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxXmin.Location = new System.Drawing.Point(220, 111);
            this.textBoxXmin.Name = "textBoxXmin";
            this.textBoxXmin.Size = new System.Drawing.Size(100, 22);
            this.textBoxXmin.TabIndex = 2;
            this.textBoxXmin.Text = global::Курсова.Properties.Settings.Default.defaultXmin;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Початкове значення y (y0):";
            // 
            // textBoxY0
            // 
            this.textBoxY0.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Курсова.Properties.Settings.Default, "defaultY0", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxY0.Location = new System.Drawing.Point(220, 83);
            this.textBoxY0.Name = "textBoxY0";
            this.textBoxY0.Size = new System.Drawing.Size(100, 22);
            this.textBoxY0.TabIndex = 1;
            this.textBoxY0.Text = global::Курсова.Properties.Settings.Default.defaultY0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Початкове значення x (x0):";
            // 
            // textBoxX0
            // 
            this.textBoxX0.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Курсова.Properties.Settings.Default, "defaultX0", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxX0.Location = new System.Drawing.Point(220, 55);
            this.textBoxX0.Name = "textBoxX0";
            this.textBoxX0.Size = new System.Drawing.Size(100, 22);
            this.textBoxX0.TabIndex = 0;
            this.textBoxX0.Text = global::Курсова.Properties.Settings.Default.defaultX0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Керування початковими умовами";
            // 
            // tabPage2
            // 
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage2.Controls.Add(this.checkBoxMultiSelect);
            this.tabPage2.Controls.Add(this.checkBoxIsIterationLimited);
            this.tabPage2.Controls.Add(this.numericUpDownIterationLimit);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.buttonShowManyMethods);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(104, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(596, 331);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Розразунок";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBoxMultiSelect
            // 
            this.checkBoxMultiSelect.AutoSize = true;
            this.checkBoxMultiSelect.Checked = global::Курсова.Properties.Settings.Default.multiSelect;
            this.checkBoxMultiSelect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMultiSelect.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Курсова.Properties.Settings.Default, "multiSelect", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxMultiSelect.Location = new System.Drawing.Point(31, 186);
            this.checkBoxMultiSelect.Name = "checkBoxMultiSelect";
            this.checkBoxMultiSelect.Size = new System.Drawing.Size(173, 17);
            this.checkBoxMultiSelect.TabIndex = 6;
            this.checkBoxMultiSelect.Text = "Спілне виділення у списках";
            this.checkBoxMultiSelect.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsIterationLimited
            // 
            this.checkBoxIsIterationLimited.AutoSize = true;
            this.checkBoxIsIterationLimited.Checked = global::Курсова.Properties.Settings.Default.isIterationLimited;
            this.checkBoxIsIterationLimited.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Курсова.Properties.Settings.Default, "isIterationLimited", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxIsIterationLimited.Location = new System.Drawing.Point(31, 158);
            this.checkBoxIsIterationLimited.Name = "checkBoxIsIterationLimited";
            this.checkBoxIsIterationLimited.Size = new System.Drawing.Size(256, 17);
            this.checkBoxIsIterationLimited.TabIndex = 5;
            this.checkBoxIsIterationLimited.Text = "Обмежити максимальну кількість ітерацій:";
            this.checkBoxIsIterationLimited.UseVisualStyleBackColor = true;
            this.checkBoxIsIterationLimited.CheckedChanged += new System.EventHandler(this.checkBoxIsIterationLimited_CheckedChanged);
            // 
            // numericUpDownIterationLimit
            // 
            this.numericUpDownIterationLimit.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Курсова.Properties.Settings.Default, "iterationLimit", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDownIterationLimit.Location = new System.Drawing.Point(293, 157);
            this.numericUpDownIterationLimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownIterationLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownIterationLimit.Name = "numericUpDownIterationLimit";
            this.numericUpDownIterationLimit.Size = new System.Drawing.Size(128, 22);
            this.numericUpDownIterationLimit.TabIndex = 4;
            this.numericUpDownIterationLimit.Value = global::Курсова.Properties.Settings.Default.iterationLimit;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.numericUpDownDecimalPlacesListY);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(6, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(581, 63);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Округлення значень";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(225, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "знаку після коми";
            // 
            // numericUpDownDecimalPlacesListY
            // 
            this.numericUpDownDecimalPlacesListY.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Курсова.Properties.Settings.Default, "decimalPlacesListY", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDownDecimalPlacesListY.Location = new System.Drawing.Point(184, 25);
            this.numericUpDownDecimalPlacesListY.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUpDownDecimalPlacesListY.Name = "numericUpDownDecimalPlacesListY";
            this.numericUpDownDecimalPlacesListY.Size = new System.Drawing.Size(35, 22);
            this.numericUpDownDecimalPlacesListY.TabIndex = 4;
            this.numericUpDownDecimalPlacesListY.Value = global::Курсова.Properties.Settings.Default.decimalPlacesListY;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(156, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Округлення значення (y) до";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(169, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Відобразити декілька методів:";
            // 
            // buttonShowManyMethods
            // 
            this.buttonShowManyMethods.Location = new System.Drawing.Point(205, 42);
            this.buttonShowManyMethods.Name = "buttonShowManyMethods";
            this.buttonShowManyMethods.Size = new System.Drawing.Size(164, 23);
            this.buttonShowManyMethods.TabIndex = 1;
            this.buttonShowManyMethods.Text = "Параметри відображення...";
            this.buttonShowManyMethods.UseVisualStyleBackColor = true;
            this.buttonShowManyMethods.Click += new System.EventHandler(this.buttonShowManyMethods_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Параметри розрахунку";
            // 
            // tabPage3
            // 
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage3.Controls.Add(this.checkBoxHideLegend);
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.checkBoxShowMarkersOnChart);
            this.tabPage3.Controls.Add(this.numericUpDownMaxPointsAtChart);
            this.tabPage3.Location = new System.Drawing.Point(104, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(596, 331);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Графік";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // checkBoxHideLegend
            // 
            this.checkBoxHideLegend.AutoSize = true;
            this.checkBoxHideLegend.Checked = global::Курсова.Properties.Settings.Default.hideLegend;
            this.checkBoxHideLegend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHideLegend.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Курсова.Properties.Settings.Default, "hideLegend", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxHideLegend.Location = new System.Drawing.Point(37, 219);
            this.checkBoxHideLegend.Name = "checkBoxHideLegend";
            this.checkBoxHideLegend.Size = new System.Drawing.Size(129, 17);
            this.checkBoxHideLegend.TabIndex = 6;
            this.checkBoxHideLegend.Text = "Приховати легенду";
            this.checkBoxHideLegend.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDownDecimalPlacesChartY);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Location = new System.Drawing.Point(10, 131);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(577, 72);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Округлення значень";
            // 
            // numericUpDownDecimalPlacesChartY
            // 
            this.numericUpDownDecimalPlacesChartY.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Курсова.Properties.Settings.Default, "decimalPlacesChartY", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDownDecimalPlacesChartY.Location = new System.Drawing.Point(201, 29);
            this.numericUpDownDecimalPlacesChartY.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUpDownDecimalPlacesChartY.Name = "numericUpDownDecimalPlacesChartY";
            this.numericUpDownDecimalPlacesChartY.Size = new System.Drawing.Size(35, 22);
            this.numericUpDownDecimalPlacesChartY.TabIndex = 4;
            this.numericUpDownDecimalPlacesChartY.Value = global::Курсова.Properties.Settings.Default.decimalPlacesChartY;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(24, 31);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(171, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Кількість знаків після коми (y):";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(334, 85);
            this.label13.MaximumSize = new System.Drawing.Size(180, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(180, 26);
            this.label13.TabIndex = 3;
            this.label13.Text = "Великі значення можуть сильно вповільнити роботу програми!";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(33, 85);
            this.label12.MaximumSize = new System.Drawing.Size(250, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(225, 26);
            this.label12.TabIndex = 1;
            this.label12.Text = "Максимальна кількість точок на графіку (окрім точки x0):";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Налаштування графіків";
            // 
            // checkBoxShowMarkersOnChart
            // 
            this.checkBoxShowMarkersOnChart.AutoSize = true;
            this.checkBoxShowMarkersOnChart.Checked = global::Курсова.Properties.Settings.Default.showMarker;
            this.checkBoxShowMarkersOnChart.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Курсова.Properties.Settings.Default, "showMarker", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxShowMarkersOnChart.Location = new System.Drawing.Point(33, 54);
            this.checkBoxShowMarkersOnChart.Name = "checkBoxShowMarkersOnChart";
            this.checkBoxShowMarkersOnChart.Size = new System.Drawing.Size(189, 17);
            this.checkBoxShowMarkersOnChart.TabIndex = 5;
            this.checkBoxShowMarkersOnChart.Text = "Відображати точки на графіку";
            this.checkBoxShowMarkersOnChart.UseVisualStyleBackColor = true;
            // 
            // numericUpDownMaxPointsAtChart
            // 
            this.numericUpDownMaxPointsAtChart.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::Курсова.Properties.Settings.Default, "maxPointsAtChart", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDownMaxPointsAtChart.Location = new System.Drawing.Point(264, 83);
            this.numericUpDownMaxPointsAtChart.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownMaxPointsAtChart.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownMaxPointsAtChart.Name = "numericUpDownMaxPointsAtChart";
            this.numericUpDownMaxPointsAtChart.Size = new System.Drawing.Size(64, 22);
            this.numericUpDownMaxPointsAtChart.TabIndex = 2;
            this.numericUpDownMaxPointsAtChart.Value = global::Курсова.Properties.Settings.Default.maxPointsAtChart;
            // 
            // tabPage4
            // 
            this.tabPage4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Controls.Add(this.buttonExportFolder);
            this.tabPage4.Controls.Add(this.label17);
            this.tabPage4.Controls.Add(this.label16);
            this.tabPage4.Controls.Add(this.textBoxExportPath);
            this.tabPage4.Controls.Add(this.checkBoxCopyByClick);
            this.tabPage4.Location = new System.Drawing.Point(104, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(596, 331);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Експорт";
            this.tabPage4.UseVisualStyleBackColor = true;
            this.tabPage4.Click += new System.EventHandler(this.buttonExportFolder_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.textBoxChartFileName);
            this.groupBox4.Controls.Add(this.textBoxPointsFileName);
            this.groupBox4.Location = new System.Drawing.Point(6, 137);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(581, 94);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Назва файлу при збереженні:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(24, 27);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(81, 13);
            this.label18.TabIndex = 7;
            this.label18.Text = "Файл графіка:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(24, 55);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(72, 13);
            this.label19.TabIndex = 8;
            this.label19.Text = "Файл точок:";
            // 
            // textBoxChartFileName
            // 
            this.textBoxChartFileName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Курсова.Properties.Settings.Default, "chartFileName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxChartFileName.Location = new System.Drawing.Point(111, 24);
            this.textBoxChartFileName.Name = "textBoxChartFileName";
            this.textBoxChartFileName.Size = new System.Drawing.Size(100, 22);
            this.textBoxChartFileName.TabIndex = 5;
            this.textBoxChartFileName.Text = global::Курсова.Properties.Settings.Default.chartFileName;
            // 
            // textBoxPointsFileName
            // 
            this.textBoxPointsFileName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Курсова.Properties.Settings.Default, "pointsFileName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxPointsFileName.Location = new System.Drawing.Point(111, 52);
            this.textBoxPointsFileName.Name = "textBoxPointsFileName";
            this.textBoxPointsFileName.Size = new System.Drawing.Size(100, 22);
            this.textBoxPointsFileName.TabIndex = 6;
            this.textBoxPointsFileName.Text = global::Курсова.Properties.Settings.Default.pointsFileName;
            // 
            // buttonExportFolder
            // 
            this.buttonExportFolder.Location = new System.Drawing.Point(450, 97);
            this.buttonExportFolder.Name = "buttonExportFolder";
            this.buttonExportFolder.Size = new System.Drawing.Size(75, 23);
            this.buttonExportFolder.TabIndex = 4;
            this.buttonExportFolder.Text = "Перегляд...";
            this.buttonExportFolder.UseVisualStyleBackColor = true;
            this.buttonExportFolder.Click += new System.EventHandler(this.buttonExportFolder_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(22, 102);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(112, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "Папка збереження:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(22, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(171, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Параметри збереження даних";
            // 
            // textBoxExportPath
            // 
            this.textBoxExportPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Курсова.Properties.Settings.Default, "ExportPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxExportPath.Location = new System.Drawing.Point(140, 99);
            this.textBoxExportPath.Name = "textBoxExportPath";
            this.textBoxExportPath.Size = new System.Drawing.Size(304, 22);
            this.textBoxExportPath.TabIndex = 1;
            this.textBoxExportPath.Text = global::Курсова.Properties.Settings.Default.exportPath;
            // 
            // checkBoxCopyByClick
            // 
            this.checkBoxCopyByClick.AutoSize = true;
            this.checkBoxCopyByClick.Checked = global::Курсова.Properties.Settings.Default.copyByClick;
            this.checkBoxCopyByClick.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Курсова.Properties.Settings.Default, "CopyByClick", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxCopyByClick.Location = new System.Drawing.Point(16, 61);
            this.checkBoxCopyByClick.Name = "checkBoxCopyByClick";
            this.checkBoxCopyByClick.Size = new System.Drawing.Size(197, 17);
            this.checkBoxCopyByClick.TabIndex = 0;
            this.checkBoxCopyByClick.Text = "Копіювання натисканням миші:";
            this.checkBoxCopyByClick.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 339);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(704, 38);
            this.panel1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(229, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Повернути значення за замовчуванням";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(619, 8);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Скасувати";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(538, 8);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "ОК";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(704, 339);
            this.panel2.TabIndex = 2;
            // 
            // FormSettings
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(704, 377);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Налаштування";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIterationLimit)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDecimalPlacesListY)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDecimalPlacesChartY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxPointsAtChart)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxX0;
        private System.Windows.Forms.TextBox textBoxY0;
        private System.Windows.Forms.TextBox textBoxXmin;
        private System.Windows.Forms.TextBox textBoxXmax;
        private System.Windows.Forms.TextBox textBoxH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBoxUseDefaultConditions;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonShowManyMethods;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDownDecimalPlacesListY;
        private System.Windows.Forms.NumericUpDown numericUpDownIterationLimit;
        private System.Windows.Forms.CheckBox checkBoxIsIterationLimited;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxPointsAtChart;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericUpDownDecimalPlacesChartY;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBoxShowMarkersOnChart;
        private System.Windows.Forms.CheckBox checkBoxHideLegend;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxChartFileName;
        private System.Windows.Forms.TextBox textBoxPointsFileName;
        private System.Windows.Forms.Button buttonExportFolder;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxExportPath;
        private System.Windows.Forms.CheckBox checkBoxCopyByClick;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBoxMultiSelect;
        private System.Windows.Forms.CheckBox checkBoxHelpWindowOnStart;
    }
}