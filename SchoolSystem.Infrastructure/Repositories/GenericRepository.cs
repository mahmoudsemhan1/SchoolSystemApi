using Microsoft.EntityFrameworkCore;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SchoolSystemDbContext _context;
        //this line because we don't know what is the table we will deel with it 
        private readonly DbSet<T> _dbSet;
        public GenericRepository(SchoolSystemDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
             await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
              _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

      
    }
}
