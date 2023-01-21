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
    public partial class ExpensesAndIncomeForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Smart1InstallmentDataClassesDataContext db = new Smart1InstallmentDataClassesDataContext();

        public int Code { get; private set; }
  
        public bool IsEdit { get; private set; }
        public ExpensesAndIncomeForm()
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
                    getdata.AccountName = Convert.ToString(AccountName.Text);
                    getdata.MovementTypeFlagId = Convert.ToInt32(MovementTypeFlag.EditValue);
                    getdata.IsActive = Convert.ToBoolean(checkEdit1.Checked);
                    db.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    Clear();
                }
                else
                {
                    Account ex = new Account();
                    ex.AccountName = Convert.ToString(AccountName.Text);
                    ex.MovementTypeFlagId = Convert.ToInt32(MovementTypeFlag.EditValue);
                   ex.IsActive = Convert.ToBoolean(checkEdit1.Checked);

                    db.Accounts.InsertOnSubmit(ex);
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


            AccountName.Text = null;
            MovementTypeFlag.EditValue = null;
            checkEdit1.Checked = true;
            MovementTypeFlag.Properties.DataSource = db.MovementTypes.ToList();

        }
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void ExpensesAndIncomeForm_Load(object sender, EventArgs e)
        {
            //searchLookUpEdit1.Properties.DataSource = db.AccountName.ToList();
            //repositoryItemLookUpBank.DataSource = db.Safes.Where(a => a.SafeFlagId == 1);
            MovementTypeFlag.Properties.DataSource = db.MovementTypes.ToList();
        }
    }
}