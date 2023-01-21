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
using System.Globalization;

namespace Smart1Instalment.Forms
{
    public partial class InvoiceForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public int docktype;
        Timer t = new Timer();
        Smart1InstallmentDataClassesDataContext db = new Smart1InstallmentDataClassesDataContext();

        public int Code { get; set; }
        public bool IsEdit { get; set; }
        public bool IsPressed = false;
        public int doctype;
        public int storeid;
        public string itembarcode;
        public string itemserial;
        public decimal InstallMentWin;

        public DataTable InvoiceDetailTable;
        public InvoiceForm()
        {
            InitializeComponent();
            SubmittedValuetextEdit.Enabled = false;
            dateEdit1.Enabled = false;
            NumberOfInstallmenttextEdit.Enabled = false;
            InstallMentValuetextEdit1.Enabled = false;
            RashioValuetextEdit1.Enabled = false;
            RashioPerstextEdit2.Enabled = false;

            CreateInvoiceGrid();
        }


        void Fill()
        {
              searchLookUpEditDocType.Properties.DataSource = db.DocTypes.ToList();
            searchLookUpEditDocType.EditValue = docktype;
            InvoicedateEdit.EditValue = DateTime.Now;
            EmployeesearchLookUpEdit1.Properties.DataSource = db.Accounts.Where(a => a.AccountFlagId == 1 && a.IsActive == true).ToList();
            BranchsearchLookUpEdit1.Properties.DataSource = db.Accounts.Where(a => a.AccountFlagId == 1002 && a.IsActive == true).ToList(); 
            SubmittedValuetextEdit.Text = "0";
            NumberOfInstallmenttextEdit.Text = "0";
            MainOverAllDiscounttextEdit1.Text = "0";
            TotalItemDiscounttextEdit.Text = "0";
            TotalValuetextEdit.Text = "0";
            TotalNetValeOverAlltextEdit.Text = "0";
            TotalTaxAddingtextEdit1.Text = "0";
            TotalTaxAddingPercentagetextEdit1.Text = "0";
            TotalTaxDisstextEdit1.Text = "0";
            TotalTaxDisstextEdit1.Text = "0";
            TotalTaxDissPercentagetextEdit1.Text = "0";
            InstallMentValuetextEdit1.Text = "0";
            RashioValuetextEdit1.Text = "0";
            RashioPerstextEdit2.Text = "0";
            BarcodetextEdit1.Focus();
            CreateInvoiceGrid();

        }
        void Clear()
        {

            Fill();
           
        }
        public int branchid;
        private void BranchsearchLookUpEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }

        //عمل ميثود لاعطاء داتا سورس للجريد كنترول
        void CreateInvoiceGrid()
        {
            InvoiceDetailTable = new DataTable();
            InvoiceDetailTable.Columns.Clear();
            InvoiceDetailTable.Columns.Add("ItemId", typeof(int));
            InvoiceDetailTable.Columns.Add("ItemSellPrice", typeof(decimal));
            InvoiceDetailTable.Columns.Add("ItemBuyPrice", typeof(decimal));
            InvoiceDetailTable.Columns.Add("Quantity", typeof(decimal));
            InvoiceDetailTable.Columns.Add("ItemSerial", typeof(string));
            InvoiceDetailTable.Columns.Add("ItemDiscount", typeof(decimal));

            if (Convert.ToInt32(searchLookUpEditDocType.EditValue) == 1 || Convert.ToInt32(searchLookUpEditDocType.EditValue) == 3 || Convert.ToInt32(searchLookUpEditDocType.EditValue) == 5 || Convert.ToInt32(searchLookUpEditDocType.EditValue) == 7)
            {
                InvoiceDetailTable.Columns.Add("ItemMainQuantity", typeof(decimal)).Expression = ("(ItemSellPrice*Quantity)");
                InvoiceDetailTable.Columns.Add("TotalPrice", typeof(decimal)).Expression = ("(ItemSellPrice*Quantity)-(ItemDiscount)");

            }
            else
            {
                InvoiceDetailTable.Columns.Add("ItemMainQuantity", typeof(decimal)).Expression = ("(ItemBuyPrice*Quantity)");
                InvoiceDetailTable.Columns.Add("TotalPrice", typeof(decimal)).Expression = ("(ItemBuyPrice*Quantity)-(ItemDiscount)");

            }
            InvoiceDetailTable.Columns.Add("InvoiceNotes", typeof(string));
            gridControl1.DataSource = InvoiceDetailTable;

        }

        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            Fill();


            //timer interval
            t.Interval = 1000;  //in milliseconds

            t.Tick += new EventHandler(this.t_Tick);

            //start timer when form loads
            t.Start();
        }

        private void searchLookUpEditDocType_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            doctype = Convert.ToInt32(e.NewValue);
            if (doctype == 1 || doctype == 3 || doctype == 5 || doctype == 7)
            {

                SuppCusSearchLookUpEdit1.Properties.DataSource = db.Accounts.Where(a => a.AccountFlagId == 2 && a.IsActive == true).ToList();
            }
            else if (doctype == 2 || doctype == 4 || doctype == 6 || doctype == 8)
            {
                SuppCusSearchLookUpEdit1.Properties.DataSource = db.Accounts.Where(a => a.AccountFlagId == 3 && a.IsActive == true).ToList();
            }
            else if (doctype == 13)
            {
                SuppCusSearchLookUpEdit1.Properties.DataSource = db.Accounts.Where(a => a.AccountFlagId == 3 && a.AccountFlagId == 2 && a.IsActive == true).ToList();
            }


        }

        private void BranchsearchLookUpEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            branchid = Convert.ToInt32(e.NewValue);
            SafeSearchLookUpEdit1.Properties.DataSource = db.Accounts.Where(a => a.AccountFlagId == 5 && a.IsActive == true && a.AccountChildId == branchid).ToList();
            StoreSearchLookUpEdit1.Properties.DataSource = db.Accounts.Where(a => a.AccountFlagId == 4 && a.IsActive == true && a.AccountChildId == branchid).ToList();
        }

        private void StoreSearchLookUpEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            storeid = Convert.ToInt32(e.NewValue);

            repositoryItemSearchLookUpEdit1.ValueMember = "ItemId";
            repositoryItemSearchLookUpEdit1.DisplayMember = "ItemDesc";
            repositoryItemSearchLookUpEdit1.DataSource = db.ItemSerials.Where(a => a.ItemIsSold == false && a.AccountId == storeid).ToList();
        }
        //مجموعة متغيرات لإدخال الاصناف فى الداتا تيبل لملأ الجريد فيو
        int id;
        decimal buyprice;
        decimal sellprice;
        decimal quantity;
        decimal diss;
        decimal totquantity;
        decimal Sprice;
        decimal? totvalue;
        string Sserial;

        //إدخال صنف بالباركود
        private void AddItemByBarCode(string barcode, int storeid)
        {
            var getitem = db.ItemSerials.Where(a => a.ItemBarCode == barcode && a.AccountId == storeid).FirstOrDefault();
            if (getitem != null)
            {
                id = Convert.ToInt32(getitem.ItemId);
                buyprice = Convert.ToDecimal(getitem.ItemBuyPrice);
                if (doctype == 1 || doctype == 5 || doctype == 3 || doctype == 7)
                {
                    sellprice = Convert.ToDecimal(getitem.ItemSellPriceCash);
                }

                else
                {
                    sellprice = 0;
                }
                if (getitem.CurrentBalance > 0)
                {
                    quantity = 1;
                }
                else
                {
                    MessageBox.Show("لايوجد كمية متاحة من هذا الصنف");
                    quantity = 0;
                }
                Sserial = getitem.ItemSerial1;
                diss = 0;
                totquantity = 1;
                totvalue = quantity * sellprice;
                decimal totbuy = quantity * buyprice;
                if (doctype == 1 || doctype == 5 || doctype == 3 || doctype == 7)
                {
                    gridControl1.DataSource = InvoiceDetailTable.Rows.Add(id, buyprice, sellprice, quantity, "", diss, totquantity, totvalue, "");

                    gridView6.FocusedRowHandle = -1;
                    CalcTotalAfterChange();
                    BarcodetextEdit1.Text = "";
                    BarcodetextEdit1.Focus();
                }
                else
                {
                    gridControl1.DataSource = InvoiceDetailTable.Rows.Add(id, buyprice, sellprice, quantity, "", diss, totquantity, totbuy, "");
                    gridView6.FocusedRowHandle = -1;
                    CalcTotalAfterChange();
                    BarcodetextEdit1.Text = "";
                    BarcodetextEdit1.Focus();
                }

            }
            else
            {
                MessageBox.Show("باركود خاطئ من فضلك أدخل الباركود الصحيح");
                BarcodetextEdit1.Text = "";
                BarcodetextEdit1.Focus();
            }

        }

        // إدخال صنف بالسيريال
        private void AddItemBySerial(string serial, int storeid)
        {
            var getitem = db.ItemSerials.Where(a => a.ItemSerial1 == serial && a.AccountId == storeid).FirstOrDefault();
            if (getitem != null)
            {
                id = Convert.ToInt32(getitem.ItemId);
                buyprice = Convert.ToDecimal(getitem.ItemBuyPrice);
                if (doctype == 1 || doctype == 5|| doctype == 3 || doctype == 7)
                {
                    sellprice = Convert.ToDecimal(getitem.ItemSellPriceCash);
                    Sserial = Convert.ToString(getitem.ItemSerial1);
                }

               
                else
                {
                    sellprice = 0;
                }
                if (getitem.CurrentBalance > 0)
                {
                    quantity = 1;
                }
                else
                {
                    MessageBox.Show("لايوجد كمية متاحة من هذا الصنف");
                    quantity = 1;
                }

                diss = 0;
                totquantity = 1;
                totvalue = quantity * sellprice;
                decimal totbuy= quantity * buyprice; 
                if (doctype == 1 || doctype == 5 || doctype == 3 || doctype == 7)
                {
                    gridControl1.DataSource = InvoiceDetailTable.Rows.Add(id, buyprice, sellprice, quantity, Sserial, diss, totquantity, totvalue, "");

                    gridView6.FocusedRowHandle = -1;
                    CalcTotalAfterChange();
                    SerialtextBox1.Text = "";
                    SerialtextBox1.Focus();
                }
                else
                {
                    gridControl1.DataSource = InvoiceDetailTable.Rows.Add(id, buyprice, sellprice, quantity, "", diss, totquantity, totbuy, "");
                    gridView6.FocusedRowHandle = -1;
                    CalcTotalAfterChange();
                    SerialtextBox1.Text = "";
                    SerialtextBox1.Focus();
                }

            }
            else
            {
                MessageBox.Show("سيريال خاطئ من فضلك أدخل السيريال الصحيح");
                SerialtextBox1.Text = "";
                SerialtextBox1.Focus();
            }

        }
        // أخذ الباركود من الشاشة
        private void BarcodetextEdit1_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter)
            {
                itembarcode = Convert.ToString(BarcodetextEdit1.Text);
                storeid = Convert.ToInt32(StoreSearchLookUpEdit1.EditValue);
                AddItemByBarCode(itembarcode, storeid);
            }
        }

        //أخذ السيريال من الشاشة
        private void SerialtextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                itemserial = Convert.ToString(SerialtextBox1.Text);
                storeid = Convert.ToInt32(StoreSearchLookUpEdit1.EditValue);
                AddItemBySerial(itemserial, storeid);
            }
        }
        //عند تغيير قيمة خلية فى الجريد فيو
        private void gridView6_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
           
            if (e.Column.FieldName == "ItemId")
            {
                int id = Convert.ToInt32(gridView6.GetRowCellValue(e.RowHandle, "ItemId"));
                var getitem = db.ItemSerials.Where(a => a.ItemId == id && a.AccountId == storeid).FirstOrDefault();
                buyprice = Convert.ToDecimal(getitem.ItemBuyPrice);
                if (doctype == 1 || doctype == 5 || doctype == 3 || doctype == 7)
                {
                    sellprice = Convert.ToDecimal(getitem.ItemSellPriceCash);
                }

                else
                {
                    sellprice = 0;
                }
                if (getitem.CurrentBalance > 0)
                {
                    quantity = 1;
                }
                else
                {
                    MessageBox.Show("لايوجد كمية متاحة من هذا الصنف");
                    quantity = 0;
                }
                Sserial = getitem.ItemSerial1;
                diss = 0;
                totquantity = 1;
                totvalue = quantity * sellprice;
               
                gridView6.SetRowCellValue(e.RowHandle, "ItemSellPrice", sellprice);
                gridView6.SetRowCellValue(e.RowHandle, "ItemBuyPrice", buyprice);
                gridView6.SetRowCellValue(e.RowHandle, "Quantity", quantity);
                

                if (doctype == 1 || doctype == 3 || doctype == 5 || doctype == 7)
                {
                    gridView6.SetRowCellValue(e.RowHandle, "ItemSerial", Sserial);
                    gridView6.SetRowCellValue(e.RowHandle, "ItemDiscount", diss);
                    gridView6.SetRowCellValue(e.RowHandle, "ItemMainQuantity", Convert.ToDecimal(sellprice) * 1);
                    gridView6.SetRowCellValue(e.RowHandle, "TotalPrice", Convert.ToDecimal((sellprice * quantity) - (diss)) * 1);
                    gridView6.SetRowCellValue(e.RowHandle, "InvoiceNotes", "");
                    gridView6.FocusedRowHandle = -1;
                    CalcTotalAfterChange();
                    BarcodetextEdit1.Text = "";
                    BarcodetextEdit1.Focus();
                }
                else
                {
                    gridView6.SetRowCellValue(e.RowHandle, "ItemSerial", "");
                    gridView6.SetRowCellValue(e.RowHandle, "ItemDiscount", diss);
                    gridView6.SetRowCellValue(e.RowHandle, "ItemMainQuantity", Convert.ToDecimal(buyprice) * 1);
                    gridView6.SetRowCellValue(e.RowHandle, "TotalPrice", Convert.ToDecimal((buyprice * quantity) - (diss)) * 1);
                    gridView6.SetRowCellValue(e.RowHandle, "InvoiceNotes", "");
                    gridView6.FocusedRowHandle = -1;
                    CalcTotalAfterChange();
                    BarcodetextEdit1.Text = "";
                    BarcodetextEdit1.Focus();
                }
                

            }
            if (e.Column.FieldName == "Quantity")
            {
                if (Convert.ToString(gridView6.GetRowCellValue(e.RowHandle, "ItemDiscount")) != null && Convert.ToString(gridView6.GetRowCellValue(e.RowHandle, "Quantity")) != null)
                {
                   
                    CalcTotalAfterChange();
                }

            }
            if (e.Column.FieldName == "ItemDiscount")
            {
                if (Convert.ToString(gridView6.GetRowCellValue(e.RowHandle, "ItemDiscount")) != null)
                {
                   
                    CalcTotalAfterChange();
                }

            }

        }
        //حساب الاجماليات بعد اي تغيير
        void CalcTotalAfterChange()
        {
            decimal Nqty = 0;
            decimal NPrice = 0;
            decimal Ntotal = 0;
            decimal Ndiss = 0;
            decimal DissAll = 0;
            decimal itemdiss = 0;
            decimal Allitemdiss = 0;


            if (doctype == 1 || doctype == 3 || doctype == 5 || doctype == 7)
            {
                for (int i = 0; i < gridView6.RowCount; i++)
                {
                    quantity = Convert.ToDecimal(gridView6.GetRowCellValue(i, "Quantity"));
                    diss = Convert.ToDecimal(gridView6.GetRowCellValue(i, "ItemDiscount"));
                    Nqty = quantity;
                    NPrice = Convert.ToDecimal(gridView6.GetRowCellValue(i, "ItemSellPrice"));
                    Ndiss = diss;
                    Ntotal = (Ntotal + (Nqty * NPrice));
                    itemdiss = Convert.ToDecimal(gridView6.GetRowCellValue(i, "ItemDiscount"));
                    Allitemdiss = Allitemdiss + itemdiss;
                }
                TotalValuetextEdit.EditValue = Ntotal;
                DissAll = Convert.ToDecimal(MainOverAllDiscounttextEdit1.Text);
                TotalItemDiscounttextEdit.EditValue = Allitemdiss;
                TotalNetValeOverAlltextEdit.EditValue = Ntotal - (DissAll + Allitemdiss);
            }
            else if (doctype == 2 || doctype == 4 || doctype == 6 || doctype == 8)
            {
                for (int i = 0; i < gridView6.RowCount; i++)
                {
                    quantity = Convert.ToDecimal(gridView6.GetRowCellValue(i, "Quantity"));
                    diss = Convert.ToDecimal(gridView6.GetRowCellValue(i, "ItemDiscount"));
                    NPrice = Convert.ToDecimal(gridView6.GetRowCellValue(i, "ItemBuyPrice"));
                    Nqty = quantity;
                    Ndiss = diss;
                    Ntotal = Ntotal + (Nqty * NPrice) - Ndiss;
                }
                TotalValuetextEdit.EditValue = Ntotal;
                DissAll = Convert.ToDecimal(MainOverAllDiscounttextEdit1.Text);
                TotalItemDiscounttextEdit.EditValue = Allitemdiss;
                TotalNetValeOverAlltextEdit.EditValue = Ntotal - (DissAll + Allitemdiss);
            }
            else if (doctype == 13)
            {

            }
        }
        //قبل مغادرة السطر الحالى
        private void gridView6_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            //gridView6.FocusedRowHandle = -1;
            //CalcTotalAfterChange();

        }
        //عند إضافة سطر جديد
        private void gridView6_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {


            //gridView6.FocusedRowHandle = -1;
            //CalcTotalAfterChange();
        }

        private void MainOverAllDiscounttextEdit1_EditValueChanged(object sender, EventArgs e)
        {


        }

        private void MainOverAllDiscounttextEdit1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter && !IsPressed)
            {
                IsPressed = true;
                decimal dissall = 0;
                decimal tot = 0;
                decimal newtot = 0;

                dissall = Convert.ToDecimal(MainOverAllDiscounttextEdit1.EditValue);

                tot = Convert.ToDecimal(TotalNetValeOverAlltextEdit.EditValue);
                if (dissall > 0)
                {

                    newtot = tot - dissall;

                    TotalNetValeOverAlltextEdit.EditValue = newtot;

                }
                else if (dissall <= 0)
                {
                    CalcTotalAfterChange();


                }



            }


        }

        private void SubmittedValuetextEdit_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter && !IsPressed)
            {
                IsPressed = true;
                CalcSumbmittedFromTotal();
            }
        }


        //عند إختيار أقساط
        private void InsallmentcheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (InsallmentcheckEdit.Checked == true)
            {
                SubmittedValuetextEdit.Enabled = true;
                dateEdit1.Enabled = true;
                InstallMentValuetextEdit1.Enabled = true;
                RashioPerstextEdit2.Enabled = true;
            }
            else if (InsallmentcheckEdit.Checked == false)
            {
                SubmittedValuetextEdit.Enabled = false;
                dateEdit1.Enabled = false;
                NumberOfInstallmenttextEdit.Enabled = false;
                InstallMentValuetextEdit1.Enabled = false;
                SubmittedValuetextEdit.EditValue = 0;
                dateEdit1.EditValue = null;
                RashioPerstextEdit2.Enabled = false;
                RashioPerstextEdit2.EditValue = 0;
                NumberOfInstallmenttextEdit.EditValue = 0;
                InstallMentValuetextEdit1.EditValue = 0;
                CalcTotalAfterChange();
            }
        }

        //تحديث الساعة
        private void t_Tick(object sender, EventArgs e)
        {
            //get current time
            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            int ss = DateTime.Now.Second;

            //time
            string time = "";

            //padding leading zero
            if (hh < 10)
            {
                time += "0" + hh;
            }
            else
            {
                time += hh;
            }
            time += ":";

            if (mm < 10)
            {
                time += "0" + mm;
            }
            else
            {
                time += mm;
            }
            time += ":";

            if (ss < 10)
            {
                time += "0" + ss;
            }
            else
            {
                time += ss;
            }

            //update Watch
            DayOfWeek wk = DateTime.Today.DayOfWeek;
            textEdit1.Text = wk+" "+ time;
           
        }

        private void NumberOfInstallmenttextEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter )
            {
                
                CalcInstallMent();
            }
        }
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            InseartOrUpdate();
        }

        //حفظ الفاتورة
        void InseartOrUpdate()
        {
            decimal createinstallment = Convert.ToDecimal(NumberOfInstallmenttextEdit.EditValue);
            DateTime Finstalldate = Convert.ToDateTime(dateEdit1.EditValue);
            try
            {
                if (!dxValidationProvider1.Validate())
                {
                    MessageBox.Show("برجاء ملأ البيانات اللازمة");
                    return;
                }
                else
                {
                    if (IsEdit)
                    {

                    }
                    else
                    {
                        //حفظ ماستر الفاتورة
                        Invoice inv = new Invoice();
                        inv.BranchId = Convert.ToInt32(BranchsearchLookUpEdit1.EditValue);
                        if (dateEdit1.EditValue == null)
                        {
                            inv.DateFirstInstallment = null;
                        }
                        else
                        {
                            inv.DateFirstInstallment = Convert.ToDateTime(dateEdit1.EditValue);
                        }
                        inv.DocTypeId = Convert.ToInt32(searchLookUpEditDocType.EditValue);
                        inv.InstallMentValue = Convert.ToDecimal(InstallMentValuetextEdit1.EditValue);
                        inv.InvoiceDate = Convert.ToDateTime(InvoicedateEdit.EditValue);
                        inv.IsInstallment = Convert.ToBoolean(InsallmentcheckEdit.Checked);
                        inv.IsClosed = Convert.ToBoolean(IsclosedcheckEdit.Checked);
                        inv.MainOverAllDiscount = Convert.ToDecimal(MainOverAllDiscounttextEdit1.EditValue);
                        inv.NumberOfInstallment = Convert.ToDecimal(NumberOfInstallmenttextEdit.EditValue);
                        inv.PendingInvoice = Convert.ToBoolean(PendingcheckEdit.Checked);
                        inv.PerviousAccount = Convert.ToDecimal(PerviousAccounttextEdit1.EditValue);
                        inv.SafeAccountId = Convert.ToInt32(SafeSearchLookUpEdit1.EditValue);
                        inv.StoreId = Convert.ToInt32(StoreSearchLookUpEdit1.EditValue);
                        inv.SubmittedValue = Convert.ToDecimal(SubmittedValuetextEdit.EditValue);
                        inv.SuppOrCusAccountId = Convert.ToInt32(SuppCusSearchLookUpEdit1.EditValue);
                        inv.TotalItemDiscount = Convert.ToDecimal(TotalItemDiscounttextEdit.EditValue);
                        inv.TotalNetValeOverAll = Convert.ToDecimal(TotalNetValeOverAlltextEdit.EditValue);
                        inv.TotalTaxAdding = Convert.ToDecimal(TotalTaxAddingtextEdit1.EditValue);
                        inv.TotalTaxAddingPercentage = Convert.ToDecimal(TotalTaxAddingPercentagetextEdit1.EditValue);
                        inv.TotalTaxDiss = Convert.ToDecimal(TotalTaxDisstextEdit1.EditValue);
                        inv.TotalTaxDissPercentage = Convert.ToDecimal(TotalTaxDissPercentagetextEdit1.EditValue);
                        inv.TotalValue = Convert.ToDecimal(TotalValuetextEdit.EditValue);
                        inv.UserId = Convert.ToInt32(EmployeesearchLookUpEdit1.EditValue);
                        db.Invoices.InsertOnSubmit(inv);
                        db.SubmitChanges();
                        //عمل إدخال للأقساط فى جدول الأقساط
                        if (createinstallment > 0)
                        {
                            DateTime date = Convert.ToDateTime(dateEdit1.Text).Date;
                            InstallmentWin();
                            for (int i = 0; i < createinstallment; i++)
                            {
                                InstallMent ins = new InstallMent();
                                ins.AccountId = inv.SuppOrCusAccountId;
                                ins.AccountSafeId = 0;
                                ins.DiscountNotice = 0;
                                ins.InstallMentDate = date.AddMonths(i);
                                ins.InstallMentIsClosed = false;
                                ins.InstallMentIsPayed = false;
                                ins.InstallMentValue = inv.InstallMentValue;
                                ins.InstallMentWin = InstallMentWin;
                                ins.InvoiceId = inv.InvoiceId;
                                ins.IsDiscountNotice = false;
                                db.InstallMents.InsertOnSubmit(ins);
                                db.SubmitChanges();
                            }
                        }

                        //حفظ ديتيل الفاتورة

                        int invid;
                        int id;
                        decimal buyprice;
                        decimal sellprice;
                        decimal qty;
                        string serial;
                        decimal itemdiss;
                        decimal totbeforediss;
                        decimal finaltot;
                        string note;
                        gridControl1.DataSource = InvoiceDetailTable;
                        InvoiceDetailTable = (DataTable)gridControl1.DataSource;
                        gridView6.FocusedRowHandle = -1;

                        for (int i = 0; i < gridView6.RowCount; i++)
                        {

                            if (inv.DocTypeId == 1 || inv.DocTypeId == 3)
                            {
                                invid = inv.InvoiceId;
                                id = Convert.ToInt32(gridView6.GetRowCellValue(i, "ItemId"));
                                buyprice = Convert.ToDecimal(gridView6.GetRowCellValue(i, "ItemBuyPrice"));
                                sellprice = Convert.ToDecimal(gridView6.GetRowCellValue(i, "ItemSellPrice"));
                                qty = Convert.ToDecimal(gridView6.GetRowCellValue(i, "Quantity"));
                                serial = Convert.ToString(gridView6.GetRowCellValue(i, "ItemSerial"));
                                itemdiss = Convert.ToDecimal(gridView6.GetRowCellValue(i, "ItemDiscount"));
                                totbeforediss = Convert.ToDecimal(gridView6.GetRowCellValue(i, "ItemMainQuantity"));
                                finaltot = Convert.ToDecimal(gridView6.GetRowCellValue(i, "TotalPrice"));
                                note = Convert.ToString(gridView6.GetRowCellValue(i, "InvoiceNotes"));

                                db.InseartInvoiceDetail(invid, id, sellprice, buyprice, qty, note, serial, itemdiss, totbeforediss, finaltot);
                                db.SubmitChanges();

                                //عمل ابديت للأيتم سيريال بعد البيع
                                var getitemserialid = db.ItemSerials.Where(a => a.ItemId == id && a.ItemSerial1 == serial).FirstOrDefault();
                                var isacceptserial = db.Items.Where(a => a.ItemId == id).FirstOrDefault();
                                if (isacceptserial.IsSerial==true)
                                {
                                    getitemserialid.ItemIsSold = true;
                                    getitemserialid.QuantityOut = 1;
                                    getitemserialid.CurrentBalance = 0;
                                    db.SubmitChanges();
                                }
                                else if(isacceptserial.IsSerial == false)
                                {
                                    ItemSerial IST = new ItemSerial();
                                    IST.AccountId = Convert.ToInt32(inv.StoreId);
                                    IST.CurrentBalance = IST.CurrentBalance - qty;
                                    IST.CurrentDate = Convert.ToDateTime(inv.InvoiceDate);
                                    IST.DocNum = Convert.ToInt32(inv.InvoiceId);
                                    IST.DocTypeId = Convert.ToInt32(inv.DocTypeId);
                                    IST.IsSerial = false;
                                    IST.ItemBarCode = isacceptserial.ItemBarCode;
                                    IST.ItemBuyPrice = buyprice;
                                    IST.ItemColor = "";
                                    IST.ItemDesc = isacceptserial.ItemDesc;
                                    IST.ItemId = id;
                                    IST.ItemIsSold = true;
                                    IST.ItemSceonedDesc = "";
                                    IST.ItemSellPriceCash = sellprice;
                                    IST.ItemSerial1 = "";
                                    IST.ItemSize = "";
                                    IST.QuantityIn = qty;
                                    IST.QuantityOut = 0;

                                    db.ItemSerials.InsertOnSubmit(IST);
                                    db.SubmitChanges();
                                }

                            }

                            

                            //   إدخال الاصناف في فورمة السيريالات فاتورة الشراء

                            if (inv.DocTypeId == 2 || inv.DocTypeId == 4)
                                {

                                    invid = inv.InvoiceId;
                                    id = Convert.ToInt32(gridView6.GetRowCellValue(i, "ItemId"));
                                    buyprice = Convert.ToDecimal(gridView6.GetRowCellValue(i, "ItemBuyPrice"));
                                    sellprice = Convert.ToDecimal(gridView6.GetRowCellValue(i, "ItemSellPrice"));
                                    qty = Convert.ToDecimal(gridView6.GetRowCellValue(i, "Quantity"));
                                    serial = Convert.ToString(gridView6.GetRowCellValue(i, "ItemSerial"));
                                    itemdiss = Convert.ToDecimal(gridView6.GetRowCellValue(i, "ItemDiscount"));
                                    totbeforediss = Convert.ToDecimal(gridView6.GetRowCellValue(i, "ItemMainQuantity"));
                                    finaltot = Convert.ToDecimal(gridView6.GetRowCellValue(i, "TotalPrice"));
                                    note = Convert.ToString(gridView6.GetRowCellValue(i, "InvoiceNotes"));

                                    db.InseartInvoiceDetail(invid, id, sellprice, buyprice, qty, note, serial, itemdiss, totbeforediss, finaltot);
                                    db.SubmitChanges();
                                    var isacceptserial = db.Items.Where(a => a.ItemId == id).FirstOrDefault();
                                    if (isacceptserial.IsSerial == true)
                                    {
                                        if (serial == null || serial == "" || serial == string.Empty)
                                        {
                                            ItemSerialForm ITSF = new ItemSerialForm();
                                            ITSF.createrow = Convert.ToInt32(qty);
                                            ITSF.itemcode = id;
                                            ITSF.isserial = true;
                                            ITSF.isinvoice = true;
                                            ITSF.invstid = Convert.ToInt32(inv.StoreId);
                                            ITSF.invid = Convert.ToInt32(inv.InvoiceId);
                                            ITSF.doctype = Convert.ToInt32(inv.DocTypeId);
                                            ITSF.invbuyprice = buyprice;
                                            ITSF.invsellprice = sellprice;
                                            ITSF.invissold = false;
                                            ITSF.ShowDialog();

                                        }
                                    }

                                    else if (isacceptserial.IsSerial == false)
                                    {
                                        ItemSerial IST = new ItemSerial();
                                        IST.AccountId = Convert.ToInt32(inv.StoreId);
                                        IST.CurrentBalance = IST.CurrentBalance + qty;
                                        IST.CurrentDate = Convert.ToDateTime(inv.InvoiceDate);
                                        IST.DocNum = Convert.ToInt32(inv.InvoiceId);
                                        IST.DocTypeId = Convert.ToInt32(inv.DocTypeId);
                                        IST.IsSerial = false;
                                        IST.ItemBarCode = isacceptserial.ItemBarCode;
                                        IST.ItemBuyPrice = buyprice;
                                        IST.ItemColor = "";
                                        IST.ItemDesc = isacceptserial.ItemDesc;
                                        IST.ItemId = id;
                                        IST.ItemIsSold = false;
                                        IST.ItemSceonedDesc = "";
                                        IST.ItemSellPriceCash = sellprice;
                                        IST.ItemSerial1 = "";
                                        IST.ItemSize = "";
                                        IST.QuantityIn = qty;
                                        IST.QuantityOut = 0;

                                        db.ItemSerials.InsertOnSubmit(IST);
                                        db.SubmitChanges();
                                    }
                                }


                            //حفظ باقي الفواتير وابديت المخزن على حسب نوع الفاتورة
                            

                        }

                        Clear();
                        MessageBox.Show(" تم الحفظ");

                    }
                }
            }
            catch
            {

                MessageBox.Show("لم يتم الحفظ برجاء التأكد من ملأ جميع البيانات والمحاولة مرة أخرى");
                return;
            }
        }
        
        private void RashioPerstextEdit2_KeyDown(object sender, KeyEventArgs e)
        {
           
                CalcRashioValue(e);
            
        }

        //تخصيم المقدم من الاجمالى
        private void CalcSumbmittedFromTotal()
        {
            decimal submitted = Convert.ToDecimal(SubmittedValuetextEdit.EditValue);
            decimal tot = Convert.ToDecimal(TotalNetValeOverAlltextEdit.EditValue);
            decimal finaltot = 0;


            if (submitted > 0)
            {
                finaltot = tot - submitted;
                TotalNetValeOverAlltextEdit.EditValue = finaltot;

            }
            else if (submitted <= 0)
            {
                CalcTotalAfterChange();

            }
        }

        //حساب نسبة الفائدة
        private void CalcRashioValue(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !IsPressed)
            {
                IsPressed = true;
                decimal value = 0;
                decimal lasttot = 0;
                decimal rashio = Convert.ToDecimal(RashioPerstextEdit2.EditValue);
                if (rashio > 0)
                {
                    decimal tot = Convert.ToDecimal(TotalValuetextEdit.EditValue);
                    decimal finaltot = Convert.ToDecimal(TotalNetValeOverAlltextEdit.EditValue);
                    value = (rashio / 100) * tot;
                    RashioValuetextEdit1.EditValue = value.ToString("0.00");
                    TotalNetValeOverAlltextEdit.Text = "";
                    lasttot = finaltot + value;
                    TotalNetValeOverAlltextEdit.EditValue = lasttot.ToString("0.00");
                    NumberOfInstallmenttextEdit.Enabled = true;
                    RashioPerstextEdit2.Enabled = false;
                }

                else if (rashio <= 0)
                {

                    RashioValuetextEdit1.EditValue = 0;
                    InsallmentcheckEdit.Checked = false;
                    CalcTotalAfterChange();
                }
            }
        }

        //حساب القسط
        void CalcInstallMent()
        {
           
            decimal totalwithoutsubmitted = 0;
            decimal totrashio = 0;
            decimal insallVal = 0;
            decimal rashio = 0;
            int count = 0;
            decimal submitted = 0;
            decimal tot = 0;
            decimal lastotal = 0;

            rashio = Convert.ToDecimal(RashioPerstextEdit2.EditValue);
            count = Convert.ToInt32(NumberOfInstallmenttextEdit.EditValue);
            submitted = Convert.ToDecimal(SubmittedValuetextEdit.EditValue);
            tot = Convert.ToDecimal(TotalValuetextEdit.EditValue);
           
          
            if (count <= 0 || submitted <= 0 || rashio <= 0)
            {
                MessageBox.Show("من فضلك أدخل عدد الأقساط والمقدم");
                InsallmentcheckEdit.Checked = false;

            }
            else if (count > 0 && submitted > 0 || rashio > 0)
            {
                totalwithoutsubmitted = tot - submitted;
                totrashio = totalwithoutsubmitted * (rashio / 100);
                lastotal = totalwithoutsubmitted + totrashio;
                insallVal = lastotal / count;
                InstallMentValuetextEdit1.Text = insallVal.ToString("0.00");
            }
            
        }
        //ربحية القسط
        void InstallmentWin()
        {
            decimal intsalcount = 0;
            decimal buyprice = 0;
            decimal totbuyprice = 0;
            decimal lastbuytot = 0;
            decimal installval = 0;
            decimal installcoast = 0;
            decimal totcoast = 0;
            decimal installmentwin = 0;
            decimal submitted = 0;

          
            for (int i = 0; i < gridView6.RowCount; i++)
            {
                buyprice = Convert.ToDecimal(gridView6.GetRowCellValue(i, "ItemBuyPrice"));
                totbuyprice = totbuyprice + buyprice;
            }
            intsalcount = Convert.ToDecimal(NumberOfInstallmenttextEdit.EditValue);
            submitted = Convert.ToDecimal(SubmittedValuetextEdit.EditValue);
            installval = Convert.ToDecimal(InstallMentValuetextEdit1.EditValue);
            lastbuytot = totbuyprice;
            totcoast = totbuyprice - submitted;
            installcoast = totcoast / intsalcount;
            installmentwin = installval - installcoast;
            InstallMentWin = installmentwin;
        }


        //الضرائب المضافة قيمة
        private void TotalTaxAddingtextEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            CalcAddTaxValue(e);
        }

        private void CalcAddTaxValue(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !IsPressed)
            {
                IsPressed = true;
                decimal taxval = Convert.ToDecimal(TotalTaxAddingtextEdit1.EditValue);
                if (taxval <= 0)
                {
                    TotalTaxAddingPercentagetextEdit1.Text = "";
                    CalcTotalAfterChange();
                    decimal taxdissval = Convert.ToDecimal(TotalTaxDisstextEdit1.EditValue);
                    if (taxdissval > 0)
                    {
                        decimal finatotal = Convert.ToDecimal(TotalNetValeOverAlltextEdit.EditValue);
                        decimal tot = Convert.ToDecimal(TotalTaxDisstextEdit1.EditValue);
                        decimal value = taxval / tot;
                        decimal percentage = (value * 100);
                        TotalTaxDissPercentagetextEdit1.Text = "";
                        TotalTaxDissPercentagetextEdit1.EditValue = percentage.ToString("0.00");
                        TotalNetValeOverAlltextEdit.EditValue = finatotal - value;
                    }

                }
                else if (taxval > 0)
                {
                    decimal finatotal = Convert.ToDecimal(TotalNetValeOverAlltextEdit.EditValue);
                    decimal tot = Convert.ToDecimal(TotalValuetextEdit.EditValue);
                    decimal value = taxval / tot;
                    decimal percentage = (value * 100);
                    TotalTaxAddingPercentagetextEdit1.Text = "";
                    TotalTaxAddingPercentagetextEdit1.EditValue = percentage.ToString("0.00");
                    TotalNetValeOverAlltextEdit.Text = "";
                    TotalNetValeOverAlltextEdit.EditValue = (finatotal + taxval).ToString("0.00");
                }

            }
        }

        //الضرائب المضافة نسبة
        private void TotalTaxAddingPercentagetextEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            CalcAddTaxPercent(e);
        }

        private void CalcAddTaxPercent(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !IsPressed)
            {
                IsPressed = true;
                decimal percentage = Convert.ToDecimal(TotalTaxAddingPercentagetextEdit1.EditValue);
                if (percentage <= 0)
                {
                    TotalTaxAddingtextEdit1.Text = "";
                    CalcTotalAfterChange();
                    decimal percentagediss = Convert.ToDecimal(TotalTaxDissPercentagetextEdit1.EditValue);
                    if (percentagediss > 0)
                    {
                        decimal finatotal = Convert.ToDecimal(TotalNetValeOverAlltextEdit.EditValue);

                        decimal tot = Convert.ToDecimal(TotalValuetextEdit.EditValue);
                        decimal value = ((percentage / 100) * tot);
                        TotalTaxDisstextEdit1.Text = "";
                        TotalTaxDisstextEdit1.EditValue = value.ToString("0.00");
                        TotalNetValeOverAlltextEdit.EditValue = (finatotal - value).ToString("0.00");
                    }
                }
                else if (percentage > 0)
                {
                    decimal finatotal = Convert.ToDecimal(TotalNetValeOverAlltextEdit.EditValue);

                    decimal tot = Convert.ToDecimal(TotalValuetextEdit.EditValue);
                    decimal value = ((percentage / 100) * tot);
                    TotalTaxAddingtextEdit1.Text = "";
                    TotalTaxAddingtextEdit1.EditValue = value.ToString("0.00");
                    TotalNetValeOverAlltextEdit.EditValue = (finatotal + value).ToString("0.00");
                }

            }
        }

        //الضرائب المخصومة قيمة
        private void TotalTaxDisstextEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            CalcDissTaxValue(e);
        }

        private void CalcDissTaxValue(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !IsPressed)
            {
                IsPressed = true;
                decimal taxval = Convert.ToDecimal(TotalTaxDisstextEdit1.EditValue);
                if (taxval <= 0)
                {
                    TotalTaxDissPercentagetextEdit1.Text = "";
                    CalcTotalAfterChange();
                    decimal taxaddval = Convert.ToDecimal(TotalTaxAddingtextEdit1.EditValue);
                    if (taxaddval > 0)
                    {
                        decimal finatotal = Convert.ToDecimal(TotalNetValeOverAlltextEdit.EditValue);
                        decimal tot = Convert.ToDecimal(TotalValuetextEdit.EditValue);
                        decimal value = taxval / tot;
                        decimal percentage = (value * 100);
                        TotalTaxAddingPercentagetextEdit1.Text = "";
                        TotalTaxAddingPercentagetextEdit1.EditValue = percentage.ToString("0.00");
                        TotalNetValeOverAlltextEdit.Text = "";
                        TotalNetValeOverAlltextEdit.EditValue = (finatotal + taxval).ToString("0.00");
                    }
                }
                else if (taxval > 0)
                {
                    decimal finatotal = Convert.ToDecimal(TotalNetValeOverAlltextEdit.EditValue);
                    decimal tot = Convert.ToDecimal(TotalValuetextEdit.EditValue);
                    decimal value = taxval / tot;
                    decimal percentage = (value * 100);
                    TotalTaxDissPercentagetextEdit1.Text = "";
                    TotalTaxDissPercentagetextEdit1.EditValue = percentage.ToString("0.00");
                    TotalNetValeOverAlltextEdit.EditValue = (finatotal - value).ToString("0.00");
                }

            }
        }

        //الضرائب المخصومة نسبة
        private void TotalTaxDissPercentagetextEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            CalcDissTaxPercent(e);
        }

        private void CalcDissTaxPercent(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !IsPressed)
            {
                IsPressed = true;
                decimal percentage = Convert.ToDecimal(TotalTaxDissPercentagetextEdit1.EditValue);
                if (percentage <= 0)
                {
                    TotalTaxDisstextEdit1.Text = "";
                    CalcTotalAfterChange();
                    decimal percentageadd = Convert.ToDecimal(TotalTaxAddingPercentagetextEdit1.EditValue);
                    if (percentageadd > 0)
                    {
                        decimal finatotal = Convert.ToDecimal(TotalNetValeOverAlltextEdit.EditValue);

                        decimal tot = Convert.ToDecimal(TotalValuetextEdit.EditValue);
                        decimal value = ((percentage / 100) * tot);
                        TotalTaxAddingtextEdit1.Text = "";
                        TotalTaxAddingtextEdit1.EditValue = value.ToString("0.00");
                        TotalNetValeOverAlltextEdit.EditValue = (finatotal + value).ToString("0.00");
                    }
                    else if (percentage > 0)
                    {
                        decimal finatotal = Convert.ToDecimal(TotalNetValeOverAlltextEdit.EditValue);

                        decimal tot = Convert.ToDecimal(TotalValuetextEdit.EditValue);
                        decimal value = ((percentage / 100) * tot);
                        TotalTaxDisstextEdit1.Text = "";
                        TotalTaxDisstextEdit1.EditValue = value.ToString("0.00");
                        TotalNetValeOverAlltextEdit.EditValue = (finatotal - value).ToString("0.00");
                    }

                }
            }
        }

        private void InvoiceForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (IsPressed)
            {
                IsPressed = false;
            }
               
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(checkEdit1.Checked)==true)
            {
                InsallmentcheckEdit.Checked = false;
                SubmittedValuetextEdit.Text = "0";
                NumberOfInstallmenttextEdit.Text = "0";
                TotalTaxAddingtextEdit1.Text = "0";
                TotalTaxAddingPercentagetextEdit1.Text = "0";
                TotalTaxDisstextEdit1.Text = "0";
                TotalTaxDisstextEdit1.Text = "0";
                TotalTaxDissPercentagetextEdit1.Text = "0";
                InstallMentValuetextEdit1.Text = "0";
                RashioValuetextEdit1.Text = "0";
                RashioPerstextEdit2.Text = "0";
                IsPressed = true;
                CalcTotalAfterChange();
                checkEdit1.Checked = false;
            }
        }
    }
}