using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HtmlTemplateXAF.Web
{
    public class CustomConnectionProvider : MSSqlConnectionProvider 
    {
        public CustomConnectionProvider(IDbConnection connection, AutoCreateOption autoCreateOption) : base(connection, autoCreateOption) { }

        public new const string XpoProviderTypeString = "SkillsMSSQL";

        public new static IDataStore CreateProviderFromString(string connectionString, AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect)
        {

            IDbConnection connection = new SqlConnection(connectionString);

            objectsToDisposeOnDisconnect = new IDisposable[] { connection };

            return CreateProviderFromConnection(connection, autoCreateOption);

        }

        public new static IDataStore CreateProviderFromConnection(IDbConnection connection, AutoCreateOption autoCreateOption)
        {
            return new CustomConnectionProvider(connection, autoCreateOption);
        }

        public new static void Register()
        {

            try
            {

                DataStoreBase.RegisterDataStoreProvider(XpoProviderTypeString, new DataStoreCreationFromStringDelegate(CreateProviderFromString));

            }
            catch (ArgumentException e)
            {

            }

        }

        protected override object ReformatReadValue(object value, ConnectionProviderSql.ReformatReadValueArgs args)
        {
            if (value != null)
            {
                Type valueType = value.GetType();
                if (valueType == typeof(DateTimeOffset))
                    return value.ToString();
            }
            return base.ReformatReadValue(value, args);
        }

    }
}