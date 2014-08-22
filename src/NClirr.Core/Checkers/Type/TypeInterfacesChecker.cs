using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace NClirr.Core.Checkers.Type
{
    public class TypeInterfacesChecker : AbstractParentChildChecker<TypeDefinition, string>
    {
        public TypeInterfacesChecker()
            : base(StringComparer.Ordinal)
        {
        }

        protected override IEnumerable<string> GetChildren(TypeDefinition parent)
        {
            return parent.Interfaces.Select(x => x.FullName);
        }

        protected override IEnumerable<ApiDifference> ChildAdded(TypeDefinition oldParent, string newChild)
        {
            yield return new ApiDifference(
                ApiDifferenceKind.TypeImplementsNewInterface,
                Severity.Warning,
                Severity.Warning,
                oldParent.FullName,
                null,
                new[] { newChild });
        }

        protected override IEnumerable<ApiDifference> ChildRemoved(TypeDefinition oldParent, string oldChild)
        {
            yield return new ApiDifference(
                ApiDifferenceKind.TypeNoLongerImplementsInterface,
                Severity.Error,
                Severity.Error,
                oldParent.FullName,
                null,
                new[] { oldChild });
        }
    }
}
