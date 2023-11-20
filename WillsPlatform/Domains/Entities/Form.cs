using WillsPlatform.Domains.Entities;

namespace Domains.Entities
{
    public class Form
    {
        public Form () {

            Questions = new List<Question>();
            Templates = new List<Template>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Question> Questions { get; set; }
        public ICollection<Template> Templates { get; set; }
    }
}
