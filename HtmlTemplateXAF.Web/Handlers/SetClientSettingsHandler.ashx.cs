using HtmlTemplateXAF.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HtmlTemplateXAF.Web.Handlers
{
    /// <summary>
    /// Summary description for SetClientSettingsHandler
    /// </summary>
    public class SetClientSettingsHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if(!string.IsNullOrEmpty(context.Request["clientOffset"]))
                SkillsGlobalSettings.Instance.ClientOffset = Convert.ToInt16(context.Request["clientOffset"].ToString());
            context.Response.ContentType = "text/plain";
            context.Response.Write("Done!");
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