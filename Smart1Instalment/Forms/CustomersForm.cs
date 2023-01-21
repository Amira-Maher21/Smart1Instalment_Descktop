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
using static System.Net.WebRequestMethods;
using System.IO;

namespace Smart1Instalment.Forms
{
    public partial class CustomersForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Smart1InstallmentDataClassesDataContext db = new Smart1InstallmentDataClassesDataContext();
        public static int Flag ;

        public bool IsEdit { get; set; }
        public int Code { get; set; }
        public object Edit { get; set; }
        public CustomersForm()
        {
            InitializeComponent();

            //اخفاء او الاظهرمن فورم او العميل الضامن
            if (Flag==2)
            {
                layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }
        void InsertOrUpdate()
        {
            if (Flag == 2)
            {
                if (!dxValidationProvider1.Validate())
                {
                    return;
                }
            }
            else
            {
                if (!dxValidationProvider2.Validate())
                {
                    return;
                }
            }
          
            try
            {
                if (IsEdit)
                {
                    var getdata = db.Accounts.Where(a => a.AccountId == Code).FirstOrDefault();

                    if (Flag == 2)
                    {
                        getdata.AccountChildId = Convert.ToInt32(searchLookUpEditChild.EditValue);
                    }
                    else
                    {
                        getdata.AccountChildId = null;
                    }

                    getdata.AccountFlagId= Flag;
                    getdata.Address = Convert.ToString(textAddress.EditValue);
                    getdata.IdentityCardNumber = Convert.ToString(textIdentityCard.EditValue);
                    getdata.PhoneNum1 = Convert.ToString(textPhoneNum1.EditValue);
                    getdata.PhoneNum2 = Convert.ToString(textPhoneNum2.EditValue);
                    
                    getdata.IsActive = Convert.ToBoolean(checkEdit.EditValue);


                    if (getdata.IdentificationId1 != null)
                    {
                        pictureEdit01.Image = new Bitmap(getdata.IdentificationId1);
                        pictureEdit01.Tag = getdata.IdentificationId1;
                    }
                    if (getdata.IdentificationId2 != null)
                    {
                        pictureEdit02.Image = new Bitmap(getdata.IdentificationId2);
                        pictureEdit02.Tag = getdata.IdentificationId2;
                    }
                    if (getdata.IdentificationId3 != null)
                    {
                        pictureEdit03.Image = new Bitmap(getdata.IdentificationId3);
                        pictureEdit03.Tag = getdata.IdentificationId3;
                    }
                    if (getdata.IdentificationId4 != null)
                    {
                        pictureEdit04.Image = new Bitmap(getdata.IdentificationId4);
                        pictureEdit04.Tag = getdata.IdentificationId4;
                    }
                    if (getdata.IdentificationId5 != null)
                    {
                        pictureEdit05.Image = new Bitmap(getdata.IdentificationId5);
                        pictureEdit05.Tag = getdata.IdentificationId5;
                    }
                    if (getdata.IdentificationId6 != null)
                    {
                        pictureEdit06.Image = new Bitmap(getdata.IdentificationId6);
                        pictureEdit06.Tag = getdata.IdentificationId6;
                    }
                    if (getdata.IdentificationId7 != null)
                    {
                        pictureEdit07.Image = new Bitmap(getdata.IdentificationId7);
                        pictureEdit07.Tag = getdata.IdentificationId7;
                    }
                    if (getdata.IdentificationId8 != null)
                    {
                        pictureEdit08.Image = new Bitmap(getdata.IdentificationId8);
                        pictureEdit08.Tag = getdata.IdentificationId8;
                    }



                    db.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    Clear();
                }
                else
                {
                    Account Acc = new Account();
                    if (Flag == 2)
                    {
                        Acc.AccountChildId = Convert.ToInt32(searchLookUpEditChild.EditValue);
                    }
                    else
                    {
                        Acc.AccountChildId = null;
                    }
                   
                    Acc.AccountName = Convert.ToString(textaccount.Text);
                    Acc.AccountFlagId = Flag;
                    Acc.Address = Convert.ToString(textAddress.EditValue);
                    Acc.IdentityCardNumber = Convert.ToString(textIdentityCard.EditValue);
                    Acc.PhoneNum1 = Convert.ToString(textPhoneNum1.EditValue);
                    Acc.PhoneNum2 = Convert.ToString(textPhoneNum2.EditValue);
                   
                    Acc.IsActive = Convert.ToBoolean(checkEdit.EditValue);
                    //حفظ الصوره

                    //1
                    if (pictureEdit01.Tag != null || pictureEdit01.Tag != DBNull.Value)
                    {
                        string path = string.Empty;
                        if (Acc.IdentificationId1 == null)
                        {
                            path = string.Empty;
                        }
                        else
                        {
                            path = Acc.IdentificationId1;
                        }
                        if (pictureEdit01.Tag == null)
                        {
                            pictureEdit01.Tag = string.Empty;
                        }

                        if (pictureEdit01.Tag.ToString() != path)
                        {
                            if (System.IO.File.Exists(path))
                            {
                                var file = System.IO.File.OpenRead(path);
                                file.Close();

                                string[] Pathes = path.Split('.');
                                System.IO.File.Copy(pictureEdit01.Tag.ToString(), Pathes[0] + "1.jpg");
                                Acc.IdentificationId1 = Pathes[0] + "1.jpg";
                            }
                            else
                            {
                                System.IO.File.Copy(pictureEdit01.Tag.ToString(), @"D:\PicSol\" + textaccount.Text.ToString() + "01.jpg");

                                Acc.IdentificationId1 = @"D:\PicSol\" + textaccount.Text.ToString() + "01.jpg";
                            }
                            }
                        }
                        else
                        {
                            Acc.IdentificationId1 = null;
                        }
                        //2
                        if (pictureEdit02.Tag != null || pictureEdit02.Tag != DBNull.Value)
                        {
                            string path = string.Empty;
                            if (Acc.IdentificationId2 == null)
                            {
                                path = string.Empty;
                            }
                            else
                            {
                                path = Acc.IdentificationId2;
                            }
                            if (pictureEdit02.Tag == null)
                            {
                                pictureEdit02.Tag = string.Empty;
                            }

                            if (pictureEdit02.Tag.ToString() != path)
                            {
                            if (System.IO.File.Exists(path))
                            {
                                var file = System.IO.File.OpenRead(path);
                                file.Close();

                                    string[] Pathes = path.Split('.');
                                    System.IO.File.Copy(pictureEdit02.Tag.ToString(), Pathes[0] + "1.jpg");
                                    Acc.IdentificationId2 = Pathes[0] + "1.jpg";
                                }
                                else
                                {
                                    System.IO.File.Copy(pictureEdit02.Tag.ToString(), @"D:\PicSol\" + textaccount.Text.ToString() + "02.jpg");
                                    Acc.IdentificationId2 = @"D:\PicSol\" + textaccount.Text.ToString() + "02.jpg";
                                }
                            }

                        }
                        else
                        {
                            Acc.IdentificationId2 = null;
                        }

                        //3

                        if (pictureEdit03.Tag != null || pictureEdit03.Tag != DBNull.Value)
                        {

                            string Path = null;
                            if (Acc.IdentificationId3 == null)
                            {
                                Path = string.Empty;
                            }
                            else
                            {
                                Path = Acc.IdentificationId3;
                            }
                            if (pictureEdit03.Tag == null)
                            {
                                pictureEdit03.Tag = string.Empty;
                            }
                            if (pictureEdit03.Tag.ToString() != Path)
                            {
                            if (System.IO.File.Exists(Path))
                            {
                                var file = System.IO.File.OpenRead(Path);
                                file.Close();
                                    
                                    string[] Pathes = Path.Split('.');
                                    System.IO.File.Copy(pictureEdit03.Tag.ToString(), Pathes[0] + "1.jpg");
                                    Acc.IdentificationId3 = Pathes[0] + "1.jpg";
                                }
                                else
                                {
                                    System.IO.File.Copy(pictureEdit03.Tag.ToString(), @"D:\PicSol\" + textaccount.Text.ToString() + "03.jpg");
                                    Acc.IdentificationId3 = @"D:\PicSol\" + textaccount.Text.ToString() + "03.jpg";
                                }
                            }



                        }
                        else
                        {
                            Acc.IdentificationId3 = null;
                        }
                        //4

                        if (pictureEdit04.Tag != null || pictureEdit04.Tag != DBNull.Value)
                        {
                            string Path = null;
                            if (Acc.IdentificationId4 == null)
                            {
                                Path = string.Empty;
                            }
                            else
                            {
                                Path = Acc.IdentificationId4;
                            }
                            if (pictureEdit04.Tag == null)
                            {
                                pictureEdit04.Tag = string.Empty;
                            }
                            if (pictureEdit04.Tag.ToString() != Path)
                            {
                            if (System.IO.File.Exists(Path))
                            {
                                var file = System.IO.File.OpenRead(Path);
                                file.Close();
                                    //System.IO.File.Delete(path);
                                    string[] Pathes = Path.Split('.');
                                    System.IO.File.Copy(pictureEdit04.Tag.ToString(), Pathes[0] + "1.jpg");
                                    Acc.IdentificationId4 = Pathes[0] + "1.jpg";
                                }
                                else
                                {
                                    System.IO.File.Copy(pictureEdit04.Tag.ToString(), @"D:\PicSol\" + textaccount.Text.ToString() + "04.jpg");
                                    Acc.IdentificationId4 = @"D:\PicSol\" + textaccount.Text.ToString() + "04.jpg";
                                }
                            }

                        }
                        else
                        {
                            Acc.IdentificationId4 = null;
                        }

                        //5
                        if (pictureEdit05.Tag != null || pictureEdit05.Tag != DBNull.Value)
                        {

                            string Path = null;
                            if (Acc.IdentificationId5 == null)
                            {
                                Path = string.Empty;
                            }
                            else
                            {
                                Path = Acc.IdentificationId5;
                            }
                            if (pictureEdit05.Tag == null)
                            {
                                pictureEdit05.Tag = string.Empty;
                            }
                            if (pictureEdit05.Tag.ToString() != Path)
                            {
                            if (System.IO.File.Exists(Path))
                            {
                                var file = System.IO.File.OpenRead(Path);
                                file.Close();
                                    //System.IO.File.Delete(path);
                                    string[] Pathes = Path.Split('.');
                                    System.IO.File.Copy(pictureEdit05.Tag.ToString(), Pathes[0] + "1.jpg");
                                    Acc.IdentificationId5 = Pathes[0] + "1.jpg";
                                }
                                else
                                {
                                    System.IO.File.Copy(pictureEdit05.Tag.ToString(), @"D:\PicSol\" + textaccount.Text.ToString() + "05.jpg");
                                    Acc.IdentificationId5 = @"D:\PicSol\" + textaccount.Text.ToString() + "05.jpg";
                                }

                            }

                        }
                        else
                        {
                            Acc.IdentificationId5 = null;
                        }
                        //6

                        if (pictureEdit06.Tag != null || pictureEdit06.Tag != DBNull.Value)
                        {

                            string path = null;
                            if (Acc.IdentificationId6 == null)
                            {
                                path = string.Empty;
                            }
                            else
                            {
                                path = Acc.IdentificationId6;
                            }
                            if (pictureEdit06.Tag == null)
                            {
                                pictureEdit06.Tag = string.Empty;
                            }
                            if (pictureEdit06.Tag.ToString() != path)
                            {
                            if (System.IO.File.Exists(path))
                            {
                                var file = System.IO.File.OpenRead(path);
                                file.Close();
                                    //System.IO.File.Delete(path);
                                    string[] Pathes = path.Split('.');
                                    System.IO.File.Copy(pictureEdit06.Tag.ToString(), Pathes[0] + "1.jpg");
                                    Acc.IdentificationId6 = Pathes[0] + "1.jpg";
                                }
                                else
                                {
                                    System.IO.File.Copy(pictureEdit05.Tag.ToString(), @"D:\PicSol\" + textaccount.Text.ToString() + "06.jpg");
                                    Acc.IdentificationId6 = @"D:\PicSol\" + textaccount.Text.ToString() + "06.jpg";
                                }

                            }

                        }
                        else
                        {
                            Acc.IdentificationId6 = null;
                        }

                    if (pictureEdit07.Tag != null || pictureEdit07.Tag != DBNull.Value)
                    {

                        string path = null;
                        if (Acc.IdentificationId7 == null)
                        {
                            path = string.Empty;
                        }
                        else
                        {
                            path = Acc.IdentificationId7;
                        }
                        if (pictureEdit07.Tag == null)
                        {
                            pictureEdit07.Tag = string.Empty;
                        }
                        if (pictureEdit07.Tag.ToString() != path)
                        {
                            if (System.IO.File.Exists(path))
                            {
                                var file = System.IO.File.OpenRead(path);
                                file.Close();
                                //System.IO.File.Delete(path);
                                string[] Pathes = path.Split('.');
                                System.IO.File.Copy(pictureEdit07.Tag.ToString(), Pathes[0] + "7.jpg");
                                Acc.IdentificationId7 = Pathes[0] + "7.jpg";
                            }
                            else
                            {
                                System.IO.File.Copy(pictureEdit07.Tag.ToString(), @"D:\PicSol\" + textaccount.Text.ToString() + "07.jpg");
                                Acc.IdentificationId7 = @"D:\PicSol\" + textaccount.Text.ToString() + "07.jpg";
                            }

                        }

                    }
                    else
                    {
                        Acc.IdentificationId7 = null;
                    }
                    if (pictureEdit08.Tag != null || pictureEdit08.Tag != DBNull.Value)
                    {

                        string path = null;
                        if (Acc.IdentificationId8 == null)
                        {
                            path = string.Empty;
                        }
                        else
                        {
                            path = Acc.IdentificationId6;
                        }
                        if (pictureEdit08.Tag == null)
                        {
                            pictureEdit08.Tag = string.Empty;
                        }
                        if (pictureEdit08.Tag.ToString() != path)
                        {
                            if (System.IO.File.Exists(path))
                            {
                                var file = System.IO.File.OpenRead(path);
                                file.Close();
                                //System.IO.File.Delete(path);
                                string[] Pathes = path.Split('.');
                                System.IO.File.Copy(pictureEdit08.Tag.ToString(), Pathes[0] + "8.jpg");
                                Acc.IdentificationId8 = Pathes[0] + "8.jpg";
                            }
                            else
                            {
                                System.IO.File.Copy(pictureEdit08.Tag.ToString(), @"D:\PicSol\" + textaccount.Text.ToString() + "08.jpg");
                                Acc.IdentificationId6 = @"D:\PicSol\" + textaccount.Text.ToString() + "08.jpg";
                            }

                        }

                    }
                    else
                    {
                        Acc.IdentificationId6 = null;
                    }





                    db.Accounts.InsertOnSubmit(Acc);
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
            textaccount.Text = "";
             searchLookUpEditChild.EditValue=null;
             textAddress.EditValue=null;
             textIdentityCard.EditValue=null;
             textPhoneNum1.EditValue=null;
             textPhoneNum2.EditValue=null;
             
             checkEdit.EditValue=true;
             pictureEdit01.Image = null;
             pictureEdit02.Image = null;
             pictureEdit03.Image = null;
             pictureEdit04.Image = null;
             pictureEdit05.Image = null;
             pictureEdit06.Image = null;
            pictureEdit07.Image = null;
            pictureEdit08.Image = null;
        }
        void Fill()
        {
            searchLookUpEditChild.Properties.DataSource = db.Accounts.Where(a=> a.AccountFlagId==8 && a.IsActive==true ).ToList();

            checkEdit.Checked = true;
           

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

        private void CustomersForm_Load(object sender, EventArgs e)
        {
            Fill();
        }

        private void pictureEdit01_DoubleClick(object sender, EventArgs e)
        {
            // open file dialog 
            OpenFileDialog open = new OpenFileDialog() { /* image filters*/Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp" };
            if (open.ShowDialog() == DialogResult.OK)
            {

                //display image in picture box
                pictureEdit01.Image = new Bitmap(open.FileName);
                pictureEdit01.Tag = open.FileName;
            }
        }

        private void pictureEdit02_DoubleClick(object sender, EventArgs e)
        {
            // open file dialog 
            OpenFileDialog open = new OpenFileDialog() { /* image filters*/Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp" };
            if (open.ShowDialog() == DialogResult.OK)
            {

                //display image in picture box
                pictureEdit02.Image = new Bitmap(open.FileName);
                pictureEdit02.Tag = open.FileName;
            }
        }

        private void pictureEdit03_DoubleClick(object sender, EventArgs e)
        {
            // open file dialog 
            OpenFileDialog open = new OpenFileDialog() { /* image filters*/Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp" };
            if (open.ShowDialog() == DialogResult.OK)
            {

                //display image in picture box
                pictureEdit03.Image = new Bitmap(open.FileName);
                pictureEdit03.Tag = open.FileName;
            }
        }

        private void pictureEdit04_DoubleClick(object sender, EventArgs e)
        {
            // open file dialog 
            OpenFileDialog open = new OpenFileDialog() { /* image filters*/Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp" };
            if (open.ShowDialog() == DialogResult.OK)
            {

                //display image in picture box
                pictureEdit04.Image = new Bitmap(open.FileName);
                pictureEdit04.Tag = open.FileName;
            }
        }

        private void pictureEdit05_DoubleClick(object sender, EventArgs e)
        {
            // open file dialog 
            OpenFileDialog open = new OpenFileDialog() { /* image filters*/Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp" };
            if (open.ShowDialog() == DialogResult.OK)
            {

                //display image in picture box
                pictureEdit05.Image = new Bitmap(open.FileName);
                pictureEdit05.Tag = open.FileName;
            }
        }

        private void pictureEdit06_DoubleClick(object sender, EventArgs e)
        {
            // open file dialog 
            OpenFileDialog open = new OpenFileDialog() { /* image filters*/Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp" };
            if (open.ShowDialog() == DialogResult.OK)
            {

                //display image in picture box
                pictureEdit06.Image = new Bitmap(open.FileName);
                pictureEdit06.Tag = open.FileName;
            }
        }

        private void pictureEdit07_DoubleClick(object sender, EventArgs e)
        {
            // open file dialog 
            OpenFileDialog open = new OpenFileDialog() { /* image filters*/Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp" };
            if (open.ShowDialog() == DialogResult.OK)
            {

                //display image in picture box
                pictureEdit07.Image = new Bitmap(open.FileName);
                pictureEdit07.Tag = open.FileName;
            }
        }

        private void pictureEdit08_DoubleClick(object sender, EventArgs e)
        {
            // open file dialog 
            OpenFileDialog open = new OpenFileDialog() { /* image filters*/Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp" };
            if (open.ShowDialog() == DialogResult.OK)
            {

                //display image in picture box
                pictureEdit08.Image = new Bitmap(open.FileName);
                pictureEdit08.Tag = open.FileName;
            }
        }
    }
}