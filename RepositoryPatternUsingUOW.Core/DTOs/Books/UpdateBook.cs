using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternUsingUOW.Core.DTOs.Books
{
    public class UpdateBook : CreateBook
    {
        [Required]
        public int Id { get; set; }
    }
}
