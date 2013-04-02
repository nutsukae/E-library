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

public partial class Admin_E_Library_ManageSubCategory : System.Web.UI.Page
{
    SubCategory SubCategory = new SubCategory();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
    }
   
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string SubCategoryId = Convert.ToString(GridView1.SelectedDataKey.Value);

        DataSet ds = SubCategory.GetDetail(SubCategoryId);
        DataTable dt = ds.Tables[0];

        txtSubCategoryName.Text = Convert.ToString(dt.Rows[0]["name"]);
        txtSubCategoryRemark.Text = Convert.ToString(dt.Rows[0]["remark"]);

        if (Convert.ToString(dt.Rows[0]["status"]) == "1") //Use
        {
            radUsed.Checked = true;
        }
        else
        {
            radUnUsed.Checked = true;
        }

        ViewState["Key"] = SubCategoryId;
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtSubCategoryName.Text = "";
        txtSubCategoryRemark.Text = "";

        ViewState["Key"] = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string CategoryId = ddlCategory.SelectedValue;
        string SubCategoryName = txtSubCategoryName.Text;
        string SubCategoryRemark = txtSubCategoryRemark.Text;
        string status = radUsed.Checked ? "1" : radUnUsed.Checked ? "0" : "";

        if (SubCategoryName == "")
        {
            Response.Write("<script>alert('กรุณากรอกชื่อ Sub Category ด้วยครับ');</script>");
            return;
        }

        if (Convert.ToString(ViewState["Key"]) != "")
        {
            if (SubCategory.Update(SubCategoryName, SubCategoryRemark, status, Convert.ToString(ViewState["Key"])))
            {
                Response.Write("<script>alert('แก้ไข Sub Category เรียบร้อยแล้วครับ');</script>");
            }
            else
            {
                Response.Write("<script>alert('พบปัญหา ไม่สามารถแก้ไข Sub Category ได้ครับ');</script>");
            }
        }
        else
        {
            if (!SubCategory.Exists(SubCategoryName,CategoryId))
            { 
                if (SubCategory.Add(CategoryId, SubCategoryName, SubCategoryRemark, status))
                {
                    Response.Write("<script>alert('เพิ่ม Sub Category เรียบร้อยแล้วครับ');</script>");
                }
                else
                {
                    Response.Write("<script>alert('พบปัญหา ไม่สามารถเพิ่ม Sub Category ได้ครับ');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('พบปัญหา ไม่สามารถเพิ่ม Sub Category ได้ เนื่องจากชื่อ Sub Category ซ้ำครับ');</script>");
            }
        }

        GridView1.DataBind();
    }
}
