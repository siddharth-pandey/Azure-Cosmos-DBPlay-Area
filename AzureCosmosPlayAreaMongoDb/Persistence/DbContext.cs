using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Authentication;
using MongoDB.Driver;

namespace AzureCosmosPlayAreaMongoDb.Persistence
{
    public class DbContext
    {
        private readonly string _username = ConfigurationManager.AppSettings.Get("CosmosDbAccountUsername");
        private readonly string _password = ConfigurationManager.AppSettings.Get("CosmosDbAccountPassword");
        private readonly string _host = ConfigurationManager.AppSettings.Get("CosmosDbAccountHost");
        private readonly int _port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("CosmosDbAccountPort"));
        private readonly string _databaseName = ConfigurationManager.AppSettings.Get("DatabaseName");

        public readonly string CollectionName = ConfigurationManager.AppSettings.Get("CollectionName");

        public IMongoDatabase Database { get; private set; }

        public DbContext()
        {
            MongoClientSettings settings = GetSettings();
            MongoIdentity identity = new MongoInternalIdentity(_databaseName, _username);
            MongoIdentityEvidence evidence = new PasswordEvidence(_password);

            settings.Credentials = new List<MongoCredential>()
            {
                new MongoCredential("SCRAM-SHA-1", identity, evidence)
            };

            var mongoClient = new MongoClient(settings);
            Database = mongoClient.GetDatabase(_databaseName);
            Database.CreateCollection(CollectionName);
        }

        private MongoClientSettings GetSettings()
        {
            var settings = new MongoClientSettings
            {
                Server = new MongoServerAddress(_host, _port),
                UseSsl = true,
                SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 }
            };

            return settings;
        }
    }
}