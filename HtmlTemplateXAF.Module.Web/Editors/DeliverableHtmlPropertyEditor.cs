﻿using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Web;
using HtmlTemplateXAF.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HtmlTemplateXAF.Module.Web.Editors
{
    [PropertyEditor(typeof(string), false)]
    public class DeliverableHtmlPropertyEditor : WebPropertyEditor
    {
        private ASPxCallbackPanel _control;

        private static ASPxCallbackPanel CreateEditorControl()
        {
            var control = new ASPxCallbackPanel();
            control.ClientInstanceName = "DeliverablePanelEditor";
            control.RenderMode = RenderMode.Div;
            RenderHelper.SetupASPxWebControl(control);
            return control;
        }

        private void SetupControlValues(Control webControl, bool isEditMode)
        {
            var deliverable = CurrentObject as Deliverable;
            if (deliverable == null) return;
            var lcontrolDeliverable = WebWindow.CurrentRequestPage.LoadControl("Controls/DeliverableHtmlWebUserControl.ascx");
            var htmlControlControlProject = lcontrolDeliverable as IDeliverableHtmlEditor;
            if (htmlControlControlProject == null) return;
            htmlControlControlProject.Deliverable = deliverable;
            htmlControlControlProject.IsEditMode = isEditMode;
            webControl.Controls.Add(lcontrolDeliverable);
            RenderHelper.SetupASPxWebControl(webControl as ASPxCallbackPanel);
        }

        public DeliverableHtmlPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        protected override object GetControlValueCore()
        {
            return null;
        }

        protected override WebControl CreateViewModeControlCore()
        {
            var control = CreateEditorControl();
            return control;
        }

        protected override WebControl CreateEditModeControlCore()
        {
            _control = CreateEditorControl();
            return _control;
        }

        protected override void ReadEditModeValueCore()
        {
            SetupControlValues(Editor, true);
        }

        protected override void ReadViewModeValueCore()
        {
            SetupControlValues(InplaceViewModeEditor, false);
        }
    }
}
