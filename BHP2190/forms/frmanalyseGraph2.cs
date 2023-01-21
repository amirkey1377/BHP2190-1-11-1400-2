﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows;
using BHP2190.classes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Threading;

namespace BHP2190.forms
{
    public partial class frmanalyseGraph2 : Form
    {
        [DllImport("User32.dll", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        private static extern IntPtr LoadCursorFromFile(String str);

        public static System.Windows.Forms.Cursor LoadCursorFromResource(System.Drawing.Icon icono)  // Assuming that the resource is an Icon, but also could be a Image or a Bitmap
        {
            // Saving cursor icon in temp file, necessary for loading through Win API
            string fileName1 = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".cur";
            using (var fileStream = File.Open(fileName1, FileMode.Create))
            {
                icono.Save(fileStream);
            }

            // Loading cursor from temp file, using Win API
            System.Windows.Forms.Cursor result = new System.Windows.Forms.Cursor(LoadCursorFromFile(fileName1));

            // Deleting temp file
            File.Delete(fileName1);

            return result;
        }
        public frmanalyseGraph2()
        {
            InitializeComponent();
          
            this.chart4.Titles.Clear();
            Title header1 = this.chart4.Titles.Add("Header");
            header1.Alignment = ContentAlignment.TopLeft;
            header1.Docking = Docking.Top;
            this.chart4.Titles[0].Text = "";
            this.chart4.Titles[0].Alignment = ContentAlignment.TopCenter;
            this.chart4.Titles[0].ForeColor = Color.Black;
           // chart3.ChartAreas.Add("a1");
            //chart3.Legends.Add("a1");
            chart3.ChartAreas[0].Area3DStyle.Enable3D = false;

            chart3.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
            chart3.ChartAreas[0].AxisX.LineColor = Color.Black;
            chart3.ChartAreas[0].AxisY.LineColor = Color.Black;

            chart3.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
            //chart3.ChartAreas[0].AxisY.Title = "coulomb(c)";
            //chart3.ChartAreas[0].AxisX.Title = "Time(s)";
            chart3.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
            chart3.ChartAreas[0].AxisY.TitleForeColor = Color.Black;

            chart3.ChartAreas[0].AxisY.LabelStyle.Format = "#.#e-0";
            chart3.ChartAreas[0].AxisX.LabelStyle.Format = "#0.##";
            this.chart3.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            this.chart3.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            this.chart3.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            this.chart3.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            this.chart3.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Black;
            this.chart3.ChartAreas[0].AxisX.MinorGrid.LineColor = Color.Black;
            this.chart3.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Black;
            this.chart3.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.Black;
            chart3.ChartAreas[0].AxisX.ScrollBar.Enabled = false;
            chart3.ChartAreas[0].AxisY.ScrollBar.Enabled = false;

            chart3.ChartAreas[0].AxisX.ScrollBar.BackColor = Color.Black;
            chart3.ChartAreas[0].AxisY.ScrollBar.BackColor = Color.Black;
         //   chart4.ChartAreas.Add("b1");
           // chart4.Legends.Add("b1");
            chart4.ChartAreas[0].Area3DStyle.Enable3D = false;

            chart4.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
            chart4.ChartAreas[0].AxisX.LineColor = Color.Black;
            chart4.ChartAreas[0].AxisY.LineColor = Color.Black;

            chart4.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
            //chart4.ChartAreas[0].AxisY.Title = "coulomb(c)";
            //chart4.ChartAreas[0].AxisX.Title = "Time(s)";
            chart4.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
            chart4.ChartAreas[0].AxisY.TitleForeColor = Color.Black;

            chart4.ChartAreas[0].AxisY.LabelStyle.Format = "#.#e-0"; 
            chart4.ChartAreas[0].AxisX.LabelStyle.Format = "#0.##";
            this.chart4.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            this.chart4.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            this.chart4.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            this.chart4.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            this.chart4.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Black;
            this.chart4.ChartAreas[0].AxisX.MinorGrid.LineColor = Color.Black;
            this.chart4.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Black;
            this.chart4.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.Black;
            chart4.ChartAreas[0].AxisX.ScrollBar.BackColor = Color.Black;
            chart4.ChartAreas[0].AxisY.ScrollBar.BackColor = Color.Black;
            chart4.ChartAreas[0].AxisX.ScrollBar.Enabled = false;
            chart4.ChartAreas[0].AxisY.ScrollBar.Enabled = false;
        }
        private System.Drawing.Rectangle rectsel;
        bool smooth_state = false;
        public string operation = "";
        public  System.Windows.Forms.DataVisualization.Charting.Chart chart ;
        public drawline_chart drawline1;
        private string statebutton = "",statebutton_1="";
        public int num_Series = 0;
        double[] othertech_val0 = new double[200];
        double[] othertech_val1 = new double[200];       
        private int shomar_val = 0;
        public int[] Overlay_tech = new int[1000];
        public byte tech = 0;
        public int grfNo = 0, nindexchart2 = 0;
 //       public frmselecttech sf;
        public frm_main mainf = new frm_main();
        public frmOption opForm = new frmOption();
        public clasMain mc = new clasMain();
        public clasPeak cPeak = new clasPeak();
        public frmShowPeaks fPeak = new frmShowPeaks();
        public double ContactpointX = 0, contactpointY = 0;
        public bool isSelectedLine = false;
        int  startpoint_selx = -1, startpoint_sely = -1, endpoint_selx = -1, endpoint_sely = -1;
      
        KeyEventArgs ee = new KeyEventArgs(Keys.Alt);
        private void GraphForm_Load(object sender, EventArgs e)
        {
            try { 
            drawline1 = new drawline_chart();
           
            if (opForm.cbDrawLine.Checked)
                Set_line_Setting();
            else
                drawline1.Active = false;


                string[] color = File.ReadAllLines(System.Windows.Forms.Application.StartupPath + "\\Color.dat");
                string s = color[1].ToString().Substring(color[1].ToString().IndexOf(":") + 1);
                Color cl = Color.FromArgb(Convert.ToInt32(s));
                chart3.BackColor = cl;
                chart3.BorderlineColor = cl;
                chart3.ChartAreas[0].BackColor = cl;
                chart4.BackColor = cl;
                chart4.BorderlineColor = cl;
                chart4.ChartAreas[0].BackColor = cl;
                panel1.BackColor = cl;
                panel2.BackColor = cl;
                panel8.BackColor = cl;


                s = color[7].ToString().Substring(color[7].ToString().IndexOf(":") + 1);
                cl = Color.FromArgb(Convert.ToInt32(s));
                for (int i = 0; i < chart3.Series.Count; i++)
                    chart3.Series[i].Color = cl;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

      
        public void Set_line_Setting()
        {
            drawline1.EnableDraw = opForm.cbDrawLine.Checked;
            drawline1.pen_ch.Color = mainf.drawColor;
            switch (Convert.ToInt16(opForm.cmbStyleDraw.SelectedIndex))
            {
                case 0: drawline1.pen_ch.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid; break;
                case 1: drawline1.pen_ch.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash; break;
                case 2: drawline1.pen_ch.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot; break;
                case 3: drawline1.pen_ch.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot; break;
                case 4: drawline1.pen_ch.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot; break;
                default: break;
            }
            drawline1.Active = true;
        }

        public void DrawLineSelectLine(drawline_chart sender)
        {
            try
            {
                if (isSelectedLine)
                {
                   
                    if (mainf.Detect_Peak && fPeak.rbManualPeak.Checked)
                    {
                        double Step = Math.Abs(chart3.Series[0].Points[1].XValue - chart3.Series[0].Points[1].XValue);
                        int X1 = sender.FromPoint.X;
                        int Y1 = sender.FromPoint.Y;
                        int X2 = sender.ToPoint.X;
                        int Y2 = sender.ToPoint.Y;
                        var results1 = chart3.HitTest(X1, Y1, false, ChartElementType.PlottingArea);
                        double X_V1=0, Y_V1=0, X_V2=0, Y_V2=0;
                        foreach (var result in results1)
                        {
                            if (result.ChartElementType == ChartElementType.PlottingArea)
                            {
                                X_V1 = result.ChartArea.AxisX.PixelPositionToValue(X1);
                                Y_V1 = result.ChartArea.AxisY.PixelPositionToValue(Y1);
                                
                            }
                        }
                        var results2 = chart3.HitTest(X2, Y2, false, ChartElementType.PlottingArea);
                        foreach (var result in results2)
                        {
                            if (result.ChartElementType == ChartElementType.PlottingArea)
                            {
                                X_V2 = result.ChartArea.AxisX.PixelPositionToValue(X2);
                                Y_V2 = result.ChartArea.AxisY.PixelPositionToValue(Y2);

                            }
                        }
                        double P1 = X_V1;
                        double P2 = X_V2;
                        double[] XX = new double[(int)(Math.Abs(P2 - P1) / Step)];
                        double[] YY = new double[(int)(Math.Abs(P2 - P1) / Step)];

                        if (X_V2 < X_V1)
                        {
                            P2 = X_V1;
                            P1 = X_V2;
                        }
                        int F = Math.Abs((int)(P1 / Step));
                        for (int i = 0; i < (int)(Math.Abs(P2 - P1) / Step); i++)
                        {
                            XX[i] = Convert.ToDouble(chart3.Series[0].Points[F + i].XValue.ToString());
                            YY[i] = Convert.ToDouble(chart3.Series[0].Points[F + i].YValues[0].ToString());
                        }
                        try
                        {
                            
                            //fPeak.richTextBox1.Text += cPeak.manual(chart3.Series[0].,XValues.Value, chart3.Series[0].YValues.Value, X_V1, Y_V1, X_V2, Y_V2, (int)(Math.Abs(P2 - P1) / Step));
                            //fPeak.richTextBox1.Text += cPeak.findp1(XX, YY, (int)((P2 - P1) / Step));
                        }
                        catch
                        {
                        }
                    }
                }
                isSelectedLine = true;
            }
            catch { }
        }

     

        private void GraphForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        //    e.Cancel = (mainf.grfNum == 0);
        //    if (mainf.grfNum > 0)
        //        mainf.grfNum--;
        }
      

      

        private void tChart1_KeyUp(object sender, KeyEventArgs e)
        {
            drawline1.EnableDraw = false;
        }

        private void GraphForm_Enter(object sender, EventArgs e)
        {
        //    if (mainf != null)
        //        mainf.grf = (sender as frmanalyseGraph2);
        }

        private void tChart1_PostPaint(object sender, ChartPaintEventArgs e)
        {
            Chart1_AfterDraw(sender);  
        }

        private void Chart1_AfterDraw(object sender)
        {
        //    mainf.Enable_Button(chart3.Series.Count);     
        }
           
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        //    opForm.mf = this.mainf;
        //    opForm.gf = this;
        //    opForm.ShowDialog();
        }
        
        private void clearPlotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearselplot();
        }
        
        public void clearform()
        {
            for (int i = treeView3.Nodes[0].Nodes.Count - 1; i >= 0; i--)
                treeView3.Nodes[0].Nodes[i].Remove();
            label4.Text = "";
            label41.Text = "";
            for (int i = chart3.Series.Count - 1; i >= 0; i--)
                chart3.Series.RemoveAt(i);
            chart3.Refresh();
            dataSet12.analyselist.Clear();          
            chart4.Titles[0].Text = "";
            chart4.Series.Clear();
            drawline1.removeall();
            drawline1.removeall_master();
        }

        private void treeView3_AfterCheck(object sender, TreeViewEventArgs e)
        {
            
            if (e.Node.Level > 0)
            {
                for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                {
                    if (treeView3.Nodes[0].Nodes[i].Checked)
                    {
                        if (i >= chart3.Series.Count)
                            chart3.Series.Add("ser" + i + 1);
                        chart3.Series[i].Enabled = true;
                        //  chart3.Titles[0].Text = classes.clasglobal.TechName[Overlay_tech[i]];
                        // chart3.Series[i].Title = chart3.Header.Text;
                        chart3.ChartAreas[0].AxisY.LabelStyle.Angle = 0;
                        chart3.ChartAreas[0].AxisY.Title = classglobal.VAxisTitle[Overlay_tech[i]];
                        chart3.ChartAreas[0].AxisX.Title = classglobal.HAxisTitle[Overlay_tech[i]];
                        chart3.ChartAreas[0].RecalculateAxesScale();
                    }
                    else
                    {
                        chart3.Series[i].Enabled = false;
                        chart3.ChartAreas[0].RecalculateAxesScale();
                    }
                    difxview = chart3.ChartAreas[0].AxisX.ScaleView.ViewMaximum - chart3.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    difyview = chart3.ChartAreas[0].AxisY.ScaleView.ViewMaximum - chart3.ChartAreas[0].AxisY.ScaleView.ViewMinimum;


                }
                //if (tcbdimy.Enabled)
                //{
                //    if (tcbdimy.Text.Trim() == "" || tcbdimy.Text.Trim() == "0")
                //        tcbdimy.Text = "1";
                //}
                //else
                //    tcbdimy.Text = "1";
                //changeaxxy_dimension(double.Parse( tcbdimy.Text.Trim()));

            }
            else if (e.Node.Level == 0) {
                if (treeView3.Nodes[0].Checked)
                {
                    for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                        treeView3.Nodes[0].Nodes[i].Checked = true;                     
                }
                else 
                {
                    for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                        treeView3.Nodes[0].Nodes[i].Checked = false;
                }
            
            }
            //chart3.Refresh();
        }

        private void treeView3_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level > 0)
            {
                if (e.Node.IsSelected)
                {
                    label4.Text=fillparam(1,e.Node.Index);
                   // fillparamdata(e.Node.Index);

                }

            }

        }
        public void opengraph()
        {
            bool overlaymain = true;
            if (treeView3.Nodes[0].Nodes.Count > 0)
            {
                switch (MessageBox.Show("Other graphs exist in this chart.Do you want to show new graph there?\n----------------------------------\nyes : show all graphs in this chart\nno : remove other graphs in this chart and show this graph only\ncancle : dont show this graph\n--------------------------------", "question", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        clearform();
                        break;
                    case DialogResult.Cancel:
                        return;
                      
                }
            }
           
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DereferenceLinks = false;
            ofd.AutoUpgradeEnabled = false;
            ofd.Multiselect = true;
            ofd.DefaultExt = "txt";

            ofd.Filter = "Bhpd Files(*.Bhpd);All Files(*.*)|*.Bhpd;*.*";
//            ofd.Filter = "Text Files(*.Txt);Data Files(*.Dat)|*.txt;*.dat";
            ofd.ShowDialog();
            bool beOverlay = overlaymain;
            for (int i = 0; i <= ofd.FileNames.GetUpperBound(0); i++)
                if (ofd.FileNames.GetValue(i).ToString() != "")
                {
                    overlaymain = true;
                    int n = dataSet12.analyselist.Rows.Count;

                    cycle = 1;
                    line_file = -1;//if cycle>1 get number line for continue read file

                    string fName = ofd.FileNames.GetValue(i).ToString();//address and filename
                    int k = fName.Length - 1;
                    // distinct file names from file path
                    while (k > 0)
                        if (fName[k] == '\\')
                        {
                            fName = fName.Substring(k + 1);  // mn_CV.dat
                            break;
                        }
                        else
                            k--;
                                       
                    string fName_2 = fName;
                    fName = "";
                    int ok = 0;
                    for (int g = 0; g < fName_2.Length; g++)
                    {
                        if (ok == 1)
                            if (fName_2[g].ToString() == "_" || fName_2[g].ToString() == "-" || fName_2[g].ToString() == ".")
                                break;
                            else
                                fName = fName + fName_2[g].ToString();  //mnfile
                            
                        
                        if (fName_2[g].ToString() == "_") ok = 1;
                    }
                    if (ok == 0) fName = fName_2.Substring(0, fName_2.Length - 4);    // اگر فرمت نام فایل غیر از تعریف نرم افزار باشد
                                      
                    for (int ii = 0; ii < cycle; ii++)
                    {
                        restore_Data(ofd.FileNames.GetValue(i).ToString(), overlaymain);
                        fillparamdata(n);
                        n++;

                        if (cycle > 1)
                            treeView3.Nodes[0].Nodes.Add(fName + "-" + (ii + 1));  // CV-1.dat
                       else
                            treeView3.Nodes[0].Nodes.Add(fName);  // CV.dat
                    }

                    n--;
                                      
                    if (dataSet12.analyselist.Rows.Count == n) {
                        MessageBox.Show("FILE FORMAT IS NOT CORRECT" + Environment.NewLine + "Please try again");
                        return;
                    }   

                    cycle = 1;
                                       
                    Text = ofd.FileNames.GetValue(i).ToString();

                }

            try
            {
                treeView3.SelectedNode = treeView3.Nodes[0].LastNode;
                treeView3.Nodes[0].LastNode.Checked = true;
                //treeView3.Nodes[0].LastNode.ForeColor = chart3.Series[chart3.Series.Count - 1].Color;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            overlaymain = beOverlay;
            Chart2 = chart3;
            treeView3.ExpandAll();
            //grf.tech = 0;


        }

        Int16 cycle = 1,line_file=-1;
        private void restore_Data(string fileName, bool overlaymain)
        {
            double[] xTmp = { 0 };
            double[] yTmp = { 0 };
            string s = "", fs = "", comment100 = "";
            bool isData = false;
            int num_ser = 0, num_data = 0;
            //int row = 0;
            if (fileName != "")
                fs = fileName;
            StreamReader Fl;
            int tch = 0; double CR = 9999, E1 = 9999, E2 = 9999, E3 = 9999, HS = 9999, QT = 9999, SRF = 9999, SRR = 9999;
            double TS = 9999, PW = 9999, PH = 9999, FR = 9999, CY=1 /*CY = 9999*/, HT = 9999, T1 = 9999, T2 = 9999, I1 = 9999, I2 = 9999;
            double AA = 9999, Q1 = 9999;
            EventArgs e = new EventArgs();
            if (fs != "")
            {
                Fl = new StreamReader(fs);
                if (overlaymain)
                    num_data = chart3.Series.Count;
                try
                {
                    Int32 line = -1;
                    
                    while (!Fl.EndOfStream)
                    {
                        s = Fl.ReadLine();
                       
                        line_file++;

                        line++;

                        if (s.ToUpper().IndexOf("NEW SERIES") >= 0)
                        {
                            overlaymain = true;
                            show_Graph(num_data + num_ser, this, "MainChart", xTmp, yTmp, overlaymain, xTmp.Length);
                            chart3.Series[num_data + num_ser].Points.Clear();
                            num_ser++;
                            s = Fl.ReadLine();
                            //row = 0;
                        }

                        if (!isData)
                        {
                            if (s.IndexOf("Cycles") >= 0)
                            {
                                if (s.IndexOf(":") >= 0)
                                {
                                    string c = s.Substring(s.IndexOf(":") + 1);
                                    cycle = Int16.Parse(c);
                                }
                            }

                            if (s.IndexOf("Tech") >= 0)
                            {
                                if (s.IndexOf(':') >= 0)
                                {
                                    for (int i = 0; i < 13; i++)
                                        if (classglobal.TechName[i] == s.Substring(s.IndexOf(':') + 1).Trim())
                                        {
                                            tch = i;
                                            if (classglobal.TechName[i].ToLower() == "cha" || classglobal.TechName[i].ToLower() == "chp" || classglobal.TechName[i].ToLower() == "chc")
                                                smooth_state = false;
                                            else
                                                smooth_state = true;
                                            break;
                                        }
                                }

                                tech = (byte)restore_One_Data(s, "Tech");
                                show_Graph(num_data + num_ser, this, "MainChart", xTmp, yTmp, overlaymain, xTmp.Length);
                                chart3.Series[num_data + num_ser].Points.Clear();

                            }
                            else if (s.IndexOf("Current Range") >= 0) CR = restore_One_Data(s, "Current Range");
                            else if (s.IndexOf("E1") >= 0) E1 = restore_One_Data(s, "E1");
                            else if (s.IndexOf("E2") >= 0) E2 = restore_One_Data(s, "E2");
                            else if (s.IndexOf("E3") >= 0) E3 = restore_One_Data(s, "E3");
                            else if (s.IndexOf("HStep") >= 0) HS = restore_One_Data(s, "HStep");
                            else if (s.IndexOf("Equilibrium Time") >= 0) QT = restore_One_Data(s, "Equilibrium Time");
                            else if (s.IndexOf("Hold Time") >= 0) HT = restore_One_Data(s, "Hold Time");
                            else if (s.IndexOf("Scan Rate") >= 0) SRF = restore_One_Data(s, "Scan Rate");
                            else if (s.IndexOf("ScanRate_R") >= 0) SRR = restore_One_Data(s, "ScanRate_R");
                            else if (s.IndexOf("TStep") >= 0) TS = restore_One_Data(s, "TStep");
                            else if (s.IndexOf("Pulse Width") >= 0) PW = restore_One_Data(s, "Pulse Width");
                            else if (s.IndexOf("pulse ") >= 0) PH = restore_One_Data(s, "Hpuls");
                            else if (s.IndexOf("Frequency") >= 0) FR = restore_One_Data(s, "Frequency");
                            else if (s.IndexOf("Cycle") >= 0) CY = restore_One_Data(s, "Cycle");
                            else if (s.IndexOf("T1") >= 0) T1 = restore_One_Data(s, "T1");
                            else if (s.IndexOf("T2") >= 0) T2 = restore_One_Data(s, "T2");
                            else if (s.IndexOf("I1") >= 0) I1 = restore_One_Data(s, "I1");
                            else if (s.IndexOf("I2") >= 0) I2 = restore_One_Data(s, "I2");
                            else if (s.IndexOf("comment") >= 0)
                                comment100 = s.Substring(s.IndexOf(":") + 1);

                            if (s.LastIndexOf("Potential") >= 0 || s.LastIndexOf("========") >= 0)
                            {
                                fillgraphlist(tch, CR, E1, E2, E3, HS, QT, SRF, SRR, TS, PW, PH, FR, CY, HT, T1, T2, I1, I2, AA, Q1, fileName, comment100);

                                isData = true;
                                shomar_val = 0;
                                Array.Clear(othertech_val0, 0, othertech_val0.Length);
                                Array.Clear(othertech_val1, 0, othertech_val1.Length);

                                if (line_file > line)
                                    line_file -= 17;//this is not cycle 1 then less lines header from all lines

                                //after insert header read go to another section file
                                while (line < line_file) { s = Fl.ReadLine(); line++; }
                            }

                        }
                        else                // if (isData)
                        {
                            if (s.Length == 0) { continue; }//newline
                                                            //if (line_file > 230) MessageBox.Show(s);
                            if (s.LastIndexOf("========") >= 0)
                            {

                                //===
                                line_file++; //cycle number
                                line_file++; //new line ""
                                line_file++; // data

                                Fl.Close();

                                break;
                            }

                            int l = 0;
                            string sss = "";
                            int num = 0;
                            double x = 0, y = 0, z = 0, w = 0;
                            while (l < s.Length)
                            {

                                if (s[l] != '\t' && s[l] != ';' && s[l] != ' ')
                                    sss = sss + s[l];

                                if ((s[l] == '\t' || s[l] == ';' || s[l] == ' ') && sss != "")
                                {
                                    while (l < s.Length && s[l] == ' ') l++;

                                    try { if (num == 0) x = Convert.ToDouble(sss); } catch { x = 0; }  //this.grf.dataE[num_data + num_ser, row] //890220
                                    try { if (num == 1) y = Convert.ToDouble(sss); } catch { y = 0; }//this.grf.dataI1[num_data + num_ser, row] = Convert.ToDouble(sss);
                                    try { if (num == 2) z = Convert.ToDouble(sss); } catch { z = 0; } //this.grf.dataI2[num_data + num_ser, row] = Convert.ToDouble(sss);
                                    try { if (num == 3) w = Convert.ToDouble(sss); } catch { w = 0; } //this.grf.dataI2[num_data + num_ser, row] = Convert.ToDouble(sss);

                                    sss = "";
                                    num++;
                                }
                                l++;
                            }

                            try { if (num == 0) x = Convert.ToDouble(sss); } catch { x = 0; }
                            try { if (num == 1) y = Convert.ToDouble(sss); } catch { y = 0; }
                            try { if (num == 2) z = Convert.ToDouble(sss); } catch { z = 0; }
                            try { if (num == 3) w = Convert.ToDouble(sss); } catch { w = 0; }

                            if (shomar_val >= othertech_val0.Length)
                            {
                                Array.Resize(ref othertech_val0, othertech_val0.Length + 10);
                                Array.Resize(ref othertech_val1, othertech_val1.Length + 10);
                            }
                            othertech_val0[shomar_val] = x;
                            othertech_val1[shomar_val] = y;
                            shomar_val++;
                            //row++;

                            //if (row >= 10000) row = 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + "selected File is not in valid Format...", "Warning");
                }

                Fl.Close();

                int k = fileName.Length - 1;
                while (k > 0)
                    if (fileName[k] == '\\')
                    {
                        fileName = fileName.Substring(k + 1);
                        break;
                    }
                    else
                        k--;
                //chart3.Series[num_data + num_ser].Title [0]= fileName;

            }

        }
        private void fillgraphlist(int tch, double CR, double E1, double E2, double E3, double HS, double QT, double SRF, double SRR,
           double TS, double PW, double PH, double FR, double CY, double HT, double T1, double T2, double I1, double I2, double AA, double Q1,string strpath,string comment100)
        {

            DataRow dr = dataSet12.analyselist.NewRow();
            dr[0] = tch;
            dr[1] = E1;
            dr[2] = E2;
            dr[3] = E3;
            dr[4] = HS;
            dr[5] = QT;
            dr[6] = SRF;
            dr[7] = TS;
            dr[8] = PW;
            dr[9] = PH;
            dr[10] = FR;
            dr[11] = CY;
            dr[12] = HT;
            dr[13] = T1;
            dr[14] = T2;
            dr[15] = I1;
            dr[16] = I2;
            dr[17] = CR;
            dr[18] = 1;
            dr[21] = strpath;
            dr[22] = comment100;
            dataSet12.analyselist.Rows.Add(dr);

        }
        private string fillparam(int typecall ,int row)
        {
             string str_fillparam = "";
            try
            {
               
                if (typecall == 1)
                {
                    str_fillparam = "Properties :" + Environment.NewLine;
                    label41.Text = "";
                    label41.Text = "Technique name :" + classglobal.TechName[int.Parse(dataSet12.analyselist.Rows[row][0].ToString())];
                }
                else {
                    str_fillparam = "Technique name :" + classglobal.TechName[int.Parse(dataSet12.analyselist.Rows[row][0].ToString())] + "\n\n"+"Properties :" +"\n";
                }
                switch (byte.Parse(dataSet12.analyselist.Rows[row][0].ToString()))
                {
                    case classglobal.DCV:


                        if (double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][4].ToString()) != 9999) str_fillparam = str_fillparam + "HStep =" + double.Parse(dataSet12.analyselist.Rows[row][4].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][6].ToString()) != 9999) str_fillparam = str_fillparam + "Scan Rate =" + double.Parse(dataSet12.analyselist.Rows[row][6].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][7].ToString()) != 9999) str_fillparam = str_fillparam + "TStep =" + double.Parse(dataSet12.analyselist.Rows[row][7].ToString()) + "\n";
                        break;
                    case classglobal.NPV:
                        if (double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][4].ToString()) != 9999) str_fillparam = str_fillparam + "HStep =" + double.Parse(dataSet12.analyselist.Rows[row][4].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][8].ToString()) != 9999) str_fillparam = str_fillparam + "Pulse Width =" + double.Parse(dataSet12.analyselist.Rows[row][8].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][6].ToString()) != 9999) str_fillparam = str_fillparam + "Scan Rate =" + double.Parse(dataSet12.analyselist.Rows[row][6].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][7].ToString()) != 9999) str_fillparam = str_fillparam + "TStep =" + double.Parse(dataSet12.analyselist.Rows[row][7].ToString()) + "\n";
                        break;
                    case classglobal.DPV:
                        if (double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][4].ToString()) != 9999) str_fillparam = str_fillparam + "HStep =" + double.Parse(dataSet12.analyselist.Rows[row][4].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][8].ToString()) != 9999) str_fillparam = str_fillparam + "Pulse Width =" + double.Parse(dataSet12.analyselist.Rows[row][8].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][9].ToString()) != 9999) str_fillparam = str_fillparam + "Pulse Height =" + double.Parse(dataSet12.analyselist.Rows[row][9].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][6].ToString()) != 9999) str_fillparam = str_fillparam + "Scan Rate =" + double.Parse(dataSet12.analyselist.Rows[row][6].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][7].ToString()) != 9999) str_fillparam = str_fillparam + "TStep =" + double.Parse(dataSet12.analyselist.Rows[row][7].ToString()) + "\n";
                        break;
                    case classglobal.SWV:
                        if (double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][4].ToString()) != 9999) str_fillparam = str_fillparam + "HStep =" + double.Parse(dataSet12.analyselist.Rows[row][4].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][9].ToString()) != 9999) str_fillparam = str_fillparam + "Pulse Height =" + double.Parse(dataSet12.analyselist.Rows[row][9].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][6].ToString()) != 9999) str_fillparam = str_fillparam + "Scan Rate =" + double.Parse(dataSet12.analyselist.Rows[row][6].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][10].ToString()) != 9999) str_fillparam = str_fillparam + "Frequency =" + double.Parse(dataSet12.analyselist.Rows[row][10].ToString()) + "\n";
                        break;
                    case classglobal.CV:
                        if (double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][3].ToString()) != 9999) str_fillparam = str_fillparam + "E3 =" + double.Parse(dataSet12.analyselist.Rows[row][3].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][4].ToString()) != 9999) str_fillparam = str_fillparam + "HStep =" + double.Parse(dataSet12.analyselist.Rows[row][4].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][11].ToString()) != 9999) str_fillparam = str_fillparam + "Cycles =" + double.Parse(dataSet12.analyselist.Rows[row][11].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][6].ToString()) != 9999) str_fillparam = str_fillparam + "Scan Rate =" + double.Parse(dataSet12.analyselist.Rows[row][6].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][7].ToString()) != 9999) str_fillparam = str_fillparam + "TStep =" + double.Parse(dataSet12.analyselist.Rows[row][7].ToString()) + "\n";
                        break;
                    case classglobal.LSV:
                        if (double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][4].ToString()) != 9999) str_fillparam = str_fillparam + "HStep =" + double.Parse(dataSet12.analyselist.Rows[row][4].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][6].ToString()) != 9999) str_fillparam = str_fillparam + "Scan Rate =" + double.Parse(dataSet12.analyselist.Rows[row][6].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][7].ToString()) != 9999) str_fillparam = str_fillparam + "TStep =" + double.Parse(dataSet12.analyselist.Rows[row][7].ToString()) + "\n";
                        break;
                    case classglobal.DCS:
                        if (double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][4].ToString()) != 9999) str_fillparam = str_fillparam + "HStep =" + double.Parse(dataSet12.analyselist.Rows[row][4].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][6].ToString()) != 9999) str_fillparam = str_fillparam + "Scan Rate =" + double.Parse(dataSet12.analyselist.Rows[row][6].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][7].ToString()) != 9999) str_fillparam = str_fillparam + "TStep =" + double.Parse(dataSet12.analyselist.Rows[row][7].ToString()) + "\n";

                        break;
                    case classglobal.DPS:
                        if (double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][4].ToString()) != 9999) str_fillparam = str_fillparam + "HStep =" + double.Parse(dataSet12.analyselist.Rows[row][4].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][9].ToString()) != 9999) str_fillparam = str_fillparam + "Pulse Height =" + double.Parse(dataSet12.analyselist.Rows[row][9].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][8].ToString()) != 9999) str_fillparam = str_fillparam + "Pulse Width =" + double.Parse(dataSet12.analyselist.Rows[row][8].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][6].ToString()) != 9999) str_fillparam = str_fillparam + "Scan Rate =" + double.Parse(dataSet12.analyselist.Rows[row][6].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][7].ToString()) != 9999) str_fillparam = str_fillparam + "TStep =" + double.Parse(dataSet12.analyselist.Rows[row][7].ToString()) + "\n";

                        break;
                    case classglobal.CPC:
                        if (double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][13].ToString()) != 9999) str_fillparam = str_fillparam + "T1 =" + double.Parse(dataSet12.analyselist.Rows[row][13].ToString())+ "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) + "\n";
                        //if (double.Parse(dschart2.analyselist.Rows[row][11].ToString()) != 9999) //str_fillparam = str_fillparam + "Cycles =" + double.Parse(dschart2.analyselist.Rows[row][11].ToString()) + "\n";
                        break;
                //    case classglobal.CCC:
                //        if (double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dschart2.analyselist.Rows[row][1].ToString()) + "\n";
                //        if (double.Parse(dataSet12.analyselist.Rows[row][13].ToString()) != 9999) str_fillparam = str_fillparam + "T1 =" + double.Parse(dschart2.analyselist.Rows[row][13].ToString()) * 1000 + "\n";
                //        if (double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dschart2.analyselist.Rows[row][5].ToString()) + "\n";
                        //if (double.Parse(dschart2.analyselist.Rows[row][11].ToString()) != 9999) //str_fillparam = str_fillparam + "Cycles =" + double.Parse(dschart2.analyselist.Rows[row][11].ToString()) + "\n";
                //        break;
                    case classglobal.CHP:
                        if (double.Parse(dataSet12.analyselist.Rows[row][15].ToString()) != 9999) str_fillparam = str_fillparam + "I1 =" + double.Parse(dataSet12.analyselist.Rows[row][15].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][16].ToString()) != 9999) str_fillparam = str_fillparam + "I2 =" + double.Parse(dataSet12.analyselist.Rows[row][16].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][13].ToString()) != 9999) str_fillparam = str_fillparam + "T1 =" + double.Parse(dataSet12.analyselist.Rows[row][13].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][14].ToString()) != 9999) str_fillparam = str_fillparam + "T2 =" + double.Parse(dataSet12.analyselist.Rows[row][14].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) + "\n";
                        //if (double.Parse(dschart2.analyselist.Rows[row][11].ToString()) != 9999) str_fillparam = str_fillparam + "Cycles =" + double.Parse(dschart2.analyselist.Rows[row][11].ToString()) + "\n";
                        break;
                    case classglobal.CHA:
                        if (double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][13].ToString()) != 9999) str_fillparam = str_fillparam + "T1 =" + double.Parse(dataSet12.analyselist.Rows[row][13].ToString())  + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][14].ToString()) != 9999) str_fillparam = str_fillparam + "T2 =" + double.Parse(dataSet12.analyselist.Rows[row][14].ToString())  + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) + "\n";
                        //if (double.Parse(dschart2.analyselist.Rows[row][11].ToString()) != 9999) str_fillparam = str_fillparam + "Cycles =" + double.Parse(dschart2.analyselist.Rows[row][11].ToString()) + "\n";
                        break;
                    case classglobal.CHC:
                        if (double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet12.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet12.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][13].ToString()) != 9999) str_fillparam = str_fillparam + "T1 =" + double.Parse(dataSet12.analyselist.Rows[row][13].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][14].ToString()) != 9999) str_fillparam = str_fillparam + "T2 =" + double.Parse(dataSet12.analyselist.Rows[row][14].ToString()) + "\n";
                        if (double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet12.analyselist.Rows[row][5].ToString()) + "\n";
                        //if (double.Parse(dschart2.analyselist.Rows[row][11].ToString()) != 9999) str_fillparam = str_fillparam + "Cycles =" + double.Parse(dschart2.analyselist.Rows[row][11].ToString()) + "\n";
                        break;

                }
                str_fillparam = str_fillparam + "comment =" + dataSet12.analyselist.Rows[row]["comment"].ToString();
                //pGrid.Refresh();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return str_fillparam;
        }
        private void fillparamdata(int row)
        {
            smootht4(row, chart3.Series[row].Color, 1, 2);
            chart3.Refresh();
        }
         private double restore_One_Data(string str, string prmName)
        {
            if (prmName == "Tech")
            {
                string strTch = "";
                byte Tch = 0;
                strTch = str.Substring(str.IndexOf(':') + 1).Trim();
                switch (strTch)
                {
                    case "DCV": Tch = classglobal.DCV; break;
                    case "NPV": Tch = classglobal.NPV; break;
                    case "DPV": Tch = classglobal.DPV; break;
                    case "SWV": Tch = classglobal.SWV; break;
                    case "CV": Tch = classglobal.CV; break;
                    case "LSV": Tch = classglobal.LSV; break;
                    case "DCs": Tch = classglobal.DCS; break;
                    case "DPs": Tch = classglobal.DPS; break;
                    case "OCP": Tch = classglobal.OCP; break;
                    case "CPC": Tch = classglobal.CPC; break;
                    case "CHP": Tch = classglobal.CHP; break;
                    case "CHA": Tch = classglobal.CHA; break;
                    case "CHC": Tch = classglobal.CHC; break;
                    //case "SC": Tch = clasglobal.SC; break;
             //       case "CCC": Tch = classglobal.CCC; break;
                    default: Tch = 15; break;
                }
                return (double)Tch;
            }
            str = str.ToLower();
            prmName = prmName.ToLower();
            double val = 0;
            if (str.IndexOf(prmName) >= 0)
            {
                if (str.IndexOf(':') >= 0)
                    val = Convert.ToDouble(str.Substring(str.IndexOf(':') + 1));
                if (str.IndexOf('=') >= 0)
                    val = Convert.ToDouble(str.Substring(str.IndexOf('=') + 1));
            }
            //if (prmName.ToLower() == "scan rate")
            //    ScanRate = val;
            return val;
        }
        public void bgsubtractionf()
        {
            operation = "Overlay";
            int shomarcheck = 0;
            for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                if (treeView3.Nodes[0].Nodes[i].Checked)
                    shomarcheck++;
            if (shomarcheck == 2)
            {
                int N1 = -1, N2 = -1;
                for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                    if (treeView3.Nodes[0].Nodes[i].Checked)
                        if (N1 == -1) N1 = i;
                        else N2 = i;
                int count1 = chart3.Series[N1].Points.Count;
                int count2 = chart3.Series[N2].Points.Count;
                int count = 0;
                if (count1 > count2) count = count2;
                else count = count1;
                double[] x = new double[count];
                double[] y = new double[count];
                for (int i = 0; i < count; i++)
                {
                    x[i] = chart3.Series[N1].Points[i].XValue;
                    y[i] = Math.Abs(chart3.Series[N2].Points[i].YValues[0] - chart3.Series[N1].Points[i].YValues[0]);
                }
                show_Graph(0, this, "SubChart", x, y, true, count);
            }
            else
                MessageBox.Show("Overlay must be checked \n Number of open graphs must be equal 2");

        }
        public void show_Graph(int num_ser, frmanalyseGraph2 grfn, string main_subchart, double[] xx, double[] yy, bool overlaymain,int shomaresh)
        {
            int L = 0;
            if (xx.Length != yy.Length) // Convert bigest Array to smallest if length of x and y is not equal
            {
                L = xx.Length;
                if (yy.Length < L) L = yy.Length;
            }
            
            if (L == 0)
                //for (int l = 0; l < L; l++)
                //{
                //    x[l] = xx[l];
                //    y[l] = yy[l];
                //}
            
            {
                Array.Resize(ref xx, shomaresh);
                Array.Resize(ref yy, shomaresh);

              
            }
            if (main_subchart == "MainChart")
            {
                Set_Graph(0, true, overlaymain);

                chart3.Series[num_Series].Points.DataBindXY(xx, yy);
            }
            if (main_subchart == "SubChart")
            {

                if (operation == "i_Reverse" || operation == "i_Forward" || operation == "Integral" || operation == "Derivation" || operation == "Mathematics" || operation == "Overlay" || operation == "Tafel" || operation == "show_without_smoothing")
                {
                    chart4.Series.Clear();
                    chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());
                    chart4.Series[0].ChartType = SeriesChartType.FastLine;
                    chart4.Titles[0].Text = operation;
                    this.chart4.ChartAreas[0].AxisX.MajorGrid.Enabled = this.chart3.ChartAreas[0].AxisX.MajorGrid.Enabled;
                    this.chart4.ChartAreas[0].AxisX.MinorGrid.Enabled = this.chart3.ChartAreas[0].AxisX.MinorGrid.Enabled;
                    this.chart4.ChartAreas[0].AxisY.MajorGrid.Enabled = this.chart3.ChartAreas[0].AxisY.MajorGrid.Enabled;
                    this.chart4.ChartAreas[0].AxisY.MinorGrid.Enabled = this.chart3.ChartAreas[0].AxisY.MinorGrid.Enabled;
                                 
                  
                    //chart4.ChartAreas[0].AxisX.LabelStyle.Format = "#0e-0";
                    //chart4.ChartAreas[0].AxisY.LabelStyle.Format = "#0e-0";
                    if ( operation == "i_Forward")
                        smootht4(0, chart4.Series[0].Color, 2, 3);
                    else if (operation == "i_Reverse" )
                        smootht4(0, chart4.Series[0].Color, 2, 4);
                    else
                        chart4.Series[0].Points.DataBindXY(xx, yy);
                    chart4.ChartAreas[0].RecalculateAxesScale();
                   // chart4.Refresh();

                }
            }
           difxview2 = chart4.ChartAreas[0].AxisX.ScaleView.ViewMaximum - chart4.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
           difyview2 = chart4.ChartAreas[0].AxisY.ScaleView.ViewMaximum - chart4.ChartAreas[0].AxisY.ScaleView.ViewMinimum;

        }
       
        public void Set_Graph(int num, bool isFromOpen, bool overlaymain)
        {
            Random Random1 = new Random();
            Random rndm2 = new Random();
            Random rndm3 = new Random();
            //chart3.Titles[0].Text = "  ";
            int Random_Color_Index = 0;
            Random_Color_Index = (int)(Random1.NextDouble() * 90);
            if (!overlaymain)
                  chart3.Series.Clear();
            System.Windows.Forms.DataVisualization.Charting.Series ser1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            ser1.Color = classglobal.color_Graph[Random_Color_Index];
            ser1.LabelFormat = "#,##0.###########";
            if (opForm.rbfLine.Checked)
            {
                ser1.ChartType=SeriesChartType.FastLine; 

            }
            if (opForm.rbLine.Checked)
            {
                ser1.ChartType = SeriesChartType.Line; 
            }
            if (opForm.rbPoint.Checked)
            {
                ser1.ChartType = SeriesChartType.Point; 
              }
  
            chart3.Series.Add(ser1);
            num_Series = chart3.Series.Count - 1;////////////////////////////
            chart3.Series[num_Series].Color = classglobal.color_Graph[Random_Color_Index];         
           
            if (!overlaymain)
            {
                chart3.Series[num_Series].Points.Clear();
               
                chart3.Series.Remove(chart3.Series[0]);
            }
          

            chart3.ChartAreas[0].Area3DStyle.Enable3D= false;
           // chart3.BackColor = Color.White;
           
            //chart3.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
            //chart3.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
            chart3.ChartAreas[0].AxisY.Title = classglobal.VAxisTitle[tech];
            chart3.ChartAreas[0].AxisX.Title = classglobal.HAxisTitle[tech];
            if (num_Series < 1)
                Overlay_tech[0] = tech;
            else
                Overlay_tech[num_Series] = tech;
            //chart3.ChartAreas[0].AxisY.LabelStyle.Format = "#.#e-0";
            //chart3.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";
            //this.chart3.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            //this.chart3.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            //this.chart3.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            //this.chart3.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            }

        private void clearselplot()
        {

            
                if (chart3.Series.Count > treeView3.Nodes[0].Nodes.Count)
                    for (int i = treeView3.Nodes[0].Nodes.Count; i < chart3.Series.Count; i++)
                    {
                        chart3.Series.Remove(this.chart3.Series[i]);
                        num_Series--;
                       
                       
                    }
              
                    if ( MessageBox.Show("Removed all selected Plot(s)?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int num_del = 0;
                        int n = chart3.Series.Count;
                        if (n == 1)
                        {

                            try
                            {
                                clearform();
                            }
                            catch { }
                           
                        }
                        else
                            try
                            {
                                for (int i = 0; i < n; i++)
                                    if (treeView3.Nodes[0].Nodes[i - num_del].Checked )
                                    {
                                        chart3.Series.Remove(chart3.Series[i - num_del]);
                                        treeView3.Nodes[0].Nodes[i - num_del].Remove();
                                        dataSet12.analyselist.Rows[i - num_del].Delete();
                                        dataSet12.analyselist.AcceptChanges();                                       
                                        num_del++;
                                        num_Series--;
                                        chart3.Refresh();
                                    }

                            }
                            catch { }
                       
                    }
                if (this.treeView3.Nodes[0].Nodes.Count > 0)
                {
                    this.treeView3.SelectedNode = this.treeView3.Nodes[0].FirstNode;
                    this.treeView3.Nodes[0].FirstNode.Checked = true;

                }
            

          
        }
      
        public void mathematicf(int XorY, string txt, double value, int i)
        {

            double[] P = new double[this.chart3.Series[i].Points.Count];
            double[] I = new double[this.chart3.Series[i].Points.Count];
            for (int k = 0; k < this.chart3.Series[i].Points.Count; k++)
            {
                P[k] = this.chart3.Series[i].Points[k].XValue;
                I[k] = this.chart3.Series[i].Points[k].YValues[0];
            }
            mc.Math_Operation(P, I, XorY, txt, value);


            // chart3.Series[num_Series].Clear();
            operation = "Mathematics";
            show_Graph(0, this, "SubChart", mc.AR, mc.BR, true,mc.AR.Length);

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            drawline1.EnableDraw = false;
            drawline1.removeall();
            int shomarcheck = 0, i1 = 0;
            for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                if (treeView3.Nodes[0].Nodes[i].Checked)
                {
                    shomarcheck++;
                    i1 = i;
                }
            if (shomarcheck == 1)
            {
                frmMath mthForm = new frmMath(i1);
                mthForm.gf = this;

                mthForm.TopMost = true;
                mthForm.ShowDialog();
                nindexchart2 = i1;
            }
            else
                MessageBox.Show("Mathematical Operation work with one graph" + (char)13 + "Please select ONE only!!!!!!");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            drawline1.EnableDraw = false;
            drawline1.removeall(); 
            int i1 = 0;
            int shomarcheck = 0;
            for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                if (treeView3.Nodes[0].Nodes[i].Checked)
                {
                    shomarcheck++;
                    i1 = i;
                }
            if (shomarcheck == 1)
            {
                operation = "Integral";
                mc.tq = tech;
                double[] P = new double[this.chart3.Series[i1].Points.Count];
                double[] I = new double[this.chart3.Series[i1].Points.Count];
                for (int k = 0; k < this.chart3.Series[i1].Points.Count; k++)
                {
                    P[k] = this.chart3.Series[i1].Points[k].XValue;
                    I[k] = this.chart3.Series[i1].Points[k].YValues[0];

                }
                mc.Integral(P, I, 0, 10);
                show_Graph(0, this, "SubChart", mc.AR, mc.BR, true, mc.AR.Length);
                nindexchart2 = i1;
            }
            else
                MessageBox.Show("Integration work with one graph" + (char)13 + "Please select ONE only!!!");
        }

       


       

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            frmShowPeaks fpeak = new frmShowPeaks();
           // bool Detect_Peak = true;
            // fpeak.fm = this;
            fpeak.Foption = this.opForm;
            fpeak.MdiParent = this;
            fpeak.Show();
        }

       

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            drawline1.EnableDraw = false;
            drawline1.removeall();
            bgsubtractionf();
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            chart3.ChartAreas[0].AxisX.LabelStyle.Enabled = !chart3.ChartAreas[0].AxisX.LabelStyle.Enabled;
          //  chart3.ChartAreas[0].AxisX.MinorTickMark.Enabled = !chart3.ChartAreas[0].AxisX.MinorTickMark.Enabled;
            chart3.ChartAreas[0].AxisX.MajorTickMark.Enabled = !chart3.ChartAreas[0].AxisX.MajorTickMark.Enabled;
            chart3.ChartAreas[0].AxisY.LabelStyle.Enabled = !chart3.ChartAreas[0].AxisY.LabelStyle.Enabled;
          //  chart3.ChartAreas[0].AxisY.MinorTickMark.Enabled = !chart3.ChartAreas[0].AxisY.MinorTickMark.Enabled;
            chart3.ChartAreas[0].AxisY.MajorTickMark.Enabled = !chart3.ChartAreas[0].AxisY.MajorTickMark.Enabled;

            this.chart3.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            this.chart3.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            this.chart3.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            this.chart3.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
          
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
        //    opForm.gf = this.mainf.grf;
        //    opForm.ShowDialog();
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            chart4.Series.Clear();
            chart4.Refresh();
            pnl_peak_search.Visible = false;
             
            btnzoomout_Click(sender, e);//هنگام باز کردن دوباره کامل نمایش یابد
            opengraph();
            btnzoomout_Click(sender, e);//هنگام باز کردن دوباره کامل نمایش یابد

            //string[] color = File.ReadAllLines(System.Windows.Forms.Application.StartupPath + "\\Color.dat");  // هنگام باز کردن رنگ همه یکی میشود
            //string s = color[7].ToString().Substring(color[7].ToString().IndexOf(":") + 1);
            //Color cl = Color.FromArgb(Convert.ToInt32(s));
            //for (int i = 0; i < chart3.Series.Count; i++)
            //    chart3.Series[i].Color = cl;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            drawline1.EnableDraw = false;
            drawline1.removeall();
            int i1 = 0;
            int shomarcheck = 0;
            for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                if (treeView3.Nodes[0].Nodes[i].Checked)
                {
                    shomarcheck++;
                    i1 = i;
                }
            if (shomarcheck == 1)
            {
                operation = "Derivation";
                mc.tq = tech;
                double[] P = new double[this.chart3.Series[i1].Points.Count];
                double[] I = new double[this.chart3.Series[i1].Points.Count];
                for (int k = 0; k < this.chart3.Series[i1].Points.Count; k++)
                {
                    P[k] = this.chart3.Series[i1].Points[k].XValue;
                    I[k] = this.chart3.Series[i1].Points[k].YValues[0];

                }
                mc.Derivate(P, I);
                show_Graph(0, this, "SubChart", mc.AR, mc.BR, true,mc.AR.Length);
                nindexchart2 = i1;
            }
            else
                MessageBox.Show("derivation work with one graph" + (char)13 + "Please select ONE only!!!");

        }

    

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            int shomarcheck = 0;
            for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                if (treeView3.Nodes[0].Nodes[i].Checked)
                    shomarcheck++;
            if (shomarcheck == 1)
            {
                operation = "Tafel";// "Mathematics";
                for (int i = 0; i < chart3.Series.Count; i++)
                    if (chart3.Series[i].Enabled)
                    {
                        double[] P = new double[this.chart3.Series[i].Points.Count];
                        double[] I = new double[this.chart3.Series[i].Points.Count];
                        for (int k = 0; k < this.chart3.Series[i].Points.Count; k++)
                        {
                            P[k] = this.chart3.Series[i].Points[k].XValue;
                            I[k] = this.chart3.Series[i].Points[k].YValues[0];
                        }
                        mc.Math_Operation(P, I, 1, "Tafel", 10);
                        show_Graph(0, this, "SubChart", mc.AR, mc.BR, true, mc.AR.Length);
                        nindexchart2 = i;
                    }
                drawline1.EnableDraw = true;
               
                chart4.Focus();
            }
            else
                MessageBox.Show("Tafel works with one graph" + (char)13 + "Please select ONE only");
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
           // save_Data();
        }

  
        private void tsbSaveAll_Click(object sender, EventArgs e)
        {
            int f = 0;

            for(int i=0;i<treeView3.Nodes[0].Nodes.Count;i++)
                if(treeView3.Nodes[0].Nodes[i].Checked==true )
                {
                    f = 1;
                    break;
                }

            if (f == 1)
                save_Data();
            else
                MessageBox.Show("There is not any case select for saving !!!");
        }
       
        private System.Drawing.Point point_start, TempPoint, point_end;
        int numline = -1;
      
        private void tChart2_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (statebutton_1.Trim() == "drawline")
                {
                    if (e.Button == MouseButtons.Left && drawline1.EnableDraw == true)
                    {
                        point_start = new System.Drawing.Point(e.X, e.Y);
                        TempPoint = new System.Drawing.Point(e.X, e.Y);
                    }
                }
                else
                {
                    mDown = chart3.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X);
                    mDowny = chart3.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y);
                }

                double x = chart4.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X);
                double y = chart4.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y);

                if (start_click == 1)
                {
                    chart4.Series[1].ChartType = SeriesChartType.Point;
                    chart4.Series[1].Color = Color.Red;
                    chart4.Series[1].BorderWidth = 3;
                    draw_point(x, y);//پیدا کردن نزدیکترین نقطه به نقطه رسم شده در گراف    
                }
                else if (end_click == 1)
                {
                    chart4.Series[1].ChartType = SeriesChartType.Spline;
                    chart4.Series[1].Color = Color.Red;
                    chart4.Series[1].BorderWidth = 1;
                    draw_point(x, y);

                    end_click = 0;
                    this.Cursor = Cursors.Default;
                    chart4.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }                   
        }

        public void draw_point(double x,double y)
        {
            try
            {
                int forward_count = Counter_num;//برای گراف سی وی که رفت و برگشت دارد این رفت
                int department_back = 0;

                for (int i = 1; i < Counter_num; i++)
                {
                    if (volt[i] < volt[i - 1])//برای تکنیک سی وی هنگام برگشت
                    {
                        forward_count = i - 1;
                        department_back = 1;

                        break;
                    }
                }


                double x_2 = -32000;
                double y_2 = -32000;
                double y_2_back = 0;
                int con = 0;
                for (int i = 1; i < Counter_num; i++)//پیدا کردن نزدیک ترین ایکس به نقطه ایجاد شده در آرایه
                {
                    if (x >= volt[i - 1] && x <= volt[i])
                    {
                        if (x == volt[i])
                            x_2 = volt[i];
                        else
                            x_2 = volt[i - 1];

                        con = i;

                        break;
                    }
                }

                if (department_back == 1)
                {
                    if (x >= volt[forward_count])//برای انتخاب آخرین نقطه ای که در مرز برگشت است
                        x_2 = volt[forward_count];
                }
                else
                {
                    if (x >= volt[forward_count - 1])//برای انتخاب آخرین نقطه
                        x_2 = volt[forward_count - 1];
                }
                


                if (x_2 == -32000)//اگر حتما مقداری انتخاب شده است
                    return;

                int flag_back = 0;

                for (int i = 0; i < Counter_num; i++)//پیدا کردن نزدیکترین ایگرگ به نقطه ایجاد شده
                {
                    if (volt[i] == x_2)
                    {
                        if (flag_back == 0)
                        {
                            y_2 = current[i];
                            flag_back = 1;
                        }
                        else
                        {
                            y_2_back = current[i];// در صورتی که گراف برگشتی است مقدار دوم را هم بگیرد
                            flag_back = 5;
                            break;
                        }
                    }
                }

                if (y_2 == -32000)
                    return;

                double final_y = y_2;

                if (flag_back == 5)//اگر برگشتی دارد
                {
                    double y_stor = y_2;
                    double y_stor_back = y_2_back;

                    if (y < 0) y = (-1) * y;//همه نقاط را مثبت میکنیم و سپس نزدیکترین نقطه را در محور ایگرگ نسبت به رفت یا برگشت انتخاب می کنیم
                    if (y_2 < 0) y_2 = (-1) * y_2;
                    if (y_2_back < 0) y_2_back = (-1) * y_2_back;

                    double result_1 = 0;
                    double result_2 = 0;

                    if (y > y_2)
                        result_1 = y - y_2;
                    else
                        result_1 = y_2 - y;

                    if (y > y_2_back)
                        result_2 = y - y_2_back;
                    else
                        result_2 = y_2_back - y;


                    if (result_2 < result_1)
                        final_y = y_stor_back;
                    else
                        final_y = y_stor;
                }
                
                chart4.Series[1].Points.AddXY(x_2, final_y);
       
                start_click = 0;
                end_click = 1;    
            }
            catch (Exception m)
            {
                this.Cursor = Cursors.UpArrow;
                MessageBox.Show(m.Message);
                new_handle();
            }
        }
        private void tChart2_MouseMove(object sender, MouseEventArgs e)
        {
            //label2.Text = chart4.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X).ToString() + " " + chart4.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y).ToString();
            try
            {
                if (statebutton_1.Trim() == "")
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        var results1 = chart3.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                        foreach (var result in results1)
                        {
                            if (result.ChartElementType == ChartElementType.PlottingArea)
                            {
                                double xv = chart4.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X);
                                double yv = chart4.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y);
                                if (xv - mDown > 0)
                                    chart4.ChartAreas[0].AxisX.ScaleView.Position = chart4.ChartAreas[0].AxisX.ScaleView.ViewMinimum + (difxview2 / 500);
                                else if (xv - mDown < 0)
                                    chart4.ChartAreas[0].AxisX.ScaleView.Position = chart4.ChartAreas[0].AxisX.ScaleView.ViewMinimum - (difxview2 / 500);
                                else
                                    chart4.ChartAreas[0].AxisX.ScaleView.Position = chart4.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                                if ((yv - mDowny) > 0)
                                    chart4.ChartAreas[0].AxisY.ScaleView.Position = chart4.ChartAreas[0].AxisY.ScaleView.ViewMinimum + (difyview2 / 500);
                                else if ((yv - mDowny) < 0)
                                    chart4.ChartAreas[0].AxisY.ScaleView.Position = chart4.ChartAreas[0].AxisY.ScaleView.ViewMinimum - (difyview2 / 500);
                                else
                                    chart4.ChartAreas[0].AxisY.ScaleView.Position = chart4.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                                chart4.ChartAreas[0].AxisX.ScaleView.Size = difxview2; //(chart3.ChartAreas[0].AxisX.ScaleView.ViewMaximum - chart3.ChartAreas[0].AxisX.ScaleView.ViewMinimum); //+ xv - mDown;chart3.ChartAreas[0].AxisX.Minimum +
                                chart4.ChartAreas[0].AxisY.ScaleView.Size = difyview2;// (chart3.ChartAreas[0].AxisY.ScaleView.ViewMaximum - chart3.ChartAreas[0].AxisY.ScaleView.ViewMinimum) + yv - mDowny;//chart3.ChartAreas[0].AxisX.Minimum +

                                mDown = xv;
                                mDowny = yv;
                            }

                        }
                    }
                }

                var results11 = chart4.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                foreach (var result in results11)
                {
                    if (result.ChartElementType == ChartElementType.PlottingArea)
                    {

                        switch (statebutton_1)
                        {
                            case "selectonly":
                                chart4.Cursor = System.Windows.Forms.Cursors.Cross;
                                break;
                            case "showvalue":
                                chart4.Cursor = System.Windows.Forms.Cursors.SizeAll;

                                break;

                            default:
                                if (start_click == 0 && end_click==0)
                                    chart4.Cursor = System.Windows.Forms.Cursors.Default;
                                break;
                        }

                    }
                    else if (start_click == 0 && end_click == 0)
                        chart4.Cursor = System.Windows.Forms.Cursors.Default;


                }
                switch (statebutton_1)
                {
                    case "selectonly":
                        System.Drawing.Point mousePoint = new System.Drawing.Point(e.X, e.Y);
                        chart4.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, false);
                        chart4.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, false);

                        break;
                    case "showvalue":
                        var results12 = chart4.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                        double x111 = 0, y111 = 0;
                        foreach (var result in results12)
                        {
                            if (result.ChartElementType == ChartElementType.PlottingArea)
                            {
                                x111 = result.ChartArea.AxisX.PixelPositionToValue(e.X);
                                y111 = result.ChartArea.AxisY.PixelPositionToValue(e.Y);
                            }
                        }

                        string x = x111.ToString("#.##");
                       
                        if (x111 < 1)//قرار دادن صفر قبل از ممیز
                        {
                            if (x111 >= 0)
                                x = "0" + x;
                            else
                            {
                                x = (x111 * -1).ToString("#.##");
                                x = "-0" + x;
                            }
                        }
                                                                      
                        mainf.xyLabel.Text = x + "," + y111.ToString("#.#e-0");
                        //mainf.xyLabel.Text = x111.ToString("#.##") + "," + y111.ToString("#.#e-0");

                        break;
                    case "drawline":
                        if (e.Button == MouseButtons.Left && drawline1.EnableDraw == true)
                        {
                            if (!(point_start == null))
                            {
                                Graphics g = this.CreateGraphics();
                                Pen ErasePen = new Pen(Color.White);
                                g.DrawLine(ErasePen, point_start, TempPoint);
                                TempPoint = new System.Drawing.Point(e.X, e.Y);
                                this.chart4.Refresh();
                            }
                        }
                        break;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void tChart2_MouseUp(object sender, MouseEventArgs e)
        {
             if (statebutton_1.Trim() == "drawline")
            {
                 if (e.Button == MouseButtons.Left && drawline1.EnableDraw == true)
                {
                    point_end = new System.Drawing.Point(e.X, e.Y);
                    drawline_chart.lines l1;
                    l1.point1 = point_start;
                    l1.point2 = point_end;
                    drawline1.Lines.Add(l1);
                    // endline = true;
                    point_start = System.Drawing.Point.Empty;
                }
            }

        }
                             
        private void tChart2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 108 || e.KeyValue == 76)
            {
                numline++;
                if (numline >= drawline1.Lines.Count)
                    numline = 0;
                chart4.Refresh();

            }
            else if (e.KeyValue == 189)
            {
                if (numline > -1 && numline < drawline1.Lines.Count)
                {
                    drawline1.remove(numline);
                    numline--;
                    chart4.Refresh();
                }


            }

        }      
        public void conv_pixel_cord(int xx,int yy)
        {
            var results = chart4.HitTest(xx, yy, false, ChartElementType.PlottingArea);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.PlottingArea)
                {
                    double xVal = result.ChartArea.AxisX.PixelPositionToValue(xx);
                    double yVal = result.ChartArea.AxisY.PixelPositionToValue(yy);
                    drawline_chart.multipoint l1;
                    l1.pointx=xVal;
                    l1.pointy=yVal;

                     drawline1.Lines_master.Add(l1);

                }
            }
        }
     
        public void smootht4(int row1,Color color1,int typechart,int colnum)
        {         
            double  I0, I1 = 0;
            int vn = shomar_val;
            if (typechart != 1 || smooth_state)
            {
                for (int i = 1; i < vn - 1; i++)
                {
                    if (Math.Abs(othertech_val1[i] - othertech_val1[i - 1]) > 1000)
                        othertech_val1[i] = (othertech_val1[i - 1] + othertech_val1[i + 1]) / 2;
                }
                for (int i = vn - 1; i >= 1; i--)
                {
                    othertech_val1[i] = (othertech_val1[i - 1] + othertech_val1[i]) / 2;
                }
                for (int i = 1; i < vn - 1; i++)
                {
                    othertech_val1[i] = (othertech_val1[i - 1] + othertech_val1[i + 1]) / 2;
                }
                for (int j1 = 3; j1 >= 0; j1--)
                {
                    for (int i = 4; i <= vn - 3; i++)
                    {

                        I0 = ((othertech_val1[i - 1] - othertech_val1[i - 2]) + (othertech_val1[i - 2] - othertech_val1[i - 3]) + (othertech_val1[i - 3] - othertech_val1[i - 4])) / 3;
                        I1 = Math.Abs(othertech_val1[i] - othertech_val1[i - 1]);
                        if (I1 > Math.Abs(j1 * I0))
                            othertech_val1[i] = (othertech_val1[i - 2] + othertech_val1[i - 1] + othertech_val1[i + 2] + othertech_val1[i + 1]) / 4;

                    }
                }

                for (int i = 3; i <= vn - 4; i++)
                {
                    othertech_val1[i] = (othertech_val1[i - 3] + othertech_val1[i - 2] + othertech_val1[i - 1] + othertech_val1[i + 3] + othertech_val1[i + 2] + othertech_val1[i + 1]) / 6;
                }
            }
            if (typechart == 1)
            {
                chart3.Series[row1].Color = color1;
                chart3.Series[row1].Points.Clear();

                if (dataSet12.analyselist.Rows[row1][0].ToString() == "10" || dataSet12.analyselist.Rows[row1][0].ToString() == "11" || dataSet12.analyselist.Rows[row1][0].ToString() == "12")
                    chart3.Series[row1].Points.AddXY(0, othertech_val1[0]);//رسم در نقطه صفر زمان // technics chp cha chc
                  
                for (int i = 0; i < vn; i++)
                {
                    chart3.Series[row1].Points.AddXY(othertech_val0[i], othertech_val1[i]);
                }
            }
            else 
            {
                chart4.Series[row1].Points.Clear();
                for (int i = 0; i < vn; i++)
                {
                    chart4.Series[row1].Points.AddXY(othertech_val0[i], othertech_val1[i]);
                }
            }            
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("are you sure?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                pnl_peak_search.Visible = false;
                clearform();
            }
        }
       
       
        //private void toolStripButton20_Click(object sender, EventArgs e)
        //{
          
        //    if (chart4.Series.Count>0)
        //    {
        //        SaveFileDialog sfd = new SaveFileDialog();
        //        sfd.AddExtension = true;
        //        sfd.DefaultExt = "bmp";
        //        sfd.Filter = "Bitmap Files(*.bmp)|*.bmp|JPeg Files(*.jpg)|*.jpg|Gif Files(*.gif)|*.gif|Png Files(*.png)|*.png|tiff Files(*.tiff)|*.tiff";
        //        sfd.ShowDialog();
        //        if (sfd.FileName != "")
        //            switch (sfd.FilterIndex)
        //            {
        //                case 1:
        //                    chart4.SaveImage(sfd.FileName, ChartImageFormat.Bmp);
        //                    break;
        //                case 2:
        //                    chart4.SaveImage(sfd.FileName, ChartImageFormat.Jpeg);
        //                    break;
        //                case 3:
        //                    chart4.SaveImage(sfd.FileName, ChartImageFormat.Gif);
        //                    break;
        //                case 4:
        //                    chart4.SaveImage(sfd.FileName, ChartImageFormat.Png);
        //                    break;
        //                case 5:
        //                    chart4.SaveImage(sfd.FileName, ChartImageFormat.Tiff);
        //                    break;
        //                default:
        //                    MessageBox.Show("FORMAT not coorect!!!");
        //                    break;
        //            }

        //    }
        //    else
        //        MessageBox.Show("EXPORTING works with one graph" + (char)13 + "Please select ONE only!!!");
           
        //}

        //private void toolStripButton22_Click(object sender, EventArgs e)
        //{

        //    if (chart4.Series.Count > 0)
        //    {
        //        SaveFileDialog sfd = new SaveFileDialog();
        //        sfd.AddExtension = true;

        //        sfd.DefaultExt = "pdf";
        //        sfd.Filter = "Acrobat reader Files(*.pdf)|*.pdf";
        //        sfd.ShowDialog();
        //        if (sfd.FileName != "")
        //        {
        //            var doc = new Document();

        //            PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
        //            doc.Open();

        //            doc.Add(new Paragraph(""));

        //            using (var chartimage = new MemoryStream())
        //            {
        //                chart4.SaveImage(chartimage, ChartImageFormat.Png);

        //                byte[] cccc = chartimage.GetBuffer();
        //                var image11 = iTextSharp.text.Image.GetInstance(cccc);
        //                image11.ScalePercent(75f);
        //                doc.Add(image11);
        //                doc.Close();
        //            }

        //        }
        //    }
        //    else
        //        MessageBox.Show("EXPORTING works with one graph" + (char)13 + "Please select ONE only!!!");
          
        //}
        private void save_Data()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
//            sfd.DefaultExt = "dat";
            sfd.DefaultExt = "bhpd";
            string dir_sfd = "";
            sfd.Filter = "BHP Files(*.bhpd)|*.bhpd|Signal File(*.sig)|*.sig";

//            sfd.Filter = "Data Files(*.dat)|*.dat|Signal File(*.sig)|*.sig"; 
            int nn = this.treeView3.Nodes[0].Nodes.Count;
            int filename_index = 1, index_main_dataset = 0;
            string filename = "0";

            if (dir_sfd != "") sfd.InitialDirectory = dir_sfd;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //string ext = sfd.FileName.Substring(sfd.FileName.Length - 3, 3);
                FileInfo f_info = new FileInfo(sfd.FileName);
                string ext = f_info.Extension;

                //if (ext == "dat")
                if (ext == ".bhpd")
                {
                    string add = sfd.FileName.Substring(0, sfd.FileName.IndexOf('.'));
                    for (int j = 0; j < nn; j++)
                    {
                        if (Convert.ToInt16(dataSet12.analyselist.Rows[j][11].ToString()) > 1) //cycle > 1
                        {
                            for (int f = j; f < (j + Convert.ToInt16(dataSet12.analyselist.Rows[j][11].ToString())); f++)
                                if (this.treeView3.Nodes[0].Nodes[f].Checked)
                                {
                                    //filename = sfd.FileName.Substring(0, sfd.FileName.Length - 4) + "_" + treeView3.Nodes[0].Nodes[f].Text + "_" + (filename_index).ToString() + ".bhpd";
                                    filename = sfd.FileName;
                                    File.Copy(dataSet12.analyselist.Rows[j][21].ToString(), filename, true);
                                    filename_index++;
                                    MessageBox.Show("The file was saved.");
                                    break;
                                }

                            j += Convert.ToInt16(dataSet12.analyselist.Rows[j][11].ToString()) - 1; // becuse cycle this here is a graf but in database not rows
                        }
                        else
                        {
                            if (this.treeView3.Nodes[0].Nodes[j].Checked)
                            {
                                //filename = sfd.FileName.Substring(0, sfd.FileName.Length - 4) + "_" + treeView3.Nodes[0].Nodes[j].Text + "_" + (filename_index).ToString() + ".bhpd";
                                filename = sfd.FileName;
                                File.Copy(dataSet12.analyselist.Rows[j][21].ToString(), filename, true);

                                filename_index++;
                                MessageBox.Show("The file was saved.");
                            }
                        }

                        index_main_dataset++;
                    }
                }
                else if(ext == "sig")
                {
                    string name = sfd.FileName.Substring(0, sfd.FileName.Length - 4);

                    for (int j = 0; j < nn; j++)
                    {
                        string file_name = name + "_" + (j + 1).ToString() + "." + ext;

                        if (this.treeView3.Nodes[0].Nodes[j].Checked)
                        {       
                            Array.Clear(volt, 0, Counter_num);
                            Array.Clear(current, 0, Counter_num);

                            Counter_num = chart3.Series[j].Points.Count;

                            Array.Resize(ref volt, Counter_num);
                            Array.Resize(ref current, Counter_num);
                                                       
                            for (int i = 0; i < Counter_num; i++)
                            {
                                double d = chart3.Series[j].Points[i].XValue;
                                string msg = string.Format("{0:N4}", d);
                                msg = msg + "1";//نرم افزاری که فایل سیگ را باز میکند اگر مقادیر اعشار نداشته باشد گراف ترسیم نمیکند
                               
                                volt[i] = Double.Parse(msg);
                                current[i] = chart3.Series[j].Points[i].YValues[0];
                            }
                                                                                    
                            File.WriteAllBytes(file_name, fill_matris());
                        }

                        row_sig++;
                    }
                }
            }
        }

        int row_sig = 0;
        double[] volt = new double[300];
        double[] current = new double[300];
        int Counter_num = 0;

        public byte[] fill_matris()
        {
            byte[] b = new byte[5000];
            int Counter = 0, Teqnic = 0;

            double Startpotintial2 = 0, firstvertexpotential = 0, voltagstep = 0, Sweeprate = 0, Initial_parge_time = 0;
            double Cleaningpotintial = 0, Cleaningtime = 0, Depositionpotential = 0, depositiontime = 0, standofpotential = 0;
            double Startpotintial = 0, Endpotintial = 0, equilibtime = 0, secondvertexpotential = 0;
            double Frequency = 0, Amplitude = 0, PulseTime = 0, BasePotential = 0, VoltageStepTime = 0, PulseAmplitude = 0;
            int Cycle = 0, Electrod_type = 0, Drop_size = 0, RDE_rpm = 0, numsweep = 0, numofsave = 0;
            byte[] Temp;
            string Start_bp = "[SIGNAL_PARAMETERS]";///19
            string Start_bpe = "[SIGNAL_PARAMETERS_END]";////21
            string Start_bd = "[SIGNAL_DATA]";////11
            string Start_bde = "[SIGNAL_DATA_END]";/////15
            switch (classglobal.TechName[int.Parse(dataSet12.analyselist.Rows[row_sig][0].ToString())])
            {
                case "NPV":
                    Teqnic = int.Parse(dataSet12.analyselist.Rows[row_sig][0].ToString());
                    Startpotintial2 = double.Parse(dataSet12.analyselist.Rows[row_sig][1].ToString());
                    firstvertexpotential = double.Parse(dataSet12.analyselist.Rows[row_sig][2].ToString());
                    voltagstep = double.Parse(dataSet12.analyselist.Rows[row_sig][4].ToString());
                    VoltageStepTime = double.Parse(dataSet12.analyselist.Rows[row_sig][7].ToString());
                    BasePotential = double.Parse(dataSet12.analyselist.Rows[row_sig][1].ToString());
                    PulseTime = double.Parse(dataSet12.analyselist.Rows[row_sig][8].ToString());

                    break;
                case "DPV":
                    Teqnic = int.Parse(dataSet12.analyselist.Rows[row_sig][0].ToString());
                    Startpotintial2 = double.Parse(dataSet12.analyselist.Rows[row_sig][1].ToString());
                    firstvertexpotential = double.Parse(dataSet12.analyselist.Rows[row_sig][2].ToString());
                    voltagstep = double.Parse(dataSet12.analyselist.Rows[row_sig][4].ToString());
                    Sweeprate = double.Parse(dataSet12.analyselist.Rows[row_sig][8].ToString());
                    PulseAmplitude = double.Parse(dataSet12.analyselist.Rows[row_sig][9].ToString());
                    PulseTime = double.Parse(dataSet12.analyselist.Rows[row_sig][8].ToString());
                    VoltageStepTime = double.Parse(dataSet12.analyselist.Rows[row_sig][7].ToString());

                    break;
                case "SWV":
                    Teqnic = int.Parse(dataSet12.analyselist.Rows[row_sig][0].ToString());
                    Startpotintial2 = double.Parse(dataSet12.analyselist.Rows[row_sig][1].ToString());
                    firstvertexpotential = double.Parse(dataSet12.analyselist.Rows[row_sig][2].ToString());
                    voltagstep = double.Parse(dataSet12.analyselist.Rows[row_sig][4].ToString());
                    Amplitude = double.Parse(dataSet12.analyselist.Rows[row_sig][9].ToString());
                    Frequency = double.Parse(dataSet12.analyselist.Rows[row_sig][10].ToString());

                    break;
                case "CV":
                    Teqnic = int.Parse(dataSet12.analyselist.Rows[row_sig][0].ToString());
                    Startpotintial2 = double.Parse(dataSet12.analyselist.Rows[row_sig][1].ToString());// MessageBox.Show(dschart2.analyselist.Rows[row_sig][1].ToString());
                    firstvertexpotential = double.Parse(dataSet12.analyselist.Rows[row_sig][2].ToString());
                    secondvertexpotential = double.Parse(dataSet12.analyselist.Rows[row_sig][3].ToString());
                    voltagstep = double.Parse(dataSet12.analyselist.Rows[row_sig][4].ToString());
                    Sweeprate = double.Parse(dataSet12.analyselist.Rows[row_sig][8].ToString());
                    numsweep = int.Parse(dataSet12.analyselist.Rows[row_sig][11].ToString());
                    numofsave = 1;

                    break;
                case "LSV":
                    Teqnic = int.Parse(dataSet12.analyselist.Rows[row_sig][0].ToString());
                    Startpotintial2 = double.Parse(dataSet12.analyselist.Rows[row_sig][1].ToString());
                    firstvertexpotential = double.Parse(dataSet12.analyselist.Rows[row_sig][2].ToString());
                    voltagstep = double.Parse(dataSet12.analyselist.Rows[row_sig][4].ToString());
                    VoltageStepTime = double.Parse(dataSet12.analyselist.Rows[row_sig][7].ToString());

                    break;
            }

            //////////////////////////////////////////////
            Counter = 0;
            ///////////////Start parameter//////////////
            ///////////0A [SIGNAL_PARAMETERS]0A////////
            /////////////////0-14 ///////////////////
            b[Counter] = 0x0A;
            Counter++;
            for (int i = 0; i < Start_bp.Length; i++)
            {
                b[Counter] = (byte)Start_bp[i];
                Counter++;
            }
            b[Counter] = 0x0A;
            Counter++;

            //////////teqnic/////////
            ///////////np=03 , dp=0 , sqw=1 , dc=02 , cv=04 
            ///////////ac=06 , psa  , ccpsa , cpvs , cvs
            ////////////15-18 //////////////
            /*
switch (comboBox2.Items[comboBox2.SelectedIndex].ToString())
{
    case "NP - Normal Pulse":
        Teqnic = 3;
        break;
    case "DP - Differential Pulse":
        Teqnic = 0;
        break;
    case "Sqw - Squar Wave":
        Teqnic = 1;
        break;
    case "CV - cyclic Voltammetry":
        Teqnic = 4;
        break;
    case "DC - Sample Direct Current":
        Teqnic = 2;
        break;
    case "AC - Alternating Current Voltammetry":
        Teqnic = 6;
        break;
    case "PSA - Potentioetric Stripping Analysis":
        Teqnic = 8;
        break;
    case "CCPSA - Constant Current PSA":
        Teqnic = 9;
        break;
    case "CPVS - Cyclic Pulse Voltammetric Stripping":
        Teqnic = 7;
        break;
    case "CVS -Cyclic Voltammetric Stripping":
        Teqnic = 4;
        break;
}
*/

            // Counter = 0x15;
            Temp = HexstringToByteArray(inttoHex(Teqnic));
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            /*
                b[Counter] = 0x04;
                Counter++;
                b[Counter] = 0x00;
                Counter++;
                b[Counter] = 0x00;
                Counter++;
                b[Counter] = 0x00;
                Counter++;
          */
            ////////////no know/////////
            ////////////19-20 //////////////
            //  Counter = 0x19;
            b[Counter] = 0x03;
            Counter++;
            b[Counter] = 0x00;
            Counter++;
            /////////////High range//////////
            ////////////0B-0E //////////////
            //   Counter = 0x0B;
            Temp = HexstringToByteArray(inttoHex(8));
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ////////////Low Range ///////////
            /////////// 0F -22 ////////////
            //  Counter = 0x0F;
            Temp = HexstringToByteArray(inttoHex(4));
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ///////////////////Initial parge time/////////////
            //////////////////23-2A ////////////////////////
            //Initial_parge_time = double.Parse(textBox1.Text);
            // Counter = 0x23;
            Initial_parge_time = 0;///double.Parse(1);
            Temp = HexstringToByteArray(DoubletoHex(Initial_parge_time));//////Initial_parge_time
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }


            ////////////////cleaning potential////////////
            //////////////////2B - 32///////////////////
            // Cleaningpotintial = double.Parse(textBox5.Text);
            Counter = 0x2B;
            Cleaningpotintial = 0;
            Temp = HexstringToByteArray(DoubletoHex(Cleaningpotintial));///////Cleaningpotintial
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ////////////////cleaning Time////////////
            /////////////////33-3A/////////////////
            // Cleaningtime = double.Parse(textBox6.Text);
            Counter = 0x33;
            Cleaningtime = 0;// double.Parse(1);
            Temp = HexstringToByteArray(DoubletoHex(Cleaningtime));////Cleaningtime
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ////////////////Deposition potential////////////
            /////////////// 3B-42 /////////////////
            Counter = 0x3B;
            Depositionpotential = 0;
            Temp = HexstringToByteArray(DoubletoHex(Depositionpotential));/////Depositionpotential
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ////////////////Deposition Time////////////
            /////////////////43-4A///////////////////
            depositiontime = 0;
            Counter = 0x43;
            Temp = HexstringToByteArray(DoubletoHex(depositiontime));/////depositiontime
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ////////////////Hydrodinamic////////////
            /////////////////4B -4E ////////////////
            //  b[0x4B] = 0x00;
            // if (checkBox2.Checked ) Temp = HexstringToByteArray(inttoHex(1));
            //else Temp = HexstringToByteArray(inttoHex(0));
            Counter = 0x4B;
            Temp = HexstringToByteArray(inttoHex(0));
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ///////////////////Cell off //////////////
            /////////////////4F -52 ////////////////
            // b[0x4F] = 0x01;
            //if (checkBox1.Checked ) Temp = HexstringToByteArray(inttoHex(1));
            //else Temp = HexstringToByteArray(inttoHex(0));
            Temp = HexstringToByteArray(inttoHex(0));
            Counter = 0x4F;
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ////////////////Stand by Potintion////////////
            /////////////////53-5A///////////////////
            //standofpotential = double.Parse(textBox16.Text);
            standofpotential = 0;
            Temp = HexstringToByteArray(DoubletoHex(standofpotential));///////standofpotential
            Counter = 0x53;
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ////////////////Start Potential////////////
            /////////////////5B-62///////////////////
            // Startpotintial = double.Parse(textBox2.Text);
            Startpotintial = 0;//// double.Parse(1);
            Temp = HexstringToByteArray(DoubletoHex(Startpotintial));////////Startpotintial
            Counter = 0x5B;
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ////////////////End Potential////////////
            /////////////////63-6A///////////////////
            // Endpotintial = double.Parse(textBox3.Text);
            Endpotintial = 0;
            Temp = HexstringToByteArray(DoubletoHex(Endpotintial));////////Endpotintial
            Counter = 0x63;
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ////////////////Cycle //////////////////
            /////////////////6B ,6C///////////////////
            // Cycle = int.Parse(textBox4.Text);
            Cycle = 1; ///int.Parse(1);
            Temp = HexstringToByteArray(inttoHex(Cycle));
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ////////////////Eq Time ////////////////
            /////////////////6D-76 //////////////////
            equilibtime = 0;
            Temp = HexstringToByteArray(DoubletoHex(equilibtime));
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ////////////////electrod////////////
            ////////0<electrod<4////////
            //SMDE=01
            //HMDE=02
            //RDE=03 /////active hydro
            //   =00
            /////////////////77-7A ////////////////
            Electrod_type = 3;
            Temp = HexstringToByteArray(inttoHex(Electrod_type));//////////Electrod_type
                                                                 // Temp = HexstringToByteArray(inttoHex(1));
            Counter = 0x77;
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ////////////////Drop /////////////////
            /////////////0<Drop<9 /////////////////
            /////////////////7B -7E ////////////////
            // Drop_size = int.Parse(domainUpDown1.Items[domainUpDown1.SelectedIndex].ToString());
            Drop_size = 9;
            Temp = HexstringToByteArray(inttoHex(Drop_size));///////////Drop_size
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ////////////////RPM ///////////////////
            ///////*200 /////////
            //RPM=3000 (MAX) ,,,,,RPM=0F ///////
            /////////////////7F -82 ////////////////
            // RDE_rpm = (int.Parse(domainUpDown2.Items[domainUpDown2.SelectedIndex].ToString())) / 200;
            RDE_rpm = 4;
            Temp = HexstringToByteArray(inttoHex(RDE_rpm));////////////RDE_rpm
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ///////////// no know /////////////
            ///////////////83 ,84 ///////////
            b[Counter] = 0x04;
            Counter++;
            b[Counter] = 0x00;
            Counter++;
            //////////Start potential////////////
            ///////////// 85 - 8C /////////////

            //Startpotintial2 = double.Parse(textBox10.Text);
            Temp = HexstringToByteArray(DoubletoHex(Startpotintial2));///////////Startpotintial2
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            //////////first  potential(cv)or Endpotintial2////////////
            ///////////// 8D - 94 /////////////
            // firstvertexpotential = double.Parse(textBox11.Text);
            Temp = HexstringToByteArray(DoubletoHex(firstvertexpotential));///////////firstvertexpotential
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            //////////voltage step////////////
            ///////////// 95 - 9C /////////////
            // voltagstep = double.Parse(textBox12.Text);
            Temp = HexstringToByteArray(DoubletoHex(voltagstep));/////voltagstep
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ///////////// No.oF SWeeps(cv) /////////////
            //////////////9D - A0 //////////////
            if (Teqnic == 4)
            {
                /// int.Parse(textBox14.Text);
                Temp = HexstringToByteArray(inttoHex(numsweep));//////numsweep
                for (int i = 0; i < Temp.Length; i++)
                {
                    b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                    Counter++;
                }
            }
            else
            {
                b[Counter] = 0x01;
                Counter++;
                b[Counter] = 0x00;
                Counter++;
                b[Counter] = 0x00;
                Counter++;
                b[Counter] = 0x00;
                Counter++;
            }

            //////////Start  potential////////////
            ///////////// A1 - A8 /////////////
            //Startpotintial2 = double.Parse(textBox10.Text);
            Temp = HexstringToByteArray(DoubletoHex(Startpotintial2));////////Startpotintial2
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            //////////Second   potential(cv) or Start  potential ////////////
            ///////////// A9 - B0  /////////////
            if (Teqnic == 4)
            {
                //secondvertexpotential = double.Parse(textBox15.Text);
                Temp = HexstringToByteArray(DoubletoHex(secondvertexpotential));///////secondvertexpotential
            }
            else
            {
                //  Startpotintial2 = double.Parse(textBox10.Text);
                Temp = HexstringToByteArray(DoubletoHex(Startpotintial2));////////Startpotintial2
            }
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ///////////// no know /////////////
            /////////////////B1 , B2 //////////
            b[Counter] = 0x02;
            Counter++;
            b[Counter] = 0x00;
            Counter++;
            /////////// Sweep rate(cv) or Base potential(npv) /////////////
            ////////// Pulse amplitude(dpv)or Amplitude(swv)
            ///////////// B3-BA  /////////////
            if (Teqnic == 4)
            {
                Temp = HexstringToByteArray(DoubletoHex(Sweeprate));
            }
            if (Teqnic == 1)
            {
                Temp = HexstringToByteArray(DoubletoHex(BasePotential));
            }
            if (Teqnic == 5)
            {
                Temp = HexstringToByteArray(DoubletoHex(VoltageStepTime));
            }
            if (Teqnic == 2)
            {
                Temp = HexstringToByteArray(DoubletoHex(PulseAmplitude));
            }
            if (Teqnic == 3)
            {
                Temp = HexstringToByteArray(DoubletoHex(Amplitude));
            }
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            ///////////// Save last(cv) or Pulse time(npv & dpv) /////////////
            //////////// Frequency(swv)
            //////////////BB - C2 /////////////////
            if (Teqnic == 4)
            {
                Temp = HexstringToByteArray(inttoHex(numofsave));
            }
            if (Teqnic == 3)
            {
                Temp = HexstringToByteArray(DoubletoHex(Frequency));
            }
            if (Teqnic == 1 || Teqnic == 2)
            {
                Temp = HexstringToByteArray(DoubletoHex(PulseTime));
            }
            if (Teqnic == 4 || Teqnic == 1 || Teqnic == 3 || Teqnic == 2)
            {
                for (int i = 0; i < Temp.Length; i++)
                {
                    b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                    Counter++;
                }
            }
            //////////////////////////////////////
            ///////////// Voltage step time(npv&dpv) /////////////
            //////////////C3 - CA //////////////
            if (Teqnic == 1 || Teqnic == 2)
            {
                Temp = HexstringToByteArray(DoubletoHex(VoltageStepTime));//////Sweeprate
                for (int i = 0; i < Temp.Length; i++)
                {
                    b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                    Counter++;
                }
            }
            b[Counter] = 0x0A;
            Counter++;
            for (int i = 0; i < Start_bpe.Length; i++)
            {
                b[Counter] = (byte)Start_bpe[i];
                Counter++;
            }
            b[Counter] = 0x0A;
            Counter++;
            b[Counter] = 0x0A;
            Counter++;
            for (int i = 0; i < Start_bd.Length; i++)
            {
                b[Counter] = (byte)Start_bd[i];
                Counter++;
            }
            b[Counter] = 0x0A;
            Counter++;
            ///////////////////////////////////////////////////////////////////
            ////////////////////number data ////////////////////////////
            Temp = HexstringToByteArray(inttoHex(Counter_num));////////////RDE_rpm
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }
            /////////////////////////////////////////////////////////////////
            ////////////////// send data volt and current //////////////////
            for (int i1 = 0; i1 < Counter_num; i1++)
            {
                Temp = HexstringToByteArray(DoubletoHex(volt[i1]));  // volttage
                for (int i2 = 0; i2 < Temp.Length; i2++)
                {
                    b[Counter] = (byte)Temp[(Temp.Length - 1) - i2];
                    Counter++;
                }
               
                ///////////current

                Temp = HexstringToByteArray(DoubletoHex(current[i1]));  // jarian
                for (int i3 = 0; i3 < Temp.Length; i3++)
                {
                    b[Counter] = (byte)Temp[(Temp.Length - 1) - i3];
                    Counter++;
                }

                //MessageBox.Show(volt[i1].ToString() + Environment.NewLine + current[i1].ToString());
            }
            ///////////////////////////////////////////////////// 
            ////////////////
            b[Counter] = 0x0A;
            Counter++;
            for (int i = 0; i < Start_bde.Length; i++)
            {
                b[Counter] = (byte)Start_bde[i];
                Counter++;
            }
            b[Counter] = 0x0A;

            b[Counter] = 0x0A;
            Counter++;

            return b;
        }

        private byte[] HexstringToByteArray(string hex)
        {
            int hexstringlength = hex.Length;
            byte[] b = new byte[hexstringlength / 2];
            for (int i = 0; i < hexstringlength; i += 2)
            {
                int topChar = (hex[i] > 0x40 ? hex[i] - 0x37 : hex[i] - 0x30) << 4;
                int bottomChar = hex[i + 1] > 0x40 ? hex[i + 1] - 0x37 : hex[i + 1] - 0x30;
                b[i / 2] = Convert.ToByte(topChar + bottomChar);
            }
            return b;
        }
        private string inttoHex(int Value_int16)
        {
            string Value_String;
            Value_String = Value_int16.ToString("X8");
            return Value_String;
        }
        private string DoubletoHex(Double Value_Double)
        {
            long Value_Long = 0;
            string Value_String;
            Value_Long = BitConverter.DoubleToInt64Bits(Value_Double);
            Value_String = Value_Long.ToString("X8");
            return Value_String;
        }

        //private void toolStripButton23_Click(object sender, EventArgs e)
        //{

        //    if (chart4.Series.Count > 0)
        //    {
        //        SaveFileDialog sfd = new SaveFileDialog();
        //        sfd.AddExtension = true;
        //        sfd.DefaultExt = "xml";
        //        sfd.Filter = "XML Files(*.xml)|*.xml";
        //        sfd.ShowDialog();
        //        if (sfd.FileName != "")
        //            chart4.Serializer.Save(sfd.FileName);
        //    }
        //    else
        //        MessageBox.Show("EXPORTING works with one graph" + (char)13 + "Please select ONE only!!!");

        //} 

        private void restore_Data_part(string fileName, int typedata1, int numberaddnode)
        {

            double[] xTmp = { 0 };
            double[] yTmp = { 0 };
            string s = "", fs = "";
            bool isData = false;
            int row = 0;
            if (fileName != "")
                fs = fileName;
            StreamReader Fl;

            EventArgs e = new EventArgs();
            if (fs != "")
            {
                Fl = new StreamReader(fs, Encoding.ASCII);

                try
                {
                    while (!Fl.EndOfStream)
                    {
                        s = Fl.ReadLine();
                        if (!isData)
                        {

                            if (s.LastIndexOf("Potential") >= 0 || s.LastIndexOf("========") >= 0)
                            {
                                isData = true;
                                s = Fl.ReadLine();
                                shomar_val = 0;
                                Array.Clear(othertech_val0, 0, othertech_val0.Length);
                                Array.Clear(othertech_val1, 0, othertech_val1.Length);
                            }
                        }
                        else                // if (isData)
                        {
                            int l = 0;
                            string sss = "";
                            int num = 0;
                            double x = 0, y = 0, z = 0, w = 0;
                            while (l < s.Length)
                            {

                                if (s[l] != '\t' && s[l] != ';' && s[l] != ' ')
                                    sss = sss + s[l];
                                if ((s[l] == '\t' || s[l] == ';' || s[l] == ' ') && sss != "")
                                {
                                    while (l < s.Length && s[l] == ' ') l++;

                                    try { if (num == 0) x = Convert.ToDouble(sss); } catch { x = 0; } //this.grf.dataE[num_data + num_ser, row] //890220
                                    try { if (num == 1) y = Convert.ToDouble(sss); } catch { y = 0; } //this.grf.dataI1[num_data + num_ser, row] = Convert.ToDouble(sss);
                                    try { if (num == 2) z = Convert.ToDouble(sss); } catch { z = 0; }//this.grf.dataI2[num_data + num_ser, row] = Convert.ToDouble(sss);
                                    try { if (num == 3) w = Convert.ToDouble(sss); } catch { w = 0; }//this.grf.dataI2[num_data + num_ser, row] = Convert.ToDouble(sss);

                                    sss = "";
                                    num++;
                                }
                                l++;
                            }

                            if (num == 3 && sss.Trim() != "")
                                w = Convert.ToDouble(sss);
                            if (shomar_val >= othertech_val0.Length)
                            {
                                Array.Resize(ref othertech_val0, othertech_val0.Length + 10);
                                Array.Resize(ref othertech_val1, othertech_val1.Length + 10);

                            }
                            othertech_val0[shomar_val] = x;
                            switch (typedata1)
                            {
                                case 1:

                                    othertech_val1[shomar_val] = y;
                                    break;
                                case 2:
                                    othertech_val1[shomar_val] = z;

                                    break;
                                case 3:
                                    othertech_val1[shomar_val] = w;

                                    break;

                            }
                            shomar_val++;
                            row++;
                            if (row >= 10000) row = 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + "selected File is not in valid Format...", "Warning");
                    //if (overlayopen == 1 || this.grf.chart3.Series.Count < 1)
                    //    this.grf.Close();

                    //isOkfile = false;
                }
                Fl.Close();


            }

        }
   
        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            drawline1.EnableDraw = false;
            drawline1.removeall(); 
            int shomarcheck = 0, ii = 0;
            for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                if (treeView3.Nodes[0].Nodes[i].Checked){
                    shomarcheck++;
                    ii=i;
                }
            if (shomarcheck == 1)
            {
                treeView3.SelectedNode = treeView3.Nodes[0].Nodes[ii];
                if (int.Parse(dataSet12.analyselist.Rows[ii][0].ToString()) == 2 || int.Parse(dataSet12.analyselist.Rows[ii][0].ToString()) == 3)
                {
                    restore_Data_part(dataSet12.analyselist.Rows[ii][21].ToString(), 2, 1);      
                   

                   
                    operation = "i_Forward";
                    show_Graph(0, this, "SubChart", othertech_val0, othertech_val1, true,shomar_val);
                    nindexchart2 = ii;
                }
                else 
                {
                    MessageBox.Show("operation is not correct for this technique", "Error");
                    return;
                }
            }
            else
                MessageBox.Show("i_Forward Operation work with one graph" + (char)13 + "Please select ONE only!!!!!!");
      
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            drawline1.EnableDraw = false;
            drawline1.removeall(); 
            int shomarcheck = 0, ii = 0;
            for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                if (treeView3.Nodes[0].Nodes[i].Checked)
                {
                    shomarcheck++;
                    ii = i;
                }
            if (shomarcheck == 1)
            {
                treeView3.SelectedNode = treeView3.Nodes[0].Nodes[ii];
                if (int.Parse(dataSet12.analyselist.Rows[ii][0].ToString()) == 2 || int.Parse(dataSet12.analyselist.Rows[ii][0].ToString()) == 3)
                {
                    restore_Data_part(dataSet12.analyselist.Rows[ii][21].ToString(), 3, 1);      

                    operation = "i_Reverse";
                    show_Graph(0, this, "SubChart", othertech_val0, othertech_val1, true,shomar_val);
                    nindexchart2 = ii;
                }
                else
                {
                    MessageBox.Show("operation is not correct for this technique", "Error");
                    return;
                }
            }
            else
                MessageBox.Show("i_Reverse Operation work with one graph" + (char)13 + "Please select ONE only!!!!!!");
      
        }

        private void toolStripButton11_Click_1(object sender, EventArgs e)
        {
            drawline1.EnableDraw = false;
            drawline1.removeall();
            int shomarcheck = 0, ii = 0;
            for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                if (treeView3.Nodes[0].Nodes[i].Checked)
                {
                    shomarcheck++;
                    ii = i;
                }
            if (shomarcheck == 1)
            {
                treeView3.SelectedNode = treeView3.Nodes[0].Nodes[ii];

                restore_Data_part(dataSet12.analyselist.Rows[ii][21].ToString(), 1, 1);      
               



                operation = "show_without_smoothing";
                show_Graph(0, this, "SubChart", othertech_val0, othertech_val1, true,shomar_val);
                nindexchart2 = ii;
            }
            else
                MessageBox.Show("show graph without smoothing Operation work with one graph" + (char)13 + "Please select ONE only!!!!!!");
        }

        private void toolStripButton5_Click_1(object sender, EventArgs e)
        {
            numline++;
            if (numline >= drawline1.Lines.Count)
                numline = 0;
            if (drawline1.Lines.Count == 0)
            {
                MessageBox.Show("There is not any line for selecting !!!");

            }

            chart4.Refresh();
        }

        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
            if (drawline1.Lines.Count == 0)
            {
                MessageBox.Show("There is not any line for deleting !!!");


            }
            if (numline > -1 && numline < drawline1.Lines.Count)
            {
                drawline1.remove(numline);
                numline--;
                chart4.Refresh();
            }
        }       

       
        private void toolStripButton9_Click_1(object sender, EventArgs e)
        {
            int shomarcheck = 0, i1 = 1, i2 = 1;
            for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                if (treeView3.Nodes[0].Nodes[i].Checked)
                {
                    shomarcheck++;
                }
            if (shomarcheck > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.AddExtension = true;
                sfd.DefaultExt = "txt";
                sfd.Filter = "Text document(*.txt)|*.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StreamWriter Fl = new StreamWriter(sfd.FileName, false, Encoding.ASCII);

                        

                        i2++;
                        for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                        {
                            if (treeView3.Nodes[0].Nodes[i].Checked)
                            {
                                Fl.WriteLine(i1.ToString().Trim() + "-" + classglobal.TechName[int.Parse(dataSet12.analyselist.Rows[i][0].ToString())]);
                                Fl.WriteLine("---------------------------------------------------");
                                Fl.WriteLine("row number      X value         Y value");
                                Fl.WriteLine("---------------------------------------------------");

                                for (int cc1 = 0; cc1 < chart3.Series[i].Points.Count; cc1++)
                                {
                                    Fl.Write((cc1 + 1).ToString().Trim() + "\t\t");
                                    Fl.Write(chart3.Series[i].Points[cc1].XValue.ToString() + "\t\t");
                                    Fl.WriteLine(chart3.Series[i].Points[cc1].YValues[0]);
                                    i2++;
                                }
                                i1++;
                                Fl.WriteLine("");


                            }
                        }
                        
                        Fl.Close();
                        MessageBox.Show("Done!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }

            }
            else
            {
                MessageBox.Show("Please select one technique!!!");
            }
        }
        private  string ExportToExcel(int colnum1,int typecall1, string outputPath)
        {
            try
            {
                // ساخت یک شی اکسل
                //ApplicationClass excelApp = new ApplicationClass();
                Microsoft.Office.Interop.Excel.Application excelApp;
                excelApp = new Microsoft.Office.Interop.Excel.Application();


                // ساخت یک WorkBook جدید
                Workbook excelWorkbook = excelApp.Workbooks.Add(Type.Missing);

                              
                int sheetIndex = 0,i2=0,i1=0;

                // ساخت آرایه به طول تعداد سطرهای دیتاتیبل+1 و تعداد ستونهای دیتاتیبل
                int rownum1=0;
                if (typecall1 == 1)
                {
                    for (int m1 = 0; m1 < chart3.Series.Count; m1++)
                    {
                        rownum1 += chart3.Series[m1].Points.Count;
                    }
                }
                else {
                    rownum1 = chart4.Series[0].Points.Count;
                }
                object[,] rawData = new object[rownum1 + 1,colnum1];
                if (typecall1 == 1)
                {
                    rawData[i2, 0] = "technique number";
                    rawData[i2, 1] = "technique name";
                    rawData[i2, 2] = "point of technique number";
                    rawData[i2, 3] = "X value";
                    rawData[i2, 4] = "Y value";
                    i2++;
                    for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                        if (treeView3.Nodes[0].Nodes[i].Checked)
                        {
                            for (int cc1 = 0; cc1 < chart3.Series[i].Points.Count; cc1++)
                            {
                                rawData[i2, 0] = i1;
                                rawData[i2, 1] = classglobal.TechName[int.Parse(dataSet12.analyselist.Rows[i][0].ToString())];
                                rawData[i2, 2] = cc1 + 1;
                                rawData[i2, 3] = chart3.Series[i].Points[cc1].XValue;
                                rawData[i2, 4] = chart3.Series[i].Points[cc1].YValues[0];
                                i2++;
                            }
                            i1++;


                        }
                }
                else {
                    rawData[i2, 0] = "row";
                    rawData[i2, 1] = "X value";
                    rawData[i2, 2] = "Y value";
                    i2++;
                    for (int cc1 = 0; cc1 < chart4.Series[0].Points.Count; cc1++)
                    {
                        rawData[i2, 0] = cc1 + 1;
                        rawData[i2, 1] = chart4.Series[0].Points[cc1].XValue;
                        rawData[i2, 2] = chart4.Series[0].Points[cc1].YValues[0];
                        i2++;
                    }
                }
                
                // محاسبه نام ستونهای اکسل

                string finalColLetter = string.Empty;
                string colCharset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                int colCharsetLen = colCharset.Length;

                if (colnum1 > colCharsetLen)
                {
                    finalColLetter = colCharset.Substring((colnum1 - 1) / colCharsetLen - 1, 1);
                }

                finalColLetter += colCharset.Substring((colnum1 - 1) % colCharsetLen, 1);

                // ساخت یک Sheet
                Worksheet excelSheet = (Worksheet)excelWorkbook.Sheets.Add(
                    excelWorkbook.Sheets.get_Item(++sheetIndex),
                    Type.Missing, 1, XlSheetType.xlWorksheet);
                //تنظیم نام شیت به نام دلخواه
                //excelSheet.Name = "List";
                //تنظیم خاصیت راست به چپ برای نمایش اطلاعات
                excelSheet.DisplayRightToLeft = false;


                // تعیین محدوده سطرها و ستونها
                string excelRange = string.Format("A1:{0}{1}", finalColLetter, rownum1 + 1);
                //انتقال اطلاعات از آرایه به شیت مورد نظر
                excelSheet.get_Range(excelRange, Type.Missing).Value2 = rawData;

                // ضخیم کردن اولین سطر برای عنوان ستونها
                ((Range)excelSheet.Rows[1, Type.Missing]).Font.Bold = true;

                // تنظیم عرض ستونها به اندازه محتوای ستونها
                for (int col = 0; col < colnum1; col++)
                {
                    ((Range)excelSheet.Columns[col + 1]).EntireColumn.AutoFit();
                }


                //ذخیره و بستن  Workbook
                excelWorkbook.SaveAs(outputPath, XlFileFormat.xlWorkbookNormal, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                excelWorkbook.Close(true, Type.Missing, Type.Missing);
                excelWorkbook = null;

                excelApp.Quit();
                excelApp = null;

                // Collect the unreferenced objects
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return "Saving in excel file is successfully done";
            }
            catch (Exception ex)
            {
                //بدست آوردن کد خطا برای مدیریت خطاها
                int code = System.Runtime.InteropServices.Marshal.GetExceptionCode();

                return ex.Message + code;
            }
        }
        private void toolStripButton10_Click_1(object sender, EventArgs e)
        {

            int shomarcheck = 0;
            for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                if (treeView3.Nodes[0].Nodes[i].Checked)
                {
                    shomarcheck++;
                }
            if (shomarcheck > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.AddExtension = true;
                sfd.DefaultExt = "xls";
                sfd.Filter = "Excel 97-2003 Workbook(*.xls)|*.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show(ExportToExcel(5, 1, sfd.FileName),"Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select one technique!!!");
            }
        }

     

    
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            chart4.Printing.PrintPreview();
        }

        private void toolStripButton8_Click_1(object sender, EventArgs e)
        {
            int i1 = 1, i2 = 1;
            if (chart4.Series.Count <= 0)
                return;
            if (chart4.Series[0].Points.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.AddExtension = true;
                sfd.DefaultExt = "txt";
                sfd.Filter = "Text document(*.txt)|*.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StreamWriter Fl = new StreamWriter(sfd.FileName, false, Encoding.ASCII);
                        i2++;

                        Fl.WriteLine(chart4.Titles[0].Text);
                        Fl.WriteLine("---------------------------------------------------");
                        Fl.WriteLine("row number      X value         Y value");
                        Fl.WriteLine("---------------------------------------------------");

                        for (int cc1 = 0; cc1 < chart4.Series[0].Points.Count; cc1++)
                        {
                            Fl.Write((cc1 + 1).ToString().Trim() + "\t\t");
                            Fl.Write(chart4.Series[0].Points[cc1].XValue.ToString() + "\t\t");
                            Fl.WriteLine(chart4.Series[0].Points[cc1].YValues[0]);
                            i2++;
                        }
                        i1++;
                        Fl.WriteLine("");




                        Fl.Close();
                        MessageBox.Show("Done!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }

            }
            else
            {
                MessageBox.Show("Please select one technique!!!");
            }
        }

        private void toolStripButton7_Click_1(object sender, EventArgs e)
        {
            try
            {
                
                if (chart4.Series.Count <= 0)
                    return;
                if (chart4.Series[0].Points.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.AddExtension = true;
                    sfd.DefaultExt = "xls";
                    sfd.Filter = "Excel 97-2003 Workbook(*.xls)|*.xls";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show(ExportToExcel(3, 2, sfd.FileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        private void btnshowvalue_Click(object sender, EventArgs e)
        {
            /*
            if (statebutton == "showvalue")
                statebutton = "";
            else
                statebutton = "showvalue";
            selectpchart_func(false);
            */
        }

        private void selectpchart_func(bool state)
        {       
            chart3.ChartAreas[0].CursorX.IsUserEnabled = state;
            chart3.ChartAreas[0].CursorY.IsUserEnabled = state;
            chart3.ChartAreas[0].CursorX.IsUserSelectionEnabled = state;
            chart3.ChartAreas[0].CursorY.IsUserSelectionEnabled = state;
            chart3.ChartAreas[0].CursorX.Interval = 0;
            chart3.ChartAreas[0].CursorY.Interval = 0;
            if (state)
            {
                chart3.ChartAreas[0].CursorX.LineWidth = 1;
                chart3.ChartAreas[0].CursorY.LineWidth = 1;
            }
            else
            {
                chart3.ChartAreas[0].CursorX.LineWidth = 0;
                chart3.ChartAreas[0].CursorY.LineWidth = 0;
            }
        }

        private void btnselectonly_Click(object sender, EventArgs e)
        {

            if (statebutton == "selectonly")
            {
                statebutton = "";
                selectpchart_func(false);
                btnselectonly.FlatAppearance.BorderSize = 0;
            }
            else
            {
                statebutton = "selectonly";
                selectpchart_func(true);
                btnselectonly.FlatAppearance.BorderSize = 1;
                btnselectonly.FlatAppearance.BorderColor = Color.Black;

            }
        }

       

        private void btnzoomin_Click(object sender, EventArgs e)
        {
            statebutton = "";
            selectpchart_func(false);
            this.chart3.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            double xMin = chart3.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            double xMax = chart3.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
            double yMin = chart3.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
            double yMax = chart3.ChartAreas[0].AxisY.ScaleView.ViewMaximum;
            chart3.ChartAreas[0].AxisX.ScaleView.Zoom(xMin, 0.95 * (xMax - xMin), DateTimeIntervalType.Auto, true);
            chart3.ChartAreas[0].AxisY.ScaleView.Zoom(yMin, 0.95 * (yMax - yMin), DateTimeIntervalType.Auto, true);
            difxview = chart3.ChartAreas[0].AxisX.ScaleView.ViewMaximum - chart3.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            difyview = chart3.ChartAreas[0].AxisY.ScaleView.ViewMaximum - chart3.ChartAreas[0].AxisY.ScaleView.ViewMinimum;            
        }

        private void btnzoomout_Click(object sender, EventArgs e)
        {
            statebutton_1 = "";
            selectpchart_func(false);
            this.chart3.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart3.ChartAreas[0].AxisX.ScaleView.ZoomReset(1);
            chart3.ChartAreas[0].AxisY.ScaleView.ZoomReset(1);
        }
               
        private void btnshowgrid_Click(object sender, EventArgs e)
        {
            statebutton = "";
            selectpchart_func(false);
            this.chart3.ChartAreas[0].AxisX.MajorGrid.Enabled = !this.chart3.ChartAreas[0].AxisX.MajorGrid.Enabled;
            this.chart3.ChartAreas[0].AxisY.MajorGrid.Enabled = !this.chart3.ChartAreas[0].AxisY.MajorGrid.Enabled;
          
        }

        private void btnshowaxes_Click(object sender, EventArgs e)
        {
            statebutton = "";
            selectpchart_func(false);
            chart3.ChartAreas[0].AxisX.LabelStyle.Enabled = !chart3.ChartAreas[0].AxisX.LabelStyle.Enabled;
            chart3.ChartAreas[0].AxisX.MajorTickMark.Enabled = !chart3.ChartAreas[0].AxisX.MajorTickMark.Enabled;
            chart3.ChartAreas[0].AxisY.LabelStyle.Enabled = !chart3.ChartAreas[0].AxisY.LabelStyle.Enabled;
            chart3.ChartAreas[0].AxisY.MajorTickMark.Enabled = !chart3.ChartAreas[0].AxisY.MajorTickMark.Enabled;
      
        }

        private void tChart1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                
                //var results11 = chart3.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                //foreach (var result in results11)
                //{
                //    if (result.ChartElementType == ChartElementType.PlottingArea)
                //    {

                //        switch (statebutton)
                //        {                        
                //            case "selectonly":
                //                chart3.Cursor = System.Windows.Forms.Cursors.Cross;
                //                break;
                //            case "showvalue":
                //                chart3.Cursor = System.Windows.Forms.Cursors.SizeAll;

                //                break;
                //            case "zoomin":

                //              //  chart3.Cursor =System.Windows.Forms.Cursors.Hand;// LoadCursorFromResource(Skydat.Properties.Resources.hard_disk_32);

                //                break;
                //            case "zoomout":
                //               // chart3.Cursor = LoadCursorFromResource(Skydat.Properties.Resources.Zoomout);

                //                break;
                //            default:
                //                chart3.Cursor = System.Windows.Forms.Cursors.Default;
                //                break;
                //        }

                //    }
                //    else
                //        chart3.Cursor = System.Windows.Forms.Cursors.Default;


                //}
                //switch (statebutton)
                //{               
                //    case "selectonly":
                //        System.Drawing.Point mousePoint = new System.Drawing.Point(e.X, e.Y);
                //        chart3.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, false);
                //        chart3.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, false);

                //        break;
                //    case "showvalue":
                //        var results12 = chart3.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                //        double x111 = 0, y111 = 0;
                //        foreach (var result in results12)
                //        {

                //            if (result.ChartElementType == ChartElemyentType.PlottingArea)
                //            {
                //                x111 = result.ChartArea.AxisX.PixelPositionToValue(e.X);
                //                y111 = result.ChartArea.AxisY.PixelPositionToValue(e.Y);
                //            }
                //        }
                //        mainf.xyLabel.Text = x111.ToString("#.##") + "," + y111.ToString("#.#e-0");

                //        break;
                //}
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            mDown = double.NaN;
            mDowny = double.NaN;
            //switch (statebutton)
            //{
               
            //    case "zoomin":
            //        var results11 = chart3.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
            //        foreach (var result in results11)
            //        {
            //            if (result.ChartElementType == ChartElementType.PlottingArea)
            //            {
            //                this.chart3.ChartAreas[0].AxisX.ScaleView.Zoomable = true;


            //                double xMin = chart3.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            //                double xMax = chart3.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
            //                double yMin = chart3.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
            //                double yMax = chart3.ChartAreas[0].AxisY.ScaleView.ViewMaximum;                             
            //                chart3.ChartAreas[0].AxisX.ScaleView.Zoom(xMin, 0.95 * (xMax-xMin), DateTimeIntervalType.Number, true);
            //                chart3.ChartAreas[0].AxisY.ScaleView.Zoom(yMin, 0.95 * (yMax-yMin), DateTimeIntervalType.Number, true);
            //            }
            //        }
            //        break;
            //    case "zoomout":
            //        var results12 = chart3.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
            //        foreach (var result in results12)
            //        {
            //            if (result.ChartElementType == ChartElementType.PlottingArea)
            //            {
            //                this.chart3.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            //                chart3.ChartAreas[0].AxisX.ScaleView.ZoomReset(1);
            //                chart3.ChartAreas[0].AxisY.ScaleView.ZoomReset(1);

            //            }
            //        }
            //        break;
            //}
        }
      

        private void showDataPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView3.SelectedNode.Index >= 0 && treeView3.SelectedNode.Level==1) {
                frmshowdata frm = new frmshowdata();
                frm.listView1.Items.Clear();
                for (int i = 0; i < chart3.Series[treeView3.SelectedNode.Index].Points.Count; i++)
                {
                    frm.listView1.Items.Add(chart3.Series[treeView3.SelectedNode.Index].Points[i].XValue + "          " + chart3.Series[treeView3.SelectedNode.Index].Points[i].YValues[0]);
                }
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmselectfiletype frm = new frmselectfiletype();
            frm.StartPosition = FormStartPosition.CenterScreen;
            int i1 = 0, shomarcheck = 0, filetype = 0;
            string strlegend = "";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                filetype = frm.filetype_select;
            }
           
            switch (filetype)
            {
                case 1:
                    toolStripButton9_Click_1(sender, e);
                    break;
                case 2:
                    toolStripButton10_Click_1(sender, e);
                    break;
                case 3:

                    double xminb=0, xmaxb=0, yminb=0, ymaxb=0,shomartypevi=0,shomartypevt=0,shomartypeit=0,shomartypeqt=0;
                    for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                        if (treeView3.Nodes[0].Nodes[i].Checked)
                        {
                            if (classglobal.HAxisTitle[int.Parse(dataSet12.analyselist.Rows[i][0].ToString())].Substring(0, 1) == "P" && classglobal.VAxisTitle[int.Parse(dataSet12.analyselist.Rows[i][0].ToString())].Substring(0, 1) == "C")
                                shomartypevi++;
                            else if (classglobal.HAxisTitle[int.Parse(dataSet12.analyselist.Rows[i][0].ToString())].Substring(0, 1) == "T" && classglobal.VAxisTitle[int.Parse(dataSet12.analyselist.Rows[i][0].ToString())].Substring(0, 1) == "C")
                                shomartypeit++;
                            else if (classglobal.HAxisTitle[int.Parse(dataSet12.analyselist.Rows[i][0].ToString())].Substring(0, 1) == "T" && classglobal.VAxisTitle[int.Parse(dataSet12.analyselist.Rows[i][0].ToString())].Substring(0, 1) == "P")
                                shomartypevt++;
                            else if (classglobal.HAxisTitle[int.Parse(dataSet12.analyselist.Rows[i][0].ToString())].Substring(0, 1) == "T" && classglobal.VAxisTitle[int.Parse(dataSet12.analyselist.Rows[i][0].ToString())].Substring(0, 1) == "Q")
                                shomartypeqt++;
                          
                            shomarcheck++;
                            i1 = i;
                            if (shomarcheck == 0)
                            {
                                MessageBox.Show("EXPORTING works with one graph" + (char)13 + "Please select ONE !!!");
                            
                            }
                            else if (shomarcheck == 1)
                            {
                                xminb = chart3.Series[i].Points.FindMinByValue("X").XValue;
                                xmaxb = chart3.Series[i].Points.FindMaxByValue("X").XValue;
                                yminb = chart3.Series[i].Points.FindMinByValue("Y").YValues[0];
                                ymaxb = chart3.Series[i].Points.FindMaxByValue("Y").YValues[0];

                            }
                            else
                            {
                                if (xminb > chart3.Series[i].Points.FindMinByValue("X").XValue)
                                    xminb = chart3.Series[i].Points.FindMinByValue("X").XValue;
                                if (xmaxb < chart3.Series[i].Points.FindMaxByValue("X").XValue)
                                    xmaxb = chart3.Series[i].Points.FindMaxByValue("X").XValue;
                                if (yminb > chart3.Series[i].Points.FindMinByValue("Y").YValues[0])
                                    yminb = chart3.Series[i].Points.FindMinByValue("Y").YValues[0];
                                if (ymaxb < chart3.Series[i].Points.FindMaxByValue("Y").YValues[0])
                                    ymaxb = chart3.Series[i].Points.FindMaxByValue("Y").YValues[0];
                            }

                        }
                    string titlegraph = "";
                    if (shomartypevi > 0 && shomartypevt == 0 && shomartypeqt == 0 && shomartypeit == 0)
                        titlegraph = "IV";
                    else if (shomartypevi == 0 && (shomartypevt > 0 && shomartypeqt == 0 && shomartypeit == 0))
                        titlegraph = "VT";
                    else if (shomartypevi == 0 && (shomartypevt == 0 && shomartypeqt > 0 && shomartypeit == 0))
                        titlegraph = "QT";
                    else if (shomartypevi == 0 && (shomartypevt == 0 && shomartypeqt == 0 && shomartypeit > 0))
                        titlegraph = "IT";
                    if (shomarcheck == 1)
                    {
                        strlegend = fillparam(2, i1);
                        frmshowgraph frm1 = new frmshowgraph(strlegend, xminb, xmaxb, yminb, ymaxb,titlegraph,"");
                        if (frm1.Chart1.Series.Count > 0)
                            frm1.Chart1.Series[0].Points.Clear();
                        for (int i = 0; i < chart3.Series[i1].Points.Count; i++)
                        {
                            frm1.Chart1.Series[0].Points.AddXY(chart3.Series[i1].Points[i].XValue, chart3.Series[i1].Points[i].YValues[0]);
                        }
                        frm1.StartPosition = FormStartPosition.CenterScreen;
                        frm1.ShowDialog();
                    }
                    else
                    {
                        // MessageBox.Show("EXPORTING works with one graph" + (char)13 + "Please select ONE only!!!");
                        strlegend = "";

                        frmshowgraph frm2 = new frmshowgraph(strlegend, xminb, xmaxb, yminb, ymaxb,titlegraph,"");
                        if (frm2.Chart1.Series.Count > 0)
                        {
                            frm2.Chart1.Series[0].Points.Clear();
                            if (dataSet12.analyselist.Rows[0]["comment"].ToString().Trim() != "")
                                frm2.Chart1.Series[0].LegendText = classglobal.TechName[int.Parse(dataSet12.analyselist.Rows[0][0].ToString())] + "(" + dataSet12.analyselist.Rows[0]["comment"].ToString().Trim() + ")";
                            else
                                frm2.Chart1.Series[0].LegendText = classglobal.TechName[int.Parse(dataSet12.analyselist.Rows[0][0].ToString())];

                        }
                        int b = 0;
                        for (int i = 0; i < treeView3.Nodes[0].Nodes.Count; i++)
                            if (treeView3.Nodes[0].Nodes[i].Checked)
                            {
                                if (frm2.Chart1.Series.Count <= b)
                                {
                                    frm2.Chart1.Series.Add(treeView3.Nodes[0].Nodes[i].Name);
                                    frm2.Chart1.Series[b].ChartType = SeriesChartType.FastLine;
                                    if (dataSet12.analyselist.Rows[i]["comment"].ToString().Trim() != "")
                                        frm2.Chart1.Series[b].LegendText = classglobal.TechName[int.Parse(dataSet12.analyselist.Rows[i][0].ToString())] + "(" + dataSet12.analyselist.Rows[i]["comment"].ToString().Trim() + ")";
                                    else
                                        frm2.Chart1.Series[b].LegendText = classglobal.TechName[int.Parse(dataSet12.analyselist.Rows[i][0].ToString())];

                                }
                                for (int i11 = 0; i11 < chart3.Series[i].Points.Count; i11++)
                                {
                                    frm2.Chart1.Series[b].Points.AddXY(chart3.Series[i].Points[i11].XValue, chart3.Series[i].Points[i11].YValues[0]);
                                }
                                b++;

                            }
                        frm2.StartPosition = FormStartPosition.CenterScreen;
                        frm2.ShowDialog();
                    }
                    break;
                default:
                    break;

            }
        }

        private void btnexport_1_Click(object sender, EventArgs e)
        {
            statebutton_1 = "";
            selectpchart_func(false);
            frmselectfiletype frm = new frmselectfiletype();
            frm.StartPosition = FormStartPosition.CenterScreen;
            int i1 = 0, shomarcheck = 0, filetype = 0;           
            if (frm.ShowDialog() == DialogResult.OK)
            {
                filetype = frm.filetype_select;
            }

            switch (filetype)
            {
                case 1:
                    toolStripButton8_Click_1(sender, e);
                    break;
                case 2:
                    toolStripButton7_Click_1(sender, e);
                    break;
                case 3:
                    if (chart4.Series.Count>0 && chart4.Series[0].Points.Count > 0)
                        shomarcheck = 1;
                    i1 = 0;                    
                    if (shomarcheck == 1)
                    {
                        string strlegend = fillparam(2, nindexchart2);
                        frmshowgraph frm1 = new frmshowgraph(strlegend.Trim(), chart4.Series[i1].Points.FindMinByValue("X").XValue, chart4.Series[i1].Points.FindMaxByValue("X").XValue, chart4.Series[i1].Points.FindMinByValue("Y").YValues[0], chart4.Series[i1].Points.FindMaxByValue("Y").YValues[0], "", chart4.Titles[0].Text.Trim());
                        if (frm1.Chart1.Series.Count > 0)
                            frm1.Chart1.Series[0].Points.Clear();
                        for (int i = 0; i < chart4.Series[i1].Points.Count; i++)
                        {
                            frm1.Chart1.Series[0].Points.AddXY(chart4.Series[i1].Points[i].XValue, chart4.Series[i1].Points[i].YValues[0]);
                        }
                        frm1.StartPosition = FormStartPosition.CenterScreen;
                        frm1.ShowDialog();
                    }
                    else
                        MessageBox.Show("EXPORTING works with one graph" + (char)13 + "Please select ONE only!!!");


                    break;
                default:
                    break;

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            numline++;
            if (numline >= drawline1.Lines.Count)
                numline = 0;
//            MessageBox.Show("numline=" + numline.ToString());
//            MessageBox.Show("drawline1.Lines.Count=" + drawline1.Lines.Count.ToString());


            if (drawline1.Lines.Count == 0)
            {
                MessageBox.Show("There is not any line for selecting !!!");

            }

            chart4.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (drawline1.Lines.Count == 0)
            {
                MessageBox.Show("There is not any line for deleting !!!");


            }
            if (numline > -1 && numline < drawline1.Lines.Count)
            {
                drawline1.remove(numline);
                numline--;
                chart4.Refresh();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (drawline1.Lines.Count != 2)
            {
                MessageBox.Show("the number of line must be equal to 2 ." + Environment.NewLine + "please try again.");
            }
            else
            {
                drawline1.removeall_master();
                conv_pixel_cord(drawline1.Lines[0].point1.X, drawline1.Lines[0].point1.Y);
                conv_pixel_cord(drawline1.Lines[0].point2.X, drawline1.Lines[0].point2.Y);
                conv_pixel_cord(drawline1.Lines[1].point1.X, drawline1.Lines[1].point1.Y);
                conv_pixel_cord(drawline1.Lines[1].point2.X, drawline1.Lines[1].point2.Y);

                if (!drawline1.intersect_line())
                {
                    MessageBox.Show("there is not intersection point ." + Environment.NewLine + "please try again.");

                }
                else
                {
                    double slop1 = (drawline1.Lines_master[1].pointy - drawline1.Lines_master[0].pointy) / (drawline1.Lines_master[1].pointx - drawline1.Lines_master[0].pointx);
                    double slp1 = 1 / Math.Abs(slop1);
                    double slop2 = (drawline1.Lines_master[3].pointy - drawline1.Lines_master[2].pointy) / (drawline1.Lines_master[3].pointx - drawline1.Lines_master[2].pointx);
                    double slp2 = 1 / Math.Abs(slop2);
                    if (tech == 10)
                    {
                        slop2 = drawline1.Lines_master[0].pointx - Math.Abs((drawline1.Lines_master[2].pointx + (drawline1.Lines_master[2].pointy - drawline1.Lines_master[0].pointy) / slop1));
                        label4.Text += Environment.NewLine + "T(s)= " + slop2;
                        label4.Text += Environment.NewLine + "I* T 1/2="; //+Math.Sqrt(slop2)*Math.Abs(IE/1000.0);
                    }
                    if (slop1 != 0 && slop2 != 0)
                    {
                        double xx = (drawline1.Lines_master[2].pointy - drawline1.Lines_master[0].pointy + slop1 * drawline1.Lines_master[0].pointx - slop2 * drawline1.Lines_master[2].pointx) / (slop1 - slop2);
                        chart4.Titles[0].Text = "Tafel Results :      ";
                        chart4.Titles[0].Text += "Ba=" + slp1.ToString("#0.00") + "        Bc=" + slp2.ToString("#0.00");
                    }
                }
            }
        }

        private void btnshowval_1_Click(object sender, EventArgs e)
        {/*
            if (statebutton_1 == "showvalue")
            {
                statebutton_1 = "";
                selectpchart_func_1(false);
            }
            else
            {
                statebutton_1 = "showvalue";
                selectpchart_func_1(true);
            }
            */
        }

        private void selectpchart_func_1(bool state)
        {
            chart4.ChartAreas[0].CursorX.IsUserEnabled = state;
            chart4.ChartAreas[0].CursorY.IsUserEnabled = state;
            chart4.ChartAreas[0].CursorX.IsUserSelectionEnabled = state;
            chart4.ChartAreas[0].CursorY.IsUserSelectionEnabled = state;
            chart4.ChartAreas[0].CursorX.Interval = 0;
            chart4.ChartAreas[0].CursorY.Interval = 0;
            if (state)
            {
                chart4.ChartAreas[0].CursorX.LineWidth = 1;
                chart4.ChartAreas[0].CursorY.LineWidth = 1;
            }
            else
            {
                chart4.ChartAreas[0].CursorX.LineWidth = 0;
                chart4.ChartAreas[0].CursorY.LineWidth = 0;
            }
            chart4.Focus();
        }

        private void btnselectonly_1_Click(object sender, EventArgs e)
        {
            if (statebutton_1 == "selectonly")
            {
                statebutton_1 = "";
                selectpchart_func_1(false);
                btnselectonly_1.FlatAppearance.BorderSize = 0;
            }
            else
            {
                statebutton_1 = "selectonly";
                selectpchart_func_1(true);
                btnselectonly_1.FlatAppearance.BorderSize = 1;
                btnselectonly_1.FlatAppearance.BorderColor = Color.Black;

            }
        }

        private void toolstripChart_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog cl = new ColorDialog();
                cl.Color = chart3.ChartAreas[0].BackColor;
                cl.CustomColors = new int[] { chart3.ChartAreas[0].BackColor.R, chart3.ChartAreas[0].BackColor.G, chart3.ChartAreas[0].BackColor.B };
                cl.FullOpen = true;

                if (cl.ShowDialog() == DialogResult.OK)
                {
                    chart3.BackColor = cl.Color;
                    chart3.BorderlineColor = cl.Color;
                    chart3.ChartAreas[0].BackColor = cl.Color;
                    chart4.BackColor = cl.Color;
                    chart4.BorderlineColor = cl.Color;
                    chart4.ChartAreas[0].BackColor = cl.Color;
                    panel1.BackColor = cl.Color;
                    panel2.BackColor = cl.Color;
                    panel8.BackColor = cl.Color;

                    string[] color = File.ReadAllLines(System.Windows.Forms.Application.StartupPath + "\\Color.dat");
                    color[1] = "2065 analize:" + cl.Color.ToArgb().ToString();
                    File.WriteAllLines(System.Windows.Forms.Application.StartupPath + "\\Color.dat", color);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }


        private void button5_Click_1(object sender, EventArgs e)
        {
            new_handle();
            button6.Enabled = true;
        }

        public void new_handle()
        {
            try
            {
                Array.Clear(series_height_index, 0, series_height_index.Length);//جهت نگهداری اندیس سری هایی از چارت که مربوط با ارتفاع است برای نمایش چک باکس
                Array.Resize(ref series_height_index, 0);
                Array.Clear(series_width_index, 0, series_width_index.Length);
                Array.Resize(ref series_width_index, 0);
                Array.Clear(series_area_index, 0, series_area_index.Length);
                Array.Resize(ref series_area_index, 0);

                chart4.Series.Clear();//پاک کردن گرافها
                chart4.Refresh();

                if (chart3.Series.Count > 0)
                {
                    int j = -1;
                    for (int i = 0; i < this.treeView3.Nodes[0].Nodes.Count; i++)
                        if (this.treeView3.Nodes[0].Nodes[i].Checked == true)//فقط اولین سری که تیک خورده باشد انتخاب می شود
                        {
                            j = i;

                            for (int k = i + 1; k < this.treeView3.Nodes[0].Nodes.Count; k++)//غیر تیک کردن بقیه گرافها
                                this.treeView3.Nodes[0].Nodes[k].Checked = false;

                            TreeNode sel = treeView3.Nodes[0].Nodes[i];//انتخاب گراف تیک دار
                            treeView3.SelectedNode = sel;
                            treeView3.Focus();

                            break;
                        }

                    if (j == -1)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("A graph of the list needs to be checked..", "Error");
                        return;
                    }

                    radioButton1.Checked = true;

                    Array.Clear(volt, 0, volt.Length);
                    Array.Clear(current, 0, current.Length);

                    Counter_num = chart3.Series[j].Points.Count;

                    Array.Resize(ref volt, Counter_num);
                    Array.Resize(ref current, Counter_num);


                    if (chart4.Series.Count == 0)
                    {
                        txt_res.Text = (Convert.ToInt16(Counter_num / 20)).ToString();//این عدد تجربی است یعنی حالتی است که بیشتر قله ها گرفته میشود
                        txt_noise.Text = "0";
                    }

                    chart4.Series.Clear();//پاک کردن گرافها
                    chart4.Refresh();

                    Thread.Sleep(100);//جهت متوجه شدن کاربر

                    chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());
                    chart4.Series[0].ChartType = SeriesChartType.Spline;
                    chart4.Series[0].Color = chart3.Series[0].Color;
                    chart4.Series[0].BorderWidth = 2;
                    chart4.Titles[0].Text = "Peak Search";
                    chart4.ChartAreas[0].AxisY.Title = chart3.ChartAreas[0].AxisY.Title;
                    chart4.ChartAreas[0].AxisY.Title = chart3.ChartAreas[0].AxisX.Title;

                    if (dataSet12.analyselist.Rows[j][0].ToString() == "10" || dataSet12.analyselist.Rows[j][0].ToString() == "11" || dataSet12.analyselist.Rows[j][0].ToString() == "12")
                        chart4.Series[0].Points.AddXY(0, othertech_val1[0]);//رسم در نقطه صفر زمان // technics chp cha chc

                    for (int i = 0; i < Counter_num; i++)
                    {
                        volt[i] = chart3.Series[j].Points[i].XValue;
                        current[i] = chart3.Series[j].Points[i].YValues[0];

                        chart4.Series[0].Points.AddXY(volt[i], current[i]);
                    }

                }
                else
                    MessageBox.Show("graph is not available.");
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }

            this.Cursor = Cursors.Default;
            chart4.Cursor = Cursors.Default;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            
        }
                 

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_peak_search.Visible = true;

                if (chart4.Series.Count == 0)
                {
                    txt_res.Text = (chart3.Series[0].Points.Count / 20).ToString();//این عدد تجربی است
                    txt_noise.Text = "0";
                }
            }
            catch { }
        }

        int start_click = 0;
        int end_click = 0;
        private void button6_Click(object sender, EventArgs e)
        {
            if (chart4.Series.Count == 1)
            {
                chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());

                start_click = 1;
                button6.Enabled = false;

                this.Cursor = Cursors.UpArrow;
                chart4.Cursor = Cursors.UpArrow;
            }
            else
                MessageBox.Show("You must first click the new button.", "Error", MessageBoxButtons.OK);
        }

      
        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (chart4.Series.Count == 2 && chart4.Series[1].Points.Count == 2)
                {
                    this.Cursor = Cursors.WaitCursor;

                    Thread.Sleep(100);//جهت متوجه شدن کاربر

                    int ser = 2;

                    //============================ شروع عملیات                             

                    Array.Clear(series_height_index, 0, series_height_index.Length);//جهت نگهداری اندیس سری هایی از چارت که مربوط با ارتفاع است برای نمایش چک باکس
                    Array.Resize(ref series_height_index, 0);
                    Array.Clear(series_width_index, 0, series_width_index.Length);
                    Array.Resize(ref series_width_index, 0);
                    Array.Clear(series_area_index, 0, series_area_index.Length);
                    Array.Resize(ref series_area_index, 0);



                    int forward_count = Counter_num;//برای گراف سی وی که رفت و برگشت دارد این رفت
                    
                    for (int i = 1; i < Counter_num; i++)
                    {
                        if (volt[i] < volt[i - 1])//برای تکنیک سی وی هنگام برگشت
                        {
                            forward_count = i - 1;
                         
                            break;
                        }
                    }

                    bool is_cv = false;//سی وی است یا خیر
                    if (forward_count < Counter_num)
                        is_cv = true;


                    double start_peak_x = chart4.Series[1].Points[0].XValue;
                    double start_peak_y = chart4.Series[1].Points[0].YValues[0];

                    double end_peak_x = chart4.Series[1].Points[1].XValue;
                    double end_peak_y = chart4.Series[1].Points[1].YValues[0];


                    //======== پیدا کردن اندیس شروع قله که کاربر انتخاب کرده است

                    int start_index = 0;

                    int contin = 0;

                    for (int i = 0; i < Counter_num; i++)
                    {
                        if ((volt[i] == start_peak_x && current[i] == start_peak_y) || (volt[i] == end_peak_x && current[i] == end_peak_y))//در صورتی که نقطه اول و دوم را برعکس انتخاب کرده است اینجا تصحیح شود
                        {
                            start_index = i;
                            contin = i;

                            break;
                        }
                    }

                    //======== پیدا کردن اندیس انتهای قله که کاربر انتخاب کرده است

                    int end_index = 0;
                    for (int i = contin + 1; i < Counter_num; i++)
                    {
                        if ((volt[i] == end_peak_x && current[i] == end_peak_y) || (volt[i] == start_peak_x && current[i] == start_peak_y))
                        {
                            end_index = i;

                            break;
                        }
                    }


                    if (is_cv == false)
                    {
                        //===================== گرفتن ماکس این قله

                        int maxs = 0;
                        double max = current[start_index];

                        for (int i = start_index; i < end_index; i++)
                        {
                            if (current[i + 1] >= max)
                            {
                                max = current[i + 1];

                                maxs = i + 1;
                            }
                        }

                        //بدست آوردن همه ارتفاع های خالص از اول پیک تا آخر پیک و پیدا کردن بلندترین آن

                        double x11 = volt[start_index], y11 = current[start_index], x22 = volt[end_index], y22 = current[end_index];
                        double shib32 = (y22 - y11) / (x22 - x11);//   (y2-y1) / (x2-x1)فرمول شیب خط
                        double x32, y32;

                        int peak = 0;
                        double max_jarian_khales = 0;

                        //==فقط جریان خالص ماکس
                        peak = maxs;
                        x32 = volt[maxs];
                        y32 = (shib32 * x32) - (shib32 * x11) + y11;//فرمول معادله خط
                        max_jarian_khales = current[maxs] - y32;
                        if (max_jarian_khales < 0)//برای مثبت کردن جریان خالص یا همان ارتفاع زیر پیک
                            max_jarian_khales = max_jarian_khales * (-1);
                        

                        if (max_jarian_khales == 0)//اگر یک پیکی گرفت اما ارتفاعش 0 بود اصلا چیزی رسم نکند
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("The height of this peak is zero.");
                            return;
                        }


                        ////=========== بدست آوردن نقطه پایین ارتفاع پیک در ناحیه قطع بیس لاین

                        double base_peak = start_peak_x;

                        double x13 = volt[start_index], y13 = current[start_index], x23 = volt[end_index], y23 = current[end_index];
                        double shib3 = (y23 - y13) / (x23 - x13);//   (y2-y1) / (x2-x1)فرمول شیب خط
                        double x3, y3;
                                               
                        x3 = volt[peak];
                        y3 = (shib3 * x3) - (shib3 * x13) + y13;//فرمول معادله خط
                        base_peak = y3;


                        ////===================================//پیدا کردن شروع نقطه پهنا در سمت چپ گراف

                        int find_half = 0;

                        double nesf = base_peak + (max_jarian_khales / 2);
                        double p1 = volt[start_index], p2 = volt[end_index];

                        for (int k = peak; k > start_index; k--)
                        {
                            if (current[k] <= nesf)
                            {
                                if (current[k] < nesf)//در صورتی که مقدار نصف با مقدار جریان در ناحیه مرزی گراف مساوی نباشد یعنی بین دو نقطه باشد
                                {
                                    double x130 = volt[k], y130 = current[k], x230 = volt[k + 1], y230 = current[k + 1];
                                    double shib30 = (y230 - y130) / (x230 - x130);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                    double x30, y30 = nesf;

                                    x30 = ((shib30 * x130) + (y30 - y130)) / shib30;//فرمول معادله خط

                                    p1 = x30;
                                }
                                else
                                    p1 = volt[k];// اگر مساوی بود که نیاز به محاسبه نیست


                                find_half = 1;

                                break;
                            }
                        }


                        if (find_half == 0)//اگر گراف در سمت چپ پیک نصف پیدا نکرد پس این پیک اشتباه گرفته شده است
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("The peak on the left is not half the height .");
                            return;
                        }

                        ////============//پیدا کردن شروع نقطه پهنا در سمت راست گراف


                        find_half = 0;

                        for (int k = peak; k < end_index; k++)
                        {
                            if (current[k] <= nesf)
                            {
                                if (current[k] < nesf)//در صورتی که مقدار نصف با مقدار جریان در ناحیه مرزی گراف مساوی نباشد
                                {
                                    double x130 = volt[k], y130 = current[k], x230 = volt[k - 1], y230 = current[k - 1];
                                    double shib30 = (y230 - y130) / (x230 - x130);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                    double x30, y30 = nesf;

                                    x30 = ((shib30 * x130) + (y30 - y130)) / shib30;//فرمول معادله خط

                                    p2 = x30;
                                }
                                else
                                    p2 = volt[k];// اگر مساوی بود که نیاز به محاسبه نیست


                                find_half = 1;

                                break;
                            }
                        }

                        if (find_half == 0)//اگر گراف در سمت راست پیک نصف پیدا نکرد پس این پیک اشتباه گرفته شده است
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("The peak on the right is not half the height .");
                            return;
                        }



                        //======== محاسبه مساحت زیر گراف

                        //رابطه بدست آمده از انتگرال
                        //  aria = (t2 - t1)[( f(t1) + f(t2) ) / 2]

                        double aria = 0, t = 0, ft = 0, ft1 = 0, ft2 = 0;

                        double xx1 = volt[start_index], yy1 = current[start_index], xx2 = volt[end_index], yy2 =current[end_index];
                        double shibb = (yy2 - yy1) / (xx2 - xx1);//   (y2-y1) / (x2-x1)فرمول شیب خط
                        double xx, yy;

                        ////(y2 - y1) = shib(x2 - x1)            فرمول معادله خط
                        ////y2 - y1 = (shib * x2) - (shib * x1)
                        ////y2 = (shib * x2) - (shib * x1) + y1

                        for (int k = start_index + 1; k <= end_index; k++)
                        {
                            xx = volt[k - 1];
                            yy = (shibb * xx) - (shibb * xx1) + yy1;//فرمول معادله خط برای بدست آوردن جریان معادل در بیس لاین
                            ft1 = current[k - 1] - yy;//هر نقطه از جریان منهای همان نقطه در بیس لاین می شود تا جریان خالص زیر پیک در هر نقطه بدست آید

                            if (ft1 < 0) ft1 = 0;//قاعدتا جریان در یک نقطه باید از بیس لاین در همان نقطه بیشتر باشد در غیر این صورت این نقطه از بیس لاین خارج از محدوده است

                            xx = volt[k];
                            yy = (shibb * xx) - (shibb * xx1) + yy1;//فرمول معادله خط
                            ft2 = current[k] - yy;

                            if (ft2 < 0) ft2 = 0;

                            t = volt[k] - volt[k - 1];
                            ft = (ft1 + ft2) / 2;
                            aria = aria + (t * ft);
                        }
                      
                        //============


                        double pahna_dar_nesf = p2 - p1;
                        if (pahna_dar_nesf < 0) pahna_dar_nesf = (-1) * pahna_dar_nesf;//مثبت کردن اندازه پهنا

                        chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم بیس لاین
                        chart4.Series[ser].ChartType = SeriesChartType.Spline;
                        chart4.Series[ser].Color = Color.Red;
                        chart4.Series[ser].BorderWidth = 1;
                        chart4.Series[ser].Points.AddXY(volt[start_index], current[start_index]);
                        chart4.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                        ser++;


                        string a = aria.ToString();
                        try { a = a.Substring(0, a.IndexOf('.') + 6); } catch { }
                        chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//نمایش اندازه مساحت
                        chart4.Series[ser].ChartType = SeriesChartType.Spline;
                        chart4.Series[ser].Color = Color.Red;
                        chart4.Series[ser].BorderWidth = 1;
                        chart4.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                        chart4.Series[ser].Points[0].Label = "Aria: " + a;
                        Array.Resize(ref series_area_index, series_area_index.Length + 1);//قرار دادن اندیس برای نمایش با چک باکس
                        series_area_index[series_area_index.Length - 1] = ser;
                        ser++;


                        string height = max_jarian_khales.ToString();
                        try { height = height.Substring(0, height.IndexOf('.') + 4); } catch { }
                        chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم ارتفاع یا همان جریان خالص
                        chart4.Series[ser].ChartType = SeriesChartType.Spline;
                        chart4.Series[ser].Color = Color.Gold;
                        chart4.Series[ser].BorderWidth = 1;
                        chart4.Series[ser].Points.AddXY(volt[peak], current[peak]);
                        chart4.Series[ser].Points.AddXY(volt[peak], base_peak);
                        chart4.Series[ser].Points[0].Label = "H: " + height;
                        Array.Resize(ref series_height_index, series_height_index.Length + 1);
                        series_height_index[series_height_index.Length - 1] = ser;
                        ser++;


                        string w = pahna_dar_nesf.ToString();
                        try { w = w.Substring(0, w.IndexOf('.') + 4); } catch { }
                        chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم پهنا در نصف ارتفاع
                        chart4.Series[ser].ChartType = SeriesChartType.Spline;
                        chart4.Series[ser].Color = Color.SpringGreen;
                        chart4.Series[ser].BorderWidth = 1;
                        chart4.Series[ser].Points.AddXY(p1, nesf);
                        chart4.Series[ser].Points.AddXY(p2, nesf);
                        chart4.Series[ser].Points[0].Label = "W: " + w;
                        Array.Resize(ref series_width_index, series_width_index.Length + 1);
                        series_width_index[series_width_index.Length - 1] = ser;
                    }
                    else//cv
                    {
                        double pahna_dar_nesf = 0, max_jarian_khales = 0, base_peak = 0, nesf = 0, p1 = 0, p2 = 0, aria = 0;
                        int peak = 0;
                        string cv_direct = "";

                        if (start_index < forward_count)//cv رفت
                        {
                            double x11 = volt[start_index], y11 = current[start_index], x22 = volt[end_index], y22 = current[end_index];
                            double shib32 = (y22 - y11) / (x22 - x11);//   (y2-y1) / (x2-x1)فرمول شیب خط
                            double x32, y32;
                            cv_direct = "Aria Forward: ";

                            x32 = volt[forward_count];
                            y32 = (shib32 * x32) - (shib32 * x11) + y11;//فرمول معادله خط
                            chart4.Series[1].Points.AddXY(x32, y32);// رسم ادامه بیس لاین

                            //===================== گرفتن ماکس این قله

                            peak = 0;
                            double max = current[end_index];

                            for (int i = end_index; i < forward_count; i++)
                            {
                                if (current[i + 1] >= max)
                                {
                                    max = current[i + 1];
                                    peak = i + 1;
                                }
                            }

                            x32 = volt[peak];
                            base_peak = (shib32 * x32) - (shib32 * x11) + y11;//فرمول معادله خط


                            max_jarian_khales = current[peak] - base_peak;
                            if (max_jarian_khales < 0)//برای مثبت کردن جریان خالص یا همان ارتفاع زیر پیک
                                max_jarian_khales = max_jarian_khales * (-1);



                            ////===================================//پیدا کردن شروع نقطه پهنا در سمت چپ گراف

                            int find_half = 0;

                            nesf = base_peak + (max_jarian_khales / 2);
                            p1 = volt[start_index]; p2 = volt[end_index];

                            for (int k = peak; k > start_index; k--)
                            {
                                if (current[k] <= nesf)
                                {
                                    if (current[k] < nesf)//در صورتی که مقدار نصف با مقدار جریان در ناحیه مرزی گراف مساوی نباشد یعنی بین دو نقطه باشد
                                    {
                                        double x130 = volt[k], y130 = current[k], x230 = volt[k + 1], y230 = current[k + 1];
                                        double shib30 = (y230 - y130) / (x230 - x130);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                        double x30, y30 = nesf;

                                        x30 = ((shib30 * x130) + (y30 - y130)) / shib30;//فرمول معادله خط

                                        p1 = x30;
                                    }
                                    else
                                        p1 = volt[k];// اگر مساوی بود که نیاز به محاسبه نیست


                                    find_half = 1;

                                    break;
                                }
                            }


                            if (find_half == 0)//اگر گراف در سمت چپ پیک نصف پیدا نکرد پس این پیک اشتباه گرفته شده است
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("The peak on the left is not half the height .");
                                return;
                            }


                            ////============//پیدا کردن شروع نقطه پهنا در سمت راست گراف


                            find_half = 0;

                            for (int k = peak; k < Counter_num; k++)
                            {
                                if (current[k] <= nesf)
                                {
                                    if (current[k] < nesf)//در صورتی که مقدار نصف با مقدار جریان در ناحیه مرزی گراف مساوی نباشد
                                    {
                                        double x130 = volt[k], y130 = current[k], x230 = volt[k - 1], y230 = current[k - 1];
                                        double shib30 = (y230 - y130) / (x230 - x130);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                        double x30, y30 = nesf;

                                        x30 = ((shib30 * x130) + (y30 - y130)) / shib30;//فرمول معادله خط

                                        p2 = x30;
                                    }
                                    else
                                        p2 = volt[k];// اگر مساوی بود که نیاز به محاسبه نیست


                                    find_half = 1;

                                    break;
                                }
                            }

                            if (find_half == 0)//اگر گراف در سمت راست پیک نصف پیدا نکرد پس این پیک اشتباه گرفته شده است
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("The peak on the right is not half the height .");
                                return;
                            }


                            pahna_dar_nesf = p2 - p1;
                            if (pahna_dar_nesf < 0) pahna_dar_nesf = (-1) * pahna_dar_nesf;//مثبت کردن اندازه پهنا


                            //======== محاسبه مساحت زیر گراف

                            //رابطه بدست آمده از انتگرال
                            //
                            //  aria = (t2 - t1)[( f(t1) + f(t2) ) / 2]

                            double t = 0, ft = 0, ft1 = 0, ft2 = 0;

                            double xx1 = volt[start_index], yy1 = current[start_index], xx2 = volt[end_index], yy2 = current[end_index];
                            double shibb = (yy2 - yy1) / (xx2 - xx1);//   (y2-y1) / (x2-x1)فرمول شیب خط
                            double xx, yy;
                                                      
                            for (int k = start_index + 1; k <= forward_count; k++)
                            {
                                xx = volt[k - 1];
                                yy = (shibb * xx) - (shibb * xx1) + yy1;//فرمول معادله خط برای بدست آوردن جریان معادل در بیس لاین
                                ft1 = current[k - 1] - yy;//هر نقطه از جریان منهای همان نقطه در بیس لاین می شود تا جریان خالص زیر پیک در هر نقطه بدست آید

                                if (ft1 < 0) ft1 = 0;//قاعدتا جریان در یک نقطه باید از بیس لاین در همان نقطه بیشتر باشد در غیر این صورت این نقطه از بیس لاین خارج از محدوده است

                                xx = volt[k];
                                yy = (shibb * xx) - (shibb * xx1) + yy1;//فرمول معادله خط
                                ft2 = current[k] - yy;

                                if (ft2 < 0) ft2 = 0;

                                t = volt[k] - volt[k - 1];
                                ft = (ft1 + ft2) / 2;
                                aria = aria + (t * ft);
                            }
        
                        }
                        //================================================
                        else  //برگشت cv
                        //================================================
                        {                           
                            double x11 = volt[start_index], y11 = current[start_index], x22 = volt[end_index], y22 = current[end_index];
                            double shib32 = (y22 - y11) / (x22 - x11);//   (y2-y1) / (x2-x1)فرمول شیب خط
                            double x32 = 0, y32 = 0;
                            cv_direct = "Aria Backward: ";

                            x32 = volt[0];
                            y32 = (shib32 * x32) - (shib32 * x11) + y11;//فرمول معادله خط
                            chart4.Series[1].Points.AddXY(x32, y32);// رسم ادامه بیس لاین


                            //===================== گرفتن ماکس این قله

                            peak = 0;
                            double max = current[start_index];

                            for (int i = end_index; i < Counter_num - 1; i++)
                            {
                                if (current[i + 1] <= max)
                                {
                                    max = current[i + 1];
                                    peak = i + 1;
                                }
                            }

                            //بدست آوردن ارتفاع  خالص 

                            max_jarian_khales = 0;

                            x32 = volt[peak];
                            base_peak = (shib32 * x32) - (shib32 * x11) + y11;//فرمول معادله خط

                            max_jarian_khales = current[peak] - base_peak;
                            if (max_jarian_khales < 0)//برای مثبت کردن جریان خالص یا همان ارتفاع زیر پیک
                                max_jarian_khales = max_jarian_khales * (-1);



                            if (max_jarian_khales == 0)//اگر یک پیکی گرفت اما ارتفاعش 0 بود اصلا چیزی رسم نکند
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("The height of this peak is zero.");
                                return;
                            }


                            ////===================================//پیدا کردن شروع نقطه پهنا در سمت چپ گراف

                            int find_half = 0;

                            nesf = base_peak - (max_jarian_khales / 2);
                            p1 = volt[start_index]; p2 = volt[end_index];

                            for (int k = peak; k > start_index; k--)
                            {
                                if (current[k] >= nesf)
                                {
                                    if (current[k] > nesf)//در صورتی که مقدار نصف با مقدار جریان در ناحیه مرزی گراف مساوی نباشد یعنی بین دو نقطه باشد
                                    {
                                        double x130 = volt[k], y130 = current[k], x230 = volt[k + 1], y230 = current[k + 1];
                                        double shib30 = (y230 - y130) / (x230 - x130);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                        double x30, y30 = nesf;

                                        x30 = ((shib30 * x130) + (y30 - y130)) / shib30;//فرمول معادله خط

                                        p1 = x30;
                                    }
                                    else
                                        p1 = volt[k];// اگر مساوی بود که نیاز به محاسبه نیست


                                    find_half = 1;

                                    break;
                                }
                            }


                            if (find_half == 0)//اگر گراف در سمت چپ پیک نصف پیدا نکرد پس این پیک اشتباه گرفته شده است
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("The peak on the left is not half the height .");
                                return;
                            }


                            ////============//پیدا کردن شروع نقطه پهنا در سمت راست گراف


                            find_half = 0;

                            for (int k = peak; k < Counter_num; k++)
                            {
                                if (current[k] >= nesf)
                                {
                                    if (current[k] > nesf)//در صورتی که مقدار نصف با مقدار جریان در ناحیه مرزی گراف مساوی نباشد
                                    {
                                        double x130 = volt[k], y130 = current[k], x230 = volt[k - 1], y230 = current[k - 1];
                                        double shib30 = (y230 - y130) / (x230 - x130);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                        double x30, y30 = nesf;

                                        x30 = ((shib30 * x130) + (y30 - y130)) / shib30;//فرمول معادله خط

                                        p2 = x30;
                                    }
                                    else
                                        p2 = volt[k];// اگر مساوی بود که نیاز به محاسبه نیست


                                    find_half = 1;

                                    break;
                                }
                            }

                            if (find_half == 0)//اگر گراف در سمت راست پیک نصف پیدا نکرد پس این پیک اشتباه گرفته شده است
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("The peak on the right is not half the height .");
                                return;
                            }

                            pahna_dar_nesf = p2 - p1;
                            if (pahna_dar_nesf < 0) pahna_dar_nesf = (-1) * pahna_dar_nesf;//مثبت کردن اندازه پهنا      


                            //======== محاسبه مساحت زیر گراف

                            //رابطه بدست آمده از انتگرال
                            //
                            //  aria = (t2 - t1)[( f(t1) + f(t2) ) / 2]

                            double t = 0, ft = 0, ft1 = 0, ft2 = 0;

                            double xx1 = volt[start_index], yy1 = current[start_index], xx2 = volt[end_index], yy2 = current[end_index];
                            double shibb = (yy2 - yy1) / (xx2 - xx1);//   (y2-y1) / (x2-x1)فرمول شیب خط
                            double xx, yy;
                                                       
                            for (int k = Counter_num - 2; k >= start_index ; k--)//حرکت معکوس
                            {
                                xx = volt[k + 1];
                                yy = (shibb * xx) - (shibb * xx1) + yy1;//فرمول معادله خط برای بدست آوردن جریان معادل در بیس لاین
                                ft1 = yy - current[k + 1];//***هر نقطه از جریان در بیس لاین منهای همان نقطه در جریان اصلی می شود تا جریان خالص زیر پیک در هر نقطه بدست آید***معکوس

                                if (ft1 < 0) ft1 = 0;//قاعدتا جریان در یک نقطه باید از بیس لاین در همان نقطه بیشتر باشد در غیر این صورت این نقطه از بیس لاین خارج از محدوده است

                                xx = volt[k];
                                yy = (shibb * xx) - (shibb * xx1) + yy1;//فرمول معادله خط
                                ft2 = yy - current[k];

                                if (ft2 < 0) ft2 = 0;

                                t = volt[k] - volt[k + 1];
                                ft = (ft1 + ft2) / 2;
                                aria = aria + (t * ft);
                            }
                           
                        }


                        string a = aria.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                        try { a = a.Substring(0, a.IndexOf('.') + 6); } catch { }
                        chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//نمایش اندازه مساحت
                        chart4.Series[ser].ChartType = SeriesChartType.Spline;
                        chart4.Series[ser].Color = Color.Red;
                        chart4.Series[ser].BorderWidth = 1;
                        chart4.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                        chart4.Series[ser].Points[0].Label = cv_direct + a;
                        Array.Resize(ref series_area_index, series_area_index.Length + 1);//قرار دادن اندیس برای نمایش با چک باکس
                        series_area_index[series_area_index.Length - 1] = ser;
                        ser++;

                        string height = max_jarian_khales.ToString();
                        try { height = height.Substring(0, height.IndexOf('.') + 4); } catch { }
                        chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم ارتفاع یا همان جریان خالص
                        chart4.Series[ser].ChartType = SeriesChartType.Spline;
                        chart4.Series[ser].Color = Color.Gold;
                        chart4.Series[ser].BorderWidth = 1;
                        chart4.Series[ser].Points.AddXY(volt[peak], current[peak]);
                        chart4.Series[ser].Points.AddXY(volt[peak], base_peak);
                        chart4.Series[ser].Points[0].Label = "H: " + height;
                        Array.Resize(ref series_height_index, series_height_index.Length + 1);
                        series_height_index[series_height_index.Length - 1] = ser;
                        ser++;


                        string w = pahna_dar_nesf.ToString();
                        try { w = w.Substring(0, w.IndexOf('.') + 4); } catch { }
                        chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم پهنا در نصف ارتفاع
                        chart4.Series[ser].ChartType = SeriesChartType.Spline;
                        chart4.Series[ser].Color = Color.SpringGreen;
                        chart4.Series[ser].BorderWidth = 1;
                        chart4.Series[ser].Points.AddXY(p1, nesf);
                        chart4.Series[ser].Points.AddXY(p2, nesf);
                        chart4.Series[ser].Points[0].Label = "W: " + w;
                        Array.Resize(ref series_width_index, series_width_index.Length + 1);
                        series_width_index[series_width_index.Length - 1] = ser;
                    }

                    ch_height_CheckedChanged(sender, e);
                    ch_width_CheckedChanged(sender, e);
                    ch_area_CheckedChanged(sender, e);
                }
                else
                    MessageBox.Show("You should choose the start point and end of base line.", "Error", MessageBoxButtons.OK);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }

            this.Cursor = Cursors.Default;
        }


        private void button11_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (chart3.Series.Count > 0)
                {
                    int j = -1;
                    for (int i = 0; i < this.treeView3.Nodes[0].Nodes.Count; i++)
                        if (this.treeView3.Nodes[0].Nodes[i].Checked == true)//فقط اولین سری که تیک خورده باشد انتخاب می شود
                        {
                            j = i;

                            for (int k = i + 1; k < this.treeView3.Nodes[0].Nodes.Count; k++)//غیر تیک کردن بقیه گرافها
                                this.treeView3.Nodes[0].Nodes[k].Checked = false;

                            TreeNode sel = treeView3.Nodes[0].Nodes[i];//انتخاب گراف تیک دار
                            treeView3.SelectedNode = sel;
                            treeView3.Focus();

                            break;
                        }

                    if (j == -1)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("A graph of the list needs to be checked.", "Error");
                        return;
                    }

                    radioButton1.Checked = true;

                    Array.Clear(volt, 0, volt.Length);
                    Array.Clear(current, 0, current.Length);

                    Counter_num = chart3.Series[j].Points.Count;

                    Array.Resize(ref volt, Counter_num);
                    Array.Resize(ref current, Counter_num);


                    if (chart4.Series.Count == 0)
                    {
                        txt_res.Text = (Convert.ToInt16(Counter_num / 20)).ToString();//این عدد تجربی است یعنی حالتی است که بیشتر قله ها گرفته میشود
                        txt_noise.Text = "0";
                    }


                    chart4.Series.Clear();//پاک کردن گرافها
                    chart4.Refresh();

                    Thread.Sleep(100);//جهت متوجه شدن کاربر                               


                    Array.Clear(series_height_index, 0, series_height_index.Length);//جهت نگهداری اندیس سری هایی از چارت که مربوط با ارتفاع است برای نمایش چک باکس
                    Array.Resize(ref series_height_index, 0);
                    Array.Clear(series_width_index, 0, series_width_index.Length);
                    Array.Resize(ref series_width_index, 0);
                    Array.Clear(series_area_index, 0, series_area_index.Length);
                    Array.Resize(ref series_area_index, 0);


                    int ser = 0;

                    chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());
                    chart4.Series[ser].ChartType = SeriesChartType.Spline;
                    chart4.Series[ser].Color = chart3.Series[0].Color;
                    chart4.Series[ser].BorderWidth = 2;
                    chart4.Titles[ser].Text = "Peak Search";
                    chart4.ChartAreas[0].AxisY.Title = chart3.ChartAreas[0].AxisY.Title;
                    chart4.ChartAreas[0].AxisX.Title = chart3.ChartAreas[0].AxisX.Title;
                    ser++;


                    if (dataSet12.analyselist.Rows[j][0].ToString() == "10" || dataSet12.analyselist.Rows[j][0].ToString() == "11" || dataSet12.analyselist.Rows[j][0].ToString() == "12")
                        chart4.Series[0].Points.AddXY(0, othertech_val1[0]);//رسم در نقطه صفر زمان // technics chp cha chc

                    for (int i = 0; i < Counter_num; i++)
                    {
                        volt[i] = chart3.Series[j].Points[i].XValue;
                        current[i] = chart3.Series[j].Points[i].YValues[0];

                        chart4.Series[0].Points.AddXY(volt[i], current[i]);
                    }


                    //============================ شروع عملیات

                    int forward_count = Counter_num;//برای گراف سی وی که رفت و برگشت دارد این رفت

                    for (int i = 1; i < Counter_num; i++)
                    {
                        if (volt[i] < volt[i - 1])//برای تکنیک سی وی هنگام برگشت
                        {
                            forward_count = i - 1;

                            break;
                        }
                    }


                    bool is_cv = false;//سی وی است یا خیر
                    if (forward_count < Counter_num)
                        is_cv = true;


                    if (Convert.ToInt16(txt_res.Text) < 1)
                        txt_res.Text = "1";

                    if (Convert.ToInt16(txt_noise.Text) < 0)
                        txt_noise.Text = "0";

                    int res = Convert.ToInt16(txt_res.Text);  //در هر این تعداد نقطه ماکس رو می گیرد
                    int result_noise = Convert.ToInt16(txt_noise.Text);  //هنگام مقایسه هر نقطه با بعدی یا قبلی اگر تعداد اختلاف از این حد بگذرد انتهای یا ابتدای پیک است

                    if (forward_count - res < 1)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Resolution is larger than the function.", "Error");
                        return;
                    }



                    //===================== گرفتن ماکس ها به مقیاس مشخص

                    int[] maxs = new int[0];

                    int index = 0;
                    int last_point = 0;

                    for (int i = 0; i < forward_count - res; i += res)
                    {
                        double max = current[i];

                        for (int x = 0; x < res; x++)
                        {
                            if (current[i + x] >= max)//بزرگتر از قبل
                            {
                                max = current[i + x];

                                index = i + x;
                            }
                        }

                        Array.Resize(ref maxs, maxs.Length + 1);
                        maxs[maxs.Length - 1] = index;

                        last_point = i;
                    }

                    if (last_point + res < forward_count - 1)//هنگامی که گام حلقه اجازه نمیدهد ادامه آرایه بررسی شود این قسمت از انتهای قبل تا انتهای آرایه بررسی می کند
                    {
                        int i = last_point + res;

                        double max = current[i];

                        for (int x = i; x < forward_count - 1; x++)
                        {
                            if (current[x + 1] >= max)//بزرگتر از قبل
                            {
                                max = current[x + 1];

                                index = x + 1;
                            }
                        }

                        Array.Resize(ref maxs, maxs.Length + 1);
                        maxs[maxs.Length - 1] = index;
                    }


                    //==========مرتب کردن ماکس ها از بزرگترین چرا که برخی ماکس های کوچک در درون بزرگهاست

                    int temp = 0;

                    for (int i = 0; i < maxs.Length; i++)
                    {
                        for (int k = 0; k < maxs.Length - 1; k++)
                        {
                            if (current[maxs[k]] < current[maxs[k + 1]])//ماکس ها را به ترتیب از کوچکترین مقدار که در حالت معکوس قله می باشد مرتب می کند
                            {
                                temp = maxs[k];
                                maxs[k] = maxs[k + 1];
                                maxs[k + 1] = temp;
                            }
                        }
                    }


                    //=============;// پیدا کردن شروع قله                  

                    int[] all_start = new int[maxs.Length];
                    int[] all_end = new int[maxs.Length];

                    int noise = 0;

                    if (is_cv == false)//اگر گراف سی وی نیست
                    {
                        for (int i = 0; i < maxs.Length; i++)
                        {
                            //===================// پیدا کردن شروع قله

                            double start = current[maxs[i]];
                            int start_index = maxs[i];

                            for (int k = maxs[i]; k > 0; k--)//اگر نقطه قبل از نقطه فعلی کوچکتراست
                            {
                                if (current[k - 1] <= start)
                                {
                                    start = current[k - 1];
                                    start_index = k - 1;
                                    noise = 0;
                                }
                                else
                                {
                                    if (noise >= result_noise)
                                        break;

                                    noise++;
                                }

                            }

                            //===================// پیدا کردن پایان قله

                            noise = 0;

                            double end = current[maxs[i]];
                            int end_index = maxs[i];

                            for (int k = maxs[i]; k < forward_count - 1; k++)
                            {
                                if (current[k + 1] <= end)
                                {
                                    end = current[k + 1];
                                    end_index = k + 1;
                                    noise = 0;
                                }
                                else
                                {
                                    if (noise >= result_noise)
                                        break;

                                    noise++;
                                }
                            }


                            ////=== بررسی عدم قطع گراف در ابتدای قله

                            ////(y2 - y1) = shib(x2 - x1)            فرمول معادله خط
                            ////y2 - y1 = (shib * x2) - (shib * x1)
                            ////y2 = (shib * x2) - (shib * x1) + y1

                            for (int k = start_index; k < maxs[i]; k++)//در برخی موارد هنگامی که بیس لاین از شروع پیک تا انتها رسم می شود در قسمت ابتدا قسمتی از گراف را قطع میکند که این خود باعت اشتباه در قسمت شروع بیس لاین است. بنا براین یک خطی فرضی از نقطه انتهای پیک به ابتدای پیک رسم کردم و در هر نقطه از ابتدا شیب آن را محاسبه کرده و  بررسی کردم که اگر این نقطه بزرگتر از نقطه گراف است شروع از آنجا باشد
                            {
                                double x1 = volt[start_index], y1 = current[start_index], x2 = volt[end_index], y2 = current[end_index];
                                double shib = (y2 - y1) / (x2 - x1);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                double x, y;

                                x = volt[k];
                                y = (shib * x) - (shib * x1) + y1;//فرمول معادله خط

                                if (y > current[k])//اگر نقطه در خط فرضی بزرگتر از همان نقطه در گراف باشد
                                {
                                    start_index = k + 1;
                                }
                            }


                            //=== بررسی عدم قطع گراف در انتهای قله

                            for (int k = end_index; k > maxs[i]; k--)//در برخی موارد هنگامی که بیس لاین از شروع پیک تا انتها رسم می شود در قسمت اانتها قسمتی از گراف را قطع میکند که این خود باعت اشتباه در قسمت انتهای بیس لاین است. بنا براین یک خطی فرضی از نقطه شروع پیک به انتهای پیک رسم کردم و در هر نقطه از انتها شیب آن را محاسبه کرده و  بررسی کردم که اگر این نقطه بزرگتر از نقطه گراف است انتها از آنجا باشد
                            {
                                double x1 = volt[start_index], y1 = current[start_index], x2 = volt[end_index], y2 = current[end_index];
                                double shib = (y2 - y1) / (x2 - x1);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                double x, y;

                                x = volt[k];
                                y = (shib * x) - (shib * x1) + y1;//فرمول معادله خط

                                if (y > current[k])
                                {
                                    end_index = k;
                                }
                            }



                            //================//اگر این  قله تو دل قله های قبلی است صرف نظر کن

                            bool reject = false;

                            for (int k = 0; k < i; k++)
                            {
                                if (start_index >= all_start[k] && end_index <= all_end[k])
                                {
                                    reject = true;
                                    break;
                                }
                            }

                            if (reject == true)
                                continue;


                            all_start[i] = start_index;
                            all_end[i] = end_index;


                            //بدست آوردن همه ارتفاع های خالص از اول پیک تا آخر پیک و پیدا کردن بلندترین آن

                            double x11 = volt[start_index], y11 = current[start_index], x22 = volt[end_index], y22 = current[end_index];
                            double shib32 = (y22 - y11) / (x22 - x11);//   (y2-y1) / (x2-x1)فرمول شیب خط
                            double x32, y32;

                            int peak = 0;
                            double max_jarian_khales = 0;



                            //==فقط جریان خالص ماکس
                            peak = maxs[i];
                            x32 = volt[maxs[i]];
                            y32 = (shib32 * x32) - (shib32 * x11) + y11;//فرمول معادله خط
                            max_jarian_khales = current[maxs[i]] - y32;
                            if (max_jarian_khales < 0)//برای مثبت کردن جریان خالص یا همان ارتفاع زیر پیک
                                max_jarian_khales = max_jarian_khales * (-1);
                            //==


                            ////این قسمت همه جریانهای خالص را از اولین نقطه زیر پیک تا آخرین نقطه زیر پیک حساب می کند و بزرگترین جریان را به همراه قله اش انتخاب میکند
                            //for (int k = start_index; k < end_index; k++)
                            //{
                            //    x32 = volt[k];
                            //    y32 = (shib32 * x32) - (shib32 * x11) + y11;//فرمول معادله خط

                            //    double H_khales = current[k] - y32;
                            //    if (H_khales < 0)//برای مثبت کردن جریان خالص یا همان ارتفاع زیر پیک
                            //        H_khales = H_khales * (-1);

                            //    if (H_khales >= max_jarian_khales)
                            //    {
                            //        peak = k;
                            //        max_jarian_khales = H_khales;
                            //    }
                            //}


                            if (max_jarian_khales == 0)//اگر یک پیکی گرفت اما ارتفاعش 0 بود اصلا چیزی رسم نکند
                                continue;


                            if (max_jarian_khales < Convert.ToDouble(txt_height.Text))//اگر ارتفاع از مقدار داده شده کاربر کمتر است پیک نگیرد
                                continue;

                            //=========== بدست آوردن نقطه پایین ارتفاع پیک در ناحیه قطع بیس لاین

                            double base_peak = start_index;

                            double x13 = volt[start_index], y13 = current[start_index], x23 = volt[end_index], y23 = current[end_index];
                            double shib3 = (y23 - y13) / (x23 - x13);//   (y2-y1) / (x2-x1)فرمول شیب خط
                            double x3, y3;

                            x3 = volt[peak];
                            y3 = (shib3 * x3) - (shib3 * x13) + y13;//فرمول معادله خط
                            base_peak = y3;


                            //===================================//پیدا کردن شروع نقطه پهنا در سمت چپ گراف

                            int find_half = 0;

                            double nesf = base_peak + (max_jarian_khales / 2);
                            double p1 = volt[start_index], p2 = volt[end_index];

                            for (int k = peak; k > start_index; k--)
                            {
                                if (current[k] <= nesf)
                                {
                                    if (current[k] < nesf)//در صورتی که مقدار نصف با مقدار جریان در ناحیه مرزی گراف مساوی نباشد یعنی بین دو نقطه باشد
                                    {
                                        double x130 = volt[k], y130 = current[k], x230 = volt[k + 1], y230 = current[k + 1];
                                        double shib30 = (y230 - y130) / (x230 - x130);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                        double x30, y30 = nesf;

                                        x30 = ((shib30 * x130) + (y30 - y130)) / shib30;//فرمول معادله خط

                                        p1 = x30;
                                    }
                                    else
                                        p1 = volt[k];// اگر مساوی بود که نیاز به محاسبه نیست


                                    find_half = 1;

                                    break;
                                }
                            }


                            if (find_half == 0)//اگر گراف در سمت چپ پیک نصف پیدا نکرد پس این پیک اشتباه گرفته شده است
                                continue;

                            //============//پیدا کردن شروع نقطه پهنا در سمت راست گراف


                            find_half = 0;

                            for (int k = peak; k < end_index; k++)
                            {
                                if (current[k] <= nesf)
                                {
                                    if (current[k] < nesf)//در صورتی که مقدار نصف با مقدار جریان در ناحیه مرزی گراف مساوی نباشد
                                    {
                                        double x130 = volt[k], y130 = current[k], x230 = volt[k - 1], y230 = current[k - 1];
                                        double shib30 = (y230 - y130) / (x230 - x130);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                        double x30, y30 = nesf;

                                        x30 = ((shib30 * x130) + (y30 - y130)) / shib30;//فرمول معادله خط

                                        p2 = x30;
                                    }
                                    else
                                        p2 = volt[k];// اگر مساوی بود که نیاز به محاسبه نیست


                                    find_half = 1;

                                    break;
                                }
                            }

                            if (find_half == 0)//اگر گراف در سمت راست پیک نصف پیدا نکرد پس این پیک اشتباه گرفته شده است
                                continue;

                            double pahna_dar_nesf = p2 - p1;
                            if (pahna_dar_nesf < 0) pahna_dar_nesf = (-1) * pahna_dar_nesf;//مثبت کردن اندازه پهنا

                            if (pahna_dar_nesf < Convert.ToDouble(txt_width.Text))//اگر پهنا از مقدار داده شده کاربر کمتر است پیک نگیرد
                                continue;


                            //======== محاسبه مساحت زیر گراف

                            //رابطه بدست آمده از انتگرال
                            //
                            //  aria = (t2 - t1)[( f(t1) + f(t2) ) / 2]

                            double aria = 0, t = 0, ft = 0, ft1 = 0, ft2 = 0;

                            double xx1 = volt[start_index], yy1 = current[start_index], xx2 = volt[end_index], yy2 = current[end_index];
                            double shibb = (yy2 - yy1) / (xx2 - xx1);//   (y2-y1) / (x2-x1)فرمول شیب خط
                            double xx, yy;

                            for (int k = start_index + 1; k <= end_index; k++)
                            {
                                xx = volt[k - 1];
                                yy = (shibb * xx) - (shibb * xx1) + yy1;//فرمول معادله خط برای بدست آوردن جریان معادل در بیس لاین
                                ft1 = current[k - 1] - yy;//هر نقطه از جریان منهای همان نقطه در بیس لاین می شود تا جریان خالص زیر پیک در هر نقطه بدست آید

                                if (ft1 < 0) ft1 = 0;//قاعدتا جریان در یک نقطه باید از بیس لاین در همان نقطه بیشتر باشد در غیر این صورت این نقطه از بیس لاین خارج از محدوده است

                                xx = volt[k];
                                yy = (shibb * xx) - (shibb * xx1) + yy1;//فرمول معادله خط
                                ft2 = current[k] - yy;

                                if (ft2 < 0) ft2 = 0;

                                t = volt[k] - volt[k - 1];
                                ft = (ft1 + ft2) / 2;
                                aria = aria + (t * ft);
                            }

                            //============


                            chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم بیس لاین
                            chart4.Series[ser].ChartType = SeriesChartType.Spline;
                            chart4.Series[ser].Color = Color.Red;
                            chart4.Series[ser].BorderWidth = 1;
                            chart4.Series[ser].Points.AddXY(volt[start_index], current[start_index]);
                            chart4.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                            ser++;


                            string a = aria.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                            try { a = a.Substring(0, a.IndexOf('.') + 6); } catch { }
                            chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//نمایش اندازه مساحت
                            chart4.Series[ser].ChartType = SeriesChartType.Spline;
                            chart4.Series[ser].Color = Color.Red;
                            chart4.Series[ser].BorderWidth = 1;
                            chart4.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                            chart4.Series[ser].Points[0].Label = "Aria: " + a;
                            Array.Resize(ref series_area_index, series_area_index.Length + 1);//قرار دادن اندیس برای نمایش با چک باکس
                            series_area_index[series_area_index.Length - 1] = ser;
                            ser++;


                            string height = max_jarian_khales.ToString();
                        try { height = height.Substring(0, height.IndexOf('.') + 4); } catch { }
                            chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم ارتفاع یا همان جریان خالص
                            chart4.Series[ser].ChartType = SeriesChartType.Spline;
                            chart4.Series[ser].Color = Color.Gold;
                            chart4.Series[ser].BorderWidth = 1;
                            chart4.Series[ser].Points.AddXY(volt[peak], current[peak]);
                            //chart4.Series[ser].Points.AddXY(volt[peak], current[peak] - (max_jarian_khales / 4));//این نقطه به جهت نمایش لیبل ارتفاع خالص است
                            chart4.Series[ser].Points.AddXY(volt[peak], base_peak);
                            chart4.Series[ser].Points[0].Label = "H: " + height;
                            Array.Resize(ref series_height_index, series_height_index.Length + 1);//قرار دادن اندیس برای نمایش با چک باکس
                            series_height_index[series_height_index.Length - 1] = ser;
                            ser++;


                            string w = pahna_dar_nesf.ToString();
                            try { w = w.Substring(0, w.IndexOf('.') + 4); } catch { }
                            chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم پهنا در نصف ارتفاع
                            chart4.Series[ser].ChartType = SeriesChartType.Spline;
                            chart4.Series[ser].Color = Color.SpringGreen;
                            chart4.Series[ser].BorderWidth = 1;
                            chart4.Series[ser].Points.AddXY(p1, nesf);
                            //chart4.Series[ser].Points.AddXY((volt[maxs[i]] + p1) / 2, nesf);//این نقطه به جهت نمایش لیبل پهنا است
                            chart4.Series[ser].Points.AddXY(p2, nesf);
                            chart4.Series[ser].Points[0].Label = "W: " + w;
                            Array.Resize(ref series_width_index, series_width_index.Length + 1);
                            series_width_index[series_width_index.Length - 1] = ser;
                            ser++;
                        }
                    }
                    //====cv
                    else
                    //====cv
                    {
                        //===================== گرفتن ماکس ها به مقیاس مشخص

                        for (int i = 0; i < maxs.Length; i++)
                        {
                            //===================// پیدا کردن شروع قله

                            double start = current[maxs[i]];
                            int start_index = maxs[i];
                            noise = 0;

                            for (int k = maxs[i]; k > 0; k--)//اگر نقطه قبل از نقطه فعلی کوچکتراست
                            {
                                if (current[k - 1] <= start)
                                {
                                    start = current[k - 1];
                                    start_index = k - 1;
                                    noise = 0;
                                }
                                else
                                {
                                    if (noise >= result_noise)
                                        break;

                                    noise++;
                                }
                            }


                            //===================// پیدا کردن پایان قله

                            noise = 0;

                            double end = current[maxs[i]];
                            int end_index = maxs[i];

                            for (int k = maxs[i]; k < forward_count - 1; k++)
                            {
                                if (current[k + 1] <= end)
                                {
                                    end = current[k + 1];
                                    end_index = k + 1;
                                    noise = 0;
                                }
                                else
                                {
                                    if (noise >= result_noise)
                                        break;

                                    noise++;
                                }
                            }


                            ////=== بررسی عدم قطع گراف در ابتدای قله

                            ////(y2 - y1) = shib(x2 - x1)            فرمول معادله خط
                            ////y2 - y1 = (shib * x2) - (shib * x1)
                            ////y2 = (shib * x2) - (shib * x1) + y1

                            for (int k = start_index; k < maxs[i]; k++)//در برخی موارد هنگامی که بیس لاین از شروع پیک تا انتها رسم می شود در قسمت ابتدا قسمتی از گراف را قطع میکند که این خود باعت اشتباه در قسمت شروع بیس لاین است. بنا براین یک خطی فرضی از نقطه انتهای پیک به ابتدای پیک رسم کردم و در هر نقطه از ابتدا شیب آن را محاسبه کرده و  بررسی کردم که اگر این نقطه بزرگتر از نقطه گراف است شروع از آنجا باشد
                            {
                                double x1 = volt[start_index], y1 = current[start_index], x2 = volt[end_index], y2 = current[end_index];
                                double shib = (y2 - y1) / (x2 - x1);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                double x, y;

                                x = volt[k];
                                y = (shib * x) - (shib * x1) + y1;//فرمول معادله خط

                                if (y > current[k])//اگر نقطه در خط فرضی بزرگتر از همان نقطه در گراف باشد
                                {
                                    start_index = k + 1;
                                }
                            }


                            //=== بررسی عدم قطع گراف در انتهای قله

                            for (int k = end_index; k > maxs[i]; k--)//در برخی موارد هنگامی که بیس لاین از شروع پیک تا انتها رسم می شود در قسمت اانتها قسمتی از گراف را قطع میکند که این خود باعت اشتباه در قسمت انتهای بیس لاین است. بنا براین یک خطی فرضی از نقطه شروع پیک به انتهای پیک رسم کردم و در هر نقطه از انتها شیب آن را محاسبه کرده و  بررسی کردم که اگر این نقطه بزرگتر از نقطه گراف است انتها از آنجا باشد
                            {
                                double x1 = volt[start_index], y1 = current[start_index], x2 = volt[end_index], y2 = current[end_index];
                                double shib = (y2 - y1) / (x2 - x1);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                double x, y;

                                x = volt[k];
                                y = (shib * x) - (shib * x1) + y1;//فرمول معادله خط

                                if (y > current[k])
                                {
                                    end_index = k;
                                }
                            }



                            //================//اگر این  قله تو دل قله های قبلی است صرف نظر کن

                            bool reject = false;

                            for (int k = 0; k < i; k++)
                            {
                                if (start_index >= all_start[k] && end_index <= all_end[k])
                                {
                                    reject = true;
                                    break;
                                }
                            }

                            if (reject == true)
                                continue;


                            all_start[i] = start_index;
                            all_end[i] = end_index;


                            //بدست آوردن همه ارتفاع های خالص از اول پیک تا آخر پیک و پیدا کردن بلندترین آن

                            double x11 = volt[start_index], y11 = current[start_index], x22 = volt[end_index], y22 = current[end_index];
                            double shib32 = (y22 - y11) / (x22 - x11);//   (y2-y1) / (x2-x1)فرمول شیب خط
                            double x32, y32;

                            int peak = 0;
                            double max_jarian_khales = 0;


                            //==فقط جریان خالص ماکس
                            peak = maxs[i];
                            x32 = volt[maxs[i]];
                            y32 = (shib32 * x32) - (shib32 * x11) + y11;//فرمول معادله خط
                            max_jarian_khales = current[maxs[i]] - y32;
                            if (max_jarian_khales < 0)//برای مثبت کردن جریان خالص یا همان ارتفاع زیر پیک
                                max_jarian_khales = max_jarian_khales * (-1);

                            //=========== بدست آوردن نقطه پایین ارتفاع پیک در ناحیه قطع بیس لاین

                            double base_peak = start_index;

                            double x13 = volt[start_index], y13 = current[start_index], x23 = volt[end_index], y23 = current[end_index];
                            double shib3 = (y23 - y13) / (x23 - x13);//   (y2-y1) / (x2-x1)فرمول شیب خط
                            double x3, y3;

                            x3 = volt[peak];
                            y3 = (shib3 * x3) - (shib3 * x13) + y13;//فرمول معادله خط
                            base_peak = y3;


                            //===================================//پیدا کردن شروع نقطه پهنا در سمت چپ گراف

                            int find_half = 0;

                            double nesf = base_peak + (max_jarian_khales / 2);
                            double p1 = volt[start_index], p2 = volt[end_index];

                            for (int k = peak; k > start_index; k--)
                            {
                                if (current[k] <= nesf)
                                {
                                    if (current[k] < nesf)//در صورتی که مقدار نصف با مقدار جریان در ناحیه مرزی گراف مساوی نباشد یعنی بین دو نقطه باشد
                                    {
                                        double x130 = volt[k], y130 = current[k], x230 = volt[k + 1], y230 = current[k + 1];
                                        double shib30 = (y230 - y130) / (x230 - x130);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                        double x30, y30 = nesf;

                                        x30 = ((shib30 * x130) + (y30 - y130)) / shib30;//فرمول معادله خط

                                        p1 = x30;
                                    }
                                    else
                                        p1 = volt[k];// اگر مساوی بود که نیاز به محاسبه نیست


                                    find_half = 1;

                                    break;
                                }
                            }


                            ////============//پیدا کردن شروع نقطه پهنا در سمت راست گراف


                            find_half = 0;

                            for (int k = peak; k < end_index; k++)
                            {
                                if (current[k] <= nesf)
                                {
                                    if (current[k] < nesf)//در صورتی که مقدار نصف با مقدار جریان در ناحیه مرزی گراف مساوی نباشد
                                    {
                                        double x130 = volt[k], y130 = current[k], x230 = volt[k - 1], y230 = current[k - 1];
                                        double shib30 = (y230 - y130) / (x230 - x130);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                        double x30, y30 = nesf;

                                        x30 = ((shib30 * x130) + (y30 - y130)) / shib30;//فرمول معادله خط

                                        p2 = x30;
                                    }
                                    else
                                        p2 = volt[k];// اگر مساوی بود که نیاز به محاسبه نیست


                                    find_half = 1;

                                    break;
                                }
                            }


                            //====***====//تا قبل از اینجا طبق روال سابق جهت صحت قله قرار دادم اما محاسبات و بیس لاین اصلی از اینجا انجام می شود


                            //======= گراف تکنیک سی وی باید بیس لاین قبل از قله پیدا شود بعد امتداد یابد
                            int st = start_index - 1;
                            if (st <= 0) st = 1;

                            x11 = volt[start_index]; y11 = current[start_index]; x22 = volt[st]; y22 = current[st];
                            double shib_a = (y22 - y11) / (x22 - x11);//شیب دونقطه قبل از استارت را به عنوان مرجع می گیریم و تا جایی که کمترین شیب موجود باشد امتداد می دهیم

                            int pt1 = st;
                            for (int k = start_index - 1; k > 0; k--)
                            {
                                x11 = volt[start_index]; y11 = current[start_index]; x22 = volt[k]; y22 = current[k];
                                double shib_b = (y22 - y11) / (x22 - x11);

                                if (shib_b < shib_a)
                                {
                                    shib_a = shib_b;
                                    pt1 = k - 1;
                                }
                            }

                            //==محدوده شروع قله تا انتخاب نقطه اول را تقسیم دو میکنیم سپس مینیمم هرقسمت نقاط ما هستند

                            int div = (st - pt1) / 2;

                            for (int k = pt1 + 1; k < pt1 + div; k++)//از محدوده بیس مشخص شده به عنوان نقطه اول مینیمم میگیریم
                            {
                                if (current[k] < current[k - 1])
                                {
                                    pt1 = k;
                                }
                            }

                            int pt2 = pt1 + div;// از محدوده مشخص شده به عنوان نقطه دوم مینیمم دوم میگیریم.
                            for (int k = pt1 + div; k < st; k++)
                            {
                                if (current[k] < current[k - 1])
                                {
                                    pt2 = k;
                                }
                            }


                            //============= بدست آوردن ارتفاع خالص

                            double x15 = volt[pt1], y15 = current[pt1], x25 = volt[pt2], y25 = current[pt2];
                            double shib5 = (y25 - y15) / (x25 - x15);//   (y2-y1) / (x2-x1)فرمول شیب خط
                            double x5, y5;
                            x5 = volt[forward_count];//بدست آوردن نقطه امتداد بیس لاین در انتها
                            y5 = (shib5 * x5) - (shib5 * x15) + y15;//فرمول معادله خط                          

                            x32 = volt[peak];//بدست آوردن ارتفاع روی بیس لاین
                            base_peak = (shib5 * x32) - (shib5 * x15) + y15;//فرمول معادله خط

                            max_jarian_khales = current[peak] - base_peak;
                            if (max_jarian_khales < 0)//برای مثبت کردن جریان خالص یا همان ارتفاع زیر پیک
                                max_jarian_khales = max_jarian_khales * (-1);

                            if (max_jarian_khales == 0)//اگر یک پیکی گرفت اما ارتفاعش 0 بود اصلا چیزی رسم نکند
                                continue;

                            if (max_jarian_khales < Convert.ToDouble(txt_height.Text))//اگر ارتفاع از مقدار داده شده کاربر کمتر است پیک نگیرد
                                continue;

                            ////===================================//پیدا کردن شروع نقطه پهنا در سمت چپ گراف

                            find_half = 0;

                            nesf = base_peak + (max_jarian_khales / 2);
                            p1 = volt[start_index]; p2 = volt[end_index];

                            for (int k = peak; k > start_index; k--)
                            {
                                if (current[k] <= nesf)
                                {
                                    if (current[k] < nesf)//در صورتی که مقدار نصف با مقدار جریان در ناحیه مرزی گراف مساوی نباشد یعنی بین دو نقطه باشد
                                    {
                                        double x130 = volt[k], y130 = current[k], x230 = volt[k + 1], y230 = current[k + 1];
                                        double shib30 = (y230 - y130) / (x230 - x130);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                        double x30, y30 = nesf;

                                        x30 = ((shib30 * x130) + (y30 - y130)) / shib30;//فرمول معادله خط

                                        p1 = x30;
                                    }
                                    else
                                        p1 = volt[k];// اگر مساوی بود که نیاز به محاسبه نیست


                                    find_half = 1;

                                    break;
                                }
                            }


                            if (find_half == 0)//اگر گراف در سمت چپ پیک نصف پیدا نکرد پس این پیک اشتباه گرفته شده است
                                continue;


                            ////============//پیدا کردن شروع نقطه پهنا در سمت راست گراف


                            find_half = 0;

                            for (int k = peak; k < Counter_num; k++)
                            {
                                if (current[k] <= nesf)
                                {
                                    if (current[k] < nesf)//در صورتی که مقدار نصف با مقدار جریان در ناحیه مرزی گراف مساوی نباشد
                                    {
                                        double x130 = volt[k], y130 = current[k], x230 = volt[k - 1], y230 = current[k - 1];
                                        double shib30 = (y230 - y130) / (x230 - x130);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                        double x30, y30 = nesf;

                                        x30 = ((shib30 * x130) + (y30 - y130)) / shib30;//فرمول معادله خط

                                        p2 = x30;
                                    }
                                    else
                                        p2 = volt[k];// اگر مساوی بود که نیاز به محاسبه نیست


                                    find_half = 1;

                                    break;
                                }
                            }

                            if (find_half == 0)//اگر گراف در سمت راست پیک نصف پیدا نکرد پس این پیک اشتباه گرفته شده است
                                continue;

                            double pahna_dar_nesf = p2 - p1;
                            if (pahna_dar_nesf < 0) pahna_dar_nesf = (-1) * pahna_dar_nesf;//مثبت کردن اندازه پهنا

                            if (pahna_dar_nesf < Convert.ToDouble(txt_width.Text))//اگر پهنا از مقدار داده شده کاربر کمتر است پیک نگیرد
                                continue;



                            //======== محاسبه مساحت زیر گراف

                            //رابطه بدست آمده از انتگرال
                            //
                            //  aria = (t2 - t1)[( f(t1) + f(t2) ) / 2]

                            double t = 0, ft = 0, ft1 = 0, ft2 = 0, aria = 0;

                            double xx1 = volt[start_index], yy1 = current[start_index], xx2 = volt[end_index], yy2 = current[end_index];
                            double shibb = (yy2 - yy1) / (xx2 - xx1);//   (y2-y1) / (x2-x1)فرمول شیب خط
                            double xx, yy;

                            for (int k = start_index + 1; k <= forward_count; k++)
                            {
                                xx = volt[k - 1];
                                yy = (shibb * xx) - (shibb * xx1) + yy1;//فرمول معادله خط برای بدست آوردن جریان معادل در بیس لاین
                                ft1 = current[k - 1] - yy;//هر نقطه از جریان منهای همان نقطه در بیس لاین می شود تا جریان خالص زیر پیک در هر نقطه بدست آید

                                if (ft1 < 0) ft1 = 0;//قاعدتا جریان در یک نقطه باید از بیس لاین در همان نقطه بیشتر باشد در غیر این صورت این نقطه از بیس لاین خارج از محدوده است

                                xx = volt[k];
                                yy = (shibb * xx) - (shibb * xx1) + yy1;//فرمول معادله خط
                                ft2 = current[k] - yy;

                                if (ft2 < 0) ft2 = 0;

                                t = volt[k] - volt[k - 1];
                                ft = (ft1 + ft2) / 2;
                                aria = aria + (t * ft);
                            }
                            

                            chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم بیس لاین
                            chart4.Series[ser].ChartType = SeriesChartType.Spline;
                            chart4.Series[ser].Color = Color.Red;
                            chart4.Series[ser].BorderWidth = 1;
                            chart4.Series[ser].Points.AddXY(volt[pt1], current[pt1]);
                            chart4.Series[ser].Points.AddXY(volt[pt2], current[pt2]);
                            chart4.Series[ser].Points.AddXY(x5, y5);
                            ser++;

                            string a = aria.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                            try { a = a.Substring(0, a.IndexOf('.') + 6); } catch { }
                            chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//نمایش اندازه مساحت
                            chart4.Series[ser].ChartType = SeriesChartType.Spline;
                            chart4.Series[ser].Color = Color.Red;
                            chart4.Series[ser].BorderWidth = 1;
                            chart4.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                            chart4.Series[ser].Points[0].Label = "Aria Forward: " + a;
                            Array.Resize(ref series_area_index, series_area_index.Length + 1);//قرار دادن اندیس برای نمایش با چک باکس
                            series_area_index[series_area_index.Length - 1] = ser;
                            ser++;

                            string height = max_jarian_khales.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                            try { height = height.Substring(0, height.IndexOf('.') + 4); } catch { }
                            chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم ارتفاع یا همان جریان خالص
                            chart4.Series[ser].ChartType = SeriesChartType.Spline;
                            chart4.Series[ser].Color = Color.Gold;
                            chart4.Series[ser].BorderWidth = 1;
                            chart4.Series[ser].Points.AddXY(volt[peak], current[peak]);
                            chart4.Series[ser].Points.AddXY(volt[peak], base_peak);
                            chart4.Series[ser].Points[0].Label = "H: " + height;
                            Array.Resize(ref series_height_index, series_height_index.Length + 1);
                            series_height_index[series_height_index.Length - 1] = ser;
                            ser++;

                            string w = pahna_dar_nesf.ToString();
                            try { w = w.Substring(0, w.IndexOf('.') + 4); } catch { }
                            chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم پهنا در نصف ارتفاع
                            chart4.Series[ser].ChartType = SeriesChartType.Spline;
                            chart4.Series[ser].Color = Color.SpringGreen;
                            chart4.Series[ser].BorderWidth = 1;
                            chart4.Series[ser].Points.AddXY(p1, nesf);
                            chart4.Series[ser].Points.AddXY(p2, nesf);
                            chart4.Series[ser].Points[0].Label = "W: " + w;
                            Array.Resize(ref series_width_index, series_width_index.Length + 1);
                            series_width_index[series_width_index.Length - 1] = ser;
                            ser++;
                        }



                        //================
                        //=============cv برگشت
                        //=============


                        Array.Clear(maxs, 0, maxs.Length);
                        Array.Resize(ref maxs, 0);

                        index = 0;
                        last_point = 0;

                        for (int i = forward_count + 1; i < Counter_num - res; i += res)
                        {
                            double max = current[i];

                            for (int x = 0; x < res; x++)
                            {
                                if (current[i + x] <= max)
                                {
                                    max = current[i + x];
                                    index = i + x;
                                }
                            }

                            Array.Resize(ref maxs, maxs.Length + 1);
                            maxs[maxs.Length - 1] = index;

                            last_point = i;
                        }

                        if (last_point + res < Counter_num - 1)//هنگامی که گام حلقه اجازه نمیدهد ادامه آرایه بررسی شود این قسمت از انتهای قبل تا انتهای آرایه بررسی می کند
                        {
                            int i = last_point + res;

                            double max = current[i];

                            for (int x = i; x < Counter_num - 1; x++)
                            {
                                if (current[x + 1] <= max)//بزرگتر از قبل
                                {
                                    max = current[x + 1];
                                    index = x + 1;
                                }
                            }

                            Array.Resize(ref maxs, maxs.Length + 1);
                            maxs[maxs.Length - 1] = index;
                        }
                        //==========مرتب کردن ماکس ها از بزرگترین چرا که برخی ماکس های کوچک در درون بزرگهاست

                        temp = 0;

                        for (int i = 0; i < maxs.Length; i++)
                        {
                            for (int k = 0; k < maxs.Length - 1; k++)
                            {
                                if (current[maxs[k]] < current[maxs[k + 1]])//ماکس ها را به ترتیب از کوچکترین مقدار که در حالت معکوس قله می باشد مرتب می کند
                                {
                                    temp = maxs[k];
                                    maxs[k] = maxs[k + 1];
                                    maxs[k + 1] = temp;
                                }
                            }
                        }

                        //=============;// پیدا کردن شروع قله       

                        int[] all_start_2 = new int[maxs.Length];
                        int[] all_end_2 = new int[maxs.Length];

                        noise = 0;

                        for (int i = 0; i < maxs.Length; i++)
                        {
                            double start = current[maxs[i]];
                            int start_index = maxs[i];

                            for (int k = maxs[i]; k > forward_count + 1; k--)
                            {
                                if (current[k - 1] >= start)
                                {
                                    start = current[k - 1];
                                    start_index = k - 1;
                                    noise = 0;
                                }
                                else
                                {
                                    if (noise >= result_noise)
                                        break;

                                    noise++;
                                }

                            }

                            //===================// پیدا کردن پایان قله

                            noise = 0;

                            double end = current[maxs[i]];
                            int end_index = maxs[i];

                            for (int k = maxs[i]; k < Counter_num - 1; k++)
                            {
                                if (current[k + 1] >= end)
                                {
                                    end = current[k + 1];
                                    end_index = k + 1;
                                    noise = 0;
                                }
                                else
                                {
                                    if (noise >= result_noise)
                                        break;

                                    noise++;
                                }
                            }

                            //=== بررسی عدم قطع گراف در ابتدای قله

                            ////(y2 - y1) = shib(x2 - x1)            فرمول معادله خط
                            ////y2 - y1 = (shib * x2) - (shib * x1)
                            ////y2 = (shib * x2) - (shib * x1) + y1

                            for (int k = start_index; k < maxs[i]; k++)//در برخی موارد هنگامی که بیس لاین از شروع پیک تا انتها رسم می شود در قسمت ابتدا قسمتی از گراف را قطع میکند که این خود باعت اشتباه در قسمت شروع بیس لاین است. بنا براین یک خطی فرضی از نقطه انتهای پیک به ابتدای پیک رسم کردم و در هر نقطه از ابتدا شیب آن را محاسبه کرده و  بررسی کردم که اگر این نقطه بزرگتر از نقطه گراف است شروع از آنجا باشد
                            {
                                double x1 = volt[end_index], y1 = current[end_index], x2 = volt[start_index], y2 = current[start_index];
                                double shib = (y2 - y1) / (x2 - x1);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                double x, y;

                                x = volt[k];
                                y = (shib * x) - (shib * x1) + y1;//فرمول معادله خط

                                if (y < current[k])//اگر نقطه در خط فرضی بزرگتر از همان نقطه در گراف باشد
                                {
                                    start_index = k + 1;
                                }
                            }


                            //=== بررسی عدم قطع گراف در انتهای قله

                            for (int k = end_index; k > maxs[i]; k--)//در برخی موارد هنگامی که بیس لاین از شروع پیک تا انتها رسم می شود در قسمت اانتها قسمتی از گراف را قطع میکند که این خود باعت اشتباه در قسمت انتهای بیس لاین است. بنا براین یک خطی فرضی از نقطه شروع پیک به انتهای پیک رسم کردم و در هر نقطه از انتها شیب آن را محاسبه کرده و  بررسی کردم که اگر این نقطه بزرگتر از نقطه گراف است انتها از آنجا باشد
                            {
                                double x1 = volt[end_index], y1 = current[end_index], x2 = volt[start_index], y2 = current[start_index];
                                double shib = (y2 - y1) / (x2 - x1);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                double x, y;

                                x = volt[k];
                                y = (shib * x) - (shib * x1) + y1;//فرمول معادله خط

                                if (y < current[k])
                                {
                                    end_index = k;
                                }
                            }



                            //================//اگر این  قله تو دل قله های قبلی است صرف نظر کن


                            bool reject = false;

                            for (int k = 0; k < i; k++)
                            {
                                if (start_index >= all_start_2[k] && end_index <= all_end_2[k])
                                {
                                    reject = true;
                                    break;
                                }
                            }

                            if (reject == true)
                                continue;

                            all_start_2[i] = start_index;
                            all_end_2[i] = end_index;


                            //بدست آوردن همه ارتفاع های خالص از اول پیک تا آخر پیک و پیدا کردن بلندترین آن

                            double x11 = volt[end_index], y11 = current[end_index], x22 = volt[start_index], y22 = current[start_index];
                            double shib32 = (y22 - y11) / (x22 - x11);//   (y2-y1) / (x2-x1)فرمول شیب خط
                            double x32, y32;

                            int peak = 0;
                            double max_jarian_khales = 0;

                            //==فقط جریان خالص ماکس
                            peak = maxs[i];
                            x32 = volt[maxs[i]];
                            y32 = (shib32 * x32) - (shib32 * x11) + y11;//فرمول معادله خط
                            max_jarian_khales = current[maxs[i]] - y32;
                            if (max_jarian_khales < 0)//برای مثبت کردن جریان خالص یا همان ارتفاع زیر پیک
                                max_jarian_khales = max_jarian_khales * (-1);


                            //=========== بدست آوردن نقطه پایین ارتفاع پیک در ناحیه قطع بیس لاین

                            double base_peak = start_index;

                            double x13 = volt[end_index], y13 = current[end_index], x23 = volt[start_index], y23 = current[start_index];
                            double shib3 = (y23 - y13) / (x23 - x13);//   (y2-y1) / (x2-x1)فرمول شیب خط
                            double x3, y3;

                            x3 = volt[peak];
                            y3 = (shib3 * x3) - (shib3 * x13) + y13;//فرمول معادله خط
                            base_peak = y3;


                            //===================================//پیدا کردن شروع نقطه پهنا در سمت چپ گراف

                            int find_half = 0;

                            double nesf = base_peak - (max_jarian_khales / 2);
                            double p1 = volt[start_index], p2 = volt[end_index];

                            for (int k = peak; k > start_index; k--)
                            {
                                if (current[k] >= nesf)
                                {
                                    if (current[k] > nesf)//در صورتی که مقدار نصف با مقدار جریان در ناحیه مرزی گراف مساوی نباشد یعنی بین دو نقطه باشد
                                    {
                                        double x130 = volt[k], y130 = current[k], x230 = volt[k + 1], y230 = current[k + 1];
                                        double shib30 = (y230 - y130) / (x230 - x130);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                        double x30, y30 = nesf;

                                        x30 = ((shib30 * x130) + (y30 - y130)) / shib30;//فرمول معادله خط

                                        p1 = x30;
                                    }
                                    else
                                        p1 = volt[k];// اگر مساوی بود که نیاز به محاسبه نیست


                                    find_half = 1;

                                    break;
                                }
                            }


                            //============//پیدا کردن شروع نقطه پهنا در سمت راست گراف


                            find_half = 0;

                            for (int k = peak; k < end_index; k++)
                            {
                                if (current[k] >= nesf)
                                {
                                    if (current[k] > nesf)//در صورتی که مقدار نصف با مقدار جریان در ناحیه مرزی گراف مساوی نباشد
                                    {
                                        double x130 = volt[k], y130 = current[k], x230 = volt[k - 1], y230 = current[k - 1];
                                        double shib30 = (y230 - y130) / (x230 - x130);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                        double x30, y30 = nesf;

                                        x30 = ((shib30 * x130) + (y30 - y130)) / shib30;//فرمول معادله خط

                                        p2 = x30;
                                    }
                                    else
                                        p2 = volt[k];// اگر مساوی بود که نیاز به محاسبه نیست


                                    find_half = 1;

                                    break;
                                }
                            }



                            //====***====//تا قبل از اینجا طبق روال سابق جهت صحت قله قرار دادم اما محاسبات و بیس لاین اصلی از اینجا انجام می شود



                            //======= گراف تکنیک سی وی باید بیس لاین قبل از قله پیدا شود بعد امتداد یابد
                            int st = start_index - 1;
                            if (st <= 0) st = 1;

                            x11 = volt[start_index]; y11 = current[start_index]; x22 = volt[st]; y22 = current[st];
                            double shib_a = (y22 - y11) / (x22 - x11);//شیب دونقطه قبل از استارت را به عنوان مرجع می گیریم و تا جایی که کمترین شیب موجود باشد امتداد می دهیم

                            int pt1 = st;
                            for (int k = start_index - 1; k > forward_count + 1; k--)
                            {
                                x11 = volt[start_index]; y11 = current[start_index]; x22 = volt[k]; y22 = current[k];
                                double shib_b = (y22 - y11) / (x22 - x11);

                                if (shib_b < shib_a)
                                {
                                    shib_a = shib_b;
                                    pt1 = k - 1;
                                }
                            }


                            //==محدوده شروع قله تا انتخاب نقطه اول را تقسیم دو میکنیم سپس مینیمم هرقسمت نقاط ما هستند

                            int div = (st - pt1) / 2;
                            int last = pt1 + div;

                            if (last > Counter_num) last = Counter_num;

                            for (int k = pt1 + 1; k < last; k++)//از محدوده بیس مشخص شده به عنوان نقطه اول مینیمم میگیریم
                            {
                                if (current[k] > current[k - 1])
                                {
                                    pt1 = k;
                                }
                            }

                            int pt2 = pt1 + div;// از محدوده مشخص شده به عنوان نقطه دوم مینیمم دوم میگیریم.
                            for (int k = pt1 + div; k < st; k++)
                            {
                                if (current[k] > current[k - 1])
                                {
                                    pt2 = k;
                                }
                            }


                            //============= بدست آوردن ارتفاع خالص

                            double x15 = volt[pt1], y15 = current[pt1], x25 = volt[pt2], y25 = current[pt2];
                            double shib5 = (y25 - y15) / (x25 - x15);//   (y2-y1) / (x2-x1)فرمول شیب خط
                            double x5, y5;
                            x5 = volt[Counter_num - 1];//بدست آوردن نقطه امتداد بیس لاین در انتها
                            y5 = (shib5 * x5) - (shib5 * x15) + y15;//فرمول معادله خط



                            x32 = volt[peak];//بدست آوردن ارتفاع روی بیس لاین
                            base_peak = (shib5 * x32) - (shib5 * x15) + y15;//فرمول معادله خط


                            max_jarian_khales = current[peak] - base_peak;
                            if (max_jarian_khales < 0)//برای مثبت کردن جریان خالص یا همان ارتفاع زیر پیک
                                max_jarian_khales = max_jarian_khales * (-1);

                            if (max_jarian_khales == 0)//اگر یک پیکی گرفت اما ارتفاعش 0 بود اصلا چیزی رسم نکند
                                continue;

                            if (max_jarian_khales < Convert.ToDouble(txt_height.Text))//اگر ارتفاع از مقدار داده شده کاربر کمتر است پیک نگیرد
                                continue;

                            ////===================================//پیدا کردن شروع نقطه پهنا در سمت چپ گراف

                            find_half = 0;

                            nesf = base_peak - (max_jarian_khales / 2);
                            p1 = volt[start_index]; p2 = volt[end_index];

                            for (int k = peak; k > start_index; k--)
                            {
                                if (current[k] >= nesf)
                                {
                                    if (current[k] > nesf)//در صورتی که مقدار نصف با مقدار جریان در ناحیه مرزی گراف مساوی نباشد یعنی بین دو نقطه باشد
                                    {
                                        double x130 = volt[k], y130 = current[k], x230 = volt[k + 1], y230 = current[k + 1];
                                        double shib30 = (y230 - y130) / (x230 - x130);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                        double x30, y30 = nesf;

                                        x30 = ((shib30 * x130) + (y30 - y130)) / shib30;//فرمول معادله خط

                                        p1 = x30;
                                    }
                                    else
                                        p1 = volt[k];// اگر مساوی بود که نیاز به محاسبه نیست


                                    find_half = 1;

                                    break;
                                }
                            }

                            if (find_half == 0)//اگر گراف در سمت چپ پیک نصف پیدا نکرد پس این پیک اشتباه گرفته شده است
                                continue;

                            ////============//پیدا کردن شروع نقطه پهنا در سمت راست گراف

                            find_half = 0;

                            for (int k = peak; k < Counter_num; k++)
                            {
                                if (current[k] >= nesf)
                                {
                                    if (current[k] > nesf)//در صورتی که مقدار نصف با مقدار جریان در ناحیه مرزی گراف مساوی نباشد
                                    {
                                        double x130 = volt[k], y130 = current[k], x230 = volt[k - 1], y230 = current[k - 1];
                                        double shib30 = (y230 - y130) / (x230 - x130);//   (y2-y1) / (x2-x1)فرمول شیب خط
                                        double x30, y30 = nesf;

                                        x30 = ((shib30 * x130) + (y30 - y130)) / shib30;//فرمول معادله خط

                                        p2 = x30;
                                    }
                                    else
                                        p2 = volt[k];// اگر مساوی بود که نیاز به محاسبه نیست


                                    find_half = 1;

                                    break;
                                }
                            }

                            if (find_half == 0)//اگر گراف در سمت راست پیک نصف پیدا نکرد پس این پیک اشتباه گرفته شده است
                                continue;

                            double pahna_dar_nesf = p2 - p1;
                            if (pahna_dar_nesf < 0) pahna_dar_nesf = (-1) * pahna_dar_nesf;//مثبت کردن اندازه پهنا

                            if (pahna_dar_nesf < Convert.ToDouble(txt_width.Text))//اگر پهنا از مقدار داده شده کاربر کمتر است پیک نگیرد
                                continue;


                            //======== محاسبه مساحت زیر گراف

                            //رابطه بدست آمده از انتگرال
                            //
                            //  aria = (t2 - t1)[( f(t1) + f(t2) ) / 2]

                            double t = 0, ft = 0, ft1 = 0, ft2 = 0, aria = 0;

                            double xx1 = volt[start_index], yy1 = current[start_index], xx2 = volt[end_index], yy2 = current[end_index];
                            double shibb = (yy2 - yy1) / (xx2 - xx1);//   (y2-y1) / (x2-x1)فرمول شیب خط
                            double xx, yy;

                            for (int k = Counter_num - 2; k >= start_index; k--)//حرکت معکوس
                            {
                                xx = volt[k + 1];
                                yy = (shibb * xx) - (shibb * xx1) + yy1;//فرمول معادله خط برای بدست آوردن جریان معادل در بیس لاین
                                ft1 = yy - current[k + 1];//***هر نقطه از جریان در بیس لاین منهای همان نقطه در جریان اصلی می شود تا جریان خالص زیر پیک در هر نقطه بدست آید***معکوس

                                if (ft1 < 0) ft1 = 0;//قاعدتا جریان در یک نقطه باید از بیس لاین در همان نقطه بیشتر باشد در غیر این صورت این نقطه از بیس لاین خارج از محدوده است

                                xx = volt[k];
                                yy = (shibb * xx) - (shibb * xx1) + yy1;//فرمول معادله خط
                                ft2 = yy - current[k];

                                if (ft2 < 0) ft2 = 0;

                                t = volt[k] - volt[k + 1];
                                ft = (ft1 + ft2) / 2;
                                aria = aria + (t * ft);
                            }



                            chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم بیس لاین
                            chart4.Series[ser].ChartType = SeriesChartType.Spline;
                            chart4.Series[ser].Color = Color.Red;
                            chart4.Series[ser].BorderWidth = 1;
                            chart4.Series[ser].Points.AddXY(volt[pt1], current[pt1]);
                            chart4.Series[ser].Points.AddXY(volt[pt2], current[pt2]);
                            chart4.Series[ser].Points.AddXY(x5, y5);
                            ser++;


                            string a = aria.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                            try { a = a.Substring(0, a.IndexOf('.') + 6); } catch { }
                            chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//نمایش اندازه مساحت
                            chart4.Series[ser].ChartType = SeriesChartType.Spline;
                            chart4.Series[ser].Color = Color.Red;
                            chart4.Series[ser].BorderWidth = 1;
                            chart4.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                            chart4.Series[ser].Points[0].Label = "Aria Backward: " + a;
                            Array.Resize(ref series_area_index, series_area_index.Length + 1);//قرار دادن اندیس برای نمایش با چک باکس
                            series_area_index[series_area_index.Length - 1] = ser;
                            ser++;

                            string height = max_jarian_khales.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                            try { height = height.Substring(0, height.IndexOf('.') + 4); } catch { }
                            chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم ارتفاع یا همان جریان خالص
                            chart4.Series[ser].ChartType = SeriesChartType.Spline;
                            chart4.Series[ser].Color = Color.Gold;
                            chart4.Series[ser].BorderWidth = 1;
                            chart4.Series[ser].Points.AddXY(volt[peak], current[peak]);
                            chart4.Series[ser].Points.AddXY(volt[peak], base_peak);
                            chart4.Series[ser].Points[0].Label = "H: " + height;
                            Array.Resize(ref series_height_index, series_height_index.Length + 1);
                            series_height_index[series_height_index.Length - 1] = ser;
                            ser++;


                            string w = pahna_dar_nesf.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                            try { w = w.Substring(0, w.IndexOf('.') + 4); } catch { }
                            chart4.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم پهنا در نصف ارتفاع
                            chart4.Series[ser].ChartType = SeriesChartType.Spline;
                            chart4.Series[ser].Color = Color.SpringGreen;
                            chart4.Series[ser].BorderWidth = 1;
                            chart4.Series[ser].Points.AddXY(p1, nesf);
                            chart4.Series[ser].Points.AddXY(p2, nesf);
                            chart4.Series[ser].Points[0].Label = "W: " + w;
                            Array.Resize(ref series_width_index, series_width_index.Length + 1);
                            series_width_index[series_width_index.Length - 1] = ser;
                            ser++;
                        }
                    }

                    ch_height_CheckedChanged(sender, e);
                    ch_width_CheckedChanged(sender, e);
                    ch_area_CheckedChanged(sender, e);
                }
                else
                    MessageBox.Show("graph is not available.");
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }

            this.Cursor = Cursors.Default;
        }

        System.Drawing.PointF p1 = new System.Drawing.PointF(10, 10), p2 = new System.Drawing.PointF(100, 100);

        System.Drawing.PointF[] p1_arr = new System.Drawing.PointF[0];

        private void toolStrip2_Click(object sender, EventArgs e)
        {
            pnl_peak_search.Visible = false;
        }

        private void tChart2_Click_1(object sender, EventArgs e)
        {

        }

      
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                chart4.Series[0].ChartType = SeriesChartType.Line;
                chart4.Series[0].BorderWidth = 2;
            }
            catch { }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                chart4.Series[0].ChartType = SeriesChartType.Point;
                chart4.Series[0].BorderWidth = 1;
            }
            catch { }
        }

        int[] series_height_index = new int[0];
        private void ch_height_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < series_height_index.Length; i++)
            {
                if (ch_height.Checked == true)
                    chart4.Series[series_height_index[i]].Enabled = true;
                else
                    chart4.Series[series_height_index[i]].Enabled = false;
            }
        }

        int[] series_width_index = new int[0];
        private void ch_width_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < series_width_index.Length; i++)
            {
                if (ch_width.Checked == true)
                    chart4.Series[series_width_index[i]].Enabled = true;
                else
                    chart4.Series[series_width_index[i]].Enabled = false;
            }
        }

        int[] series_area_index = new int[0];
        private void ch_area_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < series_area_index.Length; i++)
            {
                if (ch_area.Checked == true)
                    chart4.Series[series_area_index[i]].Enabled = true;
                else
                    chart4.Series[series_area_index[i]].Enabled = false;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog cl = new ColorDialog();
                cl.FullOpen = true;

                if (cl.ShowDialog() == DialogResult.OK)
                {
                    string[] color = File.ReadAllLines(System.Windows.Forms.Application.StartupPath + "\\Color.dat");
                    color[7] = "2065 graph analysis:" + cl.Color.ToArgb().ToString();
                    File.WriteAllLines(System.Windows.Forms.Application.StartupPath + "\\Color.dat", color);

                    //for (int i = 0; i < chart3.Series.Count; i++)
                    int i = treeView3.SelectedNode.Index;
                    chart3.Series[i].Color = cl.Color;

                    try { chart4.Series[0].Color = cl.Color; } catch { }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            if (chart4.Series.Count > 0)
            {
                forms.frm_big_analys frm = new forms.frm_big_analys();
                frm.WindowState = FormWindowState.Maximized;
               
                foreach (System.Windows.Forms.DataVisualization.Charting.Series ser in chart4.Series)
                    frm.chart1.Series.Add(ser);
                
                frm.chart1.BackColor = chart3.BackColor;
                frm.chart1.BorderlineColor = chart3.BackColor;
                frm.chart1.ChartAreas[0].BackColor = chart3.BackColor;
                frm.chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                frm.chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

                if (chart4.ChartAreas[0].AxisX.MajorGrid.Enabled == true)
                {
                    frm.chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                    frm.chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                }
                frm.ShowDialog();
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            new forms.frmShowPeaks().ShowDialog();
        }

        private void Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void TbsClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Chart4_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (statebutton_1.Trim() == "drawline")
                {
                    if (e.Button == MouseButtons.Left && drawline1.EnableDraw == true)
                    {
                        point_start = new System.Drawing.Point(e.X, e.Y);
                        TempPoint = new System.Drawing.Point(e.X, e.Y);
                    }
                }
                else
                {
                    mDown = chart3.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X);
                    mDowny = chart3.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y);
                }

                double x = chart4.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X);
                double y = chart4.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y);

                if (start_click == 1)
                {
                    chart4.Series[1].ChartType = SeriesChartType.Point;
                    chart4.Series[1].Color = Color.Red;
                    chart4.Series[1].BorderWidth = 3;
                    draw_point(x, y);//پیدا کردن نزدیکترین نقطه به نقطه رسم شده در گراف    
                }
                else if (end_click == 1)
                {
                    chart4.Series[1].ChartType = SeriesChartType.Spline;
                    chart4.Series[1].Color = Color.Red;
                    chart4.Series[1].BorderWidth = 1;
                    draw_point(x, y);

                    end_click = 0;
                    this.Cursor = Cursors.Default;
                    chart4.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void Chart4_Paint(object sender, PaintEventArgs e)
        {
            if (!point_start.IsEmpty)
                e.Graphics.DrawLine(Pens.White, point_start, TempPoint);

            for (int p = 0; p < drawline1.Lines.Count; p++)
            {
                if (p == numline)
                    e.Graphics.DrawLine(Pens.Red, drawline1.Lines[p].point1, drawline1.Lines[p].point2);
                else
                    e.Graphics.DrawLine(Pens.White, drawline1.Lines[p].point1, drawline1.Lines[p].point2);
            }

        }

        private void Chart3_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                System.Windows.Forms.DataVisualization.Charting.Axis ax = chart3.ChartAreas[0].AxisX;
                mDown = ax.PixelPositionToValue(e.Location.X);
                System.Windows.Forms.DataVisualization.Charting.Axis ay = chart3.ChartAreas[0].AxisY;
                mDowny = ay.PixelPositionToValue(e.Location.Y);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void Chart3_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {

                var results11 = chart3.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                foreach (var result in results11)
                {
                    if (result.ChartElementType == ChartElementType.PlottingArea)
                    {

                        switch (statebutton)
                        {                        
                            case "selectonly":
                                chart3.Cursor = System.Windows.Forms.Cursors.Cross;
                                break;
                            case "showvalue":
                                chart3.Cursor = System.Windows.Forms.Cursors.SizeAll;

                                break;
                            case "zoomin":
                                //
                                chart3.Cursor =System.Windows.Forms.Cursors.Hand;// LoadCursorFromResource(Skydat.Properties.Resources.hard_disk_32);

                                break;
                            case "zoomout":
                                //
                           //     chart3.Cursor = LoadCursorFromResource(BHP2190.Properties.Resources.zoom_out);

                                break;
                            default:
                                chart3.Cursor = System.Windows.Forms.Cursors.Default;
                                break;
                        }

                    }
                    else
                        chart3.Cursor = System.Windows.Forms.Cursors.Default;


                }
                switch (statebutton)
                {               
                    case "selectonly":
                        System.Drawing.Point mousePoint = new System.Drawing.Point(e.X, e.Y);
                        chart3.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, false);
                        chart3.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, false);

                        break;
                    case "showvalue":
                        var results12 = chart3.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                        double x111 = 0, y111 = 0;
                        foreach (var result in results12)
                        {

                            if (result.ChartElementType == ChartElementType.PlottingArea)
                            {
                                x111 = result.ChartArea.AxisX.PixelPositionToValue(e.X);
                                y111 = result.ChartArea.AxisY.PixelPositionToValue(e.Y);
                            }
                        }
                        mainf.xyLabel.Text = x111.ToString("#.##") + "," + y111.ToString("#.#e-0");

                        break;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            mDown = double.NaN;
            mDowny = double.NaN;
            switch (statebutton)
            {

                case "zoomin":
                    var results11 = chart3.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                    foreach (var result in results11)
                    {
                        if (result.ChartElementType == ChartElementType.PlottingArea)
                        {
                            this.chart3.ChartAreas[0].AxisX.ScaleView.Zoomable = true;


                            double xMin = chart3.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                            double xMax = chart3.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                            double yMin = chart3.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                            double yMax = chart3.ChartAreas[0].AxisY.ScaleView.ViewMaximum;                             
                            chart3.ChartAreas[0].AxisX.ScaleView.Zoom(xMin, 0.95 * (xMax-xMin), DateTimeIntervalType.Number, true);
                            chart3.ChartAreas[0].AxisY.ScaleView.Zoom(yMin, 0.95 * (yMax-yMin), DateTimeIntervalType.Number, true);
                        }
                    }
                    break;
               case "zoomout":
                    var results12 = chart3.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                    foreach (var result in results12)
                    {
                        if (result.ChartElementType == ChartElementType.PlottingArea)
                        {
                            this.chart3.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                            chart3.ChartAreas[0].AxisX.ScaleView.ZoomReset(1);
                            chart3.ChartAreas[0].AxisY.ScaleView.ZoomReset(1);

                        }
                    }
                    break;
            }

        }

        private void Chart4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 108 || e.KeyValue == 76)
            {
                numline++;
                if (numline >= drawline1.Lines.Count)
                    numline = 0;
                chart4.Refresh();

            }
            else if (e.KeyValue == 189)
            {
                if (numline > -1 && numline < drawline1.Lines.Count)
                {
                    drawline1.remove(numline);
                    numline--;
                    chart4.Refresh();
                }


            }
        }

        private void Chart4_MouseUp(object sender, MouseEventArgs e)
        {
            if (statebutton_1.Trim() == "drawline")
            {
                if (e.Button == MouseButtons.Left && drawline1.EnableDraw == true)
                {
                    point_end = new System.Drawing.Point(e.X, e.Y);
                    drawline_chart.lines l1;
                    l1.point1 = point_start;
                    l1.point2 = point_end;
                    drawline1.Lines.Add(l1);
                    // endline = true;
                    point_start = System.Drawing.Point.Empty;
                    //MessageBox.Show("drawline1.Lines.count=" + drawline1.Lines.Count.ToString());

                }
            }

        }

        private void Chart4_MouseMove(object sender, MouseEventArgs e)
        {
            //label2.Text = chart4.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X).ToString() + " " + chart4.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y).ToString();
            try
            {
                if (statebutton_1.Trim() == "")
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        var results1 = chart3.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                        foreach (var result in results1)
                        {
                            if (result.ChartElementType == ChartElementType.PlottingArea)
                            {
                                double xv = chart4.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X);
                                double yv = chart4.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y);
                                if (xv - mDown > 0)
                                    chart4.ChartAreas[0].AxisX.ScaleView.Position = chart4.ChartAreas[0].AxisX.ScaleView.ViewMinimum + (difxview2 / 500);
                                else if (xv - mDown < 0)
                                    chart4.ChartAreas[0].AxisX.ScaleView.Position = chart4.ChartAreas[0].AxisX.ScaleView.ViewMinimum - (difxview2 / 500);
                                else
                                    chart4.ChartAreas[0].AxisX.ScaleView.Position = chart4.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                                if ((yv - mDowny) > 0)
                                    chart4.ChartAreas[0].AxisY.ScaleView.Position = chart4.ChartAreas[0].AxisY.ScaleView.ViewMinimum + (difyview2 / 500);
                                else if ((yv - mDowny) < 0)
                                    chart4.ChartAreas[0].AxisY.ScaleView.Position = chart4.ChartAreas[0].AxisY.ScaleView.ViewMinimum - (difyview2 / 500);
                                else
                                    chart4.ChartAreas[0].AxisY.ScaleView.Position = chart4.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                                chart4.ChartAreas[0].AxisX.ScaleView.Size = difxview2; //(chart3.ChartAreas[0].AxisX.ScaleView.ViewMaximum - chart3.ChartAreas[0].AxisX.ScaleView.ViewMinimum); //+ xv - mDown;chart3.ChartAreas[0].AxisX.Minimum +
                                chart4.ChartAreas[0].AxisY.ScaleView.Size = difyview2;// (chart3.ChartAreas[0].AxisY.ScaleView.ViewMaximum - chart3.ChartAreas[0].AxisY.ScaleView.ViewMinimum) + yv - mDowny;//chart3.ChartAreas[0].AxisX.Minimum +

                                mDown = xv;
                                mDowny = yv;
                            }

                        }
                    }
                }

                var results11 = chart4.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                foreach (var result in results11)
                {
                    if (result.ChartElementType == ChartElementType.PlottingArea)
                    {

                        switch (statebutton_1)
                        {
                            case "selectonly":
                                chart4.Cursor = System.Windows.Forms.Cursors.Cross;
                                break;
                            case "showvalue":
                                chart4.Cursor = System.Windows.Forms.Cursors.SizeAll;

                                break;

                            default:
                                if (start_click == 0 && end_click == 0)
                                    chart4.Cursor = System.Windows.Forms.Cursors.Default;
                                break;
                        }

                    }
                    else if (start_click == 0 && end_click == 0)
                        chart4.Cursor = System.Windows.Forms.Cursors.Default;


                }
                switch (statebutton_1)
                {
                    case "selectonly":
                        System.Drawing.Point mousePoint = new System.Drawing.Point(e.X, e.Y);
                        chart4.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, false);
                        chart4.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, false);

                        break;
                    case "showvalue":
                        var results12 = chart4.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                        double x111 = 0, y111 = 0;
                        foreach (var result in results12)
                        {
                            if (result.ChartElementType == ChartElementType.PlottingArea)
                            {
                                x111 = result.ChartArea.AxisX.PixelPositionToValue(e.X);
                                y111 = result.ChartArea.AxisY.PixelPositionToValue(e.Y);
                            }
                        }

                        string x = x111.ToString("#.##");

                        if (x111 < 1)//قرار دادن صفر قبل از ممیز
                        {
                            if (x111 >= 0)
                                x = "0" + x;
                            else
                            {
                                x = (x111 * -1).ToString("#.##");
                                x = "-0" + x;
                            }
                        }

                        mainf.xyLabel.Text = x + "," + y111.ToString("#.#e-0");
                        //mainf.xyLabel.Text = x111.ToString("#.##") + "," + y111.ToString("#.#e-0");

                        break;
                    case "drawline":
                        if (e.Button == MouseButtons.Left && drawline1.EnableDraw == true)
                        {
                            if (!(point_start == null))
                            {
                                //MessageBox.Show("point_start=" + point_start.ToString());
                                Graphics g = this.CreateGraphics();
                                Pen ErasePen = new Pen(Color.White);
                                g.DrawLine(ErasePen, point_start, TempPoint);
                                TempPoint = new System.Drawing.Point(e.X, e.Y);
                                this.chart4.Refresh();
                            }
                        }
                        break;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void tChart2_Paint(object sender, PaintEventArgs e)
        {
                if (!point_start.IsEmpty)
                    e.Graphics.DrawLine(Pens.White, point_start, TempPoint);

                for (int p = 0; p < drawline1.Lines.Count; p++)
                {
                    if (p == numline)
                        e.Graphics.DrawLine(Pens.Red, drawline1.Lines[p].point1, drawline1.Lines[p].point2);
                    else
                        e.Graphics.DrawLine(Pens.White, drawline1.Lines[p].point1, drawline1.Lines[p].point2);
                }
        }
        private void btnzoomin_1_Click(object sender, EventArgs e)
        {
            statebutton_1 = "";
            selectpchart_func_1(false);
            this.chart4.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            double xMin = chart4.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            double xMax = chart4.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
            double yMin = chart4.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
            double yMax = chart4.ChartAreas[0].AxisY.ScaleView.ViewMaximum;
            chart4.ChartAreas[0].AxisX.ScaleView.Zoom(xMin, 0.95 * (xMax - xMin), DateTimeIntervalType.Auto, true);
            chart4.ChartAreas[0].AxisY.ScaleView.Zoom(yMin, 0.95 * (yMax - yMin), DateTimeIntervalType.Auto, true);
            difxview2 = chart4.ChartAreas[0].AxisX.ScaleView.ViewMaximum - chart4.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            difyview2 = chart4.ChartAreas[0].AxisY.ScaleView.ViewMaximum - chart4.ChartAreas[0].AxisY.ScaleView.ViewMinimum;

            chart4.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            chart4.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
            chart4.ChartAreas[0].AxisX.ScrollBar.BackColor = Color.DarkGray;
            chart4.ChartAreas[0].AxisY.ScrollBar.BackColor = Color.DarkGray;
        }

        private void btnzoomout_1_Click(object sender, EventArgs e)
        {
            statebutton_1 = "";
            selectpchart_func_1(false);
            this.chart4.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart4.ChartAreas[0].AxisX.ScaleView.ZoomReset(1);
            chart4.ChartAreas[0].AxisY.ScaleView.ZoomReset(1); 
        }

        private void btnshowgrid_1_Click(object sender, EventArgs e)
        {
            statebutton_1 = "";
            selectpchart_func_1(false);
            this.chart4.ChartAreas[0].AxisX.MajorGrid.Enabled = !this.chart4.ChartAreas[0].AxisX.MajorGrid.Enabled;
            this.chart4.ChartAreas[0].AxisY.MajorGrid.Enabled = !this.chart4.ChartAreas[0].AxisY.MajorGrid.Enabled;
          
        }

        private void btnshowaxes_1_Click(object sender, EventArgs e)
        {
            statebutton_1 = "";
            selectpchart_func_1(false);
            chart4.ChartAreas[0].AxisX.LabelStyle.Enabled = !chart4.ChartAreas[0].AxisX.LabelStyle.Enabled;
            chart4.ChartAreas[0].AxisX.MajorTickMark.Enabled = !chart4.ChartAreas[0].AxisX.MajorTickMark.Enabled;
            chart4.ChartAreas[0].AxisY.LabelStyle.Enabled = !chart4.ChartAreas[0].AxisY.LabelStyle.Enabled;
            chart4.ChartAreas[0].AxisY.MajorTickMark.Enabled = !chart4.ChartAreas[0].AxisY.MajorTickMark.Enabled;
      
        }
        double mDown = double.NaN, mDowny = double.NaN, difxview = double.NaN, difyview = double.NaN, difxview2 = double.NaN, difyview2 = double.NaN;
        private void tChart1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                System.Windows.Forms.DataVisualization.Charting.Axis ax = chart3.ChartAreas[0].AxisX;
                mDown = ax.PixelPositionToValue(e.Location.X);
                System.Windows.Forms.DataVisualization.Charting.Axis ay = chart3.ChartAreas[0].AxisY;
                mDowny = ay.PixelPositionToValue(e.Location.Y);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (statebutton_1.Trim() == "drawline")
            {
                statebutton_1 = "";
                //label8.Text = "";
                selectpchart_func_1(false);
            }
            else
            {
                statebutton_1 = "drawline";
                //label8.Text = "drawline";
                //selectpchart_func_1(true);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            forms.frmselecttypeshow frm = new frmselecttypeshow();
            frm.StartPosition=FormStartPosition.CenterScreen;
            if (frm.ShowDialog() == DialogResult.OK) {
                if(frm.type_txt_y.Trim()!="")
                    chart4.ChartAreas[0].AxisY.LabelStyle.Format = frm.type_txt_y;
                if (frm.type_txt_x.Trim() != "")
                    chart4.ChartAreas[0].AxisX.LabelStyle.Format = frm.type_txt_x;
                chart4.ChartAreas[0].RecalculateAxesScale();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            forms.frmselecttypeshow frm = new frmselecttypeshow();
            frm.StartPosition = FormStartPosition.CenterScreen;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.type_txt_y.Trim() != "")
                    chart3.ChartAreas[0].AxisY.LabelStyle.Format = frm.type_txt_y;
                if (frm.type_txt_x.Trim() != "")
                    chart3.ChartAreas[0].AxisX.LabelStyle.Format = frm.type_txt_x;
                chart3.ChartAreas[0].RecalculateAxesScale();
            }

        } 
    }
}