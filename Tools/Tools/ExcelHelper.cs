using Microsoft.Office.Core;
using System;
using System.Data;
using System.Data.OleDb;

namespace Tools
{
    /// <summary>
    /// <para>　</para>
    /// 常用工具类——Excel操作类
    /// <para>　------------------------------------------------</para>
    /// <para>　CreateConnection：根据Excel文件路径和EXCEL驱动版本生成OleConnection对象实例</para>
    /// <para>　ExecuteDataSet：执行一条SQL语句，返回一个DataSet对象</para>
    /// <para>　ExecuteDataTable：执行一条SQL语句，返回一个DataTable对象</para>
    /// <para>　ExecuteDataAdapter：表示一组数据命令和一个数据库连接，它们用于填充 DataSet 和更新数据源。</para>
    /// <para>　ExecuteNonQuery：执行数据库语句返回受影响的行数，失败或异常返回-1[通常为:INSERT、DELETE、UPDATE 和 SET 语句等命令]。</para>
    /// <para>　ExecuteScalar：执行数据库语句返回第一行第一列，失败或异常返回null</para>
    /// <para>　ExecuteDataReader：执行数据库语句返回一个自进结果集流</para>
    /// <para>　Create：获取Excel中的所有工作簿</para>
    /// <para>　GetWorkBookName：获取Excel中的所有工作簿</para>
    /// <para>　GetWorkBookName：获取Excel中的所有工作簿</para>
    /// <para>　GetWorkBookName：获取Excel中的所有工作簿</para>
    /// <para>　GetWorkBookName：获取Excel中的所有工作簿</para>
    /// <para>　GetWorkBookName：获取Excel中的所有工作簿</para>
    /// <para>　GetWorkBookName：获取Excel中的所有工作簿</para>
    /// <para>　GetWorkBookName：获取Excel中的所有工作簿</para>
    /// </summary>
    public class ExcelHelper
    {
        public ExcelHelper() { }

        #region EXCEL版本
        /// <summary>
        /// EXCEL版本
        /// </summary>
        public enum ExcelVerion
        {
            /// <summary>
            /// Excel97-2003版本
            /// </summary>
            Excel2003,
            /// <summary>
            /// Excel2007版本
            /// </summary>
            Excel2007
        }
        #endregion

        #region 根据EXCEL路径生成OleDbConnectin对象
        /// <summary>
        /// 根据EXCEL路径生成OleDbConnectin对象
        /// </summary>
        /// <param name="ExcelFilePath">EXCEL文件相对于站点根目录的路径</param>
        /// <param name="Verion">Excel数据驱动版本：97-2003或2007,分别需要安装数据驱动软件</param>
        /// <returns>OleDbConnection对象</returns>
        public static OleDbConnection CreateConnection(string ExcelFilePath, ExcelVerion Verion)
        {
            OleDbConnection Connection = null;
            string strConnection = string.Empty;
            try
            {
                switch (Verion)
                {
                    case ExcelVerion.Excel2003: //读取Excel97-2003版本
                        strConnection = "Provider=Microsoft.Jet.OLEDB.4.0; " +
"Data Source=" + System.Environment.CurrentDirectory + (ExcelFilePath) + ";Extended Properties=Excel 8.0";
                        break;
                    case ExcelVerion.Excel2007: //读取Excel2007版本
                        strConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 12.0;HDR=YES';data source=" + ExcelFilePath;
                        break;
                }
                if (!string.IsNullOrEmpty(strConnection)) Connection = new OleDbConnection(strConnection);
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

        #region 获取Excel中的所有工作簿
        /// <summary>
        /// 获取Excel中的所有工作簿
        /// </summary>
        /// <param name="Connection">OleDbConnection对象</param>
        /// <returns></returns>
        public static DataTable GetWorkBookName(OleDbConnection Connection)
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

        public string mFilename;
        public Microsoft.Office.Interop.Excel.Application app;
        public Microsoft.Office.Interop.Excel.Workbooks wbs;
        public Microsoft.Office.Interop.Excel.Workbook wb;
        public Microsoft.Office.Interop.Excel.Worksheets wss;
        public Microsoft.Office.Interop.Excel.Worksheet ws;

        /// <summary>
        /// 创建一个Microsoft.Office.Interop.Excel对象
        /// </summary>
        public void Create()//创建一个Microsoft.Office.Interop.Excel对象
        {
            app = new Microsoft.Office.Interop.Excel.Application();
            wbs = app.Workbooks;
            wb = wbs.Add(true);
        }
        /// <summary>
        /// 打开一个Microsoft.Office.Interop.Excel文件
        /// </summary>
        /// <param name="FileName"></param>
        public void Open(string FileName)//打开一个Microsoft.Office.Interop.Excel文件
        {
            app = new Microsoft.Office.Interop.Excel.Application();
            wbs = app.Workbooks;
            wb = wbs.Add(FileName);
            //wb = wbs.Open(FileName, 0, true, 5,"", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "t", false, false, 0, true,Type.Missing,Type.Missing);
            //wb = wbs.Open(FileName,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing);
            mFilename = FileName;
        }
        /// <summary>
        /// 获取一个工作表
        /// </summary>
        /// <param name="SheetName">工作表名称默认Sheet1</param>
        /// <returns></returns>
        public Microsoft.Office.Interop.Excel.Worksheet GetSheet(string SheetName)
        //获取一个工作表
        {
            Microsoft.Office.Interop.Excel.Worksheet s = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[SheetName];
            return s;
        }
        /// <summary>
        /// 添加一个工作表
        /// </summary>
        /// <param name="SheetName"></param>
        /// <returns></returns>
        public Microsoft.Office.Interop.Excel.Worksheet AddSheet(string SheetName)
        //添加一个工作表
        {
            Microsoft.Office.Interop.Excel.Worksheet s = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            s.Name = SheetName;
            return s;
        }
        /// <summary>
        /// 删除一个工作表
        /// </summary>
        /// <param name="SheetName"></param>
        public void DelSheet(string SheetName)//删除一个工作表
        {
            ((Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[SheetName]).Delete();
        }
        /// <summary>
        /// 重命名一个工作表一
        /// </summary>
        /// <param name="OldSheetName">Sheet1</param>
        /// <param name="NewSheetName"></param>
        /// <returns></returns>
        public Microsoft.Office.Interop.Excel.Worksheet ReNameSheet(string OldSheetName, string NewSheetName)//重命名一个工作表一
        {
            Microsoft.Office.Interop.Excel.Worksheet s = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[OldSheetName];
            s.Name = NewSheetName;
            return s;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sheet"></param>
        /// <param name="NewSheetName"></param>
        /// <returns></returns>
        public Microsoft.Office.Interop.Excel.Worksheet ReNameSheet(Microsoft.Office.Interop.Excel.Worksheet Sheet, string NewSheetName)//重命名一个工作表二
        {

            Sheet.Name = NewSheetName;

            return Sheet;
        }

        public void SetCellValue(Microsoft.Office.Interop.Excel.Worksheet ws, int x, int y, object value)
        //ws：要设值的工作表     X行Y列     value   值
        {
            ws.Cells[x, y] = value;
        }
        public void SetCellValue(string ws, int x, int y, object value)
        //ws：要设值的工作表的名称 X行Y列 value 值
        {

            GetSheet(ws).Cells[x, y] = value;
        }

        public void SetCellProperty(Microsoft.Office.Interop.Excel.Worksheet ws, int Startx, int Starty, int Endx, int Endy, int size, string name, Microsoft.Office.Interop.Excel.Constants color, Microsoft.Office.Interop.Excel.Constants HorizontalAlignment)
        //设置一个单元格的属性   字体，   大小，颜色   ，对齐方式
        {
            name = "宋体";
            size = 12;
            color = Microsoft.Office.Interop.Excel.Constants.xlAutomatic;
            HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Name = name;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Size = size;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Color = color;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).HorizontalAlignment = HorizontalAlignment;
        }

        public void SetCellProperty(string wsn, int Startx, int Starty, int Endx, int Endy, int size, string name, Microsoft.Office.Interop.Excel.Constants color, Microsoft.Office.Interop.Excel.Constants HorizontalAlignment)
        {
            //name = "宋体";
            //size = 12;
            //color = Microsoft.Office.Interop.Excel.Constants.xlAutomatic;
            //HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight;

            Microsoft.Office.Interop.Excel.Worksheet ws = GetSheet(wsn);
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Name = name;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Size = size;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Color = color;

            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).HorizontalAlignment = HorizontalAlignment;
        }


        public void UniteCells(Microsoft.Office.Interop.Excel.Worksheet ws, int x1, int y1, int x2, int y2)
        //合并单元格
        {
            ws.get_Range(ws.Cells[x1, y1], ws.Cells[x2, y2]).Merge(Type.Missing);
        }

        public void UniteCells(string ws, int x1, int y1, int x2, int y2)
        //合并单元格
        {
            GetSheet(ws).get_Range(GetSheet(ws).Cells[x1, y1], GetSheet(ws).Cells[x2, y2]).Merge(Type.Missing);

        }


        public void InsertTable(System.Data.DataTable dt, string ws, int startX, int startY)
        //将内存中数据表格插入到Microsoft.Office.Interop.Excel指定工作表的指定位置 为在使用模板时控制格式时使用一
        {

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    GetSheet(ws).Cells[startX + i, j + startY] = dt.Rows[i][j].ToString();
                }
            }
        }

        public void AddTable(System.Data.DataTable dt, Microsoft.Office.Interop.Excel.Worksheet ws, int startX, int startY)
        //将内存中数据表格添加到Microsoft.Office.Interop.Excel指定工作表的指定位置二
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    ws.Cells[i + startX, j + startY] = dt.Rows[i][j].ToString();
                }
            }

        }

        public void InsertPictures(string Filename, string ws)
        //插入图片操作一
        {
            GetSheet(ws).Shapes.AddPicture(Filename, MsoTriState.msoFalse, MsoTriState.msoTrue, 10, 10, 150, 150);
            //后面的数字表示位置
        }

        //public void InsertPictures(string Filename, string ws, int Height, int Width)
        //插入图片操作二
        //{
        //    GetSheet(ws).Shapes.AddPicture(Filename, MsoTriState.msoFalse, MsoTriState.msoTrue, 10, 10, 150, 150);
        //    GetSheet(ws).Shapes.get_Range(Type.Missing).Height = Height;
        //    GetSheet(ws).Shapes.get_Range(Type.Missing).Width = Width;
        //}
        //public void InsertPictures(string Filename, string ws, int left, int top, int Height, int Width)
        //插入图片操作三
        //{

        //    GetSheet(ws).Shapes.AddPicture(Filename, MsoTriState.msoFalse, MsoTriState.msoTrue, 10, 10, 150, 150);
        //    GetSheet(ws).Shapes.get_Range(Type.Missing).IncrementLeft(left);
        //    GetSheet(ws).Shapes.get_Range(Type.Missing).IncrementTop(top);
        //    GetSheet(ws).Shapes.get_Range(Type.Missing).Height = Height;
        //    GetSheet(ws).Shapes.get_Range(Type.Missing).Width = Width;
        //}

        public void InsertActiveChart(Microsoft.Office.Interop.Excel.XlChartType ChartType, string ws, int DataSourcesX1, int DataSourcesY1, int DataSourcesX2, int DataSourcesY2, Microsoft.Office.Interop.Excel.XlRowCol ChartDataType)
        //插入图表操作
        {
            ChartDataType = Microsoft.Office.Interop.Excel.XlRowCol.xlColumns;
            wb.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            {
                wb.ActiveChart.ChartType = ChartType;
                wb.ActiveChart.SetSourceData(GetSheet(ws).get_Range(GetSheet(ws).Cells[DataSourcesX1, DataSourcesY1], GetSheet(ws).Cells[DataSourcesX2, DataSourcesY2]), ChartDataType);
                wb.ActiveChart.Location(Microsoft.Office.Interop.Excel.XlChartLocation.xlLocationAsObject, ws);
            }
        }
        public bool Save()
        //保存文档
        {
            if (mFilename == "")
            {
                return false;
            }
            else
            {
                try
                {
                    wb.Save();
                    return true;
                }

                catch (Exception ex)
                {
                    Loger.err("", ex);
                    return false;
                }
            }
        }
        public bool SaveAs(object FileName)
        //文档另存为
        {
            try
            {
                wb.SaveAs(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                return true;

            }

            catch (Exception ex)
            {
                Loger.err("", ex);

                return false;

            }
        }
        public void Close()
        //关闭一个Microsoft.Office.Interop.Excel对象，销毁对象
        {
            //wb.Save();
            wb.Close(Type.Missing, Type.Missing, Type.Missing);
            wbs.Close();
            app.Quit();
            wb = null;
            wbs = null;
            app = null;
            GC.Collect();
        }
    }
}
