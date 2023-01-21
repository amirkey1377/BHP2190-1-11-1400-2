using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BHP2190.forms
{
    public partial class frmSmoothing : Form
    {
        //public MainForm mf; 
        public frmSmoothing()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Visible = radioButton1.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Visible = radioButton2.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {

           // mf.show_Graph(mf.dataE, mf.dataI1, mf.dataI2);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmSmoothing2065_Load(object sender, EventArgs e)
        {

        }
    }
}
