using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Test1
{
    /// <summary>
    /// Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Projects\Access\Database1.accdb
    /// </summary>
    public class DB
    {
        //Provider=Microsoft.Jet.OLEDB.4.0;Data Source = E:\C#开发程序包\toolsFromGit1\Tools\Test1\DB\Database1.
        //@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\kerogen.mdb;";
        string strConnection = @" Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source= " + Application.StartupPath + @"\DB\Database1.mdb";
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

        public bool Update(int id ,string rowName,string strUpdate) {
            string sql = "update 学生表 set " + rowName + " = '" + strUpdate + "' where id = " + id;
            OleDbCommand command = new OleDbCommand(sql,oleDb);
            int i = command.ExecuteNonQuery();
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool Update(DataTable dt)
        {
            string sql = "select * from 学生表";
            OleDbDataAdapter dbDataAdapter = new OleDbDataAdapter(sql, oleDb);
            OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(dbDataAdapter);
            int rs = dbDataAdapter.Update(dt);
            if (rs>0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        internal void insetStudentByProduce(string v1, string v2)
        {
            //OleDbCommand command = new OleDbCommand();
            //command.Connection = oleDb;
            //command.CommandType = CommandType.StoredProcedure;
            //command.CommandText = "insertNameAndSex";
            OleDbParameter par = new OleDbParameter()
            {
                ParameterName = "name",
                Value = v1
            };
            OleDbParameter par1 = new OleDbParameter()
            {
                ParameterName = "sex",
                Value = v2
            };
            OleDbParameter[] pars = { par, par1 };
            //int rs = command.ExecuteNonQuery();
            OleDbCommand command = new OleDbCommand("execute insertNameAndSex", oleDb);
            command.Parameters.AddRange(pars);
            int rs = command.ExecuteNonQuery();
        }
    }
}
