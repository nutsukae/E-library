using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SubCategory
/// </summary>
/// 
namespace tool
{ 
    public class SubCategory
    {
        public Boolean Add(string categoryid, string name, string remark, string status)
        {
            string SQL = string.Format("INSERT INTO mst_subcategory(categoryid,name,remark,status) VALUES"
                    + " ('{0}','{1}','{2}','{3}')",categoryid, name, remark, status);

            DBTool dbTool = new DBTool();
            if (dbTool.RunCommand(SQL))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean Exists(string name, string categoryid)
        {
            string SQL = "SELECT COUNT(*) FROM mst_subcategory WHERE name = '" + name + "' AND categoryid =" + categoryid;
            DBTool dbTool = new DBTool();

            if (dbTool.GetCountRecord(SQL) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataSet GetDetail(string subcategoryid)
        {
            DBTool dbTool = new DBTool();
            SqlConnection conn = dbTool.GetConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetSubCategoryDetail";
            command.Parameters.Add("@SubCategoryid", SqlDbType.Int).Value = Convert.ToInt32(subcategoryid);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "sp_GetSubCategoryDetail");

            conn.Close();
            return ds;
        }

        public DataSet GetList(string categoryid)
        {
            string SQL = "SELECT * FROM mst_subcategory WHERE categoryid = "+categoryid;

            DBTool dbTool = new DBTool();

            try
            {
                return dbTool.ExecuteDataSet(SQL);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetName(string subcategoryid)
        {
            string SQL = "SELECT name FROM mst_subcategory WHERE subcategoryid =" + subcategoryid;

            DBTool dbTool = new DBTool();

            try
            {
                return dbTool.GetOneString(SQL);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public Boolean Update(string name, string remark, string status, string subcategoryid)
        {
            string SQL = string.Format("UPDATE mst_subcategory SET name = '{0}' , remark = '{1}', status = '{2}' WHERE"
                            + " subcategoryid = '{3}'", name, remark, status, subcategoryid);

            DBTool dbTool = new DBTool();
            if (dbTool.RunCommand(SQL))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

