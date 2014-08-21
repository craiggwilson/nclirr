using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using NClirr.Core.AssemblyCheckers.TypeCheckers;

namespace NClirr.Core.AssemblyCheckers
{
    public class TypeComparingAssemblyChecker : IAssemblyChecker
    {
        private readonly List<ITypeChecker> _typeCheckers;

        public TypeComparingAssemblyChecker()
        {
            _typeCheckers = new List<ITypeChecker>
            {
                new ScopeTypeChecker(),
                new HierarchyTypeChecker(),
                new ModifierTypeChecker()
            };
        }

        public IEnumerable<ApiDifference> Check(AssemblyDefinition oldAssembly, AssemblyDefinition newAssembly)
        {
            var oldTypes = oldAssembly.MainModule.GetTypes();
            var newTypes = newAssembly.MainModule.GetTypes();

            return CompareTypes(oldTypes, newTypes);
        }

        public IEnumerable<ApiDifference> CompareTypes(IEnumerable<TypeDefinition> oldTypes, IEnumerable<TypeDefinition> newTypes)
        {
            var enumerator = new CoEnumerator<TypeDefinition>(oldTypes, newTypes, new TypeFullNameComparer()); 

            try
            {
                while(enumerator.MoveNext())
                {
                    if(enumerator.CurrentRight == null && enumerator.CurrentLeft.IsPublic)
                    {
                        yield return new ApiDifference(
                            ApiDifferenceKind.TypeRemoved,
                            Severity.Error,
                            Severity.Error,
                            enumerator.CurrentLeft.FullName,
                            null,
                            null);
                        continue;
                    }
                    else if(enumerator.CurrentLeft == null && enumerator.CurrentRight.IsPublic)
                    {
                        yield return new ApiDifference(
                            ApiDifferenceKind.TypeAdded,
                            Severity.Info,
                            Severity.Info,
                            enumerator.CurrentRight.FullName,
                            null,
                            null);
                        continue;
                    }
                    else if (enumerator.CurrentLeft != null && enumerator.CurrentRight != null)
                    {
                        foreach (var violation in CompareTypes(enumerator.CurrentLeft, enumerator.CurrentRight))
                        {
                            yield return violation;
                        }
                    }
                }
            }
            finally
            {
                enumerator.Dispose();
            }
        }

        public IEnumerable<ApiDifference> CompareTypes(TypeDefinition a, TypeDefinition b)
        {
            return _typeCheckers.SelectMany(x => x.Check(a, b));
        }
    }
}