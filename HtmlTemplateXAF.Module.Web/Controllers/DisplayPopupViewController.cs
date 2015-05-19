using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;

namespace HtmlTemplateXAF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class DisplayPopupViewController : ViewController
    {
        PopupWindowShowAction _popupAction;

        public PopupWindowShowAction PopupAction { get { return _popupAction; } }

        public DisplayPopupViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            _popupAction = new PopupWindowShowAction(this.components);
            _popupAction.TargetViewId = "WorkType_ListView";
            _popupAction.CustomizePopupWindowParams += _popupAction_CustomizePopupWindowParams;
            RegisterActions(_popupAction);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        void _popupAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
