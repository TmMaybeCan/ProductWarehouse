using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.DbContext;
using Warehouse.Entities;

namespace Warehouse.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly WarehouseContext _warehouseContext;
    
        public WarehouseService(WarehouseContext warehouseContext, IConfiguration configuration)
        {
            _warehouseContext = warehouseContext ?? throw new ArgumentNullException(nameof(warehouseContext));
            
        }

        public async Task<List<Product>> GetAllProducts()
        {
            Query productQuery = _warehouseContext.firestoreDb.Collection("products");
            QuerySnapshot productSnapshot = await productQuery.GetSnapshotAsync();

            List<Product> products = new List<Product>();

            foreach (DocumentSnapshot documentSnapshot in productSnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                { 
                    Dictionary<string, object> product = documentSnapshot.ToDictionary();

                    string json = JsonConvert.SerializeObject(product);
                    Product prod = JsonConvert.DeserializeObject<Product>(json);

                    prod.Id = documentSnapshot.Id;
                    prod.RegistrationDate = documentSnapshot.CreateTime.Value.ToDateTime();

                    products.Add(prod);
                }
            }

            List<Product> sortedProductsList = products.OrderBy(e => e.RegistrationDate).ToList();

            return sortedProductsList;
        }

        public async Task<Product> GetProduct(string id)
        {
            DocumentReference documentReference = _warehouseContext.firestoreDb.Collection("products").Document(id);
            DocumentSnapshot documentSnapshot = await documentReference.GetSnapshotAsync();

            if (documentSnapshot.Exists)
            {
                Product product = documentSnapshot.ConvertTo<Product>();

                product.Id = documentSnapshot.Id;

                return product;
            }
            else
            {
                return new Product();
            }
        }

        public async Task<bool> AddProduct(Product product)
        {
            CollectionReference productCollectionReference = _warehouseContext.firestoreDb.Collection("products");
            var result =  await productCollectionReference.AddAsync(product);

            if (result is null)
                return false;
            return true;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            DocumentReference documentReference = _warehouseContext.firestoreDb.Collection("products").Document(product.Id);

            var result =  await documentReference.SetAsync(product, SetOptions.Overwrite);

            if (result is null)
                return false;
            return true;
;
        }
        public async Task<bool> DeleteProduct(string id)
        {
            DocumentReference documentReference = _warehouseContext.firestoreDb.Collection("products").Document(id);
            var result =  await documentReference.DeleteAsync();

            if (result is null)
                return false;
            return true;

        }


    }
}
