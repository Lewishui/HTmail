using HT.DB;
using ISR_System;
using mshtml;
using Order.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
//using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;


namespace clsBuiness
{

    public enum ProcessStatus
    {
        初始化,
        登录界面,
        确认YES,
        第一页面,
        第二页面,
        Filter下拉,
        关闭页面,
        结束页面

    }
    public enum EapprovalProcessStatus
    {
        初始化,
        Current_Task,
        Task_Queue,
        Search,
        Process
    }
    public class clsAllnew
    {
        private BackgroundWorker bgWorker1;
        //private object missing = System.Reflection.Missing.Value;
        public ToolStripProgressBar pbStatus { get; set; }
        public ToolStripStatusLabel tsStatusLabel1 { get; set; }
        public log4net.ILog ProcessLogger { get; set; }
        public log4net.ILog ExceptionLogger { get; set; }

        private ProcessStatus isrun = ProcessStatus.初始化;
        private EapprovalProcessStatus isrun1 = EapprovalProcessStatus.初始化;
        private bool isOneFinished = false;
        private Form viewForm;
        private WbBlockNewUrl MyWebBrower;
        System.Timers.Timer aTimer = new System.Timers.Timer(100);//实例化Timer类，设置间隔时间为10000毫秒； 
        System.Timers.Timer t = new System.Timers.Timer(1000);//实例化Timer类，设置间隔时间为10000毫秒； 
        private DateTime StopTime;
        private DateTime MainStopTime;
        WbBlockNewUrl myDoc = null;
        private int login;
        string caizhong;
        string NOW_link;
        int Typeidlink = 0;
        bool loading;
        private string dataSource = "H.sqlite";
        string newsth;
        string PCid;

        public clsAllnew()
        {
            newsth = AppDomain.CurrentDomain.BaseDirectory + "\\" + dataSource;

            PCid = clsCommHelp.GetCpuID();
        }

        CookieContainer cookie = new CookieContainer();

        private string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        public string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }


        public void SendMail()
        {

            {
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Host = "smtp.163.com";
                client.UseDefaultCredentials = false;
                //
                //启用功能修改处
                //
                client.Credentials = new System.Net.NetworkCredential("caoyuanlang901029@126.com", "lyh07910");
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                client.Port = 465;
                client.EnableSsl = true;//经过ssl加密    
                //
                //启用功能修改处
                //
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("caoyuanlang901029@163.com", "512250428@qq.com");
                message.Subject = "忘记密码";
                message.Body = "您的登录名户和密码分别为:" + "caoyuanlang901029@163.com" + "  " + "caoyuanlang901029@163.com";
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                //  message.Headers.Add("X-Mailer", "Microsoft Outlook");

                //添加附件需将(附件先上传到服务器)
                // System.Net.Mail.Attachment data = new System.Net.Mail.Attachment(@"UpFile\fj.rar",System.Net.Mime.MediaTypeNames.Application.Octet);
                //message.Attachments.Add(data);
                try
                {
                    client.Send(message);
                    //  this.lbMessage.Text = "登录名和密码已经发送到您的" + "512250428@qq.com" + "邮箱!";
                }
                catch (Exception ex)
                {
                    // this.lbMessage.Text = "Send Email Failed." + ex.ToString();
                }
            }
        }
        public void SendMailqq()
        {

            {
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Host = "smtp.qq.com";
                client.UseDefaultCredentials = false;
                //
                //启用功能修改处
                //
                client.Credentials = new System.Net.NetworkCredential("512250428@qq.com", "bdtjdpzvnzbjbhfj");
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                client.Port = 25;
                client.EnableSsl = true;//经过ssl加密    
                //
                //启用功能修改处
                //
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("512250428@qq.com", "1623005800@qq.com");
                message.Subject = "忘记密码";
                message.Body = "您的登录名户和密码分别为:" + "1623005800@qq.com" + "  " + "c1623005800@qq.com";
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                //  message.Headers.Add("X-Mailer", "Microsoft Outlook");

                //添加附件需将(附件先上传到服务器)
                // System.Net.Mail.Attachment data = new System.Net.Mail.Attachment(@"UpFile\fj.rar",System.Net.Mime.MediaTypeNames.Application.Octet);
                //message.Attachments.Add(data);
                try
                {
                    client.Send(message);
                    //  this.lbMessage.Text = "登录名和密码已经发送到您的" + "512250428@qq.com" + "邮箱!";
                }
                catch (Exception ex)
                {
                    // this.lbMessage.Text = "Send Email Failed." + ex.ToString();
                }
            }
        }
        public void SendMail2()
        {
            try
            {
                System.Web.Mail.MailMessage message = new System.Web.Mail.MailMessage();
                // 发送方
                message.From = "caoyuanlang901029@163.com";
                // 接收方
                message.To = "512250428@qq.com";
                //主题
                message.Subject = "Send Using Web Mail";


                // 指定为HTML格式的内容
                message.BodyFormat = System.Web.Mail.MailFormat.Html;


                // HTML内容  邮件内容
                message.Body = "<HTML><BODY><B>Hello World!</B></BODY></HTML>";

                message.Priority = System.Web.Mail.MailPriority.High;
                //添加附件
                // 指定附件路径.
                //String sFile = @"D:\down\dd.txt";
                //System.Web.Mail.MailAttachment oAttch = new System.Web.Mail.MailAttachment(sFile, System.Web.Mail.MailEncoding.Base64);


                //message.Attachments.Add(oAttch);


                // 指定SMTP服务器地址
                System.Web.Mail.SmtpMail.SmtpServer = "smtp.163.com";
                //验证 
                message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                //登陆名 
                message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "caoyuanlang901029@163.com");
                //登陆密码 
                message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "901029901029");


                message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "587");
                //是否ssl
                message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");
                //Smtp服务器
                //System.Web.Mail.SmtpMail.SmtpServer = "smtp.163.com";


                System.Web.Mail.SmtpMail.Send(message);


                message = null;
                // oAttch = null;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.Read();
            }


        }


        public void SendMailUseGmail()
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add("512250428@qq.com");
            //  msg.To.Add(b@b.com);    
            /*   
             msg.To.Add("b@b.com");   
            * msg.To.Add("b@b.com");   
            * msg.To.Add("b@b.com");可以发送给多人   
            */
            //  msg.CC.Add(c@c.com);    
            /*   
            * msg.CC.Add("c@c.com");   
            * msg.CC.Add("c@c.com");可以抄送给多人   
            */
            msg.From = new System.Net.Mail.MailAddress("caoyuanlang901029@126.com", "901029901029", System.Text.Encoding.UTF8);
            /* 上面3个参数分别是发件人地址（可以随便写），发件人姓名，编码*/
            msg.Subject = "这是测试邮件";//邮件标题    
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码    
            msg.Body = "邮件内容";//邮件内容    
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码    
            msg.IsBodyHtml = false;//是否是HTML邮件    
            msg.Priority = System.Net.Mail.MailPriority.High;//邮件优先级    
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("caoyuanlang901029@126.com", "901029901029");
            //上述写你的GMail邮箱和密码    
            client.Port = 25;//Gmail使用的端口    
            client.Host = "smtp.126.com";
            client.EnableSsl = true;//经过ssl加密    
            object userState = msg;
            try
            {
                //  client.SendAsync(msg, userState);
                client.Send(msg);
                //   MessageBox.Show("发送成功");    
            }
            catch (System.Net.Mail.SmtpException ex)
            {

            }
        }


        public bool CDOMessageSend()
        {
            //lock (lockHelper)
            {
                CDO.Message objMail = new CDO.Message();
                try
                {
                    objMail.To = "512250428@qq.com";
                    objMail.From = "caoyuanlang901029@163.com";
                    objMail.Subject = "KKKSSS";
                    //if (_Format.Equals(System.Web.Mail.MailFormat.Html)  )

                    //    objMail.HTMLBody = _Body;
                    //else 
                    objMail.TextBody = "DFGHJKL";
                    //if (!_SmtpPort.Equals("25"))
                    objMail.Configuration.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"].Value = "465"; //设置端口
                    objMail.Configuration.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"].Value = "smtp.163.com";
                    objMail.Configuration.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"].Value = "2";
                    objMail.Configuration.Fields["http://schemas.microsoft.com/cdo/configuration/sendemailaddress"].Value = "caoyuanlang901029@163.com";
                    objMail.Configuration.Fields["http://schemas.microsoft.com/cdo/configuration/smtpuserreplyemailaddress"].Value = "caoyuanlang901029@163.com";
                    objMail.Configuration.Fields["http://schemas.microsoft.com/cdo/configuration/smtpaccountname"].Value = "caoyuanlang901029@163.com";
                    objMail.Configuration.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"].Value = "caoyuanlang901029@163.com";
                    objMail.Configuration.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"].Value = "901029901029";
                    objMail.Configuration.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"].Value = 1;
                    objMail.Configuration.Fields["http://schemas.microsoft.com/cdo/configuration/smtpusessl"].Value = true;


                    objMail.Configuration.Fields.Update();
                    objMail.Send();
                    return true;
                }
                catch { }
                finally
                {

                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objMail);
                objMail = null;
            }
            return false;
        }

        public bool outllookSend()
        {
            Outlook.Application olApp = new Outlook.Application();
            Outlook.MailItem mailItem = (Outlook.MailItem)olApp.CreateItem(Outlook.OlItemType.olMailItem);

            mailItem.To = "512250428@qq.com";
            mailItem.Subject = "A test";
            mailItem.BodyFormat = Outlook.OlBodyFormat.olFormatHTML;
            mailItem.HTMLBody = "Hello world";
            ((Outlook._MailItem)mailItem).Send();

            mailItem = null;
            olApp = null;

            return true;

        }

        private void InitialSystemInfo()
        {
            #region 初始化配置
            ProcessLogger = log4net.LogManager.GetLogger("ProcessLogger");
            ExceptionLogger = log4net.LogManager.GetLogger("SystemExceptionLogger");
            ProcessLogger.Fatal("System Start " + DateTime.Now.ToString());
            #endregion
        }

        public List<AddconnectGroup_info> ReadWEBAquila()
        {
            //NOW_link = "";
            caizhong = "";
            login = 0;
            try
            {
                tsStatusLabel1.Text = "玩命获取中....";
                isOneFinished = false;
                StopTime = DateTime.Now;
                InitialWebbroswerIE2();
                tsStatusLabel1.Text = "玩命获取中dd  ....";


                int time = 0;
                while (!isOneFinished)
                {
                    time++;
                    //tsStatusLabel1.Text = caizhong + "刷新中  " + time.ToString() + "....";
                    if (time > 200000)
                        time = 0;

                    System.Windows.Forms.Application.DoEvents();
                    DateTime rq2 = DateTime.Now;  //结束时间
                    int a = rq2.Second - StopTime.Second;
                    TimeSpan ts = rq2 - StopTime;
                    int timeTotal = ts.Minutes;

                    if (timeTotal >= 1)
                    {
                        tsStatusLabel1.Text = "超出时间 正在退出....";
                        ProcessLogger.Fatal("超出时间 89011" + DateTime.Now.ToString());
                        //   isOneFinished = true;

                        StopTime = DateTime.Now;
                    }
                }
                tsStatusLabel1.Text = "关闭1  ....";

                isOneFinished = false;
                return null;
            }
            catch (Exception ex)
            {
                ProcessLogger.Fatal("EX9092" + DateTime.Now.ToString() + ex);
                return null;
                throw;
            }
        }
        public void InitialWebbroswerIE2()
        {
            try
            {

                MyWebBrower = new WbBlockNewUrl();
                //不显示弹出错误继续运行框（HP方可）
                MyWebBrower.ScriptErrorsSuppressed = true;
                MyWebBrower.BeforeNewWindow += new EventHandler<WebBrowserExtendedNavigatingEventArgs>(MyWebBrower_BeforeNewWindow2);
                MyWebBrower.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(AnalysisWebInfo2);
                MyWebBrower.Dock = DockStyle.Fill;
                MyWebBrower.IsWebBrowserContextMenuEnabled = true;
                //显示用的窗体
                viewForm = new Form();
                //viewForm.Icon=
                viewForm.ClientSize = new System.Drawing.Size(550, 600);
                viewForm.StartPosition = FormStartPosition.CenterScreen;
                viewForm.Controls.Clear();
                viewForm.Controls.Add(MyWebBrower);
                viewForm.FormClosing += new FormClosingEventHandler(viewForm_FormClosing);
                viewForm.Show();
                ProcessLogger.Fatal("读取中 09010 " + DateTime.Now.ToString());

                MyWebBrower.Url = new Uri("https://ui.ptlogin2.qq.com/cgi-bin/login?style=9&appid=522005705&daid=4&s_url=https%3A%2F%2Fw.mail.qq.com%2Fcgi-bin%2Flogin%3Fvt%3Dpassport%26vm%3Dwsk%26delegate_url%3D%26f%3Dxhtml%26target%3D&hln_css=http%3A%2F%2Fmail.qq.com%2Fzh_CN%2Fhtmledition%2Fimages%2Flogo%2Fqqmail%2Fqqmail_logo_default_200h.png&low_login=1&hln_autologin=记住登录状态&pt_no_onekey=1");//&num=15


                //share
                //MyWebBrower.Url = new Uri(NOW_link + "=yl3m");              

                ProcessLogger.Fatal("接入 前 091102 " + DateTime.Now.ToString());


                tsStatusLabel1.Text = "接入 ...." + MyWebBrower.Url;

            }
            catch (Exception ex)
            {
                ProcessLogger.Fatal("Ex881" + ex + DateTime.Now.ToString());

                //   MessageBox.Show("错误：0001" + ex);
                return;
                throw ex;
            }

        }
        void MyWebBrower_BeforeNewWindow2(object sender, WebBrowserExtendedNavigatingEventArgs e)
        {
            #region 在原有窗口导航出新页
            //e.Cancel = true;//http://pro.wwpack-crest.hp.com/wwpak.online/regResults.aspx
            //MyWebBrower.Navigate(e.Url);
            #endregion
        }
        private void viewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isrun != ProcessStatus.关闭页面)
            {
                if (MessageBox.Show("正在进行，是否中止?", "关闭", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (MyWebBrower != null)
                    {
                        if (MyWebBrower.IsBusy)
                        {
                            MyWebBrower.Stop();
                        }
                        MyWebBrower.Dispose();
                        MyWebBrower = null;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        protected void AnalysisWebInfo2(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            int runindex = 0;
            try
            {
                tsStatusLabel1.Text = caizhong + "接入 89902 ....";
                ProcessLogger.Fatal("AnalysisWebInfo2接入 89902" + DateTime.Now.ToString());

                myDoc = sender as WbBlockNewUrl;

                #region 界面1  //登录
                if (myDoc != null && myDoc.Url.ToString().IndexOf(NOW_link) >= 0 && login == 0)
                {
                    HtmlElement userName = null;
                    HtmlElement password = null;
                    HtmlElement submit = null;

                    runindex = 1;


                    tsStatusLabel1.Text = caizhong + "登录 ....";
                    ProcessLogger.Fatal("接入界面" + DateTime.Now.ToString());

                    //HtmlElementCollection atab = myDoc.Document.GetElementsByTagName("a");
                    //if (atab != null)
                    //{
                    //    foreach (HtmlElement item in atab)
                    //    {
                    //        string inx = item.OuterHtml;

                    //        if (item.GetAttribute("id") == "switcher_plogin")
                    //        {
                    //            item.InvokeMember("Click");
                    //        }
                    //    }

                    //    loading = true;
                    //    while (loading == true)
                    //    {

                    //        Application.DoEvents();
                    //        Get_Kaijiang();
                    //    }


                    //}
                    HtmlElementCollection atab = myDoc.Document.GetElementsByTagName("input");
                    if (atab != null)
                        foreach (HtmlElement item in atab)
                        {
                            if (item.GetAttribute("name") == "u")
                                userName = item;
                            if (item.GetAttribute("name") == "p")
                                password = item;
                            if (item.GetAttribute("id") == "go")
                                submit = item;
                        }
                    atab = myDoc.Document.GetElementsByTagName("div");
                    if (atab != null)
                    {
                        foreach (HtmlElement item in atab)
                        {
                            if (item.GetAttribute("id") == "go")
                                submit = item;
                        }
                    }
                    if (userName != null)
                    {
                        userName.SetAttribute("Value", "512250428");
                        if (password != null)
                            password.SetAttribute("Value", "lyh15940836280");
                        //  submit.InvokeMember("Click");
                    }
                    tsStatusLabel1.Text = caizhong + "界面1 ....";
                    ProcessLogger.Fatal("接入界面 002" + DateTime.Now.ToString());
                    isrun = ProcessStatus.登录界面;
                    login++;

                }
                #endregion



            }

            catch (Exception)
            {

                throw;
            }

        }
        private void Get_Kaijiang()
        {
            if (MyWebBrower == null)
            {
                loading = false;
                return;

            }
            try
            {

                IHTMLDocument2 doc = (IHTMLDocument2)MyWebBrower.Document.DomDocument;
                HTMLDocument myDoc1 = doc as HTMLDocument;
                if (myDoc1 != null)
                {
                    IHTMLElementCollection atab = myDoc1.getElementsByTagName("a");
                    if (atab != null)
                    {
                        foreach (IHTMLElement item in atab)
                        {
                            string inx = item.outerHTML;
                            //<A class=login_box_forgotpassword href="https://aq.qq.com/cn2/findpsw/pc/pc_find_pwd_input_account?pw_type=6&amp;aquin=&amp;source_id=2705" target=_blank>忘了密码？</A>
                            if (inx.Contains("tabIndex"))
                            {

                                item.click();
                                loading = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessLogger.Fatal("EX23092" + DateTime.Now.ToString() + ex);
                return;
                throw;
            }
        }


        public string linkid(int typeid)
        {
            Typeidlink = typeid;

            string link = "";
            if (typeid == 1)
                link = "https://ui.ptlogin2.qq.com/cgi-bin/login?";//&num=15
            else if (typeid == 2)
                link = "http://hlj11x5.icaile.com/?op";
            NOW_link = link;

            return link;

        }

        #region OPE

        public int create_AddconnectGroup_Server(List<AddconnectGroup_info> AddMAPResult)
        {
            int isrun = 0;
            string sql = "insert into sendconnectgroup(name,PCid) values ('" + AddMAPResult[0].name + "','" + PCid + "')";

            //isrun = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.CONNECTION_STRING_BASE, sql, CommandType.Text, null);
            isrun = MySqlHelper.ExecuteSql(sql);

            return isrun;
        }
        public List<AddconnectGroup_info> findconnectGroup(string findtext)
        {

            //SQLiteConnection dbConn = new SQLiteConnection("Data Source=" + dataSource);

            //dbConn.Open();
            //SQLiteCommand dbCmd = dbConn.CreateCommand();
            //dbCmd.CommandText = findtext;

            //DbDataReader reader = SQLiteHelper.ExecuteReader("Data Source=" + newsth, dbCmd);

            findtext = sqlAddPCID(findtext);
            MySql.Data.MySqlClient.MySqlDataReader reader = MySqlHelper.ExecuteReader(findtext);

            List<AddconnectGroup_info> ClaimReport_Server = new List<AddconnectGroup_info>();

            while (reader.Read())
            {
                AddconnectGroup_info item = new AddconnectGroup_info();

                if (reader.GetValue(0) != null && Convert.ToString(reader.GetValue(0)) != "")
                    item._id = Convert.ToString(reader.GetValue(0));
                if (reader.GetValue(1) != null && Convert.ToString(reader.GetValue(1)) != "")
                    item.name = reader.GetString(1);

                if (reader.GetValue(2) != null && Convert.ToString(reader.GetValue(2)) != "")
                    item.PCid = reader.GetString(2);


                ClaimReport_Server.Add(item);


            }
            return ClaimReport_Server;
        }

        private string sqlAddPCID(string findtext)
        {
            if (findtext.Contains("where"))
            {
                if (findtext.Length > 0)
                {
                    findtext += " And PCid like '%" + PCid + "%'";
                }
            }
            else
            {
                if (findtext.Length > 0)
                {
                    findtext += " where PCid like '%" + PCid + "%'";
                }

            }
            return findtext;
        }
        public int create_Addconnect_Server(List<Addconnect_info> AddMAPResult)
        {
            string sql = "insert into Addsend_connect(name,mail,address,phone,cmname,weblink,groupID,PCid) values ('" + AddMAPResult[0].name + "','" + AddMAPResult[0].mail + "','" + AddMAPResult[0].address + "','" + AddMAPResult[0].phone + "','" + AddMAPResult[0].cmname + "','" + AddMAPResult[0].weblink + "','" + AddMAPResult[0].groupID + "','" + PCid + "')";

            //int isrun = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.CONNECTION_STRING_BASE, sql, CommandType.Text, null);
            int isrun = MySqlHelper.ExecuteSql(sql);

            return isrun;
        }
        public List<Addconnect_info> findAddconnec(string findtext)
        {
            findtext = sqlAddPCID(findtext);
            MySql.Data.MySqlClient.MySqlDataReader reader = MySqlHelper.ExecuteReader(findtext);
            List<Addconnect_info> ClaimReport_Server = new List<Addconnect_info>();

            while (reader.Read())
            {
                Addconnect_info item = new Addconnect_info();
                if (reader.GetValue(0) != null && Convert.ToString(reader.GetValue(0)) != "")
                    item._id = Convert.ToString(reader.GetValue(0));

                if (reader.GetValue(1) != null && Convert.ToString(reader.GetValue(1)) != "")
                    item.name = reader.GetString(1);
                if (reader.GetValue(2) != null && Convert.ToString(reader.GetValue(2)) != "")
                    item.mail = reader.GetString(2);
                if (reader.GetValue(3) != null && Convert.ToString(reader.GetValue(3)) != "")
                    item.address = reader.GetString(3);
                if (reader.GetValue(4) != null && Convert.ToString(reader.GetValue(4)) != "")
                    item.phone = reader.GetString(4);
                if (reader.GetValue(5) != null && Convert.ToString(reader.GetValue(5)) != "")
                    item.cmname = reader.GetString(5);
                if (reader.GetValue(6) != null && Convert.ToString(reader.GetValue(6)) != "")
                    item.weblink = reader.GetString(6);
                if (reader.GetValue(7) != null && Convert.ToString(reader.GetValue(7)) != "")
                    item.groupID = reader.GetString(7);


                if (reader.GetValue(8) != null && Convert.ToString(reader.GetValue(8)) != "")
                    item.PCid = reader.GetString(8);


                ClaimReport_Server.Add(item);

                //这里做数据处理....
            }
            return ClaimReport_Server;
        }
        public void downcsv(DataGridView dataGridView)
        {
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Sorry , No Data Output !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".csv";
            saveFileDialog.Filter = "csv|*.csv";
            string strFileName = "  信息" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            saveFileDialog.FileName = strFileName;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                strFileName = saveFileDialog.FileName.ToString();
            }
            else
            {
                return;
            }
            FileStream fa = new FileStream(strFileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fa, Encoding.Unicode);
            string delimiter = "\t";
            string strHeader = "";
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                strHeader += dataGridView.Columns[i].HeaderText + delimiter;
            }
            sw.WriteLine(strHeader);

            //output rows data
            for (int j = 0; j < dataGridView.Rows.Count; j++)
            {
                string strRowValue = "";

                for (int k = 0; k < dataGridView.Columns.Count; k++)
                {
                    if (dataGridView.Rows[j].Cells[k].Value != null)
                    {
                        strRowValue += dataGridView.Rows[j].Cells[k].Value.ToString().Replace("\r\n", " ").Replace("\n", "") + delimiter;


                    }
                    else
                    {
                        strRowValue += dataGridView.Rows[j].Cells[k].Value + delimiter;
                    }
                }
                sw.WriteLine(strRowValue);
            }
            sw.Close();
            fa.Close();
            MessageBox.Show("Dear User, Down File  Successful ！", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public List<Addconnect_info> GetAddconnectExcelnfo(string Alist)
        {

            List<Addconnect_info> MAPPINGResult = new List<Addconnect_info>();
            try
            {
                System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Microsoft.Office.Interop.Excel.Application excelApp;
                {
                    string path = Alist;
                    excelApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook analyWK = excelApp.Workbooks.Open(path, Type.Missing, Type.Missing, Type.Missing,
                        "htc", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    Microsoft.Office.Interop.Excel.Worksheet WS = (Microsoft.Office.Interop.Excel.Worksheet)analyWK.Worksheets[1];
                    Microsoft.Office.Interop.Excel.Range rng;
                    rng = WS.Range[WS.Cells[1, 1], WS.Cells[WS.UsedRange.Rows.Count, 16]];
                    int rowCount = WS.UsedRange.Rows.Count;
                    object[,] o = new object[2, 1];
                    o = (object[,])rng.Value2;
                    int wscount = analyWK.Worksheets.Count;
                    clsCommHelp.CloseExcel(excelApp, analyWK);

                    for (int i = 2; i <= rowCount; i++)
                    {
                        Addconnect_info temp = new Addconnect_info();

                        #region 基础信息

                        temp.name = "";
                        if (o[i, 1] != null)
                            temp.name = o[i, 1].ToString().Trim();

                        temp.mail = "";
                        if (o[i, 2] != null)
                            temp.mail = o[i, 2].ToString().Trim();


                        temp.address = "";
                        if (o[i, 3] != null)
                            temp.address = o[i, 3].ToString().Trim();

                        //卖场代码

                        temp.phone = "";
                        if (o[i, 4] != null)
                            temp.phone = o[i, 4].ToString().Trim();

                        temp.cmname = "";
                        if (o[i, 5] != null)
                            temp.cmname = o[i, 5].ToString().Trim();

                        temp.weblink = "";
                        if (o[i, 6] != null)
                            temp.weblink = o[i, 6].ToString().Trim();

                        #endregion
                        MAPPINGResult.Add(temp);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: 01032" + ex);
                return null;

                throw;
            }
            return MAPPINGResult;

        }
        public int deletecustomer(string name)
        {
            string sql2 = "delete from Addsend_connect where   _id='" + name + "'";
            int isrun = MySqlHelper.ExecuteSql(sql2);

            return isrun;

        }
        public int deletegroup(string name)
        {
            string sql2 = "delete from sendconnectgroup where   _id='" + name + "'";
            int isrun = MySqlHelper.ExecuteSql(sql2);

            return isrun;

        }

        public int create_FromTo_Server(List<FromList_info> AddMAPResult)
        {
            string sql = "insert into FromList(mail,password,mark,groupID,PCid) values ('" + AddMAPResult[0].mail + "','" + AddMAPResult[0].password + "','" + AddMAPResult[0].mark + "','" + AddMAPResult[0].groupID + "','" + PCid + "')";

            int isrun = MySqlHelper.ExecuteSql(sql);

            return isrun;
        }
        public int create_FromGroup_Server(List<AddconnectGroup_info> AddMAPResult)
        {
            int isrun = 0;
            string sql = "insert into Fromgroup(name,PCid) values ('" + AddMAPResult[0].name + "','" + PCid + "')";

            isrun = MySqlHelper.ExecuteSql(sql);

            return isrun;
        }

        public List<FromList_info> findFromList(string findtext)
        {
            findtext = sqlAddPCID(findtext);
            //   findtext = "select * from FromList where mail='hzxdqwg01@vip.sina.com'And PCid  like '%178BFBFF00500F10%'";
            MySql.Data.MySqlClient.MySqlDataReader reader = MySqlHelper.ExecuteReader(findtext);
            List<FromList_info> ClaimReport_Server = new List<FromList_info>();

            while (reader.Read())
            {
                FromList_info item = new FromList_info();
                if (reader.GetValue(0) != null && Convert.ToString(reader.GetValue(0)) != "")
                    item._id = Convert.ToString(reader.GetValue(0));

                if (reader.GetValue(1) != null && Convert.ToString(reader.GetValue(1)) != "")
                    item.mail = reader.GetString(1);
                if (reader.GetValue(2) != null && Convert.ToString(reader.GetValue(2)) != "")
                    item.password = reader.GetString(2);
                if (reader.GetValue(3) != null && Convert.ToString(reader.GetValue(3)) != "")
                    item.mark = reader.GetString(3);
                if (reader.GetValue(4) != null && Convert.ToString(reader.GetValue(4)) != "")
                    item.groupID = reader.GetString(4);

                if (reader.GetValue(5) != null && Convert.ToString(reader.GetValue(5)) != "")
                    item.PCid = reader.GetString(5);

                ClaimReport_Server.Add(item);

                //这里做数据处理....
            }
            return ClaimReport_Server;
        }
        public List<AddconnectGroup_info> findFromGroup(string findtext)
        {
            findtext = sqlAddPCID(findtext);

            MySql.Data.MySqlClient.MySqlDataReader reader = MySqlHelper.ExecuteReader(findtext);

            List<AddconnectGroup_info> ClaimReport_Server = new List<AddconnectGroup_info>();

            while (reader.Read())
            {
                AddconnectGroup_info item = new AddconnectGroup_info();

                if (reader.GetValue(0) != null && Convert.ToString(reader.GetValue(0)) != "")
                    item._id = Convert.ToString(reader.GetValue(0));
                if (reader.GetValue(1) != null && Convert.ToString(reader.GetValue(1)) != "")
                    item.name = reader.GetString(1);
                if (reader.GetValue(2) != null && Convert.ToString(reader.GetValue(2)) != "")
                    item.PCid = reader.GetString(2);

                ClaimReport_Server.Add(item);


            }
            return ClaimReport_Server;
        }
        public List<FromList_info> GetFromListExcelnfo(string Alist)
        {

            List<FromList_info> MAPPINGResult = new List<FromList_info>();
            try
            {
                System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Microsoft.Office.Interop.Excel.Application excelApp;
                {
                    string path = Alist;
                    excelApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook analyWK = excelApp.Workbooks.Open(path, Type.Missing, Type.Missing, Type.Missing,
                        "htc", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    Microsoft.Office.Interop.Excel.Worksheet WS = (Microsoft.Office.Interop.Excel.Worksheet)analyWK.Worksheets[1];
                    Microsoft.Office.Interop.Excel.Range rng;
                    rng = WS.Range[WS.Cells[1, 1], WS.Cells[WS.UsedRange.Rows.Count, 16]];
                    int rowCount = WS.UsedRange.Rows.Count;
                    object[,] o = new object[2, 1];
                    o = (object[,])rng.Value2;
                    int wscount = analyWK.Worksheets.Count;
                    clsCommHelp.CloseExcel(excelApp, analyWK);

                    for (int i = 2; i <= rowCount; i++)
                    {
                        FromList_info temp = new FromList_info();

                        #region 基础信息

                        temp.mail = "";
                        if (o[i, 1] != null)
                            temp.mail = o[i, 1].ToString().Trim();

                        temp.password = "";
                        if (o[i, 2] != null)
                            temp.password = o[i, 2].ToString().Trim();


                        temp.mark = "";
                        if (o[i, 3] != null)
                            temp.mark = o[i, 3].ToString().Trim();



                        #endregion
                        MAPPINGResult.Add(temp);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: 01032" + ex);
                return null;

                throw;
            }
            return MAPPINGResult;

        }
        public int deleteFromList(string name)
        {
            string sql2 = "delete from FromList where   _id='" + name + "'";
            int isrun = MySqlHelper.ExecuteSql(sql2);

            return isrun;

        }
        public int deleteFromgroup(string name)
        {
            string sql2 = "delete from Fromgroup where   _id='" + name + "'";
            int isrun = MySqlHelper.ExecuteSql(sql2);

            return isrun;

        }

        public int create_mailTemplateServer(List<Template_info> AddMAPResult)
        {
            string sql = "insert into mailTemplate(subject,body,acc,groupID,PCid) values ('" + AddMAPResult[0].subject + "','" + AddMAPResult[0].body + "','" + AddMAPResult[0].acc + "','" + AddMAPResult[0].groupID + "','" + PCid + "')";

            int isrun = MySqlHelper.ExecuteSql(sql);

            return isrun;
        }
        public int update_mailTemplateServer(string findtext)
        {

            //  findtext = sqlAddPCID(findtext);


            int isrun = MySqlHelper.ExecuteSql(findtext);

            return isrun;
        }

        public List<Template_info> findTemplatetGroup(string findtext)
        {
            findtext = sqlAddPCID(findtext);
            MySql.Data.MySqlClient.MySqlDataReader reader = MySqlHelper.ExecuteReader(findtext);

            List<Template_info> ClaimReport_Server = new List<Template_info>();

            while (reader.Read())
            {
                Template_info item = new Template_info();

                if (reader.GetValue(0) != null && Convert.ToString(reader.GetValue(0)) != "")
                    item._id = Convert.ToString(reader.GetValue(0));
                if (reader.GetValue(1) != null && Convert.ToString(reader.GetValue(1)) != "")
                    item.subject = reader.GetString(1);

                if (reader.GetValue(2) != null && Convert.ToString(reader.GetValue(2)) != "")
                    item.body = reader.GetString(2);

                if (reader.GetValue(3) != null && Convert.ToString(reader.GetValue(3)) != "")
                    item.acc = reader.GetString(3);
                if (reader.GetValue(4) != null && Convert.ToString(reader.GetValue(4)) != "")
                    item.groupID = reader.GetString(4);

                if (reader.GetValue(5) != null && Convert.ToString(reader.GetValue(5)) != "")
                    item.PCid = reader.GetString(5);

                ClaimReport_Server.Add(item);


            }
            return ClaimReport_Server;
        }
        public List<Template_info> GetUploadTemplateExcelnfo(string Alist)
        {

            List<Template_info> MAPPINGResult = new List<Template_info>();
            try
            {
                System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Microsoft.Office.Interop.Excel.Application excelApp;
                {
                    string path = Alist;
                    excelApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook analyWK = excelApp.Workbooks.Open(path, Type.Missing, Type.Missing, Type.Missing,
                        "htc", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    Microsoft.Office.Interop.Excel.Worksheet WS = (Microsoft.Office.Interop.Excel.Worksheet)analyWK.Worksheets[1];
                    Microsoft.Office.Interop.Excel.Range rng;
                    rng = WS.Range[WS.Cells[1, 1], WS.Cells[WS.UsedRange.Rows.Count, 16]];
                    int rowCount = WS.UsedRange.Rows.Count;
                    object[,] o = new object[2, 1];
                    o = (object[,])rng.Value2;
                    int wscount = analyWK.Worksheets.Count;
                    clsCommHelp.CloseExcel(excelApp, analyWK);

                    for (int i = 2; i <= rowCount; i++)
                    {
                        Template_info temp = new Template_info();

                        #region 基础信息

                        temp.subject = "";
                        if (o[i, 1] != null)
                            temp.subject = o[i, 1].ToString().Trim();

                        temp.body = "";
                        if (o[i, 2] != null)
                            temp.body = o[i, 2].ToString().Trim();


                        temp.acc = "";
                        if (o[i, 3] != null)
                            temp.acc = o[i, 3].ToString().Trim();



                        #endregion
                        MAPPINGResult.Add(temp);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: 01032" + ex);
                return null;

                throw;
            }
            return MAPPINGResult;

        }

        public int create_timer_Server(Timer_info AddMAPResult)
        {
            string sql = "insert into Timer(time_start,time_end,TemplateID,mail,CCmail,formto,subject,body,acc ,groupID,PCid,status  ) values ('" + AddMAPResult.time_start + "','" + AddMAPResult.time_end + "','" + AddMAPResult.TemplateID + "','" + AddMAPResult.mail + "','" + AddMAPResult.CCmail + "','" + AddMAPResult.formto + "','" + AddMAPResult.subject + "','" + AddMAPResult.body + "','" + AddMAPResult.acc + "','" + AddMAPResult.groupID + "','" + PCid + "','" + AddMAPResult.status + "')";

            // int isrun = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.CONNECTION_STRING_BASE, sql, CommandType.Text, null);
            int isrun = MySqlHelper.ExecuteSql(sql);

            return isrun;
        }

        public List<Timer_info> findTimer(string findtext)
        {


            findtext = sqlAddPCID(findtext);
            MySql.Data.MySqlClient.MySqlDataReader reader = MySqlHelper.ExecuteReader(findtext);

            List<Timer_info> ClaimReport_Server = new List<Timer_info>();

            while (reader.Read())
            {
                Timer_info item = new Timer_info();


                if (reader.GetValue(0) != null && Convert.ToString(reader.GetValue(0)) != "")
                    item._id = Convert.ToString(reader.GetValue(0));


                if (reader.GetValue(1) != null && Convert.ToString(reader.GetValue(1)) != "")
                    item.time_start = Convert.ToString(reader.GetValue(1));
                if (reader.GetValue(2) != null && Convert.ToString(reader.GetValue(2)) != "")
                    item.time_end = reader.GetString(2);

                if (reader.GetValue(3) != null && Convert.ToString(reader.GetValue(3)) != "")
                    item.TemplateID = reader.GetString(3);

                if (reader.GetValue(4) != null && Convert.ToString(reader.GetValue(4)) != "")
                    item.mail = reader.GetString(4);

                if (reader.GetValue(5) != null && Convert.ToString(reader.GetValue(5)) != "")
                    item.CCmail = reader.GetString(5);
                if (reader.GetValue(6) != null && Convert.ToString(reader.GetValue(6)) != "")
                    item.formto = reader.GetString(6);
                if (reader.GetValue(7) != null && Convert.ToString(reader.GetValue(7)) != "")
                    item.subject = reader.GetString(7);
                if (reader.GetValue(8) != null && Convert.ToString(reader.GetValue(8)) != "")
                    item.body = reader.GetString(8);

                if (reader.GetValue(9) != null && Convert.ToString(reader.GetValue(9)) != "")
                    item.acc = reader.GetString(9);

                if (reader.GetValue(10) != null && Convert.ToString(reader.GetValue(10)) != "")
                    item.groupID = reader.GetString(10);

                if (reader.GetValue(11) != null && Convert.ToString(reader.GetValue(11)) != "")
                    item.PCid = reader.GetString(11);


                if (reader.GetValue(12) != null && Convert.ToString(reader.GetValue(12)) != "")
                    item.status = reader.GetString(12);

                ClaimReport_Server.Add(item);


            }
            return ClaimReport_Server;
        }


        #endregion
    }
}
