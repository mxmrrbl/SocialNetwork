using Microsoft.EntityFrameworkCore;
using SocialNetwork.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly ApplicationContext _dbContext;

        public GenericRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Read all
        public virtual async Task<List<Entity>> GetAllAsync()
        {
            return await _dbContext.Set<Entity>().ToListAsync();
        }


        //Read one
        public virtual async Task<Entity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Entity>().FindAsync(id);
        }

        //Add
        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await _dbContext.Set<Entity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        //Update
        public virtual async Task UpdateAsync(Entity entity, int id)
        {
            var entry = await _dbContext.Set<Entity>().FindAsync(id);
            _dbContext.Entry(entry).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }

        //Delete
        public virtual async Task DeleteAsync(Entity entity)
        {
            _dbContext.Set<Entity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
