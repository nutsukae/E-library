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
using System.Globalization;

/// <summary>
/// Summary description for Utility
/// </summary>
/// 
namespace tool
{
    public class Utility
    { 
        public string GetToday()
        {
            return string.Format("{0}-{1}-{2}", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Year);
        }

        #region String
        public string HandleQuote(string sentecnce)
        {
            return sentecnce.Replace("'", "''");
        }

        public string RemoveDash(string sentence)
        {
            string tmp = sentence;
            int indexOfDash = 0;

            do
            {
                indexOfDash = tmp.IndexOf("-");
                if (indexOfDash > 0)
                {
                    tmp = tmp.Remove(indexOfDash, 1);
                }

            } while (indexOfDash > 0);

            return tmp;
        }
        #endregion

        #region DropDownList
        public DataTable GenerateMonth()
        {
            DataTable dt = new DataTable("GenerateMonth");
            dt.Columns.Add("MonthName");
            dt.Columns.Add("MonthValue");

            dt.Rows.Add("  ----------  ", "0");

            for (int i = 1; i <= 12; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                dr[1]= i;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public DataTable GenerateYear(int year)
        {
            DataTable dt = new DataTable("GenerateYear");
            dt.Columns.Add("YearName");
            dt.Columns.Add("YearValue");

            dt.Rows.Add("  ----------  ", "0");

            for (int i = DateTime.Now.Year; i > year; i--)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public DataTable GetSearchSort()
        {
            DataTable dt = new DataTable("SearchSort");
            dt.Columns.Add("SortBy");
            dt.Columns.Add("Value");
            dt.Rows.Add(" ----- ", "0");
            dt.Rows.Add("Newest", "1");
            dt.Rows.Add("Oldest", "2");
            return dt;
        }

        public DataTable GetSearchOtherOption()
        {
            DataTable dt = new DataTable("SearchOtherOption");
            dt.Columns.Add("SearchOtherOption");
            dt.Columns.Add("Value");
            dt.Rows.Add(" ----- ", "0");
            dt.Rows.Add("Show All", "1");
            dt.Rows.Add("Show No-Cover", "2");
            //dt.Rows.Add("Show No-PDF", "3");
            dt.Rows.Add("Show No-Zip", "4");
            return dt;
        }
        #endregion

    }
}
