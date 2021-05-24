using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace SqlBulkTools.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class BulkOperationsUtility
    {
        private static readonly Dictionary<Type, DbType> TypeMappings = new Dictionary<Type, DbType>()
        {
            { typeof(byte), DbType.Byte},
            { typeof(sbyte), DbType.Int16},
            { typeof(ushort), DbType.UInt16},
            { typeof(int), DbType.Int32},
            { typeof(uint), DbType.UInt32},
            { typeof(long), DbType.Int64},
            { typeof(ulong), DbType.UInt64 },
            { typeof(float), DbType.Single },
            { typeof(double), DbType.Double},
            { typeof(decimal), DbType.Decimal},
            { typeof(bool), DbType.Boolean},
            { typeof(string), DbType.String },
            { typeof(char), DbType.StringFixedLength},
            { typeof(char[]), DbType.String},
            { typeof(Guid), DbType.Guid},
            { typeof(DateTime), DbType.DateTime},
            { typeof(DateTimeOffset), DbType.DateTimeOffset },
            { typeof(byte[]), DbType.Binary},
            { typeof(byte?), DbType.Byte},
            { typeof(sbyte?), DbType.SByte },
            { typeof(short), DbType.Int16},
            { typeof(short?), DbType.Int16},
            { typeof(ushort?), DbType.UInt16},
            { typeof(int?), DbType.Int32},
            { typeof(uint?), DbType.UInt32},
            { typeof(long?), DbType.Int64},
            { typeof(ulong?), DbType.UInt64},
            { typeof(float?), DbType.Single},
            { typeof(double?), DbType.Double},
            { typeof(decimal?), DbType.Decimal},
            { typeof(bool?), DbType.Boolean},
            { typeof(char?), DbType.StringFixedLength},
            { typeof(Guid?), DbType.Guid},
            { typeof(DateTime?), DbType.DateTime },
            { typeof(DateTimeOffset?), DbType.DateTimeOffset},
            { typeof(TimeSpan), DbType.Time },
            { typeof(TimeSpan?), DbType.Time },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public static DbType GetSqlTypeFromDotNetType(Type type)
        {
            DbType dbType;

            if (TypeMappings.TryGetValue(type, out dbType))
            {
                return dbType;
            }

            throw new KeyNotFoundException($"The type {type} could not be found.");
        }

        private static readonly ConcurrentDictionary<Type, bool> _isComplexCache = new ConcurrentDictionary<Type, bool>();

        internal static bool IsComplexType(this Type type) =>
            _isComplexCache.GetOrAdd(type, t => t.GetCustomAttribute(typeof(ComplexTypeAttribute)) is object);

        /// <summary>
        /// Register a Type as a Complex Type for situations where you can't add a ComplexTypeAttribute to an existing type
        /// </summary>
        /// <typeparam name="T">The type to register as a Complex type</typeparam>
        public static void RegisterComplexType<T>() =>
            RegisterComplexType(typeof(T));

        /// <summary>
        /// Register a Type as a Complex Type for situations where you can't add a ComplexTypeAttribute to an existing type
        /// </summary>
        /// <param name="type">The type to register as a Complex type</param>
        public static void RegisterComplexType(Type type) =>
            _isComplexCache.AddOrUpdate(type, true, (t, c) => true);
    }
}
