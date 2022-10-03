using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureSql.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetById(long id);
        Task<List<T>> GetAll();
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
    }
}
