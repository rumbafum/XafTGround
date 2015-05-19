using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using HtmlTemplateXAF.Module.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.ValidationRules
{
    public interface IDeliverableTemplateRuleProperties : IRuleCriteriaProperties
    {
    }

    [NonPersistent]
    public class DeliverableTemplateRuleProperties : RuleCriteriaProperties, IDeliverableTemplateRuleProperties
    {
        private string _template;
        [RulePropertiesMemberOf("Deliverable")]
        public string Template
        {
            get { return _template; }
            set { _template = value; }
        }

        private ArrayList _rules;
        [RulePropertiesMemberOf("Deliverable")]
        public ArrayList TemplateControlRuleCollection
        {
            get { return _rules; }
            set { _rules = value; }
        }
    }

    public class DeliverableTemplateRule : RuleCriteria
    {
        public DeliverableTemplateRule() : base() { }

        public DeliverableTemplateRule(IRuleCriteriaProperties properties) : base(properties) { }

        protected override bool IsValidInternal(object target, out string errorMessageTemplate)
        {
            Deliverable deliverable = target as Deliverable;
            errorMessageTemplate = "";

            if(deliverable.TemplateControlRuleCollection == null || deliverable.TemplateControlRuleCollection.Count == 0)
                return true;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Invalid Template data: ");
            bool valid = true;
            foreach(Dictionary<string,object> element in deliverable.TemplateControlRuleCollection)
            {
                if (!string.IsNullOrEmpty(element["error"].ToString()))
                {
                    sb.AppendLine("\t" + (string.IsNullOrEmpty(element["fieldName"].ToString()) ? element["control"] : element["fieldName"]) + " - " + element["error"]);
                    valid = false;
                }
            }
            errorMessageTemplate = sb.ToString();
            return valid;
        }

        public override ReadOnlyCollection<string> UsedProperties
        {
            get
            {
                return new ReadOnlyCollection<string>(new List<string>() { "Template", "TemplateControlRuleCollection" });
            }
        }

        protected new IDeliverableTemplateRuleProperties Properties
        {
            get
            {
                return (IDeliverableTemplateRuleProperties)base.Properties;
            }
        }

        public override Type PropertiesType
        {
            get
            {
                return typeof(DeliverableTemplateRuleProperties);
            }
        }
    }

    public class DeliverableTemplateRuleAttribute : RuleBaseAttribute
    {
        protected override Type RuleType
        {
            get { return typeof(DeliverableTemplateRule); }
        }

        protected override Type PropertiesType
        {
            get
            {
                return typeof(DeliverableTemplateRuleProperties);
            }
        }

        public DeliverableTemplateRuleAttribute(string id, string targetContextIDs)
            : base(id, targetContextIDs)
        {
        }
        public DeliverableTemplateRuleAttribute(string id, DefaultContexts targetContexts)
            : base(id, targetContexts)
        {
        }

    }
}
