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
    public partial class frmMath : Form
    {

        //public clasMain mc = new clasMain();
        //public frmMain1 mf;
        public frmanalyseGraph2 gf;
        private int Graph_No_Math = 0;

        public frmMath()
        {
            InitializeComponent();
        }
        public frmMath(int i1)
        {
            InitializeComponent();
            Graph_No_Math = i1;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cmboperation1.Text == "Logarithm(Ln)")
                txtAdd.Text = Math.E.ToString();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (txtAdd.Text.Trim() == "")
            {
                MessageBox.Show("please enter value for operand");
                return;
            }

            int XorY = 1;
            if (!cbXVal.Checked && !cbYVal.Checked) XorY = -1;
            if (cbXVal.Checked) XorY = 0;
            if (cbYVal.Checked) XorY = 1;
            if (cbXVal.Checked && cbYVal.Checked) XorY = 2;
            switch (cmboperation1.Text.Trim())
            {
                case "Addition":
                    gf.mathematicf(XorY, "Add", Convert.ToDouble(txtAdd.Text), Graph_No_Math);
                    break;
                case "Subtraction":
                    gf.mathematicf(XorY, "Sub", Convert.ToDouble(txtAdd.Text), Graph_No_Math);
                    break;
                case "Multiplication":
                    gf.mathematicf(XorY, "Mul", Convert.ToDouble(txtAdd.Text), Graph_No_Math);
                    break;
                case "Division":
                    gf.mathematicf(XorY, "Div", Convert.ToDouble(txtAdd.Text), Graph_No_Math);
                    break;
                case "Exponential":
                    gf.mathematicf(XorY, "Exp", Convert.ToDouble(txtAdd.Text), Graph_No_Math);
                    break;
                case "Logarithm(Ln)":
                    gf.mathematicf(XorY, "Log", Convert.ToDouble(txtAdd.Text), Graph_No_Math);
                    break;
                case "Square":
                    gf.mathematicf(XorY, "Sqr", Convert.ToDouble(txtAdd.Text), Graph_No_Math);
                    break;
                case "SquareRoot":
                    gf.mathematicf(XorY, "Sqrt", Convert.ToDouble(txtAdd.Text), Graph_No_Math);
                    break;



            }
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                button1_Click(button1, e);
        }

        private void FrmMath12065_Load(object sender, EventArgs e)
        {

        }
    }
}
