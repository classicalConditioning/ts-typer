using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TsTyper.Errors;

namespace TsTyper
{
    public static class Parser
    {
        /// <summary>
        /// Parse an assembly at a provided path, only selecting
        /// classes that belong to the specified namespace.
        /// </summary>
        /// <param name="inputPath">The input path.</param>
        /// <param name="outputPath">The output path.</param>
        /// <param name="namespacePath">The namespace.</param>
        public static void Parse(string inputPath, string outputPath, string namespacePath, ParserOutputType outputType, string suffix)
        {
            var assembly = GetAssemblyFromPath(inputPath);
            var types = GetTypes(assembly, namespacePath).ToList();
            if (types.Count == 0)
            {
                throw new NoTypesFoundException(inputPath, namespacePath);
            }

            Console.WriteLine($"Exporting {types.Count} type(s)...");
            Builder.Build(types, outputPath, suffix);
        }

        /// <summary>
        /// Try to get an assembly from the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Assembly located at the specified path.</returns>
        public static Assembly GetAssemblyFromPath(string path)
        {
            try
            {
                var dllFile = new FileInfo(path);
                var assembly = Assembly.LoadFrom(dllFile.FullName);
                return assembly;
            }
            catch(Exception e)
            {
                throw new InvalidPathException(path);
            }
        }

        /// <summary>
        /// Get types from assembly.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="namespacePath"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypes(Assembly assembly, string namespacePath) 
        {
            try
            {
                var allTypes = assembly.GetTypes();
                if (namespacePath == "*")
                {
                    return allTypes;
                }

                return allTypes.Where(x => x.Namespace.Contains(namespacePath));
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            return new List<Type>();

        }
    }
}
