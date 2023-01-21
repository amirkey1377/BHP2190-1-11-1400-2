using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace BHP2190.classes
{
    public class errors_cls
    {
        public errors_cls(Exception ex,object sender1,int stateshow) {
            string txt = "";
            switch (ex.GetType().ToString())
            {
                case "System.ArgumentException":
                    txt = "خطا : " + ex.Message.Trim();
                    break;
               
                case "System.Data.SqlClient.SqlException":
                    switch(((System.Data.SqlClient.SqlException)ex).Number)
                    {
                        case 547:
                            txt = "خطا : عمل حذف و بروز رسانی بدلیل وجود این داده در سایر اطلاعات امکان پذیر نمی باشد  ";
                            break;
                        case 4060 :
                            txt = "خطا : تنظیمات برقراری ارتباط با پایگاه داده درست نمی باشد . لطفا بررسی نمایید ";
                            break;
                        case 2812 :
                            txt = "خطا : پروسه ای با عنوان تعریف شده در پایگاه داده موجود نمی باشد . لطفا بررسی نمایید ";
                            break;
                        default:
                            txt = "خطا : " + ex.Message.Trim();
                            break;
                    }
                    break;
                default :
                    txt = "خطا : " + ex.Message.Trim();
                    break;
            }
            if (stateshow == 1)
                MessageBox.Show(txt, "Error");
            else if(stateshow == 2)
                ((RichTextBox)sender1).Text = txt.Trim();

            
        
        }
    }
}