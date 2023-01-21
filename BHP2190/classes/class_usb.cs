
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Diagnostics;
using PVOID = System.IntPtr;
using DWORD = System.UInt32;
using System.Text;

namespace BHP2066_.classes
{
    unsafe public class class_usb
    {

        public const byte BuffSize = 63;

        #region Imported DLL functions from mpusbapi.dll

        //[DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        //static extern DWORD _MPUSBGetDLLVersion();
        //[DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        //static extern DWORD _MPUSBGetDeviceCount(string pVID_PID);
        [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern PVOID _MPUSBOpen(DWORD instance, string pVID_PID, string pEP, DWORD dwDir, DWORD dwReserved);
        //[DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        //static extern DWORD _MPUSBGetDeviceDescriptor(PVOID handle, string pDevDsc, DWORD dwLen, DWORD* pLength);
        //[DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        //static extern DWORD _MPUSBGetConfigurationDescriptor(PVOID handle, byte bIndex, string pDevDsc, DWORD dwLen, DWORD* pLength);
        //[DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        //static extern DWORD _MPUSBGetStringDescriptor(PVOID handle, byte bIndex, DWORD wLangId, string pDevDsc, DWORD dwLen, DWORD* pLength);
        //[DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        //static extern DWORD _MPUSBSetConfiguration(PVOID handle, int bConfigSetting);
        [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern DWORD _MPUSBRead(PVOID handle, PVOID pData, DWORD dwLen, DWORD* pLength, DWORD dwMilliseconds);
        [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern DWORD _MPUSBWrite(PVOID handle, PVOID pData, DWORD dwLen, DWORD* pLength, DWORD dwMilliseconds);
        //[DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        //static extern DWORD _MPUSBReadInt(PVOID handle, PVOID pData, DWORD dwLen, DWORD* pLength, DWORD dwMilliseconds);
        [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern bool _MPUSBClose(PVOID handle);

        #endregion

        #region  String Definitions of Pipes and VID_PID
        //string vid_pid_norm= "vid_04d8&pid_000b";    // Bootloader vid_pid ID 

        public const string vid_pid_norm = "vid_04d8&pid_000c";//در فایل اینفو درایور قرار دارد و شناسه  فایل درایور برای این دی ال ال است

        const string out_pipe = "\\MCHP_EP1"; // Define End Points 
        const string in_pipe = "\\MCHP_EP1";
        /// <summary>
        /// ///////////////////
        //public const string vid_pid = "vid_9610&pid_0020";

        //const int LangId = 0x0409;
        //     const byte BuffSize    =  14;//32;

        /// </summary>

        public const Byte MP_WRITE = 0;
        public const Byte MP_READ = 1;
        public const Byte MPUSB_FAIL = 0;
        public const Byte MPUSB_SUCCESS = 1;
        public const Byte MAX_NUM_MPUSB_DEV = 127;

        internal static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        static PVOID myOutPipe = INVALID_HANDLE_VALUE;
        static PVOID myInPipe = INVALID_HANDLE_VALUE;

        #endregion

        public int OpenPipes()// Open communication channels to USB module 
        {
            DWORD selection = 0; // Selects the device to connect to, in this example it is assumed you will only have one device per vid_pid connected. 

            myOutPipe = _MPUSBOpen(selection, vid_pid_norm, out_pipe, 0, 0);
            myInPipe = _MPUSBOpen(selection, vid_pid_norm, in_pipe, 1, 0);
            if ((myOutPipe == INVALID_HANDLE_VALUE) || (myInPipe == INVALID_HANDLE_VALUE))
            {
                return 0;
            }
            return 1;
        }

        public void ClosePipes()// Close communication channels to USB module 
        {
            _MPUSBClose(myOutPipe);
            _MPUSBClose(myInPipe);
            myInPipe = INVALID_HANDLE_VALUE;
            myOutPipe = INVALID_HANDLE_VALUE;
        }

        //public DWORD GetDeviceCount(string Vid_Pid) // Gets # of connected USB modules (GW's) 
        //{
        //    DWORD count = _MPUSBGetDeviceCount(Vid_Pid);
        //    return count;
        //}

        //public string getfwversion()
        //{
        //    string output = "";
        //    byte* send_buffer = stackalloc byte[16];
        //    byte* receive_buffer = stackalloc byte[16];

        //    //DWORD RecvLength = 3;
        //    //DWORD SendLength = 3;

        //    send_buffer[0] = 0x01;
        //    send_buffer[1] = 0xC5;
        //    send_buffer[2] = 0x00;

        //    return output;
        //}


        public void Outprt(byte[] data, DWORD count_byte)
        {
            byte BuffSize = 63;
            DWORD OutBuffLength;

            byte* OutBuff = stackalloc byte[BuffSize - 1];

            for (int i = 0; i < count_byte; i++)
                OutBuff[i] = data[i];

            _MPUSBWrite(myOutPipe, (PVOID)OutBuff, count_byte, &OutBuffLength, 1000);//سرعت نوشتن نسبت به خواندن دوبرابر است در این dll
        }


        public byte[] Inprt(DWORD count_byte)// دریافت را از نوع آرایه رشته قرار دادم تا به تک تک بایت ها دسترسی داشته باشم
        {
            DWORD InBuffLength = 63;
            byte* OutBuff = stackalloc byte[BuffSize - 1];
            byte* InBuff = stackalloc byte[BuffSize - 1];

            _MPUSBRead(myInPipe, (PVOID)InBuff, count_byte, &InBuffLength, 1000);//سرعت خواندن نسبت به نوشتن نصف است

            byte[] rec = new byte[count_byte];
            for (int i = 0; i < count_byte; i++)
                rec[i] = InBuff[i];

            return rec;

            //string[] rec = new string[count_byte];
            //for (int i = 0; i < count_byte; i++)
            //    rec[i] = BitConverter.ToString(new byte[] { InBuff[i] }, 0);//این تابع یک آرایه از نوع بایت را به رشته تبدیل می کند اما من نیاز دارم که یک بایت را به رشته تبدیل کنم

        }

        public void SendReceivePacket(byte* SendData, DWORD SendLength, byte* ReceiveData, DWORD* ReceiveLength)
        {
            uint SendDelay = 1000;
            uint ReceiveDelay = 1000;
            DWORD SentDataLength;
            DWORD ExpectedReceiveLength = *ReceiveLength;

            _MPUSBWrite(myOutPipe, (PVOID)SendData, SendLength, &SentDataLength, SendDelay);
            _MPUSBRead(myInPipe, (PVOID)ReceiveData, ExpectedReceiveLength, ReceiveLength, ReceiveDelay);
        }

        public class_usb()
        {
            //
            // TODO: Add constructor logic here
            //
        }

    }
}  
             
                
   








/*








using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Diagnostics;
using PVOID = System.IntPtr;
using DWORD = System.UInt32;
using System.Text;


    unsafe public class class_usb
    {
    public const byte BuffSize = 63;

    #region Imported DLL functions from mpusbapi.dll

    //[DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    //static extern DWORD _MPUSBGetDLLVersion();
    //[DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    //static extern DWORD _MPUSBGetDeviceCount(string pVID_PID);
    [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    static extern PVOID _MPUSBOpen(DWORD instance, string pVID_PID, string pEP, DWORD dwDir, DWORD dwReserved);
    //[DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    //static extern DWORD _MPUSBGetDeviceDescriptor(PVOID handle, string pDevDsc, DWORD dwLen, DWORD* pLength);
    //[DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    //static extern DWORD _MPUSBGetConfigurationDescriptor(PVOID handle, byte bIndex, string pDevDsc, DWORD dwLen, DWORD* pLength);
    //[DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    //static extern DWORD _MPUSBGetStringDescriptor(PVOID handle, byte bIndex, DWORD wLangId, string pDevDsc, DWORD dwLen, DWORD* pLength);
    //[DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    //static extern DWORD _MPUSBSetConfiguration(PVOID handle, int bConfigSetting);
    [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    static extern DWORD _MPUSBRead(PVOID handle, PVOID pData, DWORD dwLen, DWORD* pLength, DWORD dwMilliseconds);
    [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    static extern DWORD _MPUSBWrite(PVOID handle, PVOID pData, DWORD dwLen, DWORD* pLength, DWORD dwMilliseconds);
    //[DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    //static extern DWORD _MPUSBReadInt(PVOID handle, PVOID pData, DWORD dwLen, DWORD* pLength, DWORD dwMilliseconds);
    [DllImport("mpusbapi.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
    static extern bool _MPUSBClose(PVOID handle);

    #endregion

    #region  String Definitions of Pipes and VID_PID
    //string vid_pid_norm= "vid_04d8&pid_000b";    // Bootloader vid_pid ID 

    public const string vid_pid_norm = "vid_04d8&pid_000c";//در فایل اینفو درایور قرار دارد و شناسه  فایل درایور برای این دی ال ال است

    const string out_pipe = "\\MCHP_EP1"; // Define End Points 
    const string in_pipe = "\\MCHP_EP1";
    /// <summary>
    /// ///////////////////
    //public const string vid_pid = "vid_9610&pid_0020";

    //const int LangId = 0x0409;
    //     const byte BuffSize    =  14;//32;

    /// </summary>

    public const Byte MP_WRITE = 0;
    public const Byte MP_READ = 1;
    public const Byte MPUSB_FAIL = 0;
    public const Byte MPUSB_SUCCESS = 1;
    public const Byte MAX_NUM_MPUSB_DEV = 127;

    internal static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

    static PVOID myOutPipe = INVALID_HANDLE_VALUE;
    static PVOID myInPipe = INVALID_HANDLE_VALUE;

    #endregion

    public int OpenPipes()// Open communication channels to USB module 
    {
        DWORD selection = 0; // Selects the device to connect to, in this example it is assumed you will only have one device per vid_pid connected. 

        myOutPipe = _MPUSBOpen(selection, vid_pid_norm, out_pipe, 0, 0);
        myInPipe = _MPUSBOpen(selection, vid_pid_norm, in_pipe, 1, 0);
        if ((myOutPipe == INVALID_HANDLE_VALUE) || (myInPipe == INVALID_HANDLE_VALUE))
        {
            return 0;
        }
        return 1;
    }

    public void ClosePipes()// Close communication channels to USB module 
    {
        _MPUSBClose(myOutPipe);
        _MPUSBClose(myInPipe);
        myInPipe = INVALID_HANDLE_VALUE;
        myOutPipe = INVALID_HANDLE_VALUE;
    }

    //public DWORD GetDeviceCount(string Vid_Pid) // Gets # of connected USB modules (GW's) 
    //{
    //    DWORD count = _MPUSBGetDeviceCount(Vid_Pid);
    //    return count;
    //}

    //public string getfwversion()
    //{
    //    string output = "";
    //    byte* send_buffer = stackalloc byte[16];
    //    byte* receive_buffer = stackalloc byte[16];

    //    //DWORD RecvLength = 3;
    //    //DWORD SendLength = 3;

    //    send_buffer[0] = 0x01;
    //    send_buffer[1] = 0xC5;
    //    send_buffer[2] = 0x00;

    //    return output;
    //}


    public void Outprt(byte[] data, DWORD count_byte)
    {
        byte BuffSize = 63;
        DWORD OutBuffLength;

        byte* OutBuff = stackalloc byte[BuffSize - 1];

        for (int i = 0; i < count_byte; i++)
            OutBuff[i] = data[i];

        _MPUSBWrite(myOutPipe, (PVOID)OutBuff, count_byte, &OutBuffLength, 1000);//سرعت نوشتن نسبت به خواندن دوبرابر است در این dll
    }


    public byte[] Inprt(DWORD count_byte)// دریافت را از نوع آرایه رشته قرار دادم تا به تک تک بایت ها دسترسی داشته باشم
    {
        DWORD InBuffLength = 63;
        byte* OutBuff = stackalloc byte[BuffSize - 1];
        byte* InBuff = stackalloc byte[BuffSize - 1];

        _MPUSBRead(myInPipe, (PVOID)InBuff, count_byte, &InBuffLength, 1000);//سرعت خواندن نسبت به نوشتن نصف است

        byte[] rec = new byte[count_byte];
        for (int i = 0; i < count_byte; i++)
            rec[i] = InBuff[i];

        return rec;

        //string[] rec = new string[count_byte];
        //for (int i = 0; i < count_byte; i++)
        //    rec[i] = BitConverter.ToString(new byte[] { InBuff[i] }, 0);//این تابع یک آرایه از نوع بایت را به رشته تبدیل می کند اما من نیاز دارم که یک بایت را به رشته تبدیل کنم

    }

    public void SendReceivePacket(byte* SendData, DWORD SendLength, byte* ReceiveData, DWORD* ReceiveLength)
    {
        uint SendDelay = 1000;
        uint ReceiveDelay = 1000;
        DWORD SentDataLength;
        DWORD ExpectedReceiveLength = *ReceiveLength;

        _MPUSBWrite(myOutPipe, (PVOID)SendData, SendLength, &SentDataLength, SendDelay);
        _MPUSBRead(myInPipe, (PVOID)ReceiveData, ExpectedReceiveLength, ReceiveLength, ReceiveDelay);
    }

    public class_usb()
    {
        //
        // TODO: Add constructor logic here
        //
    }




/*

    public const byte BuffSize = 63;

    #region Imported DLL functions from CH341PT.dll

         [DllImport("CH341PT.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
         static extern PVOID _MPUSBOpen(DWORD instance, string pVID_PID, string pEP, DWORD dwDir, DWORD dwReserved);   

        [DllImport("CH341PT.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern DWORD _MPUSBRead(PVOID handle, PVOID pData, DWORD dwLen, DWORD* pLength, DWORD dwMilliseconds);
        [DllImport("CH341PT.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern DWORD _MPUSBWrite(PVOID handle, PVOID pData, DWORD dwLen, DWORD* pLength, DWORD dwMilliseconds);
        
        [DllImport("CH341PT.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern bool _MPUSBClose(PVOID handle);

        #endregion

        #region  String Definitions of Pipes and VID_PID
      
        public const string vid_pid_norm = "VID_4348&PID_5523";//در فایل اینفو درایور قرار دارد و شناسه  فایل درایور برای این دی ال ال است

        const string out_pipe = "\\MCHP_EP1"; // Define End Points 
        const string in_pipe = "\\MCHP_EP1";
        /// <summary>
        /// ///////////////////
        //public const string vid_pid = "vid_9610&pid_0020";

        //const int LangId = 0x0409;
        //     const byte BuffSize    =  14;//32;

        /// </summary>

        public const Byte MP_WRITE = 0;
        public const Byte MP_READ = 1;
        public const Byte MPUSB_FAIL = 0;
        public const Byte MPUSB_SUCCESS = 1;
        public const Byte MAX_NUM_MPUSB_DEV = 127;

        internal static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        static PVOID myOutPipe = INVALID_HANDLE_VALUE;
        static PVOID myInPipe = INVALID_HANDLE_VALUE;

        #endregion

        public int OpenPipes()// Open communication channels to USB module 
        {
            DWORD selection = 0; // Selects the device to connect to, in this example it is assumed you will only have one device per vid_pid connected. 
        
           
        myOutPipe = _MPUSBOpen(selection, vid_pid_norm, out_pipe, 0, 0);
        myInPipe = _MPUSBOpen(selection, vid_pid_norm, in_pipe, 1, 0);
        if ((myOutPipe == INVALID_HANDLE_VALUE) || (myInPipe == INVALID_HANDLE_VALUE))
            {
                return 0;
            }
            return 1;
        }

        public void ClosePipes()// Close communication channels to USB module 
        {
            _MPUSBClose(myOutPipe);
            _MPUSBClose(myInPipe);
            myInPipe = INVALID_HANDLE_VALUE;
            myOutPipe = INVALID_HANDLE_VALUE;
        }

        //public DWORD GetDeviceCount(string Vid_Pid) // Gets # of connected USB modules (GW's) 
        //{
        //    DWORD count = _MPUSBGetDeviceCount(Vid_Pid);
        //    return count;
        //}

        //public string getfwversion()
        //{
        //    string output = "";
        //    byte* send_buffer = stackalloc byte[16];
        //    byte* receive_buffer = stackalloc byte[16];

        //    //DWORD RecvLength = 3;
        //    //DWORD SendLength = 3;

        //    send_buffer[0] = 0x01;
        //    send_buffer[1] = 0xC5;
        //    send_buffer[2] = 0x00;

        //    return output;
        //}


        public void Outprt(byte[] data, DWORD count_byte)
        {
            byte BuffSize = 63;
            DWORD OutBuffLength;

            byte* OutBuff = stackalloc byte[BuffSize - 1];
          
            for (int i = 0; i < count_byte; i++)
                OutBuff[i] = data[i];

            _MPUSBWrite(myOutPipe, (PVOID)OutBuff, count_byte, &OutBuffLength, 50);//سرعت نوشتن نسبت به خواندن دوبرابر است در این dll
        }

        
        public byte[] Inprt(DWORD count_byte)// دریافت را از نوع آرایه رشته قرار دادم تا به تک تک بایت ها دسترسی داشته باشم
        {
            DWORD InBuffLength = 63;
            byte* OutBuff = stackalloc byte[BuffSize - 1];
            byte* InBuff = stackalloc byte[BuffSize - 1];

            _MPUSBRead(myInPipe, (PVOID)InBuff, count_byte, &InBuffLength, 50);//سرعت خواندن نسبت به نوشتن نصف است

            byte[] rec = new byte[count_byte];
            for (int i = 0; i < count_byte; i++)
                rec[i] = InBuff[i];

            return rec;

            //string[] rec = new string[count_byte];
            //for (int i = 0; i < count_byte; i++)
            //    rec[i] = BitConverter.ToString(new byte[] { InBuff[i] }, 0);//این تابع یک آرایه از نوع بایت را به رشته تبدیل می کند اما من نیاز دارم که یک بایت را به رشته تبدیل کنم

        }

        public void SendReceivePacket(byte* SendData, DWORD SendLength, byte* ReceiveData, DWORD* ReceiveLength)
        {
            uint SendDelay = 1000;
            uint ReceiveDelay = 1000;
            DWORD SentDataLength;
            DWORD ExpectedReceiveLength = *ReceiveLength;

            _MPUSBWrite(myOutPipe, (PVOID)SendData, SendLength, &SentDataLength, SendDelay);
            _MPUSBRead(myInPipe, (PVOID)ReceiveData, ExpectedReceiveLength, ReceiveLength, ReceiveDelay);
        }

        public class_usb()
       {
           //
           // TODO: Add constructor logic here
           //
       }
      
    }
 
*/                
   