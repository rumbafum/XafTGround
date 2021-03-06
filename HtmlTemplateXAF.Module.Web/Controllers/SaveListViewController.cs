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
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;
using System.Xml;
using System.Xml.Serialization;

namespace HtmlTemplateXAF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class SaveListViewController : ViewController<ListView>
    {
        ShowNavigationItemController _showNavigationItemController;
        ListViewState _listViewState;

        public SaveListViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "Deliverable_ListView;WorkType_ListView";
            PopupWindowShowAction action = new PopupWindowShowAction();
            action.Caption = "Save layout";
            action.CustomizePopupWindowParams += action_CustomizePopupWindowParams;
            action.Execute += action_Execute;
            RegisterActions(action);
        }

        void ObjectSpace_Committed(object sender, EventArgs e)
        {
            if (_listViewState == null) return;
            ListViewStateHelper.CreateCustomListView(_listViewState, Application);
        }

        void action_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            _listViewState = e.PopupWindowViewCurrentObject as ListViewState;
            if (_listViewState == null)
                return;
            View.ObjectSpace.CommitChanges();
        }

        void action_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var view = (ListView)View;
            if (!(view.Editor is ASPxGridListEditor)) return;
            var gridView = ((ASPxGridListEditor)view.Editor).Grid;

            IObjectSpace objectSpace = Application.CreateObjectSpace();
            ListViewState listViewState = objectSpace.CreateObject<ListViewState>();
            listViewState.Layout = GetGridViewLayout(gridView);
            listViewState.ListView = View.Id;
            listViewState.ObjectType = View.Model.ModelClass.TypeInfo.FullName;
            e.View = Application.CreateDetailView(objectSpace, listViewState);
            e.DialogController.ViewClosed += DialogController_ViewClosed;
            ((DetailView)e.View).ViewEditMode = ViewEditMode.Edit;
        }

        private string GetGridViewLayout(ASPxGridView gridView)
        {
            StringBuilder sb = new StringBuilder();
            using (XmlWriter xw = XmlWriter.Create(sb))
            {
                xw.WriteStartDocument();

                GridViewInfo g = new GridViewInfo(gridView);
                foreach (GridViewColumn column in gridView.Columns)
                {
                    if (column is GridViewDataColumn)
                        g.Columns.Add(new DataColumnInfo(column as GridViewDataColumn));
                    else if (column is GridViewCommandColumn)
                        g.Columns.Add(new CommandColumnInfo(column as GridViewCommandColumn));
                    else if (column is GridViewBandColumn)
                        g.Columns.Add(new BandColumnInfo(column as GridViewBandColumn));
                }
                XmlSerializer xmlSerializer = new XmlSerializer(g.GetType());
                xmlSerializer.Serialize(xw, g);
            }
            return sb.ToString();
        }

        void DialogController_ViewClosed(object sender, EventArgs e)
        {
            if (_listViewState == null) return;
            ListViewStateHelper.UpdateNavigationItems(_showNavigationItemController);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            View.ObjectSpace.Committed += ObjectSpace_Committed;
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
            View.ObjectSpace.Committed -= ObjectSpace_Committed;
        }
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            _showNavigationItemController = Frame.GetController<ShowNavigationItemController>();
        }
    }
}
