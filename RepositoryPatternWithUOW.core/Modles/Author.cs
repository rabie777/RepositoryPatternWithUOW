using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.core.Modles
{
    public class Author
    {
        public int Id { get; set; }
        [MaxLength(150)] 
        public string ?Name { get; set; }
         
    }
}
