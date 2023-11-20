using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillsPlatform.Application.DTOs
{
    public class TemplateDTO
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public IEnumerable<TokenDTO>? Tokens { get; set; }
    }
}
