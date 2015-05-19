using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HtmlTemplateXAF.Web.Handlers
{
    /// <summary>
    /// Summary description for GetDataHandler
    /// </summary>
    public class GetDataHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if(context.Request["Document"] == "Deliverable")
            {
                int i = 0;
                while (i < 2000000000)
                    i++;
                context.Response.Write("Muitos estragáveis");
            }
            if (context.Request["Document"] == "Project")
            {
                int i = 0;
                while (i < 1000000000)
                    i++;
                context.Response.Write("Muitos projectos");
            }
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}