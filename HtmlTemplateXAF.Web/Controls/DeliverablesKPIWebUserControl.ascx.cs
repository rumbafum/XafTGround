using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HtmlTemplateXAF.Web.Controls
{
    public partial class DeliverablesKPIWebUserControl : System.Web.UI.UserControl
    {
        public string DocumentType { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            CallbackPanel.ClientInstanceName = DocumentType + "CallbackPanel";
            KPILoadingPanel.ClientInstanceName = DocumentType + "KPILoadingPanel";

            //CallbackPanel.ClientSideEvents.Init = "function(s,e){ " + DocumentType + "CallbackPanel.PerformCallback('');}";
            //CallbackPanel.ClientSideEvents.BeginCallback = "function(s,e){getData('"+ DocumentType +"');}";
            CallbackPanel.ClientSideEvents.Init = "function(s,e){getData('" + DocumentType + "', s.GetMainElement(), " + DocumentType + "KPILoadingPanel);}";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CallbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
        }
    }
}