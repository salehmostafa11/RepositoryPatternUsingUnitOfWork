using RepositoryPatternUsingUOW.Core.Interfaces;
using RepositoryPatternUsingUOW.Core.Models;
using RepositoryPatternUsingUOW.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternUsingUOW.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<Author> Authors {  get; private set; }
        public IBooksRepository Books {  get; private set; }
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            Authors = new BaseRepository<Author>(_context);
            Books = new BooksRepository(_context);
        }

        public int Complete() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
        // in the memory => carbage collector is responsible for delete objects that were in the memory when finish it use 
        // but there are somethings no in the memory like the context we need to finish it => we use dispose
    }
}
