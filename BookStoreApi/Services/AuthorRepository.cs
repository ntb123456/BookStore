using BookStoreApi.Contracts;
using BookStoreApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.Services
{
    public class AuthorRepository : IAuthorRepository

    {
        private readonly ApplicationDbContext _dbContext;
        public AuthorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public  async Task<bool> Create(Author entity)
        {
           await  _dbContext.AddAsync(entity);
              return await Save();
        }

        public async Task<bool> Delete(Author entity)
        {
            _dbContext.Remove(entity);
            return await Save();
        }

        public async Task<IList<Author>> FindAll()
        {
            var authors = await _dbContext.Authors.ToListAsync();
            return authors;
        }

        public async Task<Author> FindById(int id)
        {
            var author = await  _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            return author;
        }

        public async Task<bool> isExists(int id)
        {
            return await _dbContext.Authors.AnyAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _dbContext.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Author entity)
        {
            _dbContext.Update(entity);
            return await Save();
        }
    }
}
