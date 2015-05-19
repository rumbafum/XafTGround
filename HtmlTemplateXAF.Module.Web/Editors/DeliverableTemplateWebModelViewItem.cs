using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web;
using HtmlTemplateXAF.Module.BusinessObjects;
using HtmlTemplateXAF.Module.Editors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTemplateXAF.Module.Web.Editors
{
    public interface IDeliverableTemplateWebModelViewItem : IDeliverableTemplateModelViewItem
    {
        [Category("Data")]
        string UserControlPath { get; set; }
    }
    [ViewItem(typeof(IDeliverableTemplateWebModelViewItem))]
    public class DeliverableTemplateWebModelViewItem : DeliverableTemplateModelViewItem
    {
        protected IDeliverableTemplateWebModelViewItem _model;
        private IDeliverableHtmlEditor _editor;

        public DeliverableTemplateWebModelViewItem(IModelViewItem model, Type objectType)
            : base(model, objectType)
        {
            _model = model as IDeliverableTemplateWebModelViewItem;
            if (_model == null)
                throw new ArgumentNullException("IDeliverableTemplateWebModelViewItem must extend IDeliverableTemplateModelViewItem in the ExtendModelInterfaces method of your Web ModuleBase descendant.");
            CreateControl();
        }

        protected override void OnCurrentObjectChanged()
        {
            base.OnCurrentObjectChanged();
            if (_editor.Deliverable != null)
                return;
            _editor.Deliverable = CurrentObject as Deliverable;
            _editor.IsEditMode = true;
            if (ObjectSpace != null)
            {
                ObjectSpace.Committing += ObjectSpace_Committing;
                ObjectSpace.ObjectSaved += ObjectSpace_ObjectSaved;
                ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
            }

            _editor.RefreshControl();
        }

        void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            if (_editor == null) return;
            if (e.PropertyName != "Template") return;
            _editor.RefreshControl();
        }

        void ObjectSpace_ObjectSaved(object sender, ObjectManipulatingEventArgs e)
        {
            var editor = Control as IDeliverableHtmlEditor;
            if (editor == null) return;
            editor.OnDeliverableSaved();
        }

        void ObjectSpace_Committing(object sender, CancelEventArgs e)
        {
            var editor = Control as IDeliverableHtmlEditor;
            if (editor == null) return;
            editor.OnDeliverableSaving();
        }

        protected override object CreateControlCore()
        {
            _editor = (IDeliverableHtmlEditor)WebWindow.CurrentRequestPage.LoadControl(@"Controls\DeliverableHtmlWebUserControl.ascx");
            _editor.Deliverable = CurrentObject as Deliverable;
            _editor.IsEditMode = true;
            if (ObjectSpace != null)
            {
                ObjectSpace.Committing += ObjectSpace_Committing;
                ObjectSpace.ObjectSaved += ObjectSpace_ObjectSaved;
            }
            return _editor;
        }
    }
}
