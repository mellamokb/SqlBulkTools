using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SqlBulkTools
{
    internal static class PropInfoList
    {
        public static List<PropInfo> ToPropInfoList(this Type type) =>
            type.GetProperties().OrderBy(x => x.Name).Select(p => new PropInfo(p)).ToList();

        public static List<PropInfo> ToPropInfoList(this Dictionary<string, Type> dictionary) =>
            dictionary.Select(p => new PropInfo(p.Key, p.Value)).ToList();

        public static List<PropInfo> From<T>(Dictionary<string, Type> propTypes) =>
            typeof(IDictionary<string, object>).IsAssignableFrom(typeof(T))
                ? propTypes == null
                    ? throw new SqlBulkToolsException("Need property types when entity type is IDictionary<string, object>.")
                    : propTypes.ToPropInfoList()
                : typeof(T).ToPropInfoList();
    }

    /// <summary>
    /// PropInfo abstracts away the difference between a property created from an actual PropertyInfo object, 
    /// or one created from a Dictionary[string, Type] which is passed with the WithPropertyTypes method
    /// </summary>
    public class PropInfo
    {
        private readonly PropertyInfo _propertyInfo;

        internal PropInfo(PropertyInfo propertyInfo) => 
            (_propertyInfo, Name, PropertyType) = (propertyInfo, propertyInfo.Name, propertyInfo.PropertyType);

        internal PropInfo(string name, Type type) =>
            (Name, PropertyType) = (name, type);

        /// <summary>
        /// The name of the property
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The type of the property
        /// </summary>
        public Type PropertyType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePropertyName"></param>
        /// <returns></returns>
        public string GetName(string basePropertyName) => 
            string.IsNullOrWhiteSpace(basePropertyName) ? Name : $"{basePropertyName}_{Name}";

        /// <summary>
        /// Gets the value of a property on the entity provided.
        /// </summary>
        /// <param name="entity">The entity from which you want to get its property value</param>
        /// <returns></returns>
        public object GetValue(object entity) =>
            entity is null
            ? null
            : _propertyInfo != null
                ? _propertyInfo.GetValue(entity, null)
                : entity is IDictionary<string, object> dict
                    ? dict[Name]
                    : null;

        /// <summary>
        /// Sets the value of a property on the entity provided.
        /// </summary>
        /// <param name="entity">The entity on which you want to set its property value</param>
        /// <param name="value">The value to set</param>
        public void SetValue(object entity, object value)
        {
            if (_propertyInfo != null)
            {
                _propertyInfo.SetValue(entity, value, null);
            }
            else if (entity is IDictionary<string, object> dict)
            {
                dict[Name] = value;
            }
        }

        /// <summary>
        /// Is this a writable property?
        /// </summary>
        public bool CanWrite => _propertyInfo?.CanWrite ?? true;

        /// <summary>
        /// ToString is overridden for a nicer debugging experience
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Name} <{PropertyType.Name}>";
    }
}
