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
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;
using DevExpress.Utils;
using System.Web.UI;
using System.Web;
using System.Web.UI.HtmlControls;
using DevExpress.ExpressApp.Web;
using HtmlTemplateXAF.Module.Helpers;
using HtmlTemplateXAF.Module.Web.Controllers.General;

namespace HtmlTemplateXAF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class DeliverableListViewController : PostbackAwareViewController
    {
        private string currentPageUrl = "";

        public DeliverableListViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "Deliverable_ListView";
        }
        protected override void OnActivated()
        {
            base.OnActivated();
        }
        protected override void OnViewControlsCreated()
        {
            if (IsInPostbackTransition("Deliverable_ListView")) return;
            //HttpContext.Current.Request
            base.OnViewControlsCreated();

            var listEditor = ((ListView)View).Editor as ASPxGridListEditor;
            if (listEditor == null) return;
            var gridView = listEditor.Grid;
            gridView.ClientVisible = true;
            var previewColumn = gridView.Columns["PreviewAction"] as GridViewDataColumn;
            if (previewColumn != null)
            {
                OverrideColumnBehaviour(previewColumn, new PreviewHyperLinkTemplate());
            }
                

        }

        private void OverrideColumnBehaviour(GridViewDataColumn column, ITemplate template)
        {
            column.Settings.AllowHeaderFilter = DefaultBoolean.False;
            column.Settings.AllowSort = DefaultBoolean.False;
            column.Settings.AllowGroup = DefaultBoolean.False;
            column.Settings.AllowAutoFilter = DefaultBoolean.False;
            column.DataItemTemplate = template;
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
    }

    public class PreviewHyperLinkTemplate : ITemplate
    {
        void ITemplate.InstantiateIn(Control container)
        {
            var gridViewDataItemTemplateContainer = container as GridViewDataItemTemplateContainer;
            if (gridViewDataItemTemplateContainer == null) return;
            var previewString = HttpUtility.HtmlDecode(gridViewDataItemTemplateContainer.Text);
            var panel = new ASPxPanel();
            var previewButton = new ASPxButton
            {
                RenderMode = ButtonRenderMode.Link,
                Text = "TEST",
                AutoPostBack = false
            };
            previewButton.ClientSideEvents.Click = "function(s,e){ Skill.Global.openSkillsPopup(774, 240, 'Preview', true, 'Views/HistoryWebForm.aspx'); }";
            var buttonDiv = new HtmlGenericControl("div");
            buttonDiv.Attributes.Add("title", "Preview");
            buttonDiv.Attributes.Add("class", "iconsClass");
            buttonDiv.Controls.Add(previewButton);
            panel.Controls.Add(buttonDiv);
            panel.Attributes["onclick"] = RenderHelper.EventCancelBubbleCommand;
            panel.Attributes.Add("data-previewcontainer", previewString);
            panel.Attributes.Add("data-listGridView", gridViewDataItemTemplateContainer.Grid.ClientInstanceName);
            container.Controls.Add(panel);
        }
    }

}
