using System;
using System.Collections.Generic;

namespace TsTyper
{
    public static class Mapper
    {
        private static Dictionary<Type, string> TypeMap = new Dictionary<Type, string>
        {
            { typeof(Int16), "number" },
            { typeof(Int32), "number" },
            { typeof(Int64), "number" },
            { typeof(UInt16), "number" },
            { typeof(UInt32), "number" },
            { typeof(UInt64), "number" },
            { typeof(Double), "decimal" },
            { typeof(Single), "decimal" },
            { typeof(Decimal), "decimal" },
            { typeof(String), "string" },
            { typeof(Boolean), "boolean" },
            { typeof(Byte), "string" },
            { typeof(SByte), "string" },
            { typeof(Char), "string" },
            { typeof(DateTime), "string"},
            { typeof(Guid), "string"}
        };

        public static bool IsCollection(Type type)
        {
            return type == typeof(List<>)
                || type == typeof(IEnumerable<>)
                || type == typeof(IObservable<>)
                || type == typeof(IReadOnlyCollection<>)
                || type == typeof(IReadOnlyList<>);
        }

        public static string GetType(Type type)
        {
            if (TypeMap.ContainsKey(type))
            {
                var typeName = String.Empty;
                TypeMap.TryGetValue(type, out typeName);
                return typeName;
            }

            if (IsCollection(type) && type.IsGenericType)
            {
                return $"Array<${GetType(type.GetGenericArguments()[0])}>";
            }

            return type.Name;
        }
    }
}
