using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace NClirr.Core.Checkers.Type
{
    public class TypeGenderChecker : IChecker<TypeDefinition>
    {
        public IEnumerable<ApiDifference> Check(TypeDefinition oldType, TypeDefinition newType)
        {
            if(oldType.IsInterface && !newType.IsInterface)
            {
                yield return new ApiDifference(
                    ApiDifferenceKind.ClassChangedToInterface,
                    Severity.Error,
                    Severity.Error,
                    oldType.FullName,
                    null,
                    null);
            }
            else if(!oldType.IsInterface && newType.IsInterface)
            {
                yield return new ApiDifference(
                    ApiDifferenceKind.InterfaceChangedToClass,
                    Severity.Error,
                    Severity.Error,
                    oldType.FullName,
                    null,
                    null);
            }
        }
    }
}