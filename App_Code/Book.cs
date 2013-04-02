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
/// Summary description for Book
/// </summary>
/// 
namespace tool
{ 
    public class Book
    {
        public DataSet GetBookDetailByID(string bookid)
        {
            DBTool dbTool = new DBTool();
            SqlConnection conn = dbTool.GetConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetBookDetailByID";
            command.Parameters.Add("@bookid", SqlDbType.Int).Value = Convert.ToInt32(bookid);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "sp_GetBookDetail");

            conn.Close();
            return ds;
        }

        public DataSet GetBookDetailByISBN(string ISBN)
        {
            DBTool dbTool = new DBTool();
            SqlConnection conn = dbTool.GetConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetBookDetailByISBN";
            command.Parameters.Add("@isbn", SqlDbType.NVarChar).Value = ISBN;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "sp_GetBookDetail");

            conn.Close();
            return ds;
        }

        public DataSet GetBookListByKeyword(string SearchBy, string Keyword, string SortBy)
        {
            DBTool dbTool = new DBTool();
            SqlConnection conn = dbTool.GetConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetBookListByKeyword";
            command.Parameters.Add("@SearchBy", SqlDbType.Int).Value = Convert.ToInt32(SearchBy);
            command.Parameters.Add("@Keyword", SqlDbType.NVarChar).Value = Keyword;
            command.Parameters.Add("@SortBy", SqlDbType.Int).Value = Convert.ToInt32(SortBy);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "sp_GetBookListByKeyword");

            conn.Close();
            return ds;
        }

        public DataSet GetBookListByKeyword_MB(string Keyword)
        {
            DBTool dbTool = new DBTool();
            SqlConnection conn = dbTool.GetConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetBookListByKeyword_MB";
            command.Parameters.Add("@Keyword", SqlDbType.NVarChar).Value = Keyword;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "sp_GetBookListByKeyword_MB");

            conn.Close();
            return ds;
        }

        public DataSet GetBookListByCategory(string SubCategoryId)
        {
            DBTool dbTool = new DBTool();
            SqlConnection conn = dbTool.GetConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetBookListByCategory";
            command.Parameters.Add("@SubCategoryid", SqlDbType.Int).Value = Convert.ToInt32(SubCategoryId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "sp_GetBookListByCategory");

            conn.Close();
            return ds;
        }

        public DataSet GetBookListByCategory_NG(string SubCategoryId)
        {
            DBTool dbTool = new DBTool();
            SqlConnection conn = dbTool.GetConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Book_ListBookByCategory";
            command.Parameters.Add("@SubCategoryid", SqlDbType.Int).Value = Convert.ToInt32(SubCategoryId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "sp_Book_ListBookByCategory");

            conn.Close();
            return ds;
        }

        public DataSet GetBookListByCategory_MB(string SubCategoryId)
        {
            DBTool dbTool = new DBTool();
            SqlConnection conn = dbTool.GetConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetBookListByCategory_MB";
            command.Parameters.Add("@SubCategoryid", SqlDbType.Int).Value = Convert.ToInt32(SubCategoryId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "sp_GetBookListByCategory_MB");

            conn.Close();
            return ds;
        }

        public string GetBookIdByISBN(string ISBN)
        {
            DBTool dbTool = new DBTool();
            SqlConnection conn = dbTool.GetConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetBookIdByISBN";
            command.Parameters.Add("@isbn", SqlDbType.NVarChar).Value = ISBN;

            string bookid = Convert.ToString( command.ExecuteScalar());

            conn.Close();
            return bookid;
        }

        public string GetSubcategoryByID(string bookid)
        {
            string SQL = "SELECT subcategoryid from tb_book where bookid='" + bookid + "'";

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

        public string GetLastestBookId()
        {
            DBTool dbTool = new DBTool();
            SqlConnection conn = dbTool.GetConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GetLastestBookId";
            
            string bookid = Convert.ToString(command.ExecuteScalar());

            conn.Close();
            return bookid;
        }

        public DataSet ListBooks()
        {
            DBTool dbTool = new DBTool();
            SqlConnection conn = dbTool.GetConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Book_ListBooks";

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "sp_Book_ListBooks");

            conn.Close();
            return ds;
        }

        public DataSet ListInteriorBooks()
        {
            DBTool dbTool = new DBTool();
            SqlConnection conn = dbTool.GetConnection();

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_Book_GetThisMonthInteriorBook";

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();

            adapter.Fill(ds, "sp_Book_GetThisMonthInteriorBook");

            conn.Close();
            return ds;
        }
    }
}
