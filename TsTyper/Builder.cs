using System;
using System.Collections.Generic;
using System.IO;

namespace TsTyper
{
    public static class Builder
    {
        public static void Build(IEnumerable<Type> types, string outputString, string suffix)
        {
            foreach (var type in types)
            {
                Export(type, outputString, suffix);
            }
        }

        public static void Export(Type type, string outputPath, string suffix)
        {
            var className = type.Name + suffix;
            var file = $"{outputPath}{className}.ts";
            using (StreamWriter sw = File.CreateText(file))
            {
                sw.Write($"export interface {className}");
                if (!String.IsNullOrEmpty(type.BaseType.Name))
                {
                    sw.Write($" extends interface {type.BaseType.Name + suffix}");
                }
                sw.Write(" {");
                sw.WriteLine();

                foreach (var property in type.GetProperties())
                {
                    sw.Write(property.Name);
                    var propertyType = property.GetType();
                    if (propertyType.IsPrimitive || propertyType.IsValueType || propertyType == typeof(string))
                    {
                        sw.Write($":{propertyType.Name}");
                    }
                    else
                    {
                        sw.Write($":{propertyType.Name + suffix}");
                    }
                    sw.Write(";");
                    sw.WriteLine();
                }
                sw.WriteLine("}");
            }
            Console.WriteLine($"Exported: {className}");
        }

    }
}
