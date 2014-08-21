using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NClirr.Core
{
    public class TextWriterReporter : IReporter
    {
        private readonly MessageProvider _messageProvider;
        private readonly TextWriter _writer;
        private int _indentLevel;

        public TextWriterReporter(TextWriter writer, MessageProvider messageProvider)
        {
            _writer = writer;
            _messageProvider = messageProvider;
        }

        public void Report(IEnumerable<ApiDifference> differences)
        {
            var groups = differences
                .GroupBy(x => x.AffectedType)
                .OrderBy(x => x.Key);

            foreach(var group in groups)
            {
                WriteLine(group.Key);
                Indent();
                ReportType(group);
                Unindent();
            }
        }

        private void ReportType(IEnumerable<ApiDifference> differences)
        {
            var groups = differences
                .GroupBy(x => x.AffectedMember)
                .OrderBy(x => x.Key);

            foreach (var group in groups)
            {
                if (group.Key != null)
                {
                    WriteLine(group.Key);
                    Indent();
                }
                ReportMembers(group);
                if(group.Key != null)
                {
                    Unindent();
                }
            }
        }

        private void ReportMembers(IEnumerable<ApiDifference> differences)
        {
            foreach(var diff in differences)
            {
                WriteDiff(diff);
            }
        }

        private void Indent()
        {
            _indentLevel++;
        }

        private void Unindent()
        {
            _indentLevel--;
        }

        private void WriteLine(string line)
        {
            _writer.WriteLine("".PadRight(_indentLevel * 2) + line);
        }

        private void WriteDiff(ApiDifference diff)
        {
            var message = _messageProvider.GetMessage(diff);

            var line = string.Format("[{0}] - {1} - {2}", diff.SourceSeverity.ToString(), diff.Kind.Id.ToString(), message);
            WriteLine(line);
        }
    }
}
