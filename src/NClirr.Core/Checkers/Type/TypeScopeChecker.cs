using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace NClirr.Core.Checkers.Type
{
    public class TypeScopeChecker : IChecker<TypeDefinition>
    {
        public IEnumerable<ApiDifference> Check(TypeDefinition oldType, TypeDefinition newType)
        {
            if(!oldType.IsPublic && newType.IsPublic)
            {
                yield return new ApiDifference(
                    ApiDifferenceKind.TypeVisibilityIncreased,
                    Severity.Error,
                    Severity.Error,
                    oldType.FullName,
                    null,
                    null);
            }

            if(oldType.IsPublic && !newType.IsPublic)
            {
                yield return new ApiDifference(
                    ApiDifferenceKind.TypeVisibilityDecreased,
                    Severity.Error,
                    Severity.Error,
                    oldType.FullName,
                    null,
                    null);
            }
        }
    }
}
