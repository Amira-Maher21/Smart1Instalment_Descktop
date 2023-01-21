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
    public partial class GroupForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Smart1InstallmentDataClassesDataContext db = new Smart1InstallmentDataClassesDataContext();
        public bool IsEdit { get; set; }
        public int Code { get; set; }

        public GroupForm()
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
                    var getdata = db.Groups.Where(a => a.GroupId == Code).FirstOrDefault();
                    getdata.GroupDesc = Convert.ToString(textEditGroup.Text);
                    getdata.IsActive = Convert.ToBoolean(checkEdit.Checked);
                    db.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    Clear();
                }
                else
                {
                    Group gr = new Group();
                    gr.GroupDesc = Convert.ToString(textEditGroup.Text);
                    gr.IsActive = Convert.ToBoolean(checkEdit.Checked);
                    db.Groups.InsertOnSubmit(gr);
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

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertOrUpdate();
        }
        void Clear()
        {

            textEditGroup.Text = null;
            checkEdit.Checked = true;
           
        }
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clear();
        }

        private void GroupForm_Load(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                var Edit = db.Groups.Where(a => a.GroupId == Code).FirstOrDefault();

                textEditGroup.Text = Edit.GroupDesc;
                checkEdit.Checked = Convert.ToBoolean(Edit.IsActive);
                


            }
            else
            {
                Fill();
            }
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }
        void Fill()
        {
            checkEdit.Checked = true;
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            GroupDetailForm GDF = new GroupDetailForm();
            GDF.ShowDialog();
        }
    }
}