using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using NClirr.Core.Checkers.Type;

namespace NClirr.Core.Checkers
{
    public class AssemblyTypesChecker : AbstractParentChildChecker<AssemblyDefinition, TypeDefinition>
    {
        public AssemblyTypesChecker()
            : base(new MemberReferenceFullNameComparer())
        {
        }

        protected override IEnumerable<TypeDefinition> GetChildren(AssemblyDefinition parent)
        {
            return parent.MainModule.GetTypes().Where(x => x.IsPublic);
        }

        protected override IEnumerable<ApiDifference> ChildAdded(AssemblyDefinition oldParent, TypeDefinition newChild)
        {
            yield return new ApiDifference(
                ApiDifferenceKind.TypeAdded,
                Severity.Info,
                Severity.Info,
                newChild.FullName,
                null,
                null);
        }

        protected override IEnumerable<ApiDifference> ChildRemoved(AssemblyDefinition oldParent, TypeDefinition oldChild)
        {
            yield return new ApiDifference(
                ApiDifferenceKind.TypeRemoved,
                Severity.Error,
                Severity.Error,
                oldChild.FullName,
                null,
                null);
        }

        protected override IEnumerable<ApiDifference> CompareChildren(AssemblyDefinition oldParent, TypeDefinition oldChild, TypeDefinition newChild)
        {
            return TypeCheckers.GetDefault().SelectMany(x => x.Check(oldChild, newChild));
        }
    }
}