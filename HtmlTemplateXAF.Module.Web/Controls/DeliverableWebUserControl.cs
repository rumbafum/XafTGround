using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Web;
using System.Web.UI;
using System.Reflection;
using System.IO;

namespace HtmlTemplateXAF.Module.Web.Controls
{
    public partial class DeliverableWebUserControl : ASPxWebControl
    {
        protected override void GetCreateClientObjectScript(System.Text.StringBuilder stb, string localVarName, string clientName)
        {
            stb.AppendFormat(@"dxo.Init.AddHandler(function(){{setupDeliverables();}}, this); ");
        }

        //protected override string GetClientObjectClassName()
        //{
        //    return "deliverablesWebUserControl";
        //}

        protected override void RegisterIncludeScripts()
        {
            base.RegisterIncludeScripts();
            RegisterIncludeScript(typeof(DeliverableWebUserControl), "HtmlTemplateXAF.Module.Web.Scripts.deliverableInit.js");
        }

        protected override void RenderInternal(HtmlTextWriter writer)
        {
            //base.RenderInternal(writer);
            //var asm = Assembly.GetExecutingAssembly();
            //var htmlStream = asm.GetManifestResourceStream("Skill.Module.Web.Views.deliverablesWebUserControl.html");
            //var htmlTextReader = new StreamReader(htmlStream);
            //var html = htmlTextReader.ReadToEnd();
            base.RenderInternal(writer);
            writer.Write(@"<div id='deliverables'>");
            //writer.Write(html);
            writer.Write("</div>");
        }

        protected override bool HasRootTag()
        {
            return true;
        }

        protected override bool HasClientInitialization()
        {
            return true;
        }
    }
}
