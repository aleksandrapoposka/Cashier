using Entities;
using InfrastructureMongo.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureMongo.Concrete
{
    public abstract class MongoBaseRepository<T> : IMongoBaseRepository<T> where T : MongoBaseEntity
    {

        private IMongoDatabase MongoDBContext => MongoDbContext.Database;

        public Task DeleteManyAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task InsertManyAsync(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public Task InsertOneAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}
