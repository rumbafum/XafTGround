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

namespace HtmlTemplateXAF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ListViewStateViewController : ViewController
    {
        public ListViewStateViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetObjectType = typeof(ListViewState);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            Frame.GetController<DeleteObjectsViewController>().DeleteAction.Executed += DeleteAction_Executed;
            Frame.GetController<DeleteObjectsViewController>().DeleteAction.Execute += DeleteAction_Execute;
            Frame.GetController<ShowNavigationItemController>();
        }

        void DeleteAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            foreach(ListViewState view in e.SelectedObjects)
                ListViewStateHelper.RemoveNode(view, View);
        }

        void DeleteAction_Executed(object sender, ActionBaseEventArgs e)
        {
            ListViewStateHelper.UpdateNavigationItems(Frame.GetController<ShowNavigationItemController>());
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
    }
}
