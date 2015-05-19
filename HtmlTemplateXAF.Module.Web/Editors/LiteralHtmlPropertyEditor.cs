using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.HtmlPropertyEditor.Web;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxSpellChecker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace HtmlTemplateXAF.Module.Web.Editors
{
    [PropertyEditor(typeof(string), false)]
    public class LiteralHtmlPropertyEditor : ASPxHtmlPropertyEditor
    {
        public LiteralHtmlPropertyEditor(Type objectType, IModelMemberViewItem info) :
            base(objectType, info) { }

        protected override void SetupControl(WebControl control)
        {
            base.SetupControl(control);
            control.CssClass = "htmlEditorLimit";
            if (ViewEditMode != ViewEditMode.Edit) return;
            var editor = control as ASPxHtmlEditor;
            if (editor == null) return;
            editor.SettingsHtmlEditing.AllowIdAttributes = true;
            editor.SettingsHtmlEditing.AllowStyleAttributes = true;
            editor.SettingsHtmlEditing.AllowScripts = true;
            editor.SettingsHtmlEditing.AllowFormElements = true;
            editor.Settings.AllowDesignView = false;
            editor.Settings.AllowHtmlView = false;
            editor.Styles.ContentArea.Border.BorderStyle = BorderStyle.None;
            editor.Styles.ContentArea.Paddings.Padding = Unit.Pixel(0);
            editor.Styles.PreviewArea.BackColor = Color.White;
            editor.HtmlCorrecting += editor_HtmlCorrecting;
            editor.CssClass = "htmlEditorLimit";
            editor.Styles.HtmlViewArea.CssClass = "htmlEditorLimit";
            editor.Styles.DesignViewArea.CssClass = "htmlEditorLimit";
            editor.Styles.ContentArea.CssClass = "htmlEditorLimit";
            editor.Styles.ViewArea.CssClass = "htmlEditorLimit";
            editor.Styles.PreviewArea.CssClass = "htmlEditorLimit";
        }

        private void editor_HtmlCorrecting(object sender, HtmlCorrectingEventArgs e)
        {
            e.Handled = true;
        }


        protected override object GetControlValueCore()
        {
            return "<div style='font-family:Arial;font-size:10pt;'>" + base.GetControlValueCore() + "</div>";
        }

    }
}
