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

public partial class User_nSearchByKeyword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtSearch.Attributes.Add("OnKeyPress", "return CheckEnter(event)");
        ddlSearchBy.SelectedIndex = 3;
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
}
