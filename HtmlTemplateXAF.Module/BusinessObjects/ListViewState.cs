using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class ListViewState : BaseObject
    {
        string _listView;
        [RuleRequiredField("RuleRequiredField for ListViewState.ListView", DefaultContexts.Save)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string ListView
        {
            get { return _listView; }
            set { SetPropertyValue("ListView", ref _listView, value); }
        }

        string _layout;
        [RuleRequiredField("RuleRequiredField for ListViewState.State", DefaultContexts.Save)]
        [Size(SizeAttribute.Unlimited)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Layout
        {
            get { return _layout; }
            set { SetPropertyValue("Layout", ref _layout, value); }
        }

        string _name;
        [RuleRequiredField("RuleRequiredField for ListViewState.Name", DefaultContexts.Save)]
        public string Name
        {
            get { return _name; }
            set { SetPropertyValue("Name", ref _name, value); }
        }

        string _objectType;
        [RuleRequiredField("RuleRequiredField for ListViewState.ObjectType", DefaultContexts.Save)]
        public string ObjectType
        {
            get { return _objectType; }
            set { SetPropertyValue("ObjectType", ref _objectType, value); }
        }

        public ListViewState(Session session) : base(session) { }
        public ListViewState() : base(Session.DefaultSession) { }
    }
}
