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
    public partial class SafeFormcs : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Smart1InstallmentDataClassesDataContext db = new Smart1InstallmentDataClassesDataContext();
        public static int Flag = 5;
        public int Code { get; private set; }
        public bool IsEdit { get; private set; }
        public object Edit { get; private set; }

        public SafeFormcs()
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
                    
                    getdata.AccountFlagId = Convert.ToInt32(searchLookUpEdit1AccountFlagId.EditValue);
                    getdata.AccountChildId = Convert.ToInt32(searchLookUpEdit2AccountChildId.EditValue);
                    getdata.IsActive = Convert.ToBoolean(checkEdit1.Checked);
                    db.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    Clear();
                }
                else
                {
                    Account st = new Account();
                    st.AccountName = Convert.ToString(textEditAccountName.Text);
                   
                    st.IsActive = Convert.ToBoolean(checkEdit1.Checked);
                    st.AccountFlagId = Convert.ToInt32(searchLookUpEdit1AccountFlagId.EditValue);
                    st.AccountChildId = Convert.ToInt32(searchLookUpEdit2AccountChildId.EditValue);
                    db.Accounts.InsertOnSubmit(st);
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
            searchLookUpEdit1AccountFlagId.EditValue=null;
            searchLookUpEdit2AccountChildId.EditValue=null ;
            checkEdit1.Checked = true;
            Fill();
        }

        private void storForm1cs_Load(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                var getdata = db.Accounts.Where(a => a.AccountId == Code).FirstOrDefault();
                searchLookUpEdit1AccountFlagId.EditValue = getdata.AccountFlagId;
                searchLookUpEdit2AccountChildId.EditValue = getdata.AccountChildId;
                textEditAccountName.Text = getdata.AccountName;
                checkEdit1.Checked = Convert.ToBoolean(getdata.IsActive);


            }
            else
            {
                Fill();

            }
        }

        void Fill()
        {
            searchLookUpEdit1AccountFlagId.Properties.DataSource = db.AccountFlags.Where(a => a.AccountFlagId == 5 && a.IsActive == true).ToList();
            searchLookUpEdit1AccountFlagId.EditValue = 5;
            searchLookUpEdit2AccountChildId.Properties.DataSource = db.Accounts.Where(a => a.AccountFlagId == 1002 && a.IsActive == true).ToList();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertOrUpdate();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clear();
        }
    }
}