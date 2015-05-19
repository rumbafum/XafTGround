using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.Web.Controllers.Skills
{
    public class SkillsShowViewStrategy : ShowViewStrategy
    {
        public SkillsShowViewStrategy(XafApplication application)
            : base(application)
		{
		}

        protected override Window ShowDialog(ShowViewParameters parameters, ShowViewSource showViewSource)
        {
            var w = base.ShowDialog(parameters, showViewSource);

            var currentWindow = this.GetCurrentWindow(showViewSource);
            if (currentWindow != null)
            {
                this.RegisterStartupWindowOpeningScript(parameters, w, currentWindow);
            }
            return w;
        }

        protected override void RegisterStartupWindowOpeningScript(ShowViewParameters parameters, Window window, WebWindow currentWindow)
        {
            var skillsPWM = this.Application.PopupWindowManager as SkillsPopupWindowManager;
            if (skillsPWM == null)
            {
                base.RegisterStartupWindowOpeningScript(parameters, window, currentWindow);
                return;
            }
            skillsPWM.RegisterStartupWindowOpeningScript(currentWindow, window as WebWindow, parameters.TargetWindow == TargetWindow.NewModalWindow);
        }

        //protected virtual void RegisterStartupWindowRefreshScript(ShowViewParameters parameters, WebWindow currentWindow)
        //{
        //    var controller = currentWindow.GetController<CallbackController>();
        //    if (controller != null)
        //        currentWindow.RegisterStartupScript("CallbackRefresh", controller.GetCallbackPanelRefreshCode(true));
        //}

		protected override void ShowViewFromNestedView(ShowViewParameters parameters, ShowViewSource showViewSource)
		{
			base.ShowViewFromNestedView(parameters, showViewSource);
            //var currentWindow = this.GetCurrentWindow(showViewSource);
            //if (currentWindow != null)
            //{
            //    this.RegisterStartupWindowRefreshScript(parameters, currentWindow);
            //}
            //else
            //{
            //    Tracing.Tracer.LogWarning("DevExpress.ExpressApp.Web.ShowViewStrategy.ShowViewFromNestedView: Cannot register client scripts", new object[0]);
            //}
		}
    }
}
