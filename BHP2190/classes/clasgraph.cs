using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace BHP2190.classes
{
     public class clasgraph    //این کلاس تنظیمات لازم هر نمودار را نسبت به تکنیک ست می کند
    {
        //frmanalyseGraph1 grf = new frmanalyseGraph1();

        private Color _BackColor;
        private Image _BackGroundImage;
        private ImageLayout _BackGroundImageLayout;
        //private fr _fr;
        private Axis _Axesx;
        private Axis _Axesy;

        private DockStyle _Dock;
        private bool _Enabled;
        private string _Footer;
        private string _Header;
        private Legend _Legend;
     //   private Page _Page;
        private string _SubFooter;
        private string _SubHeader;
    
        private SeriesCollection _Series;
      //  private Walls _Walls;
       // private int _Zoom;
        private ComboBox _SmoothMode;
        private bool _Visible;
                 
       
        [CategoryAttribute("Values"), DescriptionAttribute("Axesx")]
        public Axis Axesx
        {
            get
            {
                return _Axesx;
            }

            set
            {
                _Axesx = value;
            }
        }
        [CategoryAttribute("Values"), DescriptionAttribute("Axesy")]
        public Axis Axesy
        {
            get
            {
                return _Axesy;
            }

            set
            {
                _Axesy = value;
            }
        }
        [CategoryAttribute("Values"), DescriptionAttribute("Dock")]
        public DockStyle Dock
        {
            get
            {
                return _Dock;
            }

            set
            {
                _Dock = value;
            }
        }

        [CategoryAttribute("Values"), DescriptionAttribute("BackGround Image Layout")]
        public ImageLayout BackGroundImageLayout
        {
            get
            {
                return _BackGroundImageLayout;
            }

            set
            {
                _BackGroundImageLayout = value;
            }
        }

        [CategoryAttribute("Values"), DescriptionAttribute("BackColor")]
            public Color BackColor
            {
                get
                {
                    return _BackColor;
                }

                set
                {
                    _BackColor = value;
                }
            }

        [CategoryAttribute("Values"), DescriptionAttribute("BackGround Image")]
        public Image BackGroundImage
        {
            get
            {
                return _BackGroundImage;
            }

            set
            {
                _BackGroundImage = value;
            }
        }

        [CategoryAttribute("Values"), DescriptionAttribute("Enabled")]
        public bool Enabled
        {
            get
            {
                return _Enabled;
            }

            set
            {
                _Enabled = value;
            }
        }

        [CategoryAttribute("Values"), DescriptionAttribute("Footer")]
        public string Footer
        {
            get
            {
                return _Footer;
            }

            set
            {
                _Footer = value;
            }
        }

        [CategoryAttribute("Values"), DescriptionAttribute("Header")]
        public string Header
        {
            get
            {
                return _Header;
            }

            set
            {
                _Header = value;
            }
        }

        [CategoryAttribute("Values"), DescriptionAttribute("SubFooter")]
        public string SubFooter
        {
            get
            {
                return _SubFooter;
            }

            set
            {
                _SubFooter = value;
            }
        }

        [CategoryAttribute("Values"), DescriptionAttribute("SubHeader")]
        public string SubHeader
        {
            get
            {
                return _SubHeader;
            }

            set
            {
                _SubHeader = value;
            }
        }

        [CategoryAttribute("Values"), DescriptionAttribute("Legend")]
        public Legend Legend
        {
            get
            {
                return _Legend;
            }

            set
            {
                _Legend = value;
            }
        }

        

        [CategoryAttribute("Values"), DescriptionAttribute("Series")]
        public SeriesCollection Series
        {
            get
            {
                return _Series;
            }

            set
            {
                _Series = value;
            }
        }

      
        [CategoryAttribute("Values"), DescriptionAttribute("Visibility")]
        public bool Visible
        {
            get
            {
                return _Visible;
            }

            set
            {
                _Visible = value;
            }
        }

        [CategoryAttribute("Values"), DescriptionAttribute("SmoothMode")]
        public ComboBox cSmoothMode
        {
            get
            {
                return _SmoothMode;
            }

            set
            {
                _SmoothMode = value;
            }
        }

        public clasgraph()
            {
                //
                // TODO: Add constructor logic here
                //
            }
    }
}
