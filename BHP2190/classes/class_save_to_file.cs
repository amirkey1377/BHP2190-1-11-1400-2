using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BHP2190.classes;

namespace BHP2190.classes
{
    public static class class_save_to_file
    {
        public static void save_data_to_file_runtime(int technique, double[] x, double[] y, double[] z, double[] w, DataRow dr, int cycle)
        {
            try
            {
                string path_file = "";
                string header = "";

                if (cycle == 1)//ساختن نام فایل
                {
                    string date_now = methods.date_now_milady();
                    string time_now = methods.time();

                    string tech_name = classglobal.TechName[technique] + "_";
                    string date = date_now.Substring(0, 4) + "_" + date_now.Substring(5, 2) + "_" + date_now.Substring(8, 2) + "__";//2020/01/01
                    string time = time_now.Substring(0, 2) + "_" + time_now.Substring(3, 2) + "_" + time_now.Substring(6, 2);//14/20/15

                    string file_name = tech_name + date + time + ".bhpd";

                    path_file = Application.StartupPath + "//Temp file//" + file_name;
                    dr[23] = path_file;

                    string current_date = date_now + " " + time_now;//برای اینکه زمان نوشته شده در هدر با نام فایل یکسان باشد
                    header = create_header_file(technique, dr, current_date);
                }
                else
                    path_file = dr[23].ToString();//برای تکنیک سی وی در سیکل آخر که فایل قرار است ذخیره شود در سیکل اول نامش ساخته شده و با همان نام ذخیره شود 



                string data = "";

                if (cycle == 1)//اگر سیکل اول است هدر را بسازد در غیر اینصورت همان فایل را باز کند
                    data = header;
                else
                    data = File.ReadAllText(path_file);

                data += "Cycle Number:" + cycle.ToString() + "\n\n";

                for (int i = 0; i < x.Length; i++)
                {
                    data += x[i].ToString().Trim() + "\t";
                    data += y[i].ToString().Trim() + "\t";
                    data += z[i].ToString().Trim() + "\t";
                    data += w[i].ToString().Trim() + "\n";
                }

                data += "\n============================================================\n";

                File.WriteAllText(path_file, data);
            }
            catch (Exception err)
            {
                MessageBox.Show("save_data_to_file_runtime: " + err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static string create_header_file(int technique, DataRow dr, string date)//ساخت هدر فایل برای ذخیره دیتا
        {
            string header = "Settings for Parameters:";
            header += "Date:" + date;
            header += "\n\n";

            switch (technique)
            {
                case 4:

                    header += "Technic:CV  \n";
                    header += "Current Range:0  \n";
                    header += "Cycles:" + dr[13].ToString() + "\n";
                    header += "E1:" + dr[3].ToString() + "\n";
                    header += "E2:" + dr[4].ToString() + "\n";
                    header += "E3:" + dr[5].ToString() + "\n";
                    header += "Hold Time:0" + "\n";
                    header += "HStep:" + dr[6].ToString() + "\n";
                    header += "Equilibrium Time:" + dr[7].ToString() + "\n";
                    header += "Scan Rate:" + dr[8].ToString() + "\n";
                    header += "ScanRate_R:" + dr[8].ToString() + "\n";
                    header += "TStep:" + dr[9].ToString() + "\n";

                    break;


                    //if (tchName != "APP" && tchName != "SEQ")
                    //{
                    //    Fl.WriteLine("Settings for Parameters:");
                    //    Fl.WriteLine("Date:" + DateTime.Now.ToString());

                    //    if (tchName == "DCV" || tchName == "All")
                    //    {
                    //        Fl.WriteLine("\n");
                    //        Fl.WriteLine("Technic: DCV");
                    //        Fl.WriteLine("Current Range:0");
                    //        Fl.WriteLine("E1:" + dr[3].ToString());
                    //        Fl.WriteLine("E2:" + dr[4].ToString());
                    //        Fl.WriteLine("HStep:" + dr[6].ToString());
                    //        Fl.WriteLine("Equilibrium Time:" + dr[7].ToString());
                    //        Fl.WriteLine("Scan Rate:" + dr[8].ToString());
                    //        Fl.WriteLine("TStep:" + dr[9].ToString());
                    //        if (typedevice == 2)
                    //        {
                    //            Fl.WriteLine("vs_OCP:" + dr[19].ToString());
                    //            Fl.WriteLine("OCP Measurment:" + dr[20].ToString());
                    //        }
                    //    }

                    //    if (tchName == "NPV" || tchName == "All")
                    //    {
                    //        Fl.WriteLine("\n");
                    //        Fl.WriteLine("Technic: NPV");
                    //        //Fl.WriteLine("TchNo: " + NPV.ToString());
                    //        Fl.WriteLine("Current Range:0");
                    //        Fl.WriteLine("E1:" + dr[3].ToString());
                    //        Fl.WriteLine("E2:" + dr[4].ToString());
                    //        Fl.WriteLine("HStep:" + dr[6].ToString());
                    //        Fl.WriteLine("Pulse Width:" + dr[10].ToString());
                    //        Fl.WriteLine("Equilibrium Time:" + dr[7].ToString());
                    //        Fl.WriteLine("Scan Rate:" + dr[8].ToString());
                    //        Fl.WriteLine("TStep:" + dr[9].ToString());
                    //        if (typedevice == 2)
                    //        {
                    //            Fl.WriteLine("vs_OCP:" + dr[19].ToString());
                    //            Fl.WriteLine("OCP Measurment:" + dr[20].ToString());
                    //        }
                    //    }

                    //    if (tchName == "DPV" || tchName == "All")
                    //    {
                    //        Fl.WriteLine("\n");
                    //        Fl.WriteLine("Technic: DPV");
                    //        //Fl.WriteLine("TchNo: " + DPV.ToString());
                    //        Fl.WriteLine("Current Range:0");
                    //        Fl.WriteLine("E1:" + dr[3].ToString());
                    //        Fl.WriteLine("E2:" + dr[4].ToString());
                    //        Fl.WriteLine("HStep:" + dr[6].ToString());
                    //        Fl.WriteLine("Pulse Height:" + dr[11].ToString());
                    //        Fl.WriteLine("Pulse Width:" + dr[10].ToString());
                    //        Fl.WriteLine("Equilibrium Time:" + dr[7].ToString());
                    //        Fl.WriteLine("Scan Rate:" + dr[8].ToString());
                    //        Fl.WriteLine("TStep:" + dr[9].ToString());
                    //        if (typedevice == 2)
                    //        {
                    //            Fl.WriteLine("vs_OCP:" + dr[19].ToString());
                    //            Fl.WriteLine("OCP Measurment:" + dr[20].ToString());
                    //        }
                    //    }

                    //    if (tchName == "SWV" || tchName == "All")
                    //    {

                    //        Fl.WriteLine("\n");
                    //        Fl.WriteLine("Technic: SWV");
                    //        Fl.WriteLine("Current Range:0");
                    //        Fl.WriteLine("E1:" + dr[3].ToString());
                    //        Fl.WriteLine("E2:" + dr[4].ToString());
                    //        Fl.WriteLine("Frequency:" + dr[12].ToString());
                    //        Fl.WriteLine("HStep:" + dr[6].ToString());
                    //        Fl.WriteLine("Pulse Height:" + dr[11].ToString());
                    //        Fl.WriteLine("Equilibrium Time:" + dr[7].ToString());
                    //        Fl.WriteLine("Scan Rate:" + dr[8].ToString());
                    //        if (typedevice == 2)
                    //        {
                    //            Fl.WriteLine("vs_OCP:" + dr[19].ToString());
                    //            Fl.WriteLine("OCP Measurment:" + dr[20].ToString());
                    //        }
                    //    }

                    //    if (tchName == "CV" || tchName == "All")
                    //    {

                    //        Fl.WriteLine("\n");
                    //        Fl.WriteLine("Technic: CV");
                    //        //Fl.WriteLine("TchNo: " + CV.ToString());
                    //        Fl.WriteLine("Current Range:0");
                    //        //Fl.WriteLine("Cycles:" + dr[13].ToString());   // i dont knowb why cycle 1 lesses than from user set thise here
                    //        Fl.WriteLine("Cycles:" + movedata.main_cycle);
                    //        Fl.WriteLine("E1:" + dr[3].ToString());
                    //        Fl.WriteLine("E2:" + dr[4].ToString());
                    //        Fl.WriteLine("E3:" + dr[5].ToString());
                    //        Fl.WriteLine("Hold Time:0");
                    //        Fl.WriteLine("HStep:" + dr[6].ToString());
                    //        Fl.WriteLine("Equilibrium Time:" + dr[7].ToString());
                    //        Fl.WriteLine("Scan Rate:" + dr[8].ToString());
                    //        Fl.WriteLine("ScanRate_R:" + dr[8].ToString());
                    //        Fl.WriteLine("TStep:" + dr[9].ToString());
                    //        if (typedevice == 2)
                    //        {
                    //            Fl.WriteLine("vs_OCP:" + dr[19].ToString());
                    //            Fl.WriteLine("OCP Measurment:" + dr[20].ToString());
                    //        }
                    //    }

                    //    if (tchName == "LSV" || tchName == "All")
                    //    {
                    //        Fl.WriteLine("\n");
                    //        Fl.WriteLine("Technic: LSV");
                    //        //Fl.WriteLine("TchNo: " + LSV.ToString());
                    //        Fl.WriteLine("Current Range:0");
                    //        Fl.WriteLine("E1:" + dr[3].ToString());
                    //        Fl.WriteLine("E2:" + dr[4].ToString());
                    //        Fl.WriteLine("HStep:" + dr[6].ToString());
                    //        Fl.WriteLine("Equilibrium Time:" + dr[7].ToString());
                    //        Fl.WriteLine("Scan Rate:" + dr[8].ToString());
                    //        Fl.WriteLine("TStep:" + dr[9].ToString());
                    //        if (typedevice == 2)
                    //        {
                    //            Fl.WriteLine("vs_OCP:" + dr[19].ToString());
                    //            Fl.WriteLine("OCP Measurment:" + dr[20].ToString());
                    //        }

                    //    }

                    //    if (tchName == "DCS" || tchName == "All")
                    //    {
                    //        Fl.WriteLine("\n");
                    //        Fl.WriteLine("Technic: DCs");
                    //        //Fl.WriteLine("TchNo: " + DCs.ToString());
                    //        Fl.WriteLine("Current Range:0");
                    //        Fl.WriteLine("E1:" + dr[3].ToString());
                    //        Fl.WriteLine("E2:" + dr[4].ToString());
                    //        Fl.WriteLine("HStep:" + dr[6].ToString());
                    //        Fl.WriteLine("Equilibrium Time:" + dr[7].ToString());
                    //        Fl.WriteLine("Scan Rate:" + dr[8].ToString());
                    //        Fl.WriteLine("TStep:" + dr[9].ToString());
                    //        if (typedevice == 2)
                    //        {
                    //            Fl.WriteLine("vs_OCP:" + dr[19].ToString());
                    //            Fl.WriteLine("OCP Measurment:" + dr[20].ToString());
                    //        }
                    //    }

                    //    if (tchName == "DPS" || tchName == "All")
                    //    {
                    //        Fl.WriteLine("\n");
                    //        Fl.WriteLine("Technic: DPS");
                    //        //Fl.WriteLine("TchNo: " + DPs.ToString());
                    //        Fl.WriteLine("Current Range:0");
                    //        Fl.WriteLine("E1:" + dr[3].ToString());
                    //        Fl.WriteLine("E2:" + dr[4].ToString());
                    //        Fl.WriteLine("HStep:" + dr[6].ToString());
                    //        Fl.WriteLine("Pulse Height:" + dr[11].ToString());
                    //        Fl.WriteLine("Pulse Width:" + dr[10].ToString());
                    //        Fl.WriteLine("Equilibrium Time:" + dr[7].ToString());
                    //        Fl.WriteLine("Scan Rate:" + dr[8].ToString());
                    //        Fl.WriteLine("TStep:" + dr[9].ToString());
                    //        if (typedevice == 2)
                    //        {
                    //            Fl.WriteLine("vs_OCP:" + dr[19].ToString());
                    //            Fl.WriteLine("OCP Measurment:" + dr[20].ToString());
                    //        }
                    //    }

                    //    if (tchName == "CPC" || tchName == "All")
                    //    {
                    //        Fl.WriteLine("\n");
                    //        Fl.WriteLine("Technic: CPC");
                    //        //Fl.WriteLine("TchNo: " + CPC.ToString());
                    //        Fl.WriteLine("Current Range:0");
                    //        Fl.WriteLine("E1:" + dr[3].ToString());
                    //        Fl.WriteLine("Equilibrium Time:" + dr[7].ToString());
                    //        Fl.WriteLine("T1:" + dr[15].ToString());
                    //        Fl.WriteLine("Cycles:1");
                    //        if (typedevice == 2)
                    //        {
                    //            Fl.WriteLine("vs_OCP:" + dr[19].ToString());
                    //            Fl.WriteLine("OCP Measurment:" + dr[20].ToString());
                    //        }
                    //    }

                    //    if (tchName == "CCC" || tchName == "All")
                    //    {
                    //        Fl.WriteLine("\n");
                    //        Fl.WriteLine("Technic: CCC");
                    //        //Fl.WriteLine("TchNo: " + CCC.ToString());
                    //        Fl.WriteLine("Current Range:0");
                    //        Fl.WriteLine("E1:" + dr[3].ToString());
                    //        Fl.WriteLine("Equilibrium Time:" + dr[7].ToString());
                    //        Fl.WriteLine("T1:" + dr[15].ToString());
                    //        Fl.WriteLine("Cycles:1");
                    //        if (typedevice == 2)
                    //        {
                    //            Fl.WriteLine("vs_OCP:" + dr[19].ToString());
                    //            Fl.WriteLine("OCP Measurment:" + dr[20].ToString());
                    //        }
                    //    }
                    //    if (tchName == "CHP" || tchName == "All")
                    //    {
                    //        Fl.WriteLine("\n");
                    //        Fl.WriteLine("Technic: CHP");
                    //        //Fl.WriteLine("TchNo: " + CA.ToString());
                    //        Fl.WriteLine("I1:" + dr[17].ToString());
                    //        Fl.WriteLine("I2:" + dr[18].ToString());
                    //        Fl.WriteLine("Equilibrium Time:" + dr[7].ToString());
                    //        Fl.WriteLine("T1:" + double.Parse(dr[15].ToString()) * 1000);
                    //        Fl.WriteLine("T2:" + double.Parse(dr[16].ToString()) * 1000);
                    //        Fl.WriteLine("Cycles:" + "1"); //caprms.Cycles.ToString());
                    //        if (typedevice == 2)
                    //        {
                    //            Fl.WriteLine("vs_OCP:" + dr[19].ToString());
                    //            Fl.WriteLine("OCP Measurment:" + dr[20].ToString());
                    //        }
                    //    }
                    //    if (tchName == "CHA" || tchName == "All")
                    //    {
                    //        Fl.WriteLine("\n");
                    //        Fl.WriteLine("Technic: CHA");
                    //        //Fl.WriteLine("TchNo: " + CA.ToString());
                    //        Fl.WriteLine("E1:" + dr[3].ToString());
                    //        if (typedevice == 1)
                    //            Fl.WriteLine("E2:" + dr[4].ToString());
                    //        Fl.WriteLine("Equilibrium Time:" + dr[7].ToString());
                    //        if (typedevice == 1)
                    //            Fl.WriteLine("T1:" + double.Parse(dr[15].ToString()) * 1000);
                    //        else
                    //            Fl.WriteLine("T1:" + double.Parse(dr[15].ToString()));

                    //        if (typedevice == 1)
                    //            Fl.WriteLine("T2:" + double.Parse(dr[16].ToString()) * 1000);
                    //        Fl.WriteLine("Cycles:" + "1"); //caprms.Cycles.ToString());
                    //        if (typedevice == 2)
                    //        {
                    //            Fl.WriteLine("vs_OCP:" + dr[19].ToString());
                    //            Fl.WriteLine("OCP Measurment:" + dr[20].ToString());

                    //        }

                    //    }
                    //    if (tchName == "CHC" || tchName == "All")
                    //    {
                    //        Fl.WriteLine("\n");
                    //        Fl.WriteLine("Technic: CHC");
                    //        //Fl.WriteLine("TchNo: " + CA.ToString());
                    //        Fl.WriteLine("E1:" + dr[3].ToString());
                    //        Fl.WriteLine("E2:" + dr[4].ToString());
                    //        Fl.WriteLine("Equilibrium Time:" + dr[7].ToString());
                    //        Fl.WriteLine("T1:" + double.Parse(dr[15].ToString()) * 1000);
                    //        Fl.WriteLine("T2:" + double.Parse(dr[16].ToString()) * 1000);
                    //        Fl.WriteLine("Cycles:" + "1"); //caprms.Cycles.ToString());
                    //        if (typedevice == 2)
                    //        {
                    //            Fl.WriteLine("vs_OCP:" + dr[19].ToString());
                    //            Fl.WriteLine("OCP Measurment:" + dr[20].ToString());

                    //        }


                    //    }
                    //    /*
                    //    if (tchName == "SC" || tchName == "All")
                    //    {
                    //        Fl.WriteLine("\n");
                    //        Fl.WriteLine("Technic: SC");
                    //        //Fl.WriteLine("TchNo: " + SC.ToString());
                    //        Fl.WriteLine("E1:" + scprms.E1.ToString());
                    //        Fl.WriteLine("Q1:" + scprms.Q1.ToString());
                    //        Fl.WriteLine("E2:" + scprms.E2.ToString());
                    //        Fl.WriteLine("Q2:" + scprms.Q2.ToString());
                    //        Fl.WriteLine("Equilibrium Time:" + scprms.EquilibriumTime.ToString());
                    //        Fl.WriteLine("Cycles:" + scprms.Cycles.ToString());
                    //        Fl.WriteLine("OCPMeas.:" + scprms.OCPmeasurment.ToString());
                    //        Fl.WriteLine("vs_OCP:" + (Convert.ToByte(scprms.vs_OCP)).ToString());
                    //    }
                    //     * */
                    //    Fl.Close();
                    //}
                    //Fl.Close();
            }

            header += "============================================================\n";

            return header;
        }

    }
}
