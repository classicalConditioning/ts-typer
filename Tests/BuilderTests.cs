using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using TsTyper;

namespace Tests
{
    public class BuilderTests
    {
        public string PathToDll { get; set; }
        public const string Namespace = "DomainModelLayer";
        public IEnumerable<Type> Types { get; set; }
        public string OutputString { get; set; }
        public const ParserOutputType OutputType = ParserOutputType.Interface;
        public const string Suffix = "DO";


        [SetUp]
        public void Setup()
        {
            PathToDll = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\eCommerce.dll");
            OutputString = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\output\");
            var assembly = Parser.GetAssemblyFromPath(PathToDll);
            Types = Parser.GetTypes(assembly, Namespace);
        }

        [Test]
        public void CanBuildWhenAnInterfaceOutputTypeSelected()
        {
            Builder.Build(Types, OutputString, Suffix, OutputType);
        }
    }
}
