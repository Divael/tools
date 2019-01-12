using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class XMLWriteConfig
    {
        public bool xmlWrite()
        {
            DataTable dt;
            DataSet ds;
            using (ds = new DataSet())//创建数据源
            {
                using (dt = new DataTable("Config")) //创建一个名为Table的DataTalbe
                {
                    dt.Columns.Add(new DataColumn("调试模式", typeof(bool)));//为dt_dry表内建立Column
                    dt.Columns.Add(new DataColumn("设备名", typeof(string)));//为dt_dry表内建立Column
                    dt.Columns.Add(new DataColumn("设备号", typeof(string)));//为dt_dry表内建立Column
                    dt.Columns.Add(new DataColumn("串口1", typeof(string)));//为dt_dry表内建立Column
                    DataRow dr = dt.NewRow();//注意这边创建dt的新行的方法。指定类型是DataRow而不是TableRow，然后不用new直接的用创建的DataTabl
                    dr["调试模式"] = true;
                    //dr["设备名"] = Config.DEVICE_NAME;
                    //dr["设备号"] = Config.DEVICE_ID;
                    //dr["串口1"] = Config.COM;
                    dt.Rows.Add(dr);//添加到table

                    ds.Tables.Add(dt);//添加到dataSet

                    ds.WriteXml("config.xml");
                }
            }

            return true;
        }

        public void readConfig() {
            DataSet ds;
            using (ds = new DataSet())
            {
                ds.ReadXml("config.xml");
                DataTable dt = ds.Tables[0];
                try
                {
                    //Config.DEBUG = Boolean.Parse(dt.Rows[0]["调试模式"].ToString());
                    //Config.DEVICE_NAME = dt.Rows[0]["设备名"].ToString();
                    //Config.DEVICE_ID = dt.Rows[0]["设备号"].ToString();
                    //Config.COM = dt.Rows[0]["串口1"].ToString();

                }
                catch (Exception ex)
                {
                    Tools.Loger.err("App:", "配置文件加载错误,已载入默认配置" + ex.Message);
                    //SaveConfig();
                }
            }
        }
    }



}
