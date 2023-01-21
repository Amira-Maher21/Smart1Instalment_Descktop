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
    public partial class StoredForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Smart1InstallmentDataClassesDataContext db = new Smart1InstallmentDataClassesDataContext();
        public static int Flag = 1;
        public int Code { get; private set; }
        public bool IsEdit { get; private set; }

        public StoredForm()
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
                    getdata.AccountName = Convert.ToString(textEditName.Text);
                    getdata.AccountFlagId =4 ;
                    getdata.AccountChildId = Convert.ToInt32(searchLookUpEdit2BranchChildId.EditValue);
                    getdata.Address = Convert.ToString(textAddress.EditValue);
                    getdata.AccountFlagId = Convert.ToInt32(searchAccountFlag.EditValue);
                    getdata.IsActive = Convert.ToBoolean(checkEdit1.Checked);
                    db.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    Clear();
                }
                else
                {
                    Account st = new Account();
                    st.AccountName = Convert.ToString(textEditName.Text);
                    st.AccountFlagId = 4;
                    st.Address = Convert.ToString(textAddress.EditValue);
                    st.AccountChildId = Convert.ToInt32(searchLookUpEdit2BranchChildId.EditValue);
                    st.AccountFlagId = Convert.ToInt32(searchAccountFlag.EditValue);
                    st.IsActive = Convert.ToBoolean(checkEdit1.Checked);
                  
                    db.Accounts.InsertOnSubmit(st);
                    db.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    Clear();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("لم يتم الحفظ ");
            }




        }


        void Clear()
        {


            textEditName.Text = null;
            textAddress.EditValue = null;
            searchAccountFlag.EditValue = null;
            checkEdit1.Checked = true;
            searchLookUpEdit2BranchChildId.EditValue = null;
            Fill();
        }

        void Fill()
        {
            searchAccountFlag.Properties.DataSource = db.AccountFlags.Where(a => a.IsActive == true).ToList();
            searchAccountFlag.EditValue = 4;
            searchAccountFlag.Enabled = false;
            searchLookUpEdit2BranchChildId.Properties.DataSource = db.Accounts.Where(a => a.AccountFlagId == 1002 && a.IsActive == true).ToList();
            checkEdit1.Checked = true;
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clear();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertOrUpdate();
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void StoreForm_Load(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                
            }
            else
            {
                Fill();
            }
            

           
        }
    }
}