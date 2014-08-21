using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NClirr.Core
{
    public sealed class ApiDifferenceKind
    {
        // Assembly Violations

        // Type Violations
        public static readonly ApiDifferenceKind TypeAdded = new ApiDifferenceKind(2000);
        public static readonly ApiDifferenceKind TypeRemoved = new ApiDifferenceKind(2001);
        public static readonly ApiDifferenceKind TypeVisibilityIncreased = new ApiDifferenceKind(2001);
        public static readonly ApiDifferenceKind TypeVisibilityDecreased = new ApiDifferenceKind(2002);
        public static readonly ApiDifferenceKind TypeAddedToHierarchy = new ApiDifferenceKind(2003);
        public static readonly ApiDifferenceKind TypeRemovedFromHierarchy = new ApiDifferenceKind(2004);
        public static readonly ApiDifferenceKind TypeModifierSealedAddedToEffectivelySealedClass = new ApiDifferenceKind(2005);
        public static readonly ApiDifferenceKind TypeModifierSealedAdded = new ApiDifferenceKind(2006);
        public static readonly ApiDifferenceKind TypeModifierSealedRemoved = new ApiDifferenceKind(2007);
        public static readonly ApiDifferenceKind TypeModifierAbstractAdded = new ApiDifferenceKind(2008);
        public static readonly ApiDifferenceKind TypeModifierAbstractRemoved = new ApiDifferenceKind(2009);

        public ApiDifferenceKind(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}