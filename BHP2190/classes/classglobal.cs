using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static BHP2190.classes.class_params;
using static BHP2190.classes.class_params.CV_params;

namespace BHP2190.classes
{
    public class classglobal
    {
        //نام مخفف یا مستعار تکنیک ها درون آرایه قرار داده میشود و در قسمتهایی از برنامه با شماره اندیس فراخوانی می شود
        public static string[] TechName = { "CV", "OCP", "DCV", "NPV", "DPV", "SWV", "LSV", "DCS", "DPS", "CPC", "CHP", "CHA", "CHC" };

        //نام کامل تکنیکها
        public static string[] Tech_FullName = { "Cyclic Voltametry", "Open Circuit Potential", "DC Voltametry", "Normal Pulse Voltametry", "Diff. Pulse Voltametry", "Square Wave Voltametry", "Linear Sweep Voltametry", "Stripping DC Voltametry", "Stripping Diff. Pulse Voltametry", "Controlled Potential Coulometry", "Chrono Potential Coulometry", "Chrono Amperometry", "Chrono Coulometry" };

        //واحد های اندازه گیری هر تکنیک روی نمودار محور افقی 
        public static string[] HAxisTitle = { "Potential(V)", "Time(S)", "Potential(V)", "Potential(V)", "Potential(V)", "Potential(V)", "Potential(V)", "Potential(V)", "Potential(V)", "Time(S)", "Potential(V)", "Time(S)", "Time(S)" };

        //واحد های اندازه گیری هر تکنیک روی نمودار محور عمودی
        public static string[] VAxisTitle = { "Current(UA)", "Current(UA)", "Current(UA)", "Current(UA)", "Current(UA)", "Current(UA)", "Current(UA)", "Current(UA)", "Current(UA)", "Current(UA)", "Current(UA)", "Current(UA)", "Current(UA)" };

        //آرایه ای از رنگ ها که به صورت تصادفی از بین اینها برای رنگ گرافها انتخاب می شود
        public static Color[] color_Graph = {
                             Color.Blue,Color.Aqua,Color.BurlyWood,Color.CadetBlue,Color.Blue,Color.Red,Color.MediumSeaGreen,Color.DarkTurquoise,
                             Color.Chartreuse,Color.Chocolate,Color.Coral,Color.CornflowerBlue,Color.Crimson,Color.Cyan,Color.DarkGoldenrod,Color.Blue,Color.MediumSeaGreen,
                             Color.DarkMagenta,Color.DarkOrange,Color.DarkOrchid,Color.DarkSalmon,Color.DarkSeaGreen,Color.DarkTurquoise,Color.DeepPink,Color.DeepSkyBlue,Color.Fuchsia,
                             Color.Gold,Color.Goldenrod,Color.GreenYellow,Color.HotPink,Color.IndianRed,Color.Khaki,Color.LavenderBlush,Color.LawnGreen,Color.LemonChiffon,Color.Red,
                             Color.Lime,Color.Magenta,Color.MediumAquamarine,Color.MediumOrchid,Color.MediumPurple,Color.MediumSeaGreen,Color.MediumSpringGreen,Color.MediumVioletRed,
                             Color.MistyRose,Color.NavajoWhite,Color.OliveDrab,Color.Orange,Color.OrangeRed,Color.Orchid,Color.PaleGoldenrod,Color.PaleGreen,Color.PaleTurquoise,
                             Color.PaleVioletRed,Color.PeachPuff,Color.Peru,Color.Pink,Color.Plum,Color.PowderBlue,Color.Red,Color.Salmon,Color.SandyBrown,Color.SeaGreen,
                             Color.SkyBlue,Color.SpringGreen,Color.SteelBlue,Color.Tomato,Color.Turquoise,Color.Violet,Color.YellowGreen,Color.MediumTurquoise,Color.Blue,
                             Color.Red,Color.MediumSeaGreen,Color.DarkTurquoise,Color.PaleVioletRed,Color.Pink,Color.MediumSpringGreen,Color.PeachPuff
                           };


        //جهت ست کردن مقدار به پارامترهای هر تکنیک یک مقدار به تکنیک داده شده تا نام مخفف آن با عدد صدا زده شود
        public const byte CV = 0, OCP = 1, DCV = 2, NPV = 3, DPV = 4, SWV = 5, LSV = 6, DCS = 7, DPS = 8, CPC = 9, CHP = 10, CHA = 11, CHC = 12;

        public static string cv_cycle = "0";
        public static string cv_e1 = "0";
        public static string cv_e2 = "0";
        public static string cv_e3 = "0";
        public static string cv_sr = "0";
        public static string cv_hs = "0";
        public static string cv_ts = "0";
        public static string cv_eq = "0";
        public static string cv_ht = "0";
        public static string cv_com = "";

        public static string ocp_time = "0";

        public static string dcv_e1 = "0";
        public static string dcv_e2 = "0";
        public static string dcv_hs = "0";
        public static string dcv_eq = "0";
        public static string dcv_sr = "0";
        public static string dcv_ts = "0";
        public static string dcv_com = "";

        public static string npv_e1 = "0";
        public static string npv_e2 = "0";
        public static string npv_hs = "0";
        public static string npv_pw = "0";
        public static string npv_eq = "0";
        public static string npv_sr = "0";
        public static string npv_ts = "0";
        public static string npv_com = "";

        public static string dpv_e1 = "0";
        public static string dpv_e2 = "0";
        public static string dpv_hs = "0";
        public static string dpv_ph = "0";
        public static string dpv_pw = "0";
        public static string dpv_eq = "0";
        public static string dpv_sr = "0";
        public static string dpv_ts = "0";
        public static string dpv_com = "";

        public static string swv_e1 = "0";
        public static string swv_e2 = "0";
        public static string swv_fr = "0";
        public static string swv_hs = "0";
        public static string swv_ph = "0";
        public static string swv_eq = "0";
        public static string swv_sr = "0";
        public static string swv_com = "";

        public static string lsv_e1 = "0";
        public static string lsv_e2 = "0";
        public static string lsv_hs = "0";
        public static string lsv_eq = "0";
        public static string lsv_sr = "0";
        public static string lsv_ts = "0";
        public static string lsv_com = "";

        public static string dcs_e1 = "0";
        public static string dcs_e2 = "0";
        public static string dcs_hs = "0";
        public static string dcs_eq = "0";
        public static string dcs_sr = "0";
        public static string dcs_ts = "0";
        public static string dcs_com = "";

        public static string dps_e1 = "0";
        public static string dps_e2 = "0";
        public static string dps_hs = "0";
        public static string dps_ph = "0";
        public static string dps_pw = "0";
        public static string dps_eq = "0";
        public static string dps_sr = "0";
        public static string dps_ts = "0";
        public static string dps_com = "";

        public static string cpc_e1 = "0";
        public static string cpc_eq = "0";
        public static string cpc_t1 = "0";
        public static string cpc_com = "";

        public static string chp_i1 = "0";
        public static string chp_i2 = "0";
        public static string chp_eq = "0";
        public static string chp_t1 = "0";
        public static string chp_t2 = "0";
        public static string chp_com = "";

        public static string cha_e1 = "0";
        public static string cha_e2 = "0";
        public static string cha_eq = "0";
        public static string cha_t1 = "0";
        public static string cha_t2 = "0";
        public static string cha_com = "";

        public static string chc_e1 = "0";
        public static string chc_e2 = "0";
        public static string chc_eq = "0";
        public static string chc_t1 = "0";
        public static string chc_t2 = "0";
        public static string chc_com = "";





    }





}