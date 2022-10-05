using Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureMongo.Interfaces
{
    public interface IMongoBaseRepository<T> : IDisposable where T : MongoBaseEntity
    {
      

        //Async
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        Task InsertOneAsync(T item);
        Task InsertManyAsync(IEnumerable<T> items);
        Task DeleteOneAsync(Expression<Func<T, bool>> expression);
        Task DeleteManyAsync(Expression<Func<T, bool>> expression);
    }
}
