using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace HtmlTemplateXAF.Module.Web.Controllers.Skills
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class SkillsFillActionContainersController : FillActionContainersController
    {
        public SkillsFillActionContainersController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        protected override void FillContainers(IEnumerable<IActionContainer> containers, IList<Controller> controllers, IModelActionToContainerMapping actionToContainerMapping, IFrameTemplate template)
        {
            base.FillContainers(containers, controllers, actionToContainerMapping, template);
            
        }

        protected override void FillViewContainers(CompositeView view, IList<Controller> controllers, IModelActionToContainerMapping actionToContainerMapping)
        {
            base.FillViewContainers(view, controllers, actionToContainerMapping);
            OnContainersFilled(EventArgs.Empty);
        }

        public event EventHandler ContainersFilled;

        protected void OnContainersFilled(EventArgs args)
        {
            if (this.ContainersFilled != null)
                this.ContainersFilled(this, args);
        }
    }
}
