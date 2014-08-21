using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NClirr.Core
{
    public class MessageProvider
    {
        public string GetMessage(ApiDifference diff)
        {
            switch(diff.Kind.Id)
            {
                case 2000:
                    return "Has been added.";
                case 2001:
                    return "Has been removed.";
                case 2002:
                    return "Visibility has been increased.";
                case 2003:
                    return "Visibility has been decreased.";
                case 2004:
                    return string.Format("{0} has been added to the hierarchy.", diff.ExtraInfo);
                case 2005:
                    return string.Format("{0} has been removed from the hierarchy.", diff.ExtraInfo);
                case 2006:
                    return "Is now sealed. Previously, it was effectively sealed.";
                case 2007:
                    return "Is now sealed.";
                case 2008:
                    return "Is no longer sealed.";
                case 2009:
                    return "Is now abstract.";
                case 2010:
                    return "Is no longer abstract.";
                default:
                    return GetFallbackMessage(diff);
            }
        }

        protected virtual string GetFallbackMessage(ApiDifference diff)
        {
            var result = diff.Kind.GetType().Name;
            if(diff.ExtraInfo != null && diff.ExtraInfo.Length > 0)
            {
                result += ": " + string.Join(",", diff.ExtraInfo);
            }

            return result;
        }
    }
}