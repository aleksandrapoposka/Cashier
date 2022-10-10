using DataAccess.Data;
using Entities;
using InfrastructureSql.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace InfrastructureSql.Concrete
{
    public class CountryRepository : IRepository<Country>
    {
        private ApplicationDbContext _applicationDbContext;
        public CountryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Country> Add(Country entity)
        {
            _applicationDbContext.Countries.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Country entity)
        {
            var Country = _applicationDbContext.Countries.FirstOrDefault(x => x.Id == entity.Id);
            _applicationDbContext.Countries.Remove(Country);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<Country>> GetAll()
        {
            return await _applicationDbContext.Countries.ToListAsync();
        }

        public async Task<Country> GetById(long id)
        {
            return await _applicationDbContext.Countries.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Country> Update(Country entity)
        {
            _applicationDbContext.Countries.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }
    }
}

