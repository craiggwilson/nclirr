using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace NClirr.Core
{
    public interface IAssemblyChecker
    {
        IEnumerable<ApiDifference> Check(AssemblyDefinition oldAssembly, AssemblyDefinition newAssembly);
    }
}