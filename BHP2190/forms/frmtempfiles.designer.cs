namespace BHP2190.forms
{
    partial class frmtempfiles
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("All(saved or processing)");
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmtempfiles));
            this.label4 = new System.Windows.Forms.Label();
            this.treeView3 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.sortFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameascToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.namedescToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateascToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datedescToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tChart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label41 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.toolstripChart = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAll = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbshowdata = new System.Windows.Forms.ToolStripButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rowDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.y1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.y2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.reSize1 = new LarcomAndYoung.Windows.Forms.ReSize(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tChart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.toolstripChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(3, 290);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(268, 297);
            this.label4.TabIndex = 21;
            // 
            // treeView3
            // 
            this.treeView3.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.treeView3.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView3.ForeColor = System.Drawing.Color.Black;
            this.treeView3.Location = new System.Drawing.Point(2, 3);
            this.treeView3.Name = "treeView3";
            treeNode1.Name = "node0";
            treeNode1.Text = "All(saved or processing)";
            this.treeView3.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView3.Size = new System.Drawing.Size(269, 251);
            this.treeView3.TabIndex = 24;
            this.treeView3.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView3_AfterCheck);
            this.treeView3.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView3_AfterSelect);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.sortFilesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(190, 70);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem1.Text = "active multi-selection";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(189, 22);
            this.toolStripMenuItem2.Text = "delete multi_selection";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // sortFilesToolStripMenuItem
            // 
            this.sortFilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameascToolStripMenuItem,
            this.namedescToolStripMenuItem,
            this.dateascToolStripMenuItem,
            this.datedescToolStripMenuItem});
            this.sortFilesToolStripMenuItem.Name = "sortFilesToolStripMenuItem";
            this.sortFilesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.sortFilesToolStripMenuItem.Text = "sort files";
            // 
            // nameascToolStripMenuItem
            // 
            this.nameascToolStripMenuItem.Name = "nameascToolStripMenuItem";
            this.nameascToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.nameascToolStripMenuItem.Text = "name-asc";
            this.nameascToolStripMenuItem.Click += new System.EventHandler(this.nameascToolStripMenuItem_Click);
            // 
            // namedescToolStripMenuItem
            // 
            this.namedescToolStripMenuItem.Name = "namedescToolStripMenuItem";
            this.namedescToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.namedescToolStripMenuItem.Text = "name-desc";
            this.namedescToolStripMenuItem.Click += new System.EventHandler(this.namedescToolStripMenuItem_Click);
            // 
            // dateascToolStripMenuItem
            // 
            this.dateascToolStripMenuItem.Name = "dateascToolStripMenuItem";
            this.dateascToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.dateascToolStripMenuItem.Text = "date-asc";
            this.dateascToolStripMenuItem.Click += new System.EventHandler(this.dateascToolStripMenuItem_Click);
            // 
            // datedescToolStripMenuItem
            // 
            this.datedescToolStripMenuItem.Name = "datedescToolStripMenuItem";
            this.datedescToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.datedescToolStripMenuItem.Text = "date_desc";
            this.datedescToolStripMenuItem.Click += new System.EventHandler(this.datedescToolStripMenuItem_Click);
            // 
            // tChart1
            // 
            this.tChart1.BackColor = System.Drawing.Color.Black;
            this.tChart1.BorderlineColor = System.Drawing.Color.Black;
            chartArea1.BackColor = System.Drawing.Color.Black;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 100F;
            chartArea1.Position.Width = 100F;
            this.tChart1.ChartAreas.Add(chartArea1);
            this.tChart1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tChart1.Location = new System.Drawing.Point(3, 3);
            this.tChart1.Name = "tChart1";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            this.tChart1.Series.Add(series1);
            this.tChart1.Size = new System.Drawing.Size(795, 618);
            this.tChart1.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.tChart1);
            this.panel1.Location = new System.Drawing.Point(290, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(808, 628);
            this.panel1.TabIndex = 26;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.button4);
            this.panel2.Location = new System.Drawing.Point(760, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(38, 35);
            this.panel2.TabIndex = 37;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Control;
            this.button4.BackgroundImage = global::BHP2190.Properties.Resources.color;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button4.Location = new System.Drawing.Point(5, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(30, 30);
            this.button4.TabIndex = 36;
            this.toolTip1.SetToolTip(this.button4, "BackColor");
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.label41);
            this.panel3.Controls.Add(this.treeView3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(5, 108);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(279, 532);
            this.panel3.TabIndex = 28;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.label41.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label41.ForeColor = System.Drawing.Color.Black;
            this.label41.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label41.Location = new System.Drawing.Point(3, 257);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(268, 23);
            this.label41.TabIndex = 25;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.toolstripChart);
            this.panel6.Location = new System.Drawing.Point(7, 19);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(273, 70);
            this.panel6.TabIndex = 29;
            // 
            // toolstripChart
            // 
            this.toolstripChart.AllowDrop = true;
            this.toolstripChart.AutoSize = false;
            this.toolstripChart.BackColor = System.Drawing.Color.Gainsboro;
            this.toolstripChart.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tsbRefresh,
            this.tsbSaveAll,
            this.tsbDelete,
            this.tsbshowdata});
            this.toolstripChart.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolstripChart.Location = new System.Drawing.Point(0, 0);
            this.toolstripChart.Name = "toolstripChart";
            this.toolstripChart.Size = new System.Drawing.Size(269, 65);
            this.toolstripChart.TabIndex = 0;
            this.toolstripChart.TabStop = true;
            this.toolstripChart.Text = "toolStrip4";
            // 
            // tsbClose
            // 
            this.tsbClose.AutoSize = false;
            this.tsbClose.ForeColor = System.Drawing.Color.Black;
            this.tsbClose.Image = global::BHP2190.Properties.Resources.Exit1;
            this.tsbClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Margin = new System.Windows.Forms.Padding(0);
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(50, 50);
            this.tsbClose.Text = "Close";
            this.tsbClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbClose.ToolTipText = "Close";
            this.tsbClose.Click += new System.EventHandler(this.TsbClose_Click);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.AutoSize = false;
            this.tsbRefresh.ForeColor = System.Drawing.Color.Black;
            this.tsbRefresh.Image = global::BHP2190.Properties.Resources.Actions_view_refresh_icon;
            this.tsbRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(50, 50);
            this.tsbRefresh.Text = "Refresh";
            this.tsbRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbRefresh.ToolTipText = "Refresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tsbSaveAll
            // 
            this.tsbSaveAll.AutoSize = false;
            this.tsbSaveAll.ForeColor = System.Drawing.Color.Black;
            this.tsbSaveAll.Image = global::BHP2190.Properties.Resources.Save;
            this.tsbSaveAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAll.Name = "tsbSaveAll";
            this.tsbSaveAll.Size = new System.Drawing.Size(38, 58);
            this.tsbSaveAll.Text = "save";
            this.tsbSaveAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSaveAll.ToolTipText = "Save file in other path";
            this.tsbSaveAll.Click += new System.EventHandler(this.tsbSaveAll_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.AutoSize = false;
            this.tsbDelete.ForeColor = System.Drawing.Color.Black;
            this.tsbDelete.Image = global::BHP2190.Properties.Resources.delete_file_icon;
            this.tsbDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Margin = new System.Windows.Forms.Padding(0);
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(38, 58);
            this.tsbDelete.Text = "Delete";
            this.tsbDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbDelete.ToolTipText = "Delete File";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // tsbshowdata
            // 
            this.tsbshowdata.ForeColor = System.Drawing.Color.Black;
            this.tsbshowdata.Image = global::BHP2190.Properties.Resources.checklist_icon;
            this.tsbshowdata.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbshowdata.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbshowdata.Name = "tsbshowdata";
            this.tsbshowdata.Size = new System.Drawing.Size(66, 62);
            this.tsbshowdata.Text = "Show data";
            this.tsbshowdata.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbshowdata.ToolTipText = "Show data points";
            this.tsbshowdata.Click += new System.EventHandler(this.tsbshowdata_Click);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Location = new System.Drawing.Point(5, 14);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(279, 82);
            this.panel5.TabIndex = 30;
            // 
            // rowDataGridViewTextBoxColumn1
            // 
            this.rowDataGridViewTextBoxColumn1.DataPropertyName = "row";
            this.rowDataGridViewTextBoxColumn1.HeaderText = "row";
            this.rowDataGridViewTextBoxColumn1.Name = "rowDataGridViewTextBoxColumn1";
            // 
            // xDataGridViewTextBoxColumn1
            // 
            this.xDataGridViewTextBoxColumn1.DataPropertyName = "x";
            this.xDataGridViewTextBoxColumn1.HeaderText = "x";
            this.xDataGridViewTextBoxColumn1.Name = "xDataGridViewTextBoxColumn1";
            // 
            // yDataGridViewTextBoxColumn1
            // 
            this.yDataGridViewTextBoxColumn1.DataPropertyName = "y";
            this.yDataGridViewTextBoxColumn1.HeaderText = "y";
            this.yDataGridViewTextBoxColumn1.Name = "yDataGridViewTextBoxColumn1";
            // 
            // y1DataGridViewTextBoxColumn
            // 
            this.y1DataGridViewTextBoxColumn.DataPropertyName = "y1";
            this.y1DataGridViewTextBoxColumn.HeaderText = "y1";
            this.y1DataGridViewTextBoxColumn.Name = "y1DataGridViewTextBoxColumn";
            // 
            // y2DataGridViewTextBoxColumn
            // 
            this.y2DataGridViewTextBoxColumn.DataPropertyName = "y2";
            this.y2DataGridViewTextBoxColumn.HeaderText = "y2";
            this.y2DataGridViewTextBoxColumn.Name = "y2DataGridViewTextBoxColumn";
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // reSize1
            // 
            this.reSize1.About = null;
            this.reSize1.AutoCenterFormOnLoad = false;
            this.reSize1.Enabled = true;
            this.reSize1.HostContainer = this;
            this.reSize1.InitialHostContainerHeight = 700D;
            this.reSize1.InitialHostContainerWidth = 1102D;
            this.reSize1.Tag = null;
            // 
            // frmtempfiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1102, 700);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmtempfiles";
            this.Load += new System.EventHandler(this.GraphForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tChart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.toolstripChart.ResumeLayout(false);
            this.toolstripChart.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TreeView treeView3;
        public System.Windows.Forms.DataVisualization.Charting.Chart tChart1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn xDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn yDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn y1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn y2DataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStrip toolstripChart;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripButton tsbSaveAll;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton tsbshowdata;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem sortFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nameascToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem namedescToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateascToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem datedescToolStripMenuItem;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel2;
        private LarcomAndYoung.Windows.Forms.ReSize reSize1;
        private System.Windows.Forms.ToolStripButton tsbClose;
    }
}