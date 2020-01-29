using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Libs.Prism.Helpers
{
    public class ValidatorHelpers
    {
        public static bool Validate(Type type, IDataErrorInfo obj) => !type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Any(prop => !string.IsNullOrEmpty(obj[prop.Name]));
        public static string GetError(Type type, IDataErrorInfo obj) => type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance) is IEnumerable<PropertyInfo> infos && infos.Any() ? infos.Select(prop => obj[prop.Name]).Aggregate((a, b) => $"{a}\n{b}") : null;
    }
}
