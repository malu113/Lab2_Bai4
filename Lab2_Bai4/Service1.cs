using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Bai4
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            CanHandleSessionChangeEvent = true;
        }
        //Thư viện để pop up
        [DllImport("wtsapi32.dll", SetLastError = true)]
        static extern bool WTSSendMessage(
                                           IntPtr hServer,
                                           int SessionId,
                                           String pTitle,
                                           int TitleLength,
                                           String pMessage,
                                           int MessageLength,
                                           int Style,
                                           int Timeout,
                                           out int pResponse,
                                           bool bWait);
        //Thư viện hỗ trợ lấy thông tin session
        [DllImport("Kernel32.dll", SetLastError = true)]
        static extern int WTSGetActiveConsoleSessionID();
        public static IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;
        public static int WTS_CURRENT_SESSION = 1;
       
        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            //SessionUnlock: đăng nhập sau khi Lock hoặc Shutdown
            //SessionLogon: đăng nhập sau khi Sign Out
            if (changeDescription.Reason == SessionChangeReason.SessionUnlock || 
                changeDescription.Reason == SessionChangeReason.SessionLogon)
            {
                Show_Message();
            }
        }

        public static void Show_Message()
        {
            bool result = false;
            string title = "Lab2_Bai4";
            int tlen = title.Length;
            string msg = "18520113";
            int mlen = msg.Length;
            int resp = 0;
            result = WTSSendMessage(WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION, 
                                    title, tlen, msg, mlen, 0, 0, out resp, false);
        }

        protected override void OnStart(string[] args)
        {
        }


        protected override void OnStop()
        {
        }
    }
}
