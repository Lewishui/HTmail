using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;


namespace clsBuiness
{
    public class clsAllnew
    {
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


    }
}
