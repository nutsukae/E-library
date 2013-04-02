using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using tool;

public partial class nUser_nUser : System.Web.UI.MasterPage
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
        {
            Request.Browser.Adapters.Clear();
        }
        
        if (!IsPostBack)
        { 
            DBTool dbTool = new DBTool();
            Category category = new Category();
            DataSet dsCategory = category.GetList();
            DataTable dtCategory = dsCategory.Tables[0];

            Menu1.Items.Add(new MenuItem("Library"));

            foreach (DataRow dr in dtCategory.Rows)
            {
                string CatName = Convert.ToString(dr["name"]);
                string CatId= Convert.ToString(dr["categoryid"]);
                string SubCatId = dbTool.GetOneString("SELECT TOP 1 subcategoryid FROM mst_subcategory WHERE Categoryid =" + CatId);

                MenuItem node = new MenuItem(CatName, "", "", "ViewCat.aspx?Catid=" + CatId + "&SubCatId="+SubCatId, "");
                Menu1.Items[0].ChildItems.Add(node);
            }
        }
        txtSearch.Attributes.Add("onkeypress", "return AnyInput_KeyDown(event,'" + btnSearch.ClientID + "')");
    }

    protected void btnHome_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("home.aspx");
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        string SubCatId = Request.QueryString["SubCatId"] != null ? Request.QueryString["SubCatId"].ToString() : "0";
        string URL = string.Format("ViewSearch.aspx?kw={0}&SubCatId={1}",txtSearch.Text,SubCatId);
        Response.Redirect(URL);
    }

    protected void btnLogOut_Click(object sender, ImageClickEventArgs e)
    {
        Session.Abandon();
        Response.Redirect("../nLogin.aspx");
    }

   
}
