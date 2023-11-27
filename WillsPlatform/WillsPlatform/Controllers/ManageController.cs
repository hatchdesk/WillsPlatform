using Microsoft.AspNetCore.Mvc;
using WillsPlatform.Application.Services;
using WillsPlatform.Web.Models.Manage;

namespace WillsPlatform.Web.Controllers
{
    public class ManageController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IFormService _formService;
        private readonly IFieldService _fieldService;
        private readonly ITemplateService _templateService;

        public ManageController(IQuestionService questionService, IFormService formService, IFieldService fieldService, ITemplateService templateService)
        {
            _questionService = questionService;
            _formService = formService;
            _fieldService = fieldService;
            _templateService = templateService;
        }

        public async Task<IActionResult> Questionnaires()
        {
            var questions = await _questionService.GetQuestionsAsync();
            var model = new QuestionnaireViewModel()
            {
                Questionnaires = questions
            };
            return View(model);
        }

        public async Task<IActionResult> Forms()
        {
            var forms = await _formService.GetAllFormAsync();
            var model = new FormsViewModel
            {
                Forms = forms
            };       
            return View(model);
        }

        public async Task<IActionResult> Fields()
        {
            var fields = await _fieldService.GetAllFieldAsync();
            var model = new FieldsViewModel
            {
                Fields = fields
            };
            return View(model);
        }

        public async Task<IActionResult> Templates()
        {
            var templates = await _templateService.GetAllTEmplateAsync();
            var model = new TemplatesViewModel
            {
                Templates = templates
            };
            return View(model);
        }
    }
}
