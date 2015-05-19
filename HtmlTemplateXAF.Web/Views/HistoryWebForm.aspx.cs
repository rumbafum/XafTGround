using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Templates;
using HtmlTemplateXAF.Module.BusinessObjects;
using HtmlTemplateXAF.Module.Editors;
using HtmlTemplateXAF.Module.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace HtmlTemplateXAF.Web.Views
{
    public partial class HistoryWebForm : Page, ICallbackManagerHolder
    {
        Frame _workFrame;
        WorkTypeViewController _controller;

        protected void Page_Init()
        {
            _workFrame = WebApplication.Instance.CreateFrame(TemplateContext.ApplicationWindow);
            _workFrame.CreateTemplate();
            _controller = _workFrame.GetController<WorkTypeViewController>();


            _workFrame.SetView(WebApplication.Instance.CreateListView(
                WebApplication.Instance.CreateObjectSpace(), typeof(WorkType), true), _workFrame);
            //_controller = _workFrame.GetController<WorkTypeViewController>();
            _workFrame.View.ControlsCreated += View_ControlsCreated;
            //_workFrame.View.CreateControls();
        }

        void HistoryWebForm_ViewControlsCreated(object sender, EventArgs e)
        {
            //this.form2.Controls.Add(_workFrame.View.Control as Control);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        void _controller_ViewControlsCreated(object sender, EventArgs e)
        {
            //this.form2.Controls.Add(_workFrame.View.Control as Control);
        }

        void View_ControlsCreated(object sender, EventArgs e)
        {
            this.form2.Controls.Add(_workFrame.View.Control as Control);
        }

        public XafCallbackManager CallbackManager
        {
            get { return new XafCallbackManager(); }
        }
    }
}