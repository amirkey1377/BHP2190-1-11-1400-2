using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BHP2190.classes
{
    public class clasPeak
    {
        public double[] PeakX11_n = new double[20], PeakY11_n = new double[20], PeakX21_n = new double[20], PeakY21_n = new double[20];
        public double[] Peak_Height = new double[20], PeakXm1_n = new double[20], PeakYm1_n = new double[20], Areas = new double[20];
        public double SR_Peak = 0;
        string[] cf = { "Maximum", "Minimum" };


        //******************************Peak Detection
        int Peak_m = 0;
        string Find_Peak(double[] X, double[] Y, int i, double Factor, double Factor2)
        {
            int j = 0;
            int te = 0, had1 = 0;
            int[] had = new int[3];
            double[] ah = new double[2 * X.Length];
            double[] bh = new double[2 * X.Length];
            double[] ah1 = new double[2 * X.Length];
            double[] bh1 = new double[2 * X.Length];
            string Result_Str = "";
            int klm = 0;
            had[0] = -858993460;
            i--;
            if (X.Length < 10)
            {
                MessageBox.Show("Number of Points is fewer than from Minimum points(10)");
                return "";
            }
            for (klm = 0; klm < i; klm++)
            {
                if (X[klm] == X[klm + 1])
                    j++;
                else
                    break;
            }
            if (X[klm] < X[klm + 1])
            {
                for (j = 0; j < i; j++)
                    if (X[j] > X[j + 1])
                    {
                        had[0] = j;
                        break;
                    }
                if (had[0] < 0 || had[0] > i)
                    had[0] = i;
                had1 = had[0];
                if (j < i + 2)
                    te++;
                Result_Str += Find_P1(X, Y, had1, Factor, Factor2);
                if (te == 1)
                {
                    had[te] = i - had1;
                    for (j = 0; j < had[1]; j++)
                    {
                        ah[j] = X[j + had1];
                        bh[j] = Y[j + had1];
                    }

                    for (j = 0; j < had[1] - 2; j++)
                    {
                        ah1[j] = ah[had1 - j - 1];
                        bh1[j] = bh[had1 - j - 1];
                    }
                    had1 = had[1] - 2;
                    Result_Str += Find_P1(ah1, bh1, had1, Factor, Factor2);
                }

            }
            //////////********************************************************
            if (X[klm] > X[klm + 1])
            {
                for (j = 0; j < i; j++)
                {
                    if (X[j] < X[j + 1]) { had[0] = j; break; }
                }
                if (had[0] < 0 || had[0] > i) had[0] = i;
                had1 = had[0];
                if (j < i - 2)
                    te++;

                for (j = 0; j < had1; j++)
                {
                    ah1[j] = X[had1 - j - 1];
                    bh1[j] = Y[had1 - j - 1];
                }

                Result_Str += Find_P1(ah1, bh1, had1, Factor, Factor2);
                if (te == 1)
                {
                    had[te] = i - had1;
                    had1 = had[1];
                    for (j = 0; j < had1 - 1; j++)
                    {
                        ah[j] = X[j + had1];
                        bh[j] = Y[j + had1];
                    }
                    Result_Str += Find_P1(ah, bh, had1, Factor, Factor2);
                }
            }

            return Result_Str;
        }

        double ab = 0, ab1 = 0;
        int j = 0;

        bool Factor_in_Num(double ab_, double fctr, double y1, double y2, int lows)
        {
            ab = (y1 / y2);
            if (y1 > y2)
                return true;
            return false;
        }

        ///------------------------------------------------------------

        string Find_P1(double[] X1, double[] Y1, int i, double Factor, double Factor2)
        {
            int k = 0, k1 = 0, k2 = 0;
            int ma = 0, mx1 = 0, kjadid = 0;
            double max = 0;
            int mazda = 0;
            int i1 = 0, i2 = 0, jf = 0, jb = 0;
            int j1 = 0, j2 = 0;
            string cs1 = "", strP = "";
            double[] hlf = new double[2 * X1.Length], hlf1 = new double[2 * X1.Length + 1];
            double[] Ys = new double[2 * X1.Length], Ys1 = new double[2 * X1.Length];
            double tan = 0, ofset = 0, lhight = 0;//, s23 = 0;
            double[] btan = new double[2 * X1.Length];
            double line_m = 0, line_ofset = 0, y_line = 0, set = 0;
            int jb1 = 0, jb2 = 0;
            double lowsense = 1;
            int mp;

            if (Find_P3(X1, Y1, i) == 1)
            {
                mazda = 1;
                for (j = 0; j < i; j++)
                    Y1[j] = -1 * Y1[j];
            }
            else
                mazda = 0;
            ///iox=fopen("h:\\m.txt","aw");if(iox==NULL)AfxMessageBox("m              ixx");	
            k2 = 0;
        ff321:
            for (j = k2; j < i - 5; j++)
            {                                    //////////////////ASLITARIN FOR
                k = j;
                //////////////////////////DETECTING FOR FIRST OF PEAK
                ab = (Y1[j]) / Y1[j + 1];
                if (Y1[j] > Y1[j + 1] && (ab < 0))
                    j++;
            gnb:
                if (j == i - 3)
                    break;
                ab = Math.Abs((Y1[j]) / Y1[j + 1]);
                if (Y1[j] > Y1[j + 1])
                {
                    j++;
                    goto gnb;
                }
                if (ab < Factor * .97)
                {
                    k = j;
                    j++;
                    ab = Math.Abs((Y1[j]) / Y1[j + 1] * lowsense);
                    if (Y1[j] > Y1[j + 1])
                        if (lowsense == 1)
                        {
                            j++;
                            goto gnb;
                        }
                    if (ab < Factor * 1.4 * .97)
                    {
                        j++;
                        ab = Math.Abs((Y1[j]) / Y1[j + 1] * lowsense);
                        if (Y1[j] > Y1[j + 1])
                            if (lowsense == 1)
                            {
                                j++;
                                goto gnb;
                            }
                        if (ab < Factor * 1.4 * .97)
                        {
                            j++;
                            ab = Math.Abs((Y1[j]) / Y1[j + 1] * lowsense);
                            if (Y1[j] > Y1[j + 1])
                                if (lowsense == 1)
                                {
                                    j++;
                                    goto gnb;
                                }
                            if (ab < Factor * 1.4 * .97)
                            {
                                j++;
                                ab = Math.Abs((Y1[j]) / Y1[j + 1] * lowsense);
                                if ((Y1[j]) > (Y1[j + 1]))
                                    if (lowsense == 1)
                                    {
                                        j++;
                                        goto gnb;
                                    }
                                if (ab < Factor * 1.4 * .97)
                                {
                                    j++;
                                    ab = Math.Abs((Y1[j]) / Y1[j + 1] * lowsense);
                                    if ((Y1[j]) > Y1[j + 1])
                                        if (lowsense == 1)
                                        {
                                            j++;
                                            goto gnb;
                                        }
                                    if ((ab) < Factor * 1.4 * .97)
                                    {
                                        j++;
                                        ab = Math.Abs((Y1[j]) / Y1[j + 1] * lowsense);
                                        if ((Y1[j]) > Y1[j + 1])
                                            if (lowsense == 1)
                                            {
                                                j++;
                                                goto gnb;
                                            }
                                        if ((ab) < Factor * 1.4 * .97)
                                        {
                                            j++;
                                            PeakX11_n[Peak_m] = X1[k];
                                            PeakY11_n[Peak_m] = Y1[k] - (2 * Y1[k] * mazda);
                                            k1 = k;
                                            i1 = k;
                                            //if(m>0){ if((PeakX11_n[m]<PeakX21_n[m-1] && PeakX11_n[m]>PeakX11_n[m-1]) || (PeakX11_n[m]>PeakX21_n[m-1] && PeakX11_n[m]<PeakX11_n[m-1]) ){j++;goto gnb;}}
                                            j = j + 10;

                                            while (Y1[j] < Y1[j + 1])
                                                j++;
                                            ///lllllllllllllllllllllllllllllllllllllllllllll
                                            kjadid = j;
                                            ab = Math.Abs((Y1[k + 1] - Y1[k]) / (5 * Y1[k + 1]));
                                            j = k;
                                            while (true)
                                            {
                                                j--;
                                                if (j < 2)
                                                {
                                                    j = kjadid;
                                                    break;
                                                }
                                                ab1 = Math.Abs((Y1[j + 1] - Y1[j]) / Y1[j + 1]);
                                                if (ab1 < ab)
                                                {
                                                    PeakX11_n[Peak_m] = X1[j];
                                                    i1 = j;
                                                    PeakY11_n[Peak_m] = Y1[j] - (2 * Y1[j] * mazda);
                                                    k1 = j;
                                                    j = kjadid;
                                                    break;
                                                }
                                            }
                                            ///lllllllllllllllllllllllllllllllllllllllllllll
                                            //END/////////////////////DETECTING FOR FIRST OF PEAK
                                            //////SENSING FOR MAXIMUM or MINIMUM
                                            ma = 2 * (j - k);
                                            while (true)
                                            {
                                                max = Y1[k];
                                                mx1 = k;
                                                for (j = k; j < (k + ma); j++)
                                                    if (j < i - 1)
                                                        if (Y1[j + 1] > max)
                                                        {
                                                            mx1 = j + 1;
                                                            max = Y1[j + 1];/*im[m]=it[j+1];*/
                                                        }
                                                if (mx1 < k + ma - 7)
                                                    break;
                                                else
                                                    ma = ma + 10;
                                            }
                                            //END//SENSING FOR MAXIMUM or MINIMUM
                                            //////////////////////////DETECTING FOR END OF PEAK
                                            j = mx1;
                                            ab = 0; j1 = -1; j2 = 0;
                                            //pop:
                                            while (true)
                                            {
                                                j++;
                                                j1++;
                                                if (j > i - 2)
                                                    break;
                                                ab = Math.Abs((Y1[j + 1] - Y1[mx1]) / (X1[j + 1] - X1[mx1]));
                                                ab1 = Math.Abs((Y1[j + 1] - Y1[j]) / (X1[j + 1] - X1[j]));
                                                if (((5 - j2) * ab1) < ab)
                                                {
                                                    if (j1 < 7)
                                                    {
                                                        j2 = 1;
                                                        goto par;
                                                    }
                                                    PeakX21_n[Peak_m] = X1[j];
                                                    i2 = j;
                                                    PeakY21_n[Peak_m] = Y1[j] - (2 * Y1[j] * mazda);
                                                    k2 = j;
                                                    ab = Math.Abs((Y1[j + 1] - Y1[mx1]) / (X1[j + 1] - X1[mx1]));
                                                    ab1 = Math.Abs((Y1[j + 1] - Y1[j]) / (X1[j + 1] - X1[j]));
                                                    if (((5 - j2) * ab1) < ab)
                                                    {
                                                        PeakX21_n[Peak_m] = X1[j];
                                                        i2 = j;
                                                        PeakY21_n[Peak_m] = Y1[j] - (2 * Y1[j] * mazda);
                                                        k2 = j;
                                                        break;
                                                    }
                                                }
                                            par: ;
                                            }
                                            //END////////////////////DETECTING FOR END OF PEAK
                                            ///////////////////////////////////////////////DETERMINING WHAT PEAK SIDE IS MORE QUIETLY THAN OTHER.
                                            if (k1 > i - 5) k1 = i - 5;
                                            if (k2 < 5) k2 = 5;
                                            ab = (Math.Abs(Y1[k1 + 5] - Y1[k1])) / (Math.Abs(X1[k1 + 5] - X1[k1]));
                                            ab1 = (Math.Abs(Y1[k2 - 5] - Y1[k2])) / (Math.Abs(X1[k2 - 5] - X1[k2]));
                                            if (ab > ab1)
                                            {
                                                mp = k2;
                                                for (jb2 = mp - 5; jb2 < mp + 10; jb2++)
                                                    if (jb2 > i - 1)
                                                        break;
                                                    else
                                                        Ys1[jb2] = (Y1[jb2 - 1] + Y1[jb2] + Y1[jb2 + 1]) / 3;
                                                for (jb2 = mp - 5; jb2 < mp + 10; jb2++)
                                                    if (jb2 > i - 1)
                                                        break;
                                                    else
                                                        Ys[jb2] = (Ys1[jb2 - 1] + Ys1[jb2] + Ys1[jb2 + 1]) / 3;
                                                for (jb2 = mp - 5; jb2 < mp + 10; jb2++)
                                                    if (jb2 > i - 1)
                                                        break;
                                                    else
                                                        Ys1[jb2] = (Ys[jb2 - 1] + Ys[jb2] + Ys[jb2 + 1]) / 3;

                                                //************************Comment 1
                                                if (Ys1[mp - 1] != 0)
                                                    ab = (Ys1[mp - 1] - Ys1[mp]) / (Ys1[mp - 1]);
                                                else
                                                    if (Ys1[mp] != 0)
                                                        ab = (Ys1[mp - 1] - Ys1[mp]) / (Ys1[mp]);
                                                for (jb2 = mp; jb2 < mp + 10; jb2++)
                                                {
                                                    if (jb2 > i - 1)
                                                        goto boron;
                                                    if ((Ys1[jb2 - 1]) != 0)
                                                        ab1 = (Ys1[jb2 - 1] - Ys1[jb2]) / (Ys1[jb2 - 1]);
                                                    else
                                                        if (Ys1[jb2] != 0)
                                                            ab1 = (Ys1[jb2 - 1] - Ys1[jb2]) / (Ys1[jb2]);
                                                    if (ab1 < ab / 1.5)
                                                    {
                                                        jb2++;
                                                        if (jb2 > i - 1)
                                                            goto boron;
                                                        if ((Ys1[jb2 - 1]) != 0)
                                                            ab1 = (Ys1[jb2 - 1] - Ys1[jb2]) / (Ys1[jb2 - 1]);
                                                        else
                                                            if (Ys1[jb2] != 0)
                                                                ab1 = (Ys1[jb2 - 1] - Ys1[jb2]) / (Ys1[jb2]);
                                                        if (ab1 < ab / 1.5)
                                                        {
                                                            PeakX21_n[Peak_m] = X1[jb2];
                                                            PeakY21_n[Peak_m] = Y1[jb2] - (2 * Y1[jb2] * mazda);
                                                            k2 = jb2;
                                                        }
                                                    }
                                                boron: ;
                                                }
                                                jb2 = k2 + 10;
                                                if (jb2 > i - 1)
                                                    jb2 = i - 1;
                                                PeakX21_n[Peak_m] = X1[jb2];
                                                PeakY21_n[Peak_m] = Y1[jb2] - (2 * Y1[jb2] * mazda);
                                                k2 = jb2;
                                            }
                                            else
                                            {
                                                mp = k1;
                                                for (jb2 = mp + 5; jb2 < mp - 10; jb2--)
                                                    if (jb2 > i - 1)
                                                        break;
                                                    else
                                                        Ys1[jb2] = (Y1[jb2 - 1] + Y1[jb2] + Y1[jb2 + 1]) / 3;
                                                for (jb2 = mp + 5; jb2 < mp - 10; jb2--)
                                                    if (jb2 > i - 1)
                                                        break;
                                                    else
                                                        Ys[jb2] = (Ys1[jb2 - 1] + Ys1[jb2] + Ys1[jb2 + 1]) / 3;
                                                for (jb2 = mp + 5; jb2 < mp - 10; jb2--)
                                                    if (jb2 > i - 1)
                                                        break;
                                                    else
                                                        Ys1[jb2] = (Ys[jb2 - 1] + Ys[jb2] + Ys[jb2 + 1]) / 3;
                                                //************************* Comment 2
                                                if (mp < 1) mp = 1;
                                                ab = Math.Abs((Ys1[mp - 1] - Ys1[mp]) / (Ys1[mp - 1]));
                                                if ((Ys1[mp - 1]) == 0)
                                                    ab = Math.Abs((Ys1[mp - 1] - Ys1[mp]) / (Ys1[mp]));
                                                for (jb2 = mp; jb2 < mp - 10; jb2--)
                                                {
                                                    if (jb2 < 1)
                                                        goto boron1;
                                                    ab1 = Math.Abs((Ys1[jb2 - 1] - Ys1[jb2]) / (Ys1[jb2]));
                                                    if ((Ys1[jb2]) == 0)
                                                        ab1 = Math.Abs((Ys1[jb2 - 1] - Ys1[jb2]) / (Ys1[jb2 - 1]));
                                                    if (ab1 < ab / 1.5)
                                                    {
                                                        jb2++;
                                                        if (jb2 < 1)
                                                            goto boron1;
                                                        ab1 = Math.Abs((Ys1[jb2 - 1] - Ys1[jb2]) / (Ys1[jb2]));
                                                        if ((Ys1[jb2]) == 0)
                                                            ab1 = Math.Abs((Ys1[jb2 - 1] - Ys1[jb2]) / (Ys1[jb2 - 1]));
                                                        if (ab1 < ab / 1.5)
                                                        {
                                                            PeakX11_n[Peak_m] = X1[jb2];
                                                            PeakY11_n[Peak_m] = Y1[jb2] - (2 * Y1[jb2] * mazda);
                                                            k2 = jb2;
                                                        }
                                                    }
                                                boron1: ;
                                                }
                                                jb2 = k - 10;
                                                if (jb2 < 1)
                                                    jb2 = 1;
                                                PeakX11_n[Peak_m] = X1[jb2];
                                                PeakY11_n[Peak_m] = Y1[jb2] - (2 * Y1[jb2] * mazda);
                                                k1 = jb2;
                                            }
                                            //END//////////////////////////////////////////DETERMINING WHAT PEAK SIDE IS MORE QUIETLY THAN OTHER.

                                            ///////////////////////////////////////////////DETECTIN OF FIRST PEAK AT FINAL::::::::::::
                                            /*TO PROVIDE MINIMUM VALUE*/
                                            for (jf = k1; jf < mx1; jf++)
                                                if ((PeakY11_n[Peak_m] - (2 * mazda * PeakY11_n[Peak_m])) > Y1[jf])
                                                {
                                                    PeakY11_n[Peak_m] = Y1[jf] - (2 * Y1[jf] * mazda);
                                                    PeakX11_n[Peak_m] = X1[jf];
                                                    k1 = jf;
                                                }
                                            jf = k2; jb2 = 0;
                                            for (jb1 = k1 - 0; jb1 < mx1 - 1; jb1++)
                                            {
                                                if (jb1 <= 0)
                                                    jb1 = 1;
                                                line_m = (Y1[jf] - Y1[jb1]) / (X1[jf] - X1[jb1]);
                                                line_ofset = Y1[jf] - (line_m * X1[jf]);
                                                for (jb = jb1 + 1; jb < mx1; jb++)
                                                {
                                                    y_line = line_m * X1[jb] + line_ofset;
                                                    set = Y1[jb];
                                                    if (y_line > set)
                                                        jb2 = 1;
                                                }
                                                if (jb2 == 0)
                                                {
                                                    PeakY11_n[Peak_m] = Y1[jb1] - (2 * Y1[jb1] * mazda);
                                                    PeakX11_n[Peak_m] = X1[jb1]; k1 = jb1;
                                                    break;
                                                }
                                                else
                                                    jb2 = 0;
                                            }
                                            ///END/////////////////////////////////////////DETECTIN OF FIRST PEAK AT FINAL::::::::::::
                                            ///////////////////////////////////////////////DETECTIN OF END PEAK AT FINAL::::::::::::
                                            /*TO PROVIDE MINIMUM VALUE*/
                                            for (jf = k1; jf < mx1; jf++)
                                                if ((PeakY11_n[Peak_m] - (2 * mazda * PeakY11_n[Peak_m])) > Y1[jf])
                                                {
                                                    PeakY11_n[Peak_m] = Y1[jf] - (2 * Y1[jf] * mazda);
                                                    PeakX11_n[Peak_m] = X1[jf];
                                                    k1 = jf;
                                                }
                                            jf = k1; jb2 = 0;
                                            for (jb1 = k2; jb1 > mx1 + 1; jb1--)
                                            {
                                                line_m = (Y1[jf] - Y1[jb1]) / (X1[jf] - X1[jb1]);
                                                line_ofset = Y1[jf] - (line_m * X1[jf]);
                                                for (jb = jb1 - 1; jb > mx1; jb--)
                                                {
                                                    y_line = line_m * X1[jb] + line_ofset;
                                                    set = Y1[jb];
                                                    if (y_line > set)
                                                        jb2 = 1;
                                                }
                                                if (jb2 == 0)
                                                {
                                                    PeakY21_n[Peak_m] = Y1[jb1] - (2 * Y1[jb1] * mazda);
                                                    PeakX21_n[Peak_m] = X1[jb1];
                                                    k2 = jb1;
                                                    break;
                                                }
                                                jb2 = 0;
                                            }
                                            ///END/////////////////////////////////////////DETECTIN OF END PEAK AT FINAL::::::::::::

                                            if (PeakX21_n[Peak_m] <= PeakX11_n[Peak_m] || k2 > (i - 2)) { /*AfxMessageBox("not possible for peak detection");break;*/ }
                                            for (jf = 0; jf < X1.Length; jf++)
                                            {
                                                hlf[jf] = X1[jf];
                                                hlf1[jf] = Y1[jf];
                                            }
                                            //i1 = 28; k1 = 28; PeakX11_n[m] = X1[k1]; PeakY11_n[m] = -Y1[k1];
                                            PeakYm1_n[Peak_m] = Find_P2(X1, Y1, i1, i2);
                                            PeakYm1_n[Peak_m] = PeakYm1_n[Peak_m] - (2 * PeakYm1_n[Peak_m] * mazda);
                                            tan = (PeakY21_n[Peak_m] - PeakY11_n[Peak_m]) / (PeakX21_n[Peak_m] - PeakX11_n[Peak_m]);
                                            ofset = PeakY11_n[Peak_m] - (tan * PeakX11_n[Peak_m]);
                                            lhight = (tan * Find_P4(X1, Y1, i1, i2)) + ofset;
                                            Peak_Height[Peak_m] = PeakYm1_n[Peak_m] - lhight;
                                            //s23 = Surface(X1, Y1, k1, k2);
                                            Areas[Peak_m] = Surface2(X1, Y1, k1, k2);
                                            PeakXm1_n[Peak_m] = Find_P4(X1, Y1, i1, i2);
                                            double SRP = 0;
                                            if (SR_Peak != 0)
                                                SRP = Areas[Peak_m] / SR_Peak;

                                            if (Peak_m == 0)
                                                strP = "Detection Peak (Auto)->\n";
                                            else
                                                strP = "";
                                            cs1 += strP + "Peak: P" + (Peak_m + 1).ToString("0") + "\nAt: " + cf[mazda] + "\nPosition: " + PeakXm1_n[Peak_m].ToString("0.000") + "\nArea:       " + Areas[Peak_m].ToString("0.00e-0") + "\nHeight:    " + Math.Abs(Peak_Height[Peak_m]).ToString("0.00e-0") +
                                                        "\nCharge:   " + SRP.ToString("0.000e-0") + "\n-----------------------\n";
                                            //cs1 += "\nPeak" + (Peak_m + 1).ToString("0") + ":\n X1=" + PeakX11_n[Peak_m].ToString("0.000") + "\t;Y1=" + PeakY11_n[Peak_m].ToString("0.000e-0") + "\n;X2=" + PeakX21_n[Peak_m].ToString("0.000") + "\t;Y2=" + PeakY21_n[Peak_m].ToString("0.000e-0") +
                                            //            "\n At " + cf[mazda] + "\nXm=" + PeakXm1_n[Peak_m].ToString("0.000") + "\tYm=" + PeakYm1_n[Peak_m].ToString("0.000e-0") + "\nArea=" + Areas[Peak_m].ToString("0.00e-0") + "\nHeight=" + Math.Abs(Peak_Height[Peak_m]).ToString("0.00e-0") +
                                            //            "\n\n\n Area2=" + s23.ToString("0.000e-0");
                                            Peak_m++;
                                            if (Peak_m == 20)
                                                Peak_m = 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }///////////BASTENE { HAYE FIRST OF PEAK
            }///ASLITARIN FOR

            if (Peak_m == 0)
            {
                if (lowsense == -1)
                    goto f320;
                cs1 = "peak not found at case of: " + cf[mazda] + " with high sensivity ";//AfxMessageBox(cs1);
                lowsense = lowsense - 1;
                //lowsense=0;
            }
            else
                goto f320;
            if (lowsense == 0)
                goto ff321;
        f320:
            return cs1;
        }

        double Find_P2(double[] X1, double[] Y1, int i1, int i2)
        {
            int j = 0;
            double max_s = 0, min_s = 0;
            int i3 = 0;
            int maxt = 0, mint = 0;
            double max = 0, min = 0;

        aval:
            max_s = 0; min_s = 0;
            max = Y1[i1];
            maxt = i1;
            for (j = i1; j < i2 - i3; j++)
                if (max < Y1[j])
                {
                    max = Y1[j];
                    maxt = j;
                }

            min = Y1[0]; mint = 0;
            for (j = i1; j < i2 - i3; j++)
                if (min > Y1[j])
                {
                    min = Y1[j];
                    mint = j;
                }
            if (!((maxt > 3) && (maxt < (i2 - 2))))
                max_s = 1;

            if (!((mint > 3) && (mint < (i2 - 4))))
                min_s = 1;

            if (max_s == 1 && min_s == 1)
            {
                i3++;
                if (i2 > i3)
                    goto aval;
            }
            return max;
        }

        int Find_P3(double[] X1, double[] Y1, int i)
        {
            int j = 0;
            int maxt = 0, mint = 0;
            double max = 0, min = 0;
            int wm = 0;
            double max_s = 0, min_s = 0;
            int maxmin = 0, i3 = 0;
        aval:
            max_s = 0; min_s = 0;
            max = Y1[0]; maxt = 0;
            for (j = 0; j < i - i3; j++)
                if (max < Y1[j])
                {
                    max = Y1[j];
                    maxt = j;
                }

            min = Y1[0]; mint = 0;
            for (j = 0; j < i - i3; j++)
                if (min > Y1[j])
                {
                    min = Y1[j];
                    mint = j;
                }
            if ((maxt > 3) && (maxt < (i - 4)))
            {
                wm = 0;
                maxmin++;
            }
            else
                max_s = 1;
            if ((mint > 3) && (mint < (i - 4)))
            {
                wm = 1;
                maxmin++;
            }
            else min_s = 1;

            if (max_s == 1 && min_s == 1)
            {
                i3++;
                if (i > i3)
                {
                    maxmin = 0;
                    goto aval;
                }
            }
            if (maxmin == 2)
            {
                if (max > ((2 * Y1[0]) - min))
                    wm = 0;
                else
                    wm = 1;
            }
            return wm;
            ///wm=0 means peak with max
            ///wm=1 means peak with min
        }

        double Find_P4(double[] X1, double[] Y1, int i1, int i2)
        {
            int j = 0;
            int maxt = 0, mint = 0;
            double max = 0, min = 0;
            int wm = 0, max_s = 0, min_s = 1, i3 = 0;
        aval:
            max_s = 0; min_s = 0;
            max = Y1[0]; maxt = 0;
            for (j = i1; j < i2 - i3; j++)
                if (max < Y1[j])
                {
                    max = Y1[j];
                    maxt = j;
                }

            min = Y1[0]; mint = 0;
            for (j = i1; j < i2 - i3; j++)
                if (min > Y1[j])
                {
                    min = Y1[j];
                    mint = j;
                }
            if ((maxt > 3) && (maxt < (i2 - 4)))
                wm = 0;
            else
                max_s = 1;
            if ((mint > 3) && (mint < (i2 - 4)))
                wm = 1;
            else
                min_s = 1;

            if (max_s == 1 && min_s == 1)
            {
                i3++;
                if (i2 > i3)
                    goto aval;
            }
            if (wm == 0)
                return X1[maxt];
            else
                return X1[maxt];
            ///wm return the extermom value
        }

        /*double Surface(double[] X1, double[] Y1, int firsti, int endi)
        {
            double area = 0;
            int j = 0, j1 = 0, j2 = 0;
            double zoz1 = 0, zoz2 = 0;

            if (firsti < endi)
            {
                j1 = firsti;
                j2 = endi;
            }
            else
            {
                j2 = firsti;
                j1 = endi;
            }
            if (Y1[firsti] < 0 || Y1[endi] < 0)
            {
                if (Y1[firsti] < Y1[endi])
                    zoz1 = Y1[firsti];
                else
                    zoz1 = Y1[endi];
                for (j = j1; j < j2; j++)
                    Y1[j] -= zoz1;
            }

            if (firsti < endi)
            {
                j1 = firsti;
                j2 = endi;
            }
            else
            {
                j2 = firsti;
                j1 = endi;
            }
            for (j = j1; j < j2; j++)
                area += Math.Abs((Math.Abs(Y1[j]) + Math.Abs(Y1[j + 1]) * (X1[j + 1] - X1[j])) / 2);

            zoz2 = (Y1[firsti] + Y1[endi]) * (X1[j2] - X1[j1]) / 2;
            area -= zoz2;
            return area;
        }*/

        double Surface2(double[] X1, double[] Y1, int lowLimit, int highLimit)
        {
            double result = 0;
           // int h = 0;
            int j = 0;
            double sum = 0;
            double zoz2 = 0;
            if (X1.Length > 2)
            {
                if (lowLimit > highLimit)
                {
                    j = lowLimit;
                    lowLimit = highLimit;
                    highLimit = j;
                }
                for (j = lowLimit + 1; j < highLimit; j++)
                    sum += (Y1[j - 1] * (X1[j] - X1[j - 1]) + (((Y1[j] - Y1[j - 1]) * (X1[j] - X1[j - 1])) / 2));
                zoz2 = Math.Abs((Y1[lowLimit] + Y1[highLimit]) * (X1[lowLimit] - X1[highLimit]) / 2);

            }
            result = sum - zoz2;
            return result;
        }
        /*double Surface2(double[] X1, double[] Y1, int lowLimit, int highLimit)
 {
     double area = 0, a1 = -1;
     int j = -1, j1 = -1, j2 = -1;
     double zoz1 = 0, zoz2 = -1;

     if (lowLimit < highLimit) { j1 = lowLimit; j2 = highLimit; }
     else { j2 = lowLimit; j1 = highLimit; }

     if (Y1[lowLimit] < 0 || Y1[highLimit] < 0)
     {
         if (Y1[lowLimit] < Y1[highLimit])
             zoz1 = Y1[lowLimit];
         else
             zoz1 = Y1[highLimit];
         for (j = j1; j < j2; j++)
             Y1[j] -= zoz1;
     }
     if (lowLimit < highLimit)
     {
         j1 = lowLimit;
         j2 = highLimit;
     }
     else
     {
         j2 = lowLimit;
         j1 = highLimit;
     }
     for (j = j1; j < j2; j++)
     {
         if (Y1[j] < 0)
             Y1[j] *= (-1);
         if (Y1[j + 1] < 0)
             Y1[j + 1] *= (-1);
         area += Math.Abs((Y1[j] + Y1[j + 1]) * (X1[j + 1] - X1[j]) / 2);
     }
     zoz2 = (Y1[lowLimit] + Y1[highLimit]) * (X1[j2] - X1[j1]) / 2;
     area -= zoz2;
     return area;
 }
        */
        public string findp1(double[] a, double[] b, int i)
        {
            Peak_m = 0;
            for (int ij = 0; ij < 10; ij++)
            {
                PeakX11_n[ij] = 0; PeakX21_n[ij] = 0;
                PeakY11_n[ij] = 0; PeakY21_n[ij] = 0;
                Peak_Height[ij] = 0; PeakXm1_n[ij] = 0;
                PeakYm1_n[ij] = 0; Areas[ij] = 0;
            }
            string CC = Find_Peak(a, b, i, 1, 1);   //0.97,1                    
            return CC;
        }

        public string manual(double[] a, double[] b, double firstxi, double firstyi, double endxi, double endyi, int i)
        {
            ///////////A AND B IS THE INITIAL VALUES OF DATA GENERATED
            double tan = 0, ofset = 0, lhight = 0, dhight = 0, s24 = 0;
            string cs = "";
            int j = 0, jmin1 = 0, jmin2 = 0;
            double area1 = 0, distancemin1 = 0, distancemin2 = 0, dis = 0, minx1 = 0, miny1 = 0, minx2 = 0, miny2 = 0;
            int maxt = 0, mint = 0;
            double max = 0, min = 0;
            int wm = 0;
            double ffind = 0;
            double[] ah = new double[1000], bh = new double[1000];
            i = a.Length;
            for (j = 0; j < i - 1; j++)
            {
                if (a[j] == 0)
                {
                    if (a[j] == a[j + 1]) { i = j - 1; break; }
                }
            }
            distancemin1 = Math.Pow(a[0] - firstxi, 2) + Math.Pow(b[0] - firstyi, 2);
            distancemin1 = Math.Pow(distancemin1, 0.5);
            for (j = 0; j < i; j++)
            {
                dis = Math.Pow(a[j] - firstxi, 2) + Math.Pow(b[j] - firstyi, 2);
                dis = Math.Pow(dis, 0.5);
                if (dis < distancemin1)
                {
                    distancemin1 = dis;
                    minx1 = a[j];
                    miny1 = b[j];
                    jmin1 = j;
                }
            }

            distancemin2 = Math.Pow(a[0] - endxi, 2) + Math.Pow(b[0] - endyi, 2);
            distancemin2 = Math.Pow(distancemin2, 0.5);
            for (j = 0; j < i; j++)
            {
                dis = Math.Pow(a[j] - endxi, 2) + Math.Pow(b[j] - endyi, 2);
                dis = Math.Pow(dis, 0.5);
                if (dis < distancemin2)
                {
                    distancemin2 = dis;
                    minx2 = a[j];
                    miny2 = b[j];
                    jmin2 = j;
                }
            }
            if (jmin1 > jmin2) { j = jmin2; jmin2 = jmin1; jmin1 = j; }
            max = b[jmin1];
            maxt = jmin1;
            for (j = jmin1; j < jmin2; j++)
            {
                if (max < b[j])
                {
                    max = b[j];
                    maxt = j;
                }
            }
            min = b[jmin1];
            mint = jmin1;
            for (j = jmin1; j < jmin2; j++)
            {
                if (min > b[j])
                {
                    min = b[j];
                    mint = j;
                }
            }
            if ((maxt > jmin1 + 2) && (maxt < (jmin2 - 2)))
            {
                wm = 0;
                ffind = max;
                s24 = a[maxt];
            }
            else
            {
                wm = 1;
                ffind = min;
                s24 = a[mint];
            }

            tan = (b[jmin1] - b[jmin2]) / (a[jmin1] - a[jmin2]);
            ofset = b[jmin1] - (tan * a[jmin1]);
            if (wm == 1)
            {
                for (j = 0; j < i; j++) { b[j] = b[j] * -1; }
            }
            lhight = (tan * s24) + ofset; //lhight = lhight - (2 * lhight * wm);
            //lhight=  (tan * Find_P4(X1,Y1,i1   , i2   )) + ofset;
            dhight = ffind - lhight; /// -2 * (ffind - lhight);
            if (a[jmin1] > a[jmin1 + 1])
            {
                for (j = 0; j < i; j++)
                {
                    ah[j] = a[i - j - 1];
                    bh[j] = b[i - j - 1];

                }
                j = jmin1;
                jmin1 = i - jmin2 - 1;
                jmin2 = i - j - 1;
            }
            else
            {
                for (j = 0; j < i; j++)
                {
                    ah[j] = a[j];
                    bh[j] = b[j];
                }
            }
            area1 = Surface2(ah, bh, jmin1, jmin2);
            double SRP = 0;
            if (SR_Peak != 0)
                SRP = area1 / SR_Peak;

            cs = "Detection Peak (manual)->\nAt: " + cf[wm] +
                "\nPosition: " + s24.ToString("0.000e-0") + "\nArea:       " + area1.ToString("0.000e-0") +
                         "\nHeight:    " + dhight.ToString("0.000e-0") + "\nCharge:   " + SRP.ToString("0.000e-0") +
                         "\n-----------------------\n";

            if (wm == 1)
                for (j = 0; j < i; j++)
                    b[j] = b[j] * -1;
            return cs;
        }
    }
}
// Comment 1
/* for (jb2 = mp - 5; jb2 < mp + 10; jb2++)
 {
     if (jb2 > i - 1)
         break;
     Ys1[jb2] = Y1[jb2];
 }
 for (jb2 = mp - 5; jb2 < mp + 10; jb2++)
 {
     if (jb2 > i - 1)
         break;
     Ys[jb2] = (Ys1[jb2 - 1] + Ys1[jb2] + Ys1[jb2 + 1]) / 3;
 }
for (jb2 = mp - 5; jb2 < mp + 10; jb2++)
{
    if (jb2 > i - 1)
        break;
    Ys1[jb2] = Ys[jb2];
}
for (jb2 = mp - 5; jb2 < mp + 10; jb2++)
{
    if (jb2 > i - 1)
        break;
    Ys[jb2] = (Ys1[jb2 - 1] + Ys1[jb2] + Ys1[jb2 + 1]) / 3;
}
for (jb2 = mp - 5; jb2 < mp + 10; jb2++)
{
    if (jb2 > i - 1)
        break;
    Ys1[jb2] = Ys[jb2];
}
for (jb2 = mp - 5; jb2 < mp + 10; jb2++)
{
    if (jb2 > i - 1)
        break;
    Ys[jb2] = (Ys1[jb2 - 1] + Ys1[jb2] + Ys1[jb2 + 1]) / 3;
}
for (jb2 = mp - 5; jb2 < mp + 10; jb2++)
{
    if (jb2 > i - 1)
        break;
    Ys1[jb2] = Ys[jb2];
}*/
//Comment 2
/*for (jb2 = mp + 5; jb2 < mp - 10; jb2--)
    {
    if (jb2 < 1)
    break;
    Ys1[jb2] = Y1[jb2];
    }
    for (jb2 = mp + 5; jb2 < mp - 10; jb2--)
    {
    if (jb2 < 1)
    break;
    Ys[jb2] = (Ys1[jb2 - 1] + Ys1[jb2] + Ys1[jb2 + 1]) / 3;
    }
for (jb2 = mp + 5; jb2 < mp - 10; jb2--)
{
    if (jb2 < 1)
        break;
    Ys1[jb2] = Ys[jb2];
}
for (jb2 = mp + 5; jb2 < mp - 10; jb2--)
{
    if (jb2 < 1)
        break;
    Ys[jb2] = (Ys1[jb2 - 1] + Ys1[jb2] + Ys1[jb2 + 1]) / 3;
}
for (jb2 = mp + 5; jb2 < mp - 10; jb2--)
{
    if (jb2 < 1)
        break;
    Ys1[jb2] = Ys[jb2];
}
for (jb2 = mp + 5; jb2 < mp - 10; jb2--)
{
    if (jb2 < 1)
        break;
    Ys[jb2] = (Ys1[jb2 - 1] + Ys1[jb2] + Ys1[jb2 + 1]) / 3;
}
for (jb2 = mp + 5; jb2 < mp - 10; jb2--)
{
    if (jb2 < 1)
        break;
    Ys1[jb2] = Ys[jb2];
}*/



