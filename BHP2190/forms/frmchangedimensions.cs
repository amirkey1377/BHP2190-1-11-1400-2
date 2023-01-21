using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BHP2190.forms
{
    public partial class frmchangedimensions : Form
    {

        //public clasMain mc = new clasMain();
        //public frmMain1 mf;
        public double xmin, xmax, ymin, ymax;
        public int filetype_select = 0;

        public frmchangedimensions(double xmin1,double xmax1,double ymin1,double ymax1)
        {
            InitializeComponent();
            xmin = xmin1;
            ymin = ymin1;
            xmax = xmax1;
            ymax = ymax1;
        }
     
       



        private void button1_Click(object sender, EventArgs e)
        {
            if (txtxmin.Text.Trim() == "" || txtxmin.Text.Trim() == "+" || txtxmin.Text.Trim() == "-" || txtxmin.Text.Trim() == ".")
                txtxmin.Text = "0";
            if (txtymin.Text.Trim() == "" || txtymin.Text.Trim() == "+" || txtymin.Text.Trim() == "-" || txtymin.Text.Trim() == ".")
                txtymin.Text = "0";
            if (txtymax.Text.Trim() == "" || txtymax.Text.Trim() == "+" || txtymax.Text.Trim() == "-" || txtymax.Text.Trim() == ".")
                txtymax.Text = "0";
            if (txtxmax.Text.Trim() == "" || txtxmax.Text.Trim() == "+" || txtxmax.Text.Trim() == "-" || txtxmax.Text.Trim() == ".")
                txtxmax.Text = "0";
            xmin = double.Parse(txtxmin.Text.Trim());
            xmax = double.Parse(txtxmax.Text.Trim());
            ymin = double.Parse(txtymin.Text.Trim());
            ymax= double.Parse(txtymax.Text.Trim());


            this.DialogResult=DialogResult.OK;
            this.Close();
        }

        private void Frmchangedimensions_Load_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtxmin_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool statepress = true; ;
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8 || e.KeyChar == 9 || e.KeyChar == 13)
            {
                statepress = false;
            }
            else if (e.KeyChar == '+' || e.KeyChar == '-')
            {
                if (((TextBox)sender).Text.Trim() == "")
                    statepress = false;
            }           
            else if (e.KeyChar == '.')
            {
                if(! (((TextBox)sender).Text.IndexOf('.') >= 0) ){
                    statepress = false;

                }
            }
            e.Handled = statepress;

        }

        private void frmchangedimensions_Load(object sender, EventArgs e)
        {
            txtxmin.Text = xmin.ToString();
            txtymin.Text = ymin.ToString();
            txtymax.Text = ymax.ToString();
            txtxmax.Text = xmax.ToString(); 


        }

       

    }
}
