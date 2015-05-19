using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class WorkType : BaseObject
    {
        //Guid _oid;
        //[Key(true), VisibleInListView(false), VisibleInDetailView(false)]
        //public Guid Oid
        //{
        //    get { return _oid; }
        //    set { SetPropertyValue("Oid", ref _oid, value); }
        //}

        string _name;
        public string Name
        {
            get { return _name; }
            set { SetPropertyValue("Name", ref _name, value); }
        }

        bool _active;
        public bool Active
        {
            get { return _active; }
            set { SetPropertyValue("Active", ref _active, value); }
        }

        private string _templateText;
        [VisibleInListView(false), VisibleInDetailView(true), Size(SizeAttribute.Unlimited)]
        public string TemplateText
        {
            get { return _templateText; }
            set { SetPropertyValue("TemplateText", ref _templateText, value); }
        }


        public WorkType(Session session) : base(session)
        {           
        }

        public WorkType(): base(Session.DefaultSession)
        {           
        }
    }
}
