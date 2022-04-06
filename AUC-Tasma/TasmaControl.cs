using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace AUC_Tasma
{
    public class SqlConnect
    {
        public string serverName;
        public string databaseName;

        public void InfoSQL(string serverN, string databaseN)
        {
            this.serverName = serverN;
            this.databaseName = databaseN;
        }
        public SqlConnection connect()
        {
            string strConn = String.Format(@"Server={0};Database={1};Integrated Security=true",
               this.serverName, this.databaseName);
            return new SqlConnection(strConn);
        }
    }
    public class TasmaControl : SqlConnect
    {
        private SqlConnection conn;
        public TasmaControl(SqlConnection sql)
        {
            this.conn = sql;
        }
        public DataTable selectData(string tableName)
        {
            SqlDataAdapter adap = new SqlDataAdapter("SELECT * FROM " + tableName, conn);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            return dt;
        }
        public DataTable searchData(string tableName, string idName, object idValue)
        {
            string query = String.Format("SELECT * FROM {0} WHERE {1} LIKE N'%{2}%'", tableName,
                idName, idValue);
            SqlDataAdapter adap = new SqlDataAdapter(query, this.conn);
            DataTable dt = new DataTable();
            dt.Clear();
            adap.Fill(dt);
            return dt;
        }
        public string[] AutoComplete(string tableName, string autoName)
        {
            string[] nameCols;
            int lengthAC = selectData(tableName).Rows.Count;
            string query = String.Format("SELECT * FROM {0}", tableName);
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int i = 0;
                nameCols = new string[lengthAC];
                while (reader.Read())
                {
                    nameCols[i] = reader[autoName].ToString();
                    i++;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (conn != null) conn.Close();
            }
            return nameCols;
        }
    }
}
