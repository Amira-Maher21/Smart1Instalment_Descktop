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
    public partial class EmployeeForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Smart1InstallmentDataClassesDataContext db = new Smart1InstallmentDataClassesDataContext();
        public static int Flag = 1;

        public bool IsEdit { get;  set; }
        public int Code { get; set; }
        public object Edit { get;  set; }

        public EmployeeForm()
        {

            InitializeComponent();
            
        }
        void InsertOrUpdate()
        {
          if (!dxValidationProvider1.Validate())
                {
                    return;
                }
            try
            {
                if (IsEdit)
                {
                    var getdata = db.Accounts.Where(a => a.AccountId == Code).FirstOrDefault();
                    getdata.AccountName = Convert.ToString(textEditAccountName.Text);
                    getdata.AccountFlagId = 1;
                    getdata.PhoneNum1 = Convert.ToString(textEditPhoneNum.EditValue);
                    getdata.PhoneNum2 = Convert.ToString(textEditPhoneNum2.EditValue);
                    getdata.JobFlagId = Convert.ToInt32(searchLookUpEditJobFlag.EditValue);
                    getdata.RegistrationDate = Convert.ToDateTime(dateEditRegistrationDate.EditValue);
                    getdata.Address = Convert.ToString(textEditAddress.EditValue);
                    getdata.IsActive = Convert.ToBoolean(checkEdit.EditValue);
                    getdata.UserId = Convert.ToInt32(searchLookUpUser.EditValue);
                    getdata.IdentityCardNumber = Convert.ToString(textEditIdentityCardNumber.EditValue);
                    db.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    Clear();

                }
                else
                {
                    Account em = new Account();
                    em.AccountName = Convert.ToString(textEditAccountName.Text);
                    em.PhoneNum1 = Convert.ToString(textEditPhoneNum.EditValue);
                    em.AccountFlagId=  1;
                    em.PhoneNum2 = Convert.ToString(textEditPhoneNum2.EditValue);
                    em.JobFlagId = Convert.ToInt32(searchLookUpEditJobFlag.EditValue);
                    em.RegistrationDate = Convert.ToDateTime(dateEditRegistrationDate.EditValue);
                    em.Address = Convert.ToString(textEditAddress.EditValue);
                    em.IsActive = Convert.ToBoolean(checkEdit.EditValue);
                    em.UserId = Convert.ToInt32(searchLookUpUser.EditValue);
                    em.IdentityCardNumber = Convert.ToString(textEditIdentityCardNumber.EditValue);
                    db.Accounts.InsertOnSubmit(em);
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

       
            textEditAccountName.Text= null;
            textEditPhoneNum.Text= "";
            textEditPhoneNum2.Text= "";
            searchLookUpEditJobFlag.EditValue= null;
            dateEditRegistrationDate.EditValue = DateTime.Now;
            textEditAddress.Text= "";
            searchLookUpUser.EditValue = null;
            textEditIdentityCardNumber.Text = "";
            Fill();
        }

        void Fill()
        {
            searchLookUpEditJobFlag.Properties.DataSource = db.JobFlags.Where(a => a.IsActive == true).ToList();
            searchLookUpUser.Properties.DataSource = db.Users.Where(a => a.IsActive == true).ToList();

            checkEdit.Checked = true;
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clear();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertOrUpdate();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            Fill();

            if (IsEdit)
            {
                var IsEdit = db.Accounts.Where(a => a.AccountId == Code).FirstOrDefault();
                //textEditAccountName.Text = IsEdit.AccountName;
                //textEditPhoneNum.EditValue = IsEdit.PhoneNum1;
         


            
            }
        }
    }



        }
