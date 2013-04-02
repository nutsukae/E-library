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

public partial class login : System.Web.UI.Page
{
    DBTool dbTool = new DBTool();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        string GetUserDetail = string.Format("select * from tb_user where username = '{0}' and [password] = '{1}' and status = '1'", Login1.UserName, Login1.Password );
        DataSet ds = dbTool.ExecuteDataSet(GetUserDetail);
        DataTable dt = ds.Tables[0];

        if (dt.Rows.Count != 1)
        {
            Login1.FailureAction = LoginFailureAction.RedirectToLoginPage;
        }
        else
        {
            string position = Convert.ToString(dt.Rows[0]["position"]);

            Session["userid"] = Convert.ToString(dt.Rows[0]["userid"]);
            Session["firstname"] = Convert.ToString(dt.Rows[0]["firstname"]);
            Session["lastname"] = Convert.ToString(dt.Rows[0]["lastname"]);

            if (position == "admin")
            {
                Response.Redirect("Admin/E-Library/addNewBook.aspx");
            }
            else if (position == "user")
            {
                //Response.Redirect("User/SearchByCategory.aspx");
                Response.Redirect("User/UserPanel.aspx");
            }
        }
    }

}
