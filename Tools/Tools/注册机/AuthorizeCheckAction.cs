using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools.注册机
{
    public class AuthorizeCheckAction
    {

        private static object lockobj = new object();
        private static AuthorizeCheckAction sClass;
        private string MechName { get; set; }
        private string DeviceId { get; set; }

        public static AuthorizeCheckAction getInstance()
        {
            if (sClass == null)
            {
                lock (lockobj)
                {
                    return sClass ?? (sClass = new AuthorizeCheckAction());
                }
            }
            return sClass;
        }

        private AuthorizeCheckAction() {
        }

        public AuthorizeCheckAction init(string mechName,string deviceId) {
            MechName = mechName;
            DeviceId = deviceId;
            return this;
        }




        private SoftAuthorize softAuthorize = null;


        public bool AuthorizeCheck()
        {
            softAuthorize = new SoftAuthorize();
            softAuthorize.FileSavePath = Application.StartupPath + @"\Authorize.txt"; // 设置存储激活码的文件，该存储是加密的
            softAuthorize.LoadByFile();

            // 检测激活码是否正确，没有文件，或激活码错误都算作激活失败
            if (!softAuthorize.IsAuthorizeSuccess(AuthorizeEncrypted))
            {
                // 显示注册窗口
                Console.WriteLine(AuthorizeEncrypted(softAuthorize.GetMachineCodeString())); ;

                using (FormAuthorize form =
                    new FormAuthorize(
                        softAuthorize,
                        "请联系【简普智能0519-85858817】获取激活码",
                        AuthorizeEncrypted))
                {
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        // 授权失败，退出
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 一个自定义的加密方法，传入一个原始数据，返回一个加密结果
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        private string AuthorizeEncrypted(string origin)
        {
            // 此处使用了组件支持的DES对称加密技术
            string code = origin + MechName + DeviceId;
            return Tools.EncyptHelper.MD5Encrypt(code);
        }
    }
}
