using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace NClirr.Core.AssemblyCheckers.TypeCheckers
{
    public class HierarchyTypeChecker : ITypeChecker
    {
        public IEnumerable<ApiDifference> Check(TypeDefinition oldType, TypeDefinition newType)
        {
            if(!oldType.IsPublic)
            {
                yield break;
            }

            var enumerator = new CoEnumerator<TypeDefinition>(
                GetHierarchy(oldType),
                GetHierarchy(newType),
                new TypeFullNameComparer());

            try
            {
                while(enumerator.MoveNext())
                {
                    if(enumerator.CurrentLeft == null)
                    {
                        yield return new ApiDifference(
                            ApiDifferenceKind.TypeAddedToHierarchy,
                            Severity.Warning, // TODO: may not be accurate
                            Severity.Warning,
                            oldType.FullName,
                            null,
                            new[] { enumerator.CurrentRight.FullName });
                    }
                    else if(enumerator.CurrentRight == null)
                    {
                        yield return new ApiDifference(
                            ApiDifferenceKind.TypeRemovedFromHierarchy,
                            Severity.Error,
                            Severity.Error,
                            oldType.FullName,
                            null,
                            new[] { enumerator.CurrentLeft.FullName });
                    }
                }
            }
            finally
            {
                enumerator.Dispose();
            }
        }

        private IEnumerable<TypeDefinition> GetHierarchy(TypeDefinition type)
        {
            var baseType = type;
            do
            {
                if (baseType.BaseType != null)
                {
                    baseType = baseType.BaseType.Resolve();
                    yield return baseType;
                }
                else
                {
                    baseType = null;
                }
            }
            while (baseType != null);
        }
    }
}
