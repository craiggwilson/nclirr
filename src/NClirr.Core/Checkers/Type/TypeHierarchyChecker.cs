using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace NClirr.Core.Checkers.Type
{
    public class TypeHierarchyChecker : AbstractParentChildChecker<TypeDefinition, TypeDefinition>
    {
        public TypeHierarchyChecker()
            : base(new MemberReferenceFullNameComparer())
        {
        }

        protected override IEnumerable<TypeDefinition> GetChildren(TypeDefinition parent)
        {
            do
            {
                if (parent.BaseType != null)
                {
                    parent = parent.BaseType.Resolve();
                    yield return parent;
                }
                else
                {
                    parent = null;
                }
            }
            while (parent != null);
        }

        protected override IEnumerable<ApiDifference> ChildAdded(TypeDefinition oldParent, TypeDefinition newChild)
        {
            yield return new ApiDifference(
                ApiDifferenceKind.TypeAddedToHierarchy,
                Severity.Warning, // TODO: may not be accurate
                Severity.Warning,
                oldParent.FullName,
                null,
                new[] { newChild.FullName });
        }

        protected override IEnumerable<ApiDifference> ChildRemoved(TypeDefinition oldParent, TypeDefinition oldChild)
        {
            yield return new ApiDifference(
                ApiDifferenceKind.TypeRemovedFromHierarchy,
                Severity.Error,
                Severity.Error,
                oldParent.FullName,
                null,
                new[] { oldChild.FullName });
        }

    }
}
