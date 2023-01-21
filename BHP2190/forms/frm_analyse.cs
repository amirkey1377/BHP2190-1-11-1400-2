using BHP2190.classes;
using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;

namespace BHP2190.forms
{
    public partial class frm_analyse : Form
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

        public frm_analyse()
        {
            InitializeComponent();

            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;//قرار گرفتن زیر تسکبار
            this.chart2.Titles.Clear();
            Title header1 = this.chart2.Titles.Add("Header");
            header1.Alignment = ContentAlignment.TopLeft;
            header1.Docking = Docking.Top;
            this.chart2.Titles[0].Text = "";
            this.chart2.Titles[0].Alignment = ContentAlignment.TopCenter;
            this.chart2.Titles[0].ForeColor = Color.Black;
            // chart1.ChartAreas.Add("a1");
            //chart1.Legends.Add("a1");
            this.chart1.ChartAreas[0].Area3DStyle.Enable3D = false;

            this.chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
            chart1.ChartAreas[0].AxisX.LineColor = Color.Black;
            chart1.ChartAreas[0].AxisY.LineColor = Color.Black;

            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
            //chart1.ChartAreas[0].AxisY.Title = "coulomb(c)";
            //chart1.ChartAreas[0].AxisX.Title = "Time(s)";
            chart1.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;

            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "#.#e-0";
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "#0.##";
            this.chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            this.chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            this.chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            this.chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            this.chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Black;
            this.chart1.ChartAreas[0].AxisX.MinorGrid.LineColor = Color.Black;
            this.chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Black;
            this.chart1.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.Black;
            this.chart1.ChartAreas[0].AxisX.ScrollBar.Enabled = false;
            this.chart1.ChartAreas[0].AxisY.ScrollBar.Enabled = false;

            this.chart1.ChartAreas[0].AxisX.ScrollBar.BackColor = Color.Black;
            this.chart1.ChartAreas[0].AxisY.ScrollBar.BackColor = Color.Black;
            //   chart2.ChartAreas.Add("b1");
            // chart2.Legends.Add("b1");
            this.chart2.ChartAreas[0].Area3DStyle.Enable3D = false;

            this.chart2.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
            this.chart2.ChartAreas[0].AxisX.LineColor = Color.Black;
            chart2.ChartAreas[0].AxisY.LineColor = Color.Black;

            chart2.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
            //chart2.ChartAreas[0].AxisY.Title = "coulomb(c)";
            //chart2.ChartAreas[0].AxisX.Title = "Time(s)";
            chart2.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
            chart2.ChartAreas[0].AxisY.TitleForeColor = Color.Black;

            chart2.ChartAreas[0].AxisY.LabelStyle.Format = "#.#e-0";
            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "#0.##";
            this.chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            this.chart2.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            this.chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            this.chart2.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            this.chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Black;
            this.chart2.ChartAreas[0].AxisX.MinorGrid.LineColor = Color.Black;
            this.chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Black;
            this.chart2.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.Black;
            this.chart2.ChartAreas[0].AxisX.ScrollBar.BackColor = Color.Black;
            this.chart2.ChartAreas[0].AxisY.ScrollBar.BackColor = Color.Black;
            this.chart2.ChartAreas[0].AxisX.ScrollBar.Enabled = false;
            this.chart2.ChartAreas[0].AxisY.ScrollBar.Enabled = false;

        }
        private System.Drawing.Rectangle rectsel;
        public drawline_chart drawline1;

        public string operation = "";
        public System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private string statebutton = "", statebutton_1 = "";
        bool smooth_state = false;
        public int num_Series = 0;
        public byte tech = 0;
        public string techName = "";
         
        public int[] Overlay_tech = new int[1000];
        public int grfNo = 0, nindexchart2 = 0;

        public double ContactpointX = 0, contactpointY = 0;
        public bool isSelectedLine = false;
        int startpoint_selx = -1, startpoint_sely = -1, endpoint_selx = -1, endpoint_sely = -1;
        
        private int shomar_val = 0;
        double[] othertech_val0 = new double[200];
        double[] othertech_val1 = new double[200];
        KeyEventArgs ee = new KeyEventArgs(Keys.Alt);
        
        public double[] x_value = new double[0];//ستون اول دیتا موجود در فایل 
        public double[] y_value = new double[0];//ستون دوم دیتا موجود در فایل 
        public double[] z_value = new double[0];//ستون سوم دیتا موجود در فایل 
        public double[] w_value = new double[0];//ستون چهارم دیتا موجود در فایل 
        public frmOption opForm = new frmOption();
       // public frmselecttech sf;
        public frm_main mainf;
        public clasMain mc = new clasMain();
        public clasPeak cPeak = new clasPeak();
        public frmShowPeaks fPeak;
        bool mouseIsDown = false;
        System.Drawing.Point firstPoint;

        int row_sig = 0;
        double[] volt = new double[300];
        double[] current = new double[300];
        int Counter_num = 0;
        int[] series_height_index = new int[0];

        Thread th_load;

        public int start_click = 0;
        public int end_click = 0;

        public bool overlaymain = false;
        public string path_open_file = "";//اگر در ویندوز فایل دیتا را کلیک کند

        private System.Drawing.Point point_start, TempPoint, point_end;
        int numline = -1;

        int[] series_area_index = new int[0];
        int[] series_width_index = new int[0];

        double mDown = double.NaN, mDowny = double.NaN, difxview = double.NaN, difyview = double.NaN, difxview2 = double.NaN, difyview2 = double.NaN;

        private void frm_analyse_Load(object sender, EventArgs e)
        {
            try
            {
                 drawline1 = new drawline_chart();

                if (opForm.cbDrawLine.Checked)
                    Set_line_Setting();
                else
                    drawline1.Active = false;

                string[] color = File.ReadAllLines(System.Windows.Forms.Application.StartupPath + "\\Color.dat");
                string s = color[1].ToString().Substring(color[1].ToString().IndexOf(":") + 1);
                Color cl = Color.FromArgb(Convert.ToInt32(s));
                chart1.BackColor = cl;
                chart1.BorderlineColor = cl;
                chart1.ChartAreas[0].BackColor = cl;
                chart2.BackColor = cl;
                chart2.BorderlineColor = cl;
                chart2.ChartAreas[0].BackColor = cl;
                panel1.BackColor = cl;
                panel2.BackColor = cl;
//                panel8.BackColor = cl;

                s = color[7].ToString().Substring(color[7].ToString().IndexOf(":") + 1);
                cl = Color.FromArgb(Convert.ToInt32(s));
                for (int i = 0; i < chart1.Series.Count; i++)
                    chart1.Series[i].Color = cl;

                //if (path_open_file != "")
                //    open_file();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        public Color drawColor = new Color();

        public void Set_line_Setting()
        {
            drawline1.EnableDraw = opForm.cbDrawLine.Checked;
            drawline1.pen_ch.Color = drawColor;
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
                        double Step = Math.Abs(chart1.Series[0].Points[1].XValue - chart1.Series[0].Points[1].XValue);
                        int X1 = sender.FromPoint.X;
                        int Y1 = sender.FromPoint.Y;
                        int X2 = sender.ToPoint.X;
                        int Y2 = sender.ToPoint.Y;
                        var results1 = chart1.HitTest(X1, Y1, false, ChartElementType.PlottingArea);
                        double X_V1 = 0, Y_V1 = 0, X_V2 = 0, Y_V2 = 0;
                        foreach (var result in results1)
                        {
                            if (result.ChartElementType == ChartElementType.PlottingArea)
                            {
                                X_V1 = result.ChartArea.AxisX.PixelPositionToValue(X1);
                                Y_V1 = result.ChartArea.AxisY.PixelPositionToValue(Y1);

                            }
                        }
                        var results2 = chart1.HitTest(X2, Y2, false, ChartElementType.PlottingArea);
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
                            XX[i] = Convert.ToDouble(chart1.Series[0].Points[F + i].XValue.ToString());
                            YY[i] = Convert.ToDouble(chart1.Series[0].Points[F + i].YValues[0].ToString());
                        }
                        try
                        {

                            //fPeak.richTextBox1.Text += cPeak.manual(tChart1.Series[0].,XValues.Value, tChart1.Series[0].YValues.Value, X_V1, Y_V1, X_V2, Y_V2, (int)(Math.Abs(P2 - P1) / Step));
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

        private void Chart1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    double xv = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X);
                    double yv = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y);
                    if (xv - mDown > 0)
                        chart1.ChartAreas[0].AxisX.ScaleView.Position = chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum + (difxview / 500);
                    else if (xv - mDown < 0)
                        chart1.ChartAreas[0].AxisX.ScaleView.Position = chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum - (difxview / 500);
                    else
                        chart1.ChartAreas[0].AxisX.ScaleView.Position = chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    if ((yv - mDowny) > 0)
                        chart1.ChartAreas[0].AxisY.ScaleView.Position = chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum + (difyview / 500);
                    else if ((yv - mDowny) < 0)
                        chart1.ChartAreas[0].AxisY.ScaleView.Position = chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum - (difyview / 500);
                    else
                        chart1.ChartAreas[0].AxisY.ScaleView.Position = chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    chart1.ChartAreas[0].AxisX.ScaleView.Size = difxview; //(tChart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum - tChart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum); //+ xv - mDown;tChart1.ChartAreas[0].AxisX.Minimum +
                    chart1.ChartAreas[0].AxisY.ScaleView.Size = difyview;// (tChart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum - tChart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum) + yv - mDowny;//tChart1.ChartAreas[0].AxisX.Minimum +

                    mDown = xv;
                    mDowny = yv;

                }
                else
                {
                    var results11 = chart1.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                    foreach (var result in results11)
                    {
                        if (result.ChartElementType == ChartElementType.PlottingArea)
                        {

                            switch (statebutton)
                            {
                                case "selectonly":
                                    chart1.Cursor = System.Windows.Forms.Cursors.Cross;
                                    break;
                                case "showvalue":
                                    chart1.Cursor = System.Windows.Forms.Cursors.SizeAll;

                                    break;
                                default:
                                    chart1.Cursor = System.Windows.Forms.Cursors.Default;
                                    break;
                            }

                        }
                        else
                            chart1.Cursor = System.Windows.Forms.Cursors.Default;


                    }
                    switch (statebutton)
                    {
                        case "selectonly":
                            System.Drawing.Point mousePoint = new System.Drawing.Point(e.X, e.Y);
                            chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, false);
                            chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, false);

                            break;
                        case "showvalue":
                            var results12 = chart1.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
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
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btn_show_geraph_Click(object sender, EventArgs e)
        {
            try
            {
                drawline1.EnableDraw = false;
                drawline1.removeall();
                int shomarcheck = 0, ii = 0;

                for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                    if (treeView1.Nodes[0].Nodes[i].Checked)
                    {
                        shomarcheck++;
                        ii = i;
                    }
                if (shomarcheck == 1)
                {
                    treeView1.SelectedNode = treeView1.Nodes[0].Nodes[ii];

                    restore_Data_part(dataSet11.analyselist.Rows[ii][21].ToString(), 1, 1);




                    operation = "show_without_smoothing";
                    show_Graph(0, this, "SubChart", othertech_val0, othertech_val1, true, shomar_val);
                    nindexchart2 = ii;
                }
                else
                    MessageBox.Show("show graph without smoothing Operation work with one graph" + (char)13 + "Please select ONE only!!!!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");


            }

        }
        
        public void show_Graph(int num_ser, frm_analyse grfn, string main_subchart, double[] xx, double[] yy, bool overlaymain, int shomaresh)
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
                chart1.Series[num_ser].Points.DataBindXY(xx, yy);
            }
            if (main_subchart == "SubChart")
            {

                if (operation == "i_Reverse" || operation == "i_Forward" || operation == "Integral" || operation == "Derivation" || operation == "Mathematics" || operation == "Overlay" || operation == "Tafel" || operation == "show_without_smoothing")
                {
                    chart2.Series.Clear();
                    chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());
                    chart2.Series[0].ChartType = SeriesChartType.FastLine;
                    chart2.Titles[0].Text = operation;
                    chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = this.chart1.ChartAreas[0].AxisX.MajorGrid.Enabled;
                    chart2.ChartAreas[0].AxisX.MinorGrid.Enabled = this.chart1.ChartAreas[0].AxisX.MinorGrid.Enabled;
                    chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = this.chart1.ChartAreas[0].AxisY.MajorGrid.Enabled;
                    chart2.ChartAreas[0].AxisY.MinorGrid.Enabled = this.chart1.ChartAreas[0].AxisY.MinorGrid.Enabled;


                    chart2.ChartAreas[0].AxisX.LabelStyle.Format = "#0e-0";
                    chart2.ChartAreas[0].AxisY.LabelStyle.Format = "#0e-0";
                    if (operation == "i_Forward")
                        smootht4(0, chart2.Series[0].Color, 2, 3);
                    else if (operation == "i_Reverse")
                        smootht4(0, chart2.Series[0].Color, 2, 4);
                    else
                        chart2.Series[0].Points.DataBindXY(xx, yy);
                    chart2.ChartAreas[0].RecalculateAxesScale();
                    chart2.Refresh();

                }
            }
            difxview2 = chart2.ChartAreas[0].AxisX.ScaleView.ViewMaximum - chart2.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            difyview2 = chart2.ChartAreas[0].AxisY.ScaleView.ViewMaximum - chart2.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
        }

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
                    //if (overlayopen == 1 || this.grf.tChart1.Series.Count < 1)
                    //    this.grf.Close();

                    //isOkfile = false;
                }
                Fl.Close();


            }

        }
        
        private void btn_mathematic_operation_Click(object sender, EventArgs e)
        {
            drawline1.EnableDraw = false;
            drawline1.removeall();
            int shomarcheck = 0, i1 = 0;
            for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                if (treeView1.Nodes[0].Nodes[i].Checked)
                {
                    shomarcheck++;
                    i1 = i;
                }
            if (shomarcheck == 1)
            {
                frm_math mthForm = new frm_math(i1);
                mthForm.gf = this;

                mthForm.TopMost = true;
                mthForm.ShowDialog();
                nindexchart2 = i1;
            }
            else
                MessageBox.Show("Mathematical Operation work with one graph" + (char)13 + "Please select ONE only!!!!!!");

        }
        
        public void mathematicf(int XorY, string txt, double value, int i)
        {

            double[] P = new double[this.chart1.Series[i].Points.Count];
            double[] I = new double[this.chart1.Series[i].Points.Count];
            for (int k = 0; k < this.chart1.Series[i].Points.Count; k++)
            {
                P[k] = this.chart1.Series[i].Points[k].XValue;
                I[k] = this.chart1.Series[i].Points[k].YValues[0];
            }
            mc.Math_Operation(P, I, XorY, txt, value);


            // tChart1.Series[num_Series].Clear();
            operation = "Mathematics";
            show_Graph(0, this, "SubChart", mc.AR, mc.BR, true, mc.AR.Length);

        }

        private void btn_integral_operation_Click(object sender, EventArgs e)
        {
            drawline1.EnableDraw = false;
            drawline1.removeall();
            int i1 = 0;
            int shomarcheck = 0;
            for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                if (treeView1.Nodes[0].Nodes[i].Checked)
                {
                    shomarcheck++;
                    i1 = i;
                }
            if (shomarcheck == 1)
            {
                operation = "Integral";
                mc.tq = tech;
                double[] P = new double[this.chart1.Series[i1].Points.Count];
                double[] I = new double[this.chart1.Series[i1].Points.Count];
                for (int k = 0; k < this.chart1.Series[i1].Points.Count; k++)
                {
                    P[k] = this.chart1.Series[i1].Points[k].XValue;
                    I[k] = this.chart1.Series[i1].Points[k].YValues[0];

                }
                mc.Integral(P, I, 0, 10);
                show_Graph(0, this, "SubChart", mc.AR, mc.BR, true, mc.AR.Length);
                nindexchart2 = i1;
            }
            else
                MessageBox.Show("Integration work with one graph" + (char)13 + "Please select ONE only!!!");

        }

        private void btn_derivative_operation_Click(object sender, EventArgs e)
        {
            drawline1.EnableDraw = false;
            drawline1.removeall();
            int i1 = 0;
            int shomarcheck = 0;
            for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                if (treeView1.Nodes[0].Nodes[i].Checked)
                {
                    shomarcheck++;
                    i1 = i;
                }
            if (shomarcheck == 1)
            {
                operation = "Derivation";
                mc.tq = tech;
                double[] P = new double[this.chart1.Series[i1].Points.Count];
                double[] I = new double[this.chart1.Series[i1].Points.Count];
                for (int k = 0; k < this.chart1.Series[i1].Points.Count; k++)
                {
                    P[k] = this.chart1.Series[i1].Points[k].XValue;
                    I[k] = this.chart1.Series[i1].Points[k].YValues[0];

                }
                mc.Derivate(P, I);
                show_Graph(0, this, "SubChart", mc.AR, mc.BR, true, mc.AR.Length);
                nindexchart2 = i1;
            }
            else
                MessageBox.Show("derivation work with one graph" + (char)13 + "Please select ONE only!!!");

        }

        private void btn_tafel_Click(object sender, EventArgs e)
        {
            int shomarcheck = 0;
            for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                if (treeView1.Nodes[0].Nodes[i].Checked)
                    shomarcheck++;
            if (shomarcheck == 1)
            {
                operation = "Tafel";// "Mathematics";
                for (int i = 0; i < chart1.Series.Count; i++)
                    if (chart1.Series[i].Enabled)
                    {
                        double[] P = new double[this.chart1.Series[i].Points.Count];
                        double[] I = new double[this.chart1.Series[i].Points.Count];
                        for (int k = 0; k < this.chart1.Series[i].Points.Count; k++)
                        {
                            P[k] = this.chart1.Series[i].Points[k].XValue;
                            I[k] = this.chart1.Series[i].Points[k].YValues[0];
                        }
                        mc.Math_Operation(P, I, 1, "Tafel", 10);
                        show_Graph(0, this, "SubChart", mc.AR, mc.BR, true, mc.AR.Length);
                        nindexchart2 = i;
                    }
                drawline1.EnableDraw = true;

                chart2.Focus();
            }
            else
                MessageBox.Show("Tafel works with one graph" + (char)13 + "Please select ONE only");

        }

        private void btn_calculate_beta_Click(object sender, EventArgs e)
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
                        lbl_detail.Text += Environment.NewLine + "T(s)= " + slop2;
                        lbl_detail.Text += Environment.NewLine + "I* T 1/2="; //+Math.Sqrt(slop2)*Math.Abs(IE/1000.0);
                    }
                    if (slop1 != 0 && slop2 != 0)
                    {
                        double xx = (drawline1.Lines_master[2].pointy - drawline1.Lines_master[0].pointy + slop1 * drawline1.Lines_master[0].pointx - slop2 * drawline1.Lines_master[2].pointx) / (slop1 - slop2);
                        chart2.Titles[0].Text = "Tafel Results :      ";
                        chart2.Titles[0].Text += "Ba=" + slp1.ToString("#0.00") + "        Bc=" + slp2.ToString("#0.00");
                    }
                }
            }

        }

        public void conv_pixel_cord(int xx, int yy)
        {
            var results = chart2.HitTest(xx, yy, false, ChartElementType.PlottingArea);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.PlottingArea)
                {
                    double xVal = result.ChartArea.AxisX.PixelPositionToValue(xx);
                    double yVal = result.ChartArea.AxisY.PixelPositionToValue(yy);
                    drawline_chart.multipoint l1;
                    l1.pointx = xVal;
                    l1.pointy = yVal;

                    drawline1.Lines_master.Add(l1);

                }
            }
        }
               
        private void btn_i_Forward_Click(object sender, EventArgs e)
        {
            drawline1.EnableDraw = false;
            drawline1.removeall();
            int shomarcheck = 0, ii = 0;
            for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                if (treeView1.Nodes[0].Nodes[i].Checked)
                {
                    shomarcheck++;
                    ii = i;
                }
            if (shomarcheck == 1)
            {
                treeView1.SelectedNode = treeView1.Nodes[0].Nodes[ii];
                if (int.Parse(dataSet11.analyselist.Rows[ii][0].ToString()) == 2 || int.Parse(dataSet11.analyselist.Rows[ii][0].ToString()) == 3)
                {
                    restore_Data_part(dataSet11.analyselist.Rows[ii][21].ToString(), 2, 1);



                    operation = "i_Forward";
                    show_Graph(0, this, "SubChart", othertech_val0, othertech_val1, true, shomar_val);
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

        private void btn_i_Reverse_Click(object sender, EventArgs e)
        {
            drawline1.EnableDraw = false;
            drawline1.removeall();
            int shomarcheck = 0, ii = 0;
            for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                if (treeView1.Nodes[0].Nodes[i].Checked)
                {
                    shomarcheck++;
                    ii = i;
                }
            if (shomarcheck == 1)
            {
                treeView1.SelectedNode = treeView1.Nodes[0].Nodes[ii];
                if (int.Parse(dataSet11.analyselist.Rows[ii][0].ToString()) == 2 || int.Parse(dataSet11.analyselist.Rows[ii][0].ToString()) == 3)
                {
                    restore_Data_part(dataSet11.analyselist.Rows[ii][21].ToString(), 3, 1);

                    operation = "i_Reverse";
                    show_Graph(0, this, "SubChart", othertech_val0, othertech_val1, true, shomar_val);
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

        private void btn_background_subtraction_Click(object sender, EventArgs e)
        {
            drawline1.EnableDraw = false;
            drawline1.removeall();
            bgsubtractionf();
        }

        public void bgsubtractionf()
        {
            operation = "Overlay";
            int shomarcheck = 0;
            for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                if (treeView1.Nodes[0].Nodes[i].Checked)
                    shomarcheck++;
            if (shomarcheck == 2)
            {
                int N1 = -1, N2 = -1;
                for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                    if (treeView1.Nodes[0].Nodes[i].Checked)
                        if (N1 == -1) N1 = i;
                        else N2 = i;
                int count1 = chart1.Series[N1].Points.Count;
                int count2 = chart1.Series[N2].Points.Count;
                int count = 0;
                if (count1 > count2) count = count2;
                else count = count1;
                double[] x = new double[count];
                double[] y = new double[count];
                for (int i = 0; i < count; i++)
                {
                    x[i] = chart1.Series[N1].Points[i].XValue;
                    y[i] = Math.Abs(chart1.Series[N2].Points[i].YValues[0] - chart1.Series[N1].Points[i].YValues[0]);
                }
                show_Graph(0, this, "SubChart", x, y, true, count);
            }
            else
                MessageBox.Show("Overlay must be checked \n Number of open graphs must be equal 2");

        }

        private void btn_peak_search_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnl_peak_search.Visible == false)
                {
                    pnl_peak_search.Visible = true;

                    if (chart2.Series.Count == 0)
                    {
                        txt_res.Text = (chart1.Series[0].Points.Count / 20).ToString();//این عدد تجربی است
                        txt_noise.Text = "0";
                    }
                }
                else
                    pnl_peak_search.Visible = false;
            }
            catch { }
        }

        private void toolStrip_top_right_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
        }

        private void toolStrip_top_right_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        private void toolStrip_top_right_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                System.Drawing.Point cur = e.Location;

                // Get the difference between the two points
                int xDiff = firstPoint.X - cur.X;
                int yDiff = firstPoint.Y - cur.Y;

                // Set the new point
                int x = this.Left - xDiff;
                int y = this.Top - yDiff;
                this.Location = new System.Drawing.Point(x, y);
            }
        }
        
        private void btn_format_top_Click(object sender, EventArgs e)
        {
            forms.frmselecttypeshow frm = new frmselecttypeshow();
            frm.StartPosition = FormStartPosition.CenterScreen;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.type_txt_y.Trim() != "")
                    chart1.ChartAreas[0].AxisY.LabelStyle.Format = frm.type_txt_y;
                if (frm.type_txt_x.Trim() != "")
                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = frm.type_txt_x;
                chart1.ChartAreas[0].RecalculateAxesScale();
            }
        }

        private void btn_format_down_Click(object sender, EventArgs e)
        {
            forms.frmselecttypeshow frm = new frmselecttypeshow();
            frm.StartPosition = FormStartPosition.CenterScreen;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.type_txt_y.Trim() != "")
                    chart2.ChartAreas[0].AxisY.LabelStyle.Format = frm.type_txt_y;
                if (frm.type_txt_x.Trim() != "")
                    chart2.ChartAreas[0].AxisX.LabelStyle.Format = frm.type_txt_x;
                chart2.ChartAreas[0].RecalculateAxesScale();
            }
        }

        private void btn_show_value_top_Click(object sender, EventArgs e)
        {
            if (statebutton == "showvalue")
                statebutton = "";
            else
                statebutton = "showvalue";
            selectpchart_func(false);
        }

        private void selectpchart_func(bool state)
        {
            chart1.ChartAreas[0].CursorX.IsUserEnabled = state;
            chart1.ChartAreas[0].CursorY.IsUserEnabled = state;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = state;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = state;
            chart1.ChartAreas[0].CursorX.Interval = 0;
            chart1.ChartAreas[0].CursorY.Interval = 0;
            if (state)
            {
                chart1.ChartAreas[0].CursorX.LineWidth = 1;
                chart1.ChartAreas[0].CursorY.LineWidth = 1;
            }
            else
            {
                chart1.ChartAreas[0].CursorX.LineWidth = 0;
                chart1.ChartAreas[0].CursorY.LineWidth = 0;
            }
        }

        private void btn_select_only_top_Click(object sender, EventArgs e)
        {
            if (statebutton == "selectonly")
            {
                statebutton = "";
                selectpchart_func(false);
//                btn_select_only_top.FlatAppearance.BorderSize = 0;
            }
            else
            {
                statebutton = "selectonly";
                selectpchart_func(true);
//                btn_select_only_top.FlatAppearance.BorderSize = 1;
//                btn_select_only_top.FlatAppearance.BorderColor = Color.Black;

            }
        }

        private void btn_zoon_in_top_Click(object sender, EventArgs e)
        {
            statebutton = "";
            selectpchart_func(false);
            this.chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            double xMin = chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            double xMax = chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
            double yMin = chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
            double yMax = chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(xMin, 0.95 * (xMax - xMin), DateTimeIntervalType.Auto, true);
            chart1.ChartAreas[0].AxisY.ScaleView.Zoom(yMin, 0.95 * (yMax - yMin), DateTimeIntervalType.Auto, true);
            difxview = chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum - chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            difyview = chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum - chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;

        }

        private void btn_zoom_out_top_Click(object sender, EventArgs e)
        {
            statebutton_1 = "";
            selectpchart_func(false);
            this.chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset(1);
            chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset(1);

        }

        private void btn_show_grid_top_Click(object sender, EventArgs e)
        {
            statebutton = "";
            selectpchart_func(false);
            this.chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = !this.chart1.ChartAreas[0].AxisX.MajorGrid.Enabled;
            this.chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = !this.chart1.ChartAreas[0].AxisY.MajorGrid.Enabled;

        }

        private void btn_show_axis_top_Click(object sender, EventArgs e)
        {
            statebutton = "";
            selectpchart_func(false);
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = !chart1.ChartAreas[0].AxisX.LabelStyle.Enabled;
            chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = !chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled;
            chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = !chart1.ChartAreas[0].AxisY.LabelStyle.Enabled;
            chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = !chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled;

        }

        private void btn_export_top_Click(object sender, EventArgs e)
        {
            forms.frmselectfiletype frm = new frmselectfiletype();
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

                    double xminb = 0, xmaxb = 0, yminb = 0, ymaxb = 0, shomartypevi = 0, shomartypevt = 0, shomartypeit = 0, shomartypeqt = 0;
                    for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                        if (treeView1.Nodes[0].Nodes[i].Checked)
                        {
                            if (classglobal.HAxisTitle[int.Parse(dataSet11.analyselist.Rows[i][0].ToString())].Substring(0, 1) == "P" && classglobal.VAxisTitle[int.Parse(dataSet11.analyselist.Rows[i][0].ToString())].Substring(0, 1) == "C")
                                shomartypevi++;
                            else if (classglobal.HAxisTitle[int.Parse(dataSet11.analyselist.Rows[i][0].ToString())].Substring(0, 1) == "T" && classglobal.VAxisTitle[int.Parse(dataSet11.analyselist.Rows[i][0].ToString())].Substring(0, 1) == "C")
                                shomartypeit++;
                            else if (classglobal.HAxisTitle[int.Parse(dataSet11.analyselist.Rows[i][0].ToString())].Substring(0, 1) == "T" && classglobal.VAxisTitle[int.Parse(dataSet11.analyselist.Rows[i][0].ToString())].Substring(0, 1) == "P")
                                shomartypevt++;
                            else if (classglobal.HAxisTitle[int.Parse(dataSet11.analyselist.Rows[i][0].ToString())].Substring(0, 1) == "T" && classglobal.VAxisTitle[int.Parse(dataSet11.analyselist.Rows[i][0].ToString())].Substring(0, 1) == "Q")
                                shomartypeqt++;

                            shomarcheck++;
                            i1 = i;
                            if (shomarcheck == 0)
                            {
                                MessageBox.Show("EXPORTING works with one graph" + (char)13 + "Please select ONE !!!");

                            }
                            else if (shomarcheck == 1)
                            {
                                xminb = chart1.Series[i].Points.FindMinByValue("X").XValue;
                                xmaxb = chart1.Series[i].Points.FindMaxByValue("X").XValue;
                                yminb = chart1.Series[i].Points.FindMinByValue("Y").YValues[0];
                                ymaxb = chart1.Series[i].Points.FindMaxByValue("Y").YValues[0];

                            }
                            else
                            {
                                if (xminb > chart1.Series[i].Points.FindMinByValue("X").XValue)
                                    xminb = chart1.Series[i].Points.FindMinByValue("X").XValue;
                                if (xmaxb < chart1.Series[i].Points.FindMaxByValue("X").XValue)
                                    xmaxb = chart1.Series[i].Points.FindMaxByValue("X").XValue;
                                if (yminb > chart1.Series[i].Points.FindMinByValue("Y").YValues[0])
                                    yminb = chart1.Series[i].Points.FindMinByValue("Y").YValues[0];
                                if (ymaxb < chart1.Series[i].Points.FindMaxByValue("Y").YValues[0])
                                    ymaxb = chart1.Series[i].Points.FindMaxByValue("Y").YValues[0];
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
                        forms.frmshowgraph frm1 = new frmshowgraph(strlegend, xminb, xmaxb, yminb, ymaxb, titlegraph, "");
                        if (frm1.Chart1.Series.Count > 0)
                            frm1.Chart1.Series[0].Points.Clear();
                        for (int i = 0; i < chart1.Series[i1].Points.Count; i++)
                        {
                            frm1.Chart1.Series[0].Points.AddXY(chart1.Series[i1].Points[i].XValue, chart1.Series[i1].Points[i].YValues[0]);
                        }
                        frm1.StartPosition = FormStartPosition.CenterScreen;
                        frm1.ShowDialog();
                    }
                    else
                    {
                        // MessageBox.Show("EXPORTING works with one graph" + (char)13 + "Please select ONE only!!!");
                        strlegend = "";

                        forms.frmshowgraph frm2 = new frmshowgraph(strlegend, xminb, xmaxb, yminb, ymaxb, titlegraph, "");
                        if (frm2.Chart1.Series.Count > 0)
                        {
                            frm2.Chart1.Series[0].Points.Clear();
                            if (dataSet11.analyselist.Rows[0]["comment"].ToString().Trim() != "")
                                frm2.Chart1.Series[0].LegendText = classglobal.TechName[int.Parse(dataSet11.analyselist.Rows[0][0].ToString())] + "(" + dataSet11.analyselist.Rows[0]["comment"].ToString().Trim() + ")";
                            else
                                frm2.Chart1.Series[0].LegendText = classglobal.TechName[int.Parse(dataSet11.analyselist.Rows[0][0].ToString())];

                        }
                        int b = 0;
                        for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                            if (treeView1.Nodes[0].Nodes[i].Checked)
                            {
                                if (frm2.Chart1.Series.Count <= b)
                                {
                                    frm2.Chart1.Series.Add(treeView1.Nodes[0].Nodes[i].Name);
                                    frm2.Chart1.Series[b].ChartType = SeriesChartType.FastLine;
                                    if (dataSet11.analyselist.Rows[i]["comment"].ToString().Trim() != "")
                                        frm2.Chart1.Series[b].LegendText = classglobal.TechName[int.Parse(dataSet11.analyselist.Rows[i][0].ToString())] + "(" + dataSet11.analyselist.Rows[i]["comment"].ToString().Trim() + ")";
                                    else
                                        frm2.Chart1.Series[b].LegendText = classglobal.TechName[int.Parse(dataSet11.analyselist.Rows[i][0].ToString())];

                                }
                                for (int i11 = 0; i11 < chart1.Series[i].Points.Count; i11++)
                                {
                                    frm2.Chart1.Series[b].Points.AddXY(chart1.Series[i].Points[i11].XValue, chart1.Series[i].Points[i11].YValues[0]);
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
        private void toolStripButton9_Click_1(object sender, EventArgs e)
        {
            int shomarcheck = 0, i1 = 1, i2 = 1;
            for (int i = 0; i < this.treeView1.Nodes[0].Nodes.Count; i++)
                if (this.treeView1.Nodes[0].Nodes[i].Checked)
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
                        for (int i = 0; i < this.treeView1.Nodes[0].Nodes.Count; i++)
                        {
                            if (this.treeView1.Nodes[0].Nodes[i].Checked)
                            {
                                Fl.WriteLine(i1.ToString().Trim() + "-" + classglobal.TechName[int.Parse(dataSet11.analyselist.Rows[i][0].ToString())]);
                                Fl.WriteLine("---------------------------------------------------");
                                Fl.WriteLine("row number      X value         Y value");
                                Fl.WriteLine("---------------------------------------------------");

                                for (int cc1 = 0; cc1 < chart1.Series[i].Points.Count; cc1++)
                                {
                                    Fl.Write((cc1 + 1).ToString().Trim() + "\t\t");
                                    Fl.Write(chart1.Series[i].Points[cc1].XValue.ToString() + "\t\t");
                                    Fl.WriteLine(chart1.Series[i].Points[cc1].YValues[0]);
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

        private void toolStripButton10_Click_1(object sender, EventArgs e)
        {

            int shomarcheck = 0;
            for (int i = 0; i < this.treeView1.Nodes[0].Nodes.Count; i++)
                if (this.treeView1.Nodes[0].Nodes[i].Checked)
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
                    MessageBox.Show(ExportToExcel(5, 1, sfd.FileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select one technique!!!");
            }
        }
        
        private void btn_backcolor_top_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog cl = new ColorDialog();
                cl.Color = this.chart1.ChartAreas[0].BackColor;
                cl.CustomColors = new int[] { this.chart1.ChartAreas[0].BackColor.R, this.chart1.ChartAreas[0].BackColor.G, this.chart1.ChartAreas[0].BackColor.B };
                cl.FullOpen = true;

                if (cl.ShowDialog() == DialogResult.OK)
                {
                    this.chart1.BackColor = cl.Color;
                    this.chart1.BorderlineColor = cl.Color;
                    this.chart1.ChartAreas[0].BackColor = cl.Color;
                    this.chart2.BackColor = cl.Color;
                    this.chart2.BorderlineColor = cl.Color;
                    this.chart2.ChartAreas[0].BackColor = cl.Color;
                    this.panel1.BackColor = cl.Color;
                    this.panel2.BackColor = cl.Color;
             //       panel8.BackColor = cl.Color;

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

        private void btn_graph_color_top_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog cl = new ColorDialog();
                cl.FullOpen = true;

                if (cl.ShowDialog() == DialogResult.OK)
                {
                    string[] color = File.ReadAllLines(System.Windows.Forms.Application.StartupPath + "\\Color.dat");
                    color[7] = "2190 graph analysis:" + cl.Color.ToArgb().ToString();
                    File.WriteAllLines(System.Windows.Forms.Application.StartupPath + "\\Color.dat", color);

                    //for (int i = 0; i < tChart1.Series.Count; i++)
                    int i = treeView1.SelectedNode.Index;
                    this.chart1.Series[i].Color = cl.Color;

                    try { this.chart2.Series[0].Color = cl.Color; } catch { }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
         
        private void btn_new_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                clearform();
            }
        }

        public void clearform()
        {
            try
            {
                for (int i = this.treeView1.Nodes[0].Nodes.Count - 1; i >= 0; i--)
                    this.treeView1.Nodes[0].Nodes[i].Remove();

                lbl_detail.Text = "";
                for (int i = chart1.Series.Count - 1; i >= 0; i--)
                    chart1.Series.RemoveAt(i);
                this.chart1.Refresh();
                dataSet11.analyselist.Clear();
                if (this.chart2.Series.Count > 0)
                {
                    this.chart2.Titles[0].Text = "";
                    this.chart2.Series.Clear();
                }
                drawline1.removeall();
                drawline1.removeall_master();
            }
            catch { }
        }
        
        private void btn_open_Click(object sender, EventArgs e)
        {
            try
            {
                //btnzoomout_Click(sender, e);//هنگام باز کردن دوباره کامل نمایش یابد
                open_file();

            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void open_file()
        {
            bool overlaymain = true;
            try
            {
                chart2.Series.Clear();
                chart2.Refresh();

                if (this.treeView1.Nodes[0].Nodes.Count > 0)
                {
                    switch (MessageBox.Show("Other graphs exist in this chart. Do you want to show new graph there?\n----------------------------------\nyes : show all graphs in this chart\nno : remove other graphs in this chart and show this graph only\ncancle : dont show this graph\n--------------------------------", "question", MessageBoxButtons.YesNoCancel))
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

                Int16 cycle = 1;
                Int16 line_file;
                //string tech = "";

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.DereferenceLinks = false;
                ofd.AutoUpgradeEnabled = false;
                ofd.Multiselect = true;
                ofd.DefaultExt = "bhpd";

                ofd.Filter = "Bhpd Files(*.Bhpd);All Files(*.*)|*.Bhpd;*.*";
                ofd.ShowDialog();
                
                bool beOverlay = overlaymain;

                for (int i = 0; i <= ofd.FileNames.GetUpperBound(0); i++)
                    if (ofd.FileNames.GetValue(i).ToString() != "")
                    {
                        overlaymain = true;
                        int n = dataSet11.analyselist.Rows.Count;

                        cycle = 1;
                        line_file = -1;//if cycle>1 get line number for continue read file

                        string fName = ofd.FileNames.GetValue(0).ToString();//address and filename
                        int k = fName.Length - 1;

                        // distinct file names from file path
                        while (k > 0)
                            if (fName[k] == '\\')
                            {
                                fName = fName.Substring(k + 1);  
                                break;
                            }
                            else
                                k--;

                        string fName_2 = fName;
                        fName = "";
                        /*
                                                int ok = 0;
                                                for (int g = 0; g < fName_2.Length; g++)
                                                {
                                                    if (ok == 1)
                                                        if (fName_2[g].ToString() == "_" || fName_2[g].ToString() == "-" || fName_2[g].ToString() == ".")
                                                            break;
                                                        else
                                                            fName = fName + fName_2[g].ToString();  


                                                    if (fName_2[g].ToString() == "_" ) ok = 1;
                                                }


                                                if (ok == 0) fName = fName_2.Substring(0, fName_2.Length - 5);    // اگر فرمت نام فایل غیر از تعریف نرم افزار باشد
                        */

                        fName = fName_2.Substring(0, fName_2.Length - 5);
                        var Fl = new StreamReader(ofd.FileNames.GetValue(0).ToString());

                        while (!Fl.EndOfStream)
                        {
                            string s = Fl.ReadLine();
                            if (s.IndexOf("Cycles") >= 0)
                            {
                                if (s.IndexOf(":") >= 0)
                                {
                                    string c = s.Substring(s.IndexOf(":") + 1);
                                    cycle = Int16.Parse(c);
                                    break;
                                }
                            }
                        }

                        for (int ii = 0; ii < cycle; ii++)
                        {
                            restore_Data(ofd.FileNames.GetValue(i).ToString(), overlaymain);
                            if (ii == 0)
                                fillparamdata(n);//اگر چند گراف را انتخاب کرده بود و یا سیکل داشت فقط گراف اول را ترسیم کند
                            if (cycle > 1)
                                this.treeView1.Nodes[i].Nodes.Add((this.treeView1.Nodes[0].Nodes.Count + 1).ToString() + "-" + techName + "-" + (ii + 1));  // CV-1.dat
                            else
                                this.treeView1.Nodes[i].Nodes.Add((this.treeView1.Nodes[0].Nodes.Count+1).ToString() +"-" + techName);  // CV.dat
                        }
                        n--;

                        if (dataSet11.analyselist.Rows.Count == n)
                        {
                            MessageBox.Show("FILE FORMAT IS NOT CORRECT" + Environment.NewLine + "Please try again");
                            return;
                        }

                        cycle = 1;

                        Text = ofd.FileNames.GetValue(0).ToString();
                    }


                overlaymain = beOverlay;

                if (treeView1.Nodes.Count > 0)
                {
                    treeView1.SelectedNode = treeView1.Nodes[0].Nodes[treeView1.Nodes[0].Nodes.Count - 1];
                    treeView1.Nodes[0].Nodes[treeView1.Nodes[0].Nodes.Count - 1].Checked = true;
                    treeView1.Nodes[0].LastNode.ForeColor = chart1.Series[chart1.Series.Count - 1].Color;
                }

                overlaymain = beOverlay;
             //   Chart2 = chart1;
                treeView1.ExpandAll();
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void fillparamdata(int row)
        {
            chart1.Series[row].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            smootht4(row, chart1.Series[row].Color, 1, 2);
            chart1.Refresh();
        }

        private void restore_Data(string fileName, bool overlaymain)
        {
            Int16 cycle = 1, line_file = -1;

            double[] xTmp = { 0 };
            double[] yTmp = { 0 };
            string s = "", fs = "", comment100 = "";
            bool isData = false;
            int num_ser = 0, num_data = 0;
            int row = 0;
            if (fileName != "")
                fs = fileName;
            StreamReader Fl;
            int tch = 0; double CR = 9999, E1 = 9999, E2 = 9999, E3 = 9999, HS = 9999, QT = 9999, SRF = 9999, SRR = 9999;
            double TS = 9999, PW = 9999, PH = 9999, FR = 9999, CY = 1 /*CY = 9999*/, HT = 9999, T1 = 9999, T2 = 9999, I1 = 9999, I2 = 9999;
            double AA = 9999, Q1 = 9999;
            EventArgs e = new EventArgs();
            if (fs != "")
            {
                Fl = new StreamReader(fs);
                if (overlaymain)
                    num_data = chart1.Series.Count;
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
                            show_Graph(num_data + num_ser, "MainChart", xTmp, yTmp, overlaymain, xTmp.Length);
                            chart1.Series[num_data + num_ser].Points.Clear();
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

                            if (s.IndexOf("Technic") >= 0)
                            {
                                if (s.IndexOf(':') >= 0)
                                {

                                    for (int i = 0; i < 13; i++)
                                        if (classglobal.TechName[i] == s.Substring(s.IndexOf(':') + 1).Trim())
                                        {
                                            techName = s.Substring(s.IndexOf(':') + 1).Trim();
                                            tch = i;
                                            if (classglobal.TechName[i].ToLower() == "cha" || classglobal.TechName[i].ToLower() == "chp" || classglobal.TechName[i].ToLower() == "chc")
                                                smooth_state = false;
                                            else
                                                smooth_state = true;
                                            break;
                                        }
                                }
                                tech = (byte)restore_One_Data(s, "Technic");

                                show_Graph(num_data+num_ser, "MainChart", xTmp, yTmp, overlaymain, xTmp.Length);
                                chart1.Series[num_data+num_ser].Points.Clear();

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
//                                MessageBox.Show("line_file=" + line_file);
//                                MessageBox.Show("line=" + line);
                                if (line_file > line)
                                    line_file -= 16;//this is not cycle 1 then less lines header from all lines
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
                            row++;

                            if (row >= 10000) row = 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + "selected File is not in valid Format...", "Warning");
                }

                Fl.Close();
/*
                int k = fileName.Length - 1;
                while (k > 0)
                    if (fileName[k] == '\\')
                    {
                        fileName = fileName.Substring(k + 1);
                        break;
                    }
                    else
                        k--;
                //this.chart1.Series[num_data + num_ser].Titles[0]= fileName;
*/
            }
        }
        
        public void smootht4(int row1, Color color1, int typechart, int colnum)
        {
            double I0, I1 = 0;
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
                this.chart1.Series[row1].Color = color1;
                this.chart1.Series[row1].Points.Clear();

                if (dataSet11.analyselist.Rows[row1][0].ToString() == "10" || dataSet11.analyselist.Rows[row1][0].ToString() == "11" || dataSet11.analyselist.Rows[row1][0].ToString() == "12")
                    this.chart1.Series[row1].Points.AddXY(0, othertech_val1[0]);//رسم در نقطه صفر زمان // technics chp cha chc

                for (int i = 0; i < vn; i++)
                {
                    this.chart1.Series[row1].Points.AddXY(othertech_val0[i], othertech_val1[i]);// رسم همه گرافها هنگام باز شدن
                }
            }
            else
            {
                this.chart2.Series[row1].Points.Clear();
                for (int i = 0; i < vn; i++)
                {
                    this.chart2.Series[row1].Points.AddXY(othertech_val0[i], othertech_val1[i]);
                }
            }
        }
                          
        private void fillgraphlist(int tch, double CR, double E1, double E2, double E3, double HS, double QT, double SRF, double SRR,
          double TS, double PW, double PH, double FR, double CY, double HT, double T1, double T2, double I1, double I2, double AA, double Q1, string strpath, string comment100)
        {

            DataRow dr = dataSet11.analyselist.NewRow();
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
            dataSet11.analyselist.Rows.Add(dr);

        }

        public void show_Graph1(int num_ser, frm_analyse grfn, string main_subchart, double[] xx, double[] yy, bool overlaymain, int shomaresh)
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

                chart1.Series[num_Series].Points.DataBindXY(xx, yy);
            }
            if (main_subchart == "SubChart")
            {

                if (operation == "i_Reverse" || operation == "i_Forward" || operation == "Integral" || operation == "Derivation" || operation == "Mathematics" || operation == "Overlay" || operation == "Tafel" || operation == "show_without_smoothing")
                {
                    chart2.Series.Clear();
                    chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());
                    chart2.Series[0].ChartType = SeriesChartType.FastLine;
                    chart2.Titles[0].Text = operation;
                    this.chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = this.chart1.ChartAreas[0].AxisX.MajorGrid.Enabled;
                    this.chart2.ChartAreas[0].AxisX.MinorGrid.Enabled = this.chart1.ChartAreas[0].AxisX.MinorGrid.Enabled;
                    this.chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = this.chart1.ChartAreas[0].AxisY.MajorGrid.Enabled;
                    this.chart2.ChartAreas[0].AxisY.MinorGrid.Enabled = this.chart1.ChartAreas[0].AxisY.MinorGrid.Enabled;


                    chart2.ChartAreas[0].AxisX.LabelStyle.Format = "#.#e-0";
                    chart2.ChartAreas[0].AxisY.LabelStyle.Format = "#.#e-0";
                    if (operation == "i_Forward")
                        smootht4(0, chart2.Series[0].Color, 2, 3);
                    else if (operation == "i_Reverse")
                        smootht4(0, chart2.Series[0].Color, 2, 4);
                    else
                        chart2.Series[0].Points.DataBindXY(xx, yy);
                    chart2.Refresh();

                }
            }
        }
        
        public void show_Graph(int num_ser, string main_subchart, double[] xx, double[] yy, bool overlaymain, int shomaresh)
        {
            int L = 0;
            if (xx.Length != yy.Length) // Convert bigest Array to smallest if length of x and y is not equal
            {
                L = xx.Length;
                if (yy.Length < L) L = yy.Length;
            }

            if (L == 0)
            {
                Array.Resize(ref xx, shomaresh);
                Array.Resize(ref yy, shomaresh);
            }

            if (main_subchart == "MainChart")
            {
               Set_Graph(0, true, overlaymain);
               chart1.Series[num_Series].Points.DataBindXY(xx, yy);
            }
            if (main_subchart == "SubChart")
            {

                if (operation == "i_Reverse" || operation == "i_Forward" || operation == "Integral" || operation == "Derivation" || operation == "Mathematics" || operation == "Overlay" || operation == "Tafel" || operation == "show_without_smoothing")
                {
                    this.chart2.Series.Clear();
                    this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());
                    this.chart2.Series[0].ChartType = SeriesChartType.FastLine;
                    this.chart2.Titles[0].Text = operation;
                    this.chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = this.chart1.ChartAreas[0].AxisX.MajorGrid.Enabled;
                    this.chart2.ChartAreas[0].AxisX.MinorGrid.Enabled = this.chart1.ChartAreas[0].AxisX.MinorGrid.Enabled;
                    this.chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = this.chart1.ChartAreas[0].AxisY.MajorGrid.Enabled;
                    this.chart2.ChartAreas[0].AxisY.MinorGrid.Enabled = this.chart1.ChartAreas[0].AxisY.MinorGrid.Enabled;

                    if (operation == "i_Forward")
                        smootht4(0, chart2.Series[0].Color, 2, 3);
                    else if (operation == "i_Reverse")
                        smootht4(0, chart2.Series[0].Color, 2, 4);
                    else
                        this.chart2.Series[0].Points.DataBindXY(xx, yy);
                    this.chart2.ChartAreas[0].RecalculateAxesScale();
                    this.chart2.Refresh();

                }
            }
            difxview2 = this.chart2.ChartAreas[0].AxisX.ScaleView.ViewMaximum - this.chart2.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            difyview2 = this.chart2.ChartAreas[0].AxisY.ScaleView.ViewMaximum - this.chart2.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
        }

        private void btn_show_data_Click(object sender, EventArgs e)
        {
            int index = 0;
            bool is_selected = false;

            for (int i = 0; i < this.treeView1.Nodes[0].Nodes.Count; i++)
                if (this.treeView1.Nodes[0].Nodes[i].Checked == true)
                {
                    index = i;
                    is_selected = true;
                    break;
                }

            if (is_selected == true)
            {
                forms.frm_show_data frm = new forms.frm_show_data();
                frm.listView1.Items.Clear();

                for (int i = 0; i < chart1.Series[index].Points.Count; i++)
                {
                    frm.listView1.Items.Add(chart1.Series[index].Points[i].XValue + "               " + chart1.Series[index].Points[i].YValues[0]);
                }
                frm.ShowDialog();
            }
        }
        
        private double restore_One_Data(string str, string prmName)
        {
            if (prmName == "Technic")
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
                    case "DCS": Tch = classglobal.DCS; break;
                    case "DPS": Tch = classglobal.DPS; break;
                    case "OCP": Tch = classglobal.OCP; break;
                    case "CPC": Tch = classglobal.CPC; break;
                    case "CHP": Tch = classglobal.CHP; break;
                    case "CHA": Tch = classglobal.CHA; break;
                    case "CHC": Tch = classglobal.CHC; break;
                    //case "SC": Tch = classglobal.SC; break;
              //      case "CCC": Tch = classglobal.CCC; break;
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
            ////if (prmName.ToLower() == "scan rate")
              ////  ScanRate = val;
            return val;

           // return 1;
        }
               
        public void Set_Graph(int num, bool isFromOpen, bool overlaymain)
        {
            Random Random1 = new Random();
            Random rndm2 = new Random();
            Random rndm3 = new Random();
            //chart1.Titles[0].Text = "  ";
            int Random_Color_Index = 0;

            Random_Color_Index = (int)(Random1.NextDouble() * 90);

            if (!overlaymain)
                this.chart1.Series.Clear();
            System.Windows.Forms.DataVisualization.Charting.Series ser1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            ser1.Color = classglobal.color_Graph[Random_Color_Index];
            ser1.LabelFormat = "#,##0.###########";
///
               if (opForm.rbfLine.Checked)
               {
                   ser1.ChartType = SeriesChartType.FastLine;

               }
               if (opForm.rbLine.Checked)
               {
                   ser1.ChartType = SeriesChartType.Line;
               }
               if (opForm.rbPoint.Checked)
               {
                   ser1.ChartType = SeriesChartType.Point;
               }
///
            this.chart1.Series.Add(ser1);
            num_Series = chart1.Series.Count - 1 ;////////////////////////////
            this.chart1.Series[num_Series].Color = classglobal.color_Graph[Random_Color_Index];

            if (!overlaymain)
            {
                this.chart1.Series[num_Series].Points.Clear();

                this.chart1.Series.Remove(chart1.Series[0]);
            }


            this.chart1.ChartAreas[0].Area3DStyle.Enable3D = false;
            this.chart1.BackColor = Color.White;

            this.chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
            this.chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
            this.chart1.ChartAreas[0].AxisY.Title = classglobal.VAxisTitle[tech];
            this.chart1.ChartAreas[0].AxisX.Title = classglobal.HAxisTitle[tech];
            if (num_Series < 1)
                Overlay_tech[0] = tech;
            else
                Overlay_tech[num_Series] = tech;
            this.chart1.ChartAreas[0].AxisY.LabelStyle.Format = "#.#e-0";
            this.chart1.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";
            this.chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            this.chart2.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            this.chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            this.chart2.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
            int f = 0;
            //MessageBox.Show("Count="+this.treeView1.Nodes[0].Nodes.Count.ToString());
            for (int i = 0; i < this.treeView1.Nodes[0].Nodes.Count; i++)
                if (this.treeView1.Nodes[0].Nodes[i].Checked == true)
                {
                    f = 1;
                    break;
                }

            if (f == 1)
                save_Data();
            else
                MessageBox.Show("There is not any case select for saving !!!");

        }

        
        private void save_Data()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = "bhpd";
            string dir_sfd = "";
            sfd.Filter = "BHP Files(*.bhpd)|*.bhpd|Signal File(*.sig)|*.sig";
            int nn = this.treeView1.Nodes[0].Nodes.Count;

            int filename_index = 1, index_main_dataset = 0;
            string filename = "0";

            if (dir_sfd != "") sfd.InitialDirectory = dir_sfd;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileInfo f_info = new FileInfo(sfd.FileName);
                string ext = f_info.Extension;

                if (ext == ".bhpd")
                {
                    string add = sfd.FileName.Substring(0, sfd.FileName.IndexOf('.'));

                    for (int j = 0; j < nn; j++)
                    {
                         //       MessageBox.Show(dataSet11.analyselist.Rows[j][11].ToString());
                        if (Convert.ToInt16(dataSet11.analyselist.Rows[j][11].ToString()) > 1) //cycle > 1
                        {
                            for (int f = j; f < (j + Convert.ToInt16(dataSet11.analyselist.Rows[j][11].ToString())); f++)
                                if (this.treeView1.Nodes[0].Nodes[f].Checked)
                                {
                                 //   filename = add + "_" + treeView1.Nodes[0].Nodes[f].Text + "_" + (filename_index).ToString() + ".bhpd";
                                    filename = sfd.FileName;

                                    File.Copy(dataSet11.analyselist.Rows[j][21].ToString(), filename, true);
                                    filename_index++;
                                    MessageBox.Show("The file was saved.");
                                    break;
                                }

                            j += Convert.ToInt16(dataSet11.chartlist.Rows[j][11].ToString()) - 1; // becuse cycle this here is a graf but in database not rows
                        }
                        else
                        {
                            if (this.treeView1.Nodes[0].Nodes[j].Checked)
                            {
                             //   filename = add + "_" + treeView1.Nodes[0].Nodes[j].Text + "_" + (filename_index).ToString() + ".bhpd";
                                filename = sfd.FileName;
                                File.Copy(dataSet11.analyselist.Rows[j][21].ToString(), filename, true);
                                filename_index++;
                                MessageBox.Show("The file was saved.");
                            }
                        }

                        index_main_dataset++;
                    }
                }
                else if (ext == ".sig")
                {
                    string name = sfd.FileName.Substring(0, sfd.FileName.Length - 4);

                    for (int j = 0; j < nn; j++)
                    {
                        string file_name = name + "_" + (j + 1).ToString() + "." + ext;

                        if (this.treeView1.Nodes[0].Nodes[j].Checked)
                        {
                            Array.Clear(volt, 0, Counter_num);
                            Array.Clear(current, 0, Counter_num);

                            Counter_num = chart1.Series[j].Points.Count;

                            Array.Resize(ref volt, Counter_num);
                            Array.Resize(ref current, Counter_num);

                            for (int i = 0; i < Counter_num; i++)
                            {
                                double d = chart1.Series[j].Points[i].XValue;
                                string msg = string.Format("{0:N4}", d);
                                msg = msg + "1";//نرم افزاری که فایل سیگ را باز میکند اگر مقادیر اعشار نداشته باشد گراف ترسیم نمیکند

                                volt[i] = Double.Parse(msg);
                                current[i] = chart1.Series[j].Points[i].YValues[0];
                            }

                            File.WriteAllBytes(file_name, fill_matris());
                        }

                        row_sig++;
                    }
                }
            }
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
            switch (classglobal.TechName[int.Parse(dataSet11.analyselist.Rows[row_sig][0].ToString())])
            {
                case "NPV":
                    Teqnic = int.Parse(dataSet11.analyselist.Rows[row_sig][0].ToString());
                    Startpotintial2 = double.Parse(dataSet11.analyselist.Rows[row_sig][1].ToString());
                    firstvertexpotential = double.Parse(dataSet11.analyselist.Rows[row_sig][2].ToString());
                    voltagstep = double.Parse(dataSet11.analyselist.Rows[row_sig][4].ToString());
                    VoltageStepTime = double.Parse(dataSet11.analyselist.Rows[row_sig][7].ToString());
                    BasePotential = double.Parse(dataSet11.analyselist.Rows[row_sig][1].ToString());
                    PulseTime = double.Parse(dataSet11.analyselist.Rows[row_sig][8].ToString());

                    break;
                case "DPV":
                    Teqnic = int.Parse(dataSet11.analyselist.Rows[row_sig][0].ToString());
                    Startpotintial2 = double.Parse(dataSet11.analyselist.Rows[row_sig][1].ToString());
                    firstvertexpotential = double.Parse(dataSet11.analyselist.Rows[row_sig][2].ToString());
                    voltagstep = double.Parse(dataSet11.analyselist.Rows[row_sig][4].ToString());
                    Sweeprate = double.Parse(dataSet11.analyselist.Rows[row_sig][8].ToString());
                    PulseAmplitude = double.Parse(dataSet11.analyselist.Rows[row_sig][9].ToString());
                    PulseTime = double.Parse(dataSet11.analyselist.Rows[row_sig][8].ToString());
                    VoltageStepTime = double.Parse(dataSet11.analyselist.Rows[row_sig][7].ToString());

                    break;
                case "SWV":
                    Teqnic = int.Parse(dataSet11.analyselist.Rows[row_sig][0].ToString());
                    Startpotintial2 = double.Parse(dataSet11.analyselist.Rows[row_sig][1].ToString());
                    firstvertexpotential = double.Parse(dataSet11.analyselist.Rows[row_sig][2].ToString());
                    voltagstep = double.Parse(dataSet11.analyselist.Rows[row_sig][4].ToString());
                    Amplitude = double.Parse(dataSet11.analyselist.Rows[row_sig][9].ToString());
                    Frequency = double.Parse(dataSet11.analyselist.Rows[row_sig][10].ToString());

                    break;
                case "CV":
                    Teqnic = int.Parse(dataSet11.analyselist.Rows[row_sig][0].ToString());
                    Startpotintial2 = double.Parse(dataSet11.analyselist.Rows[row_sig][1].ToString());// MessageBox.Show(dschart2.analyselist.Rows[row_sig][1].ToString());
                    firstvertexpotential = double.Parse(dataSet11.analyselist.Rows[row_sig][2].ToString());
                    secondvertexpotential = double.Parse(dataSet11.analyselist.Rows[row_sig][3].ToString());
                    voltagstep = double.Parse(dataSet11.analyselist.Rows[row_sig][4].ToString());
                    Sweeprate = double.Parse(dataSet11.analyselist.Rows[row_sig][8].ToString());
                    numsweep = int.Parse(dataSet11.analyselist.Rows[row_sig][11].ToString());
                    numofsave = 1;

                    break;
                case "LSV":
                    Teqnic = int.Parse(dataSet11.analyselist.Rows[row_sig][0].ToString());
                    Startpotintial2 = double.Parse(dataSet11.analyselist.Rows[row_sig][1].ToString());
                    firstvertexpotential = double.Parse(dataSet11.analyselist.Rows[row_sig][2].ToString());
                    voltagstep = double.Parse(dataSet11.analyselist.Rows[row_sig][4].ToString());
                    VoltageStepTime = double.Parse(dataSet11.analyselist.Rows[row_sig][7].ToString());

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


            // Counter = 0x15;
            Temp = HexstringToByteArray(inttoHex(Teqnic));
            for (int i = 0; i < Temp.Length; i++)
            {
                b[Counter] = (byte)Temp[(Temp.Length - 1) - i];
                Counter++;
            }

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
        
        private void Btn_show_data_Click(object sender, EventArgs e)
        {
            int index = 0;
            bool is_selected = false;

            for (int i = 0; i < this.treeView1.Nodes[0].Nodes.Count; i++)
                if (this.treeView1.Nodes[0].Nodes[i].Checked == true)
                {
                    index = i;
                    is_selected = true;
                    break;
                }

            if (is_selected == true)
            {
                forms.frm_show_data frm = new forms.frm_show_data();
                frm.listView1.Items.Clear();

                for (int i = 0; i < this.chart1.Series[index].Points.Count; i++)
                {
                    frm.listView1.Items.Add(this.chart1.Series[index].Points[i].XValue + "               " + chart1.Series[index].Points[i].YValues[0]);
                }
                frm.ShowDialog();
            }

        }
         
        private void restore_Data(string fileName)
        {
            string s = "", fs = "";
            bool isData = false;
            if (fileName != "")
                fs = fileName;
            StreamReader Fl;

            EventArgs e = new EventArgs();
            if (fs != "")
            {
                Fl = new StreamReader(fs, Encoding.ASCII);

                try
                {
                    int cycle = 0;

                    while (!Fl.EndOfStream)
                    {
                        s = Fl.ReadLine();
                        chart1.Series[0].Points.Clear();

                        if (s.Trim().Length == 0)//خط خالی
                            s = Fl.ReadLine();

                        if (s.LastIndexOf("=====") > 0)
                        {
                            cycle++;

                            if (cycle > 1)//اگر دارای سیکل بود فقط یک سیکل را نمایش دهد
                                break;
                        }

                        if (!isData)
                        {
                            if (s.IndexOf("Technic") >= 0)
                            {
                                if (s.IndexOf(':') >= 0)
                                {
                                    for (int i = 0; i < classglobal.TechName.Length; i++)
                                    {

                                        if (classglobal.TechName[i] == s.Substring(s.IndexOf(':') + 1).Trim())
                                        {
                                            chart1.ChartAreas[0].AxisY.Title = classglobal.VAxisTitle[i];
                                            chart1.ChartAreas[0].AxisX.Title = classglobal.HAxisTitle[i];
                                            label41.Text = "Technique name :" + classglobal.TechName[i];
                                            lbl_detail.Text = "";
                                            if (classglobal.TechName[i].ToLower() == "cha" || classglobal.TechName[i].ToLower() == "chp" || classglobal.TechName[i].ToLower() == "chc")
                                                smooth_state = false;
                                            else
                                                smooth_state = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (s.IndexOf("Current Range") >= 0 || s.IndexOf("E1") >= 0 || s.IndexOf("E2") >= 0 || s.IndexOf("E3") >= 0 || s.IndexOf("HStep") >= 0 || s.IndexOf("Equilibrium Time") >= 0 || s.IndexOf("Hold Time") >= 0 || s.IndexOf("Scan Rate") >= 0 || s.IndexOf("ScanRate_R") >= 0
                                || s.IndexOf("TStep") >= 0 || s.IndexOf("Pulse Width") >= 0 || s.IndexOf("pulse ") >= 0 || s.IndexOf("Frequency") >= 0 || s.IndexOf("Cycle") >= 0 || s.IndexOf("T1") >= 0 || s.IndexOf("T2") >= 0 || s.IndexOf("I1") >= 0 || s.IndexOf("I2") >= 0 || s.IndexOf("OCP Measurment") >= 0 || s.IndexOf("vs_OCP") >= 0)
                                lbl_detail.Text = lbl_detail.Text.Trim() + Environment.NewLine + s.Trim();

                            if (s.LastIndexOf("Potential") >= 0 || s.LastIndexOf("========") >= 0)
                            {
                                isData = true;
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

                                    try { if (num == 0) x = Convert.ToDouble(sss); } catch { x = 0; }//this.grf.dataE[num_data + num_ser, row] //890220
                                    try { if (num == 1) y = Convert.ToDouble(sss); } catch { y = 0; } //this.grf.dataI1[num_data + num_ser, row] = Convert.ToDouble(sss);
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

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + "selected File is not in valid Format...", "Warning");

                }
                Fl.Close();
            }

        }

        public void smootht4()
        {
            double I0, I1 = 0;
            int vn = shomar_val;

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

            chart1.Series[0].Points.Clear();
            for (int i = 0; i < vn; i++)
            {
                chart1.Series[0].Points.AddXY(othertech_val0[i], othertech_val1[i]);
            }

        }
        
        private string fillparam(int typecall, int row)
        {
            string str_fillparam = "";
            try
            {
                if (typecall == 1)
                {
                    str_fillparam = "Properties :\n" + Environment.NewLine;
                    label41.Text = "";
                    label41.Text = "Technique name :" + classglobal.TechName[int.Parse(dataSet11.analyselist.Rows[row][0].ToString())];
                }
                else
                {
                    str_fillparam = "Technique name :" + classglobal.TechName[int.Parse(dataSet11.analyselist.Rows[row][0].ToString())] + "\n\n" + "Properties :" + "\n";
                }
                switch (byte.Parse(dataSet11.analyselist.Rows[row][0].ToString()))
                {
                    case classglobal.DCV:


                        if (double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][4].ToString()) != 9999) str_fillparam = str_fillparam + "HStep =" + double.Parse(dataSet11.analyselist.Rows[row][4].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][6].ToString()) != 9999) str_fillparam = str_fillparam + "Scan Rate =" + double.Parse(dataSet11.analyselist.Rows[row][6].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][7].ToString()) != 9999) str_fillparam = str_fillparam + "TStep =" + double.Parse(dataSet11.analyselist.Rows[row][7].ToString()) + "\n";
                        break;
                    case classglobal.NPV:
                        if (double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][4].ToString()) != 9999) str_fillparam = str_fillparam + "HStep =" + double.Parse(dataSet11.analyselist.Rows[row][4].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][8].ToString()) != 9999) str_fillparam = str_fillparam + "Pulse Width =" + double.Parse(dataSet11.analyselist.Rows[row][8].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][6].ToString()) != 9999) str_fillparam = str_fillparam + "Scan Rate =" + double.Parse(dataSet11.analyselist.Rows[row][6].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][7].ToString()) != 9999) str_fillparam = str_fillparam + "TStep =" + double.Parse(dataSet11.analyselist.Rows[row][7].ToString()) + "\n";
                        break;
                    case classglobal.DPV:
                        if (double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][4].ToString()) != 9999) str_fillparam = str_fillparam + "HStep =" + double.Parse(dataSet11.analyselist.Rows[row][4].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][8].ToString()) != 9999) str_fillparam = str_fillparam + "Pulse Width =" + double.Parse(dataSet11.analyselist.Rows[row][8].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][9].ToString()) != 9999) str_fillparam = str_fillparam + "Pulse Height =" + double.Parse(dataSet11.analyselist.Rows[row][9].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][6].ToString()) != 9999) str_fillparam = str_fillparam + "Scan Rate =" + double.Parse(dataSet11.analyselist.Rows[row][6].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][7].ToString()) != 9999) str_fillparam = str_fillparam + "TStep =" + double.Parse(dataSet11.analyselist.Rows[row][7].ToString()) + "\n";
                        break;
                    case classglobal.SWV:
                        if (double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][4].ToString()) != 9999) str_fillparam = str_fillparam + "HStep =" + double.Parse(dataSet11.analyselist.Rows[row][4].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][9].ToString()) != 9999) str_fillparam = str_fillparam + "Pulse Height =" + double.Parse(dataSet11.analyselist.Rows[row][9].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][6].ToString()) != 9999) str_fillparam = str_fillparam + "Scan Rate =" + double.Parse(dataSet11.analyselist.Rows[row][6].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][10].ToString()) != 9999) str_fillparam = str_fillparam + "Frequency =" + double.Parse(dataSet11.analyselist.Rows[row][10].ToString()) + "\n";
                        break;
                    case classglobal.CV:
                        if (double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][3].ToString()) != 9999) str_fillparam = str_fillparam + "E3 =" + double.Parse(dataSet11.analyselist.Rows[row][3].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][4].ToString()) != 9999) str_fillparam = str_fillparam + "HStep =" + double.Parse(dataSet11.analyselist.Rows[row][4].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][11].ToString()) != 9999) str_fillparam = str_fillparam + "Cycles =" + double.Parse(dataSet11.analyselist.Rows[row][11].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][6].ToString()) != 9999) str_fillparam = str_fillparam + "Scan Rate =" + double.Parse(dataSet11.analyselist.Rows[row][6].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][7].ToString()) != 9999) str_fillparam = str_fillparam + "TStep =" + double.Parse(dataSet11.analyselist.Rows[row][7].ToString()) + "\n";
                        break;
                    case classglobal.LSV:
                        if (double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][4].ToString()) != 9999) str_fillparam = str_fillparam + "HStep =" + double.Parse(dataSet11.analyselist.Rows[row][4].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][6].ToString()) != 9999) str_fillparam = str_fillparam + "Scan Rate =" + double.Parse(dataSet11.analyselist.Rows[row][6].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][7].ToString()) != 9999) str_fillparam = str_fillparam + "TStep =" + double.Parse(dataSet11.analyselist.Rows[row][7].ToString()) + "\n";
                        break;
                //    case classglobal.DCs:
                //        if (double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) + "\n";
                //        if (double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) + "\n";
                //        if (double.Parse(dataSet11.analyselist.Rows[row][4].ToString()) != 9999) str_fillparam = str_fillparam + "HStep =" + double.Parse(dataSet11.analyselist.Rows[row][4].ToString()) + "\n";
                //        if (double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) + "\n";
                //        if (double.Parse(dataSet11.analyselist.Rows[row][6].ToString()) != 9999) str_fillparam = str_fillparam + "Scan Rate =" + double.Parse(dataSet11.analyselist.Rows[row][6].ToString()) + "\n";
                //        if (double.Parse(dataSet11.analyselist.Rows[row][7].ToString()) != 9999) str_fillparam = str_fillparam + "TStep =" + double.Parse(dataSet11.analyselist.Rows[row][7].ToString()) + "\n";

                //        break;
                //    case classglobal.DPs:
                //        if (double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) + "\n";
                //        if (double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) + "\n";
                //        if (double.Parse(dataSet11.analyselist.Rows[row][4].ToString()) != 9999) str_fillparam = str_fillparam + "HStep =" + double.Parse(dataSet11.analyselist.Rows[row][4].ToString()) + "\n";
                //        if (double.Parse(dataSet11.analyselist.Rows[row][9].ToString()) != 9999) str_fillparam = str_fillparam + "Pulse Height =" + double.Parse(dataSet11.analyselist.Rows[row][9].ToString()) + "\n";
                //        if (double.Parse(dataSet11.analyselist.Rows[row][8].ToString()) != 9999) str_fillparam = str_fillparam + "Pulse Width =" + double.Parse(dataSet11.analyselist.Rows[row][8].ToString()) + "\n";
                //        if (double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) + "\n";
                //       if (double.Parse(dataSet11.analyselist.Rows[row][6].ToString()) != 9999) str_fillparam = str_fillparam + "Scan Rate =" + double.Parse(dataSet11.analyselist.Rows[row][6].ToString()) + "\n";
                //        if (double.Parse(dataSet11.analyselist.Rows[row][7].ToString()) != 9999) str_fillparam = str_fillparam + "TStep =" + double.Parse(dataSet11.analyselist.Rows[row][7].ToString()) + "\n";

                //        break;
                    case classglobal.CPC:
                        if (double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][13].ToString()) != 9999) str_fillparam = str_fillparam + "T1 =" + double.Parse(dataSet11.analyselist.Rows[row][13].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) + "\n";
                        //if (double.Parse(dataSet11.analyselist.Rows[row][11].ToString()) != 9999) //str_fillparam = str_fillparam + "Cycles =" + double.Parse(dataSet11.analyselist.Rows[row][11].ToString()) + "\n";
                        break;
                 //   case classglobal.CCC:
                 //       if (double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) + "\n";
                 //       if (double.Parse(dataSet11.analyselist.Rows[row][13].ToString()) != 9999) str_fillparam = str_fillparam + "T1 =" + double.Parse(dataSet11.analyselist.Rows[row][13].ToString()) * 1000 + "\n";
                 //       if (double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) + "\n";
                 //       //if (double.Parse(dataSet11.analyselist.Rows[row][11].ToString()) != 9999) //str_fillparam = str_fillparam + "Cycles =" + double.Parse(dataSet11.analyselist.Rows[row][11].ToString()) + "\n";
                 //       break;
                    case classglobal.CHP:
                        if (double.Parse(dataSet11.analyselist.Rows[row][15].ToString()) != 9999) str_fillparam = str_fillparam + "I1 =" + double.Parse(dataSet11.analyselist.Rows[row][15].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][16].ToString()) != 9999) str_fillparam = str_fillparam + "I2 =" + double.Parse(dataSet11.analyselist.Rows[row][16].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][13].ToString()) != 9999) str_fillparam = str_fillparam + "T1 =" + double.Parse(dataSet11.analyselist.Rows[row][13].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][14].ToString()) != 9999) str_fillparam = str_fillparam + "T2 =" + double.Parse(dataSet11.analyselist.Rows[row][14].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) + "\n";
                        //if (double.Parse(dataSet11.analyselist.Rows[row][11].ToString()) != 9999) str_fillparam = str_fillparam + "Cycles =" + double.Parse(dataSet11.analyselist.Rows[row][11].ToString()) + "\n";
                        break;
                    case classglobal.CHA:
                        if (double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][13].ToString()) != 9999) str_fillparam = str_fillparam + "T1 =" + double.Parse(dataSet11.analyselist.Rows[row][13].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][14].ToString()) != 9999) str_fillparam = str_fillparam + "T2 =" + double.Parse(dataSet11.analyselist.Rows[row][14].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) + "\n";
                        //if (double.Parse(dataSet11.analyselist.Rows[row][11].ToString()) != 9999) str_fillparam = str_fillparam + "Cycles =" + double.Parse(dataSet11.analyselist.Rows[row][11].ToString()) + "\n";
                        break;
                    case classglobal.CHC:
                        if (double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) != 9999) str_fillparam = str_fillparam + "E1 =" + double.Parse(dataSet11.analyselist.Rows[row][1].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) != 9999) str_fillparam = str_fillparam + "E2 =" + double.Parse(dataSet11.analyselist.Rows[row][2].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][13].ToString()) != 9999) str_fillparam = str_fillparam + "T1 =" + double.Parse(dataSet11.analyselist.Rows[row][13].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][14].ToString()) != 9999) str_fillparam = str_fillparam + "T2 =" + double.Parse(dataSet11.analyselist.Rows[row][14].ToString()) + "\n";
                        if (double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) != 9999) str_fillparam = str_fillparam + "Equilibrium Time =" + double.Parse(dataSet11.analyselist.Rows[row][5].ToString()) + "\n";
                        //if (double.Parse(dataSet11.analyselist.Rows[row][11].ToString()) != 9999) str_fillparam = str_fillparam + "Cycles =" + double.Parse(dataSet11.analyselist.Rows[row][11].ToString()) + "\n";
                        break;

                }
                str_fillparam = str_fillparam + "comment =" + dataSet11.analyselist.Rows[row]["comment"].ToString();
                //pGrid.Refresh();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return str_fillparam;

        }
        
        private void button11_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            //try
            //{
            if (chart1.Series.Count > 0)
            {

                int j = -1;
                for (int i = 0; i < this.treeView1.Nodes[0].Nodes.Count; i++)
                    if (this.treeView1.Nodes[0].Nodes[i].Checked == true)//فقط اولین سری که تیک خورده باشد انتخاب می شود
                    {
                        j = i;

                        for (int k = i + 1; k < this.treeView1.Nodes[0].Nodes.Count; k++)//غیر تیک کردن بقیه گرافها
                            this.treeView1.Nodes[0].Nodes[k].Checked = false;

                        TreeNode sel = treeView1.Nodes[0].Nodes[i];//انتخاب گراف تیک دار
                        treeView1.SelectedNode = sel;
                        treeView1.Focus();

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

                Counter_num = this.chart1.Series[j].Points.Count;

                Array.Resize(ref volt, Counter_num);
                Array.Resize(ref current, Counter_num);



                MessageBox.Show(volt.Count().ToString());

                if (this.chart2.Series.Count == 0)
                {
                    txt_res.Text = (Convert.ToInt16(Counter_num / 20)).ToString();//این عدد تجربی است یعنی حالتی است که بیشتر قله ها گرفته میشود
                    txt_noise.Text = "0";
                }


                this.chart2.Series.Clear();//پاک کردن گرافها
                this.chart2.Refresh();

                Thread.Sleep(100);//جهت متوجه شدن کاربر                               


                Array.Clear(series_height_index, 0, series_height_index.Length);//جهت نگهداری اندیس سری هایی از چارت که مربوط با ارتفاع است برای نمایش چک باکس
                Array.Resize(ref series_height_index, 0);
                Array.Clear(series_width_index, 0, series_width_index.Length);
                Array.Resize(ref series_width_index, 0);
                Array.Clear(series_area_index, 0, series_area_index.Length);
                Array.Resize(ref series_area_index, 0);


                int ser = 0;

                this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());
                this.chart2.Titles.Add("");
                this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                this.chart2.Series[ser].Color = chart1.Series[0].Color;
                this.chart2.Series[ser].BorderWidth = 2;
                this.chart2.Titles[ser].Text = "Peak Search";
                this.chart2.ChartAreas[0].AxisY.Title = chart1.ChartAreas[0].AxisY.Title;
                this.chart2.ChartAreas[0].AxisX.Title = chart1.ChartAreas[0].AxisX.Title;
                ser++;


                if (dataSet11.analyselist.Rows[j][0].ToString() == "10" || dataSet11.analyselist.Rows[j][0].ToString() == "11" || dataSet11.analyselist.Rows[j][0].ToString() == "12")
                    this.chart2.Series[0].Points.AddXY(0, othertech_val1[0]);//رسم در نقطه صفر زمان // technics chp cha chc
                volt[0] = this.chart1.Series[0].Points[0].XValue;
                for (int i = 0; i < Counter_num; i++)
                {
                    volt[i] = this.chart1.Series[j].Points[i].XValue;
                    current[i] = this.chart1.Series[j].Points[i].YValues[0];

                    this.chart2.Series[0].Points.AddXY(volt[i], current[i]);
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


                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم بیس لاین
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.Red;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(volt[start_index], current[start_index]);
                        this.chart2.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                        ser++;


                        string a = aria.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                        try { a = a.Substring(0, a.IndexOf('.') + 6); } catch { }
                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//نمایش اندازه مساحت
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.Red;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                        this.chart2.Series[ser].Points[0].Label = "Aria: " + a;
                        Array.Resize(ref series_area_index, series_area_index.Length + 1);//قرار دادن اندیس برای نمایش با چک باکس
                        series_area_index[series_area_index.Length - 1] = ser;
                        ser++;


                        string height = max_jarian_khales.ToString();
                        try { height = height.Substring(0, height.IndexOf('.') + 4); } catch { }
                        chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم ارتفاع یا همان جریان خالص
                        chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        chart2.Series[ser].Color = Color.Gold;
                        chart2.Series[ser].BorderWidth = 1;
                        chart2.Series[ser].Points.AddXY(volt[peak], current[peak]);
                        //tChart2.Series[ser].Points.AddXY(volt[peak], current[peak] - (max_jarian_khales / 4));//این نقطه به جهت نمایش لیبل ارتفاع خالص است
                        this.chart2.Series[ser].Points.AddXY(volt[peak], base_peak);
                        this.chart2.Series[ser].Points[0].Label = "H: " + height;
                        Array.Resize(ref series_height_index, series_height_index.Length + 1);//قرار دادن اندیس برای نمایش با چک باکس
                        series_height_index[series_height_index.Length - 1] = ser;
                        ser++;


                        string w = pahna_dar_nesf.ToString();
                        try { w = w.Substring(0, w.IndexOf('.') + 4); } catch { }
                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم پهنا در نصف ارتفاع
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.SpringGreen;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(p1, nesf);
                        //tChart2.Series[ser].Points.AddXY((volt[maxs[i]] + p1) / 2, nesf);//این نقطه به جهت نمایش لیبل پهنا است
                        this.chart2.Series[ser].Points.AddXY(p2, nesf);
                        this.chart2.Series[ser].Points[0].Label = "W: " + w;
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


                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم بیس لاین
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.Red;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(volt[pt1], current[pt1]);
                        this.chart2.Series[ser].Points.AddXY(volt[pt2], current[pt2]);
                        this.chart2.Series[ser].Points.AddXY(x5, y5);
                        ser++;

                        string a = aria.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                        try { a = a.Substring(0, a.IndexOf('.') + 6); } catch { }
                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//نمایش اندازه مساحت
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.Red;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                        this.chart2.Series[ser].Points[0].Label = "Aria Forward: " + a;
                        Array.Resize(ref series_area_index, series_area_index.Length + 1);//قرار دادن اندیس برای نمایش با چک باکس
                        series_area_index[series_area_index.Length - 1] = ser;
                        ser++;

                        string height = max_jarian_khales.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                        try { height = height.Substring(0, height.IndexOf('.') + 4); } catch { }
                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم ارتفاع یا همان جریان خالص
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.Gold;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(volt[peak], current[peak]);
                        this.chart2.Series[ser].Points.AddXY(volt[peak], base_peak);
                        this.chart2.Series[ser].Points[0].Label = "H: " + height;
                        Array.Resize(ref series_height_index, series_height_index.Length + 1);
                        series_height_index[series_height_index.Length - 1] = ser;
                        ser++;

                        string w = pahna_dar_nesf.ToString();
                        try { w = w.Substring(0, w.IndexOf('.') + 4); } catch { }
                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم پهنا در نصف ارتفاع
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.SpringGreen;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(p1, nesf);
                        this.chart2.Series[ser].Points.AddXY(p2, nesf);
                        this.chart2.Series[ser].Points[0].Label = "W: " + w;
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



                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم بیس لاین
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.Red;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(volt[pt1], current[pt1]);
                        this.chart2.Series[ser].Points.AddXY(volt[pt2], current[pt2]);
                        this.chart2.Series[ser].Points.AddXY(x5, y5);
                        ser++;


                        string a = aria.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                        try { a = a.Substring(0, a.IndexOf('.') + 6); } catch { }
                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//نمایش اندازه مساحت
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.Red;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                        this.chart2.Series[ser].Points[0].Label = "Aria Backward: " + a;
                        Array.Resize(ref series_area_index, series_area_index.Length + 1);//قرار دادن اندیس برای نمایش با چک باکس
                        series_area_index[series_area_index.Length - 1] = ser;
                        ser++;

                        string height = max_jarian_khales.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                        try { height = height.Substring(0, height.IndexOf('.') + 4); } catch { }
                        chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم ارتفاع یا همان جریان خالص
                        chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        chart2.Series[ser].Color = Color.Gold;
                        chart2.Series[ser].BorderWidth = 1;
                        chart2.Series[ser].Points.AddXY(volt[peak], current[peak]);
                        chart2.Series[ser].Points.AddXY(volt[peak], base_peak);
                        chart2.Series[ser].Points[0].Label = "H: " + height;
                        Array.Resize(ref series_height_index, series_height_index.Length + 1);
                        series_height_index[series_height_index.Length - 1] = ser;
                        ser++;


                        string w = pahna_dar_nesf.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                        try { w = w.Substring(0, w.IndexOf('.') + 4); } catch { }
                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم پهنا در نصف ارتفاع
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.SpringGreen;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(p1, nesf);
                        this.chart2.Series[ser].Points.AddXY(p2, nesf);
                        this.chart2.Series[ser].Points[0].Label = "W: " + w;
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
            //}
            //catch (Exception x)
            //{
            //    MessageBox.Show(x.Message);
            //}

            this.Cursor = Cursors.Default;
        }


        private void selectpchart_func_1(bool state)
        {

            chart2.ChartAreas[0].CursorX.IsUserEnabled = state;
            chart2.ChartAreas[0].CursorY.IsUserEnabled = state;
            chart2.ChartAreas[0].CursorX.IsUserSelectionEnabled = state;
            chart2.ChartAreas[0].CursorY.IsUserSelectionEnabled = state;
            chart2.ChartAreas[0].CursorX.Interval = 0;
            chart2.ChartAreas[0].CursorY.Interval = 0;
            if (state)
            {
                chart2.ChartAreas[0].CursorX.LineWidth = 1;
                chart2.ChartAreas[0].CursorY.LineWidth = 1;
            }
            else
            {
                chart2.ChartAreas[0].CursorX.LineWidth = 0;
                chart2.ChartAreas[0].CursorY.LineWidth = 0;
            }
            chart2.Focus();
        }

        private void Btn_zoon_in_down_Click_1(object sender, EventArgs e)
        {
            statebutton_1 = "";
            selectpchart_func_1(false);
            this.chart2.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            double xMin = this.chart2.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            double xMax = this.chart2.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
            double yMin = this.chart2.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
            double yMax = this.chart2.ChartAreas[0].AxisY.ScaleView.ViewMaximum;
            this.chart2.ChartAreas[0].AxisX.ScaleView.Zoom(xMin, 0.95 * (xMax - xMin), DateTimeIntervalType.Auto, true);
            this.chart2.ChartAreas[0].AxisY.ScaleView.Zoom(yMin, 0.95 * (yMax - yMin), DateTimeIntervalType.Auto, true);
            difxview2 = this.chart2.ChartAreas[0].AxisX.ScaleView.ViewMaximum - this.chart2.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            difyview2 = this.chart2.ChartAreas[0].AxisY.ScaleView.ViewMaximum - this.chart2.ChartAreas[0].AxisY.ScaleView.ViewMinimum;

            this.chart2.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            this.chart2.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
            this.chart2.ChartAreas[0].AxisX.ScrollBar.BackColor = Color.DarkGray;
            this.chart2.ChartAreas[0].AxisY.ScrollBar.BackColor = Color.DarkGray;

        }
         
        private void Btn_select_only_down_Click_1(object sender, EventArgs e)
        {
            if (statebutton_1 == "selectonly")
            {
                statebutton_1 = "";
                selectpchart_func_1(false);
//                btn_select_only_down.FlatAppearance.BorderSize = 0;
            }
            else
            {
                statebutton_1 = "selectonly";
                selectpchart_func_1(true);
//                btn_select_only_down.FlatAppearance.BorderSize = 1;
//                btn_select_only_down.FlatAppearance.BorderColor = Color.Black;

            }
        }

        private void Btn_zoom_out_down_Click_1(object sender, EventArgs e)
        {
            statebutton_1 = "";
            selectpchart_func_1(false);
            this.chart2.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            this.chart2.ChartAreas[0].AxisX.ScaleView.ZoomReset(1);
            this.chart2.ChartAreas[0].AxisY.ScaleView.ZoomReset(1);

        }

        private void Btn_show_grid_down_Click_1(object sender, EventArgs e)
        {
            statebutton_1 = "";
            selectpchart_func_1(false);
            this.chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = !this.chart2.ChartAreas[0].AxisX.MajorGrid.Enabled;
            this.chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = !this.chart2.ChartAreas[0].AxisY.MajorGrid.Enabled;

        }

        private void Btn_show_axis_down_Click_1(object sender, EventArgs e)
        {
            statebutton_1 = "";
            selectpchart_func_1(false);
            this.chart2.ChartAreas[0].AxisX.LabelStyle.Enabled = !this.chart2.ChartAreas[0].AxisX.LabelStyle.Enabled;
            this.chart2.ChartAreas[0].AxisX.MajorTickMark.Enabled = !this.chart2.ChartAreas[0].AxisX.MajorTickMark.Enabled;
            this.chart2.ChartAreas[0].AxisY.LabelStyle.Enabled = !this.chart2.ChartAreas[0].AxisY.LabelStyle.Enabled;
            this.chart2.ChartAreas[0].AxisY.MajorTickMark.Enabled = !this.chart2.ChartAreas[0].AxisY.MajorTickMark.Enabled;

        }

        private void Btn_export_down_Click_1(object sender, EventArgs e)
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
                    if (this.chart2.Series.Count > 0 && this.chart2.Series[0].Points.Count > 0)
                        shomarcheck = 1;
                    i1 = 0;
                    if (shomarcheck == 1)
                    {
                        string strlegend = fillparam(2, nindexchart2);
                        frmshowgraph frm1 = new frmshowgraph(strlegend.Trim(), this.chart2.Series[i1].Points.FindMinByValue("X").XValue, this.chart2.Series[i1].Points.FindMaxByValue("X").XValue, this.chart2.Series[i1].Points.FindMinByValue("Y").YValues[0], this.chart2.Series[i1].Points.FindMaxByValue("Y").YValues[0], "", this.chart2.Titles[0].Text.Trim());
                        if (frm1.Chart1.Series.Count > 0)
                            frm1.Chart1.Series[0].Points.Clear();
                        for (int i = 0; i < chart2.Series[i1].Points.Count; i++)
                        {
                            frm1.Chart1.Series[0].Points.AddXY(this.chart2.Series[i1].Points[i].XValue, this.chart2.Series[i1].Points[i].YValues[0]);
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

        private void toolStripButton7_Click_1(object sender, EventArgs e)
        {
            try
            {

                if (this.chart2.Series.Count <= 0)
                    return;
                if (this.chart2.Series[0].Points.Count > 0)
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

        private string ExportToExcel(int colnum1, int typecall1, string outputPath)
        {
            try
            {
                // ساخت یک شی اکسل
                //ApplicationClass excelApp = new ApplicationClass();
                Microsoft.Office.Interop.Excel.Application excelApp;
                excelApp = new Microsoft.Office.Interop.Excel.Application();


                // ساخت یک WorkBook جدید
                Workbook excelWorkbook = excelApp.Workbooks.Add(Type.Missing);


                int sheetIndex = 0, i2 = 0, i1 = 0;

                // ساخت آرایه به طول تعداد سطرهای دیتاتیبل+1 و تعداد ستونهای دیتاتیبل
                int rownum1 = 0;
                if (typecall1 == 1)
                {
                    for (int m1 = 0; m1 < chart1.Series.Count; m1++)
                    {
                        rownum1 += this.chart1.Series[m1].Points.Count;
                    }
                }
                else
                {
                    rownum1 = this.chart2.Series[0].Points.Count;
                }
                object[,] rawData = new object[rownum1 + 1, colnum1];
                if (typecall1 == 1)
                {
                    rawData[i2, 0] = "technique number";
                    rawData[i2, 1] = "technique name";
                    rawData[i2, 2] = "point of technique number";
                    rawData[i2, 3] = "X value";
                    rawData[i2, 4] = "Y value";
                    i2++;
                    for (int i = 0; i < this.treeView1.Nodes[0].Nodes.Count; i++)
                        if (this.treeView1.Nodes[0].Nodes[i].Checked)
                        {
                            for (int cc1 = 0; cc1 < this.chart1.Series[i].Points.Count; cc1++)
                            {
                                rawData[i2, 0] = i1;
                                rawData[i2, 1] = classglobal.TechName[int.Parse(dataSet11.analyselist.Rows[i][0].ToString())];
                                rawData[i2, 2] = cc1 + 1;
                                rawData[i2, 3] = this.chart1.Series[i].Points[cc1].XValue;
                                rawData[i2, 4] = this.chart1.Series[i].Points[cc1].YValues[0];
                                i2++;
                            }
                            i1++;


                        }
                }
                else
                {
                    rawData[i2, 0] = "row";
                    rawData[i2, 1] = "X value";
                    rawData[i2, 2] = "Y value";
                    i2++;
                    for (int cc1 = 0; cc1 < this.chart2.Series[0].Points.Count; cc1++)
                    {
                        rawData[i2, 0] = cc1 + 1;
                        rawData[i2, 1] = this.chart2.Series[0].Points[cc1].XValue;
                        rawData[i2, 2] = this.chart2.Series[0].Points[cc1].YValues[0];
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
        
        private void toolStripButton8_Click_1(object sender, EventArgs e)
        {
            int i1 = 1, i2 = 1;
            if (this.chart2.Series.Count <= 0)
                return;
            if (this.chart2.Series[0].Points.Count > 0)
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

                        Fl.WriteLine(chart2.Titles[0].Text);
                        Fl.WriteLine("---------------------------------------------------");
                        Fl.WriteLine("row number      X value         Y value");
                        Fl.WriteLine("---------------------------------------------------");

                        for (int cc1 = 0; cc1 < chart2.Series[0].Points.Count; cc1++)
                        {
                            Fl.Write((cc1 + 1).ToString().Trim() + "\t\t");
                            Fl.Write(chart2.Series[0].Points[cc1].XValue.ToString() + "\t\t");
                            Fl.WriteLine(chart2.Series[0].Points[cc1].YValues[0]);
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

 

        public void draw_point(double x, double y)
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

                chart2.Series[1].Points.AddXY(x_2, final_y);

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


        private void Chart2_MouseDown(object sender, MouseEventArgs e)
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
                    mDown = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X);
                    mDowny = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y);
                }

                double x = chart2.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X);
                double y = chart2.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y);

                if (start_click == 1)
                {
                    chart2.Series[1].ChartType = SeriesChartType.Point;
                    chart2.Series[1].Color = Color.Red;
                    chart2.Series[1].BorderWidth = 3;
                    draw_point(x, y);//پیدا کردن نزدیکترین نقطه به نقطه رسم شده در گراف    
                }
                else if (end_click == 1)
                {
                    chart2.Series[1].ChartType = SeriesChartType.Spline;
                    chart2.Series[1].Color = Color.Red;
                    chart2.Series[1].BorderWidth = 1;
                    draw_point(x, y);

                    end_click = 0;
                    this.Cursor = Cursors.Default;
                    chart2.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void Chart2_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (statebutton_1.Trim() == "")
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        var results1 = chart1.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                        foreach (var result in results1)
                        {
                            if (result.ChartElementType == ChartElementType.PlottingArea)
                            {
                                double xv = chart2.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X);
                                double yv = chart2.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y);
                                if (xv - mDown > 0)
                                    chart2.ChartAreas[0].AxisX.ScaleView.Position = chart2.ChartAreas[0].AxisX.ScaleView.ViewMinimum + (difxview2 / 500);
                                else if (xv - mDown < 0)
                                    chart2.ChartAreas[0].AxisX.ScaleView.Position = chart2.ChartAreas[0].AxisX.ScaleView.ViewMinimum - (difxview2 / 500);
                                else
                                    chart2.ChartAreas[0].AxisX.ScaleView.Position = chart2.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                                if ((yv - mDowny) > 0)
                                    chart2.ChartAreas[0].AxisY.ScaleView.Position = chart2.ChartAreas[0].AxisY.ScaleView.ViewMinimum + (difyview2 / 500);
                                else if ((yv - mDowny) < 0)
                                    chart2.ChartAreas[0].AxisY.ScaleView.Position = chart2.ChartAreas[0].AxisY.ScaleView.ViewMinimum - (difyview2 / 500);
                                else
                                    chart2.ChartAreas[0].AxisY.ScaleView.Position = chart2.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                                chart2.ChartAreas[0].AxisX.ScaleView.Size = difxview2; //(tChart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum - tChart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum); //+ xv - mDown;tChart1.ChartAreas[0].AxisX.Minimum +
                                chart2.ChartAreas[0].AxisY.ScaleView.Size = difyview2;// (tChart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum - tChart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum) + yv - mDowny;//tChart1.ChartAreas[0].AxisX.Minimum +

                                mDown = xv;
                                mDowny = yv;
                            }

                        }
                    }
                }

                var results11 = chart2.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                foreach (var result in results11)
                {
                    if (result.ChartElementType == ChartElementType.PlottingArea)
                    {

                        switch (statebutton_1)
                        {
                            case "selectonly":
                                chart2.Cursor = System.Windows.Forms.Cursors.Cross;
                                break;
                            case "showvalue":
                                chart2.Cursor = System.Windows.Forms.Cursors.SizeAll;

                                break;

                            default:
                                if (start_click == 0 && end_click == 0)
                                    chart2.Cursor = System.Windows.Forms.Cursors.Default;
                                break;
                        }

                    }
                    else if (start_click == 0 && end_click == 0)
                        chart2.Cursor = System.Windows.Forms.Cursors.Default;
                }
                switch (statebutton_1)
                {
                    case "selectonly":
                        System.Drawing.Point mousePoint = new System.Drawing.Point(e.X, e.Y);
                        chart2.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, false);
                        chart2.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, false);

                        break;
                    case "showvalue":
                        var results12 = chart2.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
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
                                this.chart2.Refresh();
                            }
                        }
                        break;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void Chart2_MouseUp(object sender, MouseEventArgs e)
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

        private void Chart2_Paint(object sender, PaintEventArgs e)
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

        private void Chart1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                drawline1.EnableDraw = true;
            if (e.KeyValue == 91)
            {
                chart.Dispose();
            }
            
        }

        private void Chart1_KeyUp(object sender, KeyEventArgs e)
        {
            drawline1.EnableDraw = false;
            
        }

        private void Chart1_MouseDown(object sender, MouseEventArgs e)
        {/*
            try
            {
                System.Windows.Forms.DataVisualization.Charting.Axis ax = chart1.ChartAreas[0].AxisX;
                mDown = ax.PixelPositionToValue(e.Location.X);
                System.Windows.Forms.DataVisualization.Charting.Axis ay = chart1.ChartAreas[0].AxisY;
                mDowny = ay.PixelPositionToValue(e.Location.Y);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            */
        }

        private void Chart1_MouseUp(object sender, MouseEventArgs e)
        {/*
            mDown = double.NaN;
            mDowny = double.NaN;
            */
        }

        private void Btn_delete_line_down_Click(object sender, EventArgs e)
        {

            if (drawline1.Lines.Count == 0)
            {
                MessageBox.Show("There is not any line for deleting !!!");
            }
            if (numline > -1 && numline < drawline1.Lines.Count)
            {
                drawline1.remove(numline);
                numline--;
                this.chart2.Refresh();
            }

        }

        private void Btn_draw_line_down_Click(object sender, EventArgs e)
        {
            if (statebutton_1.Trim() == "drawline")
            {
                statebutton_1 = "";
                selectpchart_func_1(false);
            }
            else
            {
                statebutton_1 = "drawline";
                selectpchart_func_1(true);

            }

        }

        private void Btn_select_line_down_Click(object sender, EventArgs e)
        {
            numline++;
            if (numline >= drawline1.Lines.Count)
                numline = 0;
            if (drawline1.Lines.Count == 0)
            {
                MessageBox.Show("There is not any line for selecting !!!");
            }
            this.chart2.Refresh();
        }

        private void Btn_show_value_down_Click(object sender, EventArgs e)
        {/*
            if (statebutton_1 == "showvalue")
                statebutton_1 = "";
            else
                statebutton_1 = "showvalue";
            selectpchart_func_1(false);
            */
        }

        private void Chart2_KeyDown(object sender, KeyEventArgs e)
        {/*
            if (e.KeyValue == 108 || e.KeyValue == 76)
            {
                numline++;
                if (numline >= drawline1.Lines.Count)
                    numline = 0;
                chart2.Refresh();

            }
            else if (e.KeyValue == 189)
            {
                if (numline > -1 && numline < drawline1.Lines.Count)
                {
                    drawline1.remove(numline);
                    numline--;
                    chart2.Refresh();
                }


            }
            */
        }
       
        private void Button11_Click_1(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (this.chart1.Series.Count > 0)
                {
                    int j = -1;
                    for (int i = 0; i < this.treeView1.Nodes[0].Nodes.Count; i++)
                        if (this.treeView1.Nodes[0].Nodes[i].Checked == true)//فقط اولین سری که تیک خورده باشد انتخاب می شود
                        {
                            j = i;

                            for (int k = i + 1; k < this.treeView1.Nodes[0].Nodes.Count; k++)//غیر تیک کردن بقیه گرافها
                                this.treeView1.Nodes[0].Nodes[k].Checked = false;

                            TreeNode sel = this.treeView1.Nodes[0].Nodes[i];//انتخاب گراف تیک دار
                            this.treeView1.SelectedNode = sel;
                            this.treeView1.Focus();

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

                    Counter_num = this.chart1.Series[j].Points.Count;

                    Array.Resize(ref volt, Counter_num);
                    Array.Resize(ref current, Counter_num);


                    if (chart2.Series.Count == 0)
                    {
                        txt_res.Text = (Convert.ToInt16(Counter_num / 20)).ToString();//این عدد تجربی است یعنی حالتی است که بیشتر قله ها گرفته میشود
                        txt_noise.Text = "0";
                    }


                    this.chart2.Series.Clear();//پاک کردن گرافها
                    this.chart2.Refresh();

                    Thread.Sleep(100);//جهت متوجه شدن کاربر                               


                    Array.Clear(series_height_index, 0, series_height_index.Length);//جهت نگهداری اندیس سری هایی از چارت که مربوط با ارتفاع است برای نمایش چک باکس
                    Array.Resize(ref series_height_index, 0);
                    Array.Clear(series_width_index, 0, series_width_index.Length);
                    Array.Resize(ref series_width_index, 0);
                    Array.Clear(series_area_index, 0, series_area_index.Length);
                    Array.Resize(ref series_area_index, 0);


                    int ser = 0;

                    chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());
                    chart2.Series[ser].ChartType = SeriesChartType.Spline;
                    chart2.Series[ser].Color = chart1.Series[0].Color;
                    chart2.Series[ser].BorderWidth = 2;
                    chart2.Titles[ser].Text = "Peak Search";
                    chart2.ChartAreas[0].AxisY.Title = chart1.ChartAreas[0].AxisY.Title;
                    chart2.ChartAreas[0].AxisX.Title = chart1.ChartAreas[0].AxisX.Title;
                    ser++;


                    if (dataSet11.analyselist.Rows[j][0].ToString() == "10" || dataSet11.analyselist.Rows[j][0].ToString() == "11" || dataSet11.analyselist.Rows[j][0].ToString() == "12")
                        this.chart2.Series[0].Points.AddXY(0, othertech_val1[0]);//رسم در نقطه صفر زمان // technics chp cha chc

                    for (int i = 0; i < Counter_num; i++)
                    {
                        volt[i] = chart1.Series[j].Points[i].XValue;
                        current[i] = chart1.Series[j].Points[i].YValues[0];

                        this.chart2.Series[0].Points.AddXY(volt[i], current[i]);
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


                            this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم بیس لاین
                            this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                            this.chart2.Series[ser].Color = Color.Red;
                            this.chart2.Series[ser].BorderWidth = 1;
                            this.chart2.Series[ser].Points.AddXY(volt[start_index], current[start_index]);
                            this.chart2.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                            ser++;


                            string a = aria.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                            try { a = a.Substring(0, a.IndexOf('.') + 6); } catch { }
                            this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//نمایش اندازه مساحت
                            this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                            this.chart2.Series[ser].Color = Color.Red;
                            this.chart2.Series[ser].BorderWidth = 1;
                            this.chart2.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                            this.chart2.Series[ser].Points[0].Label = "Aria: " + a;
                            Array.Resize(ref series_area_index, series_area_index.Length + 1);//قرار دادن اندیس برای نمایش با چک باکس
                            series_area_index[series_area_index.Length - 1] = ser;
                            ser++;


                            string height = max_jarian_khales.ToString();
                            try { height = height.Substring(0, height.IndexOf('.') + 4); } catch { }
                            this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم ارتفاع یا همان جریان خالص
                            this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                            this.chart2.Series[ser].Color = Color.Gold;
                            this.chart2.Series[ser].BorderWidth = 1;
                            this.chart2.Series[ser].Points.AddXY(volt[peak], current[peak]);
                            //tChart2.Series[ser].Points.AddXY(volt[peak], current[peak] - (max_jarian_khales / 4));//این نقطه به جهت نمایش لیبل ارتفاع خالص است
                            this.chart2.Series[ser].Points.AddXY(volt[peak], base_peak);
                            this.chart2.Series[ser].Points[0].Label = "H: " + height;
                            Array.Resize(ref series_height_index, series_height_index.Length + 1);//قرار دادن اندیس برای نمایش با چک باکس
                            series_height_index[series_height_index.Length - 1] = ser;
                            ser++;


                            string w = pahna_dar_nesf.ToString();
                            try { w = w.Substring(0, w.IndexOf('.') + 4); } catch { }
                            this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم پهنا در نصف ارتفاع
                            this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                            this.chart2.Series[ser].Color = Color.SpringGreen;
                            this.chart2.Series[ser].BorderWidth = 1;
                            this.chart2.Series[ser].Points.AddXY(p1, nesf);
                            //tChart2.Series[ser].Points.AddXY((volt[maxs[i]] + p1) / 2, nesf);//این نقطه به جهت نمایش لیبل پهنا است
                            this.chart2.Series[ser].Points.AddXY(p2, nesf);
                            this.chart2.Series[ser].Points[0].Label = "W: " + w;
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


                            this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم بیس لاین
                            this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                            this.chart2.Series[ser].Color = Color.Red;
                            this.chart2.Series[ser].BorderWidth = 1;
                            this.chart2.Series[ser].Points.AddXY(volt[pt1], current[pt1]);
                            this.chart2.Series[ser].Points.AddXY(volt[pt2], current[pt2]);
                            this.chart2.Series[ser].Points.AddXY(x5, y5);
                            ser++;

                            string a = aria.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                            try { a = a.Substring(0, a.IndexOf('.') + 6); } catch { }
                            this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//نمایش اندازه مساحت
                            this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                            this.chart2.Series[ser].Color = Color.Red;
                            this.chart2.Series[ser].BorderWidth = 1;
                            this.chart2.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                            this.chart2.Series[ser].Points[0].Label = "Aria Forward: " + a;
                            Array.Resize(ref series_area_index, series_area_index.Length + 1);//قرار دادن اندیس برای نمایش با چک باکس
                            series_area_index[series_area_index.Length - 1] = ser;
                            ser++;

                            string height = max_jarian_khales.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                            try { height = height.Substring(0, height.IndexOf('.') + 4); } catch { }
                            this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم ارتفاع یا همان جریان خالص
                            this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                            this.chart2.Series[ser].Color = Color.Gold;
                            this.chart2.Series[ser].BorderWidth = 1;
                            this.chart2.Series[ser].Points.AddXY(volt[peak], current[peak]);
                            this.chart2.Series[ser].Points.AddXY(volt[peak], base_peak);
                            this.chart2.Series[ser].Points[0].Label = "H: " + height;
                            Array.Resize(ref series_height_index, series_height_index.Length + 1);
                            series_height_index[series_height_index.Length - 1] = ser;
                            ser++;

                            string w = pahna_dar_nesf.ToString();
                            try { w = w.Substring(0, w.IndexOf('.') + 4); } catch { }
                            this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم پهنا در نصف ارتفاع
                            this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                            this.chart2.Series[ser].Color = Color.SpringGreen;
                            this.chart2.Series[ser].BorderWidth = 1;
                            this.chart2.Series[ser].Points.AddXY(p1, nesf);
                            this.chart2.Series[ser].Points.AddXY(p2, nesf);
                            this.chart2.Series[ser].Points[0].Label = "W: " + w;
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



                            chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم بیس لاین
                            chart2.Series[ser].ChartType = SeriesChartType.Spline;
                            chart2.Series[ser].Color = Color.Red;
                            chart2.Series[ser].BorderWidth = 1;
                            chart2.Series[ser].Points.AddXY(volt[pt1], current[pt1]);
                            chart2.Series[ser].Points.AddXY(volt[pt2], current[pt2]);
                            chart2.Series[ser].Points.AddXY(x5, y5);
                            ser++;


                            string a = aria.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                            try { a = a.Substring(0, a.IndexOf('.') + 6); } catch { }
                            chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//نمایش اندازه مساحت
                            chart2.Series[ser].ChartType = SeriesChartType.Spline;
                            chart2.Series[ser].Color = Color.Red;
                            chart2.Series[ser].BorderWidth = 1;
                            chart2.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                            chart2.Series[ser].Points[0].Label = "Aria Backward: " + a;
                            Array.Resize(ref series_area_index, series_area_index.Length + 1);//قرار دادن اندیس برای نمایش با چک باکس
                            series_area_index[series_area_index.Length - 1] = ser;
                            ser++;

                            string height = max_jarian_khales.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                            try { height = height.Substring(0, height.IndexOf('.') + 4); } catch { }
                            chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم ارتفاع یا همان جریان خالص
                            chart2.Series[ser].ChartType = SeriesChartType.Spline;
                            chart2.Series[ser].Color = Color.Gold;
                            chart2.Series[ser].BorderWidth = 1;
                            chart2.Series[ser].Points.AddXY(volt[peak], current[peak]);
                            chart2.Series[ser].Points.AddXY(volt[peak], base_peak);
                            chart2.Series[ser].Points[0].Label = "H: " + height;
                            Array.Resize(ref series_height_index, series_height_index.Length + 1);
                            series_height_index[series_height_index.Length - 1] = ser;
                            ser++;


                            string w = pahna_dar_nesf.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                            try { w = w.Substring(0, w.IndexOf('.') + 4); } catch { }
                            chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم پهنا در نصف ارتفاع
                            chart2.Series[ser].ChartType = SeriesChartType.Spline;
                            chart2.Series[ser].Color = Color.SpringGreen;
                            chart2.Series[ser].BorderWidth = 1;
                            chart2.Series[ser].Points.AddXY(p1, nesf);
                            chart2.Series[ser].Points.AddXY(p2, nesf);
                            chart2.Series[ser].Points[0].Label = "W: " + w;
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
               
        private void Button5_Click(object sender, EventArgs e)
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

                this.chart2.Series.Clear();//پاک کردن گرافها
                this.chart2.Refresh();

                if (this.chart1.Series.Count > 0)
                {
                    int j = -1;
                    for (int i = 0; i < this.treeView1.Nodes[0].Nodes.Count; i++)
                        if (this.treeView1.Nodes[0].Nodes[i].Checked == true)//فقط اولین سری که تیک خورده باشد انتخاب می شود
                        {
                            j = i;

                            for (int k = i + 1; k < this.treeView1.Nodes[0].Nodes.Count; k++)//غیر تیک کردن بقیه گرافها
                                this.treeView1.Nodes[0].Nodes[k].Checked = false;

                            TreeNode sel = this.treeView1.Nodes[0].Nodes[i];//انتخاب گراف تیک دار
                            this.treeView1.SelectedNode = sel;
                            this.treeView1.Focus();

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

                    Counter_num = this.chart1.Series[j].Points.Count;

                    Array.Resize(ref volt, Counter_num);
                    Array.Resize(ref current, Counter_num);


                    if (this.chart2.Series.Count == 0)
                    {
                        txt_res.Text = (Convert.ToInt16(Counter_num / 20)).ToString();//این عدد تجربی است یعنی حالتی است که بیشتر قله ها گرفته میشود
                        txt_noise.Text = "0";
                    }

                    this.chart2.Series.Clear();//پاک کردن گرافها
                    this.chart2.Refresh();

                    Thread.Sleep(100);//جهت متوجه شدن کاربر

                    this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());
                    this.chart2.Series[0].ChartType = SeriesChartType.Spline;
                    this.chart2.Series[0].Color = chart1.Series[0].Color;
                    this.chart2.Series[0].BorderWidth = 2;
                    this.chart2.Titles[0].Text = "Peak Search";
                    this.chart2.ChartAreas[0].AxisY.Title = chart1.ChartAreas[0].AxisY.Title;
                    this.chart2.ChartAreas[0].AxisY.Title = this.chart1.ChartAreas[0].AxisX.Title;

                    if (dataSet11.analyselist.Rows[j][0].ToString() == "10" || dataSet11.analyselist.Rows[j][0].ToString() == "11" || dataSet11.analyselist.Rows[j][0].ToString() == "12")
                        this.chart2.Series[0].Points.AddXY(0, othertech_val1[0]);//رسم در نقطه صفر زمان // technics chp cha chc

                    for (int i = 0; i < Counter_num; i++)
                    {
                        volt[i] = this.chart1.Series[j].Points[i].XValue;
                        current[i] = this.chart1.Series[j].Points[i].YValues[0];

                        this.chart2.Series[0].Points.AddXY(volt[i], current[i]);
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
            this.chart2.Cursor = Cursors.Default;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (chart2.Series.Count == 1)
            {
                chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());

                start_click = 1;
                button6.Enabled = false;

                Cursor = Cursors.UpArrow;
                chart2.Cursor = Cursors.UpArrow;
            }
            else
                MessageBox.Show("You must first click the new button.", "Error", MessageBoxButtons.OK);

        }

        private void Button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (chart2.Series.Count == 2 && chart2.Series[1].Points.Count == 2)
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


                    double start_peak_x = this.chart2.Series[1].Points[0].XValue;
                    double start_peak_y = this.chart2.Series[1].Points[0].YValues[0];

                    double end_peak_x = this.chart2.Series[1].Points[1].XValue;
                    double end_peak_y = this.chart2.Series[1].Points[1].YValues[0];


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

                        double xx1 = volt[start_index], yy1 = current[start_index], xx2 = volt[end_index], yy2 = current[end_index];
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

                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم بیس لاین
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.Red;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(volt[start_index], current[start_index]);
                        this.chart2.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                        ser++;


                        string a = aria.ToString();
                        try { a = a.Substring(0, a.IndexOf('.') + 6); } catch { }
                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//نمایش اندازه مساحت
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.Red;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                        this.chart2.Series[ser].Points[0].Label = "Aria: " + a;
                        Array.Resize(ref series_area_index, series_area_index.Length + 1);//قرار دادن اندیس برای نمایش با چک باکس
                        series_area_index[series_area_index.Length - 1] = ser;
                        ser++;


                        string height = max_jarian_khales.ToString();
                        try { height = height.Substring(0, height.IndexOf('.') + 4); } catch { }
                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم ارتفاع یا همان جریان خالص
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.Gold;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(volt[peak], current[peak]);
                        this.chart2.Series[ser].Points.AddXY(volt[peak], base_peak);
                        this.chart2.Series[ser].Points[0].Label = "H: " + height;
                        Array.Resize(ref series_height_index, series_height_index.Length + 1);
                        series_height_index[series_height_index.Length - 1] = ser;
                        ser++;


                        string w = pahna_dar_nesf.ToString();
                        try { w = w.Substring(0, w.IndexOf('.') + 4); } catch { }
                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم پهنا در نصف ارتفاع
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.SpringGreen;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(p1, nesf);
                        this.chart2.Series[ser].Points.AddXY(p2, nesf);
                        this.chart2.Series[ser].Points[0].Label = "W: " + w;
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
                            this.chart2.Series[1].Points.AddXY(x32, y32);// رسم ادامه بیس لاین

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
                            this.chart2.Series[1].Points.AddXY(x32, y32);// رسم ادامه بیس لاین


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

                        }


                        string a = aria.ToString();// توابع مقادیر را گرد میکردند و نمایش واقعی عدد نبود
                        try { a = a.Substring(0, a.IndexOf('.') + 6); } catch { }
                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//نمایش اندازه مساحت
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.Red;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(volt[end_index], current[end_index]);
                        this.chart2.Series[ser].Points[0].Label = cv_direct + a;
                        Array.Resize(ref series_area_index, series_area_index.Length + 1);//قرار دادن اندیس برای نمایش با چک باکس
                        series_area_index[series_area_index.Length - 1] = ser;
                        ser++;

                        string height = max_jarian_khales.ToString();
                        try { height = height.Substring(0, height.IndexOf('.') + 4); } catch { }
                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم ارتفاع یا همان جریان خالص
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.Gold;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(volt[peak], current[peak]);
                        this.chart2.Series[ser].Points.AddXY(volt[peak], base_peak);
                        this.chart2.Series[ser].Points[0].Label = "H: " + height;
                        Array.Resize(ref series_height_index, series_height_index.Length + 1);
                        series_height_index[series_height_index.Length - 1] = ser;
                        ser++;


                        string w = pahna_dar_nesf.ToString();
                        try { w = w.Substring(0, w.IndexOf('.') + 4); } catch { }
                        this.chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());//رسم پهنا در نصف ارتفاع
                        this.chart2.Series[ser].ChartType = SeriesChartType.Spline;
                        this.chart2.Series[ser].Color = Color.SpringGreen;
                        this.chart2.Series[ser].BorderWidth = 1;
                        this.chart2.Series[ser].Points.AddXY(p1, nesf);
                        this.chart2.Series[ser].Points.AddXY(p2, nesf);
                        this.chart2.Series[ser].Points[0].Label = "W: " + w;
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

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            {
                forms.frmselecttypeshow frm = new frmselecttypeshow();
                frm.StartPosition = FormStartPosition.CenterScreen;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (frm.type_txt_y.Trim() != "")
                        chart2.ChartAreas[0].AxisY.LabelStyle.Format = frm.type_txt_y;
                    if (frm.type_txt_x.Trim() != "")
                        chart2.ChartAreas[0].AxisX.LabelStyle.Format = frm.type_txt_x;
                    chart2.ChartAreas[0].RecalculateAxesScale();
                }

            }
        }
         
        private void TreeView1_AfterCheck_1(object sender, TreeViewEventArgs e)
        {
            chart2.Series.Clear();
            this.chart2.Titles[0].Text = "";

            if (e.Node.Level > 0)
            {
                for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                {
                    if (treeView1.Nodes[0].Nodes[i].Checked)
                    {
                        if (i >= chart1.Series.Count)
                            chart1.Series.Add("ser" + i + 1);
                        chart1.Series[i].Enabled = true;
                        //  tChart1.Titles[0].Text = classes.clasglobal.TechName[Overlay_tech[i]];
                        // tChart1.Series[i].Title = tChart1.Header.Text;
                        chart1.ChartAreas[0].AxisY.LabelStyle.Angle = 0;
                        chart1.ChartAreas[0].AxisY.Title = classglobal.VAxisTitle[Overlay_tech[i]];
                        chart1.ChartAreas[0].AxisX.Title = classglobal.HAxisTitle[Overlay_tech[i]];
                        chart1.ChartAreas[0].RecalculateAxesScale();
                    }
                    else
                    {
                        chart1.Series[i].Enabled = false;
                        chart1.ChartAreas[0].RecalculateAxesScale();
                    }
                    difxview = chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum - chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    difyview = chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum - chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;

                }

            }
            else if (e.Node.Level == 0)
            {
                if (treeView1.Nodes[0].Checked)
                {
                    for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                        treeView1.Nodes[0].Nodes[i].Checked = true;
                }
                else
                {
                    for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                        treeView1.Nodes[0].Nodes[i].Checked = false;
                }

            }


        }



        private void ch_height_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < series_height_index.Length; i++)
            {
                if (ch_height.Checked == true)
                    chart2.Series[series_height_index[i]].Enabled = true;
                else
                    chart2.Series[series_height_index[i]].Enabled = false;
            }
        }

        private void ch_width_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < series_width_index.Length; i++)
            {
                if (ch_width.Checked == true)
                    this.chart2.Series[series_width_index[i]].Enabled = true;
                else
                    this.chart2.Series[series_width_index[i]].Enabled = false;
            }
        }

        private void ch_area_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < series_area_index.Length; i++)
            {
                if (ch_area.Checked == true)
                    this.chart2.Series[series_area_index[i]].Enabled = true;
                else
                    this.chart2.Series[series_area_index[i]].Enabled = false;
            }
        }




    }




    /*
        public class frmselecttypeshow
        {
            public string type_txt_x = "", type_txt_y = "";
            //internal object type_txt_x;

            public FormStartPosition StartPosition { get; internal set; }

            internal DialogResult ShowDialog()
            {
                throw new NotImplementedException();
            }
        }
    */

}
