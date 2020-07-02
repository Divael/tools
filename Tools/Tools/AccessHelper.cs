using System;
using System.Data;
using System.Data.OleDb;

namespace Tools
{
    /// <summary>
    /// <para>　</para>
    /// 常用工具类——Access数据库操作类
    /// <para>　-------------------------------------------------</para>
    /// <para>　CreateConnection：根据MDB文件路径生成OleConnection对象实例</para>
    /// <para>　ExecuteDataSet：执行一条SQL语句，返回一个DataSet对象</para>
    /// <para>　ExecuteDataTable：执行一条SQL语句，返回一个DataTable对象</para>
    /// <para>　ExecuteDataAdapter：表示一组数据命令和一个数据库连接，它们用于填充 DataSet 和更新数据源。</para>
    /// <para>　ExecuteNonQuery：执行数据库语句返回受影响的行数，失败或异常返回-1[通常为:INSERT、DELETE、UPDATE 和 SET 语句等命令]。</para>
    /// <para>　ExecuteScalar：执行数据库语句返回第一行第一列，失败或异常返回null</para>
    /// <para>　ExecuteDataReader：执行数据库语句返回一个自进结果集流</para>
    /// <para>　GetDataTableName：获取Access数据库中的所有表名</para>
    /// </summary>
    public class AccessHelper
    {
        private AccessHelper() { }

        #region 根据MDB文件路径生成OleConnection对象实例
        /// <summary>
        /// 根据MDB文件路径生成OleConnection对象实例
        /// </summary>
        /// <param name="MdbPath">MDB文件相对于站点根目录的路径</param>
        /// <returns></returns>
        public static OleDbConnection CreateConnection(string MdbPath)
        {
            OleDbConnection Connection = null;
            try
            {
                string strConnection = "Provider=Microsoft.Jet.OLEDB.4.0; " +
"Data Source=" + System.Environment.CurrentDirectory + (MdbPath);
                Connection = new OleDbConnection(strConnection);
            }
            catch (Exception)
            {
            }

            return Connection;
        }
        #endregion

        #region 创建一个OleDbCommand对象实例
        /// <summary>
        /// 创建一个OleDbCommand对象实例
        /// </summary>
        /// <param name="CommandText">SQL命令</param>
        /// <param name="Connection">数据库连接对象实例OleDbConnection</param>
        /// <param name="OleDbParameters">可选参数</param>
        /// <returns></returns>
        private static OleDbCommand CreateCommand(string CommandText, OleDbConnection Connection, params System.Data.OleDb.OleDbParameter[] OleDbParameters)
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
            OleDbCommand comm = new OleDbCommand(CommandText, Connection);
            if (OleDbParameters != null)
            {
                foreach (OleDbParameter parm in OleDbParameters)
                {
                    comm.Parameters.Add(parm);
                }
            }
            return comm;
        }
        #endregion

        #region 执行一条SQL语句，返回一个DataSet对象
        /// <summary>
        /// 执行一条SQL语句，返回一个DataSet对象
        /// </summary>
        /// <param name="Connection">OleDbConnection对象</param>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="OleDbParameters">OleDbParameter可选参数</param>
        /// <returns>DataSet对象</returns>
        public static DataSet ExecuteDataSet(OleDbConnection Connection, string CommandText, params OleDbParameter[] OleDbParameters)
        {
            DataSet ds = new DataSet();
            try
            {
                OleDbCommand comm = CreateCommand(CommandText, Connection, OleDbParameters);
                OleDbDataAdapter da = new OleDbDataAdapter(comm);
                da.Fill(ds);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
            }

            return ds;
        }
        #endregion

        #region 执行一条SQL语句,返回一个DataTable对象
        /// <summary>
        /// 执行一条SQL语句,返回一个DataTable对象
        /// </summary>
        /// <param name="Connection">OleDbConnection对象</param>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="OleDbParameters">OleDbParameter可选参数</param>
        /// <returns>DataSet对象</returns>
        public static DataTable ExecuteDataTable(OleDbConnection Connection, string CommandText, params OleDbParameter[] OleDbParameters)
        {
            DataTable Dt = null;
            try
            {
                OleDbCommand comm = CreateCommand(CommandText, Connection, OleDbParameters);
                OleDbDataAdapter da = new OleDbDataAdapter(comm);
                DataSet Ds = new DataSet();
                da.Fill(Ds);
                Dt = Ds.Tables[0];
            }
            catch (Exception)
            {
            }
            finally
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
            }
            return Dt;
        }

        #endregion

        #region 表示一组数据命令和一个数据库连接，它们用于填充 DataSet 和更新数据源。
        /// <summary>
        /// 表示一组数据命令和一个数据库连接，它们用于填充 DataSet 和更新数据源。
        /// </summary>
        /// <param name="Connection">OleDbConnection对象</param>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="OleDbParameters">OleDbParameter可选参数</param>
        /// <returns></returns>
        public static OleDbDataAdapter ExecuteDataAdapter(OleDbConnection Connection, string CommandText, params System.Data.OleDb.OleDbParameter[] OleDbParameters)
        {
            OleDbDataAdapter Da = null;
            try
            {
                OleDbCommand comm = CreateCommand(CommandText, Connection, OleDbParameters);
                Da = new OleDbDataAdapter(comm);
                OleDbCommandBuilder cb = new OleDbCommandBuilder(Da);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
            }
            return Da;
        }
        #endregion

        #region 执行数据库语句返回受影响的行数，失败或异常返回-1[通常为:INSERT、DELETE、UPDATE 和 SET 语句等命令]。
        /// <summary>
        /// 执行数据库语句返回受影响的行数，失败或异常返回-1[通常为:INSERT、DELETE、UPDATE 和 SET 语句等命令]。
        /// </summary>
        /// <param name="Connection">OleDbConnection对象</param>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="OleDbParameters">OleDbParameter可选参数</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(OleDbConnection Connection, string CommandText, params System.Data.OleDb.OleDbParameter[] OleDbParameters)
        {
            int i = -1;
            try
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();
                OleDbCommand comm = CreateCommand(CommandText, Connection, OleDbParameters);
                i = comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
            }
            return i;
        }
        #endregion

        #region 执行数据库语句返回第一行第一列，失败或异常返回null
        /// <summary>
        /// 执行数据库语句返回第一行第一列，失败或异常返回null
        /// </summary>
        /// <param name="Connection">OleDbConnection对象</param>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="OleDbParameters">OleDbParameter可选参数</param>
        /// <returns>第一行第一列的值</returns>
        public static object ExecuteScalar(OleDbConnection Connection, string CommandText, params System.Data.OleDb.OleDbParameter[] OleDbParameters)
        {
            object Result = null;
            try
            {
                OleDbCommand comm = CreateCommand(CommandText, Connection, OleDbParameters);
                Result = comm.ExecuteScalar();
            }
            catch (Exception)
            {
            }
            finally
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
            }
            return Result;
        }
        #endregion

        #region 执行数据库语句返回一个自进结果集流
        /// <summary>
        /// 执行数据库语句返回一个自进结果集流
        /// </summary>
        /// <param name="Connection">OleDbConnection对象</param>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="OleDbParameters">OleDbParameter可选参数</param>
        /// <returns>DataReader对象</returns>
        public static OleDbDataReader ExecuteDataReader(OleDbConnection Connection, string CommandText, params System.Data.OleDb.OleDbParameter[] OleDbParameters)
        {
            OleDbDataReader Odr = null;
            try
            {
                OleDbCommand comm = CreateCommand(CommandText, Connection, OleDbParameters);
                Odr = comm.ExecuteReader();
            }
            catch (Exception)
            {
            }
            finally
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
            }
            return Odr;
        }
        #endregion

        #region 获取Access数据库中的所有表名
        /// <summary>
        /// 获取Access数据库中的所有表名
        /// </summary>
        /// <param name="Connection">OleDbConnection对象</param>
        /// <returns></returns>
        public static DataTable GetDataTableName(OleDbConnection Connection)
        {
            DataTable Dt = null;
            try
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();
                Dt = Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
            }
            return Dt;
        }
        #endregion
    }
}
