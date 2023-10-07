using RepositoryPatternWithUOW.BL.Interfaces;
using RepositoryPatternWithUOW.core.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.BL.UnitOfWork
{
    public interface IUnitOfWork :IDisposable
    {
        IBaseRepository<Author> Authors { get; }
        IBaseRepository<Book> Books { get; } 
        IBooksRepositorySpecial  booksRepositorySpecial { get; }
        int Complete();
    }
}
