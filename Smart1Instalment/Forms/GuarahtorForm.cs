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
using System.IO;

namespace Smart1Instalment.Forms
{
    public partial class GuarahtorForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Smart1InstallmentDataClassesDataContext db = new Smart1InstallmentDataClassesDataContext();
        public static int Flag = 8;

        public bool IsEdit { get; set; }
        public int Code { get; set; }
        public object Edit { get; set; }
        public object File { get; private set; }

        public GuarahtorForm()
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
                    var getdata = db.Accounts.Where(a => a.AccountId == Code).FirstOrDefault();
                    getdata.AccountName = Convert.ToString(textEditAccountName.Text);
                    getdata.AccountFlagId = 8;
                    getdata.Address = Convert.ToString(textEditAddress.Text);
                    getdata.PhoneNum1 = Convert.ToString(textEditPhoneNum1.EditValue);
                    getdata.PhoneNum2 = Convert.ToString(textEditPhoneNum2.EditValue);
                    getdata.IdentityCardNumber = Convert.ToString(textEditIdentityCard.EditValue);
                    
                    getdata.IsActive = Convert.ToBoolean(checkEdit1.Checked);
                    //تعديل الصوره



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

                    db.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    Clear();

                }
                else
                {
                    Account gu = new Account();
                    gu.AccountName = Convert.ToString(textEditAccountName.Text);
                    gu.Address = Convert.ToString(textEditAddress.Text);
                    gu.AccountFlagId = 8;
                    gu.PhoneNum1 = Convert.ToString(textEditPhoneNum1.EditValue);
                    gu.PhoneNum2 = Convert.ToString(textEditPhoneNum2.EditValue);
                    gu.IdentityCardNumber = Convert.ToString(textEditIdentityCard.EditValue);
                   
                    gu.IsActive = Convert.ToBoolean(checkEdit1.Checked);
                    //حفظ الصوره

                    //1
                    if (pictureEdit01.Tag != null || pictureEdit01.Tag != DBNull.Value)
                    {
                        string path = string.Empty;
                        if (gu.IdentificationId1 == null)
                        {
                            path = string.Empty;
                        }
                        else
                        {
                            path = gu.IdentificationId1;
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
                                gu.IdentificationId1 = Pathes[0] + "1.jpg";
                            }
                            else
                            {
                                System.IO.File.Copy(pictureEdit01.Tag.ToString(), @"D:\PicSol\" + textEditAccountName.Text.ToString() + "01.jpg");
                                gu.IdentificationId1 = @"D:\PicSol\" + textEditAccountName.Text.ToString() + "01.jpg";
                            }
                        }
                    }
                    else
                    {
                        gu.IdentificationId1 = null;
                    }
                        //2
                        if (pictureEdit02.Tag != null || pictureEdit02.Tag != DBNull.Value)
                        {
                            string path = string.Empty;
                            if (gu.IdentificationId2 == null)
                            {
                                path = string.Empty;
                            }
                            else
                            {
                                path = gu.IdentificationId2;
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
                                    gu.IdentificationId2 = Pathes[0] + "1.jpg";
                                }
                                else
                                {
                                    System.IO.File.Copy(pictureEdit02.Tag.ToString(), @"D:\PicSol\" + textEditAccountName.Text.ToString() + "02.jpg");
                                    gu.IdentificationId2 = @"D:\PicSol\" + textEditAccountName.Text.ToString() + "02.jpg";
                                }
                            }

                        }
                        else
                        {
                            gu.IdentificationId2 = null;
                        }

                        //3

                        if (pictureEdit03.Tag != null || pictureEdit03.Tag != DBNull.Value)
                        {

                            string Path = null;
                            if (gu.IdentificationId3 == null)
                            {
                                Path = string.Empty;
                            }
                            else
                            {
                                Path = gu.IdentificationId3;
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
                                    gu.IdentificationId3 = Pathes[0] + "1.jpg";
                                }
                                else
                                {
                                    System.IO.File.Copy(pictureEdit03.Tag.ToString(), @"D:\PicSol\" + textEditAccountName.Text.ToString() + "03.jpg");
                                    gu.IdentificationId3 = @"D:\PicSol\" + textEditAccountName.Text.ToString() + "03.jpg";
                                }
                            }



                        }
                        else
                        {
                            gu.IdentificationId3 = null;
                        }
                        //4

                        if (pictureEdit04.Tag != null || pictureEdit04.Tag != DBNull.Value)
                        {
                            string Path = null;
                            if (gu.IdentificationId4 == null)
                            {
                                Path = string.Empty;
                            }
                            else
                            {
                                Path = gu.IdentificationId4;
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
                                    gu.IdentificationId4 = Pathes[0] + "1.jpg";
                                }
                                else
                                {
                                    System.IO.File.Copy(pictureEdit04.Tag.ToString(), @"D:\PicSol\" + textEditAccountName.Text.ToString() + "04.jpg");
                                    gu.IdentificationId4 = @"D:\PicSol\" + textEditAccountName.Text.ToString() + "04.jpg";
                                }
                            }

                        }
                        else
                        {
                            gu.IdentificationId4 = null;
                        }

                        //5
                        if (pictureEdit05.Tag != null || pictureEdit05.Tag != DBNull.Value)
                        {

                            string Path = null;
                            if (gu.IdentificationId5 == null)
                            {
                                Path = string.Empty;
                            }
                            else
                            {
                                Path = gu.IdentificationId5;
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
                                    gu.IdentificationId5 = Pathes[0] + "1.jpg";
                                }
                                else
                                {
                                    System.IO.File.Copy(pictureEdit05.Tag.ToString(), @"D:\PicSol\" + textEditAccountName.Text.ToString() + "05.jpg");
                                    gu.IdentificationId5 = @"D:\PicSol\" + textEditAccountName.Text.ToString() + "05.jpg";
                                }

                            }

                        }
                        else
                        {
                            gu.IdentificationId5 = null;
                        }
                        //6

                        if (pictureEdit06.Tag != null || pictureEdit06.Tag != DBNull.Value)
                        {
                            
                            string path = null;
                            if (gu.IdentificationId6 == null)
                            {
                                path = string.Empty;
                            }
                            else
                            {
                                path = gu.IdentificationId6;
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
                                    gu.IdentificationId6 = Pathes[0] + "1.jpg";
                                }
                                else
                                {
                                    System.IO.File.Copy(pictureEdit05.Tag.ToString(), @"D:\PicSol\" + textEditAccountName.Text.ToString() + "06.jpg");
                                    gu.IdentificationId6 = @"D:\PicSol\" + textEditAccountName.Text.ToString() + "06.jpg";
                                }

                            }

                        }
                        else
                        {
                            gu.IdentificationId6 = null;
                        }




                        db.Accounts.InsertOnSubmit(gu);
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
       textEditAccountName.Text=null;
           textEditAddress.Text=null;
         textEditPhoneNum1.EditValue=null;
         textEditPhoneNum2.EditValue=null;
          textEditIdentityCard.EditValue=null;
            pictureEdit01.Image = null;
            pictureEdit02.Image = null;
            pictureEdit03.Image = null;
            pictureEdit04.Image = null;
            pictureEdit05.Image = null;
            pictureEdit06.Image = null;
            pictureEdit07.Image = null;
            pictureEdit08.Image = null;
            checkEdit1.Checked=true;
        }
        void Fill()
        {
            
            checkEdit1.Checked = true;


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

        private void GuarahtorForm_Load(object sender, EventArgs e)
        {

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

        private void pictureEdit06_EditValueChanged(object sender, EventArgs e)
        {

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

        private void pictureEdit08_Click(object sender, EventArgs e)
        {

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