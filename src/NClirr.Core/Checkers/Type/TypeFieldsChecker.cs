using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using NClirr.Core.Checkers.Member;

namespace NClirr.Core.Checkers.Type
{
    public class TypeFieldsChecker : AbstractParentChildChecker<TypeDefinition, FieldDefinition>
    {
        public TypeFieldsChecker()
            : base(new MemberReferenceFullNameComparer())
        {
        }

        protected override IEnumerable<FieldDefinition> GetChildren(TypeDefinition parent)
        {
            return parent.Fields.Where(x => !x.IsPrivate);
        }

        protected override IEnumerable<ApiDifference> ChildAdded(TypeDefinition oldParent, FieldDefinition newChild)
        {
            yield return new ApiDifference(
                ApiDifferenceKind.FieldAdded,
                Severity.Info,
                Severity.Info,
                oldParent.FullName,
                newChild.FullName,
                null);
        }

        protected override IEnumerable<ApiDifference> ChildRemoved(TypeDefinition oldParent, FieldDefinition oldChild)
        {
            if (oldChild.HasConstant)
            {
                yield return new ApiDifference(
                    ApiDifferenceKind.ConstantFieldRemoved,
                    Severity.Warning, // constants are inlined, so not technically binary compatibility
                    Severity.Error,
                    oldParent.FullName,
                    oldChild.FullName,
                    null);
            }
            else
            {
                yield return new ApiDifference(
                    ApiDifferenceKind.FieldRemoved,
                    Severity.Error,
                    Severity.Error,
                    oldParent.FullName,
                    oldChild.FullName,
                    null);
            }
        }

        protected override IEnumerable<ApiDifference> CompareChildren(TypeDefinition oldParent, FieldDefinition oldChild, FieldDefinition newChild)
        {
            return FieldCheckers.GetDefault(oldParent).SelectMany(x => x.Check(oldChild, newChild));
        }
    }
}