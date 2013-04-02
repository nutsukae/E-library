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
using System.IO;
using tool;

public partial class nUser_ViewBook : System.Web.UI.Page
{
    
    Book book = new Book();
    ArrayList SelectedFile = new ArrayList();
    bool hasPdf;

    protected void Page_Load(object sender, EventArgs e)
    {
        string bookid="";
        if (!IsPostBack)
        {
            bookid = Convert.ToString(Request.QueryString["bookid"]);
            Session["bookid"] = bookid;
            string command = Request.QueryString["command"];

            if (command == "0")
            {
                string tmp = Request.QueryString["filename"];
                string filename = tmp.Replace("xyx", "&");
                string ImageURL = Server.MapPath(String.Format("~/E-library/{0}/pages/{1}", bookid, filename));
                //Response.Write(filename);
                //Response.Write(ImageURL);

                FileInfo file = new FileInfo(ImageURL);
                if (!file.Exists)
                {
                    Response.Write("<script>alert('พบปัญหา ไม่พบรูปภาพที่ต้องการดาว์นโหลด');</script>");
                    return;
                }

                //Save file as dialog work Only JPEG File
                Response.ContentType = "image/jpeg";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.TransmitFile(ImageURL);
                Response.End();
            }
        }
        else
        {
            string userid = Convert.ToString(Session["userid"]);
            string command = Request.QueryString["command"];
            if (command == "1")
            {
                bookid = Convert.ToString(Session["bookid"]);
                UpdateSelection();
                CreateOnTheFlyZip(SelectedFile, bookid, userid);
            }
            else if (command == "2")
            {
                bookid = Convert.ToString(Session["bookid"]);
                string URL = Server.MapPath(String.Format("~/E-library/{0}/book.pdf", bookid));

                FileInfo file = new FileInfo(URL);
                if (!file.Exists)
                {
                    Response.Write("<script>alert('พบปัญหา ไม่พบหนังสือที่ต้องการดาว์นโหลด');</script>");
                    return;
                }

                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=book.pdf" );
	            Response.AddHeader("Content-Length", file.Length.ToString());
	            Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End(); 
            }
        }

        if (bookid != "")
        {
            DataSet ds = book.GetBookDetailByID(bookid);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count != 1)
            {
                return;
            }
            else
            {
                string CatId = dt.Rows[0]["categoryid"].ToString();
                string CatName = dt.Rows[0]["category_name"].ToString();
                string SubCatId = dt.Rows[0]["subcategoryid"].ToString();
                string SubCatName = dt.Rows[0]["subcategory_name"].ToString();
                string BookName = dt.Rows[0]["name"].ToString();
                string pdf = dt.Rows[0]["pdf"].ToString();

                lblCat.Text = CatName;
                lblCat.NavigateUrl = string.Format("ViewCat.aspx?CatId={0}&CatName={1}&SubCatId={2}", CatId, CatName, SubCatId);

                lblSubCat.Text = SubCatName;
                lblSubCat.NavigateUrl = string.Format("ViewSubCat.aspx?SubCatId={0}&SubCatName={1}&CatId={2}&CatName={3}", SubCatId, SubCatName, CatId, CatName);

                lblBookName.Text = BookName;
                lblBookName.NavigateUrl = string.Format("ViewBook.aspx?Bookid={0}", bookid);

                if (pdf == null || pdf == "")
                {
                    hasPdf = false;
                }
                else
                {
                    hasPdf = true;
                }
            }
            LoadSideBannerByCategory(bookid);
        }
    }

    #region Banner
    private void LoadSideBannerByCategory(string BookId)
    {
        string CategoryId = book.GetSubcategoryByID(BookId);

        DataSet ds = new DataSet();
        ds = book.GetBookListByCategory_NG(CategoryId);

        DataTable dt = new DataTable();
        dt = ds.Tables[0];

        int RowCount = dt.Rows.Count;

        if (RowCount > 0)
        {
            BookPanel.Controls.Add(new LiteralControl("<ul id=\"mycarousel\" class=\"jcarousel jcarousel-skin-tango\">"));
            for (int i = 0; i < RowCount; i++)
            {
                string control = string.Format("<li><a href=\"ViewBook.aspx?bookid={0}\">"
                    + "<img src=\"../E-Library/{1}\" width=\"150\" height=\"150\" alt=\"\" /></li>"
                        , Convert.ToString(dt.Rows[i]["bookid"]), Convert.ToString(dt.Rows[i]["imgpath"]));
                BookPanel.Controls.Add(new LiteralControl(control));
            }
            BookPanel.Controls.Add(new LiteralControl("</ul>"));
        }
    }
    #endregion

    #region SELECTED FIlE
    protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateSelection();
    }

    protected void ListView1_PreRender(object sender, EventArgs e)
    {
        
        
        ShowSelected();
    }

    private void UpdateSelection()
    {
        CheckBox cbxSelectFile;

        if (ViewState["Selection"] != null)
        {
            SelectedFile = (ArrayList)ViewState["Selection"];
        }

        for (int i = 0; i < ListView1.Items.Count; i++)
        {
            cbxSelectFile = (CheckBox)ListView1.Items[i].FindControl("cbxSelectFile");
            if (cbxSelectFile.Checked)
            {
                string filename = Convert.ToString(cbxSelectFile.Attributes["key"]);

                if (SelectedFile.IndexOf(filename) < 0)
                {
                    SelectedFile.Add(filename);
                }
            }
            else
            {
                string filename = Convert.ToString(cbxSelectFile.Attributes["key"]);

                if (SelectedFile.IndexOf(filename) >= 0)
                {
                    SelectedFile.Remove(filename);
                }
            }
        }

        if (SelectedFile.Count > 0)
        {
            ViewState["Selection"] = SelectedFile;
        }
    }

    private void ShowSelected()
    {
        CheckBox cbxSelectFile;

        if (ViewState["Selection"] != null)
        {
            SelectedFile = (ArrayList)ViewState["Selection"];
        }

        for (int i = 0; i < ListView1.Items.Count; i++)
        {
            cbxSelectFile = (CheckBox)ListView1.Items[i].FindControl("cbxSelectFile");

            string filename = Convert.ToString(cbxSelectFile.Attributes["key"]);

            if (SelectedFile.IndexOf(filename) >= 0)
            {
                cbxSelectFile.Checked = true;
            }
        }
    }
    #endregion

    #region Download
    private void CreateOnTheFlyZip(ArrayList SelectedFile, string bookid, string userid)
    {
        AddCounter(bookid, userid);

        string[] SelectedFileArray = new string[SelectedFile.Count];

        for (int i = 0; i < SelectedFile.Count; i++)
        {
            string filePath = Server.MapPath(String.Format("../E-library/{0}/Pages/{1}", bookid, SelectedFile[i]));
            SelectedFileArray[i] = filePath;
        }

        Response.Clear();
        Response.BufferOutput = false;

        string filename = string.Format("Book_{0}-{1}{2}{3}.zip", bookid, DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), DateTime.Now.ToString("yyyy"));

        Response.ContentType = "application/zip";
        Response.AddHeader("content-disposition", "filename=" + filename);
        using (Ionic.Zip.ZipFile Zip = new Ionic.Zip.ZipFile())
        {
            Zip.AddFiles(SelectedFileArray, "files");

            Zip.Save(Response.OutputStream);
        }
        Response.Close();
    }

    private void AddCounter(string bookid, string Userid)
    {
        DBTool dbTool = new DBTool();
        Utility Util = new Utility();

        string SQL = string.Format("INSERT INTO tb_counter(userid,bookid,load_date) VALUES ({0},{1},'{2}')",
            Userid, bookid, Util.GetToday());

        dbTool.RunCommand(SQL);
    }
    #endregion

    protected void ListView1_LayoutCreated(object sender, EventArgs e)
    {
        if (!hasPdf)
        {
            LinkButton btnLink = new LinkButton();
            btnLink = (LinkButton)ListView1.FindControl("btnDownloadAll");
            btnLink.Visible = false;
        }
    }
}
