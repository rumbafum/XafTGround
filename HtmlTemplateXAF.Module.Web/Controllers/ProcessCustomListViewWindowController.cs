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
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using HtmlTemplateXAF.Module.BusinessObjects;

namespace HtmlTemplateXAF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppWindowControllertopic.
    public partial class ProcessCustomListViewWindowController : WindowController
    {
        ShowNavigationItemController _showNavigationItemController;

        public ProcessCustomListViewWindowController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            IObjectSpace os = Application.CreateObjectSpace();
            var views = os.GetObjects<ListViewState>();
            IModelList<IModelView> modelViews = Application.Model.Views;

            foreach (var view in views)
            {
                IModelView myViewNode = modelViews[string.Format("{0}_{1}__ListView", view.Name, view.ListView)];
                if (myViewNode != null)
                    continue;
                ListViewStateHelper.CreateCustomListView(view, Application);
            }
            _showNavigationItemController = Frame.GetController<ShowNavigationItemController>();
            if (_showNavigationItemController != null)
            {
                _showNavigationItemController.NavigationItemCreated += showNavigationItemController_NavigationItemCreated;
                _showNavigationItemController.CustomShowNavigationItem += showNavigationItemController_CustomShowNavigationItem;
            }
        }

        private void showNavigationItemController_NavigationItemCreated(object sender, NavigationItemCreatedEventArgs e)
        {
            if (e.ModelNavigationItem.View == null)
                return;
            if ((e.ModelNavigationItem.View as IModelListView) == null) return;
            string objectType = ((IModelListView)e.ModelNavigationItem.View).ModelClass.Name;
            ChoiceActionItem navigationItem = e.NavigationItem;
            IObjectSpace os = Application.CreateObjectSpace();
            var views = os.GetObjects<ListViewState>().Where(lvs => lvs.ObjectType == objectType);

            IModelList<IModelView> modelViews = Application.Model.Views;
            
            if (!string.IsNullOrEmpty(navigationItem.Model.Id))
            {
                foreach (var view in views)
                {
                    IModelView myViewNode = modelViews[string.Format("{0}_{1}__ListView", view.Name, view.ListView)];
                    if (myViewNode == null)
                        continue;
                    ViewShortcut shortcut = new ViewShortcut(Type.GetType(view.ObjectType), "", string.Format("{0}_{1}__ListView", view.Name, view.ListView));
                    ChoiceActionItem newItem = new ChoiceActionItem(
                        string.Format("{0}_{1}__ListView", view.Name, view.ListView), view.Name, shortcut) { ImageName = e.NavigationItem.ImageName };
                    navigationItem.ParentItem.Items.Add(newItem);
                }
            }
        }

        private void showNavigationItemController_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e)
        {
            
        }
    }
}
