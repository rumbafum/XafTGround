using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using HtmlTemplateXAF.Module.BusinessObjects.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace HtmlTemplateXAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class EventBO : Event
    {
        public EventBO(Session session) : base(session)
        {           
        }

        public EventBO()
            : base(Session.DefaultSession)
        {
        }

        [RuleRequiredField("RuleRequiredField for EventBO.Name", DefaultContexts.Save)]
        [VisibleInListView(true), VisibleInDetailView(true)]
        public string Name
        {
            get { return GetPropertyValue<string>("Name"); }
            set { SetPropertyValue("Name", value); }
        }

        [VisibleInListView(true), VisibleInDetailView(true)]
        [ValueConverter(typeof(DateTimeOffsetConverter))]
        [DbType("DateTimeOffset")]
        public DateTimeOffset BeginDate
        {
            get { return GetPropertyValue<DateTimeOffset>("BeginDate"); }
            set { SetPropertyValue("BeginDate", value); }
        }

        [VisibleInListView(true), VisibleInDetailView(true)]
        [ValueConverter(typeof(DateTimeOffsetConverter))]
        [DbType("DateTimeOffset")]
        public DateTimeOffset EndDate
        {
            get { return GetPropertyValue<DateTimeOffset>("EndDate"); }
            set { SetPropertyValue("EndDate", value); }
        }

        [VisibleInListView(true), VisibleInDetailView(true)]
        [ValueConverter(typeof(DateTimeOffsetConverter))]
        [DbType("DateTimeOffset")]
        [Appearance("DisableProperty", Criteria = "1=1", Visibility = ViewItemVisibility.Hide )]
        public DateTimeOffset CreateDate
        {
            get { return GetPropertyValue<DateTimeOffset>("CreateDate"); }
            set { SetPropertyValue("CreateDate", value); }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            BeginDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            CreateDate = DateTime.Now;
        }
    }
}
