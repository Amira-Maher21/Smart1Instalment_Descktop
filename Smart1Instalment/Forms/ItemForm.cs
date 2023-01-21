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
    public partial class ItemForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Smart1InstallmentDataClassesDataContext db = new Smart1InstallmentDataClassesDataContext();
        public bool IsEdit { get; set; }
        public int Code { get; set; }
        public object Edit { get; set; }
        bool isserial;

        int Count = 0;
        public ItemForm()
        {
            InitializeComponent();
            StoreSearchLookUpEdit1.Enabled = false;
            InstallmentSellPricetextEdit3.Enabled = false;
        }

        void InsertOrUpdate()
        {
            if (Convert.ToBoolean(textEditIsSerial.Checked) == false && Convert.ToBoolean(IsNotSerialcheckEdit1.Checked) == false)
            {
                MessageBox.Show("من فضلك إختر هل الصنف يقبل سيريال أم لا  ؟");
                return;
            }
            if (!dxValidationProvider1.Validate())
            {
               
                return;
            }
            try
            {
                if (IsEdit)
                {
                    var getdata = db.Items.Where(a => a.ItemId == Code).FirstOrDefault();
                    getdata.ItemDesc = Convert.ToString(textEditItem.Text);
                    getdata.ItemNotes = Convert.ToString(textEdittemNotes.EditValue);
                    getdata.SoldQuantity = Convert.ToInt32(textEditQuantity.EditValue);
                    getdata.FirstPerioudBalance = Convert.ToInt32(textEditFirstPerioudBalance.EditValue);
                    getdata.IsSerial = Convert.ToBoolean(textEditIsSerial.Checked);
                    getdata.GroupDetailId = Convert.ToInt32(searchLookUpGroupDetail.EditValue);
                    getdata.ItemIsActive = Convert.ToBoolean(checkEdit.EditValue);
             
                    db.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    Clear();

                }
                else
                {

                    Item it = new Item();
                    it.ItemDesc = Convert.ToString(textEditItem.Text);
                    it.ItemNotes = Convert.ToString(textEdittemNotes.EditValue);
                    it.SoldQuantity = Convert.ToInt32(textEditQuantity.EditValue);
                    it.ItemBarCode = Convert.ToString(textEditBarCode.Text);
                    it.FirstPerioudBalance = Convert.ToInt32(textEditFirstPerioudBalance.Text);
                    it.IsSerial = Convert.ToBoolean(textEditIsSerial.Checked);
                    it.GroupDetailId = Convert.ToInt32(searchLookUpGroupDetail.EditValue);
                    it.ItemIsActive = Convert.ToBoolean(checkEdit.EditValue);
                    db.Items.InsertOnSubmit(it);
                    db.SubmitChanges();
                    //إدخال سيريالات الاصناف بعد حفظ الصنف
                    if (textEditIsSerial.Checked==true)
                    {
                        Code = it.ItemId;
                        Count = Convert.ToInt32(textEditFirstPerioudBalance.Text);
                        isserial = Convert.ToBoolean(textEditIsSerial.Checked);
                        ItemSerialForm ITSF = new ItemSerialForm();
                        ITSF.createrow = Count;
                        ITSF.itemcode = Code;
                        ITSF.isserial = isserial;
                        ITSF.isitem = true;
                        ITSF.ShowDialog();
                    }
                    else if(IsNotSerialcheckEdit1.Checked == true)
                    {
                        if (Convert.ToBoolean(textEditIsSerial.Checked) == false && Convert.ToBoolean(IsNotSerialcheckEdit1.Checked) == false)
                        {
                            MessageBox.Show("من فضلك إختر هل الصنف يقبل سيريال أم لا  ؟");
                            return;
                        }
                        if (!dxValidationProvider2.Validate())
                        {
                          
                            return;
                        }
                       //إدخال الاصناف التي لاتقبل السيريال ولها باركود فقط
                     try
                        {
                                ItemSerial ist = new ItemSerial();
                                ist.ItemId = it.ItemId;
                                ist.ItemDesc = it.ItemDesc;
                                ist.AccountId =Convert.ToInt32( StoreSearchLookUpEdit1.EditValue);
                                ist.ItemBarCode = Convert.ToString(textEditBarCode.Text);
                                ist.ItemSerial1 = "";
                                ist.IsSerial = false;
                                ist.ItemIsSold = false;
                                ist.ItemBuyPrice = Convert.ToDecimal(BuyPricetextEdit2.Text);
                                ist.ItemSellPriceInstallment = 0;
                                ist.ItemSellPriceCash = Convert.ToDecimal(CashSellPricetextEdit1.Text);
                                ist.DocTypeId = 10;
                                ist.DocNum = it.ItemId;
                                ist.QuantityIn = Convert.ToInt32(textEditFirstPerioudBalance.Text);
                                ist.QuantityOut = 0;
                                ist.CurrentBalance =  Convert.ToInt32(textEditFirstPerioudBalance.Text);
                                ist.CurrentDate = DateTime.Now.Date;
                                db.ItemSerials.InsertOnSubmit(ist);
                                db.SubmitChanges();
                        }
                            catch 
                            {

                                MessageBox.Show("لم يتم الحفظ ");
                            }
                        
                       

                    }
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

          textEditItem.Text=null;
          textEdittemNotes.Text=null;
          textEditQuantity.Text=null;
          textEditBarCode.Text=null;
          textEditFirstPerioudBalance.EditValue="";
          textEditIsSerial.Checked=false;
          searchLookUpGroupDetail.EditValue="";
          checkEdit.EditValue=null;
          StoreSearchLookUpEdit1.EditValue = null;
          IsNotSerialcheckEdit1.Checked = false;
            Fill();
        }

        void Fill()
        {
            searchLookUpGroupDetail.Properties.DataSource = db.GroupDetails.Where(a => a.IsActive == true).ToList();
            StoreSearchLookUpEdit1.Properties.DataSource = db.Accounts.Where(a => a.AccountFlagId == 4 && a.IsActive == true).ToList();
            textEditIsSerial.Checked = false;
            checkEdit.Checked = true;
            textEditFirstPerioudBalance.Text = "0";
            BuyPricetextEdit2.Text = "0";
            InstallmentSellPricetextEdit3.Text = "0";
            CashSellPricetextEdit1.Text = "0";
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void ItemForm_Load(object sender, EventArgs e)
        {
            int br;
            var barcode = db.Items.Select(a => a.ItemBarCode).Max();
            br = Convert.ToInt32(barcode);
            textEditBarCode.Text = Convert.ToInt32(br + 1).ToString();
            Fill();
            
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertOrUpdate();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clear();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            GroupDetailForm GDF = new GroupDetailForm();
            GDF.ShowDialog();
            searchLookUpGroupDetail.Properties.DataSource = db.GroupDetails.Where(a => a.IsActive == true).ToList();
        }

        private void textEditFirstPerioudBalance_EditValueChanged(object sender, EventArgs e)
        {
            if (textEditFirstPerioudBalance.Text=="" || textEditFirstPerioudBalance.Text==null)
            {
                StoreSearchLookUpEdit1.Enabled = false;
            }
           else if (Convert.ToInt32(textEditFirstPerioudBalance.EditValue) > 0)
            {
                StoreSearchLookUpEdit1.Enabled = true;
            }
            else if (Convert.ToInt32(textEditFirstPerioudBalance.EditValue) == 0)
            {
                StoreSearchLookUpEdit1.Enabled = false;
            }

        }
    }
}