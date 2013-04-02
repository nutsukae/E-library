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

public partial class Admin_E_Library_ManageAuthor : System.Web.UI.Page
{
    Author Author = new Author();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string authorid = Convert.ToString(GridView1.SelectedDataKey.Value);

        DataSet ds = Author.GetDetail(authorid);
        DataTable dt = ds.Tables[0];

        txtName.Text = Convert.ToString(dt.Rows[0]["name"]);
        txtRemark.Text = Convert.ToString(dt.Rows[0]["remark"]);

        ViewState["Key"] = authorid;
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
    }
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtName.Text = "";
        txtRemark.Text = "";

        ViewState["Key"] = "";
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string name = txtName.Text;
        string remark = txtRemark.Text;

        if (name == "")
        {
            Response.Write("<script>alert('กรุณากรอกชื่อ ผู้แต่ง ด้วยครับ');</script>");
            return;
        }

        if (Convert.ToString(ViewState["Key"]) != "")
        {
            //If exists just update
            if (Author.Update(name, remark, Convert.ToString(ViewState["Key"])))
            {
                Response.Write("<script>alert('แก้ไข ผู้แต่ง เรียบร้อยแล้วครับ');</script>");
            }
            else
            {
                Response.Write("<script>alert('พบปัญหา ไม่สามารถแก้ไข ผู้แต่ง ได้ครับ');</script>");
            }

        }
        else
        {
            if (!Author.Exists(name))
            {
                if (Author.Add(name, remark))
                {
                    Response.Write("<script>alert('เพิ่ม ผู้แต่ง เรียบร้อยแล้วครับ');</script>");
                }
                else
                {
                    Response.Write("<script>alert('พบปัญหา ไม่สามารถเพิ่ม ผู้แต่ง ได้ครับ');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('พบปัญหา ไม่สามารถเพิ่ม ผู้แต่ง ได้ เนื่องจาก ชื่อผู้แต่งซ้ำครับ ');</script>");
            }
        }

        GridView1.DataBind();
    }
}
