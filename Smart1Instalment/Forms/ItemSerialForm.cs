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
    public partial class ItemSerialForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
      public  int createrow;
        public int itemcode;
        DataTable SerailTable;
        public bool isserial;
        public bool isitem;
        public bool isinvoice;
        public int invstid;
        public int invid;
        public int doctype;
        public decimal invbuyprice;
        public decimal invsellprice;
        public bool invissold;
        public bool IsEdit { get; set; }
        public int Code { get; set; }
        Smart1InstallmentDataClassesDataContext db = new Smart1InstallmentDataClassesDataContext();
        public ItemSerialForm()
        {
            InitializeComponent();
            CreateDataGridSerial();
            layoutControlItem1.Enabled = false;
        }

        private void ItemSerialForm_Load(object sender, EventArgs e)
        {
            
            if (isinvoice)
            {
                searchLookUpEditItem.Properties.DataSource = db.Items.Where(a => a.ItemIsActive == true ).ToList();
                searchLookUpEditItem.EditValue = itemcode;
                searchLookUpEdit1Store.Properties.DataSource = db.Accounts.Where(a => a.AccountFlagId == 4 && a.IsActive == true).ToList();
                searchLookUpEdit1Store.EditValue = invstid;
                for (int i = 0; i < createrow; i++)
                {
                    gridView1.AddNewRow();
                   
                    
                }
                
            }
           
            else if (isitem)
            {
                searchLookUpEditItem.Properties.DataSource = db.Items.Where(a => a.ItemIsActive == true && a.IsSerial == true).ToList();
                searchLookUpEditItem.EditValue = itemcode;
                searchLookUpEdit1Store.Properties.DataSource = db.Accounts.Where(a => a.AccountFlagId == 4 && a.IsActive == true).ToList();
                for (int i = 0; i < createrow; i++)
                {
                    gridView1.AddNewRow();
                    
                }
            }
            if (isinvoice)
            {
                gridView1.FocusedRowHandle = -1;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    gridView1.SetRowCellValue(i, "ItemBuyPrice", invbuyprice);
                    gridView1.SetRowCellValue(i, "ItemSellPriceCash", invsellprice);
                    gridView1.SetRowCellValue(i, "ItemIsSold", invissold);
                }
            }
        }

        private void CreateDataGridSerial()
        {
            SerailTable = new DataTable();
            SerailTable.Columns.Clear();
            SerailTable.Columns.Add("ItemSerial", typeof(string));
            SerailTable.Columns.Add("ItemColor", typeof(string));
            SerailTable.Columns.Add("ItemSize", typeof(string));
            SerailTable.Columns.Add("ItemSceonedDesc", typeof(string));
            SerailTable.Columns.Add("ItemBuyPrice", typeof(decimal));
            SerailTable.Columns.Add("ItemSellPriceCash", typeof(decimal));
            //SerailTable.Columns.Add("ItemSellPriceInstallment", typeof(decimal));
            SerailTable.Columns.Add("ItemIsSold", typeof(bool));

            gridControl1.DataSource = SerailTable;


        }
        void Clear()
        {
            CreateDataGridSerial();
            searchLookUpEditItem.EditValue = null;
            searchLookUpEditItem.EditValue = null;
        }
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            //حفظ السيريالات من الاصناف
            if (isitem)
            {
                if (isserial == true)
                {
                    try
                    {
                        if (IsEdit)
                        {

                        }
                        else
                        {

                            int id;
                            string name;
                            string serial;
                            string color;
                            string size;
                            string sceoneddesc;
                            decimal buy;
                            decimal cachsell;
                            //decimal installsell;
                            bool issold;
                            int stid;

                            id = Convert.ToInt32(searchLookUpEditItem.EditValue);
                            name = Convert.ToString(searchLookUpEditItem.Text);
                            gridControl1.DataSource = SerailTable;
                            SerailTable = (DataTable)gridControl1.DataSource;

                            gridView1.FocusedRowHandle = -1;

                            for (int i = 0; i < gridView1.RowCount; i++)
                            {
                                serial = Convert.ToString(gridView1.GetRowCellValue(i, "ItemSerial"));
                                color = Convert.ToString(gridView1.GetRowCellValue(i, "ItemColor"));
                                size = Convert.ToString(gridView1.GetRowCellValue(i, "ItemSize"));
                                sceoneddesc = Convert.ToString(gridView1.GetRowCellValue(i, "ItemSceonedDesc"));
                                buy = Convert.ToDecimal(gridView1.GetRowCellValue(i, "ItemBuyPrice"));
                                cachsell = Convert.ToDecimal(gridView1.GetRowCellValue(i, "ItemSellPriceCash"));
                                //installsell = Convert.ToDecimal(gridView1.GetRowCellValue(i, "ItemSellPriceInstallment"));
                                stid = Convert.ToInt32(searchLookUpEdit1Store.EditValue);
                                if (Convert.ToBoolean(gridView1.GetRowCellValue(i, "ItemIsSold")) == false || gridView1.GetRowCellValue(i, "ItemIsSold") == DBNull.Value)
                                {
                                    issold = false;
                                }
                                else
                                {
                                    issold = true;
                                }

                                db.InseartSerials(id, name, serial, color, size, sceoneddesc, buy, cachsell, 0, issold, 0, 10, 1, 0, 1, DateTime.Now.Date, stid, true);
                                db.SubmitChanges();
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
            }
            //حفظ السيريالات من الفاتورة
           else  if (isinvoice)
            {
                if (isserial == true)
                {
                    try
                    {
                        if (IsEdit)
                        {

                        }
                        else
                        {

                            int id;
                            string name;
                            string serial;
                            string color;
                            string size;
                            string sceoneddesc;
                            decimal buy;
                            decimal cachsell;
                            //decimal installsell;
                            bool issold;
                            int stid;

                            id = Convert.ToInt32(searchLookUpEditItem.EditValue);
                            name = Convert.ToString(searchLookUpEditItem.Text);
                            gridControl1.DataSource = SerailTable;
                            SerailTable = (DataTable)gridControl1.DataSource;

                            gridView1.FocusedRowHandle = -1;

                            for (int i = 0; i < gridView1.RowCount; i++)
                            {
                                serial = Convert.ToString(gridView1.GetRowCellValue(i, "ItemSerial"));
                                color = Convert.ToString(gridView1.GetRowCellValue(i, "ItemColor"));
                                size = Convert.ToString(gridView1.GetRowCellValue(i, "ItemSize"));
                                sceoneddesc = Convert.ToString(gridView1.GetRowCellValue(i, "ItemSceonedDesc"));
                                buy = Convert.ToDecimal(gridView1.GetRowCellValue(i, "ItemBuyPrice"));
                                cachsell = Convert.ToDecimal(gridView1.GetRowCellValue(i, "ItemSellPriceCash"));
                                //installsell = Convert.ToDecimal(gridView1.GetRowCellValue(i, "ItemSellPriceInstallment"));
                                stid = invstid;
                                issold = Convert.ToBoolean(gridView1.GetRowCellValue(i, "ItemIsSold"));
                               
                                db.InseartSerials(id, name, serial, color, size, sceoneddesc, buy, cachsell, 0, issold, invid, doctype, 1, 0, 1, DateTime.Now.Date, stid, true);
                                db.SubmitChanges();
                            }


                            MessageBox.Show("تم الحفظ");
                            Clear();
                            this.Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("لم يتم الحفظ ");

                    }
                }
            }
           
        }
    }
}