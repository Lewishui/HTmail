using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using Order.Common;
using Microsoft.Win32;
using System.Threading;
using System.IO;
using HT.DB;
namespace HTmail
{
    public partial class frmMailCenter : Form
    {

        string strFileName = "";
        private System.Timers.Timer timerAlter;
        private Thread GetDataforRawDataThread;
        string adaewew;
        private bool IsRun = false;



        public frmMailCenter()
        {
            InitializeComponent();
            NewMethod();

        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }



        private void 接收邮箱设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new frmReception();

            if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void 发送邮箱设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new frmFromReception();

            if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void 发信模板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new frmMailTemplate();

            if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            var form = new frmSendpage();

            if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void 定时发信ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new frmAutosendCenter();

            if (form.ShowDialog() == DialogResult.OK)
            {

            }

        }



        #region MyRegion


        private void NewMethod()
        {
            timerAlter = new System.Timers.Timer(40000);
            timerAlter.Elapsed += new System.Timers.ElapsedEventHandler(TimeControl);
            timerAlter.AutoReset = true;
            timerAlter.Start();

            {
                Microsoft.Win32.RegistryKey key;
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HTmail.exe");
                SetAutoRun(@filePath, true);
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
            var form = new frmAutosendCenter();


            form.FindTimer();
            List<Timer_info> Timer_Server = form.Timer_Server;
            List<Timer_info> filtered = Timer_Server.FindAll(s => s.status != null && s.status == "未发送");

            foreach (Timer_info item in filtered)
            {
                if (DateTime.Now.ToString("yyyy/MM/dd/HH/mm") == item.time_start)
                {

                    List<string> txValue = new List<string>();
                    List<FromList_info> Addlist_Server = new List<FromList_info>();
                    string[] temp1 = System.Text.RegularExpressions.Regex.Split(item.formto, ",");

                    for (int i = 0; i < temp1.Length; i++)
                    {
                        FromList_info item1 = new FromList_info();

                        item1.mail = temp1[i];

                        Addlist_Server.Add(item1);
                    }

                    txValue.Add(item.mail);//sendto.txt"
                    txValue.Add(item.CCmail);//ccto.txt
                    txValue.Add(item.formto);//fromto.txt
                    txValue.Add(item.subject);//subject.txt
                    txValue.Add(item.body);//body.txt.


                    txValue.Add(item.acc);//acc.txt.txt

                    var Sendpageform = new frmSendpage();
                    Sendpageform.Addlist_Server = Addlist_Server;
                    Sendpageform.txValue = txValue;
                    Sendpageform.gotype =1;

                    Sendpageform.SendMail();




                }

            }
            if (DateTime.Now.ToString("HH-mm") == "18-30")
            {

                //  autoCopyToolStripMenuItem_Click(this, EventArgs.Empty);

            }
            IsRun = false;
        }



        #endregion

    }
}
