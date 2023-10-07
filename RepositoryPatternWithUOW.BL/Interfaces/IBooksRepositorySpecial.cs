using RepositoryPatternWithUOW.core.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.BL.Interfaces
{
    public interface IBooksRepositorySpecial :IBaseRepository<Book>
    { 
        IEnumerable<Book> GetBooksSpecialMethod();
    }
}
