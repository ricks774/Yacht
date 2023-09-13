using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Yacht
{
    public class YachtDBHelper
    {
        private readonly SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["YachtsDbConnection"].ConnectionString);
        private readonly SqlCommand sqlcommand = new SqlCommand();

        // ! 開啟連線
        private void ConnectDB()
        {
            if (connection.State != System.Data.ConnectionState.Open) connection.Open();
        }

        // ! 關閉連線
        public void CloseConnection()
        {
            connection.Close();
        }

        // ! 更新資料
        public int ExecuteSQL(string command, SqlParameter[] parameters)
        {
            ConnectDB();
            sqlcommand.Connection = connection;
            sqlcommand.CommandText = command;

            if (parameters != null)
            {
                sqlcommand.Parameters.AddRange(parameters);
            }

            int result = sqlcommand.ExecuteNonQuery();
            CloseConnection();

            // 方法中執行完 SQL 查詢後，移除了參數集合中的兩個參數
            // 避免 變數名稱 '@Act' 已經宣告。變數名稱在一個查詢批次或預存程序內必須是唯一的 錯誤發生
            foreach (SqlParameter parameter in parameters)
            {
                sqlcommand.Parameters.Remove(parameter);
            }
            return result;
        }

        // ! 查詢全部資料
        public SqlDataReader SearchData(string sql)
        {
            ConnectDB();
            sqlcommand.Connection = connection;
            sqlcommand.CommandText = sql;
            SqlDataReader reader = sqlcommand.ExecuteReader();
            return reader;
        }

        // ! 查詢資料
        public SqlDataReader SearchData(string sql, SqlParameter[] parameters)
        {
            ConnectDB();
            sqlcommand.Connection = connection;
            sqlcommand.CommandText = sql;

            if (parameters != null)
            {
                sqlcommand.Parameters.AddRange(parameters);
            }

            SqlDataReader reader = sqlcommand.ExecuteReader();
            return reader;
        }

        // ! 查詢資料
        public SqlDataReader SearchData(string sql, Dictionary<string, object> parameters)
        {
            ConnectDB();
            sqlcommand.Connection = connection;
            sqlcommand.CommandText = sql;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    SqlParameter sqlParameter = new SqlParameter(parameter.Key, parameter.Value);
                    sqlcommand.Parameters.Add(sqlParameter);
                }
            }

            SqlDataReader reader = sqlcommand.ExecuteReader();
            return reader;
        }

        // ! 新增資料後回傳剛剛新增的資料Id
        public int ExecuteSQLId(string command, SqlParameter[] parameters)
        {
            ConnectDB();
            sqlcommand.Connection = connection;
            sqlcommand.CommandText = command;
            object result = sqlcommand.ExecuteScalar();
            int res;

            if (parameters != null)
            {
                sqlcommand.Parameters.AddRange(parameters);
            }

            if (result != null && int.TryParse(result.ToString(), out res))
            {
                CloseConnection();
                return res;
            }

            // 方法中執行完 SQL 查詢後，移除了參數集合中的兩個參數
            // 避免 變數名稱 '@Act' 已經宣告。變數名稱在一個查詢批次或預存程序內必須是唯一的 錯誤發生
            foreach (SqlParameter parameter in parameters)
            {
                sqlcommand.Parameters.Remove(parameter);
            }

            CloseConnection();
            return 0;
        }

        // ! 新增資料後回傳剛剛新增的帳號資料
        public string ExecuteSQLAccount(string command)
        {
            ConnectDB();
            sqlcommand.Connection = connection;
            sqlcommand.CommandText = command;
            object result = sqlcommand.ExecuteScalar();
            string account = result as string;

            CloseConnection();
            return account;
        }

        // ! 登入檢查
        public SqlDataReader Login(string act, string pwd)
        {
            ConnectDB();
            sqlcommand.Connection = connection;
            string sql = "SELECT ui.Account, ui.[Name],ui.MaxPower, ab.AlbumAdd,ab.AlbumEdit,ab.MaxPower as RegPower FROM UserInfo ui join AlbumAuthority ab on ab.AccountName = ui.Account WHERE Account = @Id and Password = @PD";
            sqlcommand.Parameters.AddWithValue("@Id", act);
            sqlcommand.Parameters.AddWithValue("@PD", pwd);
            sqlcommand.CommandText = sql;
            SqlDataReader reader = sqlcommand.ExecuteReader();
            return reader;
        }
    }
}