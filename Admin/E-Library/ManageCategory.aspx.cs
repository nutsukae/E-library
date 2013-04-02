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

public partial class Admin_E_Library_CategoryManagement : System.Web.UI.Page
{
    Category Category = new Category();

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
  
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string categoryid =  Convert.ToString( GridView1.SelectedDataKey.Value);

        DataSet ds = Category.GetDetail(categoryid);
        DataTable dt = ds.Tables[0];

        txtCategoryName.Text = Convert.ToString( dt.Rows[0]["name"]);
        txtCategoryRemark.Text = Convert.ToString( dt.Rows[0]["remark"]);

        if (Convert.ToString(dt.Rows[0]["status"]) == "1") //Use
        {
            radUsed.Checked = true;
        }
        else
        {
            radUnUsed.Checked = true;
        }

        ViewState["Key"] = categoryid;
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string CategoryName = txtCategoryName.Text;
        string CategoryRemark = txtCategoryRemark.Text;
        string status = radUsed.Checked ? "1" : radUnUsed.Checked ? "0" : "";

        if (CategoryName == "")
        {
            Response.Write("<script>alert('กรุณากรอกชื่อ Category ด้วยครับ');</script>");
            return;
        }

        if (Convert.ToString(ViewState["Key"]) != "")
        {
            //If exists just update
            if ( Category.Update(CategoryName, CategoryRemark, status,Convert.ToString(ViewState["Key"]) ))
            {
                Response.Write("<script>alert('แก้ไข Category เรียบร้อยแล้วครับ');</script>");
            }
            else
            {
                Response.Write("<script>alert('พบปัญหา ไม่สามารถแก้ไข Category ได้ครับ');</script>");
            }
            
        }
        else
        {
            if (!Category.Exists(CategoryName))
            { 
                if( Category.Add(CategoryName, CategoryRemark, status))
                {
                    Response.Write("<script>alert('เพิ่ม Category เรียบร้อยแล้วครับ');</script>");
                }
                else
                {
                    Response.Write("<script>alert('พบปัญหา ไม่สามารถเพิ่ม Category ได้ครับ');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('พบปัญหา ไม่สามารถเพิ่ม Category ได้ เนื่องจากชื่อ Category ซ้ำครับ');</script>");
            }
        }

        GridView1.DataBind();
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtCategoryName.Text = "";
        txtCategoryRemark.Text = "";

        ViewState["Key"] = "";

    }
}
