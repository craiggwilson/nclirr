using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace NClirr.Core.AssemblyCheckers
{
    public interface ITypeChecker
    {
        IEnumerable<ApiDifference> Check(TypeDefinition oldType, TypeDefinition newType);
    }
}
