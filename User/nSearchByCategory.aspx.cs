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
using Ionic.Zip;
using tool;

public partial class User_nSearchByCategory : System.Web.UI.Page
{
    DBTool dbTool = new DBTool();
    Utility Util = new Utility();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RefreshDDLCategory();
            RefreshDDLSubCategory();
        }
        
        string mode = Request.QueryString["mode"];

        if(mode =="dab")
        {
            string bookid = Request.QueryString["bookid"];
            CreateOnTheFlyZip(bookid, "");
        }
    }

    #region DropDownList
    private void RefreshDDLSubCategory()
    {
        int count = ddlCategory.Items.Count;

        if (count > 0)
        {
            string categoryid = ddlCategory.SelectedItem.Value;

            string GetCategoryInfo = "select * from mst_SubCategory where categoryid = " + categoryid;
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
        string GetCategoryInfo = "select * from mst_Category";
        DataSet ds = dbTool.ExecuteDataSet(GetCategoryInfo);
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

    private void CreateOnTheFlyZip(string bookid, string userid)
    {
        AddCounter(bookid, userid);

        string DirectoryPath = Server.MapPath(String.Format("../E-library/{0}/Pages", bookid));

        if (Directory.Exists(DirectoryPath))
        { 
             //Set ZipFile Name
            string filename = string.Format("Book_{0}-{1}{2}{3}.zip", bookid, DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), DateTime.Now.ToString("yyyy"));

            Response.Clear();
            Response.BufferOutput = false;
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "filename=" + filename);
            using (ZipFile Zip = new ZipFile())
            {
                string[] Files = Directory.GetFiles(DirectoryPath);
                Zip.AddFiles(Files, "Zip");
                Zip.Save(Response.OutputStream);
            }
            Response.Close();
        }
    }

    private void AddCounter(string bookid, string Userid)
    {
        string SQL = string.Format("INSERT INTO tb_counter(userid,bookid,load_date) VALUES ({0},{1},'{2}')",
            Userid, bookid, Util.GetToday());

        dbTool.RunCommand(SQL);
    }

}
