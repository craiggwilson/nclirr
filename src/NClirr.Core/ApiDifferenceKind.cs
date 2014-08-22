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
        public static readonly ApiDifferenceKind TypeAdded = new ApiDifferenceKind(2000, "Type has been added.");
        public static readonly ApiDifferenceKind TypeRemoved = new ApiDifferenceKind(2001, "Type has been removed.");
        public static readonly ApiDifferenceKind TypeVisibilityIncreased = new ApiDifferenceKind(2002, "Type's visibility has been increased.");
        public static readonly ApiDifferenceKind TypeVisibilityDecreased = new ApiDifferenceKind(2003, "Type's visibility has been decreased.");
        public static readonly ApiDifferenceKind TypeAddedToHierarchy = new ApiDifferenceKind(2004, "{0} has been added to the type hierarchy.");
        public static readonly ApiDifferenceKind TypeRemovedFromHierarchy = new ApiDifferenceKind(2005, "{0} has been removed from the type hierarchy");
        public static readonly ApiDifferenceKind TypeModifierSealedAddedToEffectivelySealedClass = new ApiDifferenceKind(2006, "Effecively sealed type has been sealed.");
        public static readonly ApiDifferenceKind TypeModifierSealedAdded = new ApiDifferenceKind(2007, "Type has been sealed.");
        public static readonly ApiDifferenceKind TypeModifierSealedRemoved = new ApiDifferenceKind(2008, "Type has been unsealed.");
        public static readonly ApiDifferenceKind TypeModifierAbstractAdded = new ApiDifferenceKind(2009, "Type has been made abstract.");
        public static readonly ApiDifferenceKind TypeModifierAbstractRemoved = new ApiDifferenceKind(2010, "Type is no longer abstract.");
        public static readonly ApiDifferenceKind ClassChangedToInterface = new ApiDifferenceKind(2011, "Class has been changed to an interface.");
        public static readonly ApiDifferenceKind InterfaceChangedToClass = new ApiDifferenceKind(2012, "Interface has been changed to a class.");
        public static readonly ApiDifferenceKind TypeImplementsNewInterface = new ApiDifferenceKind(2013, "Type now implements interface {0}.");
        public static readonly ApiDifferenceKind TypeNoLongerImplementsInterface = new ApiDifferenceKind(2014, "Type no longer implements interface {0}.");

        // Member Violations
        public static readonly ApiDifferenceKind FieldAdded = new ApiDifferenceKind(2015, "Field has been added.");
        public static readonly ApiDifferenceKind FieldRemoved = new ApiDifferenceKind(2017, "Field has been removed.");
        public static readonly ApiDifferenceKind ConstantFieldRemoved = new ApiDifferenceKind(2018, "Constant field has been removed.");
        public static readonly ApiDifferenceKind FieldModifierReadonlyAdded = new ApiDifferenceKind(2019, "Field is now readonly.");
        public static readonly ApiDifferenceKind FieldModifierReadonlyRemoved = new ApiDifferenceKind(2020, "Field is no longer readonly.");
        public static readonly ApiDifferenceKind FieldModifierStaticAdded = new ApiDifferenceKind(2021, "Field is now static.");
        public static readonly ApiDifferenceKind FieldModifierStaticRemoved = new ApiDifferenceKind(2022, "Field is no longer static.");
        public static readonly ApiDifferenceKind FieldNowConstant = new ApiDifferenceKind(2021, "Field is now constant.");
        public static readonly ApiDifferenceKind FieldNoLongerConstant = new ApiDifferenceKind(2022, "Field is no longer constant.");


        public ApiDifferenceKind(int id, string message)
        {
            Id = id;
            Message = message;
        }

        public int Id { get; private set; }

        public string Message { get; private set; }
    }
}