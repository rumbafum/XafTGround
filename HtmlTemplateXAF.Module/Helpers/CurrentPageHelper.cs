using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HtmlTemplateXAF.Module.Helpers
{
    public static class CurrentPageHelper
    {
        public static string CurrentPageUrl
        {
            get;
            set;
        }

        public static string GridLayout
        {
            get;
            set;
        }

        public static HttpRequest Request
        {
            get;
            set;
        }
    }
}
