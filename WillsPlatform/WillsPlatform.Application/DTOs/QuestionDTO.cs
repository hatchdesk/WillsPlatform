using Domains.Entities;

namespace WillsPlatform.Application.DTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int FormId { get; set; }
        public int FieldId { get; set; }
        public Form Form { get; set; }
        public Field Field { get; set; }
    }
}
