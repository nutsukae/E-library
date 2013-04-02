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

public partial class nUser_ViewSubCat : System.Web.UI.Page
{
    string CatId;
    string SubCatId;

    protected void Page_Load(object sender, EventArgs e)
    {
        Category Cat = new Category();
        SubCategory SubCat = new SubCategory();

        CatId = Convert.ToString(Request.QueryString["CatId"]);
        string CatName = Cat.GetName(CatId);
        SubCatId = Convert.ToString(Request.QueryString["SubCatId"]);
        string SubCatName = SubCat.GetName(SubCatId);

        if (!IsPostBack)
        {
            
            DataSet dsSubCat = SubCat.GetList(CatId);
            ddlSubCategory.DataSource = dsSubCat;
            ddlSubCategory.DataTextField = "name";
            ddlSubCategory.DataValueField = "subcategoryid";
            ddlSubCategory.DataBind();
        }

        lblCat.Text = CatName;
        lblCat.NavigateUrl = string.Format("ViewCat.aspx?CatId={0}&CatName={1}&SubCatId={2}", CatId, CatName, SubCatId);

        lblSubCat.Text = SubCatName;
        lblSubCat.NavigateUrl = string.Format("ViewSubCat.aspx?SubCatId={0}&SubCatName={1}&CatId={2}&CatName={3}", SubCatId, SubCatName, CatId, CatName);
     
    }

    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        string LocSubCatId = ddlSubCategory.SelectedValue;

        string url = string.Format("ViewSubCat.aspx?CatId={0}&SubCatID={1}",  CatId, LocSubCatId);
        Response.Redirect(url);
    }
}
