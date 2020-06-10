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

namespace Tools
{
    public class DBUtils
    {
        //数据库连接字符串
        //Provider=Microsoft.ACE.OLEDB.12.0;Data Source=I:\易用科技\19041-干酪根酸化\Project\kerogen.accdb
        //Provider=Microsoft.Jet.OLEDB.4.0;Data Source = I:\易用科技\19041-干酪根酸化\Project\ETUSE.干酪根酸化\酸化过程表\bin\Debug\kerogen.mdb
        public static string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\kerogen.mdb;";
        private static DBUtils dBUtils;
        private static object locks = new object();
        private OleDbConnection connection;
        
        public DBUtils()
        {
            if (connection == null)
            {
                connection = new OleDbConnection(connectionString);
            }
        }

        public static DBUtils getInstance()
        {
            if (dBUtils == null)
            {
                lock (locks)
                {
                    if (dBUtils == null)
                    {
                        dBUtils = new DBUtils();
                        return dBUtils;
                    }

                }
            }
            return dBUtils;
        }

        public void OpenDB() {
            if (connection.State!= ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                }
                catch (OleDbException ex)
                {
                    throw new Exception(ex.Message) ;
                }
            }
        }

        public void CloseDB() {
            if (connection.State != ConnectionState.Closed)
            {
                try
                {
                    connection.Close();
                }
                catch (OleDbException ex)
                {
                    throw new Exception(ex.Message) ;
                }
            }
        }

        #region 获取Access数据库中的所有表名
        /// <summary>
        /// 获取Access数据库中的所有表名
        /// </summary>
        /// <param name="Connection">OleDbConnection对象</param>
        /// <returns></returns>
        public DataTable GetDataTableName()
        {
            DataTable Dt = null;
            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
                Dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open) connection.Close();
            }
            return Dt;
        }
        #endregion

        #region 公用方法

        public int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        public bool Exists(string strSql)
        {
            object obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Exists(string strSql, params OleDbParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString)
        {

            using (OleDbCommand cmd = new OleDbCommand(SQLString, connection))
            {
                try
                {
                    OpenDB();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.OleDb.OleDbException E)
                {
                    connection.Close();
                    throw new Exception(E.Message);
                }
            }

        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public void ExecuteSqlTran(ArrayList SQLStringList)
        {

            OpenDB();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connection;
            OleDbTransaction tx = connection.BeginTransaction();
            cmd.Transaction = tx;
            try
            {
                for (int n = 0; n < SQLStringList.Count; n++)
                {
                    string strsql = SQLStringList[n].ToString();
                    if (strsql.Trim().Length > 1)
                    {
                        cmd.CommandText = strsql;
                        cmd.ExecuteNonQuery();
                    }
                }
                tx.Commit();
            }
            catch (System.Data.OleDb.OleDbException E)
            {
                tx.Rollback();
                throw new Exception(E.Message);
            }

        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString, string content)
        {

            OleDbCommand cmd = new OleDbCommand(SQLString, connection);
            System.Data.OleDb.OleDbParameter myParameter = new System.Data.OleDb.OleDbParameter("@content", OleDbType.VarChar);
            myParameter.Value = content;
            cmd.Parameters.Add(myParameter);
            try
            {
                OpenDB();
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }
            catch (System.Data.OleDb.OleDbException E)
            {
                throw new Exception(E.Message);
            }
            finally
            {
                cmd.Dispose();
                //connection.Close();
            }

        }
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {

            OleDbCommand cmd = new OleDbCommand(strSQL, connection);
            System.Data.OleDb.OleDbParameter myParameter = new System.Data.OleDb.OleDbParameter("@fs", OleDbType.Binary);
            myParameter.Value = fs;
            cmd.Parameters.Add(myParameter);
            try
            {
                OpenDB();
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }
            catch (System.Data.OleDb.OleDbException E)
            {
                throw new Exception(E.Message);
            }
            finally
            {
                cmd.Dispose();
                //connection.Close();
            }

        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string SQLString)
        {

            using (OleDbCommand cmd = new OleDbCommand(SQLString, connection))
            {
                try
                {
                    OpenDB();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (System.Data.OleDb.OleDbException e)
                {
                    connection.Close();
                    throw new Exception(e.Message);
                }
            }

        }
        /// <summary>
        /// 执行查询语句，返回OleDbDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>OleDbDataReader</returns>
        public OleDbDataReader ExecuteReader(string strSQL)
        {
            OleDbCommand cmd = new OleDbCommand(strSQL, connection);
            try
            {
                OpenDB();
                OleDbDataReader myReader = cmd.ExecuteReader();
                return myReader;
            }
            catch (System.Data.OleDb.OleDbException e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string SQLString)
        {

            DataSet ds = new DataSet();
            try
            {
                OpenDB();
                OleDbDataAdapter command = new OleDbDataAdapter(SQLString, connection);
                command.Fill(ds, "ds");
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new Exception(ex.Message);
            }
            return ds;

        }


        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString, params OleDbParameter[] cmdParms)
        {

            using (OleDbCommand cmd = new OleDbCommand())
            {
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return rows;
                }
                catch (System.Data.OleDb.OleDbException E)
                {
                    throw new Exception(E.Message);
                }
            }

        }




        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的OleDbParameter[]）</param>
        public bool ExecuteSqlTran(List<SqlPars> SQLStringList)
        {

            OpenDB();
            using (OleDbTransaction trans = connection.BeginTransaction())
            {
                OleDbCommand cmd = new OleDbCommand();
                try
                {
                    //循环
                    foreach (SqlPars myDE in SQLStringList)
                    {
                        string cmdText = myDE.Sql.ToString();
                        OleDbParameter[] cmdParms = (OleDbParameter[])myDE.Parameters;
                        PrepareCommand(cmd, connection, trans, cmdText, cmdParms);
                        int val = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }

        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string SQLString, params OleDbParameter[] cmdParms)
        {

            using (OleDbCommand cmd = new OleDbCommand())
            {
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (System.Data.OleDb.OleDbException e)
                {
                    throw new Exception(e.Message);
                }
            }

        }

        /// <summary>
        /// 执行查询语句，返回OleDbDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>OleDbDataReader</returns>
        public OleDbDataReader ExecuteReader(string SQLString, params OleDbParameter[] cmdParms)
        {

            OleDbCommand cmd = new OleDbCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                OleDbDataReader myReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.OleDb.OleDbException e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string SQLString, params OleDbParameter[] cmdParms)
        {

            OleDbCommand cmd = new OleDbCommand();
            PrepareCommand(cmd, connection, null, SQLString, cmdParms);
            using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds, "ds");
                    cmd.Parameters.Clear();
                }
                catch (System.Data.OleDb.OleDbException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }

        }


        private void PrepareCommand(OleDbCommand cmd, OleDbConnection connection, OleDbTransaction trans, string cmdText, OleDbParameter[] cmdParms)
        {
            if (connection.State != ConnectionState.Open)
                OpenDB();
            cmd.Connection = connection;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (OleDbParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion

        #region 执行表的更新
        public bool ExecuteUpdateTable(string sql, DataTable dataTable)
        {

            OpenDB();
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection))
            {
                using (OleDbCommandBuilder sb1 = new OleDbCommandBuilder(adapter))
                {
                    try
                    {
                        int rs = adapter.Update(dataTable);
                        if (rs >= 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (OleDbException ex)
                    {
                        throw new Exception(ex.Message);
                        return false;
                    }

                }

            }
        }
        #endregion

        #region 将DataReaderToList
        public List<T> DataReaderToList<T>(OleDbDataReader SDR) where T : class
        {
            List<T> ListData = new List<T>();

            if (SDR.HasRows)
            {
                ListData.Clear();
                while (SDR.Read())
                {
                    object Obj = System.Activator.CreateInstance(typeof(T));
                    Type ObjType = Obj.GetType();
                    #region 一行数据赋值给一个对象
                    for (int i = 0; i < SDR.FieldCount; i++)
                    {
                        PropertyInfo PI = ObjType.GetProperty(SDR.GetName(i));
                        if (PI != null)
                        {
                            string PTName = PI.PropertyType.Name.ToString();
                            string FullName = PI.PropertyType.FullName;
                            string Name = PI.Name;

                            object Value = PI.GetValue(Obj, null);

                            switch (PI.PropertyType.ToString())
                            {
                                case "System.Int64":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToInt64(SDR[Name]), null);
                                    break;
                                case "System.Byte[]":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : (byte[])SDR[Name], null);
                                    break;
                                case "System.Boolean":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToBoolean(SDR[Name]), null);
                                    break;
                                case "System.String":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToString(SDR[Name]), null);
                                    break;
                                case "System.DateTime":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToDateTime(SDR[Name]), null);
                                    break;
                                case "System.Decimal":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToDecimal(SDR[Name]), null);
                                    break;
                                case "System.Double":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToDouble(SDR[Name]), null);
                                    break;
                                case "System.Int32":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToInt32(SDR[Name]), null);
                                    break;
                                case "System.Single":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToSingle(SDR[Name]), null);
                                    break;
                                case "System.Byte":
                                    PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToByte(SDR[Name]), null);
                                    break;
                                default:
                                    int Chindex = PTName.IndexOf("Nullable");
                                    if (FullName.IndexOf("System.Int64") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToInt64(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.Boolean") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToBoolean(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.String") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToString(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.DateTime") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToDateTime(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.Decimal") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToDecimal(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.Double") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToDouble(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.Int32") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToInt32(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.Single") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToSingle(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.Byte") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToByte(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.Int16") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToInt16(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.UInt16") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToUInt16(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.UInt32") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToUInt32(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.UInt64") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToUInt64(SDR[Name]), null);
                                    }

                                    if (FullName.IndexOf("System.SByte") >= 0)
                                    {
                                        PI.SetValue(Obj, SDR.IsDBNull(SDR.GetOrdinal(Name)) ? Value : Convert.ToSByte(SDR[Name]), null);
                                    }
                                    break;
                            }

                        }
                    }
                    #endregion
                    ListData.Add(Obj as T);
                }
            }
            if (!SDR.IsClosed)
                SDR.Close();

            return ListData;
        }
        #endregion

    }

    public class SqlPars
    {
        public string Sql { get; set; }
        public OleDbParameter[] Parameters { get; set; }
    }
}
