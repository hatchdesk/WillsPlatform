using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Web.Models.Manage
{
    public class QuestionnaireViewModel
    {
        public IEnumerable<QuestionDTO> Questionnaires { get; set; } = new List<QuestionDTO>();
    }
}
