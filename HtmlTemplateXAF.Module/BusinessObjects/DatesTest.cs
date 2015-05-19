using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using HtmlTemplateXAF.Module.BusinessObjects.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class DatesTest : BaseObject
    {
        public DatesTest(Session session)
            : base(session)
        {
        }

        public DatesTest()
            : base(Session.DefaultSession)
        {
        }

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
        public DateTime CreateDate
        {
            get { return GetPropertyValue<DateTime>("CreateDate"); }
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
        }
    }
}
