using System;

namespace TsTyper.Errors
{
    public class InvalidPathException : Exception
    {
        public InvalidPathException(string path) : base($"Path [{path}] does not point a to a dll")
        {
        }
    }
}
