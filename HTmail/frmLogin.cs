using clsBuiness;
using HT.DB;
using Microsoft.Win32;
using Order.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTmail
{
    public partial class frmLogin : Form
    {
        public log4net.ILog ProcessLogger;
        public log4net.ILog ExceptionLogger;
        private TextBox txtSAPPassword;
        private CheckBox chkSaveInfo;
        Sunisoft.IrisSkin.SkinEngine se = null;
        frmAboutBox aboutbox;
        private System.Timers.Timer timerAlter1;
        private string ipadress;
        int logis = 0;
        private frmQQSend_qun frmQQSend_qun;
        //存放要显示的信息
        List<string> messages;
        //要显示信息的下标索引
        int index = 0;
        clsAllnew BusinessHelp;
        private System.Timers.Timer timerAlter_new;
        private bool IsRun = false;
        private Thread GetDataforRawDataThread;
        List<clsalter_message> alter_Server;

        public frmLogin()
        {
            InitializeComponent();

            aboutbox = new frmAboutBox();
            BusinessHelp = new clsAllnew();

            InitialSystemInfo();
            se = new Sunisoft.IrisSkin.SkinEngine();
            se.SkinAllForm = true;
            se.SkinFile = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""), "PageColor1.ssk");

            InitialPassword();
            ProcessLogger.Fatal("login" + DateTime.Now.ToString());
            string path = AppDomain.CurrentDomain.BaseDirectory + "System\\IP.txt";


            messages = new List<string>();
            messages.Add("云合未来计算机技术有限公司人工智能系统  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            NewMethod();

            timer1.Interval = 12000;
            timer1.Start();
            timer1.Tick += timer1_Tick;

        }
        void timer1_Tick(object sender, EventArgs e)
        {
            //滚动显示
            if (alter_Server.Count > 0)
            {
                index = (index + 1) % alter_Server.Count;
                //toolStripLabel9.Text = messages[index];
                this.scrollingText1.ScrollText = alter_Server[index].text;
            }
            else
            {
                index = (index + 1) % messages.Count;
                //toolStripLabel9.Text = messages[index];
                this.scrollingText1.ScrollText = messages[index];


            }

        }
        private void NewMethod()
        {
            timerAlter_new = new System.Timers.Timer(6666);
            timerAlter_new.Elapsed += new System.Timers.ElapsedEventHandler(TimeControl);
            timerAlter_new.AutoReset = true;
            timerAlter_new.Start();
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
            bool istrue = true;
            int dddindex = 1;
            alter_Server = new List<clsalter_message>();
            string strSelect = "select * from alter_message";

            alter_Server = BusinessHelp.find_alter_message(strSelect);

            IsRun = false;
        }
        void timer2_Tick(object sender, EventArgs e)
        {

        }
        //alter_message



        private void InitialSystemInfo()
        {
            #region 初始化配置
            ProcessLogger = log4net.LogManager.GetLogger("ProcessLogger");
            ExceptionLogger = log4net.LogManager.GetLogger("SystemExceptionLogger");
            ProcessLogger.Fatal("System Start " + DateTime.Now.ToString());
            #endregion
        }
        private void InitialPassword()
        {
            try
            {
                txtSAPPassword = new TextBox();
                txtSAPPassword.PasswordChar = '*';
                ToolStripControlHost t = new ToolStripControlHost(txtSAPPassword);
                t.Width = 100;
                t.AutoSize = false;
                t.Alignment = ToolStripItemAlignment.Right;
                this.toolStrip1.Items.Insert(this.toolStrip1.Items.Count - 4, t);

                chkSaveInfo = new CheckBox();
                chkSaveInfo.Text = "";
                chkSaveInfo.Padding = new Padding(5, 2, 0, 0);
                ToolStripControlHost t1 = new ToolStripControlHost(chkSaveInfo);
                t1.AutoSize = true;

                t1.ToolTipText = clsShowMessage.MSG_002;
                t1.Alignment = ToolStripItemAlignment.Right;
                this.toolStrip1.Items.Insert(this.toolStrip1.Items.Count - 5, t1);
                getUserAndPassword();
                chkSaveInfo.Checked = false;

            }
            catch (Exception ex)
            {
                //clsLogPrint.WriteLog("<frmMain> InitialPassword:" + ex.Message);
                throw ex;
            }
        }
        private void getUserAndPassword()
        {
            try
            {
                RegistryKey rkLocalMachine = Registry.LocalMachine;
                RegistryKey rkSoftWare = rkLocalMachine.OpenSubKey(clsConstant.RegEdit_Key_SoftWare);
                RegistryKey rkAmdape2e = rkSoftWare.OpenSubKey(clsConstant.RegEdit_Key_AMDAPE2E);
                if (rkAmdape2e != null)
                {
                    this.txtSAPUserId.Text = clsCommHelp.encryptString(clsCommHelp.NullToString(rkAmdape2e.GetValue(clsConstant.RegEdit_Key_User)));
                    this.txtSAPPassword.Text = clsCommHelp.encryptString(clsCommHelp.NullToString(rkAmdape2e.GetValue(clsConstant.RegEdit_Key_PassWord)));
                    if (clsCommHelp.NullToString(rkAmdape2e.GetValue(clsConstant.RegEdit_Key_Date)) != "")
                    {
                        this.chkSaveInfo.Checked = true;
                    }
                    else
                    {
                        this.chkSaveInfo.Checked = false;
                    }
                    rkAmdape2e.Close();
                }
                rkSoftWare.Close();
                rkLocalMachine.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex;
            }
        }
        private void saveUserAndPassword()
        {
            try
            {
                RegistryKey rkLocalMachine = Registry.LocalMachine;
                RegistryKey rkSoftWare = rkLocalMachine.OpenSubKey(clsConstant.RegEdit_Key_SoftWare, true);
                RegistryKey rkAmdape2e = rkSoftWare.CreateSubKey(clsConstant.RegEdit_Key_AMDAPE2E);
                if (rkAmdape2e != null)
                {
                    rkAmdape2e.SetValue(clsConstant.RegEdit_Key_User, clsCommHelp.encryptString(this.txtSAPUserId.Text.Trim()));
                    rkAmdape2e.SetValue(clsConstant.RegEdit_Key_PassWord, clsCommHelp.encryptString(this.txtSAPPassword.Text.Trim()));
                    rkAmdape2e.SetValue(clsConstant.RegEdit_Key_Date, DateTime.Now.ToString("yyyMMdd"));
                }
                rkAmdape2e.Close();
                rkSoftWare.Close();
                rkLocalMachine.Close();

            }
            catch (Exception ex)
            {
                //ClsLogPrint.WriteLog("<frmMain> saveUserAndPassword:" + ex.Message);
                throw ex;
            }
        }

        private void 关于系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutbox.ShowDialog();
        }

        private void tsbLogin_Click(object sender, EventArgs e)
        {
            try
            {
                clsmytest buiness = new clsmytest();

                bool istue = buiness.checkname(this.txtSAPUserId.Text.Trim(), this.txtSAPPassword.Text.Trim());
                List<softTime_info> list_Server = buiness.list_Server;

                if (istue == true)
                {
                    #region 更新登录时间
                    List<clsuserinfo> userlist_Server = new List<clsuserinfo>();
                    clsuserinfo item = new clsuserinfo();
                    item.name = txtSAPUserId.Text.Trim();

                    item.denglushijian = DateTime.Now.ToString("yyyyMMdd-HH:mm:ss");


                    userlist_Server.Add(item);
                    if (list_Server[0].pid == null || list_Server[0].pid == "")
                    {
                        item.pid = BusinessHelp.PCid;

                    }
                    else
                    {
                        if (list_Server[0].pid != BusinessHelp.PCid)
                        {
                            MessageBox.Show("登录失败,一个账号只能在一台电脑使用不可以出售或转让其他人使用\r\n超出使用权限，如特殊情况请联系管理员\r\n", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }
                    }

                    BusinessHelp.updateLoginTime_Server(userlist_Server);
                    #endregion
                    this.WindowState = FormWindowState.Maximized;
                    if (chkSaveInfo.Checked == true)
                        saveUserAndPassword();

                    this.WindowState = FormWindowState.Maximized;
                    tsbLogin.Text = "登录成功";

                    toolStripDropDownButton1.Enabled = true;
                    toolStripDropDownButton3.Enabled = true;
                    toolStripDropDownButton2.Enabled = true;
                    //一键配置ToolStripMenuItem.Enabled = true;
                    pBBToolStripMenuItem.Enabled = true;
                    修改登录信息ToolStripMenuItem.Enabled = true;
                    logis++;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("登录失败,请检查网络是否畅通并重试\r\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

                throw ex;
            }
        }

        private void pBBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.scrollingText1.Visible = true;
            toolStrip1.Visible = false;
            if (frmQQSend_qun == null)
            {
                frmQQSend_qun = new frmQQSend_qun();
                frmQQSend_qun.FormClosed += new FormClosedEventHandler(FrmOMS_FormClosed);
            }
            if (frmQQSend_qun == null)
            {
                frmQQSend_qun = new frmQQSend_qun();
            }
            frmQQSend_qun.Show(this.dockPanel2);
        }
        void FrmOMS_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is frmQQSend_qun)
            {
                frmQQSend_qun = null;
            }
        }

    }
}
