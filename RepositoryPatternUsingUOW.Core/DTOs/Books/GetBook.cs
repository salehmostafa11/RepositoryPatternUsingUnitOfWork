using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternUsingUOW.Core.DTOs.Books
{
    public class GetBook : BookBase
    {
        [Display(Name ="Author Name")]
        public string AuthorName { get; set; } = string.Empty ;
    }
}
