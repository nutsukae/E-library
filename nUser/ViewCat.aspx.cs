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

public partial class nUser_ViewCat : System.Web.UI.Page
{
    string CatId;

    protected void Page_Load(object sender, EventArgs e)
    {
        Category Cat = new Category();
        CatId = Convert.ToString(Request.QueryString["CatId"]);
        string SubCatId = Convert.ToString(Request.QueryString["SubCatId"]);
        string CatName = Cat.GetName(CatId);

        if (!IsPostBack)
        {
            SubCategory SubCat = new SubCategory();
            DataSet dsSubCat = SubCat.GetList(CatId);
            ddlSubCategory.DataSource = dsSubCat;
            ddlSubCategory.DataTextField = "name";
            ddlSubCategory.DataValueField = "subcategoryid";
            ddlSubCategory.DataBind();
        }

        lblCat.Text = CatName;
        lblCat.NavigateUrl = string.Format("ViewCat.aspx?CatId={0}&SubCatId={1}", CatId, SubCatId);
    }

    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        string LocSubCatId = ddlSubCategory.SelectedValue;

        string url = string.Format("ViewSubCat.aspx?&CatId={0}&SubCatId={1}",CatId, LocSubCatId);
        Response.Redirect(url);
    }
}
