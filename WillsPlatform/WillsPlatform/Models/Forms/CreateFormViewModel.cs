using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Web.Models.Forms
{
    public class FormViewModel
    {
       public int FormId { get; set; }
       public IEnumerable<QuestionDTO> Questions { get; set; } = new List<QuestionDTO>();
    }
}

