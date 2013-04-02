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
using tool;

public partial class Admin_Book_Management : System.Web.UI.Page
{
    DBTool dbTool = new DBTool();
    Utility util = new Utility();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("~/nlogin.aspx");
            }

            BindDDLCategory();
            BindDDLSubcategory();
            LoadBook();
        }
    }

    private void LoadBook()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("bookid");
        dt.Columns.Add("no");
        dt.Columns.Add("isbn");
        dt.Columns.Add("book_name");
        dt.Columns.Add("image");
        dt.Columns.Add("zip");

        Book book = new Book();
        DataSet dsBook = book.ListBooks();

        if (dsBook.Tables[0] != null)
        {
            DataTable dtBook = dsBook.Tables[0];

            for (int i = 0; i < dtBook.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();

                dr["bookid"] = Convert.ToString(dtBook.Rows[i]["bookid"]);
                dr["no"] = (i + 1);
                dr["isbn"] = Convert.ToString(dtBook.Rows[i]["isbn"]);
                dr["book_name"] = Convert.ToString(dtBook.Rows[i]["name"]);

                string img = Convert.ToString(dtBook.Rows[i]["image"]);
                string pdf = Convert.ToString(dtBook.Rows[i]["pdf"]);
                string zip = Convert.ToString(dtBook.Rows[i]["zip"]);

                if (img != "")
                {
                    dr["image"] = "~/Image/Icon/result_apply.png";
                }
                else
                {
                    dr["image"] = "~/Image/Icon/result_error.png";
                }

                if (zip != "")
                {
                    dr["zip"] = "~/Image/Icon/result_apply.png";
                }
                else
                {
                    dr["zip"] = "~/Image/Icon/result_error.png";
                }

                dt.Rows.Add(dr);
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;

        if (radSearchByKeyword.Checked == true)
        {
            string Keyword = txtKeyword.Text;

            ViewState["search"] = "kw";
            ViewState["key"] = Keyword;

            ListBookByKeyword(Keyword);
           
        }
        else if (radSearchByCategory.Checked == true)
        {
            string subcategoryid = ddlSubCategory.SelectedItem.Value;

            ViewState["search"] = "cat";
            ViewState["key"] = subcategoryid;

            ListBookByCategory(subcategoryid);
        }
    }
    
    #region GridView
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string bookid = Convert.ToString(GridView1.DataKeys[e.RowIndex]["bookid"]);
        
        string sql = "DELETE FROM tb_book WHERE bookid = " + bookid;

        if (dbTool.RunCommand(sql))
        {
            Response.Write("<script>alert('ลบ หนังสือ เรียบร้อย');</script>");
        }
        else
        {
            Response.Write("<script>alert('ไม่สามารถ ลบ หนังสือได้');</script>");
        }
        LoadBook();
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        string bookid = Convert.ToString(GridView1.DataKeys[e.NewSelectedIndex]["bookid"]);

        string url = string.Format("editBook.aspx?bookid=" + bookid);
        Response.Redirect(url);
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

        string SearchBy = Convert.ToString(ViewState["search"]);
        string keyword = Convert.ToString(ViewState["key"]);

        if (SearchBy == "" || SearchBy == null)
        {
            LoadBook();
        }
        else if (SearchBy == "kw")
        {
            ListBookByKeyword(keyword);
        }
        else if (SearchBy == "cat")
        {
            ListBookByCategory(keyword);
        }
    }
    #endregion

    #region DropDownList
    private void BindDDLCategory()
    {
        Category Cateogory = new Category();
        DataSet ds = new DataSet();

        ds = Cateogory.GetList();

        ddlCategory.DataSource = ds;
        ddlCategory.DataValueField = "categoryid";
        ddlCategory.DataTextField = "name";
        ddlCategory.DataBind();
    }

    private void BindDDLSubcategory()
    {
        SubCategory Subcateogory = new SubCategory();
        DataSet ds = new DataSet();

        string categoryid = ddlCategory.SelectedItem.Value;

        ds = Subcateogory.GetList(categoryid);

        ddlSubCategory.DataSource = ds;
        ddlSubCategory.DataValueField = "subcategoryid";
        ddlSubCategory.DataTextField = "name";
        ddlSubCategory.DataBind();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        string categoryid = ddlCategory.SelectedItem.Value;
        BindDDLSubcategory();
    }
    #endregion

    private void ListBookByKeyword(string keyword)
    {
        Book book = new Book();

        DataSet ds = book.GetBookListByKeyword_MB(keyword);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    private void ListBookByCategory(string subcategoryid)
    {
        Book book = new Book();

        DataSet ds = book.GetBookListByCategory_MB(subcategoryid);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    
} 
