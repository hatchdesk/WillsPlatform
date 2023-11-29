using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WillsPlatform.Application.DTOs;
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

        [HttpGet]
        public async Task<IActionResult> AddQuestion()
        {
            var questionViewModel = await InitilizeModelAsync(new AddQuestionViewModel() { });
            return View(questionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(AddQuestionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await InitilizeModelAsync(model);
                return View(model);
            }

            var questionPostDTO = new QuestionDTO()
            {
                Text = model.Text,
                FormId = model.FormId,
                FieldId = model.FieldId
            };
            var isAdded = await _questionService.AddQuestionAsync(questionPostDTO);
            if (!isAdded) 
                return View(model);

            return RedirectToAction(nameof(Questionnaires));
        }

        [HttpGet]
        public async Task<IActionResult> EditQuestion(int id)
        {
            var question = await _questionService.GetQuestionsByIdAsync(id);
            if(question == null)
            {
                return RedirectToAction(nameof(Questionnaires));
            }

            IEnumerable<FormDTO> formDTOs = await _formService.GetAllFormAsync();
            IEnumerable<FieldDTO> fieldDTOs = await _fieldService.GetAllFieldAsync();
            var questionViewModel = new EditQuestionViewModel
            {
                FormType = formDTOs.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList(),
                FieldType = fieldDTOs.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList(),
                Text = question.Text,
                FormId = question.FormId,
                FieldId = question.FieldId,
                Id = question.Id
            };

            return View(questionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditQuestion(EditQuestionViewModel model)
        {
            IEnumerable<FormDTO> formDTOs = await _formService.GetAllFormAsync();
            IEnumerable<FieldDTO> fieldDTOs = await _fieldService.GetAllFieldAsync();
            model.FieldType = fieldDTOs.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();
            model.FormType = formDTOs.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var questionPostDTO = new QuestionDTO()
            {
                Text = model.Text,
                FormId = model.FormId,
                FieldId = model.FieldId
            };

            var isUpdate = await _questionService.UpdateQuestionAsync(questionPostDTO);
            if (!isUpdate)
            {
                return View(model);
            }

            return RedirectToAction(nameof(Questionnaires));
        }


        #region -- Private Helper Methods --
        private async Task<AddQuestionViewModel> InitilizeModelAsync(AddQuestionViewModel model)
        {
            var forms = await _formService.GetAllFormAsync();
            var fields = await _fieldService.GetAllFieldAsync();

            model.Forms = forms.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();
            model.Fields = fields.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();

            return model;
        }
        #endregion
    }
}
