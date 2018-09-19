using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
                client.Host = "smtp.sina.com";
                client.UseDefaultCredentials = false;
                //
                //启用功能修改处
                //
                client.Credentials = new System.Net.NetworkCredential("hzxdqwg01@sina.cn", "hm526355");
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //
                //启用功能修改处
                //
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("hzxdqwg01@sina.cn", "hm526355");
                message.Subject = "忘记密码";
                message.Body = "您的登录名户和密码分别为:" + "hzxdqwg01@sina.cn" + "  " + "hm526355";
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
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

    }
}
