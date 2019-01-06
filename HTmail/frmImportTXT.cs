using clsBuiness;
using HT.DB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTmail
{
    public partial class frmImportTXT : Form
    {
        clsAllnew BusinessHelp;
        public frmImportTXT()
        {
            InitializeComponent();
            BusinessHelp = new clsAllnew();
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "System\\Tel.txt";
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\UploadFrom.txt";
            File.Copy(path, dir, true);

            MessageBox.Show("下载完成,请到桌面查看！");
            //export();
         
            //System.Resources.ResourceManager rm = Properties.Resources.ResourceManager;

            //FileStream Stream = new FileStream(dir, FileMode.OpenOrCreate);

            //BinaryFormatter bin = new BinaryFormatter();

            //try
            //{
            //    bin.Serialize(Stream, rm.GetObject("Tel", null));
                
            //    Stream.Close();
            //}
            //catch (InvalidOperationException)
            //{
            //    throw;
            //}

        }

        private void export()
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\UploadFrom.txt";
           


            ResourceReader res = new ResourceReader("MSVirtualEvent.g.resources");//该文件放到bin
            IDictionaryEnumerator dics = res.GetEnumerator();
            while (dics.MoveNext())
            {
                Stream s = (Stream)dics.Value;
                int fileSize = (int)s.Length;
                byte[] fileContent = new byte[fileSize];
                s.Read(fileContent, 0, fileSize);
                FileStream fs;
                string filepath = dics.Key.ToString();
                filepath = Path.Combine("C://", filepath); //保存到指定目录
                filepath = Path.GetFullPath(filepath);
                var p = Path.GetDirectoryName(filepath);//要创建的目录
                if (!Directory.Exists(p))
                {
                    Directory.CreateDirectory(p);
                }

                FileInfo fi = new System.IO.FileInfo(filepath);
                fs = fi.OpenWrite();
                fs.Write(fileContent, 0, fileSize);
                fs.Close();
            }

            res.Close();


        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            string[] fileText =  File.ReadAllLines(pathTextBox.Text );

           for (int i = 0; i < fileText.Length; i++)
           {
               List<clsQQquninfo> addOrderQUNlist_Server = new List<clsQQquninfo>();
          
                string[] temp1 = System.Text.RegularExpressions.Regex.Split(fileText[i], " ");

                clsQQquninfo temp = new clsQQquninfo();
                if (temp1.Length < 2)
                    continue;

                temp.qun_name = temp1[0];
                temp.send_body = temp1[1];
                temp.is_timer = temp1[2];
                if (temp.is_timer == "是")
                {
                    DateTime dateclose = Convert.ToDateTime(temp1[3]);

                    string accrualselecttime = dateclose.ToString("yyyy/MM/dd/HH/mm").ToString();


                    temp.send_time = accrualselecttime;
                }
                if (temp1.Length>3)
                temp.mark1 = temp1[3];//代表 没有图片发送
               else
                    temp.mark1 = "否";

                addOrderQUNlist_Server.Add(temp);
                int isok = BusinessHelp.create_QQqun_Server(addOrderQUNlist_Server);

                if (isok != 1)
                {
                    MessageBox.Show("保存失败，请检查录入信息是否有误！");
                    
                }
            }
        
        }

        private void openFileBtton_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pathTextBox.Text = openFileDialog1.FileName;
            }
        }
    }
}
