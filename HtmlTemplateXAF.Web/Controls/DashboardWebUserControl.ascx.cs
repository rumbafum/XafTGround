using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HtmlTemplateXAF.Web.Controls
{
    public partial class DashboardWebUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //protected void DeliverablesCallbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        //{
        //    DeliverablesCount.Text = "1234567890 estragáveis";
        //}

        //protected void ProjectsCallbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        //{
        //    ProjectsCount.Text = "1000 Projectos";
        //}

        protected void SkillsCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            //if (e.Parameter == "project")
            //{
            //    int i = 0;
            //    while (i < 10000000)
            //    {
            //        i++;
            //    }
            //    e.Result = "Fetched project";
            //}
            //if (e.Parameter == "deliverable")
            //{
            //    int i = 0;
            //    while (i < 1000000000)
            //    {
            //        i++;
            //    }
            //    e.Result = "Fetched deliverable";
            //}
        }
    }
}