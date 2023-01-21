using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;
using BHP2190.classes;
using BHP2190.forms;

namespace BHP2190
{
    public partial class frm_main : Form
    {
        public frm_main()
        {
            InitializeComponent();
        }

        Int32[] Ri = { 100, 1000, 10000, 100000, 1000000, 10000000 };//10^2 - 10^7   //مقدار مقاومت جهت نمایش جریان

        class_file class_global_ = new class_file();
        bool is_run = false;//جهت اطلاع از درحال اجرا بودن برنامه
        bool is_stop = false;

        //public Color_params cpar = new Color_params();
        public Color drawColor = new Color();



        Panel selected_tech_panel;//پنل خصوصیات تکنیک انتخاب شده تا بتوانیم در صورت انتخاب یکی دیگه قبلی را مخفی کنیم

        private void frm_main_Load(object sender, EventArgs e)
        {
            set_panels_peroperty_location();//قرار دادن پنل مشخصات تکنیک ها در محل مقرر

            set_tooltips();//ست کردن اطلاعات ویژگی های هر تکنیک

            selected_tech_panel = pnl_cv;//تکنیک جاری انتخاب شده

            set_defualt_chart_data();//نمایش یک مقدار پیش فرض برای ظاهر اولیه چارت

            set_port_setting();//ست کردن تنظیمات پورت از فایل

            set_default_grd_data();//ست کردن مقدار خالی در گرید نمایش دیتا جهت زیبایی

            set_value_to_textbox("CV");//قرار دادن مقدار پیش فرض تکنیک در تکست باکسها

            try
            {
                if (!sp1.IsOpen)
                    sp1.Open();
            }
            catch { }

            cmp_range_step.Text = "auto";

            //data[0] = "";//تمام پکت های دریافتی درون این آرایه قرار میگیرد
        }

        void set_panels_peroperty_location()//هنگام طراحی فرم در محل هایی غیر از محل واقعی قرار دارند
        {
            pnl_cv.Location = pnl_ocp.Location = pnl_dcv.Location = pnl_npv.Location = pnl_dpv.Location = pnl_swv.Location = pnl_lsv.Location = pnl_dcs.Location = pnl_dps.Location = pnl_dps.Location = pnl_cpc.Location = pnl_chp.Location = pnl_cha.Location = pnl_chc.Location = pnl_peroperty.Location;
        }
        void set_default_grd_data()
        {
            for (int i = 0; i < 8; i++)
                grd_show_data.Rows.Add(new object[] { "", "", "" });
            grd_show_data.CurrentCell.Selected = false;
        }
        void set_port_setting()
        {
            try
            {
                string path = Application.StartupPath + "\\setting.txt";

                string[] data = File.ReadAllLines(path);
                for (int i = 0; i < data.Length; i++)
                {
                    string s = data[i];
                    string field = s.Substring(0, s.IndexOf(':'));

                    switch (field)
                    {
                        case "port": sp1.PortName = s.Substring(s.IndexOf(':') + 1); break;
                        case "baudRate": sp1.BaudRate = Convert.ToInt16(s.Substring(s.IndexOf(':') + 1)); break;
                        case "readTimeout": sp1.ReadTimeout = Convert.ToInt16(s.Substring(s.IndexOf(':') + 1)); break;
                        case "writeTimeout": sp1.WriteTimeout = Convert.ToInt16(s.Substring(s.IndexOf(':') + 1)); break;
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //StripLine stripLine_x = new StripLine();//نمایش گرید به صورت دلخواه
        //StripLine stripLine_y = new StripLine();//نمایش گرید به صورت دلخواه
        void set_defualt_chart_data()
        {
            chart1.Legends.Clear();//عدم نمایش رنگ سری در راهنمای گوشه سمت راست بالا

            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
            chart1.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;

            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            chart1.ChartAreas[0].AxisX.Minimum = -50;
            chart1.ChartAreas[0].AxisX.Maximum = 50; //یک دهم برای اینکه هنگام نمایش گرید خط آخر گرید را هم نمایش دهد
            chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 10;

            chart1.ChartAreas[0].AxisY.Minimum = -50;
            chart1.ChartAreas[0].AxisY.Maximum = 50; //یک دهم برای اینکه هنگام نمایش گرید خط آخر گرید را هم نمایش دهد
            chart1.ChartAreas[0].AxisY.LabelStyle.Interval = 10;

            chart1.Series[0].Points.AddXY(0, 0);

            chart1.ChartAreas[0].AxisY.Title = "Current (mA)";
            chart1.ChartAreas[0].AxisX.Title = "Potential (mV)";

            StripLine stripLine_x = new StripLine();//نمایش گرید به صورت دلخواه
            stripLine_x.StripWidth = 0;
            stripLine_x.BorderColor = Color.LightGray;
            stripLine_x.BorderWidth = 1;
            stripLine_x.Interval = 10;
            chart1.ChartAreas[0].AxisX.StripLines.Add(stripLine_x);

            StripLine stripLine_y = new StripLine();
            stripLine_y.StripWidth = 0;
            stripLine_y.BorderColor = Color.LightGray;
            stripLine_y.BorderWidth = 1;
            stripLine_y.Interval = 10;
            chart1.ChartAreas[0].AxisY.StripLines.Add(stripLine_y);
        }

        private int shomar_val = 0;
        double[] othertech_val0 = new double[200];
        double[] othertech_val1 = new double[200];
        bool smooth_state = false;

        public int num_Series = 0;
        public byte tech = 0;
        public string techName = "";
        public int[] Overlay_tech = new int[1000];
        public short grfNum = 0, dfrmNum = 0;

        public bool Detect_Peak = false;


        void set_value_to_textbox(string tech)
        {
            class_global_.Read_default_value_FromFile(tech);//فراخوانی دیتاهای پیش فرض تکنیک از فایل و قرار دادن در متغییرهای استاتیک

            switch (tech)
            {
                case "CV":
                    txt_cv_cycle.Text = classglobal.cv_cycle;
                    txt_cv_e1.Text = classglobal.cv_e1;
                    txt_cv_e2.Text = classglobal.cv_e2;
                    txt_cv_e3.Text = classglobal.cv_e3;
                    txt_cv_sr.Text = classglobal.cv_sr;
                    txt_cv_hs.Text = classglobal.cv_hs;
                    txt_cv_ts.Text = classglobal.cv_ts;
                    txt_cv_eq.Text = classglobal.cv_eq;
                    txt_cv_ht.Text = classglobal.cv_ht;
                    txt_cv_com.Text = classglobal.cv_com;
                    break;

                case "OCP":
                    txt_ocp_time.Text = classglobal.ocp_time;
                    break;

                case "DCV":
                    txt_dcv_e1.Text = classglobal.dcv_e1;
                    txt_dcv_e2.Text = classglobal.dcv_e2;
                    txt_dcv_eq.Text = classglobal.dcv_eq;
                    txt_dcv_hs.Text = classglobal.dcv_hs;
                    txt_dcv_sr.Text = classglobal.dcv_sr;
                    txt_dcv_ts.Text = classglobal.dcv_ts;
                    txt_dcv_com.Text = classglobal.dcv_com;
                    break;

                case "NPV":
                    txt_npv_e1.Text = classglobal.npv_e1;
                    txt_npv_e2.Text = classglobal.npv_e2;
                    txt_npv_eq.Text = classglobal.npv_eq;
                    txt_npv_hs.Text = classglobal.npv_hs;
                    txt_npv_pw.Text = classglobal.npv_pw;
                    txt_npv_ts.Text = classglobal.npv_ts;
                    txt_npv_com.Text = classglobal.npv_com;
                    break;

                case "DPV":
                    txt_dpv_e1.Text = classglobal.dpv_e1;
                    txt_dpv_e2.Text = classglobal.dpv_e2;
                    txt_dpv_eq.Text = classglobal.dpv_eq;
                    txt_dpv_hs.Text = classglobal.dpv_hs;
                    txt_dpv_ph.Text = classglobal.dpv_ph;
                    txt_dpv_pw.Text = classglobal.dpv_pw;
                    txt_dpv_sr.Text = classglobal.dpv_sr;
                    txt_dpv_ts.Text = classglobal.dpv_ts;
                    txt_dpv_com.Text = classglobal.dpv_com;
                    break;

                case "SWV":
                    txt_swv_e1.Text = classglobal.swv_e1;
                    txt_swv_e2.Text = classglobal.swv_e2;
                    txt_swv_eq.Text = classglobal.swv_eq;
                    txt_swv_fr.Text = classglobal.swv_fr;
                    txt_swv_hs.Text = classglobal.swv_hs;
                    txt_swv_ph.Text = classglobal.swv_ph;
                    txt_swv_sr.Text = classglobal.swv_sr;
                    txt_swv_com.Text = classglobal.swv_com;
                    break;

                case "LSV":
                    txt_lsv_e1.Text = classglobal.lsv_e1;
                    txt_lsv_e2.Text = classglobal.lsv_e2;
                    txt_lsv_eq.Text = classglobal.lsv_eq;
                    txt_lsv_hs.Text = classglobal.lsv_hs;
                    txt_lsv_sr.Text = classglobal.lsv_sr;
                    txt_lsv_ts.Text = classglobal.lsv_ts;
                    txt_lsv_com.Text = classglobal.lsv_com;
                    break;

                case "DCS":
                    txt_dcs_e1.Text = classglobal.dcs_e1;
                    txt_dcs_e2.Text = classglobal.dcs_e2;
                    txt_dcs_eq.Text = classglobal.dcs_eq;
                    txt_dcs_hs.Text = classglobal.dcs_hs;
                    txt_dcs_sr.Text = classglobal.dcs_sr;
                    txt_dcs_ts.Text = classglobal.dcs_ts;
                    txt_dcs_com.Text = classglobal.dcs_com;
                    break;

                case "DPS":
                    txt_dps_e1.Text = classglobal.dps_e1;
                    txt_dps_e2.Text = classglobal.dps_e2;
                    txt_dps_eq.Text = classglobal.dps_eq;
                    txt_dps_hs.Text = classglobal.dps_hs;
                    txt_dps_ph.Text = classglobal.dps_ph;
                    txt_dps_pw.Text = classglobal.dps_pw;
                    txt_dps_sr.Text = classglobal.dps_sr;
                    txt_dps_ts.Text = classglobal.dps_ts;
                    txt_dps_com.Text = classglobal.dps_com;
                    break;

                case "CPC":
                    txt_cpc_e1.Text = classglobal.cpc_e1;
                    txt_cpc_t1.Text = classglobal.cpc_t1;
                    txt_cpc_eq.Text = classglobal.cpc_eq;
                    txt_cpc_com.Text = classglobal.cpc_com;
                    break;

                case "CHP":
                    txt_chp_i1.Text = classglobal.chp_i1;
                    txt_chp_i2.Text = classglobal.chp_i2;
                    txt_chp_t1.Text = classglobal.chp_t1;
                    txt_chp_t2.Text = classglobal.chp_t2;
                    txt_chp_eq.Text = classglobal.chp_eq;
                    txt_chp_com.Text = classglobal.chp_com;
                    break;

                case "CHA":
                    txt_cha_e1.Text = classglobal.cha_e1;
                    txt_cha_e2.Text = classglobal.cha_e2;
                    txt_cha_eq.Text = classglobal.cha_eq;
                    txt_cha_t1.Text = classglobal.cha_t1;
                    txt_cha_t2.Text = classglobal.cha_t2;
                    txt_cha_com.Text = classglobal.cha_com;
                    break;

                case "CHC":
                    txt_chc_e1.Text = classglobal.chc_e1;
                    txt_chc_e2.Text = classglobal.chc_e2;
                    txt_chc_eq.Text = classglobal.chc_eq;
                    txt_chc_t1.Text = classglobal.chc_t1;
                    txt_chc_t2.Text = classglobal.chc_t2;
                    txt_chc_com.Text = classglobal.chc_com;
                    break;
            }
        }


        void set_tooltips()
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(txt_cv_cycle, "Number of complete cycle(s) scan 1 - 15");
            ToolTip1.SetToolTip(txt_cv_e1, "Initial potential(V) (-8) - (+8)");
            ToolTip1.SetToolTip(txt_cv_e2, "High limit of potential scan(V) (-8) - (+8)");
            ToolTip1.SetToolTip(txt_cv_e3, "Low limit of potential scan(V) (-8) - (+8)");
            ToolTip1.SetToolTip(txt_cv_sr, "Potential scan rate(V/S) 0.0001 - 250");
            ToolTip1.SetToolTip(txt_cv_hs, "Step height (scan increment)(V) 0.001 - 0.025");
            ToolTip1.SetToolTip(txt_cv_ts, "Step time(Hstep/ScanRate)(S) 0.0001 - 10");
            ToolTip1.SetToolTip(txt_cv_eq, "Quiescent time before potential scan(S) 0 - 2000");
            ToolTip1.SetToolTip(txt_cv_ht, "Holding time after reaching E2(S) 0 - 99");
            ToolTip1.SetToolTip(txt_cv_com, "detail of expriment");

            ToolTip1.SetToolTip(txt_ocp_time, "0   +65000");

            ToolTip1.SetToolTip(txt_cpc_e1, "Step potential(V) (-8) - (+8)");
            ToolTip1.SetToolTip(txt_cpc_t1, "Step time(S) (1) - (60000)");
            ToolTip1.SetToolTip(txt_cpc_eq, "Quiescent time before start measuring(S) (0) - (2000)");
            ToolTip1.SetToolTip(txt_cpc_com, "detail of expriment");
        }

        void txt_leave(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            string tech_name = txt.Name.Substring(0, 6);//گرفتن نام تکنیک

            //if (tech_name == "txt_cv")
            //{
            string peroperty = txt.Name.Substring(7);//گرفتن نام مشخصه

            check_data(tech_name, peroperty, txt);

            //switch(peroperty )
            //{
            //    case "cycle": check_data(tech_name, peroperty, txt); break;
            //    case "e1": break;
            //    case "e2": break;
            //    case "e3": break;
            //    case "sr": break;
            //    case "hs": break;
            //    case "ts": break;
            //    case "eq": break;
            //    case "ht": break;
            //    case "com": break;
            //    //}
            //}
            //else//cpc
            //{

            //}
        }

        void check_data(string tech_name, string peroperty_name, TextBox txt)
        {
            try
            {
                double value = 0;
                if (txt.Text.Trim() != "")
                    value = Convert.ToDouble(txt.Text);

                var regex = new System.Text.RegularExpressions.Regex("(?<=[\\.])[0-9]+");//برای بررسی ارقام بعد از اعشار

                switch (tech_name)
                {
                    case "txt_cv":

                        if (peroperty_name == "e1" || peroperty_name == "e2" || peroperty_name == "e3" || peroperty_name == "hs")
                        {
                            string digit = "";//بررسی تعداد ارقام اعشار بعضی فیلدها
                            if (regex.IsMatch(value.ToString()))
                                digit = regex.Match(value.ToString()).Value;

                            if (digit.Length > 3)
                            {
                                MessageBox.Show("Must have at most three decimal places!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txt.Text = "1";
                                break;
                            }
                        }

                        switch (peroperty_name)
                        {
                            case "e1":
                                if (value < -8 || value > 8) { showError("E1(V)", "-8V", "8V"); txt.Text = "1"; } else classglobal.cv_e1 = value.ToString();
                                break;
                            case "e2":
                                if (value < -8 || value > 8) { showError("E2(V)", "-8V", "8V"); txt.Text = "1"; } else classglobal.cv_e2 = value.ToString();
                                break;
                            case "e3":
                                if (value < -8 || value > 8) { showError("E3(V)", "-8V", "8V"); txt.Text = "1"; } else classglobal.cv_e3 = value.ToString();
                                break;
                            case "sr":
                                if (value < 0.0001 || value > 250) { showError("Scan Rate(V/S)", "0.0001V/S", "250V/S"); txt.Text = "0.0001"; } else classglobal.cv_sr = value.ToString();
                                break;
                            case "hs":
                                if (value < 0.001 || value > 0.025) { showError("HStep(V)", "0.001V", "0.025V"); txt.Text = "0.001"; } else classglobal.cv_hs = value.ToString();
                                break;
                            case "ts":
                                if (value < 0.0001 || value > 10) { showError("TStep(S)", "0.0001S", "10S"); txt.Text = "0.0001"; } else classglobal.cv_ts = value.ToString();
                                break;
                            case "cycle":
                                if (value < 1 || value > 1000) { showError("Cycle(s)", "1", "1000"); txt.Text = "1"; } else classglobal.cv_cycle = value.ToString();
                                break;
                            case "eq":
                                if (value < 0 || value > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); txt.Text = "0"; } else classglobal.cv_eq = value.ToString();
                                break;
                        }

                        if (peroperty_name == "hs" || peroperty_name == "sr")
                        {
                            if (classglobal.cv_sr != "0")
                            {
                                classglobal.cv_ts = (Math.Round(Convert.ToDouble(classglobal.cv_hs) / Convert.ToDouble(classglobal.cv_sr), 5)).ToString();
                            }
                            if (Convert.ToDouble(classglobal.cv_ts) < 0.0001 || Convert.ToDouble(classglobal.cv_ts) > 10)
                            {
                                MessageBox.Show("TStep= " + classglobal.cv_ts + "!!! \nTStep < 0.0001 OR TStep > 10\n");
                                classglobal.cv_hs = "0.01";
                                classglobal.cv_sr = "0.1";
                                classglobal.cv_ts = "0.1";
                            }
                        }
                        if (peroperty_name == "e1")
                        {
                            classglobal.cv_e3 = classglobal.cv_e1;
                            txt_cv_e3.Text = value.ToString();
                        }
                        else if (peroperty_name == "e3")
                        {
                            classglobal.cv_e1 = classglobal.cv_e3;
                            txt_cv_e1.Text = value.ToString();
                        }

                        //تنظیم مقدار یک و سه برای زمانی که کاربر مقدار وارده را مضرب اچ اس قرار نمی دهد
                        //بنا براین میکرو برای شروع و پایان مقدار خودش که از فرمول پایین بدست می آید را معیار قرار می دهد
                        //در نهایت ما در این برنامه به تعداد اچ اس خودمان نقطه روی گراف نمایش می دهیم اما میکرو بخاطر اینکه خودش ست میشود تعدادش کمتر است
                        //و از طرفی برخی از مقدارهای جا
                        //مانده از سیکل قبل را در سیکل بعد نشان میدهد که کلا خطا است.همچنین در روند اجرا سیکل بخاطر اینکه شرط اتمام ندارد ذخیره نمیشود
                        //=========================================== mn
                        decimal e2 = Convert.ToDecimal(classglobal.cv_e2);
                        decimal e3 = Convert.ToDecimal(classglobal.cv_e3);
                        decimal hss = Convert.ToDecimal(classglobal.cv_hs);

                        string hs_ok = (e3 + ((e2 - e3) % hss)).ToString();//باید حاصل تفریق تقسیم بر اچ اس باقیمانده نداشته باشد چراکه در میکرو پارامتر شروع از مقدار کاربر ست نمیشود

                        if (hs_ok != "0")
                        {
                            classglobal.cv_e1 = hs_ok;
                            classglobal.cv_e3 = hs_ok;
                        }

                        break;

                    case "txt_cpc":
                        if (Convert.ToDouble(classglobal.cpc_e1) < -8 || Convert.ToDouble(classglobal.cpc_e1) > 8) { showError("E1(V)", "-8V", "8V"); classglobal.cpc_e1 = "0"; }
                        if (Convert.ToDouble(classglobal.cpc_t1) < 0 || Convert.ToDouble(classglobal.cpc_t1) > 60000) { showError("T1(V)", "0V", "65000V"); classglobal.cpc_t1 = "0"; }
                        if (Convert.ToDouble(classglobal.cpc_eq) < 0 || Convert.ToDouble(classglobal.cpc_eq) > 2000) { showError("Equilibrium Time(S)", "0S", "2000S"); classglobal.cpc_eq = "0"; }
                        break;
                }

                //UdateDsFromParams(int.Parse(this.dschart1.chartlist.Rows[rowsel][1].ToString()), rowsel, 1);
            }
            catch { }
        }

        private void showError(string err, string val1, string val2)
        {
            MessageBox.Show("Invalid Value of '" + err + "'" + (char)13 + "Value Must between '" + val1 + "' And '" + val2 + "'", "Parameter error");
        }


        private void pic_run_MouseEnter(object sender, EventArgs e)
        {
            if (is_run == false)
            {
                pic_run.Image = global::BHP2190.Properties.Resources.start_hilight;
                pic_run.BackColor = Color.FromArgb(147, 147, 147);
            }
        }

        private void pic_run_MouseLeave(object sender, EventArgs e)
        {
            if (is_run == false)
            {
                pic_run.Image = global::BHP2190.Properties.Resources.start_default;
                pic_run.BackColor = Color.Gray;
            }
        }



        public void preLoadAnalyseForm()
        {

        }






        private void pic_run_Click(object sender, EventArgs e)
        {
            if (is_run == false)//جهت اطلاع از درحال اجرا بودن برنامه
            {
                if (history == true)
                {
                    MessageBox.Show("Please first go out from History mode and then run technique", "Running", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (dataSet11.chartlist.Rows.Count == 0)
                {
                    MessageBox.Show("There is no technique to run!", "Running", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               //int r = e.Node.Index;
               int teck_id = Convert.ToInt16(dataSet11.chartlist.Rows[0][1]);
               set_data_from_form_to_database(teck_id, 0);

                pic_run.Image = global::BHP2190.Properties.Resources.start_disable;
                pic_run.BackColor = Color.FromArgb(160, 160, 160);
                is_run = true;

                row_of_technique_running = 0;//عنصر اول لیست اجرا

                cmp_range = cmp_range_step.Text.Trim();
                start_techniqu();

            }
        }


 






        Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        double e1,e2,e3,hs,t1;
        Int16 cycle, tech_id;
        Int32 sum_step;
        int row_of_technique_running;//شماره لیست تکنیک است که قرار است اجرا شود
        Thread thered_runing;
        string cmp_range = "";

        void start_techniqu()
        {
            try
            {
                /*
                if (xlApp == null)
                {
                    MessageBox.Show("Excel is not properly installed!!");
                    return;
                }
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Cells[1, 1] = "Vi";
                xlWorkSheet.Cells[1, 2] = "Current";
                xlWorkSheet.Cells[1, 3] = "Vr";
                */

                if (!sp1.IsOpen)
                    sp1.Open();
            
                byte range = 0;
                switch (cmp_range)
                {
                    case "auto": range = 0; break;
                    case "0.4uA": range = 1; break;
                    case "4uA": range = 2; break;
                    case "40uA": range = 3; break;
                    case "0.4mA": range = 4; break;
                    case "4mA": range = 5; break;
                    case "40mA": range = 6; break;
                }

                if (row_of_technique_running < dataSet11.Tables["chartlist"].Rows.Count)
             //   for (int i = 0; i < dataSet11.Tables["chartlist"].Rows.Count; i++)
                {
                    // کد ارسال پارامتر 10 می باشد
                    string command = "@10@"
                        + (Convert.ToDouble(txt_cp1.Text) * 1000).ToString()
                        + "@" + txt_ct1.Text.Trim()
                        + "@" + (Convert.ToDouble(txt_cp2.Text) * 1000).ToString()
                        + "@" + txt_ct2.Text.Trim()
                        + "@" + (Convert.ToDouble(txt_cp3.Text) * 1000).ToString()
                        + "@" + txt_ct3.Text.Trim()
                        + "@" + range + "@";

                    set_to_chart(0, 0, 0, 0, "reset", 0);//پاک کردن گراف های قبلی چارت و گرید

                    string runing_technique_code = dataSet11.Tables["chartlist"].Rows[row_of_technique_running][1].ToString();

                    Array.Clear(x_value, 0, x_value.Length);//اگر از قبل مقدار دارد خالی شود
                    Array.Clear(y_value, 0, y_value.Length);
                    Array.Clear(z_value, 0, z_value.Length);
                    Array.Clear(w_value, 0, w_value.Length);

 //                   set_to_chart(0, 0, 0, row_of_technique_running, "setting", tech_id);//آماده کردن چارت برای تکنیک جاری

                    switch (runing_technique_code)
                    {
                        case "0"://cv
                            tech_id = 0;
                            command = command + row_of_technique_running.ToString() + "@";//شماره ردیف
                            command = command + "0@";//tech code
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][3]) * 1000).ToString() + "@";//e1  بر حسب میلی ولت
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][4]) * 1000).ToString() + "@";//e2  بر حسب میلی ولت
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][5]) * 1000).ToString() + "@";//e3  بر حسب میلی ولت
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][6]) * 1000).ToString() + "@";//hs  بر حسب میلی ولت
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][9]) * 1000).ToString() + "@";//ts  بر حسب میلی ولت
                            command = command + dataSet11.Tables["chartlist"].Rows[row_of_technique_running][14].ToString() + "@";//ht
                            command = command + dataSet11.Tables["chartlist"].Rows[row_of_technique_running][7].ToString() + "@";//qt
                            command = command + dataSet11.Tables["chartlist"].Rows[row_of_technique_running][13].ToString() + "@@";//cycle

                            //اینا زیاد چیز مهمی نیست (فقط برای مانیتورینگ استفاده شده) *درگیر نشو
                            cycle = Convert.ToInt16(dataSet11.chartlist[row_of_technique_running][13]);
                            e1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][3]);
                            e2 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][4]);
                            e3 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][5]);
                            hs = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][6]);
                            sum_step = Convert.ToInt32(((Math.Abs(e1) + Math.Abs(e2)) * 2) / hs) + 3;//برای بدست آوردن درصد پروسس بار
  //                          MessageBox.Show(Convert.ToString(e1));
                            //set_to_chart(0, 0, 0, row_of_technique_running, "cv_step_chart", tech_id, cycle, e1, e2, sum_step);//آماده کردن چارت برای تکنیک جاری
                   
   //                         for (Int16 cy = 1; cy <= cycle; cy++)gc
   //                         {
                                sp1.Write(command);
                                is_current_technique_run = true;
                                set_to_object(tech_id, cycle, "label_current_tech", row_of_technique_running);
                                set_to_chart(0, 0, 0, row_of_technique_running, "setting", tech_id);//آماده کردن چارت برای تکنیک جاری
                                set_to_chart(0, 0, 0, row_of_technique_running, "range", tech_id);//تنظیم رنج نمودار
                                ri = 0; vi = 0; vr = 0; jarian = 0; i = 0; counter_data = 0; con = 0; r1 = 0; r2 = 0;
                                j1 = 0;j2 = 0;j3 = 0;d1 = 0;d2 = 0;
                                thered_runing = new Thread(setData);
                                thered_runing.Start();

                                //setData();
                                //                         }
                            break;

                        case "1"://ocp

                            tech_id =1 ;
                            command = command + row_of_technique_running.ToString() + "@";//شماره ردیف
                            command = command + "1@";//tech code
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][15])).ToString() + "@";//t1  بر حسب ثانیه
                            command = command + "1@@";//cycle



                            cycle = 1;
                       
                            t1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][15]);
                            sp1.Write(command);
                                //sp1.Write("\r\n");//خط جدید و سر خط // برای میکرو به الزام اینطور تعریف شد
                            is_current_technique_run = true;
                            set_to_object(tech_id, cycle, "label_current_tech", row_of_technique_running);
                            set_to_chart(0, 0, 0, row_of_technique_running, "setting", tech_id);//آماده کردن چارت برای تکنیک جاری
                            set_to_chart(0, 0, 0, row_of_technique_running, "range", tech_id);//تنظیم رنج نمودار
                            ri = 0; vi = 0; vr = 0; jarian = 0; i = 0; counter_data = 0; con = 0; r1 = 0; r2 = 0;
                            j1 = 0; j2 = 0; j3 = 0; d1 = 0; d2 = 0;
                            thered_runing = new Thread(setData);
                            thered_runing.Start();

                            break;

                        case "3"://npv
                            tech_id = 3;
                            command = command + row_of_technique_running.ToString() + "@";//شماره ردیف
                            command = command + "3@";//tech code
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][3]) * 1000).ToString() + "@";//e1  بر حسب میلی ولت
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][4]) * 1000).ToString() + "@";//e2  بر حسب میلی ولت

                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][6]) * 1000).ToString() + "@";//hs  بر حسب میلی ولت
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][9]) * 1000).ToString() + "@";//ts  بر حسب میلی ثانیه

                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][10]) * 1000).ToString() + "@";//pw  بر حسب میلی ثانیه
                            command = command + dataSet11.Tables["chartlist"].Rows[row_of_technique_running][7].ToString() + "@";//qt
                            command = command + dataSet11.Tables["chartlist"].Rows[row_of_technique_running][13].ToString() + "@@";//cycle

                            cycle = 1;
                            e1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][3]);

                            e2 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][4]);

                            hs = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][6]);
                            sum_step = Convert.ToInt32(((Math.Abs(e1) + Math.Abs(e2)) * 2) / hs) + 3;//برای بدست آوردن درصد پروسس بار



                            //set_to_chart(0, 0, 0, row_of_technique_running, "cv_step_chart", tech_id, cycle, e1, e2, sum_step);//آماده کردن چارت برای تکنیک جاری

                            // for (Int16 cy = 1; cy <= cycle; cy++)
                            // {
                            sp1.Write(command);
                            //sp1.Write("\r\n");//خط جدید و سر خط // برای میکرو به الزام اینطور تعریف شد

                            is_current_technique_run = true;

                            set_to_object(tech_id, cycle, "label_current_tech", row_of_technique_running);
                            set_to_chart(0, 0, 0, row_of_technique_running, "setting", tech_id);//آماده کردن چارت برای تکنیک جاری
                            set_to_chart(0, 0, 0, row_of_technique_running, "range", tech_id);//تنظیم رنج نمودار
                            ri = 0; vi = 0; vr = 0; jarian = 0; i = 0; counter_data = 0; con = 0; r1 = 0; r2 = 0;
                            j1 = 0; j2 = 0; j3 = 0; d1 = 0; d2 = 0;
                            thered_runing = new Thread(setData);
                            thered_runing.Start();

                            break;

                        case "4"://dpv
                            tech_id = 4;
                            command = command + row_of_technique_running.ToString() + "@";//شماره ردیف
                            command = command + "4@";//tech code
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][3]) * 1000).ToString() + "@";//e1  بر حسب میلی ولت
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][4]) * 1000).ToString() + "@";//e2  بر حسب میلی ولت

                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][6]) * 1000).ToString() + "@";//hs  بر حسب میلی ولت
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][9]) * 1000).ToString() + "@";//ts  بر حسب میلی ثانیه

                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][10]) * 1000).ToString() + "@";//pw  بر حسب میلی ثانیه
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][8]) * 1000).ToString() + "@";//sr   بر حسب میلی ولت بر ثانیه
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][11]) * 1000).ToString() + "@";//ph  بر حسب میلی ولت

                            command = command + dataSet11.Tables["chartlist"].Rows[row_of_technique_running][7].ToString() + "@";//qt
                            command = command + dataSet11.Tables["chartlist"].Rows[row_of_technique_running][13].ToString() + "@@";//cycle

                            cycle = 1;
                            e1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][3]);

                            e2 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][4]);

                            hs = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][6]);
                            sum_step = Convert.ToInt32(((Math.Abs(e1) + Math.Abs(e2)) * 2) / hs) + 3;//برای بدست آوردن درصد پروسس بار



                            //set_to_chart(0, 0, 0, row_of_technique_running, "cv_step_chart", tech_id, cycle, e1, e2, sum_step);//آماده کردن چارت برای تکنیک جاری

                            sp1.Write(command);
                            //sp1.Write("\r\n");//خط جدید و سر خط // برای میکرو به الزام اینطور تعریف شد

                            is_current_technique_run = true;

                            set_to_object(tech_id, cycle, "label_current_tech", row_of_technique_running);
                            set_to_chart(0, 0, 0, row_of_technique_running, "setting", tech_id);//آماده کردن چارت برای تکنیک جاری
                            set_to_chart(0, 0, 0, row_of_technique_running, "range", tech_id);//تنظیم رنج نمودار
                            ri = 0; vi = 0; vr = 0; jarian = 0; i = 0; counter_data = 0; con = 0; r1 = 0; r2 = 0;
                            j1 = 0; j2 = 0; j3 = 0; d1 = 0; d2 = 0;
                            thered_runing = new Thread(setData);
                            thered_runing.Start();

                            break;

                        case "5"://swv
                            tech_id = 5;
                            command = command + row_of_technique_running.ToString() + "@";//شماره ردیف
                            command = command + "5@";//tech code
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][3]) * 1000).ToString() + "@";//e1  بر حسب میلی ولت
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][4]) * 1000).ToString() + "@";//e2  بر حسب میلی ولت
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][6]) * 1000).ToString() + "@";//hs  بر حسب میلی ولت


                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][12])).ToString() + "@";//fr  بر حسب هرتز
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][8]) * 1000).ToString() + "@";//sr   بر حسب میلی ولت بر ثانیه
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][11]) * 1000).ToString() + "@";//ph  بر حسب میلی ولت

                            command = command + dataSet11.Tables["chartlist"].Rows[row_of_technique_running][7].ToString() + "@";//qt
                            command = command + "1@@";//cycle
                            cycle = 1;

                            e1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][3]);
                            e2 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][4]);
                            hs = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][6]);
                            sum_step = Convert.ToInt32(((Math.Abs(e1) + Math.Abs(e2)) * 2) / hs) + 3;//برای بدست آوردن درصد پروسس بار

                               //   double fr = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][12]);
                               //   double sr = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][8]);
                              //    double ph = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][11]);
                              //    double qt = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][7]);


                           //  MessageBox.Show(Convert.ToString("fr"+fr+"sr"+sr+"ph"+ph+"qt"+qt));
                            //set_to_chart(0, 0, 0, row_of_technique_running, "cv_step_chart", tech_id, cycle, e1, e2, sum_step);//آماده کردن چارت برای تکنیک جاری
                            //     MessageBox.Show(Convert.ToString(hs));

                            sp1.Write(command);
                            //sp1.Write("\r\n");//خط جدید و سر خط // برای میکرو به الزام اینطور تعریف شد
                            is_current_technique_run = true;

                            set_to_object(tech_id, cycle, "label_current_tech", row_of_technique_running);

                            set_to_chart(0, 0, 0, row_of_technique_running, "setting", tech_id);//آماده کردن چارت برای تکنیک جاری

                            set_to_chart(0, 0, 0, row_of_technique_running, "range", tech_id);//تنظیم رنج نمودار
                            ri = 0; vi = 0; vr = 0; jarian = 0; i = 0; counter_data = 0; con = 0; r1 = 0; r2 = 0;
                            j1 = 0; j2 = 0; j3 = 0; d1 = 0; d2 = 0;
                            thered_runing = new Thread(setData);
                            thered_runing.Start();


                            break;


                        case "6"://lsv
                            tech_id = 6;
                            command = command + row_of_technique_running.ToString() + "@";//شماره ردیف
                            command = command + "6@";//tech code
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][3]) * 1000).ToString() + "@";//e1  بر حسب میلی ولت
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][4]) * 1000).ToString() + "@";//e2  بر حسب میلی ولت

                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][6]) * 1000).ToString() + "@";//hs  بر حسب میلی ولت
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][9]) * 1000).ToString() + "@";//ts  بر حسب میلی ولت

                            command = command + dataSet11.Tables["chartlist"].Rows[row_of_technique_running][7].ToString() + "@";//qt
                            command = command + dataSet11.Tables["chartlist"].Rows[row_of_technique_running][13].ToString() + "@@";//cycle

                            cycle = 1;
                            e1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][3]);
                            e2 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][4]);

                            hs = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][6]);
                            sum_step = Convert.ToInt32(((Math.Abs(e1) + Math.Abs(e2)) * 2) / hs) + 3;//برای بدست آوردن درصد پروسس بار

                            //set_to_chart(0, 0, 0, row_of_technique_running, "cv_step_chart", tech_id, cycle, e1, e2, sum_step);//آماده کردن چارت برای تکنیک جاری



                            sp1.Write(command);
                            // sp1.Write("\r\n");//خط جدید و سر خط // برای میکرو به الزام اینطور تعریف شد

                            is_current_technique_run = true;

                            set_to_object(tech_id, cycle, "label_current_tech", row_of_technique_running);
                            set_to_chart(0, 0, 0, row_of_technique_running, "setting", tech_id);//آماده کردن چارت برای تکنیک جاری
                            set_to_chart(0, 0, 0, row_of_technique_running, "range", tech_id);//تنظیم رنج نمودار
                            ri = 0; vi = 0; vr = 0; jarian = 0; i = 0; counter_data = 0; con = 0; r1 = 0; r2 = 0;
                            j1 = 0; j2 = 0; j3 = 0; d1 = 0; d2 = 0;
                            thered_runing = new Thread(setData);
                            thered_runing.Start();



                            break;


                        case "9"://cpc

                            tech_id = 9;
                            command = command + row_of_technique_running.ToString() + "@";//شماره ردیف
                            command = command + "9@";//tech code
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][3]) * 1000).ToString() + "@";//e1  بر حسب میلی ولت
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][15])).ToString() + "@";//t1  بر حسب ثانیه
                            command = command + "1@@";//cycle



                            cycle = 1;
                            e1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][3]);
                            t1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][15]);

                            //       MessageBox.Show(command);
                            //   sum_step = Convert.ToInt32(((Math.Abs(e1) + Math.Abs(e2)) * 2) / hs) + 3;//برای بدست آوردن درصد پروسس بار


                            //set_to_chart(0, 0, 0, row_of_technique_running, "cv_step_chart", tech_id, cycle, e1, e2, sum_step);//آماده کردن چارت برای تکنیک جاری


                            sp1.Write(command);
                            // sp1.Write("\r\n");//خط جدید و سر خط // برای میکرو به الزام اینطور تعریف شد
                            is_current_technique_run = true;
                            set_to_object(tech_id, cycle, "label_current_tech", row_of_technique_running);
                            set_to_chart(0, 0, 0, row_of_technique_running, "setting", tech_id);//آماده کردن چارت برای تکنیک جاری
                            set_to_chart(0, 0, 0, row_of_technique_running, "range", tech_id);//تنظیم رنج نمودار
                            ri = 0; vi = 0; vr = 0; jarian = 0; i = 0; counter_data = 0; con = 0; r1 = 0; r2 = 0;
                            j1 = 0; j2 = 0; j3 = 0; d1 = 0; d2 = 0;
                            thered_runing = new Thread(setData);
                            thered_runing.Start();
                            break;

                        case "11"://cha

                            tech_id = 11;
                            command = command + row_of_technique_running.ToString() + "@";//شماره ردیف
                            command = command + "11@";//tech code
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][3]) * 1000).ToString() + "@";//e1  بر حسب میلی ولت
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][4]) * 1000).ToString() + "@";//e2  بر حسب میلی ولت
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][15])).ToString() + "@";//t1  بر حسب ثانیه
                            command = command + (Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][16])).ToString() + "@";//t2  بر حسب ثانیه
                            command = command + dataSet11.Tables["chartlist"].Rows[row_of_technique_running][7].ToString() + "@";//qt
                            command = command + "1@@";//cycle


                            cycle = 1;
                            e1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][3]);
                            t1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][15]);
                            e2 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][4]);
                            double t2 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][16]);
                            double qt = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][7]);
                            //    MessageBox.Show("e1e2t1t2qt==" + e1 + " " + e2 + " " + t1 + " "+t2 + " " + qt);

                            //   sum_step = Convert.ToInt32(((Math.Abs(e1) + Math.Abs(e2)) * 2) / hs) + 3;//برای بدست آوردن درصد پروسس بار


                            //set_to_chart(0, 0, 0, row_of_technique_running, "cv_step_chart", tech_id, cycle, e1, e2, sum_step);//آماده کردن چارت برای تکنیک جاری
                            //          MessageBox.Show(Convert.ToString(e1));

                            sp1.Write(command);
//                                MessageBox.Show("e1e2t1t2qt==" + e1 + " " + e2 + " " + t1 + " "+t2 + " " + qt);

                            //     sp1.Write("\r\n");//خط جدید و سر خط // برای میکرو به الزام اینطور تعریف شد
                            is_current_technique_run = true;
                            set_to_object(tech_id, cycle, "label_current_tech", row_of_technique_running);
                            set_to_chart(0, 0, 0, row_of_technique_running, "setting", tech_id);//آماده کردن چارت برای تکنیک جاری
 //                         MessageBox.Show("e1e2t1t2qt==" + e1 + " " + e2 + " " + t1 + " " + t2 + " " + qt);

                            set_to_chart(0, 0, 0, row_of_technique_running, "range", tech_id);//تنظیم رنج نمودار
                            ri = 0; vi = 0; vr = 0;jarian = 0; i = 0; counter_data = 0; con = 0; r1 = 0; r2 = 0;
                            j1 = 0; j2 = 0; j3 = 0; d1 = 0; d2 = 0;
                            thered_runing = new Thread(setData);
                            thered_runing.Start();
                            break;

                }

                    //byte[] buf = System.Text.Encoding.UTF8.GetBytes(command);
                    //serialPort1.Write(buf, 0, buf.Length);
                }

               
            }
            catch (Exception msg)
            {
                MessageBox.Show("start_techniqu: " + msg.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

//        string[] data = { "" };
//        string StrRecieve;
/*
        private void sp1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                StrRecieve = sp1.ReadLine();
                this.Invoke(new EventHandler(DisplayText));
             //   MessageBox.Show(StrRecieve);


                //    byte[] buf = new byte[24];
                //    while (sp1.BytesToRead < 24) { }//در این مدل ارسالی پکت از سمت میکرو دیتا نامنظم دریافت میشوند
                //    sp1.Read(buf, 0, 24);
                //    string packet = System.Text.Encoding.UTF8.GetString(buf);
                //    setData(packet);
            }
            catch { }
        }
*/
     
  /*      private void DisplayText(object sender, EventArgs e)
        {
            string[] data_ = { "" };
            data_ = StrRecieve.Split('@');
            data = data_;
  //          MessageBox.Show("d1=" + data[1] + " d2=" + data[2] + " d3=" + data[3]);
   //         MessageBox.Show("d4="+ data[4] + " d5=" + data[5] + " d6=" + data[6]);
            switch (data[1])//نوع پروتکل  
            {
                case "30"://تایید اجرای دستور ارسالی
                    switch (data[2])//کدام پروتکل تایید شده
                    {
                        case "10"://تایید دریافت پارامترها

                            switch (data[3])//تکنیک
                            {
                                case "0"://cv
                                    cv_running(e1, e2, e3, hs, sum_step, cycle);
                                    break;

                                case "1"://cpc
                                    ocp_running(sum_step, cycle);
                                    break;

                                case "3"://npv
                                    npv_running(e1, e2, hs, sum_step, cycle, data);
                                    break;

                                case "4"://dpv
                                    npv_running(e1, e2, hs, sum_step, cycle, data);
                                    break;

                                case "5"://swv
                                    MessageBox.Show("5555");
                                    swv_running(e1, e2, hs, sum_step, cycle);
                                    break;


                                case "6"://lsv
                                    cv_running(e1, e2, e3, hs, sum_step, cycle);
                                    break;

                                case "9"://cpc
                                    cpc_running(e1, sum_step, cycle, data);
                                    break;

                            }
                            break;
                    }
                    break;
            }

                                    // MessageBox.Show(StrRecieve);
                                    // listBox2.Text+=StrRecieve;
                                    // MessageBox.Show(data_[0] + "  " + data_[1] + " " + data_[2] + " " + data_[3] + "  " + data_[4] + " " + data_[5]);
                                    // data = data_;

                       //             MessageBox.Show(data_[1] + "  " + data_[2] + " " + data_[3] + " " + data_[4] + "  " + data_[5] + " " + data_[6]);

         // MessageBox.Show("d0== "+ data[0] + "  d1==  " + data[1] + "  d2== " + data[2]);
         // MessageBox.Show("d3== "+ data[3] + "  d4==  " + data[4] + "  d5== " + data[5]);
         // setData();
        }
*/
        string[] get_data()
        {
            //MessageBox.Show(StrRecieve);
            string StrRecieve = "";
            string[] data_ = { "" };
            try
            {/*
                //byte[] readbytes = new byte[ccom.BytesToRead];
                //ccom.Read(readbytes, 0, readbytes.Length);
                   byte[] buf = new byte[sp1.BytesToRead];
    


                //    while (buf.Length==0){
                sp1.Read(buf, 0, buf.Length);
            //    }
                //    byte[] buf = new byte[22];
                //    while (sp1.BytesToRead < 5) { }//در این مدل ارسالی پکت از سمت میکرو دیتا نامنظم دریافت میشوند
                //    sp1.Read(buf, 0, 22);
                string packet = System.Text.Encoding.UTF8.GetString(buf);
               // MessageBox.Show(packet);
                string data_temp = packet.Substring(1, packet.Length - 2);//تابع قبلا از اتساین اول و بعد از آخر را هم در نظر میگرفت بنابراین پکت خالی در می اومد
                data_ = data_temp.Split('@');
                //listBox2.Items.Add(packet);
                //setData(packet);
                */
                StrRecieve = sp1.ReadLine();
                data_ = StrRecieve.Split('@');


            }
            catch { }
              //   MessageBox.Show(data_[0] + "  " + data_[1] + " " + data_[2] + " " + data_[3] + "  " + data_[4] + " " + data_[5]);
              //   MessageBox.Show(data_[0] + "  " + data_[1] );

            return data_;
        }

        //string[] data;//تمام پکت های دریافتی درون این آرایه قرار میگیرد

        //delegate void SetdataCallback();
        private void setData()
        {
            //if (this.InvokeRequired)
            //{
            //    SetdataCallback d = new SetdataCallback(setData);
            //    this.Invoke(d, new object[] {  });
            //}
            //else
            //{
            try
            {

                //listBox2.Items.Add(packet);
                //string data_temp = packet.Substring(1, packet.Length - 2);//تابع قبلا از اتساین اول و بعد از آخر را هم در نظر میگرفت بنابراین پکت خالی در می اومد
                //data = data_temp.Split('@');

                ////for (int i = 0; i < data.Length; i++)
                ////        listBox1.Items.Add(data[i]);
                //listBox1.Items.Add("======");

                for (Int16 cy = 1; cy <= cycle; cy++)
                {
                    is_current_technique_run = true;
                    while (is_current_technique_run == true)
                    {
                        if (is_stop == true)
                        {
                            sp1.WriteLine("@30@15@@");
                        }
                        else
                            sp1.WriteLine("sta");


                        string[] data = { "" };
                        data = get_data();

                        //string s = "";
                        //for (int j = 0; j < data.Length; j++)
                        //    s = s + data[j];
                        //listBox1.Items.Add(s);


                        //       MessageBox.Show(data[1] + "  " + data[2] + " " + data[3]);
                        switch (data[1])//نوع پروتکل  
                        {
                            case "30"://تایید اجرای دستور ارسالی
                                switch (data[2])//کدام پروتکل تایید شده
                                {
                                    case "10"://تایید دریافت پارامترها
                                        switch (data[3])//تکنیک
                                        {
                                            case "0"://cv
                                                cv_running(e1, e2, e3, hs, sum_step, cycle, data);
                                                break;

                                            case "1"://ocp
                                                ocp_running(sum_step, cycle, data);
                                                break;

                                            case "3"://npv
                                                npv_running(e1, e2, hs, sum_step, cycle, data);
                                                break;

                                            case "4"://dpv
                                                dpv_running(e1, e2, hs, sum_step, cycle, data);
                                                break;

                                            case "5"://swv
                                                swv_running(e1, e2, hs, sum_step, cycle, data);
                                                break;

                                            case "6"://lsv
                                                cv_running(e1, e2, e3, hs, sum_step, cycle, data);
                                                break;

                                            case "9"://cpc
                                                cpc_running(e1, sum_step, cycle, data);
                                                break;

                                            case "11"://cha
                                                      //    MessageBox.Show("11");
                                                cha_running(e1, sum_step, cycle, data);
                                                break;

                                        }//سوییچ نوع تکنیک
                                        break;

                                    case "15"://اتمام اجرای تکنیک
                                        is_current_technique_run = false;
                                        //        reset_var();
                                        break;
                                }//تایید اجرای دستور ارسالی
                                break;

                            case "40"://رد کردن اجرای دستور ارسالی
                                is_current_technique_run = false;
                                switch (data[1])//کدام پروتکل رد شده
                                {
                                    case "10":// عدم تایید ارسال پارامترها
                                        MessageBox.Show("Parameters failed to send successfully!", "Send Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        break;

                                    case "11":// عدم تایید اجرای دستور PAUSE
                                        MessageBox.Show("You can't pause technique!", "Pause", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        break;

                                    case "13"://عدم تایید توقف تکنیک جاری برای شروع تکنیک جدید
                                        MessageBox.Show("You Can't stop this technique to run a new technique!", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        break;

                                    case "14":// عدم تایید اجرای دستور stop
                                        MessageBox.Show("You can't stop technique!", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        break;

                                    case "15":// عدم تایید دستور اتمام اجرای پروژه
                                        MessageBox.Show("You can not finish the project!", "Finish", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        break;

                                    case "16"://عدم تایید دستور اتمام اجرای تکنیک
                                        MessageBox.Show("You can not finish the technique!", "Finish", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        break;

                                    default:
                                        MessageBox.Show("protocol:" + data[0] + "    under protocol:" + data[1] + "    technic:" + data[2]);
                                        break;
                                }
                                break;

                            case "50"://خطا در هنگام اجرای دستور ارسالی
                                is_current_technique_run = false;
                                MessageBox.Show("There was an error running the device!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;

                        }//سوییچ نوع پروتکل
                    }

                    if (is_current_technique_run == false)
                    {
                        is_stop = false;
                        is_run = false;
                            
                        switch (dataSet11.Tables["chartlist"].Rows[row_of_technique_running][1].ToString())
                        {
                            case "0":
                                save_Data("CV", 0);
                                break;
                            case "1":
                                save_Data("OCP", 1);
                                break;
                            case "3":
                                save_Data("NPV", 3);
                                break;
                            case "4":
                                save_Data("DPV", 4);
                                break;
                            case "5":
                                save_Data("SWV", 5);
                                break;
                            case "6":
                                save_Data("LSV", 6);
                                break;
                            case "9":
                                save_Data("CPC", 9);
                                break;
                            case "11":
                                save_Data("CHA", 11);
                                break;
                        }
                    
                        MessageBox.Show("The chart End");
                        ///              row_of_technique_running++;//شماره ردیف لیست تکنیک جدید در صورت وجود
                        ///              start_techniqu();//فراخوانی تابع اجرا کننده لیست تکنیک ها در صورت وجود تکنیک دیگر

                    }

                    //Array.Clear(data, 0, data.Length);//خالی کردن دیتا جهت کنترل گره اجرا
                }
         //       xlWorkBook.SaveAs("d:\\csharp-Excel.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
         //       xlWorkBook.Close(true, misValue, misValue);
         //       xlApp.Quit();
         //       MessageBox.Show("Excel file created , you can find the file d:\\csharp-Excel.xls");

            }
            catch (Exception err)
            {
                //              MessageBox.Show("setData: "+err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        double[] x_value = new double[0];//ستون اول دیتا موجود در فایل 
        double[] y_value = new double[0];//ستون دوم دیتا موجود در فایل 
        double[] z_value = new double[0];//ستون سوم دیتا موجود در فایل 
        double[] w_value = new double[0];//ستون چهارم دیتا موجود در فایل 
        int counter_data = 0;//شماره اندیس آرایه نگهدارنده دیتا دریافتی جهت ذخیره در فایل
        int i = 0;
        bool is_current_technique_run = false;//در مود اجرا بررسی میکند که اگر تکنیک هنوز اجرا است دیتای پورت را چک کند در غیر اینصورت گره را اتمام دهد
        double[] jarianArray;
        Int16 r1 = 0, r2 = 0;
        int con = 0;
        Int16 ri = 0;double vi = 0, vr = 0, jarian=0;
        double j1 = 0,j2 = 0,j3 = 0,d1 = 0,d2 = 0;

        void cv_running(double e1, double e2, double e3, double hs, int sum_step, Int16 cycle, string[] data)
        {
            try
            {
                tech_id = 0;//cv techniqe
                set_to_object(tech_id, cycle, "proccess", cycle, 0, sum_step);//نمایش پروسس بار
 
                ri = Convert.ToInt16(data[4]);//اندیس مقاومت
                vi = Convert.ToDouble(data[5]);
                vr = Convert.ToDouble(data[6]);
           //     MessageBox.Show("r= "+ri+" vi= "+vi+" vr= "+vr);
                vi = vi / 1000;
                vr = vr / 1000;
                jarian = vr / Ri[ri];


//                if (con == 0) { r1 = ri; r2 = ri;}
//                else { r1 = r2; r2 = ri;}

//                if (con==0 || con == 1) { j1 = jarian; j2 = jarian; j3 = jarian; }
//                else { j1 = j2; j2 = j3;j3 = jarian; }
//                d1 = j2 - j1;if (d1 < 0) d1 *=-1;
//                d2 = j3 - j2; if (d2 < 0) d2 *= -1;
         //       if (d2 - d1 > .0001) { jarian = j2 + .0001; j3 = j2 + .0001; }
 
//                    if (r1 == r2)
//                    {
                        set_to_chart(vi, vr, jarian, row_of_technique_running, "data", tech_id, cycle);//اصلی
                        set(vi, vr, Ri[ri]);

                        /////save in array and file
                        Array.Resize(ref x_value, counter_data + 1);
                        Array.Resize(ref y_value, counter_data + 1);
                        Array.Resize(ref z_value, counter_data + 1);

                        x_value[counter_data] = vi;
                        y_value[counter_data] = jarian;
                        z_value[counter_data] = jarian;

               //         xlWorkSheet.Cells[counter_data + 2, 1] = vi;
               //         xlWorkSheet.Cells[counter_data + 2, 2] = jarian;
               //         xlWorkSheet.Cells[counter_data + 2, 3] = vr;


                        counter_data++;i++;

                        set_to_object(tech_id, cycle, "proccess", row_of_technique_running, i + 2, sum_step);//نمایش پروسس بار
               //         class_save_to_file.save_data_to_file_runtime(tech_id, x_value, y_value, z_value, w_value, dataSet11.chartlist[row_of_technique_running], cycle);//ذخیره تکنیک اجرا شده در فایل 
                        set_to_object(tech_id, cycle, "state", row_of_technique_running);//نمایش پایان در وضعیت
                        set_to_object(tech_id, cycle, "end", row_of_technique_running);//فعال کردن دکمه شروع و غیرفعال کردن مابقیع
                        set_to_object(tech_id, cycle, "proccess", row_of_technique_running, 0, sum_step);//نمایش پروسس بار
//                    }
//                    else
//                    {
                    //  MessageBox.Show(Convert.ToString("There is a spike"));
//                    }
 
                con++;
            }
            catch (Exception msg)
            {
//                MessageBox.Show("runing_technique: " + msg.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ocp_running(int sum_step, Int16 cycle, string[] data)
        {
            try
            {
                tech_id = 1;//cpc techniqe
                set_to_object(tech_id, cycle, "proccess", cycle, 0, sum_step);//نمایش پروسس بار

                ri = Convert.ToInt16(data[4]);//اندیس مقاومت
                vi = Convert.ToDouble(data[5]);
                vr = Convert.ToDouble(data[6]);
 
                vi = vi / 1000;
                vr = vr / 1000;
                jarian = vr / Ri[ri];

//                if (con == 0) { r1 = ri; r2 = ri; }
//                else { r1 = r2; r2 = ri; }

//                if (con == 0 || con == 1) { j1 = jarian; j2 = jarian; j3 = jarian; }
//                else { j1 = j2; j2 = j3; j3 = jarian; }
//                d1 = j2 - j1; if (d1 < 0) d1 *= -1;
//                d2 = j3 - j2; if (d2 < 0) d2 *= -1;
   //             if (d2 - d1 > .000001) { jarian = j2 + .000001; j3 = j2 + .000001; }



//                if (r1 == r2)
//                    {
                        set_to_chart(vi, vr, jarian, row_of_technique_running, "data", tech_id, cycle);//اصلی
                        set(vi, vr, Ri[ri]);

                        /////save in array and file
                        Array.Resize(ref x_value, counter_data + 1);
                        Array.Resize(ref y_value, counter_data + 1);
                        Array.Resize(ref z_value, counter_data + 1);

                        x_value[counter_data] = vi;
                        y_value[counter_data] = jarian;
                        z_value[counter_data] = jarian;

//                        xlWorkSheet.Cells[counter_data + 2, 1] = vi;
//                        xlWorkSheet.Cells[counter_data + 2, 2] = jarian;
//                        xlWorkSheet.Cells[counter_data + 2, 3] = vr;


                        counter_data++; i++;

                        set_to_object(tech_id, cycle, "proccess", row_of_technique_running, i + 2, sum_step);//نمایش پروسس بار
                 //       class_save_to_file.save_data_to_file_runtime(tech_id, x_value, y_value, z_value, w_value, dataSet11.chartlist[row_of_technique_running], cycle);//ذخیره تکنیک اجرا شده در فایل 
                        set_to_object(tech_id, cycle, "state", row_of_technique_running);//نمایش پایان در وضعیت
                        set_to_object(tech_id, cycle, "end", row_of_technique_running);//فعال کردن دکمه شروع و غیرفعال کردن مابقیع
                        set_to_object(tech_id, cycle, "proccess", row_of_technique_running, 0, sum_step);//نمایش پروسس بار
//                    }
//                    else
//                    {
                     //   MessageBox.Show(Convert.ToString("There is a spike"));
//                    }
                con++;
            }
            catch (Exception msg)
            {
                //  MessageBox.Show("runing_technique: " + msg.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void npv_running(double e1, double e2, double hs, int sum_step, Int16 cycle, string[] data)
        {
          try
            {
                int tech_id = 3;//npv techniqe
                set_to_object(tech_id, cycle, "proccess", cycle, 0, sum_step);//نمایش پروسس بار

                ri = Convert.ToInt16(data[4]);//اندیس مقاومت
                vi = Convert.ToDouble(data[5]);
                vr = Convert.ToDouble(data[6]);

                vi = vi / 1000;
                vr = vr / 1000;
                jarian = vr / Ri[ri];

//                if (con == 0) { r1 = 
//                        ri; r2 = ri; }
//                else { r1 = r2; r2 = ri; }

//                if (con == 0 || con == 1) { j1 = jarian; j2 = jarian; j3 = jarian; }
//                else { j1 = j2; j2 = j3; j3 = jarian; }
//                d1 = j2 - j1; if (d1 < 0) d1 *= -1;
//                d2 = j3 - j2; if (d2 < 0) d2 *= -1;
    //            if (d2 - d1 > .000001) { jarian = j2 + .000001; j3 = j2 + .000001; }

//                if (r1 == r2)
//                    {
                        set_to_chart(vi, vr, jarian, row_of_technique_running, "data", tech_id, cycle);//اصلی
                        set(vi, vr, Ri[ri]);

                        /////save in array and file
                        Array.Resize(ref x_value, counter_data + 1);
                        Array.Resize(ref y_value, counter_data + 1);
                        Array.Resize(ref z_value, counter_data + 1);

                        x_value[counter_data] = vi;
                        y_value[counter_data] = jarian;
                        z_value[counter_data] = jarian;

//                        xlWorkSheet.Cells[counter_data + 2, 1] = vi;
//                        xlWorkSheet.Cells[counter_data + 2, 2] = jarian;
//                        xlWorkSheet.Cells[counter_data + 2, 3] = vr;


                        counter_data++; i++;

                        set_to_object(tech_id, cycle, "proccess", row_of_technique_running, i + 2, sum_step);//نمایش پروسس بار
                   //     class_save_to_file.save_data_to_file_runtime(tech_id, x_value, y_value, z_value, w_value, dataSet11.chartlist[row_of_technique_running], cycle);//ذخیره تکنیک اجرا شده در فایل 
                        set_to_object(tech_id, cycle, "state", row_of_technique_running);//نمایش پایان در وضعیت
                        set_to_object(tech_id, cycle, "end", row_of_technique_running);//فعال کردن دکمه شروع و غیرفعال کردن مابقیع
                        set_to_object(tech_id, cycle, "proccess", row_of_technique_running, 0, sum_step);//نمایش پروسس بار
//                    }
//                    else
//                    {
                    //    MessageBox.Show(Convert.ToString("There is a spike"));
//                    }
 
                con++;
            }
            catch (Exception msg)
            {
                //  MessageBox.Show("runing_technique: " + msg.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void dpv_running(double e1, double e2, double hs, int sum_step, Int16 cycle, string[] data)
        {
            try
            {
                int tech_id = 4;//npv techniqe
                set_to_object(tech_id, cycle, "proccess", cycle, 0, sum_step);//نمایش پروسس بار

                ri = Convert.ToInt16(data[4]);//اندیس مقاومت
                vi = Convert.ToDouble(data[5]);
                vr = Convert.ToDouble(data[6]);

                vi = vi / 1000;
                vr = vr / 1000;
                jarian = vr / Ri[ri];

//                if (con == 0) { r1 = ri; r2 = ri; }
//                else { r1 = r2; r2 = ri; }

//                if (con == 0 || con == 1) { j1 = jarian; j2 = jarian; j3 = jarian; }
//                else { j1 = j2; j2 = j3; j3 = jarian; }
//                d1 = j2 - j1; if (d1 < 0) d1 *= -1;
//                d2 = j3 - j2; if (d2 < 0) d2 *= -1;
    //            if (d2 - d1 > .000001) { jarian = j2 + .000001; j3 = j2 + .000001; }

//                if (r1 == r2)
//                    {
                        set_to_chart(vi, vr, jarian, row_of_technique_running, "data", tech_id, cycle);//اصلی
                        set(vi, vr, Ri[ri]);

                        /////save in array and file
                        Array.Resize(ref x_value, counter_data + 1);
                        Array.Resize(ref y_value, counter_data + 1);
                        Array.Resize(ref z_value, counter_data + 1);

                        x_value[counter_data] = vi;
                        y_value[counter_data] = jarian;
                        z_value[counter_data] = jarian;

//                        xlWorkSheet.Cells[counter_data + 2, 1] = vi;
//                        xlWorkSheet.Cells[counter_data + 2, 2] = jarian;
//                        xlWorkSheet.Cells[counter_data + 2, 3] = vr;


                        counter_data++; i++;

                        set_to_object(tech_id, cycle, "proccess", row_of_technique_running, i + 2, sum_step);//نمایش پروسس بار
                   //     class_save_to_file.save_data_to_file_runtime(tech_id, x_value, y_value, z_value, w_value, dataSet11.chartlist[row_of_technique_running], cycle);//ذخیره تکنیک اجرا شده در فایل 
                        set_to_object(tech_id, cycle, "state", row_of_technique_running);//نمایش پایان در وضعیت
                        set_to_object(tech_id, cycle, "end", row_of_technique_running);//فعال کردن دکمه شروع و غیرفعال کردن مابقیع
                        set_to_object(tech_id, cycle, "proccess", row_of_technique_running, 0, sum_step);//نمایش پروسس بار
//                    }
//                    else
//                    {
                    //    MessageBox.Show(Convert.ToString("There is a spike"));
//                    }
 
                con++;
            }
            catch (Exception msg)
            {
                //  MessageBox.Show("runing_technique: " + msg.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void swv_running(double e1, double e2, double hs, int sum_step, Int16 cycle, string[] data)
        {
            try
            {
                int tech_id = 5;//swv techniqe

                set_to_object(tech_id, cycle, "proccess", cycle, 0, sum_step);//نمایش پروسس بار

                ri = Convert.ToInt16(data[4]);//اندیس مقاومت
                vi = Convert.ToDouble(data[5]);
                vr = Convert.ToDouble(data[6]);

                vi = vi / 1000;
                vr = vr / 1000;
                jarian = vr / Ri[ri];

//                if (con == 0) { r1 = ri; r2 = ri; }
//                else { r1 = r2; r2 = ri; }

//                if (con == 0 || con == 1) { j1 = jarian; j2 = jarian; j3 = jarian; }
//                else { j1 = j2; j2 = j3; j3 = jarian; }
//                d1 = j2 - j1; if (d1 < 0) d1 *= -1;
//                d2 = j3 - j2; if (d2 < 0) d2 *= -1;
      //          if (d2 - d1 > .000001) { jarian = j2 + .000001; j3 = j2 + .000001;
       //             MessageBox.Show(Convert.ToString(d2-d1));
       //         }


//                if (r1 == r2)
//                    {
                        set_to_chart(vi, vr, jarian, row_of_technique_running, "data", tech_id, cycle);//اصلی
                        set(vi, vr, Ri[ri]);

                        /////save in array and file
                        Array.Resize(ref x_value, counter_data + 1);
                        Array.Resize(ref y_value, counter_data + 1);
                        Array.Resize(ref z_value, counter_data + 1);

                        x_value[counter_data] = vi;
                        y_value[counter_data] = jarian;
                        z_value[counter_data] = jarian;

//                        xlWorkSheet.Cells[counter_data + 2, 1] = vi;
//                        xlWorkSheet.Cells[counter_data + 2, 2] = jarian;
//                        xlWorkSheet.Cells[counter_data + 2, 3] = vr;


                        counter_data++; i++;

                        set_to_object(tech_id, cycle, "proccess", row_of_technique_running, i + 2, sum_step);//نمایش پروسس بار
                   //     class_save_to_file.save_data_to_file_runtime(tech_id, x_value, y_value, z_value, w_value, dataSet11.chartlist[row_of_technique_running], cycle);//ذخیره تکنیک اجرا شده در فایل 
                        set_to_object(tech_id, cycle, "state", row_of_technique_running);//نمایش پایان در وضعیت
                        set_to_object(tech_id, cycle, "end", row_of_technique_running);//فعال کردن دکمه شروع و غیرفعال کردن مابقیع
                        set_to_object(tech_id, cycle, "proccess", row_of_technique_running, 0, sum_step);//نمایش پروسس بار
//                    }
//                    else
//                    {
                    //    MessageBox.Show(Convert.ToString("There is a spike"));
//                    }

                con++;
            }
            catch (Exception msg)
            {
                //  MessageBox.Show("runing_technique: " + msg.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void cpc_running(double e1, int sum_step, Int16 cycle, string[] data)
        {
            // for (int jj = 0; jj < 3; jj++)
            //{
            //   jarianArray[jj] = 0;
            // }

            try
            {
                tech_id = 9;//cpc techniqe
                set_to_object(tech_id, cycle, "proccess", cycle, 0, sum_step);//نمایش پروسس بار

                ri = Convert.ToInt16(data[4]);//اندیس مقاومت
                vi = Convert.ToDouble(data[5]);
                vr = Convert.ToDouble(data[6]);
 
                //    vi = vi / 1000;
                vr = vr / 1000;
                jarian = vr / Ri[ri];

//                if (con == 0) { r1 = ri; r2 = ri; }
//                else { r1 = r2; r2 = ri; }

//                if (con == 0 || con == 1) {j1 = jarian; j2 = jarian; j3 = jarian; }
//                else { j1 = j2; j2 = j3; j3 = jarian; }
//                d1 = j2 - j1; if (d1 < 0) d1 *= -1;
//                d2 = j3 - j2; if (d2 < 0) d2 *= -1;
      //          if (d2 - d1 > .000001) { jarian = j2 + .000001; j3 = j2 + .000001; }


//                if (r1 == r2)
//                    {
                        set_to_chart(vi, vr, jarian, row_of_technique_running, "data", tech_id, cycle);//اصلی
                        set(vi, vr, Ri[ri]);

                        /////save in array and file
                        Array.Resize(ref x_value, counter_data + 1);
                        Array.Resize(ref y_value, counter_data + 1);
                        Array.Resize(ref z_value, counter_data + 1);

                        x_value[counter_data] = vi;
                        y_value[counter_data] = jarian;
                        z_value[counter_data] = jarian;

//                        xlWorkSheet.Cells[counter_data + 2, 1] = vi;
//                        xlWorkSheet.Cells[counter_data + 2, 2] = jarian;
//                        xlWorkSheet.Cells[counter_data + 2, 3] = vr;


                        counter_data++; i++;

                        set_to_object(tech_id, cycle, "proccess", row_of_technique_running, i + 2, sum_step);//نمایش پروسس بار
                   //     class_save_to_file.save_data_to_file_runtime(tech_id, x_value, y_value, z_value, w_value, dataSet11.chartlist[row_of_technique_running], cycle);//ذخیره تکنیک اجرا شده در فایل 
                        set_to_object(tech_id, cycle, "state", row_of_technique_running);//نمایش پایان در وضعیت
                        set_to_object(tech_id, cycle, "end", row_of_technique_running);//فعال کردن دکمه شروع و غیرفعال کردن مابقیع
                        set_to_object(tech_id, cycle, "proccess", row_of_technique_running, 0, sum_step);//نمایش پروسس بار
//                    }
//                    else
//                    {
                    //    MessageBox.Show(Convert.ToString("There is a spike"));
//                    }
 
                con++;
            }
            catch (Exception msg)
            {
                //  MessageBox.Show("runing_technique: " + msg.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void cha_running(double e1, int sum_step, Int16 cycle, string[] data)
        {
            // for (int jj = 0; jj < 3; jj++)
            //{
            //   jarianArray[jj] = 0;
            // }

            try
            {
                tech_id = 11;//cha techniqe
                set_to_object(tech_id, cycle, "proccess", cycle, 0, sum_step);//نمایش پروسس بار

                ri = Convert.ToInt16(data[4]);//اندیس مقاومت
                vi = Convert.ToDouble(data[5]);
                vr = Convert.ToDouble(data[6]);
 
                //    vi = vi / 1000;
                vr = vr / 1000;
                jarian = vr / Ri[ri];

//                if (con == 0) { r1 = ri; r2 = ri; }
//                else { r1 = r2; r2 = ri; }

//                if (con == 0 || con == 1) { j1 = jarian; j2 = jarian; j3 = jarian; }
//                else { j1 = j2; j2 = j3; j3 = jarian; }
//                d1 = j2 - j1; if (d1 < 0) d1 *= -1;
//                d2 = j3 - j2; if (d2 < 0) d2 *= -1;
     //           if (d2 - d1 > .000001) { jarian = j2 + .000001; j3 = j2 + .000001; }


//                if (r1 == r2)
//                    {
                        set_to_chart(vi, vr, jarian, row_of_technique_running, "data", tech_id, cycle);//اصلی
                        set(vi, vr, Ri[ri]);

                        /////save in array and file
                        Array.Resize(ref x_value, counter_data + 1);
                        Array.Resize(ref y_value, counter_data + 1);
                        Array.Resize(ref z_value, counter_data + 1);

                        x_value[counter_data] = vi;
                        y_value[counter_data] = jarian;
                        z_value[counter_data] = jarian;

//                        xlWorkSheet.Cells[counter_data + 2, 1] = vi;
//                        xlWorkSheet.Cells[counter_data + 2, 2] = jarian;
//                        xlWorkSheet.Cells[counter_data + 2, 3] = vr;


                        counter_data++; i++;

                        set_to_object(tech_id, cycle, "proccess", row_of_technique_running, i + 2, sum_step);//نمایش پروسس بار
                   //     class_save_to_file.save_data_to_file_runtime(tech_id, x_value, y_value, z_value, w_value, dataSet11.chartlist[row_of_technique_running], cycle);//ذخیره تکنیک اجرا شده در فایل 
                        set_to_object(tech_id, cycle, "state", row_of_technique_running);//نمایش پایان در وضعیت
                        set_to_object(tech_id, cycle, "end", row_of_technique_running);//فعال کردن دکمه شروع و غیرفعال کردن مابقیع
                        set_to_object(tech_id, cycle, "proccess", row_of_technique_running, 0, sum_step);//نمایش پروسس بار
//                    }
//                    else
//                    {
                    //    MessageBox.Show(Convert.ToString("There is a spike"));
//                    }
 
                con++;
            }
            catch (Exception msg)
            {
                //  MessageBox.Show("runing_technique: " + msg.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        delegate void set_to_chart_(double vi, double vr, double jarian, int row, string state, int tech_id, Int16 cycle = 1);
        void set_to_chart(double vi, double vr, double jarian, int row, string state, int tech_id, Int16 cycle = 1)
        {
            if (this.InvokeRequired)
            {
                set_to_chart_ d = new set_to_chart_(set_to_chart);
                this.Invoke(d, new object[] { vi, vr, jarian, row, state, tech_id, cycle});
            }
            else
            {
                switch (state)
                {
                    case "reset":
                        chart1.Series.Clear();
                        chart1.Titles.Clear();
                        chart1.ChartAreas.Clear();
                        grd_show_data.Rows.Clear();
                        break;

                    case "clear":
                        chart1.Series[row].Points.Clear();
                        break;

                    case "setting":
                        Random Random1 = new Random();
                        int Random_Color_Index = 0;
                        Random_Color_Index = (int)(Random1.NextDouble() * 1);

                        //if (chart1.Series.Count == 0)
                        chart1.Series.Add(row.ToString() + "_" + tech_id.ToString() + "_" + cycle);

                        chart1.Series[row].Color = classglobal.color_Graph[Random_Color_Index];

                        chart1.Titles.Add(classglobal.TechName[tech_id]);

                        //chart1.ChartAreas.Clear();
                        chart1.ChartAreas.Add(row.ToString());

                        chart1.ChartAreas[0].AxisY.Title = classglobal.VAxisTitle[tech_id];
                        chart1.ChartAreas[0].AxisX.Title = classglobal.HAxisTitle[tech_id];

                        chart1.Series[row].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

                        chart1.ChartAreas[0].Area3DStyle.Enable3D = false;
                        chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
                        chart1.ChartAreas[0].AxisX.LineColor = Color.Black;
                        chart1.ChartAreas[0].AxisY.LineColor = Color.Black;
                        chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
                        chart1.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
                        chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
                        //chart1.ChartAreas[0].AxisY.LabelStyle.Format = "#.#e-0";
                        //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "#0.##";
                        //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "0.00";
                        chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 1;
                        chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
                        chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                        chart1.ChartAreas[0].AxisX.MinorGrid.LineColor = Color.Black;
                        chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                        chart1.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.Black;
                        chart1.ChartAreas[0].AxisX.ScrollBar.BackColor = Color.Black;
                        chart1.ChartAreas[0].AxisY.ScrollBar.BackColor = Color.Black;
                        chart1.Series[0].IsVisibleInLegend = false;//عدم نمایش رنگ راهنما
                        break;

                    case "range":
                        double e1;
                        double e2;
                        double t1;
                        switch (tech_id)
                        {
                            case 0: 
                            e1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row][3]);
                            e2 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row][4]);
                            //double hs = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row_of_technique_running][6]);
                                          
                            if (e1 <= e2)
                            {
                                chart1.ChartAreas[0].AxisX.Maximum = (double)e2;//به صورت لحظه ای مقادیر محور ها تغییر می کند
                                chart1.ChartAreas[0].AxisX.Minimum = (double)e1;

                                //StripLine stripLine_x = new StripLine();//نمایش گرید به صورت دلخواه
                                //stripLine_x.StripWidth = 0;
                                //stripLine_x.BorderColor = Color.LightGray;// نمایش خط های پس زمینه
                                //stripLine_x.BorderWidth = 1;
                                ////stripLine_x.Interval = hs;
                                //chart1.ChartAreas[0].AxisX.StripLines.Add(stripLine_x);
                            }
                            else
                            {
                                chart1.ChartAreas[0].AxisX.Maximum =(double)e1;//به صورت لحظه ای مقادیر محور ها تغییر می کند
                                chart1.ChartAreas[0].AxisX.Minimum = (double)e2;
                            }
                          break;
                            case 1:
                                t1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row][15]);
                                chart1.ChartAreas[0].AxisX.Maximum = (double)t1;
                                chart1.ChartAreas[0].AxisX.Minimum = (double)0;
                                break;

                            case 3: 
                              e1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row][3]);
                              e2 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row][4]);
                              if (e1 <= e2)
                              {
                                  chart1.ChartAreas[0].AxisX.Maximum = (double)e2;//به صورت لحظه ای مقادیر محور ها تغییر می کند
                                  chart1.ChartAreas[0].AxisX.Minimum = (double)e1;
                              }
                              else
                              {
                                  chart1.ChartAreas[0].AxisX.Maximum = (double)e1;//به صورت لحظه ای مقادیر محور ها تغییر می کند
                                  chart1.ChartAreas[0].AxisX.Minimum = (double)e2;
                              }
                          break;

                          case 4: 
                              e1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row][3]);
                              e2 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row][4]);
                              if (e1 <= e2)
                              {
                                  chart1.ChartAreas[0].AxisX.Maximum = (double)e2;//به صورت لحظه ای مقادیر محور ها تغییر می کند
                                  chart1.ChartAreas[0].AxisX.Minimum = (double)e1;
                              }
                              else
                              {
                                  chart1.ChartAreas[0].AxisX.Maximum = (double)e1;//به صورت لحظه ای مقادیر محور ها تغییر می کند
                                  chart1.ChartAreas[0].AxisX.Minimum = (double)e2;
                              }
                              break;

                            case 5:
                                e1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row][3]);
                                e2 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row][4]);
                                if (e1 <= e2)
                                {
                                    chart1.ChartAreas[0].AxisX.Maximum = (double)e2;//به صورت لحظه ای مقادیر محور ها تغییر می کند
                                    chart1.ChartAreas[0].AxisX.Minimum = (double)e1;
                                }
                                else
                                {
                                    chart1.ChartAreas[0].AxisX.Maximum = (double)e1;//به صورت لحظه ای مقادیر محور ها تغییر می کند
                                    chart1.ChartAreas[0].AxisX.Minimum = (double)e2;
                                }
                                break;

                            case 6: 
                                e1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row][3]);
                                e2 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row][4]);
                                if (e1 <= e2)
                                {
                                    chart1.ChartAreas[0].AxisX.Maximum = (double)e2;//به صورت لحظه ای مقادیر محور ها تغییر می کند
                                    chart1.ChartAreas[0].AxisX.Minimum = (double)e1;
                                }
                                else
                                {
                                    chart1.ChartAreas[0].AxisX.Maximum = (double)e1;//به صورت لحظه ای مقادیر محور ها تغییر می کند
                                    chart1.ChartAreas[0].AxisX.Minimum = (double)e2;
                                }
                                break;
 
                          case 9:
                             e1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row][3]);
                             t1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row][15]);
                        //      MessageBox.Show("t1====",Convert.ToString(t1));
                             chart1.ChartAreas[0].AxisX.Maximum = (double)t1;
                             chart1.ChartAreas[0].AxisX.Minimum = (double)0;
                             break;

                          case 11:
                                e1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row][3]);
                                t1 = Convert.ToDouble(dataSet11.Tables["chartlist"].Rows[row][15]);
                         //         MessageBox.Show("t1====",Convert.ToString(t1));
                                chart1.ChartAreas[0].AxisX.Maximum = (double)t1;
                                chart1.ChartAreas[0].AxisX.Minimum = (double)0;
                                break;

                        }
                        //chart1.ChartAreas[0].AxisX.StripLines.Remove(stripLine_x);//خطهای پس زمینه قبلی را حذف میکند
                        //chart1.Series[row].Points.AddXY(0, 0);
                        //StripLine stripLine_x = new StripLine();//نمایش گرید به صورت دلخواه
                        //stripLine_x.StripWidth = 0;
                        //stripLine_x.BorderColor = Color.LightGray;// نمایش خط های پس زمینه
                        //stripLine_x.BorderWidth = 1;
                        ////stripLine_x.Interval = hs;
                        //chart1.ChartAreas[0].AxisX.StripLines.Add(stripLine_x);
                        break;

 

                    case "data":
                        //     if (jarian > 0.00008) jarian = 0.00008;
                        //     if (jarian < -0.00006) jarian = -0.00006;
       //                        MessageBox.Show("d1== "+ data[1] + "  d2==  " + data[2] + "  d3== " + data[3]);
                        chart1.Series[row].Points.AddXY(vi, jarian);


                        //=====================
                        grd_show_data.Rows.Add(vi, jarian, vr);
                //        grd_show_data.CurrentCell = grd_show_data.Rows[grd_show_data.Rows.Count - 1].Cells[0];
                        //=====================


                        ////=====جهت نمایش منظم واحد های روی چارت
                        //double x_min = chart1.Series[row].Points[0].XValue;//مقدار دهی صحیح پیش فرض
                        //double x_max = chart1.Series[row].Points[chart1.Series[row].Points.Count - 1].XValue;
                        //double y_min = chart1.Series[row].Points[0].YValues[0];
                        //double y_max = chart1.Series[row].Points[chart1.Series[row].Points.Count - 1].YValues[0];
                        //foreach (DataPoint dp in chart1.Series[row].Points)
                        //{
                        //    if (dp.XValue < x_min)
                        //        x_min = dp.XValue;

                        //    if (dp.XValue > x_max)
                        //        x_max = dp.XValue;

                        //    if (dp.YValues[0] < y_min)
                        //        y_min = dp.YValues[0];

                        //    if (dp.YValues[0] > y_max)
                        //        y_max = dp.YValues[0];
                        //}
                        //chart1.ChartAreas[0].AxisX.Maximum = x_max;//به صورت لحظه ای مقادیر محور ها تغییر می کند
                        //chart1.ChartAreas[0].AxisX.Minimum = x_min;
                        //chart1.ChartAreas[0].AxisY.Maximum = y_max;
                        //chart1.ChartAreas[0].AxisY.Minimum = y_min;
                        //int step_x = 1, step_y = 1;

                        //if (chart1.Series[row].Points.Count >= 2)
                        //{
                        //    step_x = Math.Abs(Convert.ToInt32(chart1.Series[row].Points[chart1.Series[row].Points.Count - 1].XValue - chart1.Series[row].Points[chart1.Series[row].Points.Count - 2].XValue));//برای بدست اوردن استپ هر واحد واحد آخر منهای واحد یکی به آخر میشود
                        //    step_y= Math.Abs(Convert.ToInt32(chart1.Series[row].Points[chart1.Series[row].Points.Count - 1].YValues[0] - chart1.Series[row].Points[chart1.Series[row].Points.Count - 2].YValues[0]));

                        //    chart1.ChartAreas[0].AxisX.LabelStyle.Interval = step_x;//واحد استپ محور افقی و عمودی
                        //    chart1.ChartAreas[0].AxisY.LabelStyle.Interval = step_y;

                        //    chart1.ChartAreas[0].AxisX.StripLines.Remove(stripLine_x);//خطهای پس زمینه قبلی را حذف میکند
                        //    chart1.ChartAreas[0].AxisY.StripLines.Remove(stripLine_y);

                        //    stripLine_x = new StripLine();//نمایش گرید به صورت دلخواه
                        //    stripLine_x.StripWidth = 0;
                        //    stripLine_x.BorderColor = Color.LightGray;// نمایش خط های پس زمینه
                        //    stripLine_x.BorderWidth = 1;
                        //    stripLine_x.Interval = step_x;
                        //    chart1.ChartAreas[0].AxisX.StripLines.Add(stripLine_x);

                        //    stripLine_y = new StripLine();
                        //    stripLine_y.StripWidth = 0;
                        //    stripLine_y.BorderColor = Color.LightGray;
                        //    stripLine_y.BorderWidth = 1;
                        //    stripLine_y.Interval = step_y;
                        //    chart1.ChartAreas[0].AxisY.StripLines.Add(stripLine_y);
                        //}

                        break;
                }
            }
        }

        delegate void set_(double vi, double vr, double ri);
        void set(double vi, double vr, double ri)
        {
            if (this.InvokeRequired)
            {
                set_ d = new set_(set);
                this.Invoke(d, new object[] { vi, vr, ri });
            }
            else
            {
                listBox2.Items.Add(vi.ToString() + "     " + vr.ToString() + "     " + ri.ToString());
            }
        }
        private void frm_main_FormClosing(object sender, FormClosingEventArgs e)
        {
 
            try
            { thered_runing.Abort();
            } catch { }
        }

        delegate void set_to_object_(int tech_id, int cycle, string object_, int row, int prossecc_now = 0, int prossecc_all = 0, string m = "");
        void set_to_object(int tech_id, int cycle, string object_, int row, int prossecc_now = 0, int prossecc_all = 0, string m = "")
        {
            if (this.InvokeRequired)
            {
                set_to_object_ d = new set_to_object_(set_to_object);
                this.Invoke(d, new object[] { tech_id, cycle, object_, row, prossecc_now, prossecc_all, m });
            }
            else
            {
                switch (object_)
                {
                    //case "label_current_tech":
                    //    if (cycle > 1)
                    //        lbl_current_technique.Text = classglobal.TechName[tech_id] + " -  Cycle:" + cycle.ToString();
                    //    else
                    //        lbl_current_technique.Text = classglobal.TechName[tech_id];
                    //    break;

                    //case "state":
                    //    txt_current_state.Text = txt_current_state.Text + tree_technic_list.Nodes[row].Text + " Finished" + Environment.NewLine;
                    //    break;

                    //case "end":
                    //    btn_start.Enabled = true;
                    //    btn_stop.Enabled = btn_pause.Enabled = false;
                    //    break;

                    case "proccess":
                        try
                        {
                            Int16 value = Convert.ToInt16((prossecc_now * 100) / prossecc_all);
                            if (value <= 100)
                                progressBar1.Value = value;
                        }
                        catch { }
                        break;

                        //case "moghawemat":
                        //    lbl_moghawemat.Text = m;
                        //    break;
                }
            }
        }
        private void pic_stop_MouseEnter(object sender, EventArgs e)
        {
            pic_stop.Image = global::BHP2190.Properties.Resources.stop_hilight;
            pic_stop.BackColor = Color.FromArgb(147, 147, 147);
        }

        private void pic_stop_MouseLeave(object sender, EventArgs e)
        {
            pic_stop.Image = global::BHP2190.Properties.Resources.stop_default;
            pic_stop.BackColor = Color.Gray;
        }

        private void pic_stop_Click(object sender, EventArgs e)
        {
            stop();
        }

        void stop()
        {
            if (is_run == true)
            {
                pic_stop.Image = global::BHP2190.Properties.Resources.stop_disable;
                pic_stop.BackColor = Color.FromArgb(160, 160, 160);
                is_run = false;
                is_stop = true;
                pic_run.Image = global::BHP2190.Properties.Resources.start_default;
                pic_run.BackColor = Color.Gray;
                /*
                                if (!sp1.IsOpen)
                                {
                                    try
                                    {
                                        sp1.Open();
                                        sp1.WriteLine("@30@15@@");
                                    }
                                    catch {
                                    } 
                                }
                  */
                //               MessageBox.Show("end");
                //row_of_technique_running++;//شماره ردیف لیست تکنیک جدید در صورت وجود
                //               start_techniqu();//فراخوانی تابع اجرا کننده لیست تکنیک ها در صورت وجود تکنیک دیگر

                /*            if (sp1.IsOpen)
                                try
                                {
                                    sp1.Close();
                                    thered_runing.Abort();
                                }
                                catch {
                                }
                  */
            }

            //try
            //{
            //    if (!serialPort1.IsOpen)
            //        serialPort1.Open();
            //}
            //catch { }
        }

        string selected_tech = "cv";
        private void pic_select_tech_Click(object sender, EventArgs e)
        {
            //int h = Convert.ToInt16(pic_select_tech.Height / 13);
            //int loc = select_tech_mouse_position.Y;

            //if (loc >= 0 && loc <= h) { pic_select_tech.Image = global::BHP2190.Properties.Resources._1_cv; selected_tech = "cv"; }
            //else if (loc >= h + 1 && loc <= h * 2) { pic_select_tech.Image = global::BHP2190.Properties.Resources._2_ocp; selected_tech = "ocp"; }
            //else if (loc >= (h * 2) + 1 && loc <= h * 3) { pic_select_tech.Image = global::BHP2190.Properties.Resources._3_dcv; selected_tech = "dcv"; }
            //else if (loc >= (h * 3) + 1 && loc <= h * 4) { pic_select_tech.Image = global::BHP2190.Properties.Resources._4_npv; selected_tech = "npv"; }
            //else if (loc >= (h * 4) + 1 && loc <= h * 5) { pic_select_tech.Image = global::BHP2190.Properties.Resources._5_dpv; selected_tech = "dpv"; }
            //else if (loc >= (h * 5) + 1 && loc <= (h * 6) + 1) { pic_select_tech.Image = global::BHP2190.Properties.Resources._6_swv; selected_tech = "swv"; }
            //else if (loc >= (h * 6) + 1 && loc <= (h * 7) + 3) { pic_select_tech.Image = global::BHP2190.Properties.Resources._7_lsv; selected_tech = "lsv"; }
            //else if (loc >= (h * 7) + 1 && loc <= (h * 8) + 4) { pic_select_tech.Image = global::BHP2190.Properties.Resources._8_dcs; selected_tech = "dcs"; }
            //else if (loc >= (h * 8) + 1 && loc <= (h * 9) + 5) { pic_select_tech.Image = global::BHP2190.Properties.Resources._9_dps; selected_tech = "dps"; }
            //else if (loc >= (h * 9) + 1 && loc <= (h * 10) + 5) { pic_select_tech.Image = global::BHP2190.Properties.Resources._10_cpc; selected_tech = "cpc"; }
            //else if (loc >= (h * 10) + 1 && loc <= (h * 11) + 5) { pic_select_tech.Image = global::BHP2190.Properties.Resources._11_chp; selected_tech = "chp"; }
            //else if (loc >= (h * 11) + 1 && loc <= (h * 12) + 5) { pic_select_tech.Image = global::BHP2190.Properties.Resources._12_cha; selected_tech = "cha"; }
            //else if (loc >= (h * 12) + 1 && loc <= (h * 13) + 5) { pic_select_tech.Image = global::BHP2190.Properties.Resources._13_chc; selected_tech = "chc"; }



            //double min = pic_select_tech.Height / select_tech_mouse_position.Y;
            //int div = Convert.ToInt16(Math.Floor(Convert.ToDouble(select_tech_mouse_position.Y / h)));


            //switch (div)
            //{
            //    case 0: pic_select_tech.Image = global::BHP2190.Properties.Resources._1_cv; selected_tech = "cv"; break;
            //    case 1: pic_select_tech.Image = global::BHP2190.Properties.Resources._2_ocp; selected_tech = "ocp"; break;
            //    case 2: pic_select_tech.Image = global::BHP2190.Properties.Resources._3_dcv; selected_tech = "dcv"; break;
            //    case 3: pic_select_tech.Image = global::BHP2190.Properties.Resources._4_npv; selected_tech = "npv"; break;
            //    case 4: pic_select_tech.Image = global::BHP2190.Properties.Resources._5_dpv; selected_tech = "dpv"; break;
            //    case 5: pic_select_tech.Image = global::BHP2190.Properties.Resources._6_swv; selected_tech = "swv"; break;
            //    case 6: pic_select_tech.Image = global::BHP2190.Properties.Resources._7_lsv; selected_tech = "lsv"; break;
            //    case 7: pic_select_tech.Image = global::BHP2190.Properties.Resources._8_dcs; selected_tech = "dcs"; break;
            //    case 8: pic_select_tech.Image = global::BHP2190.Properties.Resources._9_dps; selected_tech = "dps"; break;
            //    case 9: pic_select_tech.Image = global::BHP2190.Properties.Resources._10_cpc; selected_tech = "cpc"; break;
            //    case 10: pic_select_tech.Image = global::BHP2190.Properties.Resources._11_chp; selected_tech = "chp"; break;
            //    case 11: pic_select_tech.Image = global::BHP2190.Properties.Resources._12_cha; selected_tech = "cha"; break;
            //    case 12: pic_select_tech.Image = global::BHP2190.Properties.Resources._13_chc; selected_tech = "chc"; break;
            //}
            //MessageBox.Show(h.ToString() + "    " + select_tech_mouse_position.Y.ToString());
            //if (selected_tech == "cv")
            //{
            //    pnl_cpc.Visible = false;
            //    pnl_cv.Visible = true;
            //}
            //else
            //{
            //    pnl_cv.Visible = false;
            //    pnl_cpc.Visible = true;
            //}
        }

        Point select_tech_mouse_position;
        private void pic_select_tech_MouseDown(object sender, MouseEventArgs e)
        {
            select_tech_mouse_position = e.Location;
        }

        private void pic_analyse_MouseEnter(object sender, EventArgs e)
        {
            var btn = sender as Button;
            btn.BackgroundImage = global::BHP2190.Properties.Resources.button2;
        }

        private void pic_analyse_MouseLeave(object sender, EventArgs e)
        {
            var btn = sender as Button;
            btn.BackgroundImage = global::BHP2190.Properties.Resources.button1;
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            if (dataSet11.chartlist.Rows.Count > 0)
                if (MessageBox.Show("Do you want to save current project?", "Save project", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                   class_file.save_project(dataSet11);
                }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "bhp";
            ofd.Filter = "parameters of technique Files(*.bhp)|*.bhp";
            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add("Techniques List");
                dataSet11.Tables["chartlist"].Rows.Clear();

                DataTable dt = class_file.open_project(ofd.FileName);
               
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dataSet11.Tables["chartlist"].NewRow();

                        for (int j = 0; j < dataSet11.Tables["chartlist"].Columns.Count; j++)
                            dr[j] = dt.Rows[i][j].ToString();

                        dataSet11.Tables["chartlist"].Rows.Add(dr);
                        if (Convert.ToInt16(dataSet11.chartlist[i][13]) == 1)//اگر سیکل یک بود 
                            treeView1.Nodes[0].Nodes.Add((i + 1).ToString() + "- " + classglobal.TechName[Convert.ToInt16(dataSet11.chartlist[i][1])]);
                        else//در غیر اینصورت مقدار سیکل هم اضافه شود
                            treeView1.Nodes[0].Nodes.Add((i + 1).ToString() + "- " + classglobal.TechName[Convert.ToInt16(dataSet11.chartlist[i][1])] + "(" + dataSet11.chartlist[i][13].ToString() + ")");
                    }
                    treeView1.ExpandAll();
                }
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            class_file.save_project(dataSet11);
        }


        public void save_Data(string TechName,int TechNo)
        {
            string strpath = "";// fName = "";
            for (long i = 100001; i < 1000000; i++)
            {
                if (!File.Exists(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\tempfile\\" + classglobal.TechName[int.Parse(dataSet11.chartlist.Rows[row_of_technique_running][1].ToString())].Trim() + i.ToString().Trim() + ".dat"))
                {
                    if (int.Parse(dataSet11.chartlist.Rows[row_of_technique_running][13].ToString()) <= 1)// without cycle
                    {
                        strpath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\tempfile\\" + classglobal.TechName[int.Parse(dataSet11.chartlist.Rows[row_of_technique_running][1].ToString())].Trim() + i.ToString().Trim() + ".dat";
                        //fName = clasglobal.TechName[int.Parse(dschart1.chartlist1_run.Rows[rownum][1].ToString())].Trim() + i.ToString().Trim() + ".dat";
                        break;
                    }
                    else
                    {
                        strpath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\tempfile\\" + classglobal.TechName[int.Parse(dataSet11.chartlist.Rows[row_of_technique_running][1].ToString())].Trim() + (i - 1).ToString().Trim() + ".dat";
                        //fName = clasglobal.TechName[int.Parse(dschart1.chartlist1_run.Rows[rownum][1].ToString())].Trim() + i.ToString().Trim() + ".dat";
                        break;
                    }
                }
            }

            //           string s = "";
            //           string fileName = "";
            //           if (fileName != "")
            //               s = fileName;
            //           else
            //               s = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + ("\\tempfile\\") + TechName + ".dat";

            WriteToFile(TechName,TechNo, strpath);
            dataSet11.chartlist[row_of_technique_running][23] = strpath;
        }

        public void WriteToFile(string tchName,int tchNo, string s)//تابع ذخیره پارامترها درون فایل پروژه
        {
            StreamWriter Fl = new StreamWriter(s, false, Encoding.ASCII);
            if (tchName != "APP" && tchName != "SEQ")
            {
                Fl.WriteLine("Settings for Parameters:");
                Fl.WriteLine("Date:" + DateTime.Today.ToString());

                if (tchName == "DCV" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: DCV");
                    //Fl.WriteLine("TchNo: "+ DCV.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + classglobal.dcv_e1.ToString());
                    Fl.WriteLine("E2:" + classglobal.dcv_e2.ToString());
                    Fl.WriteLine("HStep:" + classglobal.dcv_hs.ToString());
                    Fl.WriteLine("Equilibrium Time:" + classglobal.dcv_eq.ToString());
                    Fl.WriteLine("Scan Rate:" + classglobal.dcv_sr.ToString());
                    Fl.WriteLine("TStep:" + classglobal.dcv_ts.ToString());

                }

                if (tchName == "NPV" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: NPV");
                    //Fl.WriteLine("TchNo: " + NPV.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + txt_npv_e1.Text.ToString());
                    Fl.WriteLine("E2:" + txt_npv_e2.Text.ToString());
                    Fl.WriteLine("HStep:" + txt_npv_hs.Text.ToString());
                    Fl.WriteLine("Pulse Width:" + txt_npv_pw.Text.ToString());
                    Fl.WriteLine("Equilibrium Time:" + txt_npv_eq.Text.ToString());
                    Fl.WriteLine("Scan Rate:" + classglobal.npv_sr.ToString());
                    Fl.WriteLine("Cycles:" + "1"); 
                    Fl.WriteLine("Comment:" + txt_npv_com.Text.ToString());

                }

                if (tchName == "DPV" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: DPV");
                    //Fl.WriteLine("TchNo: " + DPV.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + txt_dpv_e1.Text.ToString());
                    Fl.WriteLine("E2:" + txt_dpv_e2.Text.ToString());
                    Fl.WriteLine("HStep:" + txt_dpv_hs.Text.ToString());
                    Fl.WriteLine("Pulse Height:" + txt_dpv_ph.Text.ToString());
                    Fl.WriteLine("Pulse Width:" + txt_dpv_pw.Text.ToString());
                    Fl.WriteLine("Equilibrium Time:" + txt_dpv_eq.Text.ToString());
                    Fl.WriteLine("Scan Rate:" + txt_dpv_sr.Text.ToString());
                    Fl.WriteLine("TStep:" + txt_dpv_ts.Text.ToString());
                    Fl.WriteLine("Cycles:" + "1");
                    Fl.WriteLine("Comment:" + txt_dpv_com.Text.ToString());

                }

                if (tchName == "SWV" || tchName == "All")
                {

                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: SWV");
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + txt_swv_e1.Text.ToString());
                    Fl.WriteLine("E2:" + txt_swv_e2.Text.ToString());
                    Fl.WriteLine("Frequency:" + txt_swv_fr.Text.ToString());
                    Fl.WriteLine("HStep:" + txt_swv_hs.Text.ToString());
                    Fl.WriteLine("Pulse Height:" + txt_swv_ph.Text.ToString());
                    Fl.WriteLine("Equilibrium Time:" + txt_swv_eq.Text.ToString());
                    Fl.WriteLine("Scan Rate:" + txt_swv_sr.Text.ToString());
                    Fl.WriteLine("Cycles:" + "1");
                    Fl.WriteLine("Comment:" + txt_swv_com.Text.ToString());

                }

                if (tchName == "CV" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: CV");
                    //Fl.WriteLine("TchNo: " + CV.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("Cycles:" + txt_cv_cycle.Text.ToString());
                    Fl.WriteLine("E1:" + txt_cv_e1.Text.ToString());
                    Fl.WriteLine("E2:" + txt_cv_e2.Text.ToString());
                    Fl.WriteLine("E3:" + txt_cv_e3.Text.ToString());
                    Fl.WriteLine("Hold Time:" + txt_cv_ht.Text.ToString());
                    Fl.WriteLine("HStep:" + txt_cv_hs.Text.ToString());
                    Fl.WriteLine("Equilibrium Time:" + txt_cv_eq.Text.ToString());
                    Fl.WriteLine("Scan Rate:" + txt_cv_sr.Text.ToString());
                    Fl.WriteLine("ScanRate_R:" + classglobal.cv_sr.ToString());
                    Fl.WriteLine("TStep:" + txt_cv_ts.Text.ToString());
                    Fl.WriteLine("Comment:" + txt_cv_com.Text.ToString());
                }

                if (tchName == "LSV" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: LSV");
                    //Fl.WriteLine("TchNo: " + LSV.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + txt_lsv_e1.Text.ToString());
                    Fl.WriteLine("E2:" + txt_lsv_e2.Text.ToString());
                    Fl.WriteLine("HStep:" + txt_lsv_hs.Text.ToString());
                    Fl.WriteLine("Equilibrium Time:" + txt_lsv_eq.Text.ToString());
                    Fl.WriteLine("Scan Rate:" + txt_lsv_sr.Text.ToString());
                    Fl.WriteLine("TStep:" + txt_lsv_ts.Text.ToString());
                    Fl.WriteLine("Cycles:" + "1");
                    Fl.WriteLine("Comment:" + txt_lsv_com.Text.ToString());

                }

                if (tchName == "DCS" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: DCS");
                    //Fl.WriteLine("TchNo: " + DCs.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + classglobal.dcs_e1.ToString());
                    Fl.WriteLine("E2:" + classglobal.dcs_e2.ToString());
                    Fl.WriteLine("HStep:" + classglobal.dcs_hs.ToString());
                    Fl.WriteLine("Equilibrium Time:" + classglobal.dcs_eq.ToString());
                    Fl.WriteLine("Scan Rate:" + classglobal.dcs_sr.ToString());
                    Fl.WriteLine("TStep:" + classglobal.dcs_ts.ToString());
                }

                if (tchName == "DPS" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: DPS");
                    //Fl.WriteLine("TchNo: " + DPs.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + classglobal.dps_e1.ToString());
                    Fl.WriteLine("E2:" + classglobal.dps_e2.ToString());
                    Fl.WriteLine("HStep:" + classglobal.dps_hs.ToString());
                    Fl.WriteLine("Pulse Height:" + classglobal.dps_ph.ToString());
                    Fl.WriteLine("Pulse Width:" + classglobal.dps_pw.ToString());
                    Fl.WriteLine("Equilibrium Time:" + classglobal.dps_eq.ToString());
                    Fl.WriteLine("Scan Rate:" + classglobal.dps_sr.ToString());
                    Fl.WriteLine("TStep:" + classglobal.dps_ts.ToString());
                }


                if (tchName == "OCP" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: OCP");
                    //Fl.WriteLine("TchNo: " + OCP.ToString());
                    Fl.WriteLine("Time:" + txt_ocp_time.Text.ToString());
                    Fl.WriteLine("Cycles:" + "1"); //clasglobal.ocpprms.Cycles.ToString());
                }

                if (tchName == "CPC" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: CPC");
                    //Fl.WriteLine("TchNo: " + CPC.ToString());
                    Fl.WriteLine("Current Range:0");
                    Fl.WriteLine("E1:" + txt_cpc_e1.Text.ToString());
                    Fl.WriteLine("Equilibrium Time:" + txt_cpc_eq.Text.ToString());
                    Fl.WriteLine("T1:" + txt_cpc_t1.Text.ToString());
                    Fl.WriteLine("Cycles:" + "1");
                    Fl.WriteLine("Comment:" + txt_cpc_com.Text.ToString());
                }
                /*
                                if (tchName == "CCC" || tchName == "All")
                                {
                                    Fl.WriteLine("\n");
                                    Fl.WriteLine("Technic: CCC");
                                    //Fl.WriteLine("TchNo: " + CCC.ToString());
                                    Fl.WriteLine("Current Range:0");
                                    Fl.WriteLine("E1:" + classglobal.ccc_i1.ToString());
                                    Fl.WriteLine("Equilibrium Time:" + classglobal.ccc_eq.ToString());
                                    Fl.WriteLine("T1:" + classglobal.ccc_t1.ToString());
                                    Fl.WriteLine("Cycles:" + "1"); //clasglobal.cpcprms.Cycles.ToString());
                                }
                */
                if (tchName == "CHP" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: CHP");
                    //Fl.WriteLine("TchNo: " + CA.ToString());
                    Fl.WriteLine("I1:" + classglobal.chp_i1.ToString());
                    Fl.WriteLine("I2:" + classglobal.chp_i2.ToString());
                    Fl.WriteLine("Equilibrium Time:" + classglobal.chp_eq.ToString());
                    Fl.WriteLine("T1:" + classglobal.chp_t1.ToString());
                    Fl.WriteLine("T2:" + classglobal.chp_t2.ToString());
                    Fl.WriteLine("Cycles:" + "1"); //caprms.Cycles.ToString());
                }

                if (tchName == "CHA" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: CHA");
                    //Fl.WriteLine("TchNo: " + CA.ToString());
                    Fl.WriteLine("E1:" + txt_cha_e1.Text.ToString());
                    Fl.WriteLine("E2:" + txt_cha_e2.Text.ToString());
                    Fl.WriteLine("Equilibrium Time:" + txt_cha_eq.Text.ToString());
                    Fl.WriteLine("T1:" + txt_cha_t1.Text.ToString());
                    Fl.WriteLine("T2:" + txt_cha_t2.Text.ToString());
                    Fl.WriteLine("Cycles:" + "1");
                    Fl.WriteLine("Comment:" + txt_cha_com.Text.ToString());
                }

                if (tchName == "CHC" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: CHC");
                    //Fl.WriteLine("TchNo: " + CA.ToString());
                    Fl.WriteLine("E1:" + classglobal.chc_e1.ToString());
                    Fl.WriteLine("E2:" + classglobal.chc_e2.ToString());
                    Fl.WriteLine("Equilibrium Time:" + classglobal.chc_eq.ToString());
                    Fl.WriteLine("T1:" + classglobal.chc_t1.ToString());
                    Fl.WriteLine("T2:" + classglobal.chc_t2.ToString());
                    Fl.WriteLine("Cycles:" + "1"); //caprms.Cycles.ToString());
                }
                /*
                if (tchName == "SC" || tchName == "All")
                {
                    Fl.WriteLine("\n");
                    Fl.WriteLine("Technic: SC");
                    //Fl.WriteLine("TchNo: " + SC.ToString());
                    Fl.WriteLine("E1:" + scprms.E1.ToString());
                    Fl.WriteLine("Q1:" + scprms.Q1.ToString());
                    Fl.WriteLine("E2:" + scprms.E2.ToString());
                    Fl.WriteLine("Q2:" + scprms.Q2.ToString());
                    Fl.WriteLine("Equilibrium Time:" + scprms.EquilibriumTime.ToString());
                    Fl.WriteLine("Cycles:" + scprms.Cycles.ToString());
                    Fl.WriteLine("OCPMeas.:" + scprms.OCPmeasurment.ToString());
                    Fl.WriteLine("vs_OCP:" + (Convert.ToByte(scprms.vs_OCP)).ToString());
                }
                 * */
            }

            /*
                        if (tchName == "APP")
                        {
                            Fl.WriteLine("Port:" + portname);
                            Fl.WriteLine("BRate:" + BRate.ToString());
                            Fl.WriteLine("RT:" + RT.ToString());
                            Fl.WriteLine("WT:" + WT.ToString());
                            Fl.WriteLine("Technic:" + Convert.ToString(this.grf.tech)); //.ToString());
                            //Fl.WriteLine("TchNo: " + Convert.ToString(this.grf.tech));
                            Fl.WriteLine("DFormL:100");// + dfrm.Left.ToString());
                            Fl.WriteLine("DFormT:100");// + dfrm.Top.ToString());
                            Fl.WriteLine("DFormH:100");// + dfrm.Height.ToString());
                            Fl.WriteLine("DFormW:100");// + dfrm.Width.ToString());
                            Fl.WriteLine("GFormL:" + this.grf.Left.ToString());
                            Fl.WriteLine("GFormT:" + this.grf.Top.ToString());
                            Fl.WriteLine("GFormH:" + this.grf.Height.ToString());
                            Fl.WriteLine("GFormW:" + this.grf.Width.ToString());
                            //Fl.WriteLine("Normal:" + rbNormal.Checked.ToString());
                            //Fl.WriteLine("MulRun:" + rbMultiRun.Checked.ToString());
                            //Fl.WriteLine("SeqRun:" + rbSequence.Checked.ToString());
                            Fl.WriteLine("FLine:" + opForm.rbfLine.Checked.ToString());
                            Fl.WriteLine("Line:" + opForm.rbLine.Checked.ToString());
                            Fl.WriteLine("Point:" + opForm.rbPoint.Checked.ToString());
                        }
            */
            //if (tchName == "SEQ")
            //if (grdTech.RowCount > 1)
            //    for (int i = 0; i < grdTech.RowCount - 1; i++)
            //    {
            //        string Gstr = "";
            //        for (int k = 0; k <21 ; k++)/////grdTech.ColumnCount
            //            Gstr = Gstr + grdTech.Rows[i].Cells[k].Value.ToString() + ";";
            //        Fl.WriteLine(Gstr);
            //    }


            if (this.chart1.Series.Count <= 1)
                Fl.WriteLine("============================================================");
            string Ln = "";
            int r = this.chart1.Series[0].Points.Count;
            for (i = 0; i < r; i++)
            {
                Ln = this.chart1.Series[0].Points[i].XValue.ToString();
                Ln = Ln + "\t" + this.chart1.Series[0].Points[i].YValues[0].ToString();
                Ln = Ln + "\t" + this.chart1.Series[0].Points[i].YValues[0].ToString();
                Ln = Ln + "\t" + "0";
                Fl.WriteLine(Ln);
            }
            Fl.Close();
        }



        private void btn_analyse_Click(object sender, EventArgs e)
        {
            if (dataSet11.chartlist.Rows.Count == 0)
            {
                MessageBox.Show("please select a technique(s) for running!!!");
                return;
            }

            forms.frmanalyseGraph2 f = new forms.frmanalyseGraph2();
            
                        //forms.frm_analyse f = new forms.frm_analyse();

                        DataRow dr1 = f.dataSet12.analyselist.NewRow();
                        //    this.runToolStripMenuItem1.Enabled = false;
                        //    this.tsbRun.Enabled = false;

                        f.treeView3.Nodes.Clear();
                        f.treeView3.Nodes.Add("Techniques List");
                        f.dataSet12.analyselist.Clear();

                        for (int i = 0; i < dataSet11.chartlist.Rows.Count; i++)
                        {
                            dr1[0] = dataSet11.chartlist[i][1];
                            dr1[1] = dataSet11.chartlist[i][3];
                            dr1[2] = dataSet11.chartlist[i][4];
                            dr1[3] = dataSet11.chartlist[i][5];
                            dr1[4] = dataSet11.chartlist[i][6];

                            dr1[5] = dataSet11.chartlist[i][7];
                            dr1[6] = dataSet11.chartlist[i][8];
                            dr1[7] = dataSet11.chartlist[i][9];
                            dr1[8] = dataSet11.chartlist[i][10];
                            dr1[9] = dataSet11.chartlist[i][11];

                            dr1[10] = dataSet11.chartlist[i][12];
                            dr1[11] = dataSet11.chartlist[i][13];
                            dr1[12] = dataSet11.chartlist[i][14];
                            dr1[13] = dataSet11.chartlist[i][15];
                            dr1[14] = dataSet11.chartlist[i][16];

                            dr1[15] = dataSet11.chartlist[i][17];
                            dr1[16] = dataSet11.chartlist[i][18];
                            //dr1[17] = dataSet11.chartlist[i][1];
                            //dr1[18] = dataSet11.chartlist[i][1];
                            dr1[19] = dataSet11.chartlist[i][19];

                            dr1[20] = dataSet11.chartlist[i][20];
                            dr1[21] = dataSet11.chartlist[i][23];
                            dr1[22] = dataSet11.chartlist[i][24];

                            f.dataSet12.analyselist.Rows.Add(dr1);

                        }

                        for (int i = 0; i < dataSet11.chartlist.Rows.Count; i++)
                        {
                            if (Convert.ToInt16(dataSet11.chartlist[i][13]) == 1)//اگر سیکل یک بود 
                                f.treeView3.Nodes[0].Nodes.Add((i + 1).ToString() + "- " + classglobal.TechName[Convert.ToInt16(dataSet11.chartlist[i][1])]);
                            else//در غیر اینصورت مقدار سیکل هم اضافه شود
                                f.treeView3.Nodes[0].Nodes.Add((i + 1).ToString() + "- " + classglobal.TechName[Convert.ToInt16(dataSet11.chartlist[i][1])] + "(" + dataSet11.chartlist[i][13].ToString() + ")");
                        }
                        f.treeView3.ExpandAll();




                        //   mainf.toolStripStatusLabel5.Text = "";
                        //   mainf.tsocpTime.Text = " ";
                        //   mainf.toolStripStatusLabel3.Text = " ";
                        //   mainf.tabControl1.SelectedTab = mainf.tabControl1.TabPages[1];// نمایان کردن فرم اجرا

                        // فراخوانی اجرای تکنیک در فرم اجرا
                        //      if (checkCPsCTs.Checked)
                        //      {
                        //          f.runf(Range, clasglobal.cpctprms.CP1, clasglobal.cpctprms.CT1, clasglobal.cpctprms.CP2, clasglobal.cpctprms.CT2, clasglobal.cpctprms.CP3, clasglobal.cpctprms.CT3);
                        //      }
                        //      else
                        //      {
                        //          f.runf(Range, 0, 0, 0, 0, 0, 0);

                        //      }
                        //      

                        try
                        {
                            for (int i = 0; i < x_value.Length; i++)
                                f.chart3.Series[0].Points.AddXY(x_value[i], y_value[i]);

                            f.ShowDialog();
                        }
                        catch
                        {
                    //        MessageBox.Show("ایرادی در داده ها وجود دارد. لطفا ابتدا آنرا رفع کنید");
                        }
                        
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            forms.frm_setting frm = new forms.frm_setting();
            frm.ShowDialog();

            if (sp1.IsOpen)
                sp1.Close();

            if (frm.set==true )//اگر تنظیمات رو انجام و تایید کرد
            {
                sp1.PortName = frm.cmb_PortName.Text;
                sp1.BaudRate = Convert.ToInt16(frm.txt_BRate.Text);
                sp1.ReadTimeout = Convert.ToInt16(frm.txt_RT.Text);
                sp1.WriteTimeout = Convert.ToInt16(frm.txt_WT.Text);
            }

            try
            {
                if (!sp1.IsOpen)
                    sp1.Open();
            }
            catch
            {
                MessageBox.Show("No connected!", "Port", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //pic_select_tech.Focus();
        }

        private void frm_main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{tab}");
        }

        void set_defualt_pic_tech_btn()
        {
            pic_cv.Image = global::BHP2190.Properties.Resources.cv_2;
            pic_ocp.Image = global::BHP2190.Properties.Resources.ocp_2;
            pic_dcv.Image = global::BHP2190.Properties.Resources.dcv_2;
            pic_npv.Image = global::BHP2190.Properties.Resources.npv_2;
            pic_dpv.Image = global::BHP2190.Properties.Resources.dpv_2;
            pic_swv.Image = global::BHP2190.Properties.Resources.swv_2;
            pic_lsv.Image = global::BHP2190.Properties.Resources.lsv_2;
            pic_dcs.Image = global::BHP2190.Properties.Resources.dcs_2;
            pic_dps.Image = global::BHP2190.Properties.Resources.dps_2;
            pic_cpc.Image = global::BHP2190.Properties.Resources.cpc_2;
            pic_chp.Image = global::BHP2190.Properties.Resources.chp_2;
            pic_cha.Image = global::BHP2190.Properties.Resources.cha_2;
            pic_chc.Image = global::BHP2190.Properties.Resources.chc_2;
        }

        void select_tech_view(string tech)
        {
            switch (tech)
            {
                case "cv":
                    set_defualt_pic_tech_btn();
                    pic_cv.Image = global::BHP2190.Properties.Resources.cv_1;
                    selected_tech_panel.Visible = false;
                    pnl_cv.Visible = true;
                    selected_tech_panel = pnl_cv;
                    break;

                case "ocp":
                    set_defualt_pic_tech_btn();
                    pic_ocp.Image = global::BHP2190.Properties.Resources.ocp_1;
                    selected_tech_panel.Visible = false;
                    pnl_ocp.Visible = true;
                    selected_tech_panel = pnl_ocp;
                    break;

                case "dcv":
                    set_defualt_pic_tech_btn();
                    pic_dcv.Image = global::BHP2190.Properties.Resources.dcv_1;
                    selected_tech_panel.Visible = false;
                    pnl_dcv.Visible = true;
                    selected_tech_panel = pnl_dcv;
                    break;

                case "npv":
                    set_defualt_pic_tech_btn();
                    pic_npv.Image = global::BHP2190.Properties.Resources.npv_1;
                    selected_tech_panel.Visible = false;
                    pnl_npv.Visible = true;
                    selected_tech_panel = pnl_npv;
                    break;

                case "dpv":
                    set_defualt_pic_tech_btn();
                    pic_dpv.Image = global::BHP2190.Properties.Resources.dpv_1;
                    selected_tech_panel.Visible = false;
                    pnl_dpv.Visible = true;
                    selected_tech_panel = pnl_dpv;
                    break;

                case "swv":
                    set_defualt_pic_tech_btn();
                    pic_swv.Image = global::BHP2190.Properties.Resources.swv_1;
                    selected_tech_panel.Visible = false;
                    pnl_swv.Visible = true;
                    selected_tech_panel = pnl_swv;
                    break;

                case "lsv":
                    set_defualt_pic_tech_btn();
                    pic_lsv.Image = global::BHP2190.Properties.Resources.lsv_1;
                    selected_tech_panel.Visible = false;
                    pnl_lsv.Visible = true;
                    selected_tech_panel = pnl_lsv;
                    break;

                case "dcs":
                    set_defualt_pic_tech_btn();
                    pic_dcs.Image = global::BHP2190.Properties.Resources.dcs_1;
                    selected_tech_panel.Visible = false;
                    pnl_dcs.Visible = true;
                    selected_tech_panel = pnl_dcs;
                    break;

                case "dps":
                    set_defualt_pic_tech_btn();
                    pic_dps.Image = global::BHP2190.Properties.Resources.dps_1;
                    selected_tech_panel.Visible = false;
                    pnl_dps.Visible = true;
                    selected_tech_panel = pnl_dps;
                    break;

                case "cpc":
                    set_defualt_pic_tech_btn();
                    pic_cpc.Image = global::BHP2190.Properties.Resources.cpc_1;
                    selected_tech_panel.Visible = false;
                    pnl_cpc.Visible = true;
                    selected_tech_panel = pnl_cpc;
                    break;

                case "chp":
                    set_defualt_pic_tech_btn();
                    pic_chp.Image = global::BHP2190.Properties.Resources.chp_1;
                    selected_tech_panel.Visible = false;
                    pnl_chp.Visible = true;
                    selected_tech_panel = pnl_chp;
                    break;

                case "cha":
                    set_defualt_pic_tech_btn();
                    pic_cha.Image = global::BHP2190.Properties.Resources.cha_1;
                    selected_tech_panel.Visible = false;
                    pnl_cha.Visible = true;
                    selected_tech_panel = pnl_cha;
                    break;

                case "chc":
                    set_defualt_pic_tech_btn();
                    pic_chc.Image = global::BHP2190.Properties.Resources.chc_1;
                    selected_tech_panel.Visible = false;
                    pnl_chc.Visible = true;
                    selected_tech_panel = pnl_chc;
                    break;
            }
        }

        private void pic_cv_Click(object sender, EventArgs e)
        {
            if (selected_tech_panel != pnl_cv)//اگر پنل در حالت انتخاب است و رویش کلیک میشود مقادیر ریست نشود
            {
                select_tech_view("cv");//انتخاب ظاهری تکنیک
                set_value_to_textbox("CV");//قرار دادن مقدار پیش فرض تکنیک در تکست باکسها
            }
        }
                
        private void pic_ocp_Click(object sender, EventArgs e)
        {
            if (selected_tech_panel != pnl_ocp)//اگر پنل در حالت انتخاب است و رویش کلیک میشود مقادیر ریست نشود
            {
                select_tech_view("ocp");//انتخاب ظاهری تکنیک
                set_value_to_textbox("OCP");//قرار دادن مقدار پیش فرض تکنیک در تکست باکسها
            }
        }

        private void pic_dcv_Click(object sender, EventArgs e)
        {
            if (selected_tech_panel != pnl_dcv)//اگر پنل در حالت انتخاب است و رویش کلیک میشود مقادیر ریست نشود
            {
                select_tech_view("dcv");//انتخاب ظاهری تکنیک
                set_value_to_textbox("DCV");//قرار دادن مقدار پیش فرض تکنیک در تکست باکسها
            }
        }

        private void pic_npv_Click(object sender, EventArgs e)
        {
            if (selected_tech_panel != pnl_npv)//اگر پنل در حالت انتخاب است و رویش کلیک میشود مقادیر ریست نشود
            {
                select_tech_view("npv");//انتخاب ظاهری تکنیک
                set_value_to_textbox("NPV");//قرار دادن مقدار پیش فرض تکنیک در تکست باکسها
            }
        }

        private void pic_dpv_Click(object sender, EventArgs e)
        {
            if (selected_tech_panel != pnl_dpv)//اگر پنل در حالت انتخاب است و رویش کلیک میشود مقادیر ریست نشود
            {
                select_tech_view("dpv");//انتخاب ظاهری تکنیک
                set_value_to_textbox("DPV");//قرار دادن مقدار پیش فرض تکنیک در تکست باکسها
            }
        }

        private void pic_swv_Click(object sender, EventArgs e)
        {
            if (selected_tech_panel != pnl_swv)//اگر پنل در حالت انتخاب است و رویش کلیک میشود مقادیر ریست نشود
            {
                select_tech_view("swv");//انتخاب ظاهری تکنیک
                set_value_to_textbox("SWV");//قرار دادن مقدار پیش فرض تکنیک در تکست باکسها
            }
        }

        private void pic_lsv_Click(object sender, EventArgs e)
        {
            if (selected_tech_panel != pnl_lsv)//اگر پنل در حالت انتخاب است و رویش کلیک میشود مقادیر ریست نشود
            {
                select_tech_view("lsv");//انتخاب ظاهری تکنیک
                set_value_to_textbox("LSV");//قرار دادن مقدار پیش فرض تکنیک در تکست باکسها
            }
        }

        private void pic_dcs_Click(object sender, EventArgs e)
        {
            if (selected_tech_panel != pnl_dcs)//اگر پنل در حالت انتخاب است و رویش کلیک میشود مقادیر ریست نشود
            {
                select_tech_view("dcs");//انتخاب ظاهری تکنیک
                set_value_to_textbox("DCS");//قرار دادن مقدار پیش فرض تکنیک در تکست باکسها
            }
        }

        private void pic_dps_Click(object sender, EventArgs e)
        {
            if (selected_tech_panel != pnl_dps)//اگر پنل در حالت انتخاب است و رویش کلیک میشود مقادیر ریست نشود
            {
                select_tech_view("dps");//انتخاب ظاهری تکنیک
                set_value_to_textbox("DPS");//قرار دادن مقدار پیش فرض تکنیک در تکست باکسها
            }
        }

        private void pic_cpc_Click(object sender, EventArgs e)
        {
            if (selected_tech_panel != pnl_cpc)//اگر پنل در حالت انتخاب است و رویش کلیک میشود مقادیر ریست نشود
            {
                select_tech_view("cpc");//انتخاب ظاهری تکنیک
                set_value_to_textbox("CPC");//قرار دادن مقدار پیش فرض تکنیک در تکست باکسها
            }
        }

        private void pic_chp_Click(object sender, EventArgs e)
        {
            if (selected_tech_panel != pnl_chp)//اگر پنل در حالت انتخاب است و رویش کلیک میشود مقادیر ریست نشود
            {
                select_tech_view("chp");//انتخاب ظاهری تکنیک
                set_value_to_textbox("CHP");//قرار دادن مقدار پیش فرض تکنیک در تکست باکسها
            }
        }

        private void pic_cha_Click(object sender, EventArgs e)
        {
            if (selected_tech_panel != pnl_cha)//اگر پنل در حالت انتخاب است و رویش کلیک میشود مقادیر ریست نشود
            {
                select_tech_view("cha");//انتخاب ظاهری تکنیک
                set_value_to_textbox("CHA");//قرار دادن مقدار پیش فرض تکنیک در تکست باکسها
            }
        }

        private void pic_chc_Click(object sender, EventArgs e)
        {
            if (selected_tech_panel != pnl_chc)//اگر پنل در حالت انتخاب است و رویش کلیک میشود مقادیر ریست نشود
            {
                select_tech_view("chc");//انتخاب ظاهری تکنیک
                set_value_to_textbox("CHC");//قرار دادن مقدار پیش فرض تکنیک در تکست باکسها
            }
        }


        void set_parameter_to_ds(string tech)
        {
            DataRow dr = dataSet11.chartlist.NewRow();
            dr[0] = 0; dr[1] = 0; dr[2] = 0; dr[3] = 0; dr[4] = 0; dr[5] = 0; dr[6] = 0; dr[7] = 0; dr[8] = 0; dr[9] = 0; dr[10] = 0; dr[11] = 0; dr[12] = 0; dr[13] = 0; dr[14] = 0; dr[15] = 0; dr[16] = 0; dr[17] = 0; dr[18] = 0; dr[19] = 0;
            dataSet11.chartlist.Rows.Add(dr);
            int row = dataSet11.Tables["chartlist"].Rows.Count;
            row--;

            switch (tech)
            {
                case "cv":
                    dataSet11.Tables["chartlist"].Rows[row][0] = row.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][1] = 0;
                    //       dataSet11.Tables["chartlist"].Rows[row][2] = "0";
                    dataSet11.Tables["chartlist"].Rows[row][3] = txt_cv_e1.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][4] = txt_cv_e2.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][5] = txt_cv_e3.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][6] = txt_cv_hs.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][7] = txt_cv_eq.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][8] = txt_cv_sr.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][9] = txt_cv_ts.Text.Trim();
                    //      dataSet11.Tables["chartlist"].Rows[row][10] = "0";
                    //      dataSet11.Tables["chartlist"].Rows[row][11] = "0";
                    //      dataSet11.Tables["chartlist"].Rows[row][12] = "0";
                    dataSet11.Tables["chartlist"].Rows[row][13] = txt_cv_cycle.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][14] = txt_cv_ht.Text.Trim();
                    //       dataSet11.Tables["chartlist"].Rows[row][15] = "0";
                    //       dataSet11.Tables["chartlist"].Rows[row][16] = "0";
                    //       dataSet11.Tables["chartlist"].Rows[row][17] = "0";
                    //       dataSet11.Tables["chartlist"].Rows[row][18] = "0";
                    dataSet11.Tables["chartlist"].Rows[row][23] = txt_cv_com.Text.Trim();
                    break;

                case "ocp":
                    dataSet11.Tables["chartlist"].Rows[row][0] = row.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][1] = 1;
                    dataSet11.Tables["chartlist"].Rows[row][13] = "1";
                    dataSet11.Tables["chartlist"].Rows[row][15] = txt_ocp_time.Text.Trim();
                    break;

                case "npv":
                    dataSet11.Tables["chartlist"].Rows[row][0] = row.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][1] = 3;
                    //      dataSet11.Tables["chartlist"].Rows[row][2] = "0";
                    dataSet11.Tables["chartlist"].Rows[row][3] = txt_npv_e1.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][4] = txt_npv_e2.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][6] = txt_npv_hs.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][10] = txt_npv_pw.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][7] = txt_npv_eq.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][9] = txt_npv_ts.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][13] = "1";
                    dataSet11.Tables["chartlist"].Rows[row][23] = txt_npv_com.Text.Trim();
                    break;

                case "dpv":
                    dataSet11.Tables["chartlist"].Rows[row][0] = row.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][1] = 4;
                    //      dataSet11.Tables["chartlist"].Rows[row][2] = "0";
                    dataSet11.Tables["chartlist"].Rows[row][3] = txt_dpv_e1.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][4] = txt_dpv_e2.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][6] = txt_dpv_hs.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][10] = txt_dpv_pw.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][7] = txt_dpv_eq.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][9] = txt_dpv_ts.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][8] = txt_dpv_sr.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][11] = txt_dpv_ph.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][13] = "1";
                    dataSet11.Tables["chartlist"].Rows[row][23] = txt_dpv_com.Text.Trim();
                    break;


                case "swv":
                    dataSet11.Tables["chartlist"].Rows[row][0] = row.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][1] = 5;
                    //                   dataSet11.Tables["chartlist"].Rows[row]["CR"] = "0";
                    dataSet11.Tables["chartlist"].Rows[row][3] = txt_swv_e1.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][4] = txt_swv_e2.Text.Trim();
                    //                  dataSet11.Tables["chartlist"].Rows[row]["E3"] = "0";
                    dataSet11.Tables["chartlist"].Rows[row][6] = txt_swv_hs.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][7] = txt_swv_eq.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][8] = txt_swv_sr.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][11] = txt_swv_ph.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][12] = txt_swv_fr.Text.Trim();
                    //                  dataSet11.Tables["chartlist"].Rows[row]["TS"] = "0";
                    //                  dataSet11.Tables["chartlist"].Rows[row]["PW"] = "0";


                    //                   dataSet11.Tables["chartlist"].Rows[row]["HT"] = "0";
                    //                  dataSet11.Tables["chartlist"].Rows[row]["T1"] = "0";
                    //                  dataSet11.Tables["chartlist"].Rows[row]["T2"] = "0";
                    //                  dataSet11.Tables["chartlist"].Rows[row]["I1"] = "0";
                    //                  dataSet11.Tables["chartlist"].Rows[row]["I2"] = "0";
                    dataSet11.Tables["chartlist"].Rows[row][13] = "1";
                    dataSet11.Tables["chartlist"].Rows[row][23] = txt_swv_com.Text.Trim();
                    break;



                //      case "swv":
                //          dataSet11.Tables["chartlist"].Rows[row]["row"] = row.ToString();
                //          dataSet11.Tables["chartlist"].Rows[row]["techno"] = 5;
                //          dataSet11.Tables["chartlist"].Rows[row]["E1"] = txt_swv_e1.Text.Trim();
                //          dataSet11.Tables["chartlist"].Rows[row]["E2"] = txt_swv_e2.Text.Trim();
                //          dataSet11.Tables["chartlist"].Rows[row]["HS"] = txt_swv_hs.Text.Trim();
                //          dataSet11.Tables["chartlist"].Rows[row]["QT"] = txt_swv_eq.Text.Trim();
                //          dataSet11.Tables["chartlist"].Rows[row]["SR"] = txt_swv_sr.Text.Trim();
                //          dataSet11.Tables["chartlist"].Rows[row]["FR"] = txt_swv_fr.Text.Trim();
                //          dataSet11.Tables["chartlist"].Rows[row]["PH"] = txt_swv_ph.Text.Trim();
                //          dataSet11.Tables["chartlist"].Rows[row]["CY"] = "1";
                //          dataSet11.Tables["chartlist"].Rows[row]["comment"] = txt_swv_com.Text.Trim();
                //          break;

                case "lsv":
                    dataSet11.Tables["chartlist"].Rows[row][0] = row.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][1] = 6;
                    //                dataSet11.Tables["chartlist"].Rows[row][2] = "0";
                    dataSet11.Tables["chartlist"].Rows[row][3] = txt_lsv_e1.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][4] = txt_lsv_e2.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][6] = txt_lsv_hs.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][7] = txt_lsv_eq.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][8] = txt_lsv_sr.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][9] = txt_lsv_ts.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][13] = "1";
                    dataSet11.Tables["chartlist"].Rows[row][23] = txt_lsv_com.Text.Trim();
                    break;
                case "cpc":
                    dataSet11.Tables["chartlist"].Rows[row][0] = row.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][1] = 9;
                    dataSet11.Tables["chartlist"].Rows[row][13] = "1";
                    dataSet11.Tables["chartlist"].Rows[row][3] = txt_cpc_e1.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][15] = txt_cpc_t1.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][7] = txt_cpc_eq.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][23] = txt_cpc_com.Text.Trim();
                    break;

                case "cha":
                    dataSet11.Tables["chartlist"].Rows[row][0] = row.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][1] = 11;
                    dataSet11.Tables["chartlist"].Rows[row][13] = "1";
                    dataSet11.Tables["chartlist"].Rows[row][3] = txt_cha_e1.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][4] = txt_cha_e2.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][15] = txt_cha_t1.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][16] = txt_cha_t2.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][7] = txt_cha_eq.Text.Trim();
                    dataSet11.Tables["chartlist"].Rows[row][23] = txt_cha_com.Text.Trim();
                    break;

            }
        }
        void load_selected_technic_list()//افزودن تکنیک های انتخاب شده به لیست
        {
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("Techniques List");

            for (int i = 0; i < dataSet11.chartlist.Rows.Count; i++)
            {
                if (Convert.ToInt16(dataSet11.chartlist[i][13]) == 1)//اگر سیکل یک بود 
                    treeView1.Nodes[0].Nodes.Add((i + 1).ToString() + "- " + classglobal.TechName[Convert.ToInt16(dataSet11.chartlist[i][1])]);
                else//در غیر اینصورت مقدار سیکل هم اضافه شود
                    treeView1.Nodes[0].Nodes.Add((i + 1).ToString() + "- " + classglobal.TechName[Convert.ToInt16(dataSet11.chartlist[i][1])] + "(" + dataSet11.chartlist[i][13].ToString() + ")");
            }
            treeView1.ExpandAll();
        }

     
        string selected_tech_name_drag = "";//نام تکنیکی قرار است با درگ و دراپ به لیست افزوده شود
        string pre_name_control_clicked = "";//ابتدای نام کنترلی که کاربر روی آن برای درگ انجام داده است
        private void pnl_cv_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is Panel)
            {
                var pnl = sender as Panel;//برای درگ دراپ وقتی موس رها میشود ابتدا رویداد کنترل قبلی اجرا میشود یعنی رویداد رها کردن روی کنترلی که موس کلیک شده است
                selected_tech_name_drag = pnl.Name.Substring(4);//نام تکنیکی که انتخاب شده و قرار است به لیست افزوده شود
                pre_name_control_clicked = "pnl";
            }
            else if(sender is Label)//اگر روی لیبل عنوان هم کلیک کرد یا درگ کرد با همان شود
            {
                var lbl = sender as Label;//برای درگ دراپ وقتی موس رها میشود ابتدا رویداد کنترل قبلی اجرا میشود یعنی رویداد رها کردن روی کنترلی که موس کلیک شده است
                selected_tech_name_drag = lbl.Parent.Name.Substring(4);//نام تکنیکی که انتخاب شده و قرار است به لیست افزوده شود
                pre_name_control_clicked = "lbl";
            }
            else //اگر روی دکمه تصویری کلیک کرد
            {
                var pic = sender as PictureBox;//برای درگ دراپ وقتی موس رها میشود ابتدا رویداد کنترل قبلی اجرا میشود یعنی رویداد رها کردن روی کنترلی که موس کلیک شده است
                selected_tech_name_drag = pic.Name.Substring(4);//نام تکنیکی که انتخاب شده و قرار است به لیست افزوده شود
                pre_name_control_clicked = "pic";
            }
        }

        private void treeView1_MouseEnter(object sender, EventArgs e)
        {
            /*
            if (selected_tech_name_drag != "")//وقتی موس رها میشود بلافاصله در این رویداد برسی میشود که آیا تکنیک دارد یا نه
            {
                //if (selected_tech_name_drag == "cv" || selected_tech_name_drag == "ocp")//در این نسخه فقط این دو تکنیک فعال هستند
                //{
                 set_parameter_to_ds(selected_tech_name_drag);//افزودن تکنیک انتخابی در لیست دیتاست
                 load_selected_technic_list();//افزودن تکنیک های انتخاب شده به لیست رو فرم
                 selected_tech_name_drag = "";
                //}
                //else
                //    MessageBox.Show("You have to use upper version!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            */
        }


    private void pnl_cv_MouseMove(object sender, MouseEventArgs e)
        {
            if (pre_name_control_clicked == "pnl")//اگر برای درگ روی پنل کلیک کرده
                selected_tech_name_drag = "";//اگر کاربر موس را کلیک کرد و همانجا رها کرد مقدار نگیرد
        }
         
        private void lbl_cycle_title_MouseMove(object sender, MouseEventArgs e)
        {
            if (pre_name_control_clicked == "lbl")//اگر برای درگ روی لیبل تایتل ها کلیک کرده
                selected_tech_name_drag = "";//اگر کاربر موس را کلیک کرد و همانجا رها کرد مقدار نگیرد
        }

        private void Btn_about_Click(object sender, EventArgs e)
        {
            frmAbout af = new frmAbout();
            af.StartPosition = FormStartPosition.CenterScreen;
            //af.VerLabel.Text = Version_No;
            af.ShowDialog();

        }

        public bool history = false;
        private void Btn_history_Click(object sender, EventArgs e)
        {
            forms.frmtempfiles f = new forms.frmtempfiles();
            f.ShowDialog();


            /*
            if (history == false)
                history = true;
            else
                history = false;
 
            if (history == true)
            {
                chart1.Visible = false;
                chart2.Visible = true;

                treeView2.Visible = true;
                label37.Visible = true;
                label37.Text = "";

                lbl_detail.Visible = true;
                lbl_detail.Text = "";

                filltreeview3(4);

                //reset
                chart2.Series.Clear();
                chart2.Titles.Clear();
                chart2.ChartAreas.Clear();
                grd_show_data.Rows.Clear();
        
                
                //setting
                Random Random1 = new Random();
                int Random_Color_Index = 0;
                Random_Color_Index = (int)(Random1.NextDouble() * 1);

                //if (chart2.Series.Count == 0)
                chart2.Series.Add(0.ToString() + "_" + tech_id.ToString() + "_" + cycle);

                chart2.Series[0].Color = classglobal.color_Graph[Random_Color_Index];

                chart2.Titles.Add(classglobal.TechName[tech_id]);

                chart2.ChartAreas.Clear();
                chart2.ChartAreas.Add(0.ToString());

                chart2.ChartAreas[0].AxisY.Title = classglobal.VAxisTitle[tech_id];
                chart2.ChartAreas[0].AxisX.Title = classglobal.HAxisTitle[tech_id];

                chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

                chart2.ChartAreas[0].Area3DStyle.Enable3D = false;
                chart2.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
                chart2.ChartAreas[0].AxisX.LineColor = Color.Black;
                chart2.ChartAreas[0].AxisY.LineColor = Color.Black;
                chart2.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
                chart2.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
                chart2.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
                //chart2.ChartAreas[0].AxisY.LabelStyle.Format = "#.#e-0";
                //chart2.ChartAreas[0].AxisX.LabelStyle.Format = "#0.##";
                //chart2.ChartAreas[0].AxisX.LabelStyle.Format = "0.00";
                chart2.ChartAreas[0].AxisX.MajorGrid.LineWidth = 1;
                chart2.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
                chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                chart2.ChartAreas[0].AxisX.MinorGrid.LineColor = Color.Black;
                chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                chart2.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.Black;
                chart2.ChartAreas[0].AxisX.ScrollBar.BackColor = Color.Black;
                chart2.ChartAreas[0].AxisY.ScrollBar.BackColor = Color.Black;
                chart2.Series[0].IsVisibleInLegend = false;//عدم نمایش رنگ راهنما

                //clear
                chart2.Series[0].Points.Clear();
                chart2.Titles[0].Text = "";

            }
            else
            {
                treeView2.Visible = false;
                label37.Visible = false;
                lbl_detail.Visible = false;

                chart1.Visible = true;
                chart2.Visible = false;

            }
            */
        }

        private void filltreeview3(int typecall)
        {
             treeView2.Nodes[0].Nodes.Clear();

            DirectoryInfo info = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\tempfile");
            FileInfo[] files = null;
            switch (typecall)
            {
                case 1:
                    files = info.GetFiles().OrderBy(p => p.Name).ToArray();
                    break;
                case 2:
                    files = info.GetFiles().OrderByDescending(p => p.Name).ToArray();
                    break;
                case 3:
                    files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();
                    break;
                case 4:
                    files = info.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();
                    break;
            }
            foreach (FileInfo file in files)
            {
                treeView2.Nodes[0].Nodes.Add(file.Name);
            }

        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void TreeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
                if (e.Node.Level > 0)
                {
                    if (e.Node.IsSelected)
                    {
                       restore_Data(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\tempfile\\" + e.Node.Text.Trim());
                       fillparamdata(0);//اگر چند گراف را انتخاب کرده بود و یا سیکل داشت فقط گراف اول را ترسیم کند

                    /*    if (smooth_state)
                            smootht4();
                         else
                         {
                            for (int i = 0; i < shomar_val; i++)
                            {
                                this.chart1.Series[0].Points.AddXY(othertech_val0[i], othertech_val1[i]);
                            }
                         }
                    */
                    }

                }
        }

        private void restore_Data(string fileName)
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
                            chart2.Series[0].Points.DataBindXY(xTmp, yTmp);
                            chart2.Series[0].Points.Clear();
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
                                            label37.Text = "Technique name :" + classglobal.TechName[i];
                                            lbl_detail.Text = "";

                                            if (classglobal.TechName[i].ToLower() == "cha" || classglobal.TechName[i].ToLower() == "chp" || classglobal.TechName[i].ToLower() == "chc")
                                                smooth_state = false;
                                            else
                                                smooth_state = true;
                                            break;
                                        }
                                }
                         
                                tech = (byte)restore_One_Data(s, "Technic");
                                chart2.Series[0].Points.DataBindXY(xTmp, yTmp);
                                chart2.Series[0].Points.Clear();
                        //        ds.chartlist.Clear();
                                chart2.ChartAreas[0].AxisY.Title = classglobal.VAxisTitle[tech];
                                chart2.ChartAreas[0].AxisX.Title = classglobal.HAxisTitle[tech];
                                chart2.Titles[0].Text = techName;
                            }
                            else if (s.IndexOf("Current Range") >= 0 || s.IndexOf("E1") >= 0 || s.IndexOf("E2") >= 0 || s.IndexOf("E3") >= 0 || s.IndexOf("HStep") >= 0 || s.IndexOf("Equilibrium Time") >= 0 || s.IndexOf("Hold Time") >= 0 || s.IndexOf("Scan Rate") >= 0 || s.IndexOf("ScanRate_R") >= 0
                            || s.IndexOf("TStep") >= 0 || s.IndexOf("Pulse Width") >= 0 || s.IndexOf("pulse ") >= 0 || s.IndexOf("Frequency") >= 0 || s.IndexOf("Cycle") >= 0 || s.IndexOf("T1") >= 0 || s.IndexOf("T2") >= 0 || s.IndexOf("I1") >= 0 || s.IndexOf("I2") >= 0 || s.IndexOf("OCP Measurment") >= 0 || s.IndexOf("vs_OCP") >= 0)
                                lbl_detail.Text = lbl_detail.Text.Trim() + Environment.NewLine + s.Trim();


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
                                    line_file -= 13;//this is not cycle 1 then less lines header from all lines
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
            }
        }


        private void fillgraphlist(int tch, double CR, double E1, double E2, double E3, double HS, double QT, double SRF, double SRR,
  double TS, double PW, double PH, double FR, double CY, double HT, double T1, double T2, double I1, double I2, double AA, double Q1, string strpath, string comment100)
        {

            DataRow dr = ds.chartlist.NewRow();
           // MessageBox.Show(dataSet11.Tables["chartlist"].Rows.Count.ToString());

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
            ds.chartlist.Rows.Add(dr);

        }



        private void fillparamdata(int row)
        {

            //MessageBox.Show("row=" + row.ToString()+ "  Color="+ this.chart1.Series[row].Color.ToString());
            chart2.Series[row].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            smootht4(row, chart2.Series[row].Color, 1, 2);
            chart2.Refresh();
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
                this.chart2.Series[row1].Color = color1;
                this.chart2.Series[row1].Points.Clear();

                if (ds.chartlist.Rows[row1][0].ToString() == "10" || ds.chartlist.Rows[row1][0].ToString() == "11" || ds.chartlist.Rows[row1][0].ToString() == "12")
                    this.chart2.Series[row1].Points.AddXY(0, othertech_val1[0]);//رسم در نقطه صفر زمان // technics chp cha chc

                for (int i = 0; i < vn; i++)
                {
                    this.chart2.Series[row1].Points.AddXY(othertech_val0[i], othertech_val1[i]);// رسم همه گرافها هنگام باز شدن
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
            ////return val;

            return 1;
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
               // Set_Graph(0, true, overlaymain);

                try
                {
                this.chart1.Series[0].Points.DataBindXY(xx, yy);
                MessageBox.Show("SetGraphNext");
                //MessageBox.Show("num_Series-2");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + "   Valid Not", "Warning");

                 }
            }
        }


        public void Set_Graph(int num, bool isFromOpen, bool overlaymain)
        {
            Random Random1 = new Random();
            Random rndm2 = new Random();
            Random rndm3 = new Random();
            //chart1.Titles[0].Text = "  ";
            int Random_Color_Index = 0;

            Random_Color_Index = (int)(Random1.NextDouble() * 90);

//            if (!overlaymain)
//                this.chart1.Series.Clear();
            System.Windows.Forms.DataVisualization.Charting.Series ser1 = new System.Windows.Forms.DataVisualization.Charting.Series();

            ser1.Color = classglobal.color_Graph[Random_Color_Index];
            //         MessageBox.Show("ser1.Color=" + ser1.Color.ToString());
            ser1.LabelFormat = "#,##0.###########";
            //   if (opForm.rbfLine.Checked)
            //   {
            //       ser1.ChartType = SeriesChartType.FastLine;

            //   }
            //   if (opForm.rbLine.Checked)
            //   {
            //       ser1.ChartType = SeriesChartType.Line;
            //   }
            //   if (opForm.rbPoint.Checked)
            //   {
            //       ser1.ChartType = SeriesChartType.Point;
            //   }

            this.chart1.Series.Add(ser1);
            num_Series = chart1.Series.Count - 1;
            num_Series = 0;
            ////////////////////////////
            //MessageBox.Show("num_Series=" + num_Series.ToString());
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
            //this.chart2.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            this.chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            //this.chart2.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
        }


        private void restore_Data1(string fileName)
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
                        this.chart1.Series[0].Points.Clear();

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
                                            this.chart1.ChartAreas[0].AxisY.Title = classglobal.VAxisTitle[i];
                                            this.chart1.ChartAreas[0].AxisX.Title = classglobal.HAxisTitle[i];
                                            label37.Text = "Technique name :" + classglobal.TechName[i];
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

        public void smootht41()
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

            this.chart1.Series[0].Points.Clear();
            for (int i = 0; i < vn; i++)
            {
                this.chart1.Series[0].Points.AddXY(othertech_val0[i], othertech_val1[i]);
            }

        }


        private void pic_cv_MouseMove(object sender, MouseEventArgs e)
        {
            if (pre_name_control_clicked == "pic")//اگر برای درگ روی دکمه اصلی کلیک کرده
                selected_tech_name_drag = "";//اگر کاربر موس را کلیک کرد و همانجا رها کرد مقدار نگیرد
        }

        private void TreeView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(e.ToString());
                //int r = e.Node.Index;
              //  int teck_id = Convert.ToInt16(dataSet11.chartlist.Rows[r][1]);
              //  set_data_from_form_to_database(teck_id, r);
            }
            catch { }

        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                int r = e.Node.Index;
                int teck_id = Convert.ToInt16(dataSet11.chartlist.Rows[r][1]);
                set_data_from_form_to_database(teck_id, r);
            }
            catch { }

        }

        private void pnl_cv_DoubleClick(object sender, EventArgs e)
        {
            if (dataSet11.chartlist.Rows.Count == 0)
                if (sender is Panel)//اگر روی پنل دابل کلیک کرد
                {
                    var pnl = sender as Panel;
                    set_parameter_to_ds(pnl.Name.Substring(4));
                    load_selected_technic_list();
                }
                else if (sender is Label)//اگر روی لیبل کلیک کرد
                {
                    var lbl = sender as Label;
                    set_parameter_to_ds(lbl.Parent.Name.Substring(4));//نام تکنیکی که انتخاب شده و قرار است به لیست افزوده شود
                    load_selected_technic_list();
                }
                else //اگر روی دکمه اصلی دابل کلیک کرد
                {
                    var pic = sender as PictureBox;//برای درگ دراپ وقتی موس رها میشود ابتدا رویداد کنترل قبلی اجرا میشود یعنی رویداد رها کردن روی کنترلی که موس کلیک شده است
                    set_parameter_to_ds(pic.Name.Substring(4));//نام تکنیکی که انتخاب شده و قرار است به لیست افزوده شود
                    load_selected_technic_list();
                }
             else
                    {
                MessageBox.Show("Only one tecnique can be run");
                return;
            }
    }

 

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            
            try
            {
                int r = e.Node.Index;
                int teck_id = Convert.ToInt16(dataSet11.chartlist.Rows[r][1]);
                set_data_from_dataset_to_form(teck_id, r);
            }
            catch { }
            
        }

        void set_data_from_dataset_to_form(int tech_id,int row)
        {
            switch (tech_id)
            {
                case 0:
                    select_tech_view("cv");//انتخاب ظاهری تکنیک
                    txt_cv_e1.Text = dataSet11.Tables["chartlist"].Rows[row][3].ToString();
                    txt_cv_e2.Text = dataSet11.Tables["chartlist"].Rows[row][4].ToString();
                    txt_cv_e3.Text = dataSet11.Tables["chartlist"].Rows[row][5].ToString();
                    txt_cv_hs.Text = dataSet11.Tables["chartlist"].Rows[row][6].ToString();
                    txt_cv_eq.Text = dataSet11.Tables["chartlist"].Rows[row][7].ToString();
                    txt_cv_sr.Text = dataSet11.Tables["chartlist"].Rows[row][8].ToString();
                    txt_cv_ts.Text = dataSet11.Tables["chartlist"].Rows[row][9].ToString();
                    txt_cv_cycle.Text = dataSet11.Tables["chartlist"].Rows[row][13].ToString();
                    txt_cv_ht.Text = dataSet11.Tables["chartlist"].Rows[row][14].ToString();
                    txt_cv_com.Text = dataSet11.Tables["chartlist"].Rows[row][24].ToString();

                    break;

                case 1:
                    select_tech_view("ocp");//انتخاب ظاهری تکنیک
                    txt_ocp_time.Text = dataSet11.Tables["chartlist"].Rows[row][15].ToString();
                    dataSet11.Tables["chartlist"].Rows[row]["CY"] = "1";
                    break;

                case 3:
                    select_tech_view("npv");//انتخاب ظاهری تکنیک
                    txt_npv_e1.Text = dataSet11.Tables["chartlist"].Rows[row][3].ToString();
                    txt_npv_e2.Text = dataSet11.Tables["chartlist"].Rows[row][4].ToString();
                    txt_npv_hs.Text = dataSet11.Tables["chartlist"].Rows[row][6].ToString();
                    txt_npv_pw.Text = dataSet11.Tables["chartlist"].Rows[row][10].ToString();
                    txt_npv_eq.Text = dataSet11.Tables["chartlist"].Rows[row][7].ToString();
                    txt_npv_ts.Text = dataSet11.Tables["chartlist"].Rows[row][9].ToString();
                    dataSet11.Tables["chartlist"].Rows[row]["CY"] = "1";
                    dataSet11.Tables["chartlist"].Rows[row][24] = txt_npv_com.Text.Trim();
                    break;

                case 4:
                    select_tech_view("dpv");//انتخاب ظاهری تکنیک
                    txt_dpv_e1.Text = dataSet11.Tables["chartlist"].Rows[row][3].ToString();
                    txt_dpv_e2.Text = dataSet11.Tables["chartlist"].Rows[row][4].ToString();
                    txt_dpv_hs.Text = dataSet11.Tables["chartlist"].Rows[row][6].ToString();
                    txt_dpv_pw.Text = dataSet11.Tables["chartlist"].Rows[row][10].ToString();
                    txt_dpv_eq.Text = dataSet11.Tables["chartlist"].Rows[row][7].ToString();
                    txt_dpv_ts.Text = dataSet11.Tables["chartlist"].Rows[row][9].ToString();
                    txt_dpv_sr.Text = dataSet11.Tables["chartlist"].Rows[row][8].ToString();
                    txt_dpv_ph.Text = dataSet11.Tables["chartlist"].Rows[row][11].ToString();
                    dataSet11.Tables["chartlist"].Rows[row][13] = "1";
                    dataSet11.Tables["chartlist"].Rows[row][24] = txt_dpv_com.Text.Trim();
                    break;

                case 5:
                    select_tech_view("swv");//انتخاب ظاهری تکنیک
                    txt_swv_e1.Text = dataSet11.Tables["chartlist"].Rows[row][3].ToString();
                    txt_swv_e2.Text = dataSet11.Tables["chartlist"].Rows[row][4].ToString();
                    txt_swv_hs.Text = dataSet11.Tables["chartlist"].Rows[row][6].ToString();
                    txt_swv_fr.Text = dataSet11.Tables["chartlist"].Rows[row][12].ToString();
                    txt_swv_eq.Text = dataSet11.Tables["chartlist"].Rows[row][7].ToString();
                    txt_swv_sr.Text = dataSet11.Tables["chartlist"].Rows[row][8].ToString();
                    txt_swv_ph.Text = dataSet11.Tables["chartlist"].Rows[row][11].ToString();
                    dataSet11.Tables["chartlist"].Rows[row][13] = "1";
                    dataSet11.Tables["chartlist"].Rows[row][24] = txt_swv_com.Text.Trim();
                    break;


                case 6:
                    select_tech_view("lsv");//انتخاب ظاهری تکنیک
                    txt_lsv_e1.Text = dataSet11.Tables["chartlist"].Rows[row][3].ToString();
                    txt_lsv_e2.Text = dataSet11.Tables["chartlist"].Rows[row][4].ToString();
                    txt_lsv_hs.Text = dataSet11.Tables["chartlist"].Rows[row][6].ToString();
                    txt_lsv_eq.Text = dataSet11.Tables["chartlist"].Rows[row][7].ToString();
                    txt_lsv_sr.Text = dataSet11.Tables["chartlist"].Rows[row][8].ToString();
                    txt_lsv_ts.Text = dataSet11.Tables["chartlist"].Rows[row][9].ToString();
                    txt_lsv_com.Text = dataSet11.Tables["chartlist"].Rows[row][24].ToString();
                    dataSet11.Tables["chartlist"].Rows[row]["CY"] = "1";
                    break;

                case 9:
                    select_tech_view("cpc");//انتخاب ظاهری تکنیک
                    txt_cpc_e1.Text = dataSet11.Tables["chartlist"].Rows[row][3].ToString();
                    txt_cpc_t1.Text = dataSet11.Tables["chartlist"].Rows[row][15].ToString();
                    txt_cpc_eq.Text = dataSet11.Tables["chartlist"].Rows[row][7].ToString();
                    dataSet11.Tables["chartlist"].Rows[row][13] = "1";
                    txt_cpc_com.Text = dataSet11.Tables["chartlist"].Rows[row][24].ToString();
                    break;

                case 11:
                    select_tech_view("cha");//انتخاب ظاهری تکنیک
                    txt_cha_e1.Text = dataSet11.Tables["chartlist"].Rows[row][3].ToString();
                    txt_cha_e2.Text = dataSet11.Tables["chartlist"].Rows[row][4].ToString();
                    txt_cha_t1.Text = dataSet11.Tables["chartlist"].Rows[row][15].ToString();
                    txt_cha_t2.Text = dataSet11.Tables["chartlist"].Rows[row][16].ToString();
                    txt_cha_eq.Text = dataSet11.Tables["chartlist"].Rows[row][7].ToString();
                    dataSet11.Tables["chartlist"].Rows[row][13] = "1";
                    txt_cha_com.Text = dataSet11.Tables["chartlist"].Rows[row][24].ToString();
                    break;

            }
        }

        void set_data_from_form_to_database(int tech_id, int row)
        {
            switch (tech_id)
            {
                case 0:
                    select_tech_view("cv");//انتخاب ظاهری تکنیک
                    dataSet11.Tables["chartlist"].Rows[row][3] = txt_cv_e1.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][4] = txt_cv_e2.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][5] = txt_cv_e3.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][6] = txt_cv_hs.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][7] = txt_cv_eq.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][8] = txt_cv_sr.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][9] = txt_cv_ts.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][13] = txt_cv_cycle.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][14] = txt_cv_ht.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][24] = txt_cv_com.Text.ToString();

                    break;

                case 1:
                    select_tech_view("ocp");//انتخاب ظاهری تکنیک
                    dataSet11.Tables["chartlist"].Rows[row][15] = txt_ocp_time.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row]["CY"] = "1";
                    break;

                case 3:
                    select_tech_view("npv");//انتخاب ظاهری تکنیک
                    dataSet11.Tables["chartlist"].Rows[row][3] = txt_npv_e1.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][4] = txt_npv_e2.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][6] = txt_npv_hs.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][10] = txt_npv_pw.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][7] = txt_npv_eq.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][9] = txt_npv_ts.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row]["CY"] = "1";
                    dataSet11.Tables["chartlist"].Rows[row][24] = txt_npv_com.Text.Trim();
                    break;

                case 4:
                    select_tech_view("dpv");//انتخاب ظاهری تکنیک
                    dataSet11.Tables["chartlist"].Rows[row][3] = txt_dpv_e1.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][4] = txt_dpv_e2.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][6] = txt_dpv_hs.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][10] = txt_dpv_pw.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][7] = txt_dpv_eq.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][9] = txt_dpv_ts.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][8] = txt_dpv_sr.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][11] = txt_dpv_ph.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][13] = "1";
                    dataSet11.Tables["chartlist"].Rows[row][24] = txt_dpv_com.Text.Trim();
                    break;

                case 5:
                    select_tech_view("swv");//انتخاب ظاهری تکنیک
                    dataSet11.Tables["chartlist"].Rows[row][3] = txt_swv_e1.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][4] = txt_swv_e2.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][6] = txt_swv_hs.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][12] = txt_swv_fr.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][7] = txt_swv_eq.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][8] = txt_swv_sr.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][11] = txt_swv_ph.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][13] = "1";
                    dataSet11.Tables["chartlist"].Rows[row][24] = txt_swv_com.Text.Trim();
                    break;


                case 6:
                    select_tech_view("lsv");//انتخاب ظاهری تکنیک
                    dataSet11.Tables["chartlist"].Rows[row][3] = txt_lsv_e1.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][4] = txt_lsv_e2.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][6] = txt_lsv_hs.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][7] = txt_lsv_eq.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][8] = txt_lsv_sr.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][9] = txt_lsv_ts.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][24] = txt_lsv_com.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row]["CY"] = "1";
                    break;

                case 9:
                    select_tech_view("cpc");//انتخاب ظاهری تکنیک
                    dataSet11.Tables["chartlist"].Rows[row][3] = txt_cpc_e1.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][15] = txt_cpc_t1.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][7] = txt_cpc_eq.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][13] = "1";
                    dataSet11.Tables["chartlist"].Rows[row][24] = txt_cpc_com.Text.ToString();
                    break;

                case 11:
                    select_tech_view("cha");//انتخاب ظاهری تکنیک
                    dataSet11.Tables["chartlist"].Rows[row][3] = txt_cha_e1.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][4] = txt_cha_e2.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][15] = txt_cha_t1.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][16] = txt_cha_t2.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][7] = txt_cha_eq.Text.ToString();
                    dataSet11.Tables["chartlist"].Rows[row][13] = "1";
                    dataSet11.Tables["chartlist"].Rows[row][24] = txt_cha_com.Text.ToString();
                    break;

            }
        }


        private void mnu_remove_Click(object sender, EventArgs e)
        {
            try
            {
                int r = -1;
                r = treeView1.SelectedNode.Index;

                dataSet11.chartlist.Rows.RemoveAt(r);

                load_selected_technic_list();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mnu_list.Show(treeView1, new Point(e.X, e.Y));
                treeView1.SelectedNode = e.Node;//وقتی راست کلیک میکنند همان نود انتخاب شود
            }
        }

        private void mnu_copy_Click(object sender, EventArgs e)
        {
            try
            {
                int r = treeView1.SelectedNode.Index;

                DataRow dr = dataSet11.chartlist.NewRow();

                for (int j = 0; j < dataSet11.chartlist.Columns.Count; j++)
                    dr[j] = dataSet11.chartlist.Rows[r][j];
                dataSet11.chartlist.Rows.Add(dr);

                load_selected_technic_list();

                TreeNode node = treeView1.Nodes[0].Nodes[r];
                treeView1.SelectedNode = node;
            }
            catch { }
        }

        private void copyMultiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode n = treeView1.Nodes[0];
                if (treeView1.SelectedNode == n) return;

                forms.frm_copy_multi f = new forms.frm_copy_multi();
                f.ShowDialog();

                if (f.set == true)
                {
                    int r = treeView1.SelectedNode.Index;

                    for (int k = 1; k <= Convert.ToInt16(f.textBox1.Text); k++)
                    {
                        DataRow dr = dataSet11.chartlist.NewRow();

                        for (int j = 0; j < dataSet11.chartlist.Columns.Count; j++)
                            dr[j] = dataSet11.chartlist.Rows[r][j];
                        dataSet11.chartlist.Rows.Add(dr);
                    }

                    load_selected_technic_list();

                    TreeNode node = treeView1.Nodes[0].Nodes[r];
                    treeView1.SelectedNode = node;
                }
            }
            catch { }
        }

        private void mnu_up_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.Nodes.Count == 0) return;
                int r = treeView1.SelectedNode.Index;
                if (r == 0) return;

                this.Cursor = Cursors.WaitCursor;

                DataTable dt = new DataTable();
                foreach (DataColumn cl in dataSet11.chartlist.Columns)
                    dt.Columns.Add(cl.ColumnName);

                for (int i = 0; i < dataSet11.chartlist.Rows.Count; i++)
                {
                    DataRow dr;
                    dr = dt.NewRow();

                    if (i + 1 == r)
                    {
                        for (int j = 0; j < dataSet11.chartlist.Columns.Count; j++)
                            dr[j] = dataSet11.chartlist.Rows[i + 1][j];
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        for (int j = 0; j < dataSet11.chartlist.Columns.Count; j++)
                            dr[j] = dataSet11.chartlist.Rows[i][j];
                        dt.Rows.Add(dr);

                        i++;
                    }
                    else
                    {
                        for (int j = 0; j < dataSet11.chartlist.Columns.Count; j++)
                            dr[j] = dataSet11.chartlist.Rows[i][j];
                        dt.Rows.Add(dr);
                    }
                }

                dataSet11.chartlist.Rows.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr;
                    dr = dataSet11.chartlist.NewRow();
                    for (int j = 0; j < dt.Columns.Count; j++)
                        dr[j] = dt.Rows[i][j];

                    dataSet11.chartlist.Rows.Add(dr);
                }

                load_selected_technic_list();

                TreeNode node = treeView1.Nodes[0].Nodes[r - 1];
                treeView1.SelectedNode = node;
                treeView1.Focus();
            }
            catch { }
           
            this.Cursor = Cursors.Default;
        }

        private void mnu_down_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.Nodes.Count == 0) return;
                int r = treeView1.SelectedNode.Index;
                if (r == treeView1.Nodes[0].Nodes.Count - 1) return;

                this.Cursor = Cursors.WaitCursor;

                DataTable dt = new DataTable();
                foreach (DataColumn cl in dataSet11.chartlist.Columns)
                    dt.Columns.Add(cl.ColumnName);

                for (int i = 0; i < dataSet11.chartlist.Rows.Count; i++)
                {
                    DataRow dr;
                    dr = dt.NewRow();

                    if (i == r)
                    {
                        for (int j = 0; j < dataSet11.chartlist.Columns.Count; j++)
                            dr[j] = dataSet11.chartlist.Rows[i + 1][j];
                        dt.Rows.Add(dr);

                        dr = dt.NewRow();
                        for (int j = 0; j < dataSet11.chartlist.Columns.Count; j++)
                            dr[j] = dataSet11.chartlist.Rows[i][j];
                        dt.Rows.Add(dr);

                        i++;
                    }
                    else
                    {
                        for (int j = 0; j < dataSet11.chartlist.Columns.Count; j++)
                            dr[j] = dataSet11.chartlist.Rows[i][j];
                        dt.Rows.Add(dr);
                    }
                }

                dataSet11.chartlist.Rows.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr;
                    dr = dataSet11.chartlist.NewRow();
                    for (int j = 0; j < dt.Columns.Count; j++)
                        dr[j] = dt.Rows[i][j];

                    dataSet11.chartlist.Rows.Add(dr);
                }

                load_selected_technic_list();

                TreeNode node = treeView1.Nodes[0].Nodes[r + 1];
                treeView1.SelectedNode = node;
                treeView1.Focus();
            }
            catch { }

            this.Cursor = Cursors.Default;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            //if (listBox2.Visible == true)
            //    listBox2.Visible = false;
            //else
            //    listBox2.Visible = true;

            try
            {
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Help.pdf");
            }
            catch (Exception s)
            {
                MessageBox.Show(s.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
