namespace BHP2190.forms
{
    partial class frmselectfiletype
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmselectfiletype));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbother = new System.Windows.Forms.RadioButton();
            this.rdbxls = new System.Windows.Forms.RadioButton();
            this.rdbtext = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::BHP2190.Properties.Resources.OK;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(33, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 33);
            this.button1.TabIndex = 16;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::BHP2190.Properties.Resources.Exit;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.Location = new System.Drawing.Point(93, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 33);
            this.button2.TabIndex = 17;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbother);
            this.groupBox1.Controls.Add(this.rdbxls);
            this.groupBox1.Controls.Add(this.rdbtext);
            this.groupBox1.Location = new System.Drawing.Point(4, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 97);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "type of file";
            // 
            // rdbother
            // 
            this.rdbother.AutoSize = true;
            this.rdbother.Location = new System.Drawing.Point(17, 72);
            this.rdbother.Name = "rdbother";
            this.rdbother.Size = new System.Drawing.Size(77, 17);
            this.rdbother.TabIndex = 2;
            this.rdbother.Text = "others type";
            this.rdbother.UseVisualStyleBackColor = true;
//            this.rdbother.CheckedChanged += new System.EventHandler(this.Rdbother_CheckedChanged);
            // 
            // rdbxls
            // 
            this.rdbxls.AutoSize = true;
            this.rdbxls.Location = new System.Drawing.Point(17, 45);
            this.rdbxls.Name = "rdbxls";
            this.rdbxls.Size = new System.Drawing.Size(50, 17);
            this.rdbxls.TabIndex = 1;
            this.rdbxls.Text = "excel";
            this.rdbxls.UseVisualStyleBackColor = true;
            // 
            // rdbtext
            // 
            this.rdbtext.AutoSize = true;
            this.rdbtext.Checked = true;
            this.rdbtext.Location = new System.Drawing.Point(17, 19);
            this.rdbtext.Name = "rdbtext";
            this.rdbtext.Size = new System.Drawing.Size(42, 17);
            this.rdbtext.TabIndex = 0;
            this.rdbtext.TabStop = true;
            this.rdbtext.Text = "text";
            this.rdbtext.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(4, 102);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 42);
            this.panel1.TabIndex = 19;
            // 
            // frmselectfiletype
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 146);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmselectfiletype";
            this.Text = "select type of file for exporting";
//            this.Load += new System.EventHandler(this.Frmselectfiletype_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbother;
        private System.Windows.Forms.RadioButton rdbxls;
        private System.Windows.Forms.RadioButton rdbtext;
        private System.Windows.Forms.Panel panel1;
    }
}