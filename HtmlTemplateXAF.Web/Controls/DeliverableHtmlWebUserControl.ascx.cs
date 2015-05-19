using DevExpress.ExpressApp;
using DevExpress.Xpo;
using HtmlTemplateXAF.Module.BusinessObjects;
using HtmlTemplateXAF.Module.Web.Editors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HtmlTemplateXAF.Web.Controls
{
    public partial class DeliverableHtmlWebUserControl : UserControl, IDeliverableHtmlEditor
    {

        public string HTML { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsCallback)
            {
                RefreshControl();
            }
        }

        public void RefreshControl()
        {
            if (Deliverable == null) return;
            HTML = Deliverable.Template;
        }

        public void OnDeliverableSaved()
        {

        }

        public void OnDeliverableSaving()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var formDataValue = js.Deserialize<Dictionary<string, object>>(formData.Value);
            if (formDataValue == null)
                return;
            Deliverable.Template = formDataValue["HTML"].ToString();
            Deliverable.TemplateControlRuleCollection = formDataValue["Controls"] as ArrayList;
        }

        public Deliverable Deliverable
        {
            get;
            set;
        }

        public bool IsEditMode
        {
            get;
            set;
        }

    }
}