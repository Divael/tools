using System;
using System.Windows.Controls;

namespace 工厂模式
{
    class UIFactory
    {
    }

    public abstract class UIHelperabs
    {
        public UserControl UPpage { get; protected set; }
        public UserControl Downpage { get; protected set; }
        public UserControl Curpage { get; protected set; }
        public UserControl Umain { get; protected set; }
        public ContentControl ContentControl { get; protected set; }

        public abstract void SetMainPage(ContentControl ContentControl);
        public abstract void ToPage(UserControl userControl);
        public abstract void ToUp(UserControl userControl);
        public abstract void ToMain();
    }

    public class UIHelper : UIHelperabs
    {
        public override void SetMainPage(ContentControl ContentControl)
        {
            this.ContentControl = ContentControl;
        }

        public override void ToMain()
        {
            this.ContentControl = Umain;
        }

        public override void ToPage(UserControl userControl)
        {
            if (ContentControl != null)
            {
                if (Umain == null)
                {
                    Umain = userControl;
                }

            }
        }

        public override void ToUp(UserControl userControl)
        {
            throw new NotImplementedException();
        }
    }
}
