using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.Web.Controllers.Skills
{
    public interface ISkillsWebWindow
    {
        LightDictionary<string, string> StartUpScripts { get; }
    }

    public interface ICallbackFrameTemplate : IFrameTemplate
    {
        ICollection<ASPxCallbackPanel> GetASPxCallbackPanels();
        ASPxCallbackPanel ProcessActionCallbackPanel { get; }
    }
}
