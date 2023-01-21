
using BHP2190.classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BHP2190.classes
{
    public class class_file
    {
        public void Read_default_value_FromFile(string tech)//فراخوانی اطلاعات تکنیک ها و پارامترها از فایل پروژه
        {
            try
            {
                string s = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Prms.prj";

                StreamReader Fl = new StreamReader(s, Encoding.ASCII);
                s = "";

                while (!Fl.EndOfStream)
                {
                    s = Fl.ReadLine();
                    if (s.LastIndexOf("Technic") >= 0)
                    {
                        if (s.Substring(9) != tech) continue;

                        if ((s.Substring(9) == "CV") && s.LastIndexOf("Technic") >= 0 && s != "")
                        {
                            try
                            {
                                s = Fl.ReadLine();
                                s = Fl.ReadLine(); classglobal.cv_cycle = s.Substring(7);
                                s = Fl.ReadLine(); classglobal.cv_e1 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.cv_e2 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.cv_e3 = s.Substring(3);
                                s = Fl.ReadLine();
                                s = Fl.ReadLine(); classglobal.cv_hs = s.Substring(6);
                                s = Fl.ReadLine(); classglobal.cv_eq = s.Substring(17);
                                s = Fl.ReadLine(); classglobal.cv_sr = s.Substring(10);
                                s = Fl.ReadLine();
                                s = Fl.ReadLine(); classglobal.cv_ts = s.Substring(6);
                                s = Fl.ReadLine();
                                if (s.Trim().Length >= 7)
                                {
                                    if (s.Trim().Substring(0, 7) == "comment")
                                    {
                                        classglobal.cv_com = s.Substring(8);
                                    }
                                    else
                                        classglobal.cv_com = "";

                                }
                                else
                                {
                                    classglobal.cv_com = "";
                                }
                                s = "            ";
                            }
                            catch { }
                            break;
                        }

                        if ((s.Substring(9) == "OCP") && s.LastIndexOf("Technic") >= 0 && s != "")
                        {
                            try
                            {
                                s = Fl.ReadLine();
                                s = Fl.ReadLine(); classglobal.ocp_time = s.Substring(5);
                                s = "            ";
                            }
                            catch { }
                            break;
                        }

                        if ((s.Substring(9) == "DCV") && s.LastIndexOf("Technic") >= 0 && s != "")
                        {
                            try
                            {
                                s = Fl.ReadLine();
                                s = Fl.ReadLine(); classglobal.dcv_e1 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.dcv_e2 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.dcv_hs = s.Substring(6);
                                s = Fl.ReadLine(); classglobal.dcv_eq = s.Substring(17);
                                s = Fl.ReadLine(); classglobal.dcv_sr = s.Substring(10);
                                s = Fl.ReadLine(); classglobal.dcv_ts = s.Substring(6);
                                s = Fl.ReadLine();
                                if (s.Trim().Length >= 7)
                                {
                                    if (s.Trim().Substring(0, 7) == "comment")
                                    {
                                        classglobal.dcv_com = s.Substring(8);
                                    }
                                    else
                                        classglobal.dcv_com = "";
                                }
                                else
                                {
                                    classglobal.dcv_com = "";
                                }
                                s = "            ";
                            }
                            catch { }
                            break;
                        }

                        if ((s.Substring(9) == "NPV") && s.LastIndexOf("Technic") >= 0 && s != "")
                        {
                            try
                            {
                                s = Fl.ReadLine();
                                s = Fl.ReadLine(); classglobal.npv_e1 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.npv_e2 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.npv_hs = s.Substring(6);
                                s = Fl.ReadLine(); classglobal.npv_pw = s.Substring(12);
                                s = Fl.ReadLine(); classglobal.npv_eq = s.Substring(17);
                                s = Fl.ReadLine(); classglobal.npv_sr = s.Substring(10);
                                s = Fl.ReadLine(); classglobal.npv_ts = s.Substring(6);
                                s = Fl.ReadLine();
                                if (s.Trim().Length >= 7)
                                {
                                    if (s.Trim().Substring(0, 7) == "comment")
                                    {
                                        classglobal.npv_com = s.Substring(8);
                                    }
                                    else
                                        classglobal.npv_com = "";
                                }
                                else
                                {
                                    classglobal.npv_com = "";
                                }
                                s = "            ";
                            }
                            catch { }
                            break;
                        }

                        if ((s.Substring(9) == "DPV") && s.LastIndexOf("Technic") >= 0 && s != "")
                        {
                            try
                            {
                                s = Fl.ReadLine();
                                s = Fl.ReadLine(); classglobal.dpv_e1 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.dpv_e2 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.dpv_hs = s.Substring(6);
                                s = Fl.ReadLine(); classglobal.dpv_ph = s.Substring(13);
                                s = Fl.ReadLine(); classglobal.dpv_pw = s.Substring(12);
                                s = Fl.ReadLine(); classglobal.dpv_eq = s.Substring(17);
                                s = Fl.ReadLine(); classglobal.dpv_sr = s.Substring(10);
                                s = Fl.ReadLine(); classglobal.dpv_ts = s.Substring(6);
                                s = Fl.ReadLine();
                                if (s.Trim().Length >= 7)
                                {
                                    if (s.Trim().Substring(0, 7) == "comment")
                                    {
                                        classglobal.dpv_com = s.Substring(8);
                                    }
                                    else
                                        classglobal.dpv_com = "";

                                }
                                else
                                {
                                    classglobal.dpv_com = "";
                                }
                                s = "            ";
                            }
                            catch { }
                        }

                        if ((s.Substring(9) == "SWV") && s.LastIndexOf("Technic") >= 0 && s != "")
                        {
                            try
                            {
                                s = Fl.ReadLine();
                                s = Fl.ReadLine(); classglobal.swv_e1 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.swv_e2 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.swv_fr = s.Substring(10);
                                s = Fl.ReadLine(); classglobal.swv_hs = s.Substring(6);
                                s = Fl.ReadLine(); classglobal.swv_ph = s.Substring(13);
                                s = Fl.ReadLine(); classglobal.swv_eq = s.Substring(17);
                                s = Fl.ReadLine(); classglobal.swv_sr = s.Substring(10);
                                s = Fl.ReadLine();
                                if (s.Trim().Length >= 7)
                                {
                                    if (s.Trim().Substring(0, 7) == "comment")
                                    {
                                        classglobal.swv_com = s.Substring(8);
                                    }
                                    else
                                        classglobal.swv_com = "";
                                }
                                else
                                {
                                    classglobal.swv_com = "";
                                }
                                s = "            ";
                            }
                            catch { }
                            break;
                        }

                        if ((s.Substring(9) == "LSV") && s.LastIndexOf("Technic") >= 0 && s != "")
                        {
                            try
                            {
                                s = Fl.ReadLine();
                                s = Fl.ReadLine(); classglobal.lsv_e1 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.lsv_e2 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.lsv_hs = s.Substring(6);
                                s = Fl.ReadLine(); classglobal.lsv_eq = s.Substring(17);
                                s = Fl.ReadLine(); classglobal.lsv_sr = s.Substring(10);
                                s = Fl.ReadLine(); classglobal.lsv_ts = s.Substring(6);
                                s = Fl.ReadLine();
                                if (s.Trim().Length >= 7)
                                {
                                    if (s.Trim().Substring(0, 7) == "comment")
                                    {
                                        classglobal.lsv_com = s.Substring(8);
                                    }
                                    else
                                        classglobal.lsv_com = "";

                                }
                                else
                                {
                                    classglobal.lsv_com = "";
                                }
                                s = "            ";
                            }
                            catch { }
                        }

                        if ((s.Substring(9) == "DCS") && s.LastIndexOf("Technic") >= 0 && s != "")
                        {
                            try
                            {
                                s = Fl.ReadLine();
                                s = Fl.ReadLine(); classglobal.dcs_e1 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.dcs_e2 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.dcs_hs = s.Substring(6);
                                s = Fl.ReadLine(); classglobal.dcs_eq = s.Substring(17);
                                s = Fl.ReadLine(); classglobal.dcs_sr = s.Substring(10);
                                s = Fl.ReadLine(); classglobal.dcs_ts = s.Substring(6);
                                s = Fl.ReadLine();
                                if (s.Trim().Length >= 7)
                                {
                                    if (s.Trim().Substring(0, 7) == "comment")
                                    {
                                        classglobal.dcv_com = s.Substring(8);
                                    }
                                    else
                                        classglobal.dcv_com = "";

                                }
                                else
                                {
                                    classglobal.dcv_com = "";
                                }
                                s = "            ";
                            }
                            catch { }
                            break;
                        }

                        if ((s.Substring(9) == "DPS") && s.LastIndexOf("Technic") >= 0 && s != "")
                        {
                            try
                            {
                                s = Fl.ReadLine();
                                s = Fl.ReadLine(); classglobal.dps_e1 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.dps_e2 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.dps_hs = s.Substring(6);
                                s = Fl.ReadLine(); classglobal.dps_ph = s.Substring(13);
                                s = Fl.ReadLine(); classglobal.dps_pw = s.Substring(12);
                                s = Fl.ReadLine(); classglobal.dps_eq = s.Substring(17);
                                s = Fl.ReadLine(); classglobal.dps_sr = s.Substring(10);
                                s = Fl.ReadLine(); classglobal.dps_ts = s.Substring(6);
                                s = Fl.ReadLine();
                                if (s.Trim().Length >= 7)
                                {
                                    if (s.Trim().Substring(0, 7) == "comment")
                                    {
                                        classglobal.dps_com = s.Substring(8);
                                    }
                                    else
                                        classglobal.dps_com = "";
                                }
                                else
                                {
                                    classglobal.dps_com = "";
                                }
                                s = "            ";
                            }
                            catch { }
                            break;
                        }

                        if ((s.Substring(9) == "CPC") && s.LastIndexOf("Technic") >= 0 && s != "")
                        {
                            try
                            {
                                s = Fl.ReadLine();
                                s = Fl.ReadLine(); classglobal.cpc_e1 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.cpc_eq = s.Substring(17);
                                s = Fl.ReadLine(); classglobal.cpc_t1 = s.Substring(3);
                                s = Fl.ReadLine();
                                s = Fl.ReadLine();
                                if (s.Trim().Length >= 7)
                                {
                                    if (s.Trim().Substring(0, 7) == "comment")
                                    {
                                        classglobal.cpc_com = s.Substring(8);
                                    }
                                    else
                                        classglobal.cpc_com = "";

                                }
                                else
                                {
                                    classglobal.cpc_com = "";
                                }
                                s = "            ";
                            }
                            catch { }
                            break;
                        }

                        if ((s.Substring(9) == "CHP") && s.LastIndexOf("Technic") >= 0 && s != "")
                        {
                            try
                            {
                                s = Fl.ReadLine(); classglobal.chp_i1 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.chp_i2 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.chp_eq = s.Substring(17);
                                s = Fl.ReadLine(); classglobal.chp_t1 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.chp_t2 = s.Substring(3);
                                s = Fl.ReadLine();
                                s = Fl.ReadLine();
                                if (s.Trim().Length >= 7)
                                {
                                    if (s.Trim().Substring(0, 7) == "comment")
                                    {
                                        classglobal.chp_com = s.Substring(8);
                                    }
                                    else
                                        classglobal.chp_com = "";
                                }
                                else
                                {
                                    classglobal.chp_com = "";
                                }
                                s = "            ";
                            }
                            catch { }
                            break;
                        }

                        if ((s.Substring(9) == "CHA") && s.LastIndexOf("Technic") >= 0 && s != "")
                        {
                            try
                            {
                                s = Fl.ReadLine(); classglobal.cha_e1 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.cha_e2 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.cha_eq = s.Substring(17);
                                s = Fl.ReadLine(); classglobal.cha_t1 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.cha_t2 = s.Substring(3);
                                s = Fl.ReadLine();
                                s = Fl.ReadLine();
                                if (s.Trim().Length >= 7)
                                {
                                    if (s.Trim().Substring(0, 7) == "comment")
                                    {
                                        classglobal.cha_com = s.Substring(8);
                                    }
                                    else
                                        classglobal.cha_com = "";

                                }
                                else
                                {
                                    classglobal.cha_com = "";
                                }
                                s = "            ";
                            }
                            catch { }
                            break;
                        }

                        if ((s.Substring(9) == "CHC") && s.LastIndexOf("Technic") >= 0 && s != "")
                        {
                            try
                            {
                                s = Fl.ReadLine(); classglobal.chc_e1 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.chc_e2 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.chc_eq = s.Substring(17);
                                s = Fl.ReadLine(); classglobal.chc_t1 = s.Substring(3);
                                s = Fl.ReadLine(); classglobal.chc_t2 = s.Substring(3);
                                s = Fl.ReadLine();
                                s = Fl.ReadLine();
                                if ((s is DBNull) || s.Trim().Length < 7)
                                {
                                    classglobal.chc_com = "";
                                }
                                else
                                {
                                    if (s.Trim().Substring(0, 7) == "comment")
                                    {
                                        classglobal.chc_com = s.Substring(8);
                                    }
                                    else
                                        classglobal.chc_com = "";
                                }
                                s = "            ";
                            }
                            catch { }
                            break;
                        }
                    }
                }

                Fl.Close();
            }
            catch { }
        }

        public static void save_project(DataSet ds)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.DefaultExt = "bhp";
                sfd.Filter = "Data Files(*.bhp)|*.bhp";
                sfd.ShowDialog();
                if (sfd.FileName != "")
                {
                    string file_name = sfd.FileName;

                    StreamWriter Fl = new StreamWriter(file_name, false, Encoding.ASCII);

                    for (int iii = 0; iii < ds.Tables["chartlist"].Rows.Count; iii++)
                    {
                        switch (int.Parse(ds.Tables["chartlist"].Rows[iii][1].ToString()))
                        {
                            case 0://cv
                                Fl.WriteLine("\n");
                                Fl.WriteLine("Technic: CV");
                                Fl.WriteLine("Current Range:0");
                                Fl.WriteLine("Cycles:" + ds.Tables["chartlist"].Rows[iii][13].ToString());
                                Fl.WriteLine("E1:" + ds.Tables["chartlist"].Rows[iii][3].ToString());
                                Fl.WriteLine("E2:" + ds.Tables["chartlist"].Rows[iii][4].ToString());
                                Fl.WriteLine("E3:" + ds.Tables["chartlist"].Rows[iii][5].ToString());
                                Fl.WriteLine("Hold Time:" + ds.Tables["chartlist"].Rows[iii][14].ToString());
                                Fl.WriteLine("HStep:" + ds.Tables["chartlist"].Rows[iii][6].ToString());
                                Fl.WriteLine("Equilibrium Time:" + ds.Tables["chartlist"].Rows[iii][7].ToString());
                                Fl.WriteLine("Scan Rate:" + ds.Tables["chartlist"].Rows[iii][8].ToString());
                                Fl.WriteLine("ScanRate_R:" + ds.Tables["chartlist"].Rows[iii][8].ToString());
                                Fl.WriteLine("TStep:" + ds.Tables["chartlist"].Rows[iii][9].ToString());
                                Fl.WriteLine("comment:" + ds.Tables["chartlist"].Rows[iii]["comment"].ToString());
                                break;

                            //case 0:
                            //    Fl.WriteLine("\n");
                            //    Fl.WriteLine("Technic: DCV");
                            //    //Fl.WriteLine("TchNo: "+ DCV.ToString());
                            //    Fl.WriteLine("Current Range:0");
                            //    Fl.WriteLine("E1:" + double.Parse(dataset1.chartlist.Rows[iii][3].ToString()));
                            //    Fl.WriteLine("E2:" + double.Parse(dataset1.chartlist.Rows[iii][4].ToString()));
                            //    Fl.WriteLine("HStep:" + double.Parse(dataset1.chartlist.Rows[iii][6].ToString()));
                            //    Fl.WriteLine("Equilibrium Time:" + double.Parse(dataset1.chartlist.Rows[iii][7].ToString()));
                            //    Fl.WriteLine("Scan Rate:" + double.Parse(dataset1.chartlist.Rows[iii][8].ToString()));
                            //    Fl.WriteLine("TStep:" + double.Parse(dataset1.chartlist.Rows[iii][9].ToString()));
                            //    Fl.WriteLine("comment:" + dataset1.chartlist.Rows[iii]["comment"].ToString());
                            //    break;


                            case 1://ocp
                                Fl.WriteLine("\n");
                                Fl.WriteLine("Technic: OCP");
                                Fl.WriteLine("Cycles:" + ds.Tables["chartlist"].Rows[iii][13].ToString());
                                Fl.WriteLine("Time:" + ds.Tables["chartlist"].Rows[iii][15].ToString());
                                break;

                            case 3://npv
                                Fl.WriteLine("\n");
                                Fl.WriteLine("Technic: NPV");
                                Fl.WriteLine("Current Range:0");
                                Fl.WriteLine("Cycles:" + ds.Tables["chartlist"].Rows[iii][13].ToString());
                                Fl.WriteLine("E1:" + ds.Tables["chartlist"].Rows[iii][3].ToString());
                                Fl.WriteLine("E2:" + ds.Tables["chartlist"].Rows[iii][4].ToString());
                                Fl.WriteLine("HStep:" + ds.Tables["chartlist"].Rows[iii][6].ToString());
                                Fl.WriteLine("TStep:" + ds.Tables["chartlist"].Rows[iii][9].ToString());
                                Fl.WriteLine("Pulse width:" + ds.Tables["chartlist"].Rows[iii][10].ToString());
                                Fl.WriteLine("Equilibrium Time:" + ds.Tables["chartlist"].Rows[iii][7].ToString());
                                Fl.WriteLine("comment:" + ds.Tables["chartlist"].Rows[iii]["comment"].ToString());
                                break;

                            case 4://dpv
                                Fl.WriteLine("\n");
                                Fl.WriteLine("Technic: DPV");
                                Fl.WriteLine("Current Range:0");
                                Fl.WriteLine("Cycles:" + ds.Tables["chartlist"].Rows[iii][13].ToString());
                                Fl.WriteLine("E1:" + ds.Tables["chartlist"].Rows[iii][3].ToString());
                                Fl.WriteLine("E2:" + ds.Tables["chartlist"].Rows[iii][4].ToString());
                                Fl.WriteLine("HStep:" + ds.Tables["chartlist"].Rows[iii][6].ToString());
                                Fl.WriteLine("TStep:" + ds.Tables["chartlist"].Rows[iii][9].ToString());
                                Fl.WriteLine("Pulse width:" + ds.Tables["chartlist"].Rows[iii][10].ToString());
                                Fl.WriteLine("Scan Rate:" + ds.Tables["chartlist"].Rows[iii][8].ToString());
                                Fl.WriteLine("Pulse Height:" + ds.Tables["chartlist"].Rows[iii][11].ToString());
                                Fl.WriteLine("Equilibrium Time:" + ds.Tables["chartlist"].Rows[iii][7].ToString());
                                Fl.WriteLine("comment:" + ds.Tables["chartlist"].Rows[iii]["comment"].ToString());
                                break;

                            case 5://swv
                                Fl.WriteLine("\n");
                                Fl.WriteLine("Technic: SWV");
                                Fl.WriteLine("Current Range:0");
                                Fl.WriteLine("Cycles:" + ds.Tables["chartlist"].Rows[iii][13].ToString());
                                Fl.WriteLine("E1:" + ds.Tables["chartlist"].Rows[iii][3].ToString());
                                Fl.WriteLine("E2:" + ds.Tables["chartlist"].Rows[iii][4].ToString());
                                Fl.WriteLine("HStep:" + ds.Tables["chartlist"].Rows[iii][6].ToString());
                                Fl.WriteLine("Frequency:" + ds.Tables["chartlist"].Rows[iii][12].ToString());
                                Fl.WriteLine("Scan Rate:" + ds.Tables["chartlist"].Rows[iii][8].ToString());
                                Fl.WriteLine("Pulse Height:" + ds.Tables["chartlist"].Rows[iii][11].ToString());
                                Fl.WriteLine("Equilibrium Time:" + ds.Tables["chartlist"].Rows[iii][7].ToString());
                                Fl.WriteLine("comment:" + ds.Tables["chartlist"].Rows[iii]["comment"].ToString());
                                break;

                            case 6://lsv
                                Fl.WriteLine("\n");
                                Fl.WriteLine("Technic: LSV");
                                Fl.WriteLine("Current Range:0");
                                Fl.WriteLine("Cycles:" + ds.Tables["chartlist"].Rows[iii][13].ToString());
                                Fl.WriteLine("E1:" + ds.Tables["chartlist"].Rows[iii][3].ToString());
                                Fl.WriteLine("E2:" + ds.Tables["chartlist"].Rows[iii][4].ToString());
                                Fl.WriteLine("HStep:" + ds.Tables["chartlist"].Rows[iii][6].ToString());
                                Fl.WriteLine("TStep:" + ds.Tables["chartlist"].Rows[iii][9].ToString());
                                Fl.WriteLine("Equilibrium Time:" + ds.Tables["chartlist"].Rows[iii][7].ToString());
                                Fl.WriteLine("comment:" + ds.Tables["chartlist"].Rows[iii]["comment"].ToString());
                                break;

                            case 9://cpc
                                Fl.WriteLine("\n");
                                Fl.WriteLine("Technic: CPC");
                                Fl.WriteLine("Cycles:" + ds.Tables["chartlist"].Rows[iii][13].ToString());
                                Fl.WriteLine("E1:" + ds.Tables["chartlist"].Rows[iii][3].ToString());
                                Fl.WriteLine("Time:" + ds.Tables["chartlist"].Rows[iii][15].ToString());
                                break;

                            case 11://cha
                                Fl.WriteLine("\n");
                                Fl.WriteLine("Technic: CHA");
                                Fl.WriteLine("Cycles:" + ds.Tables["chartlist"].Rows[iii][13].ToString());
                                Fl.WriteLine("E1:" + ds.Tables["chartlist"].Rows[iii][3].ToString());
                                Fl.WriteLine("E2:" + ds.Tables["chartlist"].Rows[iii][4].ToString());
                                Fl.WriteLine("Time1:" + ds.Tables["chartlist"].Rows[iii][15].ToString());
                                Fl.WriteLine("Time2:" + ds.Tables["chartlist"].Rows[iii][16].ToString());
                                break;

                        }
                    }
                    Fl.Close();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("save project: " + err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static DataTable open_project(string file_name)
        {
            DataTable dt = new DataTable();

            for (int i = 0; i < 25; i++)
                dt.Columns.Add(i.ToString());

            try
            {
                int row = 0;

                string s = "";

                StreamReader Fl = new StreamReader(file_name);
                while (!Fl.EndOfStream)
                {
                    s = Fl.ReadLine();
                    // MessageBox.Show("s=" + s.ToString());
                    if (s.LastIndexOf("Technic") >= 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = row.ToString();

                        //if ((s.Substring(9) == "DCV") && s.LastIndexOf("Technic") >= 0 && s != "")
                        //    try
                        //    {
                        //        this.mainf.grf.tech = clasglobal.DCV;
                        //        s = Fl.ReadLine(); //clasglobal.dcvprms.CurrentRange = Convert.ToDouble(s.Substring(13));
                        //        s = Fl.ReadLine(); clasglobal.dcvprms.E1 = Convert.ToDouble(s.Substring(3));
                        //        s = Fl.ReadLine(); clasglobal.dcvprms.E2 = Convert.ToDouble(s.Substring(3));
                        //        s = Fl.ReadLine(); clasglobal.dcvprms.HStep = Convert.ToDouble(s.Substring(6));
                        //        s = Fl.ReadLine(); clasglobal.dcvprms.EquilibriumTime = Convert.ToDouble(s.Substring(17));
                        //        s = Fl.ReadLine(); clasglobal.dcvprms.ScanRate = Convert.ToDouble(s.Substring(10));
                        //        s = Fl.ReadLine(); clasglobal.dcvprms.TStep = Convert.ToDouble(s.Substring(6));
                        //        s = "            ";
                        //    }
                        //    catch { }
                        switch (s.Substring(9))
                        {
                            case "CV":
                                s = Fl.ReadLine(); //string range = s.Substring(13);
                                dr[1] = 0;//tech
                                s = Fl.ReadLine(); dr[13] = s.Substring(7);//cycle
                                s = Fl.ReadLine(); dr[3] = s.Substring(3);//e1
                                s = Fl.ReadLine(); dr[4] = s.Substring(3);//e2
                                s = Fl.ReadLine(); dr[5] = s.Substring(3);//e3
                                s = Fl.ReadLine(); dr[14] = s.Substring(10);//hold time
                                s = Fl.ReadLine(); dr[6] = s.Substring(6);//hstep
                                s = Fl.ReadLine(); dr[7] = s.Substring(17);//equ... time
                                s = Fl.ReadLine(); dr[8] = s.Substring(10);//ScanRate
                                s = Fl.ReadLine(); //string ScanRate_R = s.Substring(11);
                                s = Fl.ReadLine(); dr[9] = s.Substring(6);//tstep
                                s = Fl.ReadLine(); //comment
                                dt.Rows.Add(dr);
                                break;

                            case "OCP":
                                s = Fl.ReadLine(); //string range = s.Substring(13);
                                dr[1] = 1;//tech
                                s = Fl.ReadLine(); dr[13] = s.Substring(7);//cycle
                                s = Fl.ReadLine(); dr[15] = s.Substring(5);//time  /T1
                                dt.Rows.Add(dr);
                                break;

                            case "NPV":
                                s = Fl.ReadLine(); //string range = s.Substring(13);
                                dr[1] = 3;//tech 
                                s = Fl.ReadLine(); dr[13] = s.Substring(7);//cycle
                                s = Fl.ReadLine(); dr[3] = s.Substring(3);//e1
                                s = Fl.ReadLine(); dr[4] = s.Substring(3);//e2
                                s = Fl.ReadLine(); dr[6] = s.Substring(6);//hstep
                                s = Fl.ReadLine(); dr[9] = s.Substring(6);//tstep
                                s = Fl.ReadLine(); dr[10] = s.Substring(12);//pulse width
                                s = Fl.ReadLine(); dr[7] = s.Substring(17);//equ... time
                                s = Fl.ReadLine();//comment
                                dt.Rows.Add(dr);
                                break;

                            case "DPV":
                                s = Fl.ReadLine(); //string range = s.Substring(13);
                                dr[1] = 4;//tech 
                                s = Fl.ReadLine(); dr[13] = s.Substring(7);//cycle
                                s = Fl.ReadLine(); dr[3] = s.Substring(3);//e1
                                s = Fl.ReadLine(); dr[4] = s.Substring(3);//e2
                                s = Fl.ReadLine(); dr[6] = s.Substring(6);//hstep
                                s = Fl.ReadLine(); dr[9] = s.Substring(6);//tstep
                                s = Fl.ReadLine(); dr[10] = s.Substring(12);//pulse width
                                s = Fl.ReadLine(); dr[8] = s.Substring(10);//scan rate
                                s = Fl.ReadLine(); dr[11] = s.Substring(13);//pulse height
                                s = Fl.ReadLine(); dr[7] = s.Substring(17);//equ... time
                                s = Fl.ReadLine();//comment
                                dt.Rows.Add(dr);
                                break;

                            case "SWV":
                                s = Fl.ReadLine(); //string range = s.Substring(13);
                                dr[1] = 5;//tech 
                                s = Fl.ReadLine(); dr[13] = s.Substring(7);//cycle
                                s = Fl.ReadLine(); dr[3] = s.Substring(3);//e1
                                s = Fl.ReadLine(); dr[4] = s.Substring(3);//e2
                                s = Fl.ReadLine(); dr[6] = s.Substring(6);//hstep
                                s = Fl.ReadLine(); dr[12] = s.Substring(10);//frequency
                                s = Fl.ReadLine(); dr[8] = s.Substring(10);//scan rate
                                s = Fl.ReadLine(); dr[11] = s.Substring(13);//pulse height
                                s = Fl.ReadLine(); dr[7] = s.Substring(17);//equ... time
                                s = Fl.ReadLine();//comment
                                dt.Rows.Add(dr);
                                break;

                            case "LSV":
                                s = Fl.ReadLine(); //string range = s.Substring(13);
                                dr[1] = 6;//tech
                                s = Fl.ReadLine(); dr[13] = s.Substring(7);//cycle
                                s = Fl.ReadLine(); dr[3] = s.Substring(3);//e1
                                s = Fl.ReadLine(); dr[4] = s.Substring(3);//e2
                                s = Fl.ReadLine(); dr[6] = s.Substring(6);//hstep
                                s = Fl.ReadLine(); dr[9] = s.Substring(6);//tstep
                                s = Fl.ReadLine(); dr[7] = s.Substring(17);//equ... time
                                s = Fl.ReadLine(); //string ScanRate_R = s.Substring(11);
                                s = Fl.ReadLine();//comment
                                dt.Rows.Add(dr);
                                break;

                            case "CPC":
                                s = Fl.ReadLine(); //string range = s.Substring(13);
                                dr[1] = 9;//tech
                                s = Fl.ReadLine(); dr[13] = s.Substring(7);//cycle
                                s = Fl.ReadLine(); dr[3] = s.Substring(3);//e1
                                s = Fl.ReadLine(); dr[15] = s.Substring(5);//t1
                                dt.Rows.Add(dr);
                                break;


                            case "CHA":
                                s = Fl.ReadLine(); //string range = s.Substring(13);
                                dr[1] = 11;//tech
                                s = Fl.ReadLine(); dr[13] = s.Substring(7);//cycle
                                s = Fl.ReadLine(); dr[3] = s.Substring(3);//e1
                                s = Fl.ReadLine(); dr[4] = s.Substring(3);//e2
                                s = Fl.ReadLine(); dr[15] = s.Substring(6);//t1
                                s = Fl.ReadLine(); dr[16] = s.Substring(6);//t2
                                                                           //s = Fl.ReadLine(); dr[7] = s.Substring(17);//equ... time
                                dt.Rows.Add(dr);
                                break;


                        }


                        row++;
                    }
                }
                Fl.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show("Open project: This file not supported! \n" + err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }
    }
}

