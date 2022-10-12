using DataAccess.Data;
using Entities.Orders;
using InfrastructureSql.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureSql.Concrete
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Order> Add(Order entity)
        {
            await _context.Orders.AddAsync(entity);
            return entity;
        }
        
        public async Task Delete(Order entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }
        
        public async Task<Order> GetById(long id)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Order> Update(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
