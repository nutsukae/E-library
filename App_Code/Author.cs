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
/// Summary description for Author
/// </summary>
/// 
namespace tool
{ 
    public class Author
    {
        public Boolean Add(string name, string remark)
        {
            string SQL = string.Format("INSERT INTO mst_author(name,remark) VALUES ('{0}','{1}')",
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
            string SQL = "SELECT COUNT(*) FROM mst_author WHERE name = '" + name + "'";
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

        public DataSet GetDetail(string authorid)
        {
            DBTool dbTool = new DBTool();
            SqlConnection conn = dbTool.GetConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetAuthorDetail";
            command.Parameters.Add("@authorid", SqlDbType.Int).Value = Convert.ToInt32(authorid);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "sp_GetAuthorDetail");

            conn.Close();
            return ds;
        }

        public Boolean Update(string name, string remark, string authorid)
        {
            string SQL = string.Format("UPDATE mst_author SET name = '{0}' , remark = '{1}' WHERE"
                        + " authorid = '{3}'", name, remark, authorid);

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

