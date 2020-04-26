using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    /// <summary>
    /// Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Projects\Access\Database1.accdb
    /// </summary>
    public class DB
    {
        const string strConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Projects\Access\Database1.accdb";
        OleDbConnection oleDb;

        public DB()
        {
            oleDb = new OleDbConnection(strConnection);
            oleDb.Open();
        }
        public DataTable Get()
        {
            string sql = "select * from 学生表";
            DataTable dt = new DataTable();
            OleDbDataAdapter dbDataAdapter = new OleDbDataAdapter(sql, oleDb);//创建一个OleDb的适配器
            dbDataAdapter.Fill(dt);//将适配到的对象填充到表中
            return dt;
        }



    }
}
