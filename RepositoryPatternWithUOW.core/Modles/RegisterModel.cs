using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.core.Modles
{
    public class RegisterModel
    {
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)] 
        public string LastName { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}
