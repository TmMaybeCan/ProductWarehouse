using Google.Cloud.Firestore;

namespace Warehouse.Entities
{
    [FirestoreData]   
    public class Manufacturer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
    }
}
