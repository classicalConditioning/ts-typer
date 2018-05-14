
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using TsTyper;

namespace Tests
{
    public class Tests
    {
        public string PathToDll { get; set; }
        public const string Namespace = "DomainModelLayer";
        public const int ExpectedTypesCount = 50;

        [SetUp]
        public void Setup()
        {
            PathToDll = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\eCommerce.dll");
        }

        [Test]
        public void CanGetAssemblyFromPath()
        {
            var assembly = Parser.GetAssemblyFromPath(PathToDll);
            Assert.IsNotNull(assembly);
        }

        [Test]
        public void CanGetTypes()
        {
            var assembly = Parser.GetAssemblyFromPath(PathToDll);
            var types = Parser.GetTypes(assembly, Namespace).ToList();
            Assert.AreEqual(types.Count, ExpectedTypesCount);
        }
    }
}