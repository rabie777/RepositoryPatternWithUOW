using RepositoryPatternWithUOW.BL.Interfaces;
using RepositoryPatternWithUOW.core.Database;
using RepositoryPatternWithUOW.core.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.BL.Repository
{
    public class BooksRepositorySpecial : BaseRepository<Book>, IBooksRepositorySpecial
    {
        public BooksRepositorySpecial(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<Book> GetBooksSpecialMethod()
        {
            throw new NotImplementedException();
        }
    }
}
