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
/// Summary description for Category
/// </summary>
/// 
namespace tool
{ 
    public class Category
    {
        public Boolean Add(string name, string remark, string status)
        {
            string SQL = string.Format("INSERT INTO mst_category(name,remark,status) VALUES ('{0}','{1}','{2}')",
                name, remark, status);

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

        public Boolean Exists(string name)
        {
            string SQL = "SELECT COUNT(*) FROM mst_category WHERE name = '" + name + "'";
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

        public DataSet GetDetail(string categoryid)
        {
            DBTool dbTool = new DBTool();
            SqlConnection conn = dbTool.GetConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetCategoryDetail";
            command.Parameters.Add("@Categoryid", SqlDbType.Int).Value = Convert.ToInt32(categoryid);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "sp_GetCategoryDetail");

            conn.Close();
            return ds;
        }

        public DataSet GetList()
        {
            string SQL = "SELECT * FROM mst_category where status = 1 ORDER BY [name]";

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
        public string GetName(string categoryid)
        {
            string SQL = "SELECT name FROM mst_category WHERE categoryid = " + categoryid;

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

        public Boolean Update(string name, string remark, string status, string categoryid)
        {
            string SQL = string.Format("UPDATE mst_category SET name = '{0}' , remark = '{1}', status = '{2}' WHERE"
                        + " categoryid = '{3}'", name, remark, status, categoryid);

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

