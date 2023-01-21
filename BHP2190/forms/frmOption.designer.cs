namespace BHP2190.forms
{
    partial class frmOption
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOption));
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.BtnColorDraw = new System.Windows.Forms.Button();
            this.cmbStyleDraw = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.cbDrawLine = new System.Windows.Forms.CheckBox();
            this.cbSelection = new System.Windows.Forms.CheckBox();
            this.rbPoint = new System.Windows.Forms.RadioButton();
            this.rbfLine = new System.Windows.Forms.RadioButton();
            this.rbLine = new System.Windows.Forms.RadioButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button3.Location = new System.Drawing.Point(277, 75);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(70, 23);
            this.button3.TabIndex = 29;
            this.button3.Text = "OK";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.BtnColorDraw);
            this.groupBox5.Controls.Add(this.cmbStyleDraw);
            this.groupBox5.Controls.Add(this.label32);
            this.groupBox5.Controls.Add(this.cbDrawLine);
            this.groupBox5.Location = new System.Drawing.Point(6, 29);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(416, 40);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            // 
            // BtnColorDraw
            // 
            this.BtnColorDraw.Location = new System.Drawing.Point(271, 11);
            this.BtnColorDraw.Name = "BtnColorDraw";
            this.BtnColorDraw.Size = new System.Drawing.Size(43, 23);
            this.BtnColorDraw.TabIndex = 23;
            this.BtnColorDraw.Text = "Color";
            this.BtnColorDraw.UseVisualStyleBackColor = true;
            this.BtnColorDraw.Click += new System.EventHandler(this.BtnColorDraw_Click);
            this.BtnColorDraw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // cmbStyleDraw
            // 
            this.cmbStyleDraw.FormattingEnabled = true;
            this.cmbStyleDraw.Items.AddRange(new object[] {
            "Solid",
            "Dash",
            "DashDot",
            "DashDotDot",
            "Dot"});
            this.cmbStyleDraw.Location = new System.Drawing.Point(115, 13);
            this.cmbStyleDraw.Name = "cmbStyleDraw";
            this.cmbStyleDraw.Size = new System.Drawing.Size(77, 21);
            this.cmbStyleDraw.TabIndex = 22;
            this.cmbStyleDraw.Text = "Solid";
            this.cmbStyleDraw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(80, 16);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(30, 13);
            this.label32.TabIndex = 3;
            this.label32.Text = "Style";
            // 
            // cbDrawLine
            // 
            this.cbDrawLine.AutoSize = true;
            this.cbDrawLine.ForeColor = System.Drawing.Color.Fuchsia;
            this.cbDrawLine.Location = new System.Drawing.Point(8, -1);
            this.cbDrawLine.Name = "cbDrawLine";
            this.cbDrawLine.Size = new System.Drawing.Size(74, 17);
            this.cbDrawLine.TabIndex = 20;
            this.cbDrawLine.Text = "Draw Line";
            this.cbDrawLine.UseVisualStyleBackColor = true;
            this.cbDrawLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // cbSelection
            // 
            this.cbSelection.AutoSize = true;
            this.cbSelection.ForeColor = System.Drawing.Color.Fuchsia;
            this.cbSelection.Location = new System.Drawing.Point(17, 79);
            this.cbSelection.Name = "cbSelection";
            this.cbSelection.Size = new System.Drawing.Size(68, 17);
            this.cbSelection.TabIndex = 28;
            this.cbSelection.Text = "selection";
            this.cbSelection.UseVisualStyleBackColor = true;
            this.cbSelection.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // rbPoint
            // 
            this.rbPoint.AutoSize = true;
            this.rbPoint.Location = new System.Drawing.Point(128, 6);
            this.rbPoint.Name = "rbPoint";
            this.rbPoint.Size = new System.Drawing.Size(49, 17);
            this.rbPoint.TabIndex = 3;
            this.rbPoint.Text = "Point";
            this.rbPoint.UseVisualStyleBackColor = true;
            this.rbPoint.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // rbfLine
            // 
            this.rbfLine.AutoSize = true;
            this.rbfLine.Checked = true;
            this.rbfLine.Location = new System.Drawing.Point(6, 6);
            this.rbfLine.Name = "rbfLine";
            this.rbfLine.Size = new System.Drawing.Size(65, 17);
            this.rbfLine.TabIndex = 1;
            this.rbfLine.TabStop = true;
            this.rbfLine.Text = "FastLine";
            this.rbfLine.UseVisualStyleBackColor = true;
            this.rbfLine.CheckedChanged += new System.EventHandler(this.RbfLine_CheckedChanged);
            this.rbfLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // rbLine
            // 
            this.rbLine.AutoSize = true;
            this.rbLine.Location = new System.Drawing.Point(77, 6);
            this.rbLine.Name = "rbLine";
            this.rbLine.Size = new System.Drawing.Size(45, 17);
            this.rbLine.TabIndex = 2;
            this.rbLine.Text = "Line";
            this.rbLine.UseVisualStyleBackColor = true;
            this.rbLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTypeSmooth_KeyDown);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(353, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 23);
            this.button1.TabIndex = 30;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // frmOption12065
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 102);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.cbSelection);
            this.Controls.Add(this.rbPoint);
            this.Controls.Add(this.rbfLine);
            this.Controls.Add(this.rbLine);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmOption12065";
            this.Text = "OptionForm";
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.Button BtnColorDraw;
        public System.Windows.Forms.ComboBox cmbStyleDraw;
        public System.Windows.Forms.Label label32;
        public System.Windows.Forms.CheckBox cbDrawLine;
        public System.Windows.Forms.CheckBox cbSelection;
        public System.Windows.Forms.RadioButton rbPoint;
        public System.Windows.Forms.RadioButton rbfLine;
        public System.Windows.Forms.RadioButton rbLine;
        private System.Windows.Forms.ColorDialog colorDialog1;
        public System.Windows.Forms.Button button1;
    }
}