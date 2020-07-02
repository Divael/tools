using System;
using System.Data.SqlClient;

namespace 数据库配置工具
{
    public class DBClass
    {
        private static SqlConnection connection = null;

        private string connectionString;
        public DBClass(string server, string dbname, string username, string password)
        {
            connectionString =
                $"Data Source={server};Initial Catalog={dbname};Persist Security Info=True;User ID={username};Password={password}";
            connection = new SqlConnection(connectionString);
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
                Tools.Loger.err("DBTool", e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

        }

        public static DBConfig getConnectionString()
        {
            DBConfig config = new DBConfig();
            Tools.IniHelper.SetFilePath(System.Environment.CurrentDirectory + @"\config.ini");
            config.Server = Tools.IniHelper.ReadIniData("数据库配置", "DataSource", "");
            config.Database = Tools.IniHelper.ReadIniData("数据库配置", "Database", "");
            config.UserId = Tools.IniHelper.ReadIniData("数据库配置", "UserID", "");
            string s = Tools.IniHelper.ReadIniData("数据库配置", "Password", "").Trim();
            if (!string.IsNullOrWhiteSpace(s))
            {
                config.PassWord = Tools.EncyptHelper.MD5Decrypt(s);
            }
            else
            {
                config.PassWord = "";
            }
            config.TopSelected = Tools.IniHelper.ReadIniData("数据库配置", "最大连接数", "");
            return config;
        }
    }


    public class DBConfig
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public string PassWord { get; set; }
        public string TopSelected { get; set; }

        public override string ToString()
        {
            //Provider=SQLOLEDB.1;Password=aza@lea@123;Persist Security Info=True;User ID=sa;Initial Catalog=fkgl;Data Source=ftp.smartpioneer.cn
            return $"Password ={PassWord}; Persist Security Info = True; User ID = {UserId}; Initial Catalog = {Database}; Data Source = {Server}";
        }
    }
}
