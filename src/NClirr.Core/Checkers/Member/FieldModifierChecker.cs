using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace NClirr.Core.Checkers.Member
{
    public class FieldModifierChecker : IChecker<FieldDefinition>
    {
        private readonly TypeDefinition _oldType;

        public FieldModifierChecker(TypeDefinition oldType)
        {
            _oldType = oldType;
        }

        public IEnumerable<ApiDifference> Check(FieldDefinition oldField, FieldDefinition newField)
        {
            if (oldField.IsInitOnly && !newField.IsInitOnly)
            {
                yield return new ApiDifference(
                    ApiDifferenceKind.FieldModifierReadonlyAdded,
                    Severity.Error,
                    Severity.Error,
                    _oldType.FullName,
                    oldField.FullName,
                    null);
            }
            else if (!oldField.IsInitOnly && newField.IsInitOnly)
            {
                yield return new ApiDifference(
                    ApiDifferenceKind.FieldModifierReadonlyRemoved,
                    Severity.Info,
                    Severity.Info,
                    _oldType.FullName,
                    oldField.FullName,
                    null);
            }

            if (!oldField.IsStatic && newField.IsStatic)
            {
                yield return new ApiDifference(
                    ApiDifferenceKind.FieldModifierStaticAdded,
                    Severity.Error,
                    Severity.Error,
                    _oldType.FullName,
                    oldField.FullName,
                    null);
            }
            else if(oldField.IsStatic && !newField.IsStatic)
            {
                yield return new ApiDifference(
                    ApiDifferenceKind.FieldModifierStaticRemoved,
                    Severity.Error,
                    Severity.Error,
                    _oldType.FullName,
                    oldField.FullName,
                    null);
            }

            // TODO: handle volatile?
        }
    }
}
