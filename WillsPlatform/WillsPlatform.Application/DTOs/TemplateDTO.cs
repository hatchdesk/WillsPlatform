namespace WillsPlatform.Application.DTOs
{
    public class TemplateDTO
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public IEnumerable<TokenDTO>? Tokens { get; set; }
    }
}
