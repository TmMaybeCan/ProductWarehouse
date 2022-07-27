using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using System;

namespace Warehouse.DbContext
{
    public class WarehouseContext
    {
        private readonly string project;
        public readonly FirestoreDb firestoreDb;
        public WarehouseContext(IConfiguration configuration)
        {
            string filePath = configuration.GetValue<string>("FirestoreAPIKey:Path");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filePath);
            project = "warehouse";
            firestoreDb = FirestoreDb.Create(project);
        }
    }
}
