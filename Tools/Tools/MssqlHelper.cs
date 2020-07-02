using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Tools
{
    /// <summary>
    /// <para>　</para>
    /// 　常用工具类——MSSQL数据库操作类
    /// <para>　-------------------------------------------------</para>
    /// <para>　GetConnectionString：读取Web.Config中的连接字符串</para>
    /// <para>　CreateConnection：生成IdbConnection 对象</para>
    /// <para>　ExecuteNonQuery：执行数据库语句返回受影响的行数，失败或异常返回-1 [ +2 重载 ]</para>
    /// <para>　ExecuteScalar：执行数据库语句返回第一行第一列，失败或异常返回null</para>
    /// <para>　ExecuteDataTable：执行数据库语句返回第一个内存表</para>
    /// <para>　ExecuteDataSet：执行数据库语句返回第一个DataSet</para>
    /// <para>　ExecuteReader：执行数据库语句返回一个自进结果集流</para>
    /// <para>　GetDataTableName：获取数据库全部列表</para>
    /// </summary>
    public sealed class MssqlHelper
    {
        private MssqlHelper() { }

        #region 读取应用程序配置中的连接字符串
        /// <summary>
        /// 读取Web.Config中的连接字符串
        /// </summary>
        /// <param name="Name">字符串的name值</param>
        /// <returns>字符串name值对应的连接字符串</returns>
        public static string GetConnectionString(string Name)
        {
            return ConfigurationManager.ConnectionStrings[Name].ConnectionString;
        }
        #endregion

        #region 生成IdbConnection 对象
        /// <summary>
        /// 生成IdbConnection 对象
        /// </summary>
        /// <param name="ConnStr">数据库连接字符串</param>
        /// <returns></returns>
        public static SqlConnection CreateConnection(string ConnStr)
        {
            SqlConnection Connss = null;
            try
            {
                Connss = new SqlConnection(ConnStr);
            }
            catch (Exception ex)
            {
                ex.logThis();
            }
            return Connss;
        }
        #endregion

        #region  向命令添加参数
        /// <summary>
        /// 向命令添加参数
        /// </summary>
        /// <param name="Command">IDbCommand对象</param>
        /// <param name="CommandParameters">要添加的参数</param>
        private static void AttachParameters(IDbCommand Command, IDataParameter[] CommandParameters)
        {
            if (Command == null) throw new ArgumentNullException("创建数据库命令对象（IDbCommand对象）时失败！");
            if (CommandParameters != null)
            {
                foreach (IDataParameter p in CommandParameters)
                {
                    if (p != null)
                    {
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        Command.Parameters.Add(p);
                    }
                }
            }
        }
        #endregion

        #region  初始化命令
        /// <summary>
        /// 初始化命令
        /// </summary>
        /// <param name="Command">IDbCommand对象</param>
        /// <param name="Connection">数据库连接对象</param>
        /// <param name="CommandType">SQL语句类型</param>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="CommandParameters">数据库参数</param>
        /// <param name="MustCloseConnection">返回一个bool值，如果是方法内部打开的连接则返回true，否则返回false</param>
        private static void PrepareCommand(IDbCommand Command, IDbConnection Connection, CommandType CommandType, string CommandText, IDataParameter[] CommandParameters, out bool MustCloseConnection)
        {
            if (Command == null) throw new ArgumentNullException("创建数据库命令对象（IDbCommand对象）时失败！");
            if (CommandText == null || CommandText.Length == 0) throw new ArgumentNullException("SQL语句为空");
            if (Connection.State != ConnectionState.Open)
            {
                MustCloseConnection = true;
                Connection.Open();
            }
            else
            {
                MustCloseConnection = false;
            }
            Command.Connection = Connection;
            Command.CommandText = CommandText;
            Command.CommandType = CommandType;
            if (CommandParameters != null)
            {
                AttachParameters(Command, CommandParameters);
            }
            return;
        }
        #endregion

        #region  执行数据库语句返回受影响的行数，失败或异常返回-1
        /// <summary>
        /// 执行数据库语句返回受影响的行数，失败或异常返回-1
        /// </summary>
        /// <param name="Connection">数据库连接对象</param>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="CommandType">SQL语句类型</param>
        /// <param name="Parameter">数据库参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(IDbConnection Connection, string CommandText, CommandType CommandType, params IDataParameter[] Parameter)
        {
            if (Connection == null || string.IsNullOrEmpty(Connection.ConnectionString))
            {
                throw new ArgumentException("connection参数为null或者提供的连接字符串为空！");
            }
            bool mustCloseConnection = false;
            int result = 0;
            IDbCommand command = Connection.CreateCommand();
            PrepareCommand(command, Connection, CommandType, CommandText, Parameter, out mustCloseConnection);
            result = command.ExecuteNonQuery();
            command.Parameters.Clear();
            if (mustCloseConnection)
                Connection.Close();
            return result;
        }
        /// <summary>
        /// 执行数据库语句返回受影响的行数，失败或异常返回-1
        /// </summary>
        /// <param name="Tans">数据库事务对象</param>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="CommandType">SQL语句类型</param>
        /// <param name="Parameter">数据库参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlTransaction Tans, string CommandText, CommandType CommandType, params IDataParameter[] Parameter)
        {
            bool mustCloseConnection = false;
            int result = 0;
            SqlCommand command = new SqlCommand();
            PrepareCommand(command, Tans.Connection, CommandType, CommandText, Parameter, out mustCloseConnection);
            result = command.ExecuteNonQuery();
            command.Parameters.Clear();
            command.Dispose();
            return result;
        }
        #endregion

        #region 执行数据库语句返回第一行第一列，失败或异常返回null
        /// <summary>
        /// 执行数据库语句返回第一行第一列，失败或异常返回null
        /// </summary>
        /// <param name="Connection">数据库连接对象</param>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="CommandType">SQL语句类型</param>
        /// <param name="Parameter">数据库参数</param>
        /// <returns></returns>
        public static object ExecuteScalar(IDbConnection Connection, string CommandText, CommandType CommandType, params IDataParameter[] Parameter)
        {
            if (Connection == null || string.IsNullOrEmpty(Connection.ConnectionString))
            {
                throw new ArgumentException("connection参数为null或者提供的连接字符串为空！");
            }
            bool mustCloseConnection = false;
            object result = null;
            IDbCommand command = Connection.CreateCommand();
            PrepareCommand(command, Connection, CommandType, CommandText, Parameter, out mustCloseConnection);
            result = command.ExecuteScalar();
            command.Parameters.Clear();
            if (mustCloseConnection)
                Connection.Close();
            return result;
        }
        #endregion

        #region 执行数据库语句返回第一个内存表
        /// <summary>
        /// 执行数据库语句返回第一个内存表
        /// </summary>
        /// <param name="Connection">数据库连接对象</param>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="CommandType">SQL语句类型</param>
        /// <param name="Parameter">数据库参数</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(IDbConnection Connection, string CommandText, CommandType CommandType, params IDataParameter[] Parameter)
        {
            if (Connection == null || string.IsNullOrEmpty(Connection.ConnectionString))
            {
                throw new ArgumentException("connection参数为null或者提供的连接字符串为空！");
            }
            DataTable dataTable = new DataTable();
            dataTable.Load(ExecuteReader(Connection, CommandText, CommandType, Parameter));
            return dataTable;
        }
        #endregion

        #region 执行数据库语句返回第一个DataSet
        /// 执行数据库语句返回第一个DataSet
        /// <summary>
        /// <param name="Connection">数据库连接对象</param>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="CommandType">SQL语句类型</param>
        /// <param name="Parameter">数据库参数</param>
        /// <returns></returns>
        /// </summary>
        public static DataSet ExecuteDataSet(IDbConnection Connection, string CommandText, CommandType CommandType, params IDataParameter[] Parameter)
        {
            DataSet DS = null;
            if (Connection == null || string.IsNullOrEmpty(Connection.ConnectionString))
            {
                throw new ArgumentException("connection参数为null或者提供的连接字符串为空！");
            }
            else
            {
                DS = new DataSet();
                DS.Load(ExecuteReader(Connection, CommandText, CommandType, Parameter), LoadOption.OverwriteChanges, new string[] { "TableName" });
            }
            return DS;
        }
        #endregion

        #region 执行数据库语句返回一个自进结果集流
        /// <summary>
        /// 执行数据库语句返回一个自进结果集流
        /// </summary>
        /// <param name="Connection">数据库连接对象</param>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="CommandType">SQL语句类型</param>
        /// <param name="Parameter">数据库参数</param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(IDbConnection Connection, string CommandText, CommandType CommandType, params IDataParameter[] Parameter)
        {
            if (Connection == null || string.IsNullOrEmpty(Connection.ConnectionString))
            {
                throw new ArgumentException("connection参数为null或者提供的连接字符串为空！");
            }
            bool mustCloseConnection = false;
            IDbCommand command = Connection.CreateCommand();
            PrepareCommand(command, Connection, CommandType, CommandText, Parameter, out mustCloseConnection);
            IDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            bool canClear = true;
            foreach (IDataParameter commandParameter in command.Parameters)
            {
                if (commandParameter.Direction != ParameterDirection.Input)
                    canClear = false;
            }
            if (canClear)
            {
                command.Parameters.Clear();
            }
            return dataReader;
        }
        #endregion     

        #region 获取数据库全部列表
        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <param name="Connection">连接Connection对象</param>
        /// <returns></returns>
        public static DataTable GetDataTableName(SqlConnection Connection)
        {
            DataTable Dt = null;
            try
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();
                Dt = Connection.GetSchema("Tables");
            }
            catch (Exception)
            { }
            finally
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
            }
            return Dt;
        }
        #endregion
    }
}
