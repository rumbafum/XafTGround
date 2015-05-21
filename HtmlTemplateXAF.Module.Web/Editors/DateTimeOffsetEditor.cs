using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Editors;
using DevExpress.Web;
using HtmlTemplateXAF.Module.BusinessObjects;
using HtmlTemplateXAF.Module.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace HtmlTemplateXAF.Module.Web.Editors
{
    [PropertyEditor(typeof(DateTimeOffset), true)]
    public class DateTimeOffsetEditor : WebPropertyEditor
    {
        ASPxDateEdit _control;

        public DateTimeOffsetEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        private static ASPxDateEdit CreateEditorControl()
        {
            var control = new ASPxDateEdit
            {
                EditFormat = EditFormat.Custom,
                DisplayFormatString = "dd/MM/yyyy HH:mm:ss",
                EditFormatString = "dd/MM/yyyy HH:mm:ss",
            };
            RenderHelper.SetupASPxWebControl(control);
            return control;
        }

        //private DateTimeOffset FromDateOffsetToLocalDateTime(DateTimeOffset dt)
        //{
        //    DateTime local = TimeZoneInfo.ConvertTimeFromUtc(dt.UtcDateTime, SkillsGlobalSettings.Instance.DefaultTimeZone);
        //    DateTimeOffset localDateOffset = new DateTimeOffset(local, SkillsGlobalSettings.Instance.DefaultTimeZone.GetUtcOffset(dt));
        //    return localDateOffset;
        //    //DateTime now = dt.UtcDateTime.AddMinutes(-SkillsGlobalSettings.Instance.ClientOffset);

        //    //return now;
        //}

        private DateTimeOffset FromLocalDateToDateTimeOffset(DateTime dt)
        {
            DateTimeOffset dto = new DateTimeOffset(dt, SkillsGlobalSettings.Instance.DefaultTimeZone.GetUtcOffset(dt));
            return dto;
        }

        private void SetupControlValues(WebControl webControl, bool inViewMode)
        {
            if (PropertyValue == null) return;
            if(inViewMode)
                ((ASPxLabel)webControl).Text = DateTimeHelper.ConvertToLocalTime(((DateTimeOffset)PropertyValue)).DateTime.ToString("dd/MM/yyyy");
            else
                ((ASPxDateEdit)webControl).Value = DateTimeHelper.ConvertToLocalTime(((DateTimeOffset)PropertyValue)).DateTime;
        }

        protected override object GetControlValueCore()
        {
            var value = ((ASPxDateEdit)Editor).Value;
            if (value == null) return null;
            return FromLocalDateToDateTimeOffset((DateTime)value);
        }

        protected override WebControl CreateViewModeControlCore()
        {
            //var control = CreateEditorControl();
            var control = new ASPxLabel();
            return control;
        }

        protected override WebControl CreateEditModeControlCore()
        {
            _control = CreateEditorControl();
            _control.ValueChanged += EditValueChangedHandler;
            return _control;
        }

        protected override void ReadEditModeValueCore()
        {
            SetupControlValues(Editor, false);
        }

        protected override void ReadViewModeValueCore()
        {
            SetupControlValues(InplaceViewModeEditor, true);
        }

        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            if (_control != null)
                _control.ValueChanged -= EditValueChangedHandler;
            base.BreakLinksToControl(unwireEventsOnly);
        }
    }
}
