﻿using clsBuiness;
using HT.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTmail
{
    public partial class frmBath_uploadSend : Form
    {
        string path;
        string groupID;

        public frmBath_uploadSend(string groupID1)
        {
            InitializeComponent();
            groupID = groupID1;


        }

        private void importButton_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "System\\UploadSend.xls";
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\UploadSend.xls";
            File.Copy(path, dir, true);

            MessageBox.Show("下载完成,请到桌面查看！");


        }

        private void openFileBtton_Click(object sender, EventArgs e)
        {
            OpenFileDialog tbox = new OpenFileDialog();
            tbox.Multiselect = false;
            tbox.Filter = "Excel Files(*.xls,*.xlsx,*.xlsm,*.xlsb)|*.xls;*.xlsx;*.xlsm;*.xlsb";
            if (tbox.ShowDialog() == DialogResult.OK)
            {
                path = tbox.FileName;
                pathTextBox.Text = tbox.FileName;
            }
            if (path == null || path == "")
                return;


        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (path == null || path == "")
                return;
            clsAllnew BusinessHelp = new clsAllnew();
            List<Addconnect_info> KEYResult = BusinessHelp.GetAddconnectExcelnfo(path);
            if (KEYResult != null)
            {
                int ISURN = 0;
                foreach (Addconnect_info item in KEYResult)
                {
                    item.groupID = groupID;
                    List<Addconnect_info> userlist_Server1 = new List<Addconnect_info>();
                    Addconnect_info item1 = new Addconnect_info();
                    if (item.mail == null || item.mail == "")
                    {
                        continue;
                    }
                    userlist_Server1.Add(item);
                    ISURN = BusinessHelp.create_Addconnect_Server(userlist_Server1);
                }
                if (ISURN == 1)
                {
                    if (MessageBox.Show(" 创建成功 , 是否继续添加 ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {

                    }
                    else
                        this.Close();
                }
                if (ISURN == 0)
                {
                    MessageBox.Show("创建失败,请检查是否录入有误！");
                }
            }
        }
    }
}
