using System.Collections.Generic;
using System.Reflection;

namespace TsTyper
{
    public interface IParser
    {
        int Parse(string inputPath, string outputPath = "./", string namespacePath = "*");
        Assembly GetAssemblyFromPath(string path);
        IEnumerable<System.Type> GetTypes(Assembly assembly, string namespacePath);
    }
}
