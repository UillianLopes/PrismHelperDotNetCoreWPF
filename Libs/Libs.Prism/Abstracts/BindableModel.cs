using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Libs.Prism.Abstracts
{
    public abstract class BindableModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void RaisePropertyChanged<T>(Expression<Func<T>> expression)
        {
            var body = expression.Body as MemberExpression;
            var member = body.Member;
            RaisePropertyChanged(member.Name);
        }

        protected void RaisePropertyChanged(string name) => PropertyChanged?
            .Invoke(this, new PropertyChangedEventArgs(name));

    }
}
