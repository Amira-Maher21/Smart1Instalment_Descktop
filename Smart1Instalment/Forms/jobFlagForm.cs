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
    public partial class jobFlagForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Smart1InstallmentDataClassesDataContext db = new Smart1InstallmentDataClassesDataContext();
        public jobFlagForm()
        {
            InitializeComponent();
        }

        public int Code { get; private set; }
        public bool IsEdit { get; private set; }

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
                    var getdata = db.JobFlags.Where(a => a.JobFlagId == Code).FirstOrDefault();
                     getdata.IsActive = Convert.ToBoolean(checkEdit.Checked);
                    db.SubmitChanges();
                    MessageBox.Show("تم الحفظ");

                }
                else
                {
                    JobFlag jf = new JobFlag();
                    jf.JobFlagDesc = TxtEditJobFlag.Text;
                    jf.IsActive = Convert.ToBoolean(checkEdit.Checked);
                    db.JobFlags.InsertOnSubmit(jf);
                    db.SubmitChanges();
                    MessageBox.Show("تم الحفظ");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("لم يتم الحفظ ");
            }
        }
        void Clear()
        {
            TxtEditJobFlag.Text = "";
            checkEdit.Checked = true;
        }
        private void jobFlagForm_Load(object sender, EventArgs e)
        {
            checkEdit.Checked = true;

            if (IsEdit)
            {
                var getdata = db.JobFlags.Where(a => a.JobFlagId == Code).FirstOrDefault();
                TxtEditJobFlag.Text = getdata.JobFlagDesc;
                checkEdit.Checked = Convert.ToBoolean(getdata.IsActive);
            }
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {

            Close();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clear();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertOrUpdate();
        }
    }
}