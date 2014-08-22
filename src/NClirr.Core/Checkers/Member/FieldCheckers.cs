using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace NClirr.Core.Checkers.Member
{
    public static class FieldCheckers
    {
        public static IEnumerable<IChecker<FieldDefinition>> GetDefault(TypeDefinition oldType)
        {
            yield return new FieldModifierChecker(oldType);
            yield return new FieldConstantChecker(oldType);
        }
    }
}
