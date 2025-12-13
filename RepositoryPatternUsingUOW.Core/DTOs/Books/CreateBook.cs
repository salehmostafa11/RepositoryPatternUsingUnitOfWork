using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternUsingUOW.Core.DTOs.Books
{
    public class CreateBook : BookBase
    {
        [Required]
        public int AuthorId { get; set; }
    }
}
