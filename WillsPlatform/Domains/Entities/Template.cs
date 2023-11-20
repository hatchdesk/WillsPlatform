using Domains.Entities;

namespace WillsPlatform.Domains.Entities
{
    public class Template
    {
        public Template() { 
        
            Form = new Form();
            Tokens = new List<Token>();
        }
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int FormId { get; set; }
        public virtual Form Form { get; set; }

        public virtual ICollection<Token> Tokens { get; set; }
    }
}
