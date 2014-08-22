using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace NClirr.Core.Checkers.Type
{
    public static class TypeCheckers
    {
        public static IEnumerable<IChecker<TypeDefinition>> GetDefault()
        {
            yield return new TypeScopeChecker();
            yield return new TypeHierarchyChecker();
            yield return new TypeModifierChecker();
            yield return new TypeGenderChecker();
            yield return new TypeInterfacesChecker();
            yield return new TypeFieldsChecker();
        }
    }
}
