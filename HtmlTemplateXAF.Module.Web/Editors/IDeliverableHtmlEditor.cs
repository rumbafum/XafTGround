using DevExpress.ExpressApp;
using DevExpress.Xpo;
using HtmlTemplateXAF.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.Web.Editors
{
    public interface IDeliverableHtmlEditor
    {
        Deliverable Deliverable { get; set; }
        bool IsEditMode { get; set; }

        void RefreshControl();

        void OnDeliverableSaving();

        void OnDeliverableSaved();
    }
}
