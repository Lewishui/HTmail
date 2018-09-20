using clsBuiness;
using HT.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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


        public frmMain()
        {
            InitializeComponent();
            InitialSystemInfo();


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

            //BusinessHelp. SendMail();
            //   BusinessHelp.CDOMessageSend();


            // BusinessHelp. SendMail2();   

            //   BusinessHelp.SendMailUseGmail();

            //   BusinessHelp.outllookSend();

            BusinessHelp.ProcessLogger = ProcessLogger;
            BusinessHelp.ExceptionLogger = ExceptionLogger;
            BusinessHelp.pbStatus = pbStatus;
            BusinessHelp.tsStatusLabel1 = toolStripLabel1;


            BusinessHelp.linkid(Convert.ToInt32(1));

            List<clsend_info> ddd = BusinessHelp.ReadWEBAquila();

            MessageBox.Show("ok");
        }
    }
}
