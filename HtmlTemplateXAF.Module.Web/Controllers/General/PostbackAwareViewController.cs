using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using HtmlTemplateXAF.Module.Helpers;

namespace HtmlTemplateXAF.Module.Web.Controllers.General
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class PostbackAwareViewController : ViewController<ListView>
    {
        public PostbackAwareViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        protected override void OnViewChanged()
        {
            base.OnViewChanged();
            CurrentPageHelper.CurrentPageUrl = View == null ? "" : View.Id;
        }

        public bool IsInPostbackTransition(string viewId)
        {
            if (string.IsNullOrEmpty(CurrentPageHelper.CurrentPageUrl))
                return true;
            if (!CurrentPageHelper.CurrentPageUrl.Contains(viewId))
                return true;
            return false;
        }
    }
}
