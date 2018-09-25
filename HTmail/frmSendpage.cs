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
    public partial class frmSendpage : Form
    {

        string path;
        List<string> filename = new List<string>();

        public frmSendpage()
        {
            InitializeComponent();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

            string fajianren = "";


            wirite_txt();


            string path = AppDomain.CurrentDomain.BaseDirectory + "System\\mail";
            string dir = @"C:\Program Files (x86)\HTmail\System\\mail";
            CopyFolder(path, dir);

          //  File.Copy(path, dir, true);



            fajianren = textBox2.Text;

            if (fajianren != "" && fajianren.Contains("qq.com"))
            {

                string ZFCEPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""), "");
                System.Diagnostics.Process.Start("stop Q.exe", ZFCEPath);
            }
            if (fajianren != "" && fajianren.Contains("sina.com"))
            {

                string ZFCEPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""), "");
                System.Diagnostics.Process.Start("catch XL.exe", ZFCEPath);
            }


        }
        public static void CopyFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    //目标目录不存在则创建
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("创建目标目录失败：" + ex.Message);
                    }
                }
                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    File.Copy(c, destFile, true);//覆盖模式
                });
                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));
                folders.ForEach(c =>
                {
                    string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //采用递归的方法实现
                    CopyFolder(c, destDir);
                });
            }
            else
            {
                throw new DirectoryNotFoundException("源目录不存在！");
            }
        }


        private void wirite_txt()
        {
            string A_Path = AppDomain.CurrentDomain.BaseDirectory + "System\\mail\\sendto.txt";

            StreamWriter sw = new StreamWriter(A_Path);
            sw.WriteLine(textBox1.Text);
            sw.Flush();
            sw.Close();


            A_Path = AppDomain.CurrentDomain.BaseDirectory + "System\\mail\\ccto.txt";

            sw = new StreamWriter(A_Path);
            sw.WriteLine(textBox5.Text);
            sw.Flush();
            sw.Close();

            A_Path = AppDomain.CurrentDomain.BaseDirectory + "System\\mail\\fromto.txt";

            sw = new StreamWriter(A_Path);
            sw.WriteLine(textBox2.Text);
            sw.Flush();
            sw.Close();


            A_Path = AppDomain.CurrentDomain.BaseDirectory + "System\\mail\\subject.txt";

            sw = new StreamWriter(A_Path);
            sw.WriteLine(textBox3.Text);
            sw.Flush();


            sw.Close();
            A_Path = AppDomain.CurrentDomain.BaseDirectory + "System\\mail\\body.txt";

            sw = new StreamWriter(A_Path);
            sw.WriteLine(textBox4.Text);
            sw.Flush();
            sw.Close();

            A_Path = AppDomain.CurrentDomain.BaseDirectory + "System\\mail\\acc.txt";

            sw = new StreamWriter(A_Path);
            for (int i = 0; i < filename.Count; i++)
                sw.WriteLine(filename[i]);
            sw.Flush();
            sw.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filename = new List<string>();

            OpenFileDialog tbox = new OpenFileDialog();
            tbox.Multiselect = false;
            tbox.Filter = "所有文件|*.*";
            tbox.Multiselect = true;
            tbox.SupportMultiDottedExtensions = true;
            if (tbox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                listBox1.Items.Clear();

                foreach (string s in tbox.SafeFileNames)
                {
                    filename.Add(tbox.FileName);

                    listBox1.Items.Add(s);
                }
            }
        }
    }
}
