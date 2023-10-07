using RepositoryPatternWithUOW.BL.Interfaces;
using RepositoryPatternWithUOW.BL.Repository;
using RepositoryPatternWithUOW.core.Database;
using RepositoryPatternWithUOW.core.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.BL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationContext _context;
        public IBaseRepository<Author> Authors { get; private set; } 
        public IBaseRepository<Book> Books { get; private set; }

        public IBooksRepositorySpecial booksRepositorySpecial { get; private set; }

        public UnitOfWork(ApplicationContext context)
        {
                _context = context;
            Authors = new BaseRepository<Author>(_context);
            Books = new BaseRepository<Book>(_context);
            booksRepositorySpecial=new BooksRepositorySpecial(_context);
        }
        
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
