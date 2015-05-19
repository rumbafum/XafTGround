using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.HtmlPropertyEditor.Web;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxSpellChecker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace HtmlTemplateXAF.Module.Web.Editors
{
    [PropertyEditor(typeof(string), false)]
    public class HtmlPropertyEditor : ASPxHtmlPropertyEditor
    {
        public HtmlPropertyEditor(Type objectType, IModelMemberViewItem info) :
            base(objectType, info) { }


        private static void SetItemSelected(IEnumerable toolbarListEditItemCollection, string selectItemValue)
        {
            foreach (ToolbarListEditItem toolbarListEditItem in toolbarListEditItemCollection)
                if (toolbarListEditItem.Value.ToString() == selectItemValue)
                    toolbarListEditItem.Selected = true;
        }

        private void SetDefaultFont(ASPxHtmlEditor asPxHtmlEditor)
        {
            ToolbarFontSizeEdit toolbarFontSizeEdit = null;
            ToolbarFontNameEdit toolbarFontNameEdit = null;
            foreach (HtmlEditorToolbar toolBar in asPxHtmlEditor.Toolbars)
                foreach (var toolBarItem in toolBar.Items)
                {
                    if (toolBarItem as ToolbarFontSizeEdit != null)
                        toolbarFontSizeEdit = toolBarItem as ToolbarFontSizeEdit;
                    if (toolBarItem as ToolbarFontNameEdit != null)
                        toolbarFontNameEdit = toolBarItem as ToolbarFontNameEdit;
                }
            if (toolbarFontSizeEdit != null) SetItemSelected(toolbarFontSizeEdit.Items, "2");
            if (toolbarFontNameEdit != null) SetItemSelected(toolbarFontNameEdit.Items, "Arial");
        }

        protected override void SetupControl(WebControl control)
        {
            base.SetupControl(control);
            control.Height = 400;
            control.Width = 800;
            control.CssClass = "htmlEditorLimit";
            if (ViewEditMode != ViewEditMode.Edit) return;
            var editor = control as ASPxHtmlEditor;
            if (editor == null) return;
            editor.Width = 800;
            editor.SettingsHtmlEditing.AllowIdAttributes = true;
            editor.SettingsHtmlEditing.AllowStyleAttributes = true;
            editor.SettingsHtmlEditing.AllowScripts = true;
            editor.SettingsHtmlEditing.AllowFormElements = true;
            editor.HtmlCorrecting += editor_HtmlCorrecting;
            if (editor.Font.Size.IsEmpty) editor.Font.Size = 10;
            editor.CssClass = "htmlEditorLimit";
            editor.Styles.HtmlViewArea.CssClass = "htmlEditorLimit";
            editor.Styles.DesignViewArea.CssClass = "htmlEditorLimit";
            editor.Styles.ContentArea.CssClass = "htmlEditorLimit";
            editor.Styles.ViewArea.CssClass = "htmlEditorLimit";
            editor.Styles.PreviewArea.CssClass = "htmlEditorLimit";
            //SetDefaultFont(editor);
            editor.SettingsImageUpload.ValidationSettings.AllowedFileExtensions = new[] { ".jpeg", ".gif", ".png", ".bmp", ".jpg" };
            if (editor.Toolbars.Count > 0)
            {
                editor.Toolbars[1].Items.Add(new ToolbarCheckSpellingButton());
            }
        }

        private void editor_HtmlCorrecting(object sender, HtmlCorrectingEventArgs e)
        {
            e.Handled = true;
        }


        protected override object GetControlValueCore()
        {
            return base.GetControlValueCore();
        }

        //protected override 

        private static HtmlEditorToolbar CreateCustomToolbar()
        {
            var customToolbar = new HtmlEditorToolbar();
            var bold = new ToolbarBoldButton();
            ASPxImageHelper.SetImageProperties(bold.Image, "bold");
            bold.ViewStyle = ViewStyle.Image;
            var italic = new ToolbarItalicButton();
            ASPxImageHelper.SetImageProperties(italic.Image, "italic");
            italic.ViewStyle = ViewStyle.Image;
            var underline = new ToolbarUnderlineButton();
            ASPxImageHelper.SetImageProperties(underline.Image, "underline");
            underline.ViewStyle = ViewStyle.Image;
            var unorderedList = new ToolbarInsertUnorderedListButton();
            ASPxImageHelper.SetImageProperties(unorderedList.Image, "bullets");
            unorderedList.ViewStyle = ViewStyle.Image;
            var foreColor = new ToolbarFontColorButton();
            ASPxImageHelper.SetImageProperties(foreColor.Image, "color");
            foreColor.ViewStyle = ViewStyle.Image;
            var fontName = new ToolbarFontNameEdit();
            fontName.CreateDefaultItems();
            var fontSize = new ToolbarFontSizeEdit();
            fontSize.CreateDefaultItems();
            var checkSpellingButton = new ToolbarCheckSpellingButton();
            customToolbar.Items.Add(bold);
            customToolbar.Items.Add(italic);
            customToolbar.Items.Add(underline);
            customToolbar.Items.Add(unorderedList);
            customToolbar.Items.Add(foreColor);
            customToolbar.Items.Add(fontName);
            customToolbar.Items.Add(fontSize);
            customToolbar.Items.Add(checkSpellingButton);
            return customToolbar;
        }
    }
}
