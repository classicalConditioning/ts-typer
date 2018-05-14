using Microsoft.Extensions.CommandLineUtils;
using System;

namespace TsTyper
{
    public class TsTyper
    {
        public static string InputPath { get; set; }
        public static string OutputPath { get; set; }
        public static string NamespacePath { get; set; }
        public static ParserOutputType OutputType { get; set; }

        static void Main(string[] args)
        {
            var app = new CommandLineApplication();

            app.Name = "ts-typer";
            app.Description = "A .Net core app that converts C# classes to typescript classes or interfaces.";

            var inputPathOption = app.Option("-in|--inputPath", "Input path value", CommandOptionType.SingleValue);
            var outputPathOption = app.Option("-out|--outputPath", "Output path value", CommandOptionType.SingleValue);
            var namespacePathOption = app.Option("-ns|--namespace", "Namespace path value", CommandOptionType.SingleValue);
            var outputTypeOption = app.Option("-t|--type", "Output type value", CommandOptionType.SingleValue);

            app.OnExecute(() => {
                if (inputPathOption.HasValue())
                {
                    InputPath = inputPathOption.Value();
                }

                if (outputPathOption.HasValue())
                {
                    OutputPath = outputPathOption.Value();
                }

                if (namespacePathOption.HasValue())
                {
                    NamespacePath = namespacePathOption.Value();
                }
                else
                {
                    NamespacePath = "*";
                }

                if (outputPathOption.HasValue())
                {
                    var value = 0;
                    Int32.TryParse(outputPathOption.Value(), out value);
                    if (Enum.IsDefined(typeof(ParserOutputType), value)) {
                        OutputType = (ParserOutputType) value;
                    }
                }

                return 0;
            });

            app.Command("export", (command) =>
            {
                command.Description = "Begin export.";
                command.HelpOption("-?|-h|--help");

                command.OnExecute(() =>
                {
                    if (String.IsNullOrEmpty(InputPath))
                    {
                        Console.WriteLine("Please provide input path");
                        return 0;
                    }

                    if (String.IsNullOrEmpty(OutputPath))
                    {
                        Console.WriteLine("Please provide output path");
                        return 0;
                    }

                    Parser.Parse(InputPath, OutputPath, NamespacePath, OutputType);
                    return 0;
                });
            });

            app.Execute(args);
        }
    }
}
