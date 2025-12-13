using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternUsingUOW.Core.DTOs
{
    public class GeneralResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public T Data { get; set; }
    }
}
