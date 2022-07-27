using Google.Cloud.Firestore;
using System;

namespace Warehouse.Entities
{
    [FirestoreData]
    public class Product
    {
        public string Id { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public Manufacturer Manufacturer { get; set; }

        //[FirestoreProperty]
        public DateTime RegistrationDate { get; set; }

        [FirestoreProperty]
        public string Category { get; set; }

        [FirestoreProperty]
        public double Price { get; set; }

        [FirestoreProperty]
        public int Count { get; set; }
    }
}
