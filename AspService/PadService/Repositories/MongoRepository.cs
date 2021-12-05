using MongoDB.Bson;
using MongoDB.Driver;
using PadService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace PadService.Repositories
{
    public class MongoRepository
    {
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<UserDTO> _collection;

        public MongoRepository(IConfiguration config)
        {
            _db = new MongoClient(config.GetConnectionString("MongoDb")).GetDatabase("PortalHR");

            _collection = _db.GetCollection<UserDTO>("UserDTO");
        }

        public void Delete(Guid id)
        {
            _collection.DeleteOne(doc => doc.Id == id);
        }

        public List<UserDTO> GetAll()
        {
            return _collection.Find(new BsonDocument()).ToList();
        }

        public UserDTO GetById(Guid id)
        {
            var record = _collection.Find(doc => doc.Id == id).FirstOrDefault();
            return record;
        }

        public UserDTO Insert(UserDTO record)
        {
           _collection.InsertOne(record);
            return record;
        }

        public UserDTO Update(UserDTO record)
        {
            _collection.ReplaceOne(doc => doc.Id == record.Id, record);
            return record;
        }
    }
}
