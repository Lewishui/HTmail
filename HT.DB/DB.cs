using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace HT.DB
{
    public class AddconnectGroup_info
    {
        public string _id { get; set; }//玩法种类
        public string name { get; set; }//玩法种类
        public string PCid { get; set; }//玩法种类
  
    }
    public class clsuserinfo
    {
        public string Order_id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string Btype { get; set; }
        public string denglushijian { get; set; }
        public string Createdate { get; set; }
        public string AdminIS { get; set; }
        public string jigoudaima { get; set; }
        public string pid { get; set; }

    }
    public class Addconnect_info
    {
        public string _id { get; set; }//玩法种类
        public string name { get; set; }//玩法种类
        public string mail { get; set; }//玩法种类
        public string address { get; set; }//玩法种类
        public string phone { get; set; }//玩法种类
        public string cmname { get; set; }//玩法种类
        public string weblink { get; set; }//玩法种类
        public string groupID { get; set; }//玩法种类
        public string PCid { get; set; }//玩法种类
  
        
    }
    public class FromGroup_info
    {
        public string _id { get; set; }//玩法种类
        public string name { get; set; }//玩法种类
        public string PCid { get; set; }//玩法种类
  
    }
    public class FromList_info
    {
        public string _id { get; set; }//玩法种类
     
        public string mail { get; set; }//玩法种类
        public string password { get; set; }//玩法种类
        public string mark { get; set; }//玩法种类
        public string groupID { get; set; }//玩法种类
        public string PCid { get; set; }//玩法种类
  


    }
    public class Template_info
    {
        public string _id { get; set; } 

        public string subject { get; set; } 
        public string body { get; set; } 
        public string acc { get; set; } 
        public string groupID { get; set; } 
        public string PCid { get; set; } 
  


    }
    public class AutoSend_info
    {
        public string _id { get; set; } 

        public string zhuangtai { get; set; }
        public string zhuti { get; set; }
        public string neirong { get; set; }
        public string shoujianren { get; set; }
        public string fajianren { get; set; }
        public string kaishijian { get; set; }
        public string tingzhishijian { get; set; }
        public string jindu { get; set; }
        public string yaoqiuyueduhuizhi { get; set; }
        public string youxianji { get; set; } 

    }
    public class Timer_info
    {
        public string _id { get; set; }

        public string time_start { get; set; }
        public string time_end { get; set; }        
        public string TemplateID { get; set; }
        public string mail { get; set; } 
        public string CCmail { get; set; }
        public string formto { get; set; }     
    
        public string subject { get; set; }
        public string body { get; set; }
        public string acc { get; set; }
        public string groupID { get; set; }
        public string PCid { get; set; }
        public string status { get; set; }
       

    }


    public class softTime_info
    {
        public string _id { get; set; }//玩法种类

        public string starttime { get; set; }//玩法种类
        public string name { get; set; }//玩法种类
        public string endtime { get; set; }//玩法种类
        public string soft_name { get; set; }//玩法种类
        public string denglushijian { get; set; }//玩法种类
      

        public string password { get; set; }//玩法种类
        public string pid { get; set; }//玩法种类
        public string mark1 { get; set; }//玩法种类
        public string mark2 { get; set; }//玩法种类
        public string mark3 { get; set; }//玩法种类
        public string mark4 { get; set; }//玩法种类
        public string mark5 { get; set; }//玩法种类
    }
    public class clsQQquninfo
    {
        public string Order_id { get; set; }
        public string qun_name { get; set; }
        public string send_body { get; set; }
        public string  is_timer{ get; set; }
        public string send_time { get; set; }
        public string mark1 { get; set; }
        public string mark2 { get; set; }
        public string mark3 { get; set; }
        public string mark4 { get; set; }
        public string mark5 { get; set; }

    }
    public class clsalter_message
    {
        public string _id { get; set; }
        public string project_id { get; set; }
        public string project_name { get; set; }
        public string text { get; set; }
        public string mark1 { get; set; }
        public string mark2 { get; set; }
        public string mark3 { get; set; }
        public string mark4 { get; set; }
        public string mark5 { get; set; }

    }
}
