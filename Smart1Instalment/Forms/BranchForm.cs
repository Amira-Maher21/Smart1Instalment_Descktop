using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Smart1Instalment.DB;

namespace Smart1Instalment.Forms
{
    public partial class BranchForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Smart1InstallmentDataClassesDataContext db = new Smart1InstallmentDataClassesDataContext();
        public static int Flag = 1002;
        public int Code { get;  set; }
        public bool IsEdit { get;  set; }
       
        public BranchForm()
        {
            InitializeComponent();
        }
        void InsertOrUpdate()
        {
            if (!dxValidationProvider1.Validate())
            {
                MessageBox.Show("من فضلك البيانات المطلوبة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                if (IsEdit)
                {
                    var getdata = db.Accounts.Where(a => a.AccountId == Code).FirstOrDefault();
                    getdata.AccountName = Convert.ToString(textEditAccountName.Text);
                    getdata.AccountFlagId = 1002;
                    getdata.Address = Convert.ToString(textEditAddress.Text);
                    getdata.PhoneNum1 = Convert.ToString(textEditPhoneNum.Text);
                    getdata.IsActive = Convert.ToBoolean(checkEdit1.Checked);
                    db.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    Clear();
                }
                else
                {
                    Account ch = new Account();
                    ch.AccountName = Convert.ToString(textEditAccountName.Text);
                    ch.AccountFlagId = 1002;
                    ch.Address = Convert.ToString(textEditAddress.Text);
                    ch.PhoneNum1 = Convert.ToString(textEditPhoneNum.Text);
                    ch.IsActive = Convert.ToBoolean(checkEdit1.Checked);
                    db.Accounts.InsertOnSubmit(ch);
                    db.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    Clear();
                }
            }
            catch 
            {

                MessageBox.Show("لم يتم الحفظ ");
            }

        }


        void Clear()
        {

            textEditAccountName.Text = null;
            textEditAddress.Text = null;
            textEditPhoneNum .Text= null;
            checkEdit1.Checked = true;
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertOrUpdate();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clear();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }
    }
}