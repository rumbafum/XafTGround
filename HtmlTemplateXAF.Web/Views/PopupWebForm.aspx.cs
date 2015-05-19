using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using HtmlTemplateXAF.Module.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HtmlTemplateXAF.Web.Views
{
    public partial class PopupWebForm : System.Web.UI.Page
    {
        Frame _workFrame;
        WorkTypeViewController _controller;

        protected void Page_Load(object sender, EventArgs e)
        {
            _workFrame = WebApplication.Instance.CreateFrame(TemplateContext.View);
            
            _workFrame.SetView(WebApplication.Instance.CreateDashboardView(WebApplication.Instance.CreateObjectSpace(), "PopupDashboardView", true));
            _workFrame.View.ControlsCreated += View_ControlsCreated;
            _controller = _workFrame.GetController<WorkTypeViewController>();
            _workFrame.View.CreateControls();
        }

        private void View_ControlsCreated(object sender, EventArgs e)
        {
            this.form1.Controls.Add(_workFrame.View.Control as Control);
            _controller.CreateView("WorkType_ListView");
        }


    }
}