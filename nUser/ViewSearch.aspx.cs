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

public partial class nUser_ViewSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string SubCatId = Request.QueryString["SubCatId"].ToString();
        string Keyword = Request.QueryString["kw"].ToString();
        this.SearchKeyword.SelectParameters["SubCatid"].DefaultValue = SubCatId;
        this.SearchKeyword.SelectParameters["keyword"].DefaultValue = Keyword;

        ListView1.DataSource = SearchKeyword;
        ListView1.DataBind();
    }
}
