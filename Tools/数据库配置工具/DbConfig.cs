using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGJLToolLib.Tool.Encrpy;
using SGJLToolLib.Tool.Log;

namespace 数据库配置工具
{
    public class DbConfig
    {
        /// <summary>
        /// 数据库地址
        /// </summary>
        public string Sevice = @"ftp.smartpioneer.cn";

        /// <summary>
        /// Database
        /// </summary>
        public string database = "yhxr";

        public string username = "sa";

        public string password = "aza@lea@123";

        private string ConfigFileName = "dbconfig.xml";

        public int selectNumber = 60;

        public DbConfig()
        {
            if (!File.Exists(ConfigFileName))
            {
                SaveConfig("DbConfig", ConfigFileName);
            }
            else
            {
                LoadConfig("DbConfig", ConfigFileName);
            }

            SGLog.Log("DBToolConfig", "DbConfig 配置文件加载完成");
        }

        public void SaveConfig()
        {
            SaveConfig("MechConfig", ConfigFileName);
        }

        public void LoadConfig(string dataSetName, string configFileName)
        {
            DataSet ds = new DataSet(dataSetName);
            ds.ReadXml(configFileName);
            var dt = ds.Tables[0];
            try
            {
                var t = GetType();
                var fs = t.GetFields();
                foreach (var f in fs)
                {
                    string tempStr = dt.Rows[0][f.Name].ToString();
                    if (f.Name.Equals("password"))
                    {
                        tempStr = MD5Tool.MD5Decrypt(tempStr, "????MT?>");
                    }
                    if (f.FieldType == typeof(bool))
                    {
                        f.SetValue(this, bool.Parse(tempStr));
                    }
                    else if (f.FieldType == typeof(int))
                    {
                        f.SetValue(this, int.Parse(tempStr));
                    }
                    else if (f.FieldType == typeof(float))
                    {
                        f.SetValue(this, float.Parse(tempStr));
                    }
                    else if (f.FieldType == typeof(decimal))
                    {
                        f.SetValue(this, decimal.Parse(tempStr));
                    }
                    else if (f.FieldType == typeof(double))
                    {
                        f.SetValue(this, double.Parse(tempStr));
                    }
                    else
                    {
                        f.SetValue(this, tempStr);
                    }

                }
            }
            catch (Exception ex)
            {
                SGLog.Log("DBToolConfig", configFileName + "配置文件加载错误,已载入默认配置 错误原因:" + ex.Message, "Error");
                SaveConfig(dataSetName, configFileName);
            }
        }

        public void SaveConfig(string dataSetName, string configFileName)
        {
            DataSet ds = new DataSet(dataSetName);
            DataTable dt = new DataTable("configData");

            List<DataColumn> list = new List<DataColumn>();
            var t = GetType();
            var fs = t.GetFields();
            foreach (var f in fs)
            {
                list.Add(new DataColumn(f.Name, typeof(string)));
            }
            dt.Columns.Clear();
            dt.Columns.AddRange(list.ToArray());

            DataRow dr = dt.NewRow();

            foreach (var f in fs)
            {
                if (f.Name.Equals("password"))
                {
                    dr[f.Name] = MD5Tool.MD5Encrypt(f.GetValue(this).ToString(), "????MT?>");
                }
                else
                {
                    dr[f.Name] = f.GetValue(this).ToString();
                }
            }

            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            ds.WriteXml(configFileName);

        }

    }
}
