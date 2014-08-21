using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NClirr.Core
{
    public sealed class ApiDifference
    {
        public ApiDifference(ApiDifferenceKind kind, Severity binarySeverity, Severity sourceSeverity, string affectedType, string affectedMember, string[] extraInfo)
        {
            Kind = kind;
            BinarySeverity = binarySeverity;
            SourceSeverity = sourceSeverity;
            AffectedType = affectedType;
            AffectedMember = affectedMember;
            ExtraInfo = extraInfo;
        }

        public ApiDifferenceKind Kind { get; private set; }

        public Severity BinarySeverity { get; private set; }

        public Severity SourceSeverity { get; private set; }

        public string AffectedType { get; private set; }

        public string AffectedMember { get; private set; }
         
        public string[] ExtraInfo { get; private set; }
    }
}
