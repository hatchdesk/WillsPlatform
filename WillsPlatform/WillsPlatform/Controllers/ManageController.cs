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
                TempData["error"] = $"Please enter valid data";
                await InitilizeModelAsync(model);
                return View(model);
            }

            var questionPostDTO = new QuestionDTO()
            {
                Text = model.Text,
                FormId = (int)model.FormId,
                FieldId = (int)model.FieldId
            };
            var isAdded = await _questionService.AddQuestionAsync(questionPostDTO);
            if (!isAdded) 
                return View(model);

            TempData["success"] = $"Questions added successfully";

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
                TempData["error"] = $"Please enter valid data";
                return View(model);
            }

            var questionPostDTO = new QuestionDTO()
            {
                Id = model.Id,
                Text = model.Text,
                FormId = (int)model.FormId,
                FieldId = (int)model.FieldId
            };

            var isUpdate = await _questionService.UpdateQuestionAsync(questionPostDTO);
            if (!isUpdate)
            {
                return View(model);
            }

            TempData["success"] = $"Questions update successfully";
            return RedirectToAction(nameof(Questionnaires));
        }

        [HttpGet]
        public IActionResult AddForm()
        {
            return View(new AddFormViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddForm(AddFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(new AddFormViewModel());
            }

            var formPostDTO = new FormDTO()
            {
                Name = model.Name
            };
            var isAdded = await _formService.AddFormAsync(formPostDTO);
            if (!isAdded)
                return View(model);

            return RedirectToAction(nameof(Forms));
        }

        [HttpGet]
        public async Task<IActionResult> EditForm(int id)
        {
            var form = await _formService.GetFormByIdAsync(id);
            if (form == null)
            {
                return RedirectToAction(nameof(Forms));
            }
            var formViewModel = new EditFormViewModel
            {
                Name = form.Name,
                Id = form.Id
            };

            return View(formViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditForm(EditFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var formPostDTO = new FormDTO()
            {
                Id = model.Id,
                Name = model.Name
            };

            var isUpdate = await _formService.UpdateFormAsync(formPostDTO);
            if (!isUpdate)
            {
                return View(model);
            }

            return RedirectToAction(nameof(Forms));
        }

        [HttpGet]
        public IActionResult AddField()
        {
            return View(new AddFieldViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddField(AddFieldViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(new AddFieldViewModel());
            }

            var fieldPostDTO = new FieldDTO()
            {
                Name = model.Name
            };
            var isAdded = await _fieldService.AddFieldAsync(fieldPostDTO);
            if (!isAdded)
                return View(model);

            return RedirectToAction(nameof(Fields));
        }

        [HttpGet]
        public async Task<IActionResult> EditField(int id)
        {
            var field = await _fieldService.GetFieldByIdAsync(id);
            if (field == null)
            {
                return RedirectToAction(nameof(Fields));
            }
            var fieldViewModel = new EditFieldViewModel
            {
                Name = field.Name,
                Id = field.Id
            };

            return View(fieldViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditField(EditFieldViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var fieldPostDTO = new FieldDTO()
            {
                Id = model.Id,
                Name = model.Name
            };

            var isUpdate = await _fieldService.UpdateFieldAsync(fieldPostDTO);
            if (!isUpdate)
            {
                return View(model);
            }

            return RedirectToAction(nameof(Fields));
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
