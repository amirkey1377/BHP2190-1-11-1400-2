using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BHP2190.forms
{
    public partial class frm_setting : Form
    {
        public frm_setting()
        {
            InitializeComponent();
        }

        public bool set = false;
        string path = Application.StartupPath + "\\setting.txt";
        private void frm_setting_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (string str in System.IO.Ports.SerialPort.GetPortNames())
                {
                    ((ComboBox)cmb_PortName).Items.Add(str);
                }

                string[] data = File.ReadAllLines(path);
                for (int i = 0; i < data.Length; i++)
                {
                    string s = data[i];
                    string field = s.Substring(0, s.IndexOf(':'));

                    switch (field)
                    {
                        case "port": cmb_PortName.Text = s.Substring(s.IndexOf(':') + 1); break;
                        case "baudRate": txt_BRate.Text = s.Substring(s.IndexOf(':') + 1); break;
                        case "readTimeout": txt_RT.Text = s.Substring(s.IndexOf(':') + 1); break;
                        case "writeTimeout": txt_WT.Text = s.Substring(s.IndexOf(':') + 1); break;
                    }
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            string[] data = File.ReadAllLines(path);
            for (int i = 0; i < data.Length; i++)
            {
                string s = data[i];
                string field = s.Substring(0, s.IndexOf(':'));

                switch (field)
                {
                    case "port": data[i] = field + ":" + cmb_PortName.Text.Trim(); break;
                    case "baudRate": data[i] = field + ":" + txt_BRate.Text; break;
                    case "readTimeout": data[i] = field + ":" + txt_RT.Text; break;
                    case "writeTimeout": data[i] = field + ":" + txt_WT.Text; break;
                }
            }
            File.WriteAllLines(path, data);

            set = true;
            this.Close();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            cmb_PortName.Text = "COM1";
            txt_BRate.Text = "9600";
            txt_RT.Text = "1000";
            txt_WT.Text = "1000";
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_setting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{tab}");
        }
    }
}
