using Domains.Entities;

namespace WillsPlatform.Domains.Entities
{
    public class Token
    {
        public Token() {

            Question = new Question();
            Template = new Template();
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        public int TemplateId { get; set; }
        public virtual Question Question { get; set; }
        public virtual Template Template { get; set; }    
    }
}
