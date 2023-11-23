using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WillsPlatform.Application.Services;
using WillsPlatform.Web.Models.Manage;

namespace WillsPlatform.Web.Controllers
{
    public class ManageController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IQuestionService _questionService;
        public ManageController(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IActionResult> QuestionnairesAsync()
        {
            var result = await _questionService.GetQuestionsAsync();
            var questios = new QuestionnariesVM();
            questios.QuestionDTOs = result;
            return View(questios);
        }
    }
}
