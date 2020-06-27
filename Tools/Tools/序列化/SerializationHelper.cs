using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

namespace Tools.序列化
{
    /// <summary>
    /// 序列化
    /// </summary>
    public class SerializationHelper
    {
        /// <summary>
        /// 枚举,序列化类型，是Soap还是Binary
        /// </summary>
        public enum SerializationFormatterType
        {
            Soap,
            Binary
        }


        /**//// <summary>
            /// 按照串行化的编码要求，生成对应的编码器。
            /// </summary>
            /// <param name="formatterType"></param>
            /// <returns></returns>
        private static IRemotingFormatter GetFormatter(SerializationFormatterType formatterType)
        {
            switch (formatterType)
            {
                case SerializationFormatterType.Binary:
                    return new BinaryFormatter();
                case SerializationFormatterType.Soap:
                    return new SoapFormatter();
                default:
                    throw new NotSupportedException();
            }
        }



        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static string SerializeObjectToString(object graph)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                IRemotingFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, graph);
                Byte[] arrGraph = memoryStream.ToArray();
                return Convert.ToBase64String(arrGraph);
            }
        }


        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="formatterType"></param>
        /// <returns></returns>
        public static string SerializeObjectToString(object graph, SerializationFormatterType formatterType)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                IRemotingFormatter formatter = GetFormatter(formatterType);
                formatter.Serialize(memoryStream, graph);
                Byte[] arrGraph = memoryStream.ToArray();
                return Convert.ToBase64String(arrGraph);
            }
        }


        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializedGraph"></param>
        /// <returns></returns>
        public static T DeserializeStringToObject<T>(string serializedGraph)
        {
            Byte[] arrGraph = Convert.FromBase64String(serializedGraph);
            using (MemoryStream memoryStreamn = new MemoryStream(arrGraph))
            {
                IRemotingFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(memoryStreamn);
            }
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializedGraph"></param>
        /// <param name="formatterType"></param>
        /// <returns></returns>
        public static T DeserializeStringToObject<T>(string serializedGraph, SerializationFormatterType formatterType)
        {
            Byte[] arrGraph = Convert.FromBase64String(serializedGraph);
            using (MemoryStream memoryStream = new MemoryStream(arrGraph))
            {
                IRemotingFormatter formatter = GetFormatter(formatterType);
                return (T)formatter.Deserialize(memoryStream);
            }
        }
    }
}
