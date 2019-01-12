using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;


namespace Tools
{
    /// <summary>
    /// <para>　</para>
    /// 　常用工具类——注册表操作类
    /// <para>　------------------------------------------</para>
    /// <para>　GetRegValue：读取路径为不包含根键的keypath，键名为keyname的注册表键值，缺省返回null</para>
    /// <para>　SetRegValue：设置路径为不包含根键的keypath，键名为keyname的注册表键值为SetValue，失败返回False</para>
    /// <para>　CreateRegPath：创建不含根键的路径为regpath的路径</para>
    /// <para>　CreateRegKeyPath： 创建一个注册表路径，新建一个键名，并设置键值</para>
    /// <para>　DeleteSubKey：删除路径为SubKey的子项</para>
    /// <para>　DeleteSubKeyTree：删除路径为SubPath的子项及其附属子项</para>
    /// <para>　DeleteRegKeyName：删除指定键名的键名</para>
    /// </summary>
    public class RegistryHelper
    {
        #region 当前操作的注册表根键名称枚举
        /// <summary>
        /// 当前操作的注册表根键
        /// </summary>
        public enum RootKey
        {
            /// <summary>
            /// 该主键包含了文件的扩展名和应用程序的关联信息以及Window Shell和OLE用于储存注册表的信息。该主键下的子键决定了在WINDOWS中如何显示该类文件以及他们的图标，该主键是从HKEY_LCCAL_MACHINE\SOFTWARE\Classes映射过来的
            /// </summary>
            ClassesRoot,
            /// <summary>
            /// 该主键包含了如用户窗口信息，桌面设置等当前用户的信息
            /// </summary>
            CurrentUser,
            /// <summary>
            /// 主键包含了计算机软件和硬件的安装和配置信息，该信息可供所有用户使用
            /// </summary>
            LocalMachine,
            /// <summary>
            /// 该主键记录了当前用户的设置信息，每次用户登入系统时，就会在该主键下生成一个与用户登入名一样的子键，该子键保存了当前用户的桌面设置、背景位图、快捷键，字体等信息。一般应用程序不直接访问改主键，而是通过主键HKEY_CURRENT_USER进行访问
            /// </summary>
            Users,
            /// <summary>
            /// 该主键保存了计算机当前硬件的配置信息，这些配置可以根据当前所连接的网络类型或硬件驱动软件安装的改变而改变
            /// </summary>
            CurrentConfig,
            /// <summary>
            /// 
            /// </summary>
            DynData,
            /// <summary>
            /// 
            /// </summary>
            PerformanceData
        }
        #endregion
        #region 注册键值
        /// <summary>
        /// 注册键值
        /// </summary>
        /// <param name="RootKey">根键名称</param>
        /// <returns></returns>
        private static RegistryKey RegistryEx(RootKey RootKey)
        {
            RegistryKey RegKey = null;
            switch (RootKey)
            {
                case RootKey.ClassesRoot:
                    RegKey = Registry.ClassesRoot;
                    break;
                case RootKey.CurrentUser:
                    RegKey = Registry.CurrentUser;
                    break;
                case RootKey.LocalMachine:
                    RegKey = Registry.LocalMachine;
                    break;
                case RootKey.Users:
                    RegKey = Registry.Users;
                    break;
                case RootKey.CurrentConfig:
                    RegKey = Registry.CurrentConfig;
                    break;
                //已过时
                //case RootKey.DynData:
                //    RegKey = Registry.DynData;
                //    break;
                case RootKey.PerformanceData:
                    RegKey = Registry.PerformanceData;
                    break;
                default:
                    RegKey = Registry.CurrentUser;
                    break;
            }
            return RegKey;
        }
        #endregion

        #region 读取路径为不包含根键的keypath，键名为keyname的注册表键值，缺省返回null
        /// <summary>
        /// 读取路径为不包含根键的keypath，键名为keyname的注册表键值，缺省返回null
        /// </summary>
        /// <param name="RootKey">根键枚举</param>
        /// <param name="KeyPath">健路径(不含根键)</param>
        /// <param name="KeyName">键名</param>
        /// <returns>读取到的注册表键值</returns>
        public static string GetRegValue(RootKey RootKey, string KeyPath, string KeyName)
        {
            string BackMsg = null;
            try
            {
                RegistryKey Key = RegistryEx(RootKey).OpenSubKey(KeyPath);
                BackMsg= Key.GetValue(KeyName, null).ToString();
            }
            catch (Exception)
            {
            }
            return BackMsg;
        }
        #endregion

        #region 设置路径为不包含根键的keypath，键名为keyname的注册表键值为SetValue，失败返回False
        /// <summary>
        /// 设置路径为不包含根键的keypath，键名为keyname的注册表键值为SetValue，失败返回False
        /// </summary>
        /// <param name="RootKey">根键枚举</param>
        /// <param name="KeyPath">健路径(不含根键)</param>
        /// <param name="KeyName">键名</param>
        /// <param name="KeyValue">键值</param>
        /// <returns></returns>
        public static bool SetRegValue(RootKey RootKey,string KeyPath,string KeyName,object KeyValue)
        {
            bool flag = false;
            try
            {
                RegistryKey Key = RegistryEx(RootKey).OpenSubKey(KeyPath, true);
                Key.SetValue(KeyName,KeyValue);
                flag=true;
            }
            catch (Exception)
            {
            }
            return flag;
        }
        #endregion

        #region 创建不含根键的路径为regpath的路径
        /// <summary>
        /// 创建不含根键的路径为regpath的路径
        /// </summary>
        /// <param name="RootKey">根键枚举</param>
        /// <param name="RegPath">要创建的键值路径</param>
        /// <returns></returns>
        public static bool CreateRegPath(RootKey RootKey, string RegPath)
        {
            bool flag = false;
            try
            {
                RegistryEx(RootKey).CreateSubKey(RegPath);
                flag = true;
            }
            catch (Exception)
            {
            }
            return flag;
        }
        #endregion

        #region 创建一个注册表路径，新建一个键名，并设置键值
        /// <summary>
        /// 创建一个注册表路径，新建一个键名，并设置键值
        /// </summary>
        /// <param name="RootKey">根键枚举</param>
        /// <param name="RegPath">要创建的键路径</param>
        /// <param name="KeyName">要创建的键名</param>
        /// <param name="KeyValue">键名的键值</param>
        /// <returns></returns>
        public static bool CreateRegKeyPath(RootKey RootKey, string RegPath, string KeyName, object KeyValue)
        {
            bool flag = false;
            try
            {
                RegistryEx(RootKey).CreateSubKey(RegPath);
                RegistryKey Key = RegistryEx(RootKey).OpenSubKey(RegPath,true);
                Key.SetValue(KeyName, KeyValue);
                flag = true;
            }
            catch (Exception)
            {
            }
            return flag;
        }
        #endregion

        #region 删除路径为SubKey的子项
        /// <summary>
        /// 删除路径为SubKey的子项
        /// </summary>
        /// <param name="RootKey">根键枚举</param>
        /// <param name="SubPath">要删除的键路径</param>
        /// <returns></returns>
        public static bool DeleteSubKey(RootKey RootKey, string SubPath)
        {
            bool flag = false;
            try
            {
                RegistryEx(RootKey).DeleteSubKey(SubPath);
                flag = true;
            }
            catch (Exception)
            {
            }
            return flag;
        }
        #endregion

        #region 删除路径为SubPath的子项及其附属子项
        /// <summary>
        /// 删除路径为SubPath的子项及其附属子项
        /// </summary>
        /// <param name="RootKey">根键枚举</param>
        /// <param name="SubPath">要删除的路径</param>
        /// <returns></returns>
        public static bool DeleteSubKeyTree(RootKey RootKey,string SubPath)
        {
            bool flag = false;
            try
            {
                RegistryEx(RootKey).DeleteSubKeyTree(SubPath);
                flag = true;
            }
            catch (Exception)
            {
            }
            return flag;
        }
        #endregion

        #region 删除指定键名的键名
        /// <summary>
        /// 删除指定键名的键名
        /// </summary>
        /// <param name="RootKey">根键枚举</param>
        /// <param name="SubPath">路径</param>
        /// <param name="KeyName">键名</param>
        /// <returns></returns>
        public static bool DeleteRegKeyName(RootKey RootKey, string SubPath, string KeyName)
        {
            bool flag = false;
            try
            {
                RegistryKey Key = RegistryEx(RootKey).OpenSubKey(SubPath, true);
                Key.DeleteValue(KeyName);
                flag = true;
            }
            catch (Exception)
            {
            }
            return flag;
        }
        #endregion

    }
}
