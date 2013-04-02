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

public partial class User_SearchByCategory : System.Web.UI.Page
{
    DBTool dbTool = new DBTool();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RefreshDDLCategory();
            RefreshDDLSubCategory();
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
}
