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
    public partial class frm_copy_multi : Form
    {
        public frm_copy_multi()
        {
            InitializeComponent();
        }

        public bool set = false;
        private void frm_copy_multi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{tab}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length > 0 && Convert.ToInt16(textBox1.Text) > 0)
            {
                set = true;
                this.Close();
            }
            else
                MessageBox.Show("Copy must be greater than 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = Convert.ToInt16(textBox1.Text).ToString();
            }
            catch
            {
                try { textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Trim().Length - 1); } catch { textBox1.Text = ""; }
            }

            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void Frm_copy_multi_Load(object sender, EventArgs e)
        {

        }
    }
}
