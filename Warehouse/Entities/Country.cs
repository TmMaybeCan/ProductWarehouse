using Google.Cloud.Firestore;

namespace Warehouse.Entities
{
    [FirestoreData]
    public class Country
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
}
