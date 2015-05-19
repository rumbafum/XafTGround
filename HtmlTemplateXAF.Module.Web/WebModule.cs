using System;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using HtmlTemplateXAF.Module.ValidationRules;
using DevExpress.ExpressApp.Validation;

namespace HtmlTemplateXAF.Module.Web
{
    [ToolboxItemFilter("Xaf.Platform.Web")]
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppModuleBasetopic.
    public sealed partial class HtmlTemplateXAFAspNetModule : ModuleBase
    {
        public HtmlTemplateXAFAspNetModule()
        {
            InitializeComponent();
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            return ModuleUpdater.EmptyModuleUpdaters;
        }
        public override void ExtendModelInterfaces(DevExpress.ExpressApp.Model.ModelInterfaceExtenders extenders)
        {
            base.ExtendModelInterfaces(extenders);
            extenders.Add<Module.Editors.IDeliverableTemplateModelViewItem, Editors.IDeliverableTemplateWebModelViewItem>();
            extenders.Add<Module.Editors.IModelCustomUserControlViewItem, Editors.IModelWebCustomUserControlViewItem>();
        }
        public override void Setup(ApplicationModulesManager moduleManager)
        {
            base.Setup(moduleManager);
            ValidationRulesRegistrator.RegisterRule(moduleManager, typeof(DeliverableTemplateRule), typeof(IDeliverableTemplateRuleProperties));
        }
        public override void Setup(XafApplication application)
        {
            base.Setup(application);
            // Manage various aspects of the application UI and behavior at the module level.
        }
    }
}
