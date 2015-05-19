using System;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Web;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System.Data.SqlClient;
using HtmlTemplateXAF.Module.Web.Controllers.Skills;
using DevExpress.ExpressApp.ViewVariantsModule;
using DevExpress.ExpressApp.Scheduler.Web;
//using DevExpress.ExpressApp.Security;

namespace HtmlTemplateXAF.Web
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/DevExpressExpressAppWebWebApplicationMembersTopicAll
    public partial class HtmlTemplateXAFAspNetApplication : SkillsWebApplication
    {
        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule module2;
        private HtmlTemplateXAF.Module.HtmlTemplateXAFModule module3;
        private DevExpress.ExpressApp.HtmlPropertyEditor.Web.HtmlPropertyEditorAspNetModule htmlPropertyEditorAspNetModule1;
        private DevExpress.ExpressApp.Validation.ValidationModule validationModule1;
        private ViewVariantsModule viewVariantsModule;
        private SchedulerAspNetModule schedulerAspNetModule;
        private HtmlTemplateXAF.Module.Web.HtmlTemplateXAFAspNetModule module4;

        public HtmlTemplateXAFAspNetApplication()
        {
            InitializeComponent();

            CustomConnectionProvider.Register();
        }
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
        {
            args.ObjectSpaceProvider = new XPObjectSpaceProvider(args.ConnectionString, args.Connection, true);
        }

        private void HtmlTemplateXAFAspNetApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e)
        {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            //if (System.Diagnostics.Debugger.IsAttached)
            //{
                e.Updater.Update();
                e.Handled = true;
            //}
            //else
            //{
            //    string message = "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application.\r\n" +
            //        "This error occurred  because the automatic database update was disabled when the application was started without debugging.\r\n" +
            //        "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " +
            //        "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " +
            //        "or manually create a database using the 'DBUpdater' tool.\r\n" +
            //        "Anyway, refer to the following help topics for more detailed information:\r\n" +
            //        "'Update Application and Database Versions' at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument2795.htm\r\n" +
            //        "'Database Security References' at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument3237.htm\r\n" +
            //        "If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/";

            //    if (e.CompatibilityError != null && e.CompatibilityError.Exception != null)
            //    {
            //        message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
            //    }
            //    throw new InvalidOperationException(message);
            //}
#endif
        }

        private void InitializeComponent()
        {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule();
            this.module3 = new HtmlTemplateXAF.Module.HtmlTemplateXAFModule();
            this.module4 = new HtmlTemplateXAF.Module.Web.HtmlTemplateXAFAspNetModule();
            this.htmlPropertyEditorAspNetModule1 = new DevExpress.ExpressApp.HtmlPropertyEditor.Web.HtmlPropertyEditorAspNetModule();
            this.validationModule1 = new DevExpress.ExpressApp.Validation.ValidationModule();
            this.viewVariantsModule = new ViewVariantsModule();
            this.schedulerAspNetModule = new SchedulerAspNetModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // validationModule1
            // 
            this.validationModule1.AllowValidationDetailsAccess = true;
            this.validationModule1.IgnoreWarningAndInformationRules = false;
            // 
            // HtmlTemplateXAFAspNetApplication
            // 
            this.ApplicationName = "HtmlTemplateXAF";
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.htmlPropertyEditorAspNetModule1);
            this.Modules.Add(this.module4);
            this.Modules.Add(this.validationModule1);
            this.Modules.Add(this.viewVariantsModule);
            this.Modules.Add(this.schedulerAspNetModule);
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.HtmlTemplateXAFAspNetApplication_DatabaseVersionMismatch);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
