using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using tool;
using ICSharpCode.SharpZipLib.Zip;

public partial class Admin_e_library : System.Web.UI.Page
{
    DBTool dbTool = new DBTool();
    Utility util = new Utility();
    Book book = new Book();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("~/nlogin.aspx");
            }

            txtBarCode.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            RefreshDDLCategory();
            RefreshDDLSubCategory();
            BindDDLMonth();
            BindDDLYear(1980);
            SetComponent("Enabled");

            Page.RegisterStartupScript("SetFocus", "<script>document.getElementById('" + txtBarCode.ClientID + "').focus();</script>");
        }
    }

    #region Pop Up

    protected void btnSubmitCategory_Click1(object sender, EventArgs e)
    {
        string CategoryName = txtCategory.Text;
        string CategoryRemark = txtCategoryRemark.Text;

        string ChkDupCategory = string.Format("select count(*) from mst_Category where name = '{0}'", CategoryName);
        if (dbTool.GetCountRecord(ChkDupCategory) == 0)
        {
            string InsertCategory = String.Format("INSERT INTO mst_Category(name, remark, status)"
                           + " VALUES ('{0}','{1}','1') ", CategoryName, CategoryRemark);
            
            if (dbTool.RunCommand(InsertCategory))
            {
                Response.Write("<script>alert('เพิ่ม Category เรียบร้อย');</script>");
             
                RefreshDDLCategory();
             
                string GetLastestCategotyID = "SELECT TOP 1 categoryid FROM mst_category ORDER BY categoryid DESC";
                string LastestCategotyID = dbTool.GetOneString(GetLastestCategotyID);

                ddlCategory.SelectedValue  = LastestCategotyID;

                RefreshDDLSubCategory();

                txtCategory.Text = "";
                txtCategoryRemark.Text = "";
            }
            else
            {
                Response.Write("<script>alert('พบปัญหาเพิ่ม Category ไม่สำเร็จ');</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('พบปัญหาเพิ่ม Category ไม่สำเร็จ : เนื่องจากชื่อ Category ซ้ำ ');</script>");
        }
    }

    protected void PanelSubCategory_Load(object sender, EventArgs e)
    {
        lblCategory.Text = ddlCategory.SelectedItem.Text;
    }

    protected void btnSubmitSubCategory_Click1(object sender, EventArgs e)
    {
        string SubCategoryName = txtSubCategory.Text;
        string SubCategoryRemark = txtSubCategoryRemark.Text;
        string Categoryid = ddlCategory.SelectedValue;

        string ChkDupSubCategory = string.Format("select count(*) from mst_SubCategory where name = '{0}' and categoryid = '{1}'", SubCategoryName, Categoryid);

        if (dbTool.GetCountRecord(ChkDupSubCategory) == 0)
        {
            string InsertSubCategory = String.Format("INSERT INTO mst_SubCategory(categoryid, name, remark, status)"
                        + " VALUES ('{0}','{1}','{2}','1') ", Categoryid, SubCategoryName, SubCategoryRemark);
            if (dbTool.RunCommand(InsertSubCategory))
            {
                Response.Write("<script>alert('เพิ่ม SubCategory เรียบร้อย');</script>");

                RefreshDDLSubCategory();

                string GetLastestSubCategotyID = "SELECT TOP 1 subcategoryid FROM mst_SubCategory ORDER BY subcategoryid DESC";
                string LastestSubCategotyID = dbTool.GetOneString(GetLastestSubCategotyID);

                ddlSubCategory.SelectedValue = LastestSubCategotyID;
            }
            else
            {
                Response.Write("<script>alert('พบปัญหาเพิ่ม SubCategory ไม่สำเร็จ');</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('พบปัญหาเพิ่ม SubCategory ไม่สำเร็จ เนื่องจากซื่อ SubCategory ซ้ำ');</script>");
        }
    }

    protected void btnSubmitAuthor_Click1(object sender, EventArgs e)
    {
        string AuthorName = txtAddAuthor.Text;
        string AuthorRemark = txtAuthorRemark.Text;

        string ChkDupAuthor = string.Format("select count(*) from mst_author where name = '{0}'", AuthorName);

        if (dbTool.GetCountRecord(ChkDupAuthor) == 0)
        {
            string InsertAuthor = string.Format("insert into mst_author(name,remark,status)"
                       + " values ('{0}','{1}','1')", AuthorName, AuthorRemark);

            if (dbTool.RunCommand(InsertAuthor))
            {
                Response.Write("<script>alert('เพิ่ม ผู้แต่ง เรียบร้อย');</script>");
                txtAuthor.Text = AuthorName;
            }
            else
            {
                Response.Write("<script>alert('พบปัญหาเพิ่ม ผู้แต่ง ไม่สำเร็จ');</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('พบปัญหาเพิ่ม ผู้แต่ง ไม่สำเร็จ เนื่องจาก ชื่อผู้แต่งซ้ำ');</script>");
        }
    }

    protected void btnSubmitPublisher_Click1(object sender, EventArgs e)
    {
        string PublisherName = txtAddPublisher.Text;
        string PublisherRemark = txtPublisherRemark.Text;

        string ChkDupPublisher = string.Format("select count(*) from mst_publisher where name = '{0}'", PublisherName);

        if (dbTool.GetCountRecord(ChkDupPublisher) == 0)
        {
            string InsertPublisher = string.Format("insert into mst_publisher(name,remark,status)"
                        + " values ('{0}','{1}','1')", PublisherName, PublisherRemark);

            if (dbTool.RunCommand(InsertPublisher))
            {
                Response.Write("<script>alert('เพิ่ม สำนักพิมพ์ เรียบร้อย');</script>");
                txtPublisher.Text = PublisherName;
            }
            else
            {
                Response.Write("<script>alert('พบปัญหาเพิ่ม สำนักพิมพ์ ไม่สำเร็จ');</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('พบปัญหาเพิ่ม ผู้แต่ง ไม่สำเร็จ เนื่องจาก ชื่อสำนักพิมพ์ซ้ำ');</script>");
        }
    }
        
    #endregion

    #region DropDownList

    private void BindDDLMonth()
    {
        DataTable dt = util.GenerateMonth();

        ddlPrintMonth.DataSource = dt;
        ddlPrintMonth.DataBind();
    }

    private void BindDDLYear(int year)
    {
        DataTable dt = util.GenerateYear(year);

        ddlPrintYear.DataSource = dt;
        ddlPrintYear.DataBind();
    }

    private DataSet BindDDLCategory()
    {
        string GetCategoryInfo = "SELECT * FROM mst_category WHERE status = 1";
        DataSet ds = dbTool.ExecuteDataSet(GetCategoryInfo);
        return ds;
    }

    private void RefreshDDLSubCategory()
    {
        int count = ddlCategory.Items.Count;

        if (count > 0)
        {
            string categoryid = ddlCategory.SelectedItem.Value;

            string GetCategoryInfo = "select * from mst_SubCategory where categoryid = " + categoryid + " and status = 1";
            DataSet ds = dbTool.ExecuteDataSet(GetCategoryInfo);
            DataTable dt = ds.Tables[0];

            ddlSubCategory.DataSource = dt;
            ddlSubCategory.DataValueField = "SubCategoryId";
            ddlSubCategory.DataTextField = "name";
            ddlSubCategory.DataBind();
        }
    }

    private void RefreshDDLCategory()
    {
        DataSet ds = BindDDLCategory();
        DataTable dt = ds.Tables[0];

        ddlCategory.DataSource = dt;
        ddlCategory.DataValueField = "CategoryId";
        ddlCategory.DataTextField = "name";
        ddlCategory.DataBind();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshDDLSubCategory();
    }
    #endregion

    #region AutoComplete
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetAuthorList(string prefixText, int count, string contextKey)
    {
        string GetAuthorList = "select  name from mst_author where name like '" +prefixText+"%'";
        DataSet ds = new DBTool().ExecuteDataSet(GetAuthorList);
        DataTable dt = ds.Tables[0];

        int rows = dt.Rows.Count;
        string[] author = new string[rows];

        for (int i = 0; i < rows; i++)
        {
            author[i] = Convert.ToString(dt.Rows[i][0]);
        }

        return author;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetPublisherList(string prefixText, int count, string contextKey)
    {
        string GetPublisherList = "select  name from mst_publisher where name like '" + prefixText + "%'";
        DataSet ds = new DBTool().ExecuteDataSet(GetPublisherList);
        DataTable dt = ds.Tables[0];

        int rows = dt.Rows.Count;
        string[] publisher = new string[rows];

        for (int i = 0; i < rows; i++)
        {
            publisher[i] = Convert.ToString(dt.Rows[i][0]);
        }

        return publisher;
    }
    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string isbn = util.RemoveDash(txtISBN.Text);
        string barcode = txtBarCode.Text;

        string SubCategoryId = "";
        if (ddlSubCategory.SelectedValue == "")
        {
            SubCategoryId = "0";
        }
        else
        {
            SubCategoryId = ddlSubCategory.SelectedValue;
        }
        
        string Name = txtName.Text;

        string description = util.HandleQuote(txtDescription.Text);

        string GetAuthorId = "select Authorid from mst_author where name = '" + txtAuthor.Text + "'";
        string GetPublisherId = "select publisherid from mst_publisher where name = '" + txtPublisher.Text + "'";

        string AuthorId = dbTool.GetOneString(GetAuthorId);
        string PublisherId = dbTool.GetOneString(GetPublisherId);
        string PrintMonth = ddlPrintMonth.SelectedValue;
        string PrintYear = ddlPrintYear.SelectedValue;
        string Img = "";
        string Zip = "";
        string Pdf = "";
        string bookid = "";
        string Remark = util.HandleQuote(txtRemark.Text);
        string RecUser = Convert.ToString(Session["userid"]);
        string RecDate = util.GetToday();

        string DirectoryPath = "";

        string CheckDuplicateBook = "SELECT COUNT(*) FROM tb_book WHERE name = '" + Name + "' AND barcode = '" + barcode + "'";

        int DuplicateCount = dbTool.GetCountRecord(CheckDuplicateBook);

        if (DuplicateCount > 0)
        {
            Response.Write("<script>alert('พบปัญหา หนังสือเล่มนี้มีอยู่แล้วในระบบครับ');</script>");
            
        }
        else
        {
            string InsertBook = string.Format("Insert into tb_book (isbn ,barcode ,SubCategoryId"
                    + " ,name, description, authorId, publisherId, print_month, print_year"
                    + " ,remark, rec_user, rec_date)"
                    + " values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}'"
                    + " ,'{11}')",
                isbn, barcode, SubCategoryId, Name, description, AuthorId, PublisherId,
                PrintMonth, PrintYear, Remark, RecUser, RecDate);

            if (dbTool.RunCommand(InsertBook))
            {
                bookid = book.GetLastestBookId(); 

                if (PictureUploader.HasFile)
                {
                    string extension = Path.GetExtension(PictureUploader.PostedFile.FileName);

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".gif" || extension.ToLower() == ".png")
                    {
                        DirectoryPath = Server.MapPath(String.Format("~/E-Library/{0}/", bookid));
                        string ImgName = String.Format("Cover{0}", extension);
                        string ImgPath = String.Format("{0}{1}", DirectoryPath, ImgName);

                        if (!Directory.Exists(DirectoryPath))
                        {
                            Directory.CreateDirectory(DirectoryPath);
                        }

                        Bitmap OriBMP = new Bitmap(PictureUploader.FileContent);

                        int ratio = OriBMP.Width / OriBMP.Height;

                        if (ratio == 0)
                        {
                            ratio = 1;
                        }

                        int newWidth = 150;
                        int newHeight = newWidth / ratio;

                        Bitmap NewBMP = new Bitmap(OriBMP, newWidth, newHeight);

                        Graphics oGraphic = Graphics.FromImage(NewBMP);

                        oGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        oGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                        oGraphic.DrawImage(NewBMP, 0, 0, newWidth, newHeight);

                        GC.Collect();

                        NewBMP.Save(ImgPath);
                        
                        oGraphic.Dispose();
                        NewBMP.Dispose();
                        OriBMP.Dispose();

                        Img = ImgName;
                        //Response.Write("<script>alert('อัพโหลดรูปภาพเรียบร้อยแล้ว');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('พบปัญหา ไม่สามารถอัพโหลดรูปภาพได้');</script>");
                        return;
                    }
                }

                if (ZipUploader.HasFile)
                {
                    string extension = Path.GetExtension(ZipUploader.PostedFile.FileName);

                    if (extension.ToLower() == ".zip")
                    {
                        DirectoryPath = Server.MapPath(String.Format("~/E-Library/{0}/", bookid));
                        string ZipName = String.Format("Image_Zip{0}", extension);
                        string ZipPath = String.Format("{0}{1}", DirectoryPath, ZipName);

                        if (!Directory.Exists(DirectoryPath))
                        {
                            Directory.CreateDirectory(DirectoryPath);
                        }

                        ZipUploader.SaveAs(ZipPath);

                        Zip = ZipName;
                        //Response.Write("<script>alert('อัพโหลด Zip File เรียบร้อยแล้ว');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('พบปัญหา ไม่สามารถอัพโหลด Zip File ได้');</script>");
                        return;
                    }
                }

                if (PDFUploader.HasFile)
                {
                    string extension = Path.GetExtension(PDFUploader.PostedFile.FileName);
                    
                    if (extension.ToLower() == ".pdf")
                    {
                        DirectoryPath = Server.MapPath(String.Format("~/E-Library/{0}/", bookid));
                        string pdfName = "Book.pdf";
                        string pdfPath = String.Format("{0}{1}", DirectoryPath, pdfName);

                        if (!Directory.Exists(DirectoryPath))
                        {
                            Directory.CreateDirectory(DirectoryPath);
                        }

                        ZipUploader.SaveAs(pdfPath);

                        Pdf  = pdfName;
                    }
                    else
                    {
                        Response.Write("<script>alert('พบปัญหา ไม่สามารถอัพโหลด Zip File ได้');</script>");
                        return;
                    }
                }

                string UpdateFilePath = string.Format("update tb_book set image = '{0}', zip = '{1}', pdf = '{2}' where bookid = '{3}'",
                 Img, Zip, Pdf,bookid);

                if (dbTool.RunCommand(UpdateFilePath))
                {
                    Response.Write("<script>alert('เพิ่ม หนังสือ เรียบร้อย');</script>");
                    if (ZipUploader.HasFile)
                    {
                        UnZip(bookid, DirectoryPath, Zip);
                    }
                    Reset();
                }
                else
                {
                    Response.Write("<script>alert('พบปัญหาเพิ่ม หนังสือ ไม่สำเร็จ');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('พบปัญหาเพิ่ม หนังสือ ไม่สำเร็จ ');</script>");
            }
        }
    }

    private void UnZip(string bookid, string DirectoryPath, string ZipName)
    {
        //Get Zip Path
        string ZipFile = DirectoryPath + ZipName;
        
        //Create Zip Obj
        FastZip MyZip = new FastZip();

        //Create Extract File Folder
        string ExDirectory = string.Format("{0}Pages", DirectoryPath);
               
        if (Directory.Exists(ExDirectory))
        {
            Directory.Delete(ExDirectory);
        }
        else
        {
            Directory.CreateDirectory(ExDirectory);
        }

        //Extract File to New Folder
        MyZip.ExtractZip(ZipFile, ExDirectory, "");

        //Remove Zip File
        File.Delete(ZipFile);

        //Create Images Index
        CreateImageIndex(ExDirectory, bookid);
    }

    private void CreateImageIndex(string PagesFolderPath, string bookid)
    {
        //Get All File in Directory
        string[] Files = Directory.GetFiles(PagesFolderPath);

        foreach (string file in Files)
        {
            //Get File Extension
            string Extension = file.Substring(file.LastIndexOf("."));

            if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".gif" || Extension == ".png")
            { 
                //Index data to tb_book_pages
                string CreateIndex = String.Format("INSERT INTO tb_book_pages(bookid, filename) VALUES ({0},'{1}')",bookid, Path.GetFileName(file));
                dbTool.RunCommand(CreateIndex);
            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }

    private void Reset()
    {
        txtAuthor.Text = "";
        txtBarCode.Text = "";
        txtISBN.Text = "";
        txtPublisher.Text = "";
        txtRemark.Text = "";
        txtName.Text = "";
        txtDescription.Text = "";
    }

    //protected void txtISBN_TextChanged(object sender, EventArgs e)
    //{
    //    int length = util.RemoveDash(txtISBN.Text).Length ;

    //    //ถ้าความยาวของ ISBN ไม่เท่ากับ 0 หรือ 10 หรือ 13 จะ SetComponent เป็น Disabled
    //    if (length == 0 || length == 10 || length == 13)
    //    {
    //        SetComponent("Enabled");
    //    }
    //    else
    //    {
    //        SetComponent("Disabled"); 
    //    }
    //}

    private void SetComponent(string Option)
    {
        if (Option == "Enabled")
        {
            txtBarCode.Enabled = true;
            ddlCategory.Enabled = true;
            btnAddCategory.Enabled = true;
            ddlSubCategory.Enabled = true;
            btnSubCategory.Enabled = true;
            txtName.Enabled = true;
            txtDescription.Enabled = true;
            txtAuthor.Enabled = true;
            btnAddAuthor.Enabled = true;
            txtPublisher.Enabled = true;
            btnAddPublisher.Enabled = true;
            ddlPrintMonth.Enabled = true;
            ddlPrintYear.Enabled = true;
            txtRemark.Enabled = true;
            btnSubmit.Enabled = true;
            btnReset.Enabled = true;
        }
        else if (Option == "Disabled")
        {
            txtBarCode.Enabled = false;
            ddlCategory.Enabled = false;
            btnAddCategory.Enabled = false;
            ddlSubCategory.Enabled = false;
            btnSubCategory.Enabled = false;
            txtName.Enabled = false;
            txtDescription.Enabled = false;
            txtAuthor.Enabled = false;
            btnAddAuthor.Enabled = false;
            txtPublisher.Enabled = false;
            btnAddPublisher.Enabled = false;
            ddlPrintMonth.Enabled = false;
            ddlPrintYear.Enabled = false;
            txtRemark.Enabled = false;
            btnSubmit.Enabled = false;
            btnReset.Enabled = false;
        }
    }

   
}
