using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.Web.Controllers.Skills
{
    public class SkillsWebApplication : WebApplication
    {
        protected override DevExpress.ExpressApp.Window CreateWindowCore(DevExpress.ExpressApp.TemplateContext context, 
            ICollection<DevExpress.ExpressApp.Controller> controllers, bool isMain, bool activateControllersImmediatelly)
        {
            return base.CreateWindowCore(context, controllers, isMain, activateControllersImmediatelly);
        }

        protected override DevExpress.ExpressApp.Window CreatePopupWindowCore(DevExpress.ExpressApp.TemplateContext context, 
            ICollection<DevExpress.ExpressApp.Controller> controllers)
        {
            return new SkillsPopupWindow(this, context, controllers);
        }

        protected override ShowViewStrategyBase CreateShowViewStrategy()
        {
            return new SkillsShowViewStrategy(this);
        }

        protected override PopupWindowManager CreateWebPopupWindowManager()
        {
            return new SkillsPopupWindowManager(this);
        }
    }
}
