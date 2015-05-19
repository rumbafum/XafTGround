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
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Web.Templates.ActionContainers;
using DevExpress.ExpressApp.Web;
using DevExpress.Web;
using DevExpress.ExpressApp.ViewVariantsModule;
using HtmlTemplateXAF.Module.BusinessObjects;

namespace HtmlTemplateXAF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppWindowControllertopic.
    public partial class MainWindowController : WindowController, IXafCallbackHandler
    {
        ChangeVariantController _controller;

        public MainWindowController()
        {
            InitializeComponent();
            TargetWindowType = WindowType.Main;
            // Target required Windows (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            Frame.ProcessActionContainer += Window_ProcessActionContainer;
            Frame.ViewChanged += Frame_ViewChanged;
        }

        private void Window_ProcessActionContainer(object sender, ProcessActionContainerEventArgs e)
        {
            //if (e.ActionContainer is NavigationTabsActionContainer)
            //{
            //    NavigationTabsActionContainer navActionContainer = e.ActionContainer as NavigationTabsActionContainer;
            //    navActionContainer.PageControl.EnableClientSideAPI = true;
            //    navActionContainer.PageControl.AutoPostBack = false;
            //    navActionContainer.PageControl.EnableCallBacks = true;
            //    navActionContainer.PageControl.ClientSideEvents.TabClick = "function(s,e){return Skill.Global.onTabClick(s,e);}";
            //    XafCallbackManager callbackManager = ((ICallbackManagerHolder)WebWindow.CurrentRequestPage).CallbackManager;
            //    callbackManager.RegisterHandler("NavBarViewChangingController", this);
            //    navActionContainer.PageControl.ClientSideEvents.ActiveTabChanged = string.Format("function(s, e){{{0}}}", 
            //        callbackManager.GetScript("NavBarViewChangingController", "e.tab.index"));
            //    Frame.ProcessActionContainer -= Window_ProcessActionContainer;
            //}
        }
        void Frame_ViewChanged(object sender, ViewChangedEventArgs e)
        {
            if ((e.SourceFrame != null) && (e.SourceFrame.View != null))
                e.SourceFrame.View.ControlsCreated += View_ControlsCreated;
            _controller = Frame.GetController<ChangeVariantController>();
        }

        void View_ControlsCreated(object sender, EventArgs e)
        {
            RegisterXafCallbackHandler();
        }

        protected override void OnDeactivated()
        {
            Frame.ProcessActionContainer -= Window_ProcessActionContainer;
            Frame.ViewChanged -= Frame_ViewChanged;
            base.OnDeactivated();
        }
        protected override void OnViewClosing(object sender, EventArgs e)
        {
            base.OnViewClosing(sender, e);
        }
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
        }

        #region IXafCallbackHandler Members

        private void RegisterXafCallbackHandler()
        {
            if (XafCallbackManager != null)
            {
                XafCallbackManager.RegisterHandler("SkillsMainCallback", this);
            }
        }

        protected XafCallbackManager XafCallbackManager
        {
            get
            {
                return WebWindow.CurrentRequestPage != null ? ((ICallbackManagerHolder)WebWindow.CurrentRequestPage).CallbackManager : null;
            }
        }

        //public void ProcessAction(string parameter)
        //{
        //    //if ((Frame.View != null) && string.IsNullOrEmpty(parameter))
        //    //{
        //    //    Frame.View.ObjectSpace.Refresh();
        //    //}
        //}

        public void ProcessAction(string parameter)
        {
            SkillsGlobalSettings.Instance.ClientOffset = Convert.ToInt16(parameter);

            //int index = Int32.Parse(parameter);
            //SingleChoiceAction showNavigationItemAction = Frame.GetController<ShowNavigationItemController>().ShowNavigationItemAction;
            //showNavigationItemAction.DoExecute(showNavigationItemAction.Items[0].Items[index]);
        }

        #endregion

    }
}
