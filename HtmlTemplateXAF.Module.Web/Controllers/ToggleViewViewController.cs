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
using DevExpress.ExpressApp.ViewVariantsModule;
using HtmlTemplateXAF.Module.BusinessObjects;
using DevExpress.ExpressApp.Scheduler.Web;
using HtmlTemplateXAF.Module.Helpers;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using DevExpress.Utils.Serializing;

namespace HtmlTemplateXAF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ToggleViewViewController : ViewController<ListView>
    {
        public ToggleViewViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetObjectType = typeof(EventBO);
            SimpleAction listAction = new SimpleAction();
            listAction.Caption = "TOGGLE VIEW";
            listAction.Execute += listAction_Execute;
            //SimpleAction calendarAction = new SimpleAction();
            //calendarAction.Caption = "TOGGLE VIEW";
            //calendarAction.Execute += calendarAction_Execute;
            RegisterActions(listAction);
            //RegisterActions(calendarAction);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        void calendarAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ChangeVariantController controller = Frame.GetController<ChangeVariantController>();
            if (controller == null) return;
            controller.ChangeVariantAction.Active["a"] = true;
            controller.ChangeVariantAction.DoExecute(controller.ChangeVariantAction.FindItemByIdPath(
                View.Id == "EventBO_ListView" ? "TheCalendarView" : "TheListView"));
            controller.ChangeVariantAction.Active["a"] = false;
        }

        void listAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ChangeVariantController controller = Frame.GetController<ChangeVariantController>();
            if (controller == null) return;
            controller.ChangeVariantAction.Active["a"] = true;
            controller.ChangeVariantAction.DoExecute(controller.ChangeVariantAction.FindItemByIdPath(
                View.Id == "EventBO_ListView" ? "TheCalendarView" : "TheListView"));
            controller.ChangeVariantAction.Active["a"] = false;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ChangeVariantController controller = Frame.GetController<ChangeVariantController>();
            if (controller == null) return;
            controller.ChangeVariantAction.Active["a"] = false;
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            if (View.Id != "EventBO_ListView")
            {
                var view = (ListView)View;
                if (!(view.Editor is ASPxSchedulerListEditor)) return;
                var scheduler = ((ASPxSchedulerListEditor)view.Editor).SchedulerControl;

                GridViewInfo gridInfo = null;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(GridViewInfo));
                using (XmlReader xr = XmlReader.Create(new StringReader(CurrentPageHelper.GridLayout)))
                    gridInfo = (GridViewInfo)xmlSerializer.Deserialize(xr);
                if (gridInfo == null) return;
                view.CollectionSource.Criteria["MyFilter"] = CriteriaOperator.Parse(gridInfo.FilterExpression);
            }
            else
            {
                var view = (ListView)View;
                if (!(view.Editor is ASPxGridListEditor)) return;
                var grid = ((ASPxGridListEditor)view.Editor).Grid;
                grid.ClientLayout += grid_ClientLayout;
            }

            // Access and customize the target View control.
        }

        void grid_ClientLayout(object sender, DevExpress.Web.ASPxClientLayoutArgs e)
        {
            if (e.LayoutMode == ClientLayoutMode.Loading)
                return;
            StringBuilder sb = new StringBuilder();
            using (XmlWriter xw = XmlWriter.Create(sb))
            {
                xw.WriteStartDocument();

                GridViewInfo g = new GridViewInfo(sender as ASPxGridView);
                foreach (GridViewColumn column in (sender as ASPxGridView).Columns)
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
            
            CurrentPageHelper.GridLayout = sb.ToString();
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
