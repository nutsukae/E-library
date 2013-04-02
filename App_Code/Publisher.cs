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
/// Summary description for Publisher
/// </summary>
/// 
namespace tool
{ 
    public class Publisher
    {
        public Boolean Add(string name, string remark)
        {
            string SQL = string.Format("INSERT INTO mst_publisher(name,remark) VALUES ('{0}','{1}')",
                name, remark);

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
            string SQL = "SELECT COUNT(*) FROM mst_publisher WHERE name = '" + name + "'";
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

        public DataSet GetDetail(string publisherid)
        {
            DBTool dbTool = new DBTool();
            SqlConnection conn = dbTool.GetConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetPublisherDetail";
            command.Parameters.Add("@publisherid", SqlDbType.Int).Value = Convert.ToInt32(publisherid);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "sp_GetPublisherDetail");

            conn.Close();
            return ds;
        }

        public Boolean Update(string name, string remark, string publisherid)
        {
            string SQL = string.Format("UPDATE mst_publisher SET name = '{0}' , remark = '{1}' WHERE"
                        + " publisherid = '{3}'", name, remark, publisherid);

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

