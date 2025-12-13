using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternUsingUOW.Core.DTOs.Books
{
    public class BookBase
    {
        [Required,MaxLength(200), Display(Name = "Book Name")]
        public string Name { get; set; } = string.Empty;
    }
}
