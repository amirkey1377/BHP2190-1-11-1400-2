using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BHP2190.classes
{
  public  class class_params
    {
        
        //CV Properties *******************************************************************
        [DefaultPropertyAttribute("E1")]

        public class CV_params
        {
            private double _Cycles;
            private double _E1;
            private double _E2;
            private double _E3;
            private double _ScanRate;
            private double _HStep;
            private double _TStep;
            private double _EquilibriumTime;
            private double _HoldTime;
            private string _comment1 = "";

            [CategoryAttribute("CV Values"), DescriptionAttribute("Number of complete cycle(s) scan\n 1    15")]
            public double Cycles
            {
                get
                {
                    return _Cycles;
                }

                set
                {
                    _Cycles = value;
                }
            }

            [CategoryAttribute("CV Values"), DescriptionAttribute("Initial potential(V)\n -8    +8")]
            public double E1
            {
                get
                {
                    return _E1;
                }

                set
                {
                    _E1 = value;
                }
            }

            [CategoryAttribute("CV Values"), DescriptionAttribute("High limit of potential scan(V)\n -8     +8")]
            public double E2
            {
                get
                {
                    return _E2;
                }

                set
                {
                    _E2 = value;
                }
            }

            [CategoryAttribute("CV Values"), DescriptionAttribute("Low limit of potential scan(V)\n -8    +8")]
            public double E3
            {
                get
                {
                    return _E3;
                }

                set
                {
                    _E3 = value;
                }
            }

            [CategoryAttribute("CV Values"), DescriptionAttribute("Step height (scan increment)(V)\n 0.001    0.025")]
            public double HStep
            {
                get
                {
                    return _HStep;
                }

                set
                {
                    _HStep = value;
                }
            }

            [CategoryAttribute("CV Values"), DescriptionAttribute("Step time(Hstep/ScanRate)(S)\n 0.0001    10")]
            [ReadOnlyAttribute(true)]
            public double TStep
            {
                get
                {
                    return _TStep;
                }

                set
                {
                    _TStep = value;
                }
            }

            [CategoryAttribute("CV Values"), DescriptionAttribute(" Potential scan rate(V/S)\n 0.0001    250")]
            [DisplayName("Scan Rate")]
            public double ScanRate
            {
                get
                {
                    return _ScanRate;
                }

                set
                {
                    _ScanRate = value;
                }
            }

            [CategoryAttribute("CV Values"), DescriptionAttribute("Quiescent time before potential scan(S)\n 0    2000")]
            [DisplayName("Equilibrium Time")]
            public double EquilibriumTime
            {
                get
                {
                    return _EquilibriumTime;
                }

                set
                {
                    _EquilibriumTime = value;
                }
            }
            [CategoryAttribute("CV Values"), DescriptionAttribute("Holding time after reaching E2(S)\n 0    99")]
            [DisplayName("HoldTime")]
            public double HoldTime
            {
                get
                {
                    return _HoldTime;
                }

                set
                {
                    _HoldTime = value;
                }
            }

            [CategoryAttribute("CV Values"), DescriptionAttribute("detail of expriment")]
            [DisplayName("Comment")]
            public string comment1
            {
                get
                {
                    return _comment1;
                }

                set
                {
                    _comment1 = value;
                }
            }

            public CV_params()
            {
                //
                // TODO: Add constructor logic here
                //
            }

            // LSV Properties *******************************************************************
            public class LSV_params
            {
                private double _E1;
                private double _E2;
                private double _ScanRate;
                private double _HStep;
                private double _TStep;
                private double _EquilibriumTime;
                private string _comment1;
                [CategoryAttribute("LSV Values"), DescriptionAttribute("Initial potential(V)\n -8    +8")]
                public double E1
                {
                    get
                    {
                        return _E1;
                    }

                    set
                    {
                        _E1 = value;
                    }
                }

                [CategoryAttribute("LSV Values"), DescriptionAttribute("Final potential(V)\n -8    +8")]
                public double E2
                {
                    get
                    {
                        return _E2;
                    }

                    set
                    {
                        _E2 = value;
                    }
                }

                [CategoryAttribute("LSV Values"), DescriptionAttribute("Step height (scan increment)(V)\n 0.001    0.025")]
                public double HStep
                {
                    get
                    {
                        return _HStep;
                    }

                    set
                    {
                        _HStep = value;
                    }
                }

                [CategoryAttribute("LSV Values"), DescriptionAttribute("Step time(Hstep/ScanRate)(S)\n 0.0001    10")]
                [ReadOnlyAttribute(true)]
                public double TStep
                {
                    get
                    {
                        return _TStep;
                    }

                    set
                    {
                        _TStep = value;
                    }
                }

                [CategoryAttribute("LSV Values"), DescriptionAttribute("potential scan rate(V/S)\n 0.0001    250")]
                [DisplayName("Scan Rate")]
                public double ScanRate
                {
                    get
                    {
                        return _ScanRate;
                    }

                    set
                    {
                        _ScanRate = value;
                    }
                }

                [CategoryAttribute("LSV Values"), DescriptionAttribute("Quiescent time before potential scan(S)\n 0    2000")]
                [DisplayName("Equilibrium Time")]
                public double EquilibriumTime
                {
                    get
                    {
                        return _EquilibriumTime;
                    }

                    set
                    {
                        _EquilibriumTime = value;
                    }
                }
                [CategoryAttribute("LSV Values"), DescriptionAttribute("detail of expriment")]
                public string comment1
                {
                    get
                    {
                        return _comment1;
                    }

                    set
                    {
                        _comment1 = value;
                    }
                }
                public LSV_params()
                {
                    //
                    // TODO: Add constructor logic here
                    //
                }
            }

            // DCS Properties  ******************************************************************* 
            public class DCS_params
            {
                private double _E1;
                private double _E2;
                private double _ScanRate;
                private double _HStep;
                private double _TStep;
                private double _EquilibriumTime;
                private string _comment1;


                [CategoryAttribute("DCS Values"), DescriptionAttribute("Initial potential(V)\n -8    +8")]
                public double E1
                {
                    get
                    {
                        return _E1;
                    }

                    set
                    {
                        _E1 = value;
                    }
                }

                [CategoryAttribute("DCS Values"), DescriptionAttribute("Final potential(V)\n -8    +8")]
                public double E2
                {
                    get
                    {
                        return _E2;
                    }

                    set
                    {
                        _E2 = value;
                    }
                }

                [CategoryAttribute("DCS Values"), DescriptionAttribute("Step height (scan increment)(V)\n 0.001    0.025")]
                public double HStep
                {
                    get
                    {
                        return _HStep;
                    }

                    set
                    {
                        _HStep = value;
                    }
                }

                [CategoryAttribute("DCS Values"), DescriptionAttribute("Step time(Hstep/ScanRate)(S)\n 0.1    10")]
                [ReadOnlyAttribute(true)]
                public double TStep
                {
                    get
                    {
                        return _TStep;
                    }

                    set
                    {
                        _TStep = value;
                    }
                }

                [CategoryAttribute("DCS Values"), DescriptionAttribute("potential scan rate(V/S)\n 0.0001    0.25")]
                [DisplayName("Scan Rate")]
                public double ScanRate
                {
                    get
                    {
                        return _ScanRate;
                    }

                    set
                    {
                        _ScanRate = value;
                    }
                }

                [CategoryAttribute("DCS Values"), DescriptionAttribute("Quiescent time before potential scan(S)\n 0    2000")]
                [DisplayName("Equilibrium Time")]
                public double EquilibriumTime
                {
                    get
                    {
                        return _EquilibriumTime;
                    }

                    set
                    {
                        _EquilibriumTime = value;
                    }
                }
                [CategoryAttribute("DCS Values"), DescriptionAttribute("detail of expriment")]
                public string comment1
                {
                    get
                    {
                        return _comment1;
                    }

                    set
                    {
                        _comment1 = value;
                    }
                }
                public DCS_params()
                {
                    //
                    // TODO: Add constructor logic here
                    //
                }
            }

            //DCV Properties ******************************************************************
            public class DCV_params
            {
                private double _E1;
                private double _E2;
                private double _ScanRate;
                private double _HStep;
                private double _TStep;
                private double _EquilibriumTime;
                private string _comment1;

                [CategoryAttribute("DCV Values"), DescriptionAttribute("Initial potential(V)\n -8    +8")]
                public double E1
                {
                    get
                    {
                        return _E1;
                    }

                    set
                    {
                        _E1 = value;
                    }
                }

                [CategoryAttribute("DCV Values"), DescriptionAttribute("Final potential(V)\n -8    +8")]
                public double E2
                {
                    get
                    {
                        return _E2;
                    }

                    set
                    {
                        _E2 = value;
                    }
                }

                [CategoryAttribute("DCV Values"), DescriptionAttribute("Step height (scan increment)(V)\n 0.001    0.025")]
                public double HStep
                {
                    get
                    {
                        return _HStep;
                    }

                    set
                    {
                        _HStep = value;
                    }
                }

                [CategoryAttribute("DCV Values"), DescriptionAttribute("Step time(Hstep/ScanRate)(S)\n 0.1    10")]
                [ReadOnlyAttribute(true)]
                public double TStep
                {
                    get
                    {
                        return _TStep;
                    }

                    set
                    {
                        _TStep = value;
                    }
                }

                [CategoryAttribute("DCV Values"), DescriptionAttribute("Potential scan rate(V/S)\n 0.0001    0.25")]
                [DisplayName("Scan Rate")]
                public double ScanRate
                {
                    get
                    {
                        return _ScanRate;
                    }

                    set
                    {
                        _ScanRate = value;
                    }
                }

                [CategoryAttribute("DCV Values"), DescriptionAttribute("Quiescent time before potential scan(S)\n 0    2000")]
                [DisplayName("Equilibrium Time")]
                public double EquilibriumTime
                {
                    get
                    {
                        return _EquilibriumTime;
                    }

                    set
                    {
                        _EquilibriumTime = value;
                    }
                }
                [CategoryAttribute("DCV Values"), DescriptionAttribute("detail of expriment")]
                public string comment1
                {
                    get
                    {
                        return _comment1;
                    }

                    set
                    {
                        _comment1 = value;
                    }
                }
                public DCV_params()
                {
                    //
                    // TODO: Add constructor logic here
                    //
                }
            }

            //NPV Properties  ****************************************************************
            public class NPV_params
            {
                private double _E1;
                private double _E2;
                private double _ScanRate;
                private double _HStep;
                private double _TStep;
                private double _PulseWidth;
                private double _EquilibriumTime;
                private string _comment1;

                [CategoryAttribute("NPV Values"), DescriptionAttribute("Initial potential(V)\n -8    +8")]
                public double E1
                {
                    get
                    {
                        return _E1;
                    }

                    set
                    {
                        _E1 = value;
                    }
                }

                [CategoryAttribute("NPV Values"), DescriptionAttribute("Final potential(V)\n -8    +8")]
                public double E2
                {
                    get
                    {
                        return _E2;
                    }

                    set
                    {
                        _E2 = value;
                    }
                }

                [CategoryAttribute("NPV Values"), DescriptionAttribute("Step height (scan increment)(V)\n 0.001    0.025")]
                public double HStep
                {
                    get
                    {
                        return _HStep;
                    }

                    set
                    {
                        _HStep = value;
                    }
                }

                [CategoryAttribute("NPV Values"), DescriptionAttribute("Step time(Hstep/ScanRate)(S)\n 0.1    10")]
                [ReadOnlyAttribute(true)]
                public double TStep
                {
                    get
                    {
                        return _TStep;
                    }

                    set
                    {
                        _TStep = value;
                    }
                }

                [CategoryAttribute("NPV Values"), DescriptionAttribute("potential scan rate(V/S)\n 0.0001    0.25")]
                [DisplayName("Scan Rate")]
                public double ScanRate
                {
                    get
                    {
                        return _ScanRate;
                    }

                    set
                    {
                        _ScanRate = value;
                    }
                }

                [CategoryAttribute("NPV Values"), DescriptionAttribute("Potential pulse time(S)\n 0.05    (TStep-0.05)")]
                [DisplayName("Pulse Width")]
                public double PulseWidth
                {
                    get
                    {
                        return _PulseWidth;
                    }

                    set
                    {
                        _PulseWidth = value;
                    }
                }

                [CategoryAttribute("NPV Values"), DescriptionAttribute("Quiescent time before potential scan(S)\n 0    2000")]
                [DisplayName("Equilibrium Time")]
                public double EquilibriumTime
                {
                    get
                    {
                        return _EquilibriumTime;
                    }

                    set
                    {
                        _EquilibriumTime = value;
                    }
                }
                [CategoryAttribute("NPV Values"), DescriptionAttribute("detail of expriment")]
                public string comment1
                {
                    get
                    {
                        return _comment1;
                    }

                    set
                    {
                        _comment1 = value;
                    }
                }
                public NPV_params()
                {
                    //
                    // TODO: Add constructor logic here
                    //
                }
            }

            //DPV Properties  ****************************************************************
            public class DPV_params
            {
                private double _E1;
                private double _E2;
                private double _ScanRate;
                private double _HStep;
                private double _TStep;
                private double _PulseWidth;
                private double _PulseHeight;
                private double _EquilibriumTime;
                private string _comment1;

                [CategoryAttribute("DPV Values"), DescriptionAttribute("Initial potential(V)\n -8    +8")]
                public double E1
                {
                    get
                    {
                        return _E1;
                    }

                    set
                    {
                        _E1 = value;
                    }
                }

                [CategoryAttribute("DPV Values"), DescriptionAttribute("Final potential(V)\n -8    +8")]
                public double E2
                {
                    get
                    {
                        return _E2;
                    }

                    set
                    {
                        _E2 = value;
                    }
                }

                [CategoryAttribute("DPV Values"), DescriptionAttribute("Step height (scan increment)(V)\n 0.001    0.025")]
                public double HStep
                {
                    get
                    {
                        return _HStep;
                    }

                    set
                    {
                        _HStep = value;
                    }
                }

                [CategoryAttribute("DPV Values"), DescriptionAttribute("Step time(Hstep/ScanRate)(S)\n 0.1    10")]
                [ReadOnlyAttribute(true)]
                public double TStep
                {
                    get
                    {
                        return _TStep;
                    }

                    set
                    {
                        _TStep = value;
                    }
                }

                [CategoryAttribute("DPV Values"), DescriptionAttribute("Potential scan rate(V/S)\n 0.0001    0.25")]
                [DisplayName("Scan Rate")]
                public double ScanRate
                {
                    get
                    {
                        return _ScanRate;
                    }

                    set
                    {
                        _ScanRate = value;
                    }
                }

                [CategoryAttribute("DPV Values"), DescriptionAttribute("Potential pulse time(S)\n 0.05    (TStep-0.05)")]
                [DisplayName("Pulse Width")]
                public double PulseWidth
                {
                    get
                    {
                        return _PulseWidth;
                    }

                    set
                    {
                        _PulseWidth = value;
                    }
                }

                [CategoryAttribute("DPV Values"), DescriptionAttribute("Potential Pulse Amplitude superimposed(V)\n 0.001    0.25")]
                [DisplayName("Pulse Height")]
                public double PulseHeight
                {
                    get
                    {
                        return _PulseHeight;
                    }

                    set
                    {
                        _PulseHeight = value;
                    }
                }

                [CategoryAttribute("DPV Values"), DescriptionAttribute("Quiescent time before potential scan(S)\n 0    2000")]
                [DisplayName("Equilibrium Time")]
                public double EquilibriumTime
                {
                    get
                    {
                        return _EquilibriumTime;
                    }

                    set
                    {
                        _EquilibriumTime = value;
                    }
                }
                [CategoryAttribute("DPV Values"), DescriptionAttribute("detail of expriment")]
                public string comment1
                {
                    get
                    {
                        return _comment1;
                    }

                    set
                    {
                        _comment1 = value;
                    }
                }
                public DPV_params()
                {
                    //
                    // TODO: Add constructor logic here
                    //
                }
            }

            //SWV Properties  ****************************************************************
            public class SWV_params
            {
                private double _E1;
                private double _E2;
                private double _ScanRate;
                private double _Frequency;
                private double _HStep;
                private double _PulseHeight;
                private double _EquilibriumTime;
                private string _comment1;

                [CategoryAttribute("SWV Values"), DescriptionAttribute("Initial potential(V)\n -8    +8")]
                public double E1
                {
                    get
                    {
                        return _E1;
                    }

                    set
                    {
                        _E1 = value;
                    }
                }

                [CategoryAttribute("SWV Values"), DescriptionAttribute("Final potential(V)\n -8    +8")]
                public double E2
                {
                    get
                    {
                        return _E2;
                    }

                    set
                    {
                        _E2 = value;
                    }
                }

                [CategoryAttribute("SWV Values"), DescriptionAttribute("Square wave frequency(Hstep/ScanRate)(Hz)\n 1    1000")]
                [ReadOnlyAttribute(true)]
                public double Frequency
                {
                    get
                    {
                        return _Frequency;
                    }

                    set
                    {
                        _Frequency = value;
                    }
                }

                [CategoryAttribute("SWV Values"), DescriptionAttribute("Step height (scan increment)(V)\n 0.001    0.025")]
                public double HStep
                {
                    get
                    {
                        return _HStep;
                    }

                    set
                    {
                        _HStep = value;
                    }
                }

                [CategoryAttribute("SWV Values"), DescriptionAttribute("potential scan rate(V/S)\n 0.001    25")]
                [DisplayName("Scan Rate")]
                public double ScanRate
                {
                    get
                    {
                        return _ScanRate;
                    }

                    set
                    {
                        _ScanRate = value;
                    }
                }

                [CategoryAttribute("SWV Values"), DescriptionAttribute("Potential Pulse Amplitude superimposed(V)\n .001    0.25")]
                [DisplayName("Pulse Height")]
                public double PulseHeight
                {
                    get
                    {
                        return _PulseHeight;
                    }

                    set
                    {
                        _PulseHeight = value;
                    }
                }

                [CategoryAttribute("SWV Values"), DescriptionAttribute("Quiescent time before potential scan(S)\n 0    2000")]
                [DisplayName("Equilibrium Time")]
                public double EquilibriumTime
                {
                    get
                    {
                        return _EquilibriumTime;
                    }

                    set
                    {
                        _EquilibriumTime = value;
                    }
                }
                [CategoryAttribute("SWV Values"), DescriptionAttribute("detail of expriment")]
                public string comment1
                {
                    get
                    {
                        return _comment1;
                    }

                    set
                    {
                        _comment1 = value;
                    }
                }
                public SWV_params()
                {
                    //
                    // TODO: Add constructor logic here
                    //
                }
            }

            //DPS Properties  ****************************************************************
            public class DPS_params
            {
                private double _E1;
                private double _E2;
                private double _ScanRate;
                private double _HStep;
                private double _TStep;
                private double _PulseWidth;
                private double _PulseHeight;
                private double _EquilibriumTime;
                private string _comment1;

                [CategoryAttribute("DPS Values"), DescriptionAttribute("Initial potential(V)\n -8    +8")]
                public double E1
                {
                    get
                    {
                        return _E1;
                    }

                    set
                    {
                        _E1 = value;
                    }
                }

                [CategoryAttribute("DPS Values"), DescriptionAttribute("Final potential(V)\n -8    +8")]
                public double E2
                {
                    get
                    {
                        return _E2;
                    }

                    set
                    {
                        _E2 = value;
                    }
                }

                [CategoryAttribute("DPS Values"), DescriptionAttribute("Step height (scan increment)(V)\n 0.001    0.025")]
                public double HStep
                {
                    get
                    {
                        return _HStep;
                    }

                    set
                    {
                        _HStep = value;
                    }
                }

                [CategoryAttribute("DPS Values"), DescriptionAttribute("Step time(Hstep/ScanRate)(S)\n 0.1    10")]
                [ReadOnlyAttribute(true)]
                public double TStep
                {
                    get
                    {
                        return _TStep;
                    }

                    set
                    {
                        _TStep = value;
                    }
                }

                [CategoryAttribute("DPS Values"), DescriptionAttribute("potential scan rate(V/S)\n 0.0001    0.25")]
                [DisplayName("Scan Rate")]
                public double ScanRate
                {
                    get
                    {
                        return _ScanRate;
                    }

                    set
                    {
                        _ScanRate = value;
                    }
                }

                [CategoryAttribute("DPS Values"), DescriptionAttribute("Potential pulse time(S)\n 0.05    (TStep-0.05)")]
                [DisplayName("Pulse Width")]
                public double PulseWidth
                {
                    get
                    {
                        return _PulseWidth;
                    }

                    set
                    {
                        _PulseWidth = value;
                    }
                }

                [CategoryAttribute("DPS Values"), DescriptionAttribute("Potential Pulse Amplitude superimposed(V)\n 0.001    0.25")]
                [DisplayName("Pulse Height")]
                public double PulseHeight
                {
                    get
                    {
                        return _PulseHeight;
                    }

                    set
                    {
                        _PulseHeight = value;
                    }
                }

                [CategoryAttribute("DPS Values"), DescriptionAttribute("Quiescent time before potential scan(S)\n 0    2000")]
                [DisplayName("Equilibrium Time")]
                public double EquilibriumTime
                {
                    get
                    {
                        return _EquilibriumTime;
                    }

                    set
                    {
                        _EquilibriumTime = value;
                    }
                }
                [CategoryAttribute("DPS Values"), DescriptionAttribute("detail of expriment")]
                public string comment1
                {
                    get
                    {
                        return _comment1;
                    }

                    set
                    {
                        _comment1 = value;
                    }
                }
                public DPS_params()
                {
                    //
                    // TODO: Add constructor logic here
                    //
                }
            }

            //CPC Properties  ****************************************************************
            public class CPC_params
            {
                private double _E1;
                private double _T1;
                private double _EquilibriumTime;
                private string _comment1;

                [CategoryAttribute("CPC Values"), DescriptionAttribute("Step potential(V)\n -8    +8")]
                public double E1
                {
                    get
                    {
                        return _E1;
                    }

                    set
                    {
                        _E1 = value;
                    }
                }

                [CategoryAttribute("CPC Values"), DescriptionAttribute("Step time(S)\n 1    60000")]
                public double T1
                {
                    get
                    {
                        return _T1;
                    }

                    set
                    {
                        _T1 = value;
                    }
                }

                [CategoryAttribute("CPC Values"), DescriptionAttribute("Quiescent time before start measuring(S)\n 0    2000")]
                [DisplayName("Equilibrium Time")]
                public double EquilibriumTime
                {
                    get
                    {
                        return _EquilibriumTime;
                    }

                    set
                    {
                        _EquilibriumTime = value;
                    }
                }
                [CategoryAttribute("CPC Values"), DescriptionAttribute("detail of expriment")]
                public string comment1
                {
                    get
                    {
                        return _comment1;
                    }

                    set
                    {
                        _comment1 = value;
                    }
                }
                public CPC_params()
                {
                    //
                    // TODO: Add constructor logic here
                    //
                }
            }


            //CHP Properties  ****************************************************************
            public class CHP_params
            {
                private double _I1;
                private double _T1;
                private double _I2;
                private double _T2;
                private double _EquilibriumTime;
                private string _comment1;

                [CategoryAttribute("CHP Values"), DescriptionAttribute("First current step(mA)\n -8    +8")]
                public double I1
                {
                    get
                    {
                        return _I1;
                    }

                    set
                    {
                        _I1 = value;
                    }
                }
                [CategoryAttribute("CHP Values"), DescriptionAttribute("First step time(S)\n 1    32000")]
                public double T1
                {
                    get
                    {
                        return _T1;
                    }

                    set
                    {
                        _T1 = value;
                    }
                }

                [CategoryAttribute("CHP Values"), DescriptionAttribute("Second current step(mA)\n -8    +8")]
                public double I2
                {
                    get
                    {
                        return _I2;
                    }

                    set
                    {
                        _I2 = value;
                    }
                }
                [CategoryAttribute("CHP Values"), DescriptionAttribute("Second step time(S)\n 1    32000-T1")]
                public double T2
                {
                    get
                    {
                        return _T2;
                    }

                    set
                    {
                        _T2 = value;
                    }
                }

                [CategoryAttribute("CHP Values"), DescriptionAttribute("Quiescent time before start measuring(S)\n 0    2000")]
                [DisplayName("Equilibrium Time")]
                public double EquilibriumTime
                {
                    get
                    {
                        return _EquilibriumTime;
                    }

                    set
                    {
                        _EquilibriumTime = value;
                    }
                }
                [CategoryAttribute("CHP Values"), DescriptionAttribute("detail of expriment")]
                public string comment1
                {
                    get
                    {
                        return _comment1;
                    }

                    set
                    {
                        _comment1 = value;
                    }
                }
                public CHP_params()
                {
                    //
                    // TODO: Add constructor logic here
                    //
                }
            }

            //CHA Properties  ****************************************************************
            public class CHA_params
            {
                private double _E1;
                private double _T1;
                private double _E2;
                private double _T2;
                private double _EquilibriumTime;
                private string _comment1;

                [CategoryAttribute("CHA Values"), DescriptionAttribute("First potential step(V)\n -8    +8")]
                public double E1
                {
                    get
                    {
                        return _E1;
                    }

                    set
                    {
                        _E1 = value;
                    }
                }
                [CategoryAttribute("CHA Values"), DescriptionAttribute("First step time(S)\n 1    32000")]
                public double T1
                {
                    get
                    {
                        return _T1;
                    }

                    set
                    {
                        _T1 = value;
                    }
                }

                [CategoryAttribute("CHA Values"), DescriptionAttribute("second potential step(V)\n -8    +8")]
                public double E2
                {
                    get
                    {
                        return _E2;
                    }

                    set
                    {
                        _E2 = value;
                    }
                }
                [CategoryAttribute("CHA Values"), DescriptionAttribute("second step time(S)\n 1    32000-T1")]
                public double T2
                {
                    get
                    {
                        return _T2;
                    }

                    set
                    {
                        _T2 = value;
                    }
                }

                [CategoryAttribute("CHA Values"), DescriptionAttribute("Quiescent time before start measuring(S)\n 0    2000")]
                [DisplayName("Equilibrium Time")]
                public double EquilibriumTime
                {
                    get
                    {
                        return _EquilibriumTime;
                    }

                    set
                    {
                        _EquilibriumTime = value;
                    }
                }
                [CategoryAttribute("CHA Values"), DescriptionAttribute("detail of expriment")]
                public string comment1
                {
                    get
                    {
                        return _comment1;
                    }

                    set
                    {
                        _comment1 = value;
                    }
                }
                public CHA_params()
                {
                    //
                    // TODO: Add constructor logic here
                    //
                }
            }


            //CHC Properties  ****************************************************************
            public class CHC_params
            {
                private double _E1;
                private double _T1;
                private double _E2;
                private double _T2;
                private double _EquilibriumTime;
                private string _comment1;

                [CategoryAttribute("CHC Values"), DescriptionAttribute("First potential step(V)\n -8    +8")]
                public double E1
                {
                    get
                    {
                        return _E1;
                    }

                    set
                    {
                        _E1 = value;
                    }
                }
                [CategoryAttribute("CHC Values"), DescriptionAttribute("First step time(S)\n 1    32000")]
                public double T1
                {
                    get
                    {
                        return _T1;
                    }

                    set
                    {
                        _T1 = value;
                    }
                }

                [CategoryAttribute("CHC Values"), DescriptionAttribute("Second potential step(V)\n -8    +8")]
                public double E2
                {
                    get
                    {
                        return _E2;
                    }

                    set
                    {
                        _E2 = value;
                    }
                }
                [CategoryAttribute("CHC Values"), DescriptionAttribute("Second step time(S)\n 1    32000-T1")]
                public double T2
                {
                    get
                    {
                        return _T2;
                    }

                    set
                    {
                        _T2 = value;
                    }
                }

                [CategoryAttribute("CHC Values"), DescriptionAttribute("Quiescent time before start measuring(S)\n 0    2000")]
                [DisplayName("Equilibrium Time")]
                public double EquilibriumTime
                {
                    get
                    {
                        return _EquilibriumTime;
                    }

                    set
                    {
                        _EquilibriumTime = value;
                    }
                }
                [CategoryAttribute("CHC Values"), DescriptionAttribute("detail of expriment")]
                public string comment1
                {
                    get
                    {
                        return _comment1;
                    }

                    set
                    {
                        _comment1 = value;
                    }
                }
                public CHC_params()
                {
                    //
                    // TODO: Add constructor logic here
                    //
                }
            }


            //OCP Properties  ****************************************************************
            public class OCP_params
            {
                private double _Time;
                private string _comment1;

                [CategoryAttribute("OCP Values"), DescriptionAttribute("Time(S)\n 0    +65000")]
                public double Time
                {
                    get
                    {
                        return _Time;
                    }

                    set
                    {
                        _Time = value;
                    }
                }
                [CategoryAttribute("OCP Values"), DescriptionAttribute("detail of expriment")]
                public string comment1
                {
                    get
                    {
                        return _comment1;
                    }

                    set
                    {
                        _comment1 = value;
                    }
                }
                public OCP_params()
                {
                    //
                    // TODO: Add constructor logic here
                    //
                }
            }

            [DefaultPropertyAttribute("BackGroundColor")]
            public class Color_params
            {
                //private CV2_params _AX;
                private Color _BackGround;
                //private Color _ForeColorButtons;
                //private Color _BackColorButtons;
                //private Color _ForeColorLable;
                private Color _ForeColorParameterGrid;
                private Color _BackColorParameterGrid;
                private Color _ForeColorDataGrid;
                private Color _BackColorDataGrid;
                private Color _ChartBackground;

                [CategoryAttribute("Color Values"), DescriptionAttribute("BackGround Color")]
                public Color BackGround
                {
                    get
                    {
                        return _BackGround;
                    }

                    set
                    {
                        _BackGround = value;
                    }
                }

        
                [CategoryAttribute("Color Values"), DescriptionAttribute("Parameter's Grid ForeColor")]
                public Color ForeColorParameterGrid
                {
                    get
                    {
                        return _ForeColorParameterGrid;
                    }

                    set
                    {
                        _ForeColorParameterGrid = value;
                    }
                }

                [CategoryAttribute("Color Values"), DescriptionAttribute("Parameter's Grid BackColor")]
                public Color BackColorParameterGrid
                {
                    get
                    {
                        return _BackColorParameterGrid;
                    }

                    set
                    {
                        _BackColorParameterGrid = value;
                    }
                }

                [CategoryAttribute("Color Values"), DescriptionAttribute("Data Grid's ForeColor")]
                public Color ForeColorDataGrid
                {
                    get
                    {
                        return _ForeColorDataGrid;
                    }

                    set
                    {
                        _ForeColorDataGrid = value;
                    }
                }

                [CategoryAttribute("Color Values"), DescriptionAttribute("Data Grid's BackColor")]
                public Color BackColorDataGrid
                {
                    get
                    {
                        return _BackColorDataGrid;
                    }

                    set
                    {
                        _BackColorDataGrid = value;
                    }
                }

                [CategoryAttribute("Color Values"), DescriptionAttribute("Chart Background Color")]
                public Color ChartBackground
                {
                    get
                    {
                        return _ChartBackground;
                    }

                    set
                    {
                        _ChartBackground = value;
                    }
                }
                public Color_params()
                {
                    //
                    // TODO: Add constructor logic here
                    //
                }
            }

        }
                 
    }
}
