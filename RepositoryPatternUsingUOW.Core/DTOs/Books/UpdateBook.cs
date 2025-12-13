using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternUsingUOW.Core.DTOs.Books
{
    public class UpdateBook : BookBase
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}
