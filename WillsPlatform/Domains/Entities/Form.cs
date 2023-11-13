namespace Domains.Entities
{
    public class Form
    {
        public Form() {

            Questions = new List<Question>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Question> Questions { get; set; }
    }
}
