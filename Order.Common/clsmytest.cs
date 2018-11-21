using HT.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Order.Common
{
    public class clsmytest
    {

        public  bool checkname()
        {
            #region Noway
            //bool success = NewMySqlHelper.DbConnectable();

            //if (success == false)
            //{
            //    MessageBox.Show("系统网络异常,请保持网络畅通或联系开发人员 !");
            //    return;
            //}

            string strSelect = "select * from control_soft_time where name='" + "QQSend01" + "'";
            List<softTime_info> list_Server = new List<softTime_info>();
            list_Server = findsoftTime(strSelect);
            DateTime oldDate = DateTime.Now;
            DateTime dt3;
            string endday = DateTime.Now.ToString("yyyy/MM/dd");
            dt3 = Convert.ToDateTime(endday);
            DateTime dt2;
            if (list_Server.Count == 0 || list_Server[0].endtime == null || list_Server[0].endtime == "")
            {
                MessageBox.Show("系统网络异常,请保持网络畅通或联系开发人员 !");
                return false;
            }
            else
                dt2 = Convert.ToDateTime(list_Server[0].endtime);

            TimeSpan ts = dt2 - dt3;
            int timeTotal = ts.Days;

            if (timeTotal > 0 && timeTotal < 10)
            {
                MessageBox.Show("本系统【HTmail】服务即将到期,请及时续费以免影响使用 !\r\n\r\n温馨提示：联系方式网址：www.yhocn.com\r\nQQ：512250428\r\n微信：bqwl07910", "服务到期", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            if (timeTotal < 0)
            {
                MessageBox.Show("本系统【HTmail】服务到期,请及时续费 !\r\n\r\n温馨提示：联系方式网址：www.yhocn.com\r\nQQ：512250428\r\n微信：bqwl07910", "服务到期", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Application.Exit();

                //return;
                return false;

            }
            return true;

            #endregion
        }
        public List<softTime_info> findsoftTime(string findtext)
        {
            //    findtext = sqlAddPCID(findtext);
            MySql.Data.MySqlClient.MySqlDataReader reader = NewMySqlHelper.ExecuteReader(findtext);
            List<softTime_info> ClaimReport_Server = new List<softTime_info>();

            while (reader.Read())
            {
                softTime_info item = new softTime_info();
                if (reader.GetValue(0) != null && Convert.ToString(reader.GetValue(0)) != "")
                    item._id = Convert.ToString(reader.GetValue(0));

                if (reader.GetValue(1) != null && Convert.ToString(reader.GetValue(1)) != "")
                    item.name = reader.GetString(1);
                if (reader.GetValue(2) != null && Convert.ToString(reader.GetValue(2)) != "")
                    item.starttime = reader.GetString(2);
                if (reader.GetValue(3) != null && Convert.ToString(reader.GetValue(3)) != "")
                    item.endtime = reader.GetString(3);

                ClaimReport_Server.Add(item);

                //这里做数据处理....
            }
            return ClaimReport_Server;
        }
    }
}
