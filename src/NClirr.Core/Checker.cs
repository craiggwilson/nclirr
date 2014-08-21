using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using NClirr.Core.AssemblyCheckers;

namespace NClirr.Core
{
    public sealed class Checker
    {
        private readonly List<IAssemblyChecker> _assemblyCheckers;

        public Checker()
        {
            _assemblyCheckers = new List<IAssemblyChecker>
            {
                new TypeComparingAssemblyChecker()
            };
        }

        public IEnumerable<ApiDifference> Check(string oldAssemblyPath, string newAssemblyPath)
        {
            var oldAssembly = LoadAssembly(oldAssemblyPath);
            var newAssembly = LoadAssembly(newAssemblyPath);

            return _assemblyCheckers
                .SelectMany(x => x.Check(oldAssembly, newAssembly));
        }

        private AssemblyDefinition LoadAssembly(string path)
        {
            var rp = new ReaderParameters
            {
                AssemblyResolver = new Resolver(Path.GetDirectoryName(path))
            };
            return AssemblyDefinition.ReadAssembly(path, rp);
        }

        private class Resolver : BaseAssemblyResolver
        {
            private readonly string _basePath;

            public Resolver(string basePath)
            {
                _basePath = basePath;
            }

            public override AssemblyDefinition Resolve(AssemblyNameReference name)
            {
                try
                {
                    return base.Resolve(name);
                }
                catch
                {
                    var fullPath = Path.Combine(_basePath, name.Name + ".dll");
                    var rp = new ReaderParameters
                    {
                        AssemblyResolver = this
                    };
                    return AssemblyDefinition.ReadAssembly(fullPath, rp);
                }
            }
        }
    }
}