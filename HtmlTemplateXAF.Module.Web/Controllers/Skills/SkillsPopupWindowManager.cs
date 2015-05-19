using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HtmlTemplateXAF.Module.Web.Controllers.Skills
{
    public class SkillsPopupWindowManager : PopupWindowManager
    {
        public SkillsPopupWindowManager(WebApplication webManager)
			: base(webManager)
		{
		}

        #region By reflection
        public string GenerateOpeningScript(Control control, int windowWidth, int windowHeight, string callBackFuncName, bool modal, bool forcePostBack, string url)
        {
            var methodInfo =
                typeof(PopupWindowManager).GetMethod("GenerateOpeningScript", BindingFlags.Instance | BindingFlags.NonPublic, null,
                                                     new[]
				                                     	{
				                                     		typeof (Control),
				                                     		typeof(int),
				                                     		typeof(int),
				                                     		typeof(string),
				                                     		typeof(bool),
				                                     		typeof(bool),
				                                     		typeof(string),
				                                     	},
                                                     null);
            return (string)methodInfo.Invoke(this, new object[] { control, windowWidth, windowHeight, callBackFuncName, modal, forcePostBack, url });
        }

        public string GetWindowUrl(WebWindow window)
        {
            return (string)typeof(PopupWindowManager).GetMethod("GetWindowUrl", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(this, new[] { window });
        }

        public string GenerateOpeningScript(WebControl control, PopupWindowShowAction action, int windowWidth, int windowHeight, string callBackFuncName, bool forcePostBack, params string[] requestParams)
        {
            var methodInfo =
                typeof(PopupWindowManager).GetMethod("GenerateOpeningScript", BindingFlags.Instance | BindingFlags.NonPublic, null,
                                                     new[]
				                                     	{
				                                     		typeof (WebControl),
				                                     		typeof(PopupWindowShowAction),
				                                     		typeof(int),
				                                     		typeof(int),
				                                     		typeof(string),
				                                     		typeof(bool),
				                                     		typeof(string[]),
				                                     	},
                                                     null);
            return (string)methodInfo.Invoke(this, new object[] { control, action, windowWidth, windowHeight, callBackFuncName, forcePostBack, requestParams });
        }
        #endregion

        new public void RegisterStartupWindowOpeningScript(WebWindow currentWindow, WebWindow targetPopupWindow, bool isModal)
        {
            var url = this.GetWindowUrl(targetPopupWindow);
            var page = ((Control)currentWindow.Template).Page;
            var callbackFunc = string.Empty;
            var template = currentWindow.Template as ICallbackFrameTemplate;
            if (template != null)
            {
                var controller = currentWindow.GetController<CallbackController>();
                if (controller != null)
                    callbackFunc = controller.GetCallbackPanelRefreshCode(true);
            }
            var script = this.GenerateOpeningScript(page, PopupWindow.WindowWidth, PopupWindow.WindowHeight, callbackFunc, isModal, false, url);
            currentWindow.RegisterStartupScript("ShowViewInNewWindow", script);
        }

        new public void RegisterStartupPopupWindowShowActionScript(WebControl control, PopupWindowShowAction action)
        {
            var controller = action.Controller.Frame.GetController<CallbackController>();
            if (controller == null) throw new InvalidOperationException("Can't get CallbackControler");

            var script = GenerateOpeningScript(control, action, PopupWindow.WindowWidth, PopupWindow.WindowHeight,
                                               controller.GetCallbackPanelRefreshCode(true), false, new string[0]);
            if (WebWindow.CurrentRequestWindow != null)
            {
                WebWindow.CurrentRequestWindow.RegisterStartupScript("ShowPopupWindow", script);
            }
        }
    }
}
