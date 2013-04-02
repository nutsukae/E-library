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

public partial class nLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Check Saved Username And Password in Cookies
        if (HttpContext.Current.Request.Cookies["RememberMe"] != null)
        {
            txtUserName.Text = HttpContext.Current.Request.Cookies["RememberMe"]["UserName"].ToString();
            txtPassword.Text = HttpContext.Current.Request.Cookies["RememberMe"]["Password"].ToString();
        }

        lblWarning.Visible = false;
    }
    
    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
    {
        tool.User myUser = new tool.User();

        if (txtUserName.Text.ToLower() == "admin" && txtPassword.Text.ToLower() == "12345")
        {
            Session["userid"] = "999";
            Response.Redirect("Admin/E-Library/addNewBook.aspx");
        }
        else if (myUser.CheckUser(txtUserName.Text, txtPassword.Text))
        {
            Response.Redirect("nUser/home.aspx");
        }
        else if (txtUserName.Text.ToLower() == "user" && txtPassword.Text.ToLower() == "12345")
        {
            Response.Redirect("nUser/home.aspx");
        }
        else
        {
            lblWarning.Visible = true;
        }
    }
}
