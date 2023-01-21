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

namespace Smart1Instalment.Forms
{
    public partial class ShowInvForm : DevExpress.XtraBars.Ribbon.RibbonForm

    {

       
        public static int docid;
        public ShowInvForm()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            docid = 2;
            InvoiceForm inv = new InvoiceForm();
            inv.docktype = docid;
            inv.ShowDialog();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            docid = 4;
            InvoiceForm inv = new InvoiceForm();
            inv.docktype = docid;
            inv.ShowDialog();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            docid = 1;
            InvoiceForm inv = new InvoiceForm();
            inv.docktype = docid;
            inv.ShowDialog();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            docid = 3;
            InvoiceForm inv = new InvoiceForm();
            inv.docktype = docid;
            inv.ShowDialog();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            docid = 6;
            InvoiceForm inv = new InvoiceForm();
            inv.docktype = docid;
            inv.ShowDialog();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            docid = 8;
            InvoiceForm inv = new InvoiceForm();
            inv.docktype = docid;
            inv.ShowDialog();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            docid = 5;
            InvoiceForm inv = new InvoiceForm();
            inv.docktype = docid;
            inv.ShowDialog();
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            docid = 7;
            InvoiceForm inv = new InvoiceForm();
            inv.docktype = docid;
            inv.ShowDialog();
        }








        // 1	بيع نقدى
        //2	شراء نقدى
        //3	بيع أجل
        //4	شراء أجل
        //5	مرتجع بيع نقدى
        //6	مرتجع شراء نقدى
        //7	مرتجع بيع أجل
        //8	مرتجع شراء اجل
    }
}