namespace Common
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    /// <summary>
    /// ViewModelの基本クラス。INotifyPropertyChangedの実装を提供します。
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// プロパティの変更があったときに発行されます。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public static class PropertyHelper
        {
            // http://blogs.msdn.com/b/csharpfaq/archive/2010/03/11/how-can-i-get-objects-and-property-values-from-expression-trees.aspx
            /// <summary>
            /// 引数で渡されたプロパティから当該プロパティの名前を返します。
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="e"></param>
            /// <returns></returns>
            public static string GetName<T>(Expression<Func<T>> e)
            {
                var member = (MemberExpression)e.Body;
                return member.Member.Name;
            }
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> e)
        {
            RaisePropertyChanged(PropertyHelper.GetName(e));
        }

        /// <summary>
        /// PropertyChangedイベントを発行します。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var h = this.PropertyChanged;
            if (h != null)
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
