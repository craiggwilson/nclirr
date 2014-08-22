using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace NClirr.Core.Checkers.Member
{
    public class FieldConstantChecker : IChecker<FieldDefinition>
    {
        private readonly TypeDefinition _oldType;

        public FieldConstantChecker(TypeDefinition oldType)
        {
            _oldType = oldType;
        }

        public IEnumerable<ApiDifference> Check(FieldDefinition oldField, FieldDefinition newField)
        {
            if(oldField.IsLiteral && !newField.IsLiteral)
            {
                yield return new ApiDifference(
                    ApiDifferenceKind.FieldNowConstant,
                    Severity.Error,
                    Severity.Error,
                    _oldType.FullName,
                    oldField.FullName,
                    null);
            }
            else if(!oldField.IsLiteral && newField.IsLiteral)
            {
                yield return new ApiDifference(
                    ApiDifferenceKind.FieldNoLongerConstant,
                    Severity.Warning,
                    Severity.Error,
                    _oldType.FullName,
                    oldField.FullName,
                    null);
            }

        }
    }
}
