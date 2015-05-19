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
using System.ComponentModel;
using DevExpress.ExpressApp.Web.Templates.ActionContainers;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Web;

namespace HtmlTemplateXAF.Module.Web.Controllers.Skills
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class CallbackController : Controller
    {
        private readonly Dictionary<string, ActionBase> mapIdToAction = new Dictionary<string, ActionBase>();
        private string callbackClientInstanceName = string.Format("ProcessActionCallback_{0}", new Random().Next(1000, 100000));

        protected ASPxCallbackPanel ProcessActionCallbackPanel { get; private set; }

        public CallbackController()
        {
            InitializeComponent();
            RegisterActions((IContainer)components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            this.Frame.ViewChanged += Frame_ViewChanged;
        }

        void Frame_ViewChanged(object sender, EventArgs e)
        {
            this.NeedRefresh = true;
        }

        public bool NeedRefresh { get; private set; }

        protected override void OnDeactivated()
        {
            //this.createdActionItems.Clear();
            this.mapIdToAction.Clear();
            //this.mapControlToAction.Clear();
            this.RemoveProcessActionCallback();
            this.Frame.ViewChanged -= Frame_ViewChanged;
            base.OnDeactivated();
        }
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            var template = this.Frame.Template as ICallbackFrameTemplate;
            if (template != null)
            {
                this.CreateProcessActionCallbackPanel(template);
            }
            else
                this.Frame.TemplateChanged += Frame_TemplateChanged;
            //var controller = this.Frame.GetController<SkillsFillActionContainersController>();
            //if (controller != null)
            //{
            //    controller.CustomFillContainers += controller_CustomFillContainers;
            //    controller.ContainersFilled += controller_ContainersFilled;
            //}
        }

        private void controller_ContainersFilled(object sender, EventArgs e)
        {
            foreach (var container in Frame.Template.GetContainers())
            {
                foreach (var action in container.Actions)
                    RegisterToMapAction(action);

                if (container is NavigationTabsActionContainer)
                    UpdateNavigationTabsActionContainer(container as NavigationTabsActionContainer);
            }
        }

        private void UpdateNavigationTabsActionContainer(NavigationTabsActionContainer container)
        {
            if (container == null) return;
            var helper = (ASPxPageControlHelper)typeof(NavigationTabsActionContainer).GetField("helper",
                                                                                           BindingFlags.Instance |
                                                                                           BindingFlags.NonPublic).GetValue(container);
            var menus = (List<SingleChoiceActionMenuContainerControl>)
                                          typeof(ASPxPageControlHelper).GetField("menus",
                                                                                           BindingFlags.Instance |
                                                                                           BindingFlags.NonPublic).GetValue(helper);
            //var actionItemsToMenuItemsMap = (List<MenuChoiceActionItem>)typeof(SingleChoiceActionMenuContainerControl).
            //    GetField("menuActionItems", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(menus[0]);

            var menu = (ASPxMenu)typeof(SingleChoiceActionMenuContainerControl).
                GetField("menu", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(menus[0]);

            //var methodInfo = typeof(SingleChoiceActionMenuContainerControl).GetMethod("menu_ItemClick", BindingFlags.Instance | BindingFlags.NonPublic);
            //var handler = (EventHandler)Delegate.CreateDelegate(typeof(EventHandler), menus[0], methodInfo, false); 
            
            //var events = (EventHandlerList)typeof(ASPxMenu).GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(menu, null);

            //events.RemoveHandler(EventServerClick, handler);
            //menu.AutoPostBack = false;
            //XafCallbackManager callbackManager = ((ICallbackManagerHolder)WebWindow.CurrentRequestPage).CallbackManager;
            //callbackManager.RegisterHandler("NavBarViewChangingController", this);
            menu.ClientSideEvents.ItemClick = "function(s,e){return Skill.Global.onTabClick(s,e);}";

            //foreach (var item in actionItemsToMenuItemsMap)
            //{
            //    UpdateLinkChoiceActionItem(item);
            //}
        }

        void UpdateLinkChoiceActionItem(MenuChoiceActionItem item)
        {
            //item.
            //#region make callback hyperlink for item

            var action = (SingleChoiceAction)typeof(MenuChoiceActionItem).GetField("action",
                                                                                     BindingFlags.Instance |
                                                                                     BindingFlags.NonPublic).GetValue(item);
            var methodInfo = typeof(MenuChoiceActionItem).GetMethod("menuActionControl_ServerClick",
                                                                  BindingFlags.Instance |
                                                                  BindingFlags.NonPublic);
            //var handler = (EventHandler)Delegate.CreateDelegate(typeof(EventHandler), item, methodInfo);
            //var events = (EventHandlerList)typeof(Control).GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(item.Control, null);
            //var EventServerClick = typeof(HtmlAnchor).GetField("EventServerClick",
            //                                                                         BindingFlags.Static |
            //                                                                         BindingFlags.NonPublic).GetValue(null);
            //events.RemoveHandler(EventServerClick, handler);

            //#endregion

            //item.Control.HRef = string.Format("javascript:{0}",
            //                                  this.GetProcessActionCallbackInvokeCode(action,
            //                                                                          GetChoiceActionItemPath(action, item.ActionItem)));
        }

        static string GetChoiceActionItemPath(ChoiceActionBase action, ChoiceActionItem item)
        {
            var current = item;
            var path = new List<string>();
            while (current.ParentItem != null)
            {
                path.Add(current.ParentItem.Items.IndexOf(current).ToString());
                current = current.ParentItem;
            }
            path.Add(action.Items.IndexOf(current).ToString());
            return string.Join(",", path.Reverse<string>().ToArray());
        }


        private void controller_CustomFillContainers(object sender, CustomFillContainersEventArgs e)
        {
            foreach (var container in Frame.Template.GetContainers())
            {
                foreach (var action in container.Actions)
                    RegisterToMapAction(action);

                if (container is NavigationTabsActionContainer)
                    UpdateNavigationTabsActionContainer(container as NavigationTabsActionContainer);
            }
            //if (!(e.Template is ICallbackFrameTemplate)) return;
            //createdActionItems.Clear();
            //mapIdToAction.Clear();
            //mapControlToAction.Clear();
            //foreach (var container in e.Template.GetContainers())
            //{
            //    var wac = container as WebActionContainer;
            //    if (wac != null)
            //        wac.ActionItemCreated += wac_ActionItemCreated;
            //}
        }

        private void Frame_TemplateChanged(object sender, EventArgs e)
        {
            var template = this.Frame.Template as ICallbackFrameTemplate;
            if (template != null)
            {
                this.CreateProcessActionCallbackPanel(template);
            }
            //throw new NotImplementedException();
        }

        private string GetActionKey(ActionBase action)
        {
            return action.GetHashCode().ToString();
        }

        public void RegisterToMapAction(ActionBase action)
        {
            this.mapIdToAction[GetActionKey(action)] = action;
        }

        public string GetProcessActionCallbackInvokeCode(ActionBase action, bool comma, params string[] parameters)
        {
            return action == null
                    ? string.Empty
                    : string.Format("{0}.PerformCallback('{1}:{2}'){3}", callbackClientInstanceName,
                                    this.GetActionKey(action), string.Join(":", parameters), (comma ? ";" : string.Empty));
        }

        public string GetProcessActionCallbackInvokeCode(ActionBase action, params string[] parameters)
        {
            return GetProcessActionCallbackInvokeCode(action, true, parameters);
        }

        public string GetCallbackPanelRefreshCode(bool includeMainPanel)
        {
            var template = this.Frame.Template as ICallbackFrameTemplate;
            if (template == null) return string.Empty;

            var script = "if ({0}) {0}.PerformCallback('');";
            var s = new List<string>();
            if (includeMainPanel)
                s.Add(string.Format(script, callbackClientInstanceName));
            foreach (var panel in template.GetASPxCallbackPanels().Where(a => a != this.ProcessActionCallbackPanel && a.ClientVisible))
                s.Add(string.Format(script, panel.ClientID));
            var code = string.Join(string.Empty, s.ToArray());
            return code;
        }



        private string callbackResult;

        private void CreateProcessActionCallbackPanel(ICallbackFrameTemplate control)
        {
            this.RemoveProcessActionCallback();
            this.ProcessActionCallbackPanel = control.ProcessActionCallbackPanel;
            this.ProcessActionCallbackPanel.HideContentOnCallback = false;
            this.ProcessActionCallbackPanel.ID = callbackClientInstanceName;
            this.ProcessActionCallbackPanel.ClientInstanceName = callbackClientInstanceName;
            this.ProcessActionCallbackPanel.ClientSideEvents.EndCallback = "function (s,e) { if (s.cpResult) xafEvalFunc(s.cpResult); }";
            this.ProcessActionCallbackPanel.Callback += Callback_Callback;
            this.ProcessActionCallbackPanel.CustomJSProperties += Callback_CustomJSProperties;
        }

        void Callback_CustomJSProperties(object sender, CustomJSPropertiesEventArgs e)
        {
            e.Properties["cpResult"] = string.IsNullOrEmpty(this.callbackResult) ? string.Empty : this.callbackResult;
        }

        void Callback_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                this.callbackResult = string.Empty;
                if (string.IsNullOrEmpty(e.Parameter)) return;

                var ss = e.Parameter.Split(':');
                ActionBase action;
                if (this.mapIdToAction.TryGetValue(ss[0], out action))
                {
                    var simpleAction = action as SimpleAction;
                    if (simpleAction != null)
                        simpleAction.DoExecute();
                    var singleChoiceAction = action as SingleChoiceAction;
                    if (singleChoiceAction != null)
                    {
                        ChoiceActionItem currentItem = null;
                        if (string.IsNullOrEmpty(ss[1]))
                            currentItem = singleChoiceAction.Items.FirstActiveItem;
                        else
                        {
                            var currentItems = singleChoiceAction.Items;
                            foreach (var s in ss[1].Split(','))
                            {
                                int index;
                                if (int.TryParse(s, out index))
                                {
                                    currentItem = currentItems[index];
                                    currentItems = currentItem.Items;
                                }
                                else
                                {
                                    currentItem = null;
                                    break;
                                }
                            }
                        }
                        if (currentItem == null)
                            throw new InvalidOperationException("");
                        else
                            singleChoiceAction.DoExecute(currentItem);
                    }
                    var popupWindowShowAction = action as PopupWindowShowAction;
                    if (popupWindowShowAction != null)
                    {
                        (WebApplication.Instance.PopupWindowManager as SkillsPopupWindowManager).RegisterStartupPopupWindowShowActionScript
                            (sender as ASPxCallbackPanel, popupWindowShowAction);
                    }
                    var parametrizedAction = action as ParametrizedAction;
                    if (parametrizedAction != null)
                    {
                        if (parametrizedAction.ValueType == typeof(string))
                            parametrizedAction.DoExecute(ss[1]);
                        else if (parametrizedAction.ValueType == typeof(int))
                            parametrizedAction.DoExecute(int.Parse(ss[1]));
                        else if (parametrizedAction.ValueType == typeof(DateTime))
                            parametrizedAction.DoExecute(DateTime.Parse(ss[1]));
                    }
                }
                var template = this.Frame.Template as ICallbackFrameTemplate;
                if (template == null) return;

                var currentRequsetWindow = WebWindow.CurrentRequestWindow as ISkillsWebWindow;
                if (currentRequsetWindow != null)
                {
                    this.callbackResult = string.Join("\n", currentRequsetWindow.StartUpScripts.Values.ToArray());
                    currentRequsetWindow.StartUpScripts.Clear();
                }

                if (string.IsNullOrEmpty(this.callbackResult))
                    this.callbackResult = this.GetCallbackPanelRefreshCode(false);
            }
            finally
            {
                this.NeedRefresh = false;
            }
        }

        public string GetCallbackResult(string result)
        {
            var controller = WebWindow.CurrentRequestWindow.GetController<CallbackController>();
            if (controller != null && controller.NeedRefresh)
            {
                return controller.GetCallbackPanelRefreshCode(true);
            }
            var currentRequsetWindow = WebWindow.CurrentRequestWindow as ISkillsWebWindow;
            if (currentRequsetWindow != null)
            {
                result += string.Join("\n", currentRequsetWindow.StartUpScripts.Values.ToArray());
                currentRequsetWindow.StartUpScripts.Clear();
            }
            if (this.NeedRefresh || string.IsNullOrEmpty(result))
                result = this.GetCallbackPanelRefreshCode(true);
            return result;
        }

        private void RemoveProcessActionCallback()
        {
            if (this.ProcessActionCallbackPanel == null) return;
            this.ProcessActionCallbackPanel.Callback -= this.Callback_Callback;
            this.ProcessActionCallbackPanel = null;
        }

    }
}
