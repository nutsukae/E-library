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

public partial class nUser_home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadSideBanner();
    }

    #region Banner
    private void LoadSideBanner()
    {
        Book book = new Book();

        DataSet ds = new DataSet();
        ds = book.ListInteriorBooks();

        DataTable dt = new DataTable();
        dt = ds.Tables[0];

        int RowCount = dt.Rows.Count;

        if (RowCount > 0)
        {
            BookPanel.Controls.Add(new LiteralControl("<ul id=\"mycarousel\" class=\"jcarousel jcarousel-skin-tango\">"));
            for (int i = 0; i < RowCount; i++)
            {
                string control = string.Format("<li><a href=\"ViewBook.aspx?bookid={0}\">"
                    + "<img src=\"../E-Library/{1}\" width=\"150\" height=\"150\" alt=\"\" /></li>"
                        , Convert.ToString(dt.Rows[i]["bookid"]), Convert.ToString(dt.Rows[i]["imgpath"]));
                BookPanel.Controls.Add(new LiteralControl(control));
            }
            BookPanel.Controls.Add(new LiteralControl("</ul>"));
        }
    }
    #endregion
}
