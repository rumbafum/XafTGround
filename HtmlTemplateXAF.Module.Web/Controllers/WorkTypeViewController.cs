using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using HtmlTemplateXAF.Module.BusinessObjects;
using HtmlTemplateXAF.Module.Web.Controllers.General;
using DevExpress.ExpressApp.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace HtmlTemplateXAF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class WorkTypeViewController : PostbackAwareViewController
    {
        SimpleAction _action;
        string _viewId;

        public WorkTypeViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "WorkType_ListView";
            _action = new SimpleAction(Container);
            _action.Execute += _action_Execute;
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        void _action_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            CollectionSourceBase cs = Application.CreateCollectionSource(os, typeof(WorkType), _viewId);
            e.ShowViewParameters.CreatedView = Application.CreateListView(_viewId, cs, true);
            e.ShowViewParameters.Context = TemplateContext.View;
            e.ShowViewParameters.TargetWindow = TargetWindow.Default;
        }

        public void CreateView(string viewId)
        {
            _viewId = viewId;
            _action.DoExecute();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            if (View.Control == null) return;
            Panel p = View.Control as Panel;
            //if (p == null) return;
            //p.Style.Add("position", "relative");
            //HtmlGenericControl container = new HtmlGenericControl();
            //container.Style.Add("position", "absolute");
            //container.Style.Add("right", "0");
            //container.Style.Add("top", "0");
            //container.Style.Add("width", "100vw");
            //Label l = new Label
            //{
            //    Text = "SOU UM PATIFE!!!!"
            //};
            //container.Controls.Add(l);
            //p.Controls.Add(container);
            // Access and customize the target View control.
        }

        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
