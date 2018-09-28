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
}
