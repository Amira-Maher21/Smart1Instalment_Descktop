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
    public partial class JobForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Smart1InstallmentDataClassesDataContext db = new Smart1InstallmentDataClassesDataContext();

        public bool IsEdit { get; private set; }
        public int Code { get; private set; }

        public JobForm1()
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
                    var getdata = db.Jobs.Where(a => a.JobId == Code).FirstOrDefault();
                    getdata.JobDesc = Convert.ToString(textEditAccountName.Text);
                    getdata.JobFlagId = Convert.ToInt32(searchLookUpJobFlag.EditValue);
                    getdata.IsActive = Convert.ToBoolean(checkEdit1.Checked);
                    db.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    Clear();

                }
                else
                {
                    Job st = new Job();
                    st.JobDesc = Convert.ToString(textEditAccountName.Text);
                    st.JobFlagId = Convert.ToInt32(searchLookUpJobFlag.EditValue);
                    st.IsActive = Convert.ToBoolean(checkEdit1.Checked);

                    db.Jobs.InsertOnSubmit(st);
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
            textEditAccountName.Text = null;
            searchLookUpJobFlag.EditValue = null;
            checkEdit1.Checked = true;
        }

        void Fill()
        {
            searchLookUpJobFlag.Properties.DataSource = db.JobFlags.Where(a => a.IsActive == true).ToList();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertOrUpdate();
        }

        private void jobForm1_Load(object sender, EventArgs e)
        {
            Fill();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clear();
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            jobFlagForm jf = new jobFlagForm();
            jf.ShowDialog();
            searchLookUpJobFlag.Properties.DataSource = db.JobFlags.Where(a => a.IsActive == true).ToList();
        }
    }
}