using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Utils;
using DevExpress.Xpo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.Editors
{
    public interface IDeliverableTemplateModelViewItem : IModelViewItem { }

    [ViewItem(typeof(IDeliverableTemplateModelViewItem))]
    public abstract class DeliverableTemplateModelViewItem : ViewItem, IComplexViewItem
    {
        protected DeliverableTemplateModelViewItem(IModelViewItem model, Type objectType)
            : base(objectType, model != null ? model.Id : string.Empty)
        {
        }

        private IObjectSpace _theObjectSpace;
        private XafApplication _theApplication;

        public IObjectSpace ObjectSpace
        {
            get { return _theObjectSpace; }
        }

        public XafApplication Application
        {
            get { return _theApplication; }
        }

        public void Setup(IObjectSpace objectSpace, XafApplication application)
        {
            _theObjectSpace = objectSpace;
            _theApplication = application;
            new XpoSessionAwareControlInitializer(Control as IXpoSessionAwareControl, _theObjectSpace);
        }
    }

    public class XpoSessionAwareControlInitializer
    {
        public XpoSessionAwareControlInitializer(IXpoSessionAwareControl sessionAwareControl, IObjectSpace theObjectSpace)
        {
            //    //The IXpoSessionAwareControl interface is needed to pass a Session into a custom user control that is supposed to implement this interface.
            //    // Session is required to query data when a custom user control is XPO-aware only.
            //    if (sessionAwareControl != null)
            //        // If a custom user control is XAF-aware, then use the ObjectSpace to query data.
            //        // You can pass an XafApplication into your custom control in a similar manner, if necessary.

            //TODO . unable to cast to ObjectSpace
            //        sessionAwareControl.Session = ((ObjectSpace)theObjectSpace).Session;            
        }

        public XpoSessionAwareControlInitializer(IXpoSessionAwareControl sessionAwareControl, XafApplication theApplication)
            : this(sessionAwareControl, theApplication != null ? theApplication.CreateObjectSpace() : null)
        {
        }
    }
}
