using DataAccess.Data;
using Entities.Articles;
using InfrastructureSql.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureSql.Concrete
{
    public class ArticleRepository : IRepository<Article>
    {
        private ApplicationDbContext _applicationDbContext;
        public ArticleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public async Task<Article> Add(Article entity)
        {
            _applicationDbContext.Articles.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }
        
        public async Task Delete(Article entity)
        {
            var article = _applicationDbContext.Articles.FirstOrDefault(x => x.Id == entity.Id);
            _applicationDbContext.Articles.Remove(article);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<Article>> GetAll()
        {
            return await _applicationDbContext.Articles.ToListAsync();
        }

        public async Task<Article> GetById(long id)
        {
            return await _applicationDbContext.Articles.FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<Article> Update(Article entity)
        {
            _applicationDbContext.Articles.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }
    }
}
