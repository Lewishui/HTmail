using clsBuiness;
using HT.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTmail
{
    public partial class frmMain : Form
    {
        public Order.Common.ScrollingText scrollingText1;
        //多线程访问 davgiridview 报错

        private delegate void InvokeHandler();
        public log4net.ILog ProcessLogger;
        public log4net.ILog ExceptionLogger;
        // 后台执行控件
        private BackgroundWorker bgWorker;
        // 消息显示窗体
        private frmMessageShow frmMessageShow;
        // 后台操作是否正常完成
        private bool blnBackGroundWorkIsOK = false;
        //后加的后台属性显
        private bool backGroundRunResult;
        // 后台操作是否正常完成

        string strFileName = "";
        private System.Timers.Timer timerAlter;
        private Thread GetDataforRawDataThread;
        string adaewew;
        private bool IsRun = false;


        public frmMain()
        {
            InitializeComponent();
            InitialSystemInfo();
            NewMethod();


        }
        private void InitialSystemInfo()
        {
            #region 初始化配置
            ProcessLogger = log4net.LogManager.GetLogger("ProcessLogger");
            ExceptionLogger = log4net.LogManager.GetLogger("SystemExceptionLogger");
            ProcessLogger.Fatal("System Start " + DateTime.Now.ToString());
            #endregion
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            //初始化信息
            clsAllnew BusinessHelp = new clsAllnew();
            //   BusinessHelp. SendMailqq();

            //BusinessHelp. SendMail();
            //   BusinessHelp.CDOMessageSend();


            // BusinessHelp. SendMail2();   

            //   BusinessHelp.SendMailUseGmail();

            BusinessHelp.outllookSend();

            BusinessHelp.ProcessLogger = ProcessLogger;
            BusinessHelp.ExceptionLogger = ExceptionLogger;
            BusinessHelp.pbStatus = pbStatus;
            BusinessHelp.tsStatusLabel1 = toolStripLabel1;


            BusinessHelp.linkid(Convert.ToInt32(1));

            List<AddconnectGroup_info> ddd = BusinessHelp.ReadWEBAquila();

            MessageBox.Show("ok");
        }


        private void NewMethod()
        {
            timerAlter = new System.Timers.Timer(40000);
            timerAlter.Elapsed += new System.Timers.ElapsedEventHandler(TimeControl);
            timerAlter.AutoReset = true;
            timerAlter.Start();

            {
                Microsoft.Win32.RegistryKey key;
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HTmail.exe");
                SetAutoRun(@filePath, false);
                // MessageBox.Show("Set OK !");
            }
        }
        public static void SetAutoRun(string fileName, bool isAutoRun)
        {
            RegistryKey reg = null;
            try
            {
                if (!System.IO.File.Exists(fileName))
                    throw new Exception("EX:09023:no find file!");
                String name = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
                reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (reg == null)
                    reg = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                if (isAutoRun)
                    reg.SetValue(name, fileName);
                else
                    reg.SetValue(name, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (reg != null)
                    reg.Close();
            }
        }
        private void TimeControl(object sender, EventArgs e)
        {
            if (!IsRun)
            {
                IsRun = true;
                GetDataforRawDataThread = new Thread(TimeMethod);
                GetDataforRawDataThread.Start();
            }
        }

        private void TimeMethod()
        {
            if (DateTime.Now.ToString("HH-mm") == "18-30")
            {

                //  autoCopyToolStripMenuItem_Click(this, EventArgs.Empty);

            }
            IsRun = false;
        }

        private void autosend(object sender, EventArgs e)
        {

        }
    }
}
