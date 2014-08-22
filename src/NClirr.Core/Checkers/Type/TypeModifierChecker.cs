using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace NClirr.Core.Checkers.Type
{
    public class TypeModifierChecker : IChecker<TypeDefinition>
    {
        public IEnumerable<ApiDifference> Check(TypeDefinition oldType, TypeDefinition newType)
        {
            if(oldType.IsSealed && !newType.IsSealed)
            {
                yield return new ApiDifference(
                    ApiDifferenceKind.TypeModifierSealedRemoved,
                    Severity.Info,
                    Severity.Info,
                    oldType.FullName,
                    null,
                    null);
            }
            else if(!oldType.IsSealed && newType.IsSealed)
            {
                if(IsEffectivelyFinal(oldType))
                {
                    // sealing a class with only private constructors
                    // isn't really a change at all
                    yield return new ApiDifference(
                        ApiDifferenceKind.TypeModifierSealedAddedToEffectivelySealedClass,
                        Severity.Info,
                        Severity.Info,
                        oldType.FullName,
                        null,
                        null);
                }
                else
                {
                    yield return new ApiDifference(
                        ApiDifferenceKind.TypeModifierSealedAdded,
                        Severity.Error,
                        Severity.Error,
                        oldType.FullName,
                        null,
                        null);
                }
            }

            if(oldType.IsAbstract && !newType.IsAbstract)
            {
                yield return new ApiDifference(
                    ApiDifferenceKind.TypeModifierAbstractRemoved,
                    Severity.Info,
                    Severity.Info,
                    oldType.FullName,
                    null,
                    null);
            }
            else if (!oldType.IsAbstract && newType.IsAbstract)
            {
                yield return new ApiDifference(
                    ApiDifferenceKind.TypeModifierAbstractAdded,
                    Severity.Error,
                    Severity.Error,
                    oldType.FullName,
                    null,
                    null);
            }
        }

        private bool IsEffectivelyFinal(TypeDefinition type)
        {
            return type.Methods
                .Where(x => x.IsConstructor && !x.IsStatic)
                .All(x => !x.IsPublic);
        }
    }
}
