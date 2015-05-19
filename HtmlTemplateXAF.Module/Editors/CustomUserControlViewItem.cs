using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.Editors
{
    public interface IModelCustomUserControlViewItem : IModelViewItem { }

    [ViewItem(typeof(IModelCustomUserControlViewItem))]
    public abstract class CustomUserControlViewItem : ViewItem, IComplexViewItem
    {
        public CustomUserControlViewItem(IModelViewItem model, Type objectType)
            : base(objectType, model != null ? model.Id : string.Empty)
        {
        }

        private IObjectSpace _objectSpace;
        private XafApplication _application;

        public IObjectSpace ObjectSpace
        {
            get { return _objectSpace; }
        }

        public XafApplication Application
        {
            get { return _application; }
        }

        public void Setup(IObjectSpace objectSpace, XafApplication application)
        {
            _objectSpace = objectSpace;
            _application = application;
        }

        protected override void OnControlCreated()
        {
            base.OnControlCreated();
            //Initializing a control when it is created by XAF (as part of a ViewItem).
            if (Control as IXpoSessionAwareControl == null) return;
            //new XpoSessionAwareControlInitializer(Control as IXpoSessionAwareControl, _objectSpace);
            var xpObjectSpace = ((DevExpress.ExpressApp.Xpo.XPObjectSpace)_objectSpace);
            // Session is required to query data when a custom control is XPO-aware only.
            var control = (IXpoSessionAwareControl)Control;
            control.UpdateDataSource(xpObjectSpace.Session);
            //It is required to update the session when ObjectSpace is reloaded.
            _objectSpace.Reloaded += ObjectSpaceOnReloaded;
        }

        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            base.BreakLinksToControl(unwireEventsOnly);
            _objectSpace.Reloaded -= ObjectSpaceOnReloaded;
        }

        private void ObjectSpaceOnReloaded(object sender, EventArgs eventArgs)
        {
            var control = (IXpoSessionAwareControl)Control;
            var xpObjectSpace = ((DevExpress.ExpressApp.Xpo.XPObjectSpace)_objectSpace);
            control.UpdateDataSource(xpObjectSpace.Session);
        }
    }

    public interface IXpoSessionAwareControl
    {
        void UpdateDataSource(Session session);
        Session Session { get; set; }
    }

    //public class XpoSessionAwareControlInitializer
    //{
    //    public XpoSessionAwareControlInitializer(IXpoSessionAwareControl control, IObjectSpace objectSpace)
    //    {
    //        //The IXpoSessionAwareControl interface is needed to pass a Session into a custom control that is supposed to implement this interface.

    //        Guard.ArgumentNotNull(control, "control");
    //        Guard.ArgumentNotNull(objectSpace, "objectSpace");
    //        //If a custom control is XAF-aware, then use the ObjectSpace to query data.
    //        // You can pass an XafApplication into your custom control in a similar manner, if necessary.
    //        var xpObjectSpace = ((DevExpress.ExpressApp.Xpo.XPObjectSpace)objectSpace);
    //        // Session is required to query data when a custom control is XPO-aware only.
    //        control.UpdateDataSource(xpObjectSpace.Session);
    //        //It is required to update the session when ObjectSpace is reloaded.
    //        objectSpace.Reloaded += delegate { control.UpdateDataSource(xpObjectSpace.Session); };
    //    }

    //    public XpoSessionAwareControlInitializer(IXpoSessionAwareControl sessionAwareControl, XafApplication theApplication)
    //        : this(sessionAwareControl, theApplication != null ? theApplication.CreateObjectSpace() : null)
    //    {
    //    }
    //}
}
