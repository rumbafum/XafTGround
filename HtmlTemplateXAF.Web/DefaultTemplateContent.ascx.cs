﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DevExpress.ExpressApp.Web.Controls;
using DevExpress.ExpressApp.Web.Templates.ActionContainers;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Templates;
using HtmlTemplateXAF.Module.Web.Controllers.Skills;
using DevExpress.Web;
namespace HtmlTemplateXAF.Web
{
    public partial class DefaultTemplateContent : TemplateContent
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (WebWindow.CurrentRequestWindow != null)
            {
                WebWindow.CurrentRequestWindow.PagePreRender += new EventHandler(CurrentRequestWindow_PagePreRender);
            }
            AppearanceStyleBase b = new AppearanceStyleBase();
            b.CssClass = "LALA";
            SpaceTabTemplateStyle style = new SpaceTabTemplateStyle();
            NTAC.PageControl.SpaceAfterTabsTemplateStyle.Assign(b);
        }
        protected override void OnUnload(EventArgs e)
        {
            if (WebWindow.CurrentRequestWindow != null)
            {
                WebWindow.CurrentRequestWindow.PagePreRender -= new EventHandler(CurrentRequestWindow_PagePreRender);
            }
            base.OnUnload(e);
        }
        private void CurrentRequestWindow_PagePreRender(object sender, EventArgs e)
        {
            WebWindow window = (WebWindow)sender;
            string isLeftPanelVisible = (VTC.HasActiveActions() || DAC.HasActiveActions()).ToString().ToLower();
            window.RegisterStartupScript("OnLoadCore", string.Format(@"Init(""{1}"", ""DefaultCS"");OnLoadCore(""LPcell"", ""separatorCell"", ""separatorImage"", {0}, true);", isLeftPanelVisible, BaseXafPage.CurrentTheme), true);
        }
        public override IActionContainer DefaultContainer
        {
            get
            {
                if (TB != null)
                {
                    return TB.FindActionContainerById("View");
                }
                return null;
            }
        }
        public override void SetStatus(ICollection<string> statusMessages)
        {
            InfoMessagesPanel.Text = string.Join("<br>", new List<string>(statusMessages).ToArray());
        }
        public override object ViewSiteControl
        {
            get
            {
                return VSC;
            }
        }
        public ThemedImageControl HeaderImageControl { get { return TIC; } }


        
    }
}
