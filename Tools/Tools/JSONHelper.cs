using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Reflection;
namespace Tools
{
    /// <summary>
    /// <para>　</para>
    /// 　常用工具类——JSON工具类[对于服务器不支持Net3.0以上的可用此类]
    /// <para>　------------------------------------------------</para>
    /// <para>　ToJsonAll：对象转换为Json字符串</para>
    /// <para>　ToString[IEnumerable]：对象集合转换Json</para>
    /// <para>　ToString[DataTable]：Datatable转换为Json</para>
    /// <para>　ToString[DbDataReader]：DataReader转换为Json</para>
    /// <para>　ToString[DataSet]：DataSet转换为Json</para>
    /// <para>　ToArrayString：普通集合转换Json</para>
    /// </summary>
    public class JSONHelper
    {


        #region 对象转换为Json字符串
        /// <summary> 
        /// 对象转换为Json字符串 
        /// </summary> 
        /// <param name="jsonObject">对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJsonAll(object jsonObject)
        {
            string jsonString = "{";
            PropertyInfo[] propertyInfo = jsonObject.GetType().GetProperties();
            for (int i = 0; i < propertyInfo.Length; i++)
            {
                object objectValue = propertyInfo[i].GetGetMethod().Invoke(jsonObject, null);
                string value = string.Empty;
                if (objectValue is DateTime || objectValue is string || objectValue is Guid || objectValue is TimeSpan)
                {
                    value = "'" + objectValue + "'";
                }
                else if (objectValue is IEnumerable)
                {
                    value = ToString((IEnumerable)objectValue);
                }
                else
                {
                    value = objectValue.ToString();
                }
                jsonString += "\"" + propertyInfo[i].Name + "\":" + value + ",";
            }
            return JSONHelper.DeleteLast(jsonString) + "}";
        }
        #endregion

        #region 对象集合转换Json
        /// <summary> 
        /// 对象集合转换Json 
        /// </summary> 
        /// <param name="array">集合对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToString(IEnumerable array)
        {
            string jsonString = "[";
            foreach (object item in array)
            {
                jsonString += JSONHelper.ToJsonAll(item) + ",";
            }
            return JSONHelper.DeleteLast(jsonString) + "]";
        }
        #endregion

        #region 普通集合转换Json
        /// <summary> 
        /// 普通集合转换Json 
        /// </summary> 
        /// <param name="array">集合对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToArrayString(IEnumerable array)
        {
            string jsonString = "[";
            foreach (object item in array)
            {
                jsonString = item + ",";
            }
            return JSONHelper.DeleteLast(jsonString) + "]";
        }
        #endregion

        #region 删除结尾字符 
        /// <summary> 
        /// 删除结尾字符 
        /// </summary> 
        /// <param name="str">需要删除的字符</param> 
        /// <returns>完成后的字符串</returns> 
        private static string DeleteLast(string str)
        {
            if (str.Length > 1)
            {
                return str.Substring(0, str.Length - 1);
            }
            return str;
        }
        #endregion

        #region Datatable转换为Json
        /// <summary> 
        /// Datatable转换为Json 
        /// </summary> 
        /// <param name="table">Datatable对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToString(DataTable table)
        {
            string jsonString = "[";
            DataRowCollection drc = table.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString += "{";
                foreach (DataColumn column in table.Columns)
                {
                    jsonString += "\"" + column.ColumnName + "\":";
                    if (column.DataType == typeof(DateTime) || column.DataType == typeof(string))
                    {
                        jsonString += "\"" + drc[i][column.ColumnName] + "\",";
                    }
                    else
                    {
                        jsonString += drc[i][column.ColumnName] + ",";
                    }
                }
                jsonString = DeleteLast(jsonString) + "},";
            }
            return DeleteLast(jsonString) + "]";
        }
        #endregion

        #region DataReader转换为Json
        /// <summary> 
        /// DataReader转换为Json 
        /// </summary> 
        /// <param name="dataReader">DataReader对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToString(DbDataReader dataReader)
        {
            string jsonString = "[";
            while (dataReader.Read())
            {
                jsonString += "{";

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    jsonString += "\"" + dataReader.GetName(i) + "\":";
                    if (dataReader.GetFieldType(i) == typeof(DateTime) || dataReader.GetFieldType(i) == typeof(string))
                    {
                        jsonString += "\"" + dataReader[i] + "\",";
                    }
                    else
                    {
                        jsonString += dataReader[i] + ",";
                    }
                }
                jsonString = DeleteLast(jsonString) + "}";
            }
            dataReader.Close();
            return DeleteLast(jsonString) + "]";
        }
        #endregion

        #region DataSet转换为Json
        /// <summary> 
        /// DataSet转换为Json 
        /// </summary> 
        /// <param name="dataSet">DataSet对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToString(DataSet dataSet)
        {
            string jsonString = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                jsonString += "\"" + table.TableName + "\":" + ToString(table) + ",";
            }
            return jsonString = DeleteLast(jsonString) + "}";
        }
        #endregion

        //#region JSON Data Get

        ///// <summary>
        ///// 一个泛型方法，提供json对象的数据读取 ->
        ///// A generic method that provides data read for a JSON object
        ///// </summary>
        ///// <typeparam name="T">读取的泛型</typeparam>
        ///// <param name="json">json对象</param>
        ///// <param name="value_name">值名称</param>
        ///// <param name="default_value">默认值</param>
        ///// <returns>值对象</returns>
        ///// <example>
        ///// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="GetValueFromJsonObjectExample" title="GetValueFromJsonObject示例" />
        ///// </example>
        //public static T GetValueFromJsonObject<T>(JObject json, string value_name, T default_value)
        //{
        //    if (json.Property(value_name) != null)
        //    {
        //        return json.Property(value_name).Value.Value<T>();
        //    }
        //    else
        //    {
        //        return default_value;
        //    }
        //}



        ///// <summary>
        ///// 一个泛型方法，提供json对象的数据写入 ->
        ///// A generic method that provides data writing to a JSON object
        ///// </summary>
        ///// <typeparam name="T">写入的泛型</typeparam>
        ///// <param name="json">json对象</param>
        ///// <param name="property">值名称</param>
        ///// <param name="value">值数据</param>
        ///// <example>
        ///// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="JsonSetValueExample" title="JsonSetValue示例" />
        ///// </example>
        //public static void JsonSetValue<T>(JObject json, string property, T value)
        //{
        //    if (json.Property(property) != null)
        //    {
        //        json.Property(property).Value = new JValue(value);
        //    }
        //    else
        //    {
        //        json.Add(property, new JValue(value));
        //    }
        //}


        //#endregion
    }
}
