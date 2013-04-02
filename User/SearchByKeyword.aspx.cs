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

public partial class User_SearchByKeyword : System.Web.UI.Page
{
    DBTool dbTool = new DBTool();
    Utility Util = new Utility();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtSearch.Attributes.Add("OnKeyPress","return CheckEnter(event)");
        ddlSearchBy.SelectedIndex = 3;

        string mode = Request.QueryString["mode"];

        if (mode == "dab")
        {
            string bookid = Request.QueryString["bookid"];
            CreateOnTheFlyZip(bookid, "");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        MySearchKeyword();
    }

    private void MySearchKeyword()
    {
        this.SearchKeyword.SelectParameters["SearchBy"].DefaultValue = ddlSearchBy.SelectedItem.Value == "0" ? "3" : ddlSearchBy.SelectedItem.Value;
        this.SearchKeyword.SelectParameters["keyword"].DefaultValue = txtSearch.Text;
        this.SearchKeyword.SelectParameters["SortBy"].DefaultValue = ddlSortBy.SelectedItem.Value;

        ListView1.DataSource = SearchKeyword;
        ListView1.DataBind();
    }

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
