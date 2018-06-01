using System;
using System.Collections.Generic;
using System.IO;

namespace TsTyper
{
    public static class Builder
    {
        public static void Build(IEnumerable<Type> types, string outputString, string suffix, ParserOutputType outputType)
        {
            foreach (var type in types)
            {
                if (outputType == ParserOutputType.Interface && !type.IsInterface)
                {
                    Export(type, outputString, suffix);
                }
            }
        }

        private static string GetBaseType(Type type, string suffix)
        {
            if (type.BaseType == typeof(Object))
            {
                return string.Empty;
            }

            return " extends " + type.BaseType.Name + suffix;
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
                    sw.Write(GetBaseType(type, suffix));
                }
                sw.Write(" {");
                sw.WriteLine();

                foreach (var property in type.GetProperties())
                {
                    sw.Write("  " + Char.ToLowerInvariant(property.Name[0]) + property.Name.Substring(1));
                    var propertyType = property.PropertyType;
                    sw.Write($": {Mapper.GetType(propertyType)};");
                    sw.WriteLine();
                }
                sw.WriteLine("}");
            }
            Console.WriteLine($"Exported: {className}");
        }

    }
}
