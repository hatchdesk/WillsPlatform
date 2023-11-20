namespace WillsPlatform.Web.Models.Forms
{
    public class FormViewModel
    {
       public int FormId { get; set; }
       public IEnumerable<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();
    }

    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int FieldId { get; set; }
    }
}

