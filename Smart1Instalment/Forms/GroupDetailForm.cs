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
    public partial class GroupDetailForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        Smart1InstallmentDataClassesDataContext db = new Smart1InstallmentDataClassesDataContext();
        public bool IsEdit { get; set; }
        public int Code { get; set; }
        public object Edit { get; set; }
        public GroupDetailForm()
        {
            InitializeComponent();
        }


        void InsertOrUpdate()
        {
            if (!dxValidationProvider11.Validate())
            {
                return;
            }
            try
            {
                if (IsEdit)
                {
                    var getdata = db.GroupDetails.Where(a => a.GroupDetailId == Code).FirstOrDefault();
                    getdata.GroupDetailDesc= Convert.ToString(textEditGroupDetail.Text);
                    getdata.GroupId = Convert.ToInt32(SearchEditGroup.EditValue);
                    getdata.IsActive = Convert.ToBoolean(checkEditIsActive.Checked);


                    db.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    Clear();

                }
                else
                {
                    GroupDetail gr = new GroupDetail();
                    gr.GroupDetailDesc = Convert.ToString(textEditGroupDetail.Text);
                    gr.GroupId = Convert.ToInt32(SearchEditGroup.EditValue);
                    gr.IsActive= Convert.ToBoolean(checkEditIsActive.Checked);

                    db.GroupDetails.InsertOnSubmit(gr);
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

            SearchEditGroup.EditValue = null;
            checkEditIsActive.Checked = false;
            textEditGroupDetail.Text = "";

               Fill();
        }

        void Fill()
        {
            SearchEditGroup.Properties.DataSource = db.Groups.Where(a => a.IsActive == true).ToList();
            checkEditIsActive.Checked = true;
        }
     
        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            GroupForm GF = new GroupForm();
            GF.ShowDialog();
            SearchEditGroup.Properties.DataSource = db.Groups.Where(a => a.IsActive == true).ToList();
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

        private void GroupDetailForm_Load(object sender, EventArgs e)
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