using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;

namespace Connector.CcvShop.Core
{
    //TODO: think of a better way to compare what has changed
    public class Compare
    {
        /// <summary>
        /// Tracks whether a value has been set or not and only returns the values that are set.
        /// Drawback of this is that every value type must be Nullable, if these aren't, the default value can for instance be 0 for an int. If you wanted to set the int to a 0, it will not be used.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static object GetChanges<T>(T item)
        {
            Dictionary<string, string> nameValues = new Dictionary<string, string>();
            var properties = item.GetType().GetProperties();
            foreach (var property in properties)
            {
                object value = property.GetValue(item);
                if (value == null)
                    continue;

                var defaultValue = GetDefaultValue(property);
                if(!value.Equals(defaultValue))
                    nameValues.Add(property.Name, value.ToString());
            }

            return nameValues;
        }

        static object GetDefaultValue(PropertyInfo prop)
        {
            if (prop.PropertyType.IsValueType)
                return Activator.CreateInstance(prop.PropertyType);
            return null;
        }
    }
}
