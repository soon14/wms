using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.DBHelper;
using Nodes.UI;
using Nodes.Entities;
using Nodes.Shares;

namespace Nodes.Outstore
{
    public partial class FrmScanContainerNew : DevExpress.XtraEditors.XtraForm
    {
        public FrmScanContainerNew()
        {
            InitializeComponent();
        }
        GroupingTasksDal GetUserPICK = new GroupingTasksDal();

        DataTable Gdt = new DataTable();
        DataTable Idt = new DataTable();




        private int BoxNum = 1;

        //绑定签到拣货人
        public void LoadDataAndBindGrid()
        {
            try
            {
                bindingSource1.DataSource = this.GetUserPICK.GetUserOnline();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        //绑定数据
        private void DataGetGronpingTask()
        {

            Gdt = GetUserPICK.GetGronpingTask(searchLookUpEdit1.Text);
            //Gdt.Rows[0]["PIECES"] = 6;
            this.gridTasks.DataSource = Gdt;
            this.gridTasks.RefreshDataSource();
            this.txtCarCode.Focus();
        }


        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //绑定当前拣货员锁定单据及所有零散拣货单据


            this.txtCarCode.Enabled = true;
            this.txtCarCode.Focus();

            this.txtCarCode.Text = "";
            this.txtBoxCade.Text = "";
            this.txtBoxCade.Enabled = false;

            BoxClear();
            BoxNum = 1;

            if (this.searchLookUpEdit1.Text != "")
            {
                if (GetUserPICK.GetUserTasksNum(this.searchLookUpEdit1.Text) != 0)
                {
                    splashLabel1.Show("此拣货员正在执行任务无法分配！", false);

                    Gdt.Rows.Clear();
                    this.gridTasks.DataSource = Gdt;
                    return;
                }
            }
            else
            {
                return;
            }





            DataGetGronpingTask();


        }



        private void FrmScanContainerNew_Load(object sender, EventArgs e)
        {


            //绑定当前签到拣货员
            LoadDataAndBindGrid();

            this.txtCarCode.Focus();
            this.txtBoxCade.Enabled = false;


            Idt.Columns.Add("PV_CODE");
            Idt.Columns.Add("CT_CODE");
            Idt.Columns.Add("Tasks_ID");
            Idt.Columns.Add("USER_CODE");
            Idt.Columns.Add("CREATE_USER");
            Idt.Columns.Add("BILL_ID");

        }
        //清空人员
        private void ButUser_Click(object sender, EventArgs e)
        {
            this.searchLookUpEdit1.Text = "";
            this.txtCarCode.Enabled = true;
            this.txtCarCode.Focus();

            this.txtCarCode.Text = "";
            this.txtBoxCade.Text = "";
            this.txtBoxCade.Enabled = false;

            BoxClear();

            BoxNum = 1;



            this.gridTasks.DataSource = null;



        }

        private void BoxClear()
        {
            this.Box1.Items.Clear();
            this.Box2.Items.Clear();
            this.Box3.Items.Clear();
            this.Box4.Items.Clear();
            this.Box5.Items.Clear();
            this.Box6.Items.Clear();

            this.Box1.Text = "";
            this.Box2.Text = "";
            this.Box3.Text = "";
            this.Box4.Text = "";
            this.Box5.Text = "";

            Idt.Rows.Clear();

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            GetUserPICK.GetGronpingTask("");
        }


        private bool IFCode(string Code, string key)
        {
            string s = Code.Substring(0, 1);
            if (s == key)
            {
                return true;
            }
            return false;
        }


        //小车回车事件
        private void txtCarCode_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter && e.KeyValue == 13)
            {
                if (searchLookUpEdit1.Text == "")
                {
                    splashLabel1.Show("请先选择拣货人！", false);
                    this.txtCarCode.Text = "";
                    return;
                }

                if (txtCarCode.Text == "")
                {
                    splashLabel1.Show("请扫描物流车条码！", false);
                    return;
                }


                //判断小车条码是否符合要求

                if (txtCarCode.Text.Length < 3)
                {
                    splashLabel1.Show("扫描的条码不符合要求！", false);
                    return;
                }
                //判断条码首字母是否为小车
                if (!IFCode(txtCarCode.Text.Trim(), "8"))
                {
                    splashLabel1.Show("扫描的条码不属于物流笼车！", false);
                    return;
                }
                //判断小车条码是否在数据库中
                if (!GetUserPICK.IFC_CODE(txtCarCode.Text.Trim()))
                {
                    splashLabel1.Show("此条码不在本系统中！", false);
                    return;
                }
                //判断小车是否正在被使用




                if (Gdt.Rows.Count == 0)
                {
                    splashLabel1.Show("无分配单据！", false);
                    return;
                }
                this.txtCarCode.Enabled = false;
                this.txtBoxCade.Enabled = true;
                this.txtBoxCade.Text = "";
                this.txtBoxCade.Focus();

                BoxClear();
            }


            //判断是否是系统中小车条码

            //不是提示

            //
        }
        //小车清空
        private void ButCar_Click(object sender, EventArgs e)
        {
            this.txtCarCode.Enabled = true;
            this.txtCarCode.Focus();

            this.txtCarCode.Text = "";
            this.txtBoxCade.Text = "";
            this.txtBoxCade.Enabled = false;

            this.Box1.Items.Clear();
            this.Box2.Items.Clear();
            this.Box3.Items.Clear();
            this.Box4.Items.Clear();
            this.Box5.Items.Clear();
            this.Box6.Items.Clear();
            this.Box1.Text = "";
            this.Box2.Text = "";
            this.Box3.Text = "";
            this.Box4.Text = "";
            this.Box5.Text = "";
            this.Box6.Text = "";


            BoxNum = 1;

            if (searchLookUpEdit1.Text == "")
            {
                return;
            }
            DataGetGronpingTask();

        }


        //扫描物流箱
        private void txtBoxCade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.KeyValue == 13)
            {
                if (txtBoxCade.Text == "")
                {
                    splashLabel1.Show("请扫描物流箱条码！", false);
                    return;
                }




                //判断小车条码是否符合要求

                if (txtBoxCade.Text.Length < 3)
                {
                    splashLabel1.Show("扫描的条码不符合要求！", false);
                    return;
                }
                //判断条码首字母是否为小车
                if (!IFCode(txtBoxCade.Text.Trim(),"2"))
                {
                    splashLabel1.Show("扫描的条码不属于物流箱！", false);
                    return;
                }
                //判断小车条码是否在数据库中
                if (!GetUserPICK.IFC_CODE(txtBoxCade.Text.Trim()))
                {
                    splashLabel1.Show("此条码不在本系统中！", false);
                    return;
                }



                if (Gdt.Rows.Count == 0)
                {
                    splashLabel1.Show("无可分配单据！", false);
                    return;
                }




                //判断物流箱
                switch (BoxNum)
                {
                    case 1:
                        this.Box1.Items.Add("1号物流箱");
                        this.Box1.Items.Add("物流箱条码： " + "" + txtBoxCade.Text + "");
                        this.Box1.Items.Add("单号：" + Gdt.Rows[0]["BILL_NO"].ToString());
                        this.Box1.Text = Gdt.Rows[0]["TASK_ID"].ToString();

                        BoxNum += 1;
                        break;
                    case 2:
                        this.Box2.Items.Add("2号物流箱");
                        this.Box2.Items.Add("物流箱条码： " + "" + txtBoxCade.Text + "");
                        this.Box2.Items.Add("单号：" + Gdt.Rows[0]["BILL_NO"].ToString());
                        this.Box2.Text = Gdt.Rows[0]["TASK_ID"].ToString();

                        BoxNum += 1;
                        break;
                    case 3:
                        this.Box3.Items.Add("3号物流箱");
                        this.Box3.Items.Add("物流箱条码： " + "" + txtBoxCade.Text + "");
                        this.Box3.Items.Add("单号：" + Gdt.Rows[0]["BILL_NO"].ToString());
                        this.Box3.Text = Gdt.Rows[0]["TASK_ID"].ToString();

                        BoxNum += 1;
                        break;
                    case 4:
                        this.Box4.Items.Add("4号物流箱");
                        this.Box4.Items.Add("物流箱条码： " + "" + txtBoxCade.Text + "");
                        this.Box4.Items.Add("单号：" + Gdt.Rows[0]["BILL_NO"].ToString());
                        this.Box4.Text = Gdt.Rows[0]["TASK_ID"].ToString();

                        BoxNum += 1;
                        break;
                    case 5:
                        this.Box5.Items.Add("5号物流箱");
                        this.Box5.Items.Add("物流箱条码： " + "" + txtBoxCade.Text + "");
                        this.Box5.Items.Add("单号：" + Gdt.Rows[0]["BILL_NO"].ToString());
                        this.Box5.Text = Gdt.Rows[0]["TASK_ID"].ToString();

                        BoxNum += 1;
                        break;
                    case 6:
                        this.Box6.Items.Add("6号物流箱");
                        this.Box6.Items.Add("物流箱条码： " + "" + txtBoxCade.Text + "");
                        this.Box6.Items.Add("单号：" + Gdt.Rows[0]["BILL_NO"].ToString());
                        this.Box6.Text = Gdt.Rows[0]["TASK_ID"].ToString();

                        BoxNum += 1;
                        break;
                    case 7:
                        splashLabel1.Show("扫描完毕请保存！", false);
                        this.txtBoxCade.Text = "";
                        return;
                }



                //Idt.Columns.Add("PV_CODE");
                //Idt.Columns.Add("CT_CODE");
                //Idt.Columns.Add("Tasks_ID");
                //Idt.Columns.Add("USER_CODE");
                //Idt.Columns.Add("CREATE_USER");


                DataRow dr = Idt.NewRow();
                object[] objs = { this.txtCarCode.Text.ToString(), this.txtBoxCade.Text.ToString(), 
                                    Gdt.Rows[0]["TASK_ID"].ToString(), this.searchLookUpEdit1.Text.ToString(),
                                     GlobeSettings.LoginedUser.UserName, Gdt.Rows[0]["BILL_ID"].ToString() };
                dr.ItemArray = objs;
                Idt.Rows.Add(dr);

                DataGdtUp();
                this.txtBoxCade.Text = "";
            }

        }
        private void DataGdtUp()
        {
            int Hnum = Convert.ToInt32(Gdt.Rows[0]["H_PIECES"]);
            int Pnum = Convert.ToInt32(Gdt.Rows[0]["PIECES"]);
            Gdt.Rows[0]["H_PIECES"] = Hnum + 1;
            if (Pnum == Hnum + 1)
            {


                Gdt.Rows.Remove(Gdt.Rows[0]);
            }
            this.gridTasks.DataSource = Gdt;
            this.gridTasks.RefreshDataSource();

        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (this.searchLookUpEdit1.Text == "")
            {
                splashLabel1.Show("请先选择拣货人！", false);
                return;
            }
            if (Idt.Rows.Count == 0)
            {
                splashLabel1.Show("无关联数据不能保存！", false);
                return;
            }

            if (BoxNum < 6)
            {
                if (MsgBox.AskYes("此车并未装满是否保存？") == DialogResult.No)
                {
                    return;
                }
            }




            GetUserPICK.GronpingTaskAdd1(Idt);

            this.txtCarCode.Enabled = true;
            this.txtCarCode.Focus();

            this.txtCarCode.Text = "";
            this.txtBoxCade.Text = "";
            this.txtBoxCade.Enabled = false;

            BoxClear();


            BoxNum = 1;

            searchLookUpEdit1.Text = "";
            
        
            DataGetGronpingTask();

        }

        private void gridTasks_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }






    }
}
