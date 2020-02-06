using Libs.Prism.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Libs.Prism.Abstracts
{
    public abstract class ValidableModel : BindableModel, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                if (string.IsNullOrEmpty(columnName))
                {
                    throw new ArgumentException("Invalid property name", columnName);
                }
                string error = string.Empty;
                var value = GetValue(columnName);
                var results = new List<ValidationResult>(1);
                var result = Validator.TryValidateProperty(
                    value,
                    new ValidationContext(this, null, null)
                    {
                        MemberName = columnName
                    },
                    results);
                if (!result)
                {
                    var validationResult = results.First();
                    error = validationResult.ErrorMessage;
                }
                return error;
            }
        }

        public bool IsValid => ValidatorHelpers.Validate(GetType(), this);

        public string Error => ValidatorHelpers.GetError(GetType(), this);

        private object GetValue(string propertyName)
        {
            PropertyInfo propInfo = GetType().GetProperty(propertyName);
            return propInfo.GetValue(this);
        }

        public override void RaisePropertyChanged<T>(Expression<Func<T>> expression)
        {
            base.RaisePropertyChanged(expression);
            RaisePropertyChanged("IsValid");
        }
    }
}
