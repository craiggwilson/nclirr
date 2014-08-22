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
            if(diff.ExtraInfo != null)
            {
                return string.Format(diff.Kind.Message, diff.ExtraInfo);
            }

            return diff.Kind.Message;
        }
    }
}