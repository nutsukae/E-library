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
using System.Drawing;
using System.IO;
using tool;
using ICSharpCode.SharpZipLib.Zip;

public partial class Admin_editBook : System.Web.UI.Page
{
    Book book = new Book();
    Utility util = new Utility();
    DBTool dbTool = new DBTool();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("~/nlogin.aspx");
            }

            RefreshDDLCategory();
            RefreshDDLSubCategory();
            BindDDLMonth();
            BindDDLYear(1980);
            txtBarCode.Attributes.Add("onkeydown", "return (event.keyCode!=13);");

            string bookid = Request.QueryString["bookid"];
            if (bookid != null)
            {
                hidBookId.Value = bookid;
                LoadBookDetail(bookid);
            }
        }
    }

    private void LoadBookDetail(string bookid)
    {
        DataSet ds = book.GetBookDetailByID(bookid);
        DataTable dt = ds.Tables[0];

        txtISBN.Text = Convert.ToString(dt.Rows[0]["isbn"]);
        txtBarCode.Text = Convert.ToString(dt.Rows[0]["barcode"]);
        txtName.Text = Convert.ToString(dt.Rows[0]["name"]);
        txtDescription.Text = Convert.ToString(dt.Rows[0]["description"]);
        txtRemark.Text = Convert.ToString(dt.Rows[0]["remark"]);
        txtAuthor.Text = Convert.ToString(dt.Rows[0]["Author_name"]);
        txtPublisher.Text = Convert.ToString(dt.Rows[0]["Publisher_name"]);
                
        ddlCategory.SelectedValue = Convert.ToString(dt.Rows[0]["categoryid"]);
        RefreshDDLSubCategory();
        ddlSubCategory.SelectedValue = Convert.ToString(dt.Rows[0]["subcategoryid"]);
        ddlPrintMonth.SelectedValue = Convert.ToString(dt.Rows[0]["print_month"]);
        ddlPrintYear.SelectedValue = Convert.ToString(dt.Rows[0]["print_year"]);

        string coverPath = Convert.ToString(dt.Rows[0]["image"]);
        string zipPath = Convert.ToString(dt.Rows[0]["zip"]);
        string pdfPath = Convert.ToString(dt.Rows[0]["pdf"]);

        hidImgPath.Value = coverPath;
        hidZipPath.Value = zipPath;
        hidPdfPath.Value = pdfPath;

        if (coverPath != "")
        {
            CoverImage.ImageUrl = String.Format("~/E-Library/{0}/{1}", bookid, coverPath);
        }
        else
        {
            CoverImage.ImageUrl = "~/E-Library/no_cover.jpg";
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

                ddlCategory.SelectedValue = LastestCategotyID;

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
            Response.Write("<script>alert('พบปัญหาเพิ่ม ผู้แต่ง ไม่สำเร็จ เนื่องจาก ชื่อโรงพิมพ์ซ้ำ');</script>");
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
        string GetAuthorList = "select  name from mst_author where name like '" + prefixText + "%'";
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
       
    private void Reset()
    {
        txtName.Text = "";
        txtDescription.Text = "";
        txtAuthor.Text = "";
        txtPublisher.Text = "";
        txtRemark.Text = "";
    }

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        string bookid = hidBookId.Value;
        string SubCategoryId = ddlSubCategory.SelectedItem.Value;
        string Name = util.HandleQuote(txtName.Text);
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
        string Remark = util.HandleQuote(txtRemark.Text);
        string RecUser = Convert.ToString(Session["userid"]);
        string RecDate = util.GetToday();
        string DirectoryPath = "";

        if (PictureUploader.HasFile)
        {
            string extension = Path.GetExtension(PictureUploader.PostedFile.FileName);

            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".gif" || extension.ToLower() == ".png")
            {
                //If have old file. Remove before save new one
                if (hidImgPath.Value != "")
                {
                    File.Delete(Server.MapPath(String.Format("~/E-Library/{0}/{1}", bookid, hidImgPath.Value)));
                }

                string ImgDirectoryPath = Server.MapPath(String.Format("~/E-Library/{0}/", bookid));
                string ImgName = String.Format("Cover{0}", extension);
                string ImgPath = String.Format("{0}{1}", ImgDirectoryPath, ImgName);

                if (!Directory.Exists(ImgDirectoryPath))
                {
                    Directory.CreateDirectory(ImgDirectoryPath);
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
                Response.Write("<script>alert('พบปัญหา ประเภทไฟล์รูปภาพไม่รองรับ');</script>");
                return;
            }
        }

        if (ZipUploader.HasFile)
        {
            string extension = Path.GetExtension(ZipUploader.PostedFile.FileName);

            if (extension.ToLower() == ".zip")
            {
                //If have old file. Remove before save new one
                if (hidZipPath.Value != "")
                {
                    File.Delete(Server.MapPath(String.Format("~/E-Library/{0}/{1}", bookid, hidZipPath.Value)));
                }

                string ZipDirectoryPath = Server.MapPath(string.Format("~/E-Library/{0}/", bookid));
                string ZipName = String.Format("Image_Zip{0}", extension);
                string ZipPath = String.Format("{0}{1}", ZipDirectoryPath, ZipName);

                DirectoryPath = ZipDirectoryPath;

                if (!Directory.Exists(ZipDirectoryPath))
                {
                    Directory.CreateDirectory(ZipDirectoryPath);
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

        if (pdfUploader.HasFile)
        {
            string extension = Path.GetExtension(ZipUploader.PostedFile.FileName);

            if (extension.ToLower() == ".pdf")
            {
                //If have old file. Remove before save new one
                if (hidPdfPath.Value != "")
                {
                    File.Delete(Server.MapPath(String.Format("~/E-Library/{0}/{1}", bookid, hidPdfPath.Value)));
                }

                string PDFDirectoryPath = Server.MapPath(String.Format("~/E-Library/{0}/", bookid));
                string pdfName = "Book.pdf";
                string pdfPath = String.Format("{0}{1}", PDFDirectoryPath, pdfName);

                if (!Directory.Exists(PDFDirectoryPath))
                {
                    Directory.CreateDirectory(PDFDirectoryPath);
                }

                ZipUploader.SaveAs(pdfPath);

                Pdf = pdfName;
                //Response.Write("<script>alert('อัพโหลด Zip File เรียบร้อยแล้ว');</script>");
            }
            else
            {
                Response.Write("<script>alert('พบปัญหา ไม่สามารถอัพโหลด PDF File ได้');</script>");
                return;
            }
        }

        string sql = "UPDATE tb_book SET"
                + " SubCategoryid = '" + SubCategoryId + "'"
                + " ,name = '" + Name + "'"
                + " ,description = '" + description + "'"
                + " ,AuthorId = '" + AuthorId + "'"
                + " ,PublisherId = '" + PublisherId + "'"
                + " ,Remark = '" + Remark + "'";
        if (PictureUploader.HasFile)
        {
            sql += " ,Image = '" + Img + "'";
        }
        if (ZipUploader.HasFile)
        {
            sql += " ,zip = '" + Zip + "'";
        }
        if (pdfUploader.HasFile)
        {
            sql += " ,pdf = '" + Pdf  + "'";
        }

        sql += " WHERE bookid = '" + bookid + "'";

        //txtRemark.Text = sql;

        if (dbTool.RunCommand(sql))
        {
            Response.Write("<script>alert('แก้ไข หนังสือ เรียบร้อย');</script>");
            if (ZipUploader.HasFile)
            { 
                UnZip(bookid, DirectoryPath, Zip);
            }
            Reset();
            LoadBookDetail(bookid);
        }
        else
        {
            Response.Write("<script>alert('พบปัญหาแก้ไขหนังสือ ไม่สำเร็จ');</script>");
        }
        
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
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
            Directory.Delete(ExDirectory,true);
            string SQL = string.Format("DELETE FROM tb_book_pages WHERE bookid = {0}",bookid);
            try
            {
                dbTool.RunCommand(SQL);
            }
            catch (Exception) { }
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
                string CreateIndex = String.Format("INSERT INTO tb_book_pages(bookid, filename) VALUES ({0},'{1}')", bookid, Path.GetFileName(file));
                dbTool.RunCommand(CreateIndex);
            }
        }
    }
}
