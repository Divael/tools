using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Tools.mvvm
{
    /***
     *  private string _leader;
        public string leader {
            get { return _leader; }
            set {
                if (value != _leader)
                {
                    _leader = value;
                    this.NotifyPropertyChanged(()=>leader);
                }
            }
        }
     * */
    /// <summary>
    /// 应用于wpf、winform 窗体控件和后台代码 双向发生变化 同时更新
    /// </summary>
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        /// <summary>
        /// 属性更改通知事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 引发属性更改通知事件
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        public void NotifyPropertyChanged<T>(Expression<Func<T>> property)
        {
            if (PropertyChanged == null)
                return;

            var memberExpression = property.Body as MemberExpression;
            if (memberExpression == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
        }

    }
}
