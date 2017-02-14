using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Nodes.UpdateEngine;

namespace Nodes.Updater
{
    public partial class Form1 : Form
    {
        private string tempUpdatePath = string.Empty;
        private string m_appId, m_appExeName, m_files, m_downPath, m_newVer, m_desc;
        private int m_fileCount = 0;

        public Form1(string[] args)
            : this()
        {
            m_appId = args[0];
            m_appExeName = args[1];
            m_newVer = args[2];
            m_downPath = args[3];
            m_files = args[4];
            m_desc = args[5];
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void OnCancel(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            timer1.Enabled = false;
            Application.DoEvents();
            GetUpdateList();
            label1.Text = "组件更新列表";
            textBox1.Text = "本次更新内容\r\n" + m_desc;
            btnUpdate.Enabled = true;
            Cursor.Current = Cursors.Default;

        }

        private void GetUpdateList()
        {
            try
            {
                string[] fileArray = m_files.Split(',');
                m_fileCount = fileArray.Length;

                foreach(string file in fileArray)
                    lvUpdateList.Items.Add(new ListViewItem(new string[]{file, ""}));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        

        private void OnUpdate(object sender, EventArgs e)
        {
            try
            {
                if (m_fileCount > 0)
                {
                    if (btnUpdate.Text.Equals("更新"))
                    {
                        if (MessageBox.Show("在更新过程中程序将会自动关闭，点击“更新”将开始下载。", "自动更新",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.OK)
                            return;

                        btnUpdate.Enabled = false;

                        DownUpdateFile();
                    }
                    else
                    {
                        CopyFile(tempUpdatePath, Directory.GetCurrentDirectory());
                        System.IO.Directory.Delete(tempUpdatePath, true);

                        //更新本地版本号
                        UpdateHelper.UpdateVersion(m_appId, m_newVer);
                        Process pro = new Process();
                        pro.StartInfo.FileName = Application.StartupPath + System.IO.Path.DirectorySeparatorChar + m_appExeName;
                        pro.StartInfo.CreateNoWindow = true;
                        pro.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        pro.Start();

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("没有可用的更新!");
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void DownUpdateFile()
        {
            this.Cursor = Cursors.WaitCursor;

            Process[] allProcess = Process.GetProcesses();
            foreach (Process p in allProcess)
            {
                if (p.ProcessName.ToLower() + ".exe" == m_appExeName.ToLower())
                {
                    for (int i = 0; i < p.Threads.Count; i++)
                        p.Threads[i].Dispose();

                    p.Kill();
                }
            }

            tempUpdatePath = Path.GetTempPath() + "\\" + "_" + m_appExeName + "\\";
            for (int i = 0; i < this.lvUpdateList.Items.Count; i++)
            {
                string UpdateFile = lvUpdateList.Items[i].Text.Trim();
                string updateFileUrl = m_downPath + "//" + lvUpdateList.Items[i].Text.Trim();
                long fileLength = 0;

                WebRequest webReq = WebRequest.Create(updateFileUrl);
                WebResponse webRes = webReq.GetResponse();
                fileLength = webRes.ContentLength;

                Stream srm = webRes.GetResponseStream();
                StreamReader srmReader = new StreamReader(srm);
                byte[] bufferbyte = new byte[fileLength];
                int allByte = (int)bufferbyte.Length;
                int startByte = 0;

                while (fileLength > 0)
                {
                    Application.DoEvents();
                    int downByte = srm.Read(bufferbyte, startByte, allByte);
                    if (downByte == 0) { break; };
                    startByte += downByte;
                    allByte -= downByte;

                    float part = (float)startByte / 1024;
                    float total = (float)bufferbyte.Length / 1024;
                    int percent = Convert.ToInt32((part / total) * 100);

                    this.lvUpdateList.Items[i].SubItems[1].Text = percent.ToString() + "%";
                }

                string tempPath = tempUpdatePath + UpdateFile;
                CreateDirtory(tempPath);
                FileStream fs = new FileStream(tempPath, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(bufferbyte, 0, bufferbyte.Length);
                srm.Close();
                srmReader.Close();
                fs.Close();
            }
            btnUpdate.Text = "完成";
            btnUpdate.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        //创建目录
        private void CreateDirtory(string path)
        {
            if (!File.Exists(path))
            {
                string[] dirArray = path.Split('\\');
                string temp = string.Empty;
                for (int i = 0; i < dirArray.Length - 1; i++)
                {
                    temp += dirArray[i].Trim() + "\\";
                    if (!Directory.Exists(temp))
                        Directory.CreateDirectory(temp);
                }
            }
        }

        //复制文件;
        public void CopyFile(string sourcePath, string objPath)
        {
            if (!Directory.Exists(objPath))
                Directory.CreateDirectory(objPath);

            string[] files = Directory.GetFiles(sourcePath);
            for (int i = 0; i < files.Length; i++)
            {
                string[] childfile = files[i].Split('\\');
                File.Copy(files[i], objPath + @"\" + childfile[childfile.Length - 1], true);
            }
            
            string[] dirs = Directory.GetDirectories(sourcePath);
            for (int i = 0; i < dirs.Length; i++)
            {
                string[] childdir = dirs[i].Split('\\');
                CopyFile(dirs[i], objPath + @"\" + childdir[childdir.Length - 1]);
            }
        }
    }
}