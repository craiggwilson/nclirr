using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using NClirr.Core;

namespace NClirr.Cli
{
    class Program
    {
        static int Main(string[] args)
        {
            if(args == null || args.Length != 2)
            {
                Console.WriteLine("NClirr requires 2 arguments. Both of them must be assembly names.");
                return 1;
            }

            var oldPath = Path.GetFullPath(args[0]);
            var newPath = Path.GetFullPath(args[1]);

            bool hasErrors = false;
            var checker = new Checker();
            var differences = checker.Check(oldPath, newPath)
                .Select(x =>
                {
                    if (x.BinarySeverity == Severity.Error || x.SourceSeverity == Severity.Error)
                    {
                        hasErrors = true;
                    }
                    return x;
                });

            var reporter = new FilteringReporter(
                new TextWriterReporter(Console.Out, new MessageProvider()),
                d => d.SourceSeverity == Severity.Error || d.BinarySeverity == Severity.Error);

            reporter.Report(differences);

            Console.ReadLine();
            return hasErrors ? 2 : 0;
        }
    }
}