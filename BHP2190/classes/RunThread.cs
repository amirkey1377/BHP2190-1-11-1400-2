using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BHP2190.classes;

namespace BHP2190
{
    class RunThread
    {
        /*
        public frm_main mf = new frm_main();
        public void thread_Show_Data()
        {
            mf.new_numSeries = mf.grf.num_Series;
            //System.Windows.Forms.Timer ti = new System.Windows.Forms.Timer();
            int num_Buffer_Bytes = 0;
            char[] bf = new char[mf.sp1.ReadBufferSize];
            mf.Row_Data = 0;
            for (int ii = 0; ii < 65536; ii++)
            {
                mf.grf.dataE[mf.grf.num_Series, ii] = 0;
                mf.grf.dataI1[mf.grf.num_Series, ii] = 0;
                mf.grf.dataI2[mf.grf.num_Series, ii] = 0;
            }
            bool flag = true;
            //textBox1.Text = sp1.ReadExisting();
            string endstr = "";
            int hh = 0;
            if (mf.sp1.IsOpen)
            {
                //ti.Interval = sp1.ReadTimeout;
                while (mf.sp1.BytesToRead < 20 && hh < 5)
                {
                    System.Threading.Thread.Sleep(500);
                    hh++;
                    if (mf.sp1.BytesToRead >= 20) break;
                }
                //while (sp1.BytesToRead < 20 && hh < 200000) hh++;
                mf.sp1.Read(bf, 0, num_Buffer_Bytes = mf.sp1.BytesToRead);

                int i_h = 0;
                while (bf[i_h] != '$' && i_h < num_Buffer_Bytes) i_h++;
                mf.toolStripProgressBar1.Visible = true;
                mf.xyLabel.Visible = false;
                mf.toolStripProgressBar1.Maximum = 50;
                while (flag)    //&& num_Buffer_Bytes > 0
                {
                    while (i_h < num_Buffer_Bytes)
                    {
                        endstr = endstr + bf[i_h];
                        i_h++;
                        while (bf[i_h] != '$' && i_h < num_Buffer_Bytes)
                        {
                            endstr = endstr + bf[i_h];
                            i_h++;
                        }
                        if (i_h < num_Buffer_Bytes - 2 && bf[i_h + 2] == '$' && endstr.Length > 13)
                        {
                            endstr = endstr + "$#";
                            i_h += 2;
                        }

                        if (endstr.IndexOf("END") < 0 && endstr.IndexOf("DEN") < 0)  // && endstr.length < 15
                        {
                            if (endstr.Length > 16)
                            {
                                mf.Calc_V_I1_I2(endstr, mf.ShowOnline);
                                endstr = "";
                            }
                        }
                        else
                        {
                            if (endstr.Length > 15 && (endstr.IndexOf("END") > 15 || endstr.IndexOf("DEN") > 15))
                            {
                                mf.Calc_V_I1_I2(endstr, mf.ShowOnline);

                                if (mf.cycles > 1 && endstr.IndexOf("ENDE") < 0 && endstr.IndexOf("DEND") < 0)
                                    mf.Set_Graph(mf.grf, 0);
                            }
                            if (endstr.Length > 15 && (endstr.IndexOf("ENDE") > 15 || endstr.IndexOf("DEND") > 15))
                            {
                                mf.Calc_V_I1_I2(endstr, mf.ShowOnline);

                                endstr = "";
                                flag = false;
                                endstr = "";
                                mf.Row_Data = 0;
                            }
                            endstr = "";
                        }
                    }

                    if (num_Buffer_Bytes >= 0 && flag)
                    {
                        hh = 0;
                        //ti.Start();
                        //while (sp1.BytesToRead < 5 && hh < 5)
                        //{
                            //System.Threading.Thread.Sleep(300);
                            //hh++;
                            //if (sp1.BytesToRead >= 5) break;
                        //}
                        if (mf.cycles == 1)
                            while (mf.sp1.BytesToRead < 5 && hh < 500000) { hh++; if (hh % 2000 == 0) mf.Refresh(); }
                        if (mf.cycles > 1)
                            while (mf.sp1.BytesToRead < 5 && hh < 8000000) { hh++; if (hh % 2000 == 0) mf.Refresh(); }
                        mf.sp1.Read(bf, 0, num_Buffer_Bytes = mf.sp1.BytesToRead);
                        if (num_Buffer_Bytes == 0)
                        {
                            flag = false;
                            mf.flag_running = false;
                            mf.cbOverlay.Checked = mf.isOverlay;
                            EventArgs e = new EventArgs();
                            mf.toolStripButton15_Click(mf.toolStripButton15, e);
                            mf.toolStripButton4.Enabled = true;
                            mf.toolStripButton5.Enabled = false;
                            mf.toolStripButton6.Enabled = false;
                            mf.toolStripMenuItem7.Enabled = false;
                            mf.toolStripMenuItem8.Enabled = false;
                            mf.runToolStripMenuItem.Enabled = true;
                            mf.pausePlayToolStripMenuItem.Enabled = false;
                            mf.stopToolStripMenuItem.Enabled = false;
                        }
                        i_h = 0;
                    }
                    //}
                }
                mf.toolStripProgressBar1.Visible = false;
                mf.xyLabel.Visible = true;
            }
            //thread_grf = new Thread(new ThreadStart(doRun));
        //thread_grf.Start();
        //grf.Close();
        //grf.Show();
        //thread_Run.Name = "Running";
        //thread_grf.Priority = ThreadPriority.Normal;
        //Thread_ID = thread_grf.ManagedThreadId;
        }
*/
    }

}
