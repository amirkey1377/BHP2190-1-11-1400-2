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
    public partial class frmselecttypeshow : Form
    {

        //public clasMain mc = new clasMain();
        //public frmMain1 mf;
        public string type_txt_x = "", type_txt_y = "";
        public int filetype_select = 0;

        public frmselecttypeshow()
        {
            InitializeComponent();
           
        }
     
       



        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (txtdnumberx.Text.Trim() == "" || txtdnumbery.Text.Trim() == "")
                {
                    MessageBox.Show("please fill decimal place");
                    return;
                }
                if (rdbgeneralx.Checked)
                {
                    if (int.Parse(txtdnumberx.Text.Trim()) == 0)
                        type_txt_x = "#0";
                    else
                    {
                        type_txt_x = "#0.";
                        for (int i = 0; i < int.Parse(txtdnumberx.Text.Trim()); i++)
                        {
                            type_txt_x += "0";
                        }
                    }
                }
                else
                {
                    if (int.Parse(txtdnumberx.Text.Trim()) == 0)
                        type_txt_x = "0e+0";
                    else
                    {
                        type_txt_x = "0.";
                        for (int i = 0; i < int.Parse(txtdnumberx.Text.Trim()); i++)
                        {
                            type_txt_x += "0";
                        }
                        type_txt_x += "e+0";
                    }

                }
            }
            else
                type_txt_x = "";
            if (checkBox2.Checked)
            {
                if (rdbgeneraly.Checked)
                {
                    if (int.Parse(txtdnumbery.Text.Trim()) == 0)
                        type_txt_y = "#";
                    else
                    {
                        type_txt_y = "#0.";
                        for (int i = 0; i < int.Parse(txtdnumbery.Text.Trim()); i++)
                        {
                            type_txt_y += "#";
                        }
                    }
                }
                else
                {
                    if (int.Parse(txtdnumbery.Text.Trim()) == 0)
                        type_txt_y = "0e+0";
                    else
                    {
                        type_txt_y = "0.";
                        for (int i = 0; i < int.Parse(txtdnumbery.Text.Trim()); i++)
                        {
                            type_txt_y += "0";
                        }
                        type_txt_y += "e+0";
                    }

                }
            }
            else
                type_txt_y = "";
            this.DialogResult=DialogResult.OK;
            this.Close();
        }

        private void Frmselecttypeshow_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

     
       

    }
}
