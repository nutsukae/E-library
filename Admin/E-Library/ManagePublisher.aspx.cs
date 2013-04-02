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

public partial class Admin_E_Library_ManagePublisher : System.Web.UI.Page
{
    Publisher Publisher = new Publisher();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
    }
    
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string publisherid = Convert.ToString(GridView1.SelectedDataKey.Value);

        DataSet ds = Publisher.GetDetail(publisherid);
        DataTable dt = ds.Tables[0];

        txtName.Text = Convert.ToString(dt.Rows[0]["name"]);
        txtRemark.Text = Convert.ToString(dt.Rows[0]["remark"]);

        ViewState["Key"] = publisherid;
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
            Response.Write("<script>alert('กรุณากรอกชื่อ สำนักพิมพ์ ด้วยครับ');</script>");
            return;
        }

        if (Convert.ToString(ViewState["Key"]) != "")
        {
            //If exists just update
            if (Publisher.Update(name, remark, Convert.ToString(ViewState["Key"])))
            {
                Response.Write("<script>alert('แก้ไข สำนักพิมพ์ เรียบร้อยแล้วครับ');</script>");
            }
            else
            {
                Response.Write("<script>alert('พบปัญหา ไม่สามารถแก้ไข สำนักพิมพ์ ได้ครับ');</script>");
            }

        }
        else
        {
            if (!Publisher.Exists(name))
            { 
                if (Publisher.Add(name, remark))
                {
                    Response.Write("<script>alert('เพิ่ม สำนักพิมพ์ เรียบร้อยแล้วครับ');</script>");
                }
                else
                {
                    Response.Write("<script>alert('พบปัญหา ไม่สามารถเพิ่ม สำนักพิมพ์ ได้ครับ');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('พบปัญหา ไม่สามารถเพิ่ม สำนักพิมพ์ ได้ เนื่องจากชื่อสำนักพิมพ์ซ้ำครับ');</script>");
            }
            
        }
    }
}
