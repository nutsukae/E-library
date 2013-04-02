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
using System.Web.Configuration;
using System.IO;

/// <summary>
/// Summary description for DBTool
/// </summary>
/// 
namespace tool
{
    public class DBTool
    {
        public SqlConnection GetConnection()
        {
            string StrCon = WebConfigurationManager.ConnectionStrings["connection"].ConnectionString;

            SqlConnection conn = new SqlConnection(StrCon);
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                conn.Open();

                return conn;
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

        public Int32 GetCountRecord(string sql)
        {
            SqlConnection conn = GetConnection();
            SqlCommand command = new SqlCommand(sql, conn);

            try
            {
                int temp = Convert.ToInt32(command.ExecuteScalar());

                conn.Close();
                return temp;
            }
            catch (SqlException ex)
            {
                //WriteErrorLog(sql, ex.Message);
                return 0;
            }
        }

        public String GetOneString(string sql)
        {
            SqlConnection conn = GetConnection();
            SqlCommand command = new SqlCommand(sql, conn);

            try
            {
                string temp = Convert.ToString(command.ExecuteScalar());

                if (temp == "")
                {
                    conn.Close();
                    return "0";
                }
                else
                {
                   
                    conn.Close();
                    return temp;
                }
            }
            catch (SqlException ex)
            {
                //WriteErrorLog(sql, ex.Message);
                return null;
            }
        }

        public DataSet ExecuteDataSet(string sql)
        {
            DataSet ds = new DataSet();

            SqlConnection conn = GetConnection();
            SqlCommand command = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            try
            {
                adapter.Fill(ds, "table");

                conn.Close();
                return ds;
            }
            catch (SqlException ex)
            {
                //WriteErrorLog(sql, ex.Message);
                conn.Close();
                return null;
            }
        }

        public Boolean RunCommand(string sql)
        {
            SqlConnection conn = GetConnection();
            SqlCommand command = new SqlCommand(sql, conn);

            try
            {
                command.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (SqlException ex)
            {
                //WriteErrorLog(sql, ex.Message);
                conn.Close();
                return false;
            }
        }

        //private void WriteErrorLog(string SQL, string MSG)
        //{ 
        //    string DirectoryPath = "~/ErrorLog";
        //    if (!Directory.Exists(DirectoryPath))
        //    {
        //        Directory.CreateDirectory(DirectoryPath);
        //    }

        //    string FilePath = string.Format("{0}/Errorlog_{1}{2}{3}",DirectoryPath,DateTime.Now.Month, DateTime.Now.Day,DateTime.Now.Year);
        //    if (!File.Exists(FilePath))
        //    {
        //        File.Create(FilePath);
        //    }
        //    else
        //    {
        //        FileStream fs = new FileStream(FilePath, FileMode.Append);
        //        StreamWriter sw = new StreamWriter(fs);

        //        string Temp = string.Format("{0} : {1}", MSG, SQL);

        //        sw.WriteLine(sw);

        //        sw.Close();
        //        fs.Close();
        //    }
        //}
    }
}

