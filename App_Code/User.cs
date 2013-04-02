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
using System.Web.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Summary description for User
/// </summary>
/// 
namespace tool
{ 
    public class User
    {
        public DataSet CheckUsernamePassword(string Username, string Password)
        {
            DBTool dbTool = new DBTool ();

            string GetUserDetail = string.Format("SELECT * FROM tb_user WHERE username = '{0}' and [password] = '{1}' and status = '1'", Username, Password);
            return dbTool.ExecuteDataSet(GetUserDetail);
        }

        public bool CheckUser(string Username, string Password)
        {
            string conStr = WebConfigurationManager.ConnectionStrings["IVMISCoreCon"].ConnectionString;
            SqlConnection Conn = new SqlConnection(conStr);
            SqlCommand Command = new SqlCommand();
            Command.Connection = Conn;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "sp_Emp_CheckUser";
            Command.Parameters.Add("@username", SqlDbType.NVarChar).Value = Username;
            Command.Parameters.Add("@password", SqlDbType.NVarChar).Value = Password;

            try
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }

                Conn.Open();

                int count = Convert.ToInt32(Command.ExecuteScalar());

                Command = null;
                Conn.Close();
                Conn = null;

                if (count == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

