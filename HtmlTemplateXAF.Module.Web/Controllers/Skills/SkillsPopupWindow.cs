using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.Web.Controllers.Skills
{
    public class SkillsPopupWindow : PopupWindow, ISkillsWebWindow
    {
        public SkillsPopupWindow(XafApplication application, TemplateContext context, ICollection<Controller> controllers)
			: base(application, context, controllers)
		{
		}

        protected override bool NeedToRedirect()
        {
            return false;
        }

        #region By reflection

        private LightDictionary<string, string> startUpScripts
        {
            get
            {
                return (LightDictionary<string, string>)typeof(WebWindow).GetField("startUpScripts", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(this);
            }
        }
        #endregion

        #region ISkillsWebWindow Members

        LightDictionary<string, string> ISkillsWebWindow.StartUpScripts
        {
            get { return startUpScripts; }
        }

        #endregion
    }
}
