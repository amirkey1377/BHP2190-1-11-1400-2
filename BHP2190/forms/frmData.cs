using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BHP2190.forms
{
    public partial class frmData : Form
    {
        public frmData()
        {
            InitializeComponent();
        }
        public int dfrno = 0;
        public frm_main mf;
        public frmOption opForm;

        private void DataForm_Load(object sender, EventArgs e)
        {
            /*for (int i = 0; i <= 100; i++)
            {
                mf.dataE[mf.grfNum, i] = i;
                mf.dataI1[mf.grfNum, i] = (2 * i * i) + (2 * i) - 10;
                mf.dataI2[mf.grfNum, i] = (-2 * Math.Sin(i) - 2 * Math.Cos(i));
            }*/
        }

        private void DataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //mf.shToolStripMenuItem.Checked = false;
            e.Cancel = true; // = (dfrno == 0);
            if (dfrno > 0)
                dfrno--;
            Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*if (checkBox1.Checked)
                grdData.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            else
                grdData.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            grdData.SelectAll();
            Clipboard.SetDataObject(grdData.GetClipboardContent());*/
        }

        private void checkBox1_MouseUp(object sender, MouseEventArgs e)
        {
            button1_Click(button1, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
//            string CD = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\cv.txt";
//            richTextBox1.LoadFile(CD);
        }

        private void grdData_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            /*try
            {
                if (grdData.Focused)
                {
                    mf.grf.Chart1.Graphics3D.Pen.Color = Color.Green;
                    int x = mf.grf.Chart1.Series[0].CalcXPosValue((double)grdData.Rows[e.RowIndex].Cells[1].Value);
                    int y = mf.grf.Chart1.Series[0].CalcYPosValue((double)grdData.Rows[e.RowIndex].Cells[2].Value);
                    mf.grf.Chart1.Graphics3D.FillRectangle(Brushes.Blue, x, y, 5, 5);
                }
            }
            catch { }*/
        }

       

       

       

              
    }
}