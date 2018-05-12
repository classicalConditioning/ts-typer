using System;

namespace TsTyper.Errors
{
    public class NoTypesFoundException : Exception 
    {
        public NoTypesFoundException(string path, string namespacePath) : base($"Error: can not find any types that belong to namespace: [{namespacePath}] in dll imported from path: [{path}]")
        {
        }
    }
}
