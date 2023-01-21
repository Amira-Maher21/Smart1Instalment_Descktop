using DevExpress.XtraEditors;
using Smart1Instalment.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Smart1Instalment
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public enum FormMode
        {
            CustomersForm, EmployeeForm, ExpensesAndIncomeForm, GuarahtorForm, jobFlagForm, jobForm1, StoredForm, storForm1cs,
        }
        public int AccFlag;
        public Form1()
        {
            InitializeComponent();
            //تغيير خط البرنامج
            WindowsFormsSettings.DefaultFont = new Font("Times New Roman", 10, FontStyle.Bold);
        }

        private void navBarItem1_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AccFlag = 0;
            AccFlag = EmployeeForm.Flag;
            EmployeeForm emp = new EmployeeForm();
            emp.ShowDialog();

        }

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

           
            JobForm1 job = new JobForm1();
            job.ShowDialog();
        }


        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AccFlag = 2;
            CustomersForm.Flag= AccFlag  ;
            CustomersForm Acc = new CustomersForm();
           Acc.ShowDialog();
        }

        private void navBarControl1_Click(object sender, EventArgs e)
        {

        }

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            AccFlag = 3;
            CustomersForm.Flag = AccFlag;
            CustomersForm Acc = new CustomersForm();
            Acc.ShowDialog();
        }

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //AccFlag = 0;
            //AccFlag = StoredForm.Flag;
            StoredForm jo = new StoredForm();
            jo.ShowDialog();
        }

        private void navBarItem24_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AccFlag = 3;
            GuarahtorForm.Flag = AccFlag;
            GuarahtorForm gu = new GuarahtorForm();
            gu.ShowDialog();
        }

        private void navBarItem25_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
        }

        private void navBarItem8_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            BranchForm brf = new BranchForm();
            brf.ShowDialog();
        }

        private void navBarItem9_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SafeFormcs sf = new SafeFormcs();
            sf.ShowDialog();
        }

        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ItemForm Itf = new ItemForm();
            Itf.ShowDialog();
        }

        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ExpensesAndIncomeForm EIF = new ExpensesAndIncomeForm();
            EIF.ShowDialog();
        }

        private void navBarItem25_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            GroupForm Gf = new GroupForm();
            Gf.ShowDialog();
        }

        private void navBarItem10_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowInvForm INvF = new ShowInvForm();
            INvF.ShowDialog();
        }
    }
    
}
