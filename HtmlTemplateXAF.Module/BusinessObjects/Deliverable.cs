using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using HtmlTemplateXAF.Module.ValidationRules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DeliverableTemplateRule("TemplateRule", DefaultContexts.Save)]
    public class Deliverable : BaseObject
    {
        
        string _name;
        [RuleRequiredField("RuleRequiredField for Deliverable.Name", DefaultContexts.Save)]
        public string Name
        {
            get { return _name; }
            set { SetPropertyValue("Name", ref _name, value); }
        }

        [RuleRequiredField("RuleRequiredField for Deliverable.Client", DefaultContexts.Save)]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public string Client
        {
            get { return GetPropertyValue<string>("Client"); }
            set { SetPropertyValue("Client", value); }
        }

        [RuleRequiredField("RuleRequiredField for Deliverable.WorkType", DefaultContexts.Save)]
        [ImmediatePostData]
        public WorkType WorkType
        {
            get { return GetPropertyValue<WorkType>("WorkType"); }
            set { SetPropertyValue("WorkType", value); }
        }

        [VisibleInListView(false), VisibleInDetailView(true), Size(SizeAttribute.Unlimited)]
        [ImmediatePostData]
        public string Template
        {
            get { return GetPropertyValue<string>("Template"); }
            set { SetPropertyValue("Template", value); }
        }

        [NonPersistent]
        ArrayList _templateControlRules;
        public ArrayList TemplateControlRuleCollection
        {
            set { _templateControlRules = value; }
            get { return _templateControlRules; }
        }

        [NonPersistent, VisibleInDetailView(false), VisibleInListView(true)]
        public string PreviewAction
        {
            get
            {
               return Oid.ToString();
            }
        }

        public Deliverable(Session session) : base(session)
        {           
        }

        public Deliverable() : base(Session.DefaultSession)
        {           
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (WorkType != null)
                Template = WorkType.TemplateText;
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            switch (propertyName)
            {
                case "WorkType":
                    if (Oid != Guid.NewGuid() && WorkType != null)
                        Template = WorkType.TemplateText;
                    else
                        Template = null;
                    break;
                default:
                    break;
            }
        }
    }
}
