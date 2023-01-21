namespace BHP2190.forms
{
    partial class frmselecttypeshow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmselecttypeshow));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtdnumberx = new System.Windows.Forms.MaskedTextBox();
            this.rdbgeneralx = new System.Windows.Forms.RadioButton();
            this.rdbscientificx = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtdnumbery = new System.Windows.Forms.MaskedTextBox();
            this.rdbgeneraly = new System.Windows.Forms.RadioButton();
            this.rdbscientificy = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::BHP2190.Properties.Resources.OK;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(71, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 33);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::BHP2190.Properties.Resources.Exit;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.Location = new System.Drawing.Point(154, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 33);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtdnumberx);
            this.groupBox1.Controls.Add(this.rdbgeneralx);
            this.groupBox1.Controls.Add(this.rdbscientificx);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 76);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select type for x axis";
            // 
            // txtdnumberx
            // 
            this.txtdnumberx.Location = new System.Drawing.Point(130, 50);
            this.txtdnumberx.Mask = "00";
            this.txtdnumberx.Name = "txtdnumberx";
            this.txtdnumberx.Size = new System.Drawing.Size(57, 20);
            this.txtdnumberx.TabIndex = 26;
            this.txtdnumberx.Text = "2";
            // 
            // rdbgeneralx
            // 
            this.rdbgeneralx.AutoSize = true;
            this.rdbgeneralx.Checked = true;
            this.rdbgeneralx.Location = new System.Drawing.Point(25, 25);
            this.rdbgeneralx.Name = "rdbgeneralx";
            this.rdbgeneralx.Size = new System.Drawing.Size(62, 17);
            this.rdbgeneralx.TabIndex = 25;
            this.rdbgeneralx.TabStop = true;
            this.rdbgeneralx.Text = "General";
            this.rdbgeneralx.UseVisualStyleBackColor = true;
            // 
            // rdbscientificx
            // 
            this.rdbscientificx.AutoSize = true;
            this.rdbscientificx.Location = new System.Drawing.Point(185, 25);
            this.rdbscientificx.Name = "rdbscientificx";
            this.rdbscientificx.Size = new System.Drawing.Size(68, 17);
            this.rdbscientificx.TabIndex = 24;
            this.rdbscientificx.Text = "Scientific";
            this.rdbscientificx.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "decimal place :";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(4, 217);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(282, 42);
            this.panel1.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtdnumbery);
            this.groupBox2.Controls.Add(this.rdbgeneraly);
            this.groupBox2.Controls.Add(this.rdbscientificy);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(16, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 76);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select type for y axis";
            // 
            // txtdnumbery
            // 
            this.txtdnumbery.Location = new System.Drawing.Point(130, 50);
            this.txtdnumbery.Mask = "00";
            this.txtdnumbery.Name = "txtdnumbery";
            this.txtdnumbery.Size = new System.Drawing.Size(57, 20);
            this.txtdnumbery.TabIndex = 26;
            this.txtdnumbery.Text = "2";
            // 
            // rdbgeneraly
            // 
            this.rdbgeneraly.AutoSize = true;
            this.rdbgeneraly.Checked = true;
            this.rdbgeneraly.Location = new System.Drawing.Point(25, 25);
            this.rdbgeneraly.Name = "rdbgeneraly";
            this.rdbgeneraly.Size = new System.Drawing.Size(62, 17);
            this.rdbgeneraly.TabIndex = 25;
            this.rdbgeneraly.TabStop = true;
            this.rdbgeneraly.Text = "General";
            this.rdbgeneraly.UseVisualStyleBackColor = true;
            // 
            // rdbscientificy
            // 
            this.rdbscientificy.AutoSize = true;
            this.rdbscientificy.Location = new System.Drawing.Point(185, 25);
            this.rdbscientificy.Name = "rdbscientificy";
            this.rdbscientificy.Size = new System.Drawing.Size(68, 17);
            this.rdbscientificy.TabIndex = 24;
            this.rdbscientificy.Text = "Scientific";
            this.rdbscientificy.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "decimal place :";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(4, 7);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(52, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "x axis";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(4, 115);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(52, 17);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "y axis";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // frmselecttypeshow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 263);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmselecttypeshow";
            this.Text = "select show type for decimal number";
            this.Load += new System.EventHandler(this.Frmselecttypeshow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtdnumberx;
        private System.Windows.Forms.RadioButton rdbgeneralx;
        private System.Windows.Forms.RadioButton rdbscientificx;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox txtdnumbery;
        private System.Windows.Forms.RadioButton rdbgeneraly;
        private System.Windows.Forms.RadioButton rdbscientificy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}