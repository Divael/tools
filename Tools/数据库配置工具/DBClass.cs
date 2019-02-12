using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGJLToolLib.Tool.Log;

namespace 数据库配置工具
{
    public class DBClass
    {
        private static SqlConnection connection = null;

        private string connectionString;
        public DBClass(string server,string dbname,string username,string password)
        {
            connectionString =
                $"Data Source={server};Initial Catalog={dbname};Persist Security Info=True;User ID={username};Password={password}";
            if (connection == null)
            {
                connection = new SqlConnection(connectionString);
            }
        }

        public bool OpenTest()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                SGLog.Log("DBTool", e.Message, "Error");
                return false;
            }
            finally
            {
                connection.Close();
            }

        }
    }
}
