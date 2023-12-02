using Domains.Entities;

namespace WillsPlatform.Application.DTOs
{
    public class TemplateDTO
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public IEnumerable<TokenDTO>? Tokens { get; set; }
        public int FormId { get; set; }
        public Form? Form { get; set; }
    }
}
