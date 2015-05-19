using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using HtmlTemplateXAF.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HtmlTemplateXAF.Module.Web
{
    public static class ListViewStateHelper
    {
        public static void CreateCustomListView(ListViewState listViewState, XafApplication application)
        {
            CreateCustomListViewInternal(listViewState, application);
        }

        public static void UpdateNavigationItems(ShowNavigationItemController showNavigationItemController)
        {
            showNavigationItemController.RecreateNavigationItems();
        }

        private static void CreateCustomListViewInternal(ListViewState listViewState, XafApplication application)
        {
            IModelClass modelClass = application.Model.BOModel.AddNode<IModelClass>(string.Format("{0}_{1}__Class", listViewState.Name, listViewState.ListView));
            modelClass.SetValue<ITypeInfo>("TypeInfo", XafTypesInfo.Instance.FindTypeInfo(listViewState.ObjectType));
            IModelListView modelListView = application.Model.Views.AddNode<IModelListView>(string.Format("{0}_{1}__ListView", listViewState.Name, listViewState.ListView));
            modelClass.Caption = listViewState.Name;
            modelListView.ModelClass = modelClass;

            try
            {
                GridViewInfo gridInfo = null;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(GridViewInfo));
                using (XmlReader xr = XmlReader.Create(new StringReader(listViewState.Layout)))
                    gridInfo = (GridViewInfo)xmlSerializer.Deserialize(xr);
                if(gridInfo != null)
                {
                    foreach (var column in gridInfo.Columns)
                    {
                        DataColumnInfo dataColumn = column as DataColumnInfo;
                        if (dataColumn == null) continue;
                        if (dataColumn.VisibleIndex < 0 || !dataColumn.Visible) continue;
                        IModelColumn modelColumn = modelListView.Columns.AddNode<IModelColumn>(dataColumn.FieldName);
                        modelColumn.Caption = dataColumn.Caption;
                        modelColumn.PropertyName = dataColumn.FieldName;
                        modelColumn.GroupIndex = dataColumn.GroupIndex;
                        modelColumn.SortIndex = dataColumn.SortIndex;
                        modelColumn.SortOrder = dataColumn.SortOrder;
                    }
                    if(!string.IsNullOrEmpty(gridInfo.FilterExpression))
                        modelListView.Criteria = gridInfo.FilterExpression;
                }
            }
            catch(Exception exception){
            }

        }

        public static void RemoveNode(ListViewState listViewState, View view)
        {
            IModelList<IModelView> modelViews = view.Model.Application.Views;
            IModelView myViewNode = modelViews[string.Format("{0}_{1}__ListView", listViewState.Name, listViewState.ListView)];
            if (myViewNode != null)
                myViewNode.Remove();
        }
    }
}
