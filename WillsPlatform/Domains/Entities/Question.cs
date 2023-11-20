using WillsPlatform.Domains.Entities;

namespace Domains.Entities
{
    public class Question
    {
        public Question() { 

            Form = new Form();
            Field = new Field();
        }

        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int FormId { get; set; }
        public int FieldId { get; set; }

        public virtual Form Form { get; set; }
        public virtual Field Field { get; set; }
    }
}
