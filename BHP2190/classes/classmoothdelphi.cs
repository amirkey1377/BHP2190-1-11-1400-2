using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;


namespace BHP2190.classes
{
    class classmoothdelphi
    {
        private bool grftfl = false;
        //tech number=1
        public void smoothnpv_delphi()
        {
            grftfl = true;
        }
        //tech number=2
        public void smoothdpv_delphi() { }
        //tech number=3
        public void smoothswv_delphi() { }
        //tech number=4
        public void smoothcv_delphi()
        {
            grftfl = true;
        }
        //tech number=5
        public void smoothlsv_delphi()
        {
            grftfl = true;
        }
        //tech number=6
        public void smoothdcs_delphi()
        {
            grftfl = true;
        }
        //tech number=7
        public void smoothdps_delphi()
        {
            grftfl = false;
        }
        //tech number=8
        public void smoothccc_delphi()
        {
            grftfl = false;
        }
        //tech number=9
        public void smoothcpc_delphi()
        {
            grftfl = false;
        }
        //tech number=10
        public void smoothchp_delphi()
        {
            grftfl = false;
        }
        //tech number=11
        public void smoothcha_delphi()
        {
            grftfl = false;
        }
        //tech number=12
        public void smoothchc_delphi()
        {
            grftfl = false;
        }
        //tech number=15
        public void smoothocp_delphi()
        {
            grftfl = false;
        }

        //tech number=0
        public void smoothdcv_delphi(Chart  Chart1) {
            byte[] data=new byte[65000];
            int[] Powr=new int[65000];
            int[] Crnt=new int[65000];
            double[] C = new double[65000];
            Color[] Colr = new Color[100];
            //string bat="";            
            byte Stp=0;
            double IE=0, FE=0, EE3=0, I_tot=0, A0, A1, B0,  AB, A2, ai, I0, I1=0, b, Ec, V1, SS, EE=0, EE1=0, EE2=0, mini, MM, Mp, Vi=0, Tstp, Tss=0;
            int Ts, len=0, Fore=0, Rev=0, k, j, c_ycl=0, cl=0, Difr=0, Frs, k1, Len=0, Lst, ser=0, NUM, MaxC, vx=0, ngtv=0, NM=0, Num=0, R_cmp=0, vn=0;
            bool Vry = false, onc = false, Dbl = false, cmpn = false, Colm = false, Conv = false, ctrl = false, Draw_Itself = false, Drvt = false, F_or = false, mx = false, R_vr = false, Suby=false;
            int Tq=0,L_Teq,Teq=0,g=0,a;
            //char Sign='0';
            L_Teq=Teq;
            if (Teq<13)
            {
                if ((Teq==9)&&(data[g+2]>0)) 
                    Vry=true;
                IE=data[g+4]*256+data[g+3];
                if (Teq!=9) 
                {
                    FE=data[g+6]*256+data[g+5];
                    a=g+9;
                    if (Teq<8) 
                    {
                        onc=true;
                        Stp=data[g+2];
                        Ts=data[g+8]*256+data[g+7];
                        if (Ts>32768)
                        {
                            Ts=(Ts & 32767) / 100;
                            Tss=Ts/10;
                            //Rng=10;
                        }
                        else
                        {
                            if (Teq==3)
                            {
                                Tss=1000/Ts;
                                Ts=(int)Math.Round((double)Tss);
                            }
                            else
                            {    
                                Tss=Ts;
                            }
                        }
                        if (Teq==4)
                        {
                            EE3=data[g+10]*256+data[g+9];
                            a=g+11;
                        }

                    }
                }
                else 
                    a=g+5;
                if (Teq>9) 
                    Ts=(data[g+8]*256+data[g+7]) / 10;
                Fore=len / 4;
                Rev=len / 2;
                if (Teq==4) 
                    Fore=len / 2;
            }
            else 
                a=g+9;
            k=a;
            j=0;
            while (k<len) 
            {
                if ((Teq==9)&&(((data[k]& data[k+1])==255)||((data[k] | data[k+1])==0)))
                {
                    if (data[k]==0)
                        Vry=false;
                    else 
                        Vry=true;
                }
                else
                {
                    if ((Teq!=10)&&(Teq!=13)&&(Vry==false))
                    {
                        Powr[j]=data[k] & 15;
                        Crnt[j]=(data[k+1] << 4) | (data[k] >> 4);
                        if ((Teq<8) && (FE<IE) )
                        {
                            if (Powr[j] % 2==0) 
                                Crnt[j]=Crnt[j]*-1;  //invers curve
                        }
                        else
                            if (Powr[j] % 2==1) 
                                Crnt[j]=Crnt[j]*-1;
                        Powr[j]=Powr[j] >> 1;
                        j=j+1;
                        if ((Teq==2)||(Teq==3)||(Teq==7))
                        {
                            Dbl=true;
                            k=k+2;
                            Powr[j]=(data[k] & 15);
                            Crnt[j]=(data[k+1] << 4) | (data[k] >> 4);
                            if (FE<IE) 
                            {
                                if (Powr[j] % 2==0) 
                                    Crnt[j]=Crnt[j]*-1;  //invers curve
                            }
                            else  
                                if (Powr[j] % 2==1)  
                                    Crnt[j]=Crnt[j]*-1;  //invers curve
                            Powr[j]=Powr[j] >> 1;
                            Powr[j+Fore]=Powr[j-1];
                            Crnt[j+Fore]=Crnt[j-1];
                            Powr[j+Rev]=Powr[j];
                            Crnt[j+Rev]=Crnt[j];
                            if (Powr[j-1]>Powr[j]) 
                            {
                                I_tot=Crnt[j-1]*  Math.Pow(10,Powr[j-1]-Powr[j])-Crnt[j];
                                Powr[j-1]=Powr[j];
                            }
                            else
                                I_tot=Crnt[j-1]-Crnt[j]*Math.Pow(10,Powr[j]-Powr[j-1]);
                            while(Math.Abs(I_tot)>9999|| (I_tot<-9999.0))
                            {
                                I_tot=I_tot/10;
                                Powr[j-1]=Powr[j-1]+1;
                            }
                            Crnt[j-1]=(int)Math.Round((double)I_tot);
                            if (((Teq!=3) && (FE>IE)) || ((Teq==3) && (FE<IE))) 
                                Crnt[j-1]=Crnt[j-1]*-1;
                        }
                    }
                    else
                    {
                        Crnt[j]=(data[k+1]*256+data[k]);
                        Powr[j]=8;
                        if (Teq==10) 
                            Crnt[j]=Crnt[j]*-1;
                        j=j+1;
                    }
                }
                k=k+2;
            }
            Fore=Fore+1;
            Rev=Rev+1;
            if (Teq>8) 
            {
                MM=(data[len]*256+data[len-1]);
                NUM=j-2;
                if ((Teq==9)||(Teq==13))
                {
                    Tstp=MM;
                    Ts=(int)Tstp;
                    SS=0.02;
                    if (Tstp>100)
                    SS=(Tstp / 100)*SS;
                }
                else
                {
                    Tstp=MM / 10;
                    SS=0.005;
                    if (Tstp>25)
                        SS=(Tstp / 25)*SS;
                 }
            }
            else
            {
                NUM=j-1;
                SS=(Tss/1000);
            }
            if (Teq==4)
            {
                vx=NUM/ 2;
                //if (IE==EE3)
                //    vn=(FE-IE) / (Stp*2);
                //else 
                //    vn=((FE-IE) / Stp)+((FE-EE3) / Stp);
                if (vn>=NUM)
                    onc=true;
                else 
                    onc=false;
            }
            if (Suby)
                if((Tq!=Teq)||(NM!=NUM)||((Teq<8)&&((EE1!=IE)||(EE2!=FE))))
                {
                    
                    return;
                }
                else
                    ser++;
            MaxC=0;
            vn=NUM;
            if (Dbl)
            {
                vn=NUM+Rev;
                vx=NUM+Fore;
            }
            Vry=false;
            k=0;
            for(int i=0;i<= vn-1;i++)
            {
                if (Dbl)
                {
                    if((k==NUM)||(k==vx)) 
                        k=k+4; //8
                    if (k==vn)
                        break;
                }
                if ((Teq==10)||(Teq==13))
                    C[i]=Crnt[i];
                else  
                    C[i]=Crnt[i]*Math.Pow(10,Powr[i]-6);
            }
            if (Teq<8) 
            {   
                if (Teq==4) 
                {
                    IE=IE+(FE-IE) % Stp;
                    EE3=EE3+(FE-EE3) % Stp;
                    if (EE3!=IE) 
                        EE=EE3;
                    else 
                        EE=IE;
                }
                if ((FE-EE)>0) 
                {
                    ngtv=1;
                   // Sign=' ';
                }
                else
                {
                    ngtv=-1;
                   // Sign='-';
                }
            }
            if (Teq<11) 
            {
                k=1;
                for(int i=1;i<=vn-1;i++) 
                {
                    if (Dbl)
                    {
                        if((k==NUM-1)||(k==vx-1))
                            k=k+6;
                        if (k==vn-1)
                            break;
                    }
                    if (Math.Abs(C[k]-C[k-1])>1000)
                        C[k]=(C[k-1]+C[k+1])/ 2;
                    k=k+1;
                }
                k=vn;
                for(int i=vn; i>=2;i--)
                {
                    if ((Dbl)&&((k==Fore+1)||(k==Rev+1)))
                        k=k-5;
                    C[k]=(C[k-1]+C[k])/ 2;
                    k=k-1;
                    if ((Dbl) && (k==1)) 
                        break;
                }
                k=1;
                for (int i=1;i<=vn-2;i++)
                {
                    if (Dbl)
                    {
                        if((k==NUM-1)||(k==vx-1))
                            k=k+6; //6
                        if (k==vn-1)
                            break;
                    }
                    C[k]=(C[k-1]+C[k+1])/ 2;
                    k=k+1;
                }
                for(int j1=3;j1>=0;j1--)
                {
                    k=4;
                    for(int i=4;i<=vn-3;i++)
                    {
                        if (Dbl)
                        {
                            if((k==NUM-2)||(k==vx-2))
                                k=k+10;
                            if (k==vn-2)
                                break;
                        }
                        I0=((C[k-1]-C[k-2])+(C[k-2]-C[k-3])+(C[k-3]-C[k-4]))/ 3;
                        I1=Math.Abs(C[k]-C[k-1]);
                        if (I1>Math.Abs(j1*I0)) 
                            C[k]=(C[k-2]+C[k-1]+C[k+2]+C[k+1])/4;
                        k=k+1;
                    }
                }
                k=3;
                for(int i=3;i<=vn-4;i++)
                {
                    if (Dbl)
                    {
                        if((k==NUM-3)||(k==vx-3))
                            k=k+10;
                        if (k==vn-3)
                            break;
                    }
                    C[k]=(C[k-3]+C[k-2]+C[k-1]+C[k+3]+C[k+2]+C[k+1])/6;
                    k=k+1;
                }
                if ((cl==1)&&(Teq<8))
                {
                    I_tot=0; 
                    MaxC=0; 
                    mx=false;
                    mini=10000;
                    k=NUM;
                    if (Teq==4)
                        k=vx;
                    for(int i=1;i<=k-5;i++)
                    {
                        if ((i>10)&&(C[i]>I_tot)&&(C[i]>=C[i+1])&&(C[i]>=C[i-1]))
                        {
                            I_tot=C[i];
                            MaxC=i;       
                            mx=true;
                        }
                        if (C[i]<mini)
                            mini=C[i];
                    }

                    if (Teq==4)
                    {
                        I_tot=0; 
                        Rev=0; 
                      
                        for(int i=k+10;i<=NUM-10;i++) 
                        {
                            if ((C[i]<I_tot)&&(C[i]<=C[i+1])&&(C[i]<=C[i-1]))
                            {
                                I_tot=C[i];
                                Rev=i;
                              
                            }
                        }
                    }

                }
            }
            else
            {
                for(int j1=2;j1>=1;j1--)
                {
                    for (int i=2 ;i<=NUM-2;i++)
                        if ((C[i]*C[i+1]<0)||(C[i]*C[i-1]<0)||(C[i]*C[i+2]<0)||(C[i]*C[i-2]<0)){
                        }
                        else 
                            C[i]=(C[i-1]+C[i+1])/2;
                }
            }
            if (Teq==12)
                Colm=true;
            if ((((Teq<8)&&(! Dbl)&&(NUM<10000)&&(onc))||(Teq==9)||(Teq>10))&& Colm) 
            {
                C[10000]=0;
                k=10001;
                I_tot=0;
                for(int j1=0;j1<=NUM-1;j1++)
                {
                    I_tot=I_tot+(C[j1]+C[j1+1])*SS/2;
                    C[k]=I_tot;
                    k=k+1;
                }
            }
            else 
            Colm=false;
            
          ///**************************************************/
          if ((((Teq<8)&&(! Dbl)&&(NUM<10000)&&(onc))||(Teq==9)||(Teq>10))&& Drvt) 
          {
              C[10000]=0;      
              k=10001;
              for(int j1=1;j1<=NUM;j1++) 
              {
                  C[k]=(C[j1]-C[j1-1])/SS;
                  k=k+1;
              }
          }
          else 
              Drvt=false;
            //{/**************************************************/
            if ((Teq==4) && (Conv))  //C
            {
                C[Fore]=0;
                k=Fore+1;
                Rev=0;
                for(int j1=1;j1<=NUM;j1++)
                {
                    if (IE+(j1*Stp)==FE) 
                    {
                        Rev=j1;
                        Stp=(byte)((int)((~Stp)+1));
                    }
                    I_tot=0;
                    for(int i=0;i<=j1-1;i++) 
                    {
                        I_tot=I_tot+C[i]*1.0/Math.Sqrt(j1-i);
                    }
                    I_tot=I_tot*Math.Sqrt(Tss)*0.017841241;
                    C[k]=I_tot;
                    k=k+1;
                }
                Stp = (byte)((int)((~Stp) + 1));
            }
            len=NUM;
            if (ctrl) 
            {
                //bat="(t:sec)-1/2";
                for (int i=len -1 ;i>=1;i--)
                {
                    V1=1/Math.Sqrt(SS*i);
                    I1=C[i];
                    if(Teq==9) 
                    {
                        Chart1.Series[ser].Points.AddXY(V1,I1,"",Colr[cl]);
                        //if ((Math.Abs(I1) < 1000) && (Math.Abs(V1) < 1000))
                        //    Writeln(tmpDat,floattostrf(V1,ffNumber,4,3)+"  "+floattostrf(I1,ffNumber,4,3));
                        //else
                        //    Writeln(tmpDat,V1.ToString()+"  "+I1.ToString());
                    }
                    else
                    {
                        Chart1.Series[ser].Points.AddXY(V1,I1*10,"",Colr[cl]);
                        //if ((Math.Abs(I1 * 10) < 1000) && (Math.Abs(V1) < 1000)) 
                        //    Writeln(tmpDat,floattostrf(V1,ffNumber,4,3)+"  "+floattostrf(I1*10,ffNumber,4,3));
                        //else
                        //    Writeln(tmpDat,V1.ToString()+"  "+(I1*10).ToString());
                    }
                }
            }
            else
            {
                k=0;
                a=0;
                if (Teq<8) 
                    Vi=IE+Stp*k;
                k1=0;
                I1=C[0];//$$$$
                if ((Colm)||(Drvt)) 
                {
                    k1=10000;      
                    I1=C[k1];
                }
                if ((Teq==4)&&(Conv))   //C
                {
                    cl=2;
                    ser=2;
                    Chart1.Series[ser].Color=Colr[cl];
                    len=NUM+Fore;
                    k=Fore;
                }
                if ((Dbl)&&(F_or)) 
                {
                    cl=2;
                    ser=1;
                    Chart1.Series[ser].Color = Colr[cl];
                    k=Fore;
                    Len=Num+Fore;
                    F_or=false;
                }
                if ((Dbl)&&(R_vr)) 
                {
                    cl=3;
                    ser=2;
                    Chart1.Series[ser].Color = Colr[cl];
                    k=Rev;
                    Len=Num+Rev;
                    R_vr=false;
                }
                I_tot=0;
                for(int i=k;i<=len-2;i++)
                {
                    if ((Colm)||(Drvt)) 
                        I1=C[k+k1];
                    else  
                        I1=C[k];
                    if (Teq<8) 
                        V1=Vi/1000;
                    else  
                        V1=SS*(i+1);
        if ((I1==0)||((C[k]*C[k-1])<0))
            Ec=V1; 

        if ((Teq==4)&&(cmpn)){} 
        else
           V1=V1-(ngtv*R_cmp*I1)/1000000;
        
        {
          if (grftfl)
          {
           if (I1!=0)
             {
               Chart1.Series[ser].Points.AddXY(V1,Math.Log10(Math.Abs(I1))-6,"",Colr[cl]);
               //if ((Math.Abs(Math.Log10(Math.Abs(I1))-6) < 1000) && (Math.Abs(V1) < 1000))
               //    Writeln(tmpDat, floattostrf(V1, ffNumber, 4, 3) + "  " + floattostrf(Math.Log10(Math.Abs(I1)) - 6, ffNumber, 4, 3));
               // else
               //    Writeln(tmpDat, V1.ToString() + "  " + (Math.Log10(Math.Abs(I1)) - 6).ToString());
             }
          }
          else
          {
           if(Teq==9)
             {
               Chart1.Series[ser].Points.AddXY(V1,I1,"",Colr[cl]);
               //if ((Math.Abs(I1) < 1000) && (Math.Abs(V1) < 1000) )
               //    Writeln(tmpDat,floattostrf(V1,ffNumber,4,3)+"  "+floattostrf(I1,ffNumber,4,3));
               // else
               //    Writeln(tmpDat,V1.ToString()+"  "+I1.ToString());
             }
           else
           {
               Chart1.Series[ser].Points.AddXY(V1,I1*10,"",Colr[cl]);
               //if ((Math.Abs(I1 * 10) < 1000) && (Math.Abs(V1) < 1000))
               //    Writeln(tmpDat,floattostrf(V1,ffNumber,4,3)+"  "+floattostrf(I1*10,ffNumber,4,3));
               // else
               //    Writeln(tmpDat,V1.ToString()+"  "+(I1*10).ToString());
             }
          }
        }
        Vi=Vi+Stp;
        if (Teq==4)
        {
           if (Vi==FE) 
           {
               if (!Draw_Itself) 
                   ser=ser+1;
               Chart1.Series[ser].Color=Colr[cl];           
               //(Chart1.Series[ser] as Tfastlineseries).LinePen.width=Thik[cl];
               Stp = (byte)((int)((~Stp) + 1));
           }
           if (Vi==EE)
           {
              if (ser<30) 
              {
                  if (cl==1) 
                    Frs=k;
                  Lst=k; 
                  if (!Draw_Itself )
                      ser=ser+1;
                  cl=cl+1;
                  c_ycl=c_ycl+1;
                  Chart1.Series[ser].Color=Colr[cl];
                  //(Chart1.Series[ser] as Tfastlineseries).LinePen.width=Thik[cl];
                  Stp = (byte)((int)((~Stp) + 1));
              }
              else 
                  break;
           }
        }
                    k=k+1;
                }
              if (Teq==4)
                  Stp = (byte)((int)((~Stp) + 1));
            }
           
            if ((Teq<8)&&(cl==1)&&(!grftfl)&&(!Colm)&&(!Drvt))
            {
                I0=10000; 
                Mp=(25 / Math.Abs(Stp));
                if (Mp<5)  
                    Mp=5;
                if (mx){
                    for(int j1=MaxC-(MaxC / 4);j1>=Mp;j1--)
                    {
                        A0=0; 
                        A1=0; 
                        B0=0; 
                        
                        AB=0;
                        V1=(IE+Stp*j1)/1000;
                        for(int i=j1;i>=(j1-Mp+1);i--)
                        {   
                            V1=V1-Stp/1000;
                            A0=A0+V1;
                            A1=A1+V1*V1;
                            B0=B0+C[i];
                            AB=AB+V1*C[i];
                        }
                        AB=Mp*AB-A0*B0; 
                        A2=Mp*A1-A0*A0; 
                        b=AB/A2;
                        ai=(B0-b*A0)/Mp;

                        if (Math.Abs(b) < I0)
                        {
                            I0 = Math.Abs(b);
                            I1=b;
                            I_tot=ai;
                            k=j;//-Mp;
                        }
                    }
                    if ((Teq==2)||(Teq==3)||(Teq==7))
                    {
                        I0=10000;
                        for(int j1=MaxC+(MaxC/ 4);j1<=NUM;j1++) 
                        {
                            A0=0;
                            A1=0;
                            B0=0;
                          
                            AB=0;
                            V1=(IE+Stp*j1)/1000;
                            for(int i=j1;i<=(j1+Mp-1);i++)
                            {
                                V1=V1+Stp/1000;
                                A0=A0+V1;
                                A1=A1+V1*V1;
                                B0=B0+C[i];
                                AB=AB+V1*C[i];
                            }
                            AB=Mp*AB-A0*B0; // AB=AB-(A0*B0)/n;
                            A2=Mp*A1-A0*A0; // A2=A1-(A0*A0)/n;
                            b=AB/A2;
                            ai=(B0-b*A0)/Mp; // a=(B0/n-b*(A0/n));
                            if (Math.Abs(b)<I0)
                            {
                                I0=Math.Abs(b);
                                I1=b;
                                //   SS=ai;
                                Difr=j1;//-Mp;
                            }
                        }
                        I1=(C[Difr]-C[k])/((Difr-k)*Stp/1000);
                        I_tot=C[k]-I1*(IE+k*Stp)/1000;
                    }
                    AB=I1*(IE+MaxC*Stp)/1000+I_tot;
                }
            }
        }
    }
}