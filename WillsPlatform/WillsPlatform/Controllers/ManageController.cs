﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using WillsPlatform.Application.Constants;
using WillsPlatform.Application.DTOs;
using WillsPlatform.Application.Services;
using WillsPlatform.Web.Models;
using WillsPlatform.Web.Models.Forms;
using WillsPlatform.Web.Models.Manage;

namespace WillsPlatform.Web.Controllers
{
    public class ManageController : BaseController
    {
        private readonly IQuestionService _questionService;
        private readonly IFormService _formService;
        private readonly IFieldService _fieldService;
        private readonly ITemplateService _templateService;
        private readonly ITokenService _tokenService;

        public ManageController(IQuestionService questionService, 
                                IFormService formService, 
                                IFieldService fieldService, 
                                ITemplateService templateService,
                                ITokenService tokenService)
        {
            _questionService = questionService;
            _formService = formService;
            _fieldService = fieldService;
            _templateService = templateService;
            _tokenService = tokenService;
        }

        public async Task<IActionResult> Questionnaires()
        {
            var questions = await _questionService.GetQuestionsAsync();
            var breadcrumbs = InitializeBreadcrumbsList();
            breadcrumbs.Add(new(nameof(Questionnaires)));
            var model = new QuestionnaireViewModel()
            {
                Questionnaires = questions,
                Heading = nameof(Questionnaires),
                Breadcrumbs = breadcrumbs
            };

            return View(model);
        }

        public async Task<IActionResult> Forms()
        {
            var forms = await _formService.GetAllFormAsync();
            var breadcrumbs = InitializeBreadcrumbsList();
            breadcrumbs.Add(new(nameof(Forms)));
            var model = new FormsViewModel
            {
                Forms = forms,
                Breadcrumbs = breadcrumbs,
                Heading = nameof(Forms)
            };       
            return View(model);
        }

        public async Task<IActionResult> Fields()
        {
            var fields = await _fieldService.GetAllFieldAsync();
            var breadcrumbs = InitializeBreadcrumbsList();
            breadcrumbs.Add(new(nameof(Fields)));
            var model = new FieldsViewModel
            {
                Fields = fields,
                Breadcrumbs = breadcrumbs,
                Heading = nameof(Fields)
            };
            return View(model);
        }

        public async Task<IActionResult> Templates()
        {
            var templates = await _templateService.GetAllTEmplateAsync();
            var breadcrumbs = InitializeBreadcrumbsList();
            breadcrumbs.Add(new(nameof(Templates)));
            var model = new TemplatesViewModel
            {
                Templates = templates,
                Breadcrumbs = breadcrumbs,
                Heading = nameof(Templates)
            };
            return View(model);
        }

        public async Task<IActionResult> AddQuestion()
        {
            var questionViewModel = await InitilizeModelAsync(new AddQuestionViewModel() { });
            var breadcrumbs = InitializeBreadcrumbsList();
            breadcrumbs.Add(new(nameof(Questionnaires), GetCurrentControllerName()));
            breadcrumbs.Add(new(PagesHeadingNames.Add));
            questionViewModel.Heading = PagesHeadingNames.Add;
            questionViewModel.Breadcrumbs = breadcrumbs;
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
                FormId = model.FormId??0,
                FieldId = model.FieldId ?? 0
            };
            var isAdded = await _questionService.AddQuestionAsync(questionPostDTO);
            if (!isAdded) 
                return View(model);

            TempData["success"] = $"Questions added successfully";

            return RedirectToAction(nameof(Questionnaires));
        }

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

            var breadcrumbs = InitializeBreadcrumbsList();
            breadcrumbs.Add(new(nameof(Questionnaires), GetCurrentControllerName()));
            breadcrumbs.Add(new(PagesHeadingNames.Edit));
            questionViewModel.Heading = PagesHeadingNames.Edit;
            questionViewModel.Breadcrumbs = breadcrumbs;
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

        public IActionResult AddForm()
        {
            var breadcrumbs = InitializeBreadcrumbsList();
            breadcrumbs.Add(new(nameof(Forms), GetCurrentControllerName()));
            breadcrumbs.Add(new(PagesHeadingNames.Add));
            var model = new AddFormViewModel();
            model.Heading = PagesHeadingNames.Add;
            model.Breadcrumbs = breadcrumbs;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddForm(AddFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = $"Enter valid data";
                return View(new AddFormViewModel());
            }

            var formPostDTO = new FormDTO()
            {
                Name = model.Name
            };
            var isAdded = await _formService.AddFormAsync(formPostDTO);
            if (!isAdded)
            {
                TempData["error"] = $"Enter valid data";
                return View(model);
            }

            TempData["success"] = $"Form added success";
            return RedirectToAction(nameof(Forms));
        }

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

            var breadcrumbs = InitializeBreadcrumbsList();
            breadcrumbs.Add(new(nameof(Forms), GetCurrentControllerName()));
            breadcrumbs.Add(new(PagesHeadingNames.Edit));
            formViewModel.Heading = PagesHeadingNames.Edit;
            formViewModel.Breadcrumbs = breadcrumbs;

            return View(formViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditForm(EditFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = $"Enter valid data";
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
                TempData["error"] = $"Enter valid data";
                return View(model);
            }
            TempData["success"] = $"Updated forms susscessfully";
            return RedirectToAction(nameof(Forms));
        }

        public IActionResult AddField()
        {
            var breadcrumbs = InitializeBreadcrumbsList();
            breadcrumbs.Add(new(nameof(Fields), GetCurrentControllerName()));
            breadcrumbs.Add(new(PagesHeadingNames.Add));
            var model = new AddFieldViewModel();
            model.Heading = PagesHeadingNames.Add;
            model.Breadcrumbs = breadcrumbs;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddField(AddFieldViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = $"Enter valid data";
                return View(new AddFieldViewModel());
            }

            var fieldPostDTO = new FieldDTO()
            {
                Name = model.Name
            };
            var isAdded = await _fieldService.AddFieldAsync(fieldPostDTO);
            if (!isAdded)
            {
                TempData["error"] = $"Enter valid data";
                return View(model);
            }

            TempData["success"] = $"Field added successfully";
            return RedirectToAction(nameof(Fields));
        }

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

            var breadcrumbs = InitializeBreadcrumbsList();
            breadcrumbs.Add(new(nameof(Fields), GetCurrentControllerName()));
            breadcrumbs.Add(new(PagesHeadingNames.Edit));
            fieldViewModel.Heading = PagesHeadingNames.Edit;
            fieldViewModel.Breadcrumbs = breadcrumbs;

            return View(fieldViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditField(EditFieldViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = $"Enter valid data";
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
                TempData["error"] = $"Enter valid data";
                return View(model);
            }

            TempData["success"] = $"Field updated successfully";
            return RedirectToAction(nameof(Fields));
        }

        public async Task<IActionResult> AddTemplates()
        {
            var forms = await _formService.GetAllFormAsync();
            var model = new AddTemplateViewModel();
            model.Forms = forms.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();

            var breadcrumbs = InitializeBreadcrumbsList();
            breadcrumbs.Add(new(nameof(Templates), GetCurrentControllerName()));
            breadcrumbs.Add(new(PagesHeadingNames.Add));
            model.Heading = PagesHeadingNames.Add;
            model.Breadcrumbs = breadcrumbs;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTemplates(AddTemplateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = $"Please enter valid data";
                var forms = await _formService.GetAllFormAsync();
                model.Forms = forms.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();
                return View(model);
            }

            var templatePostDTO = new TemplateDTO()
            {
                Text = model.Text,
                FormId = model.FormId ?? 0
            };
            var isAdded = await _templateService.AddTemplateAsync(templatePostDTO);
            if (!isAdded)
            {
                var forms = await _formService.GetAllFormAsync();
                model.Forms = forms.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();
                TempData["error"] = $"Please enter valid data";
                return View(model);
            }

            if (!string.IsNullOrEmpty(model.Text))
            {
                var tokens = await ExtractTokensAsync(templatePostDTO);
                if (tokens.Any())
                {
                    //var isAddedTokens = await _tokenService.AddTokenAsync(tokens);
                }
            }

            TempData["success"] = $"Template added successfully";

            return RedirectToAction(nameof(Templates));
        }

        public async Task<IActionResult> EditTemplates(int id)
        {
            var forms = await _formService.GetAllFormAsync();
            var templateDTO = await _templateService.GetTemplateByIdAsync(id);
            var model = new EditTemplateViewModel();
            model.Text = templateDTO.Text;
            model.FormId = templateDTO.FormId;
            model.Id = id;
            model.Forms = forms.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();

            var breadcrumbs = InitializeBreadcrumbsList();
            breadcrumbs.Add(new(nameof(Templates), GetCurrentControllerName()));
            breadcrumbs.Add(new(PagesHeadingNames.Edit));
            model.Heading = PagesHeadingNames.Edit;
            model.Breadcrumbs = breadcrumbs;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditTemplates(EditTemplateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = $"Please enter valid data";
                var forms = await _formService.GetAllFormAsync();
                model.Forms = forms.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();
                return View(model);
            }

            var templatePostDTO = new TemplateDTO()
            {
                Text = model.Text,
                FormId = model.FormId ?? 0,
                Id = model.Id
            };
            var isUpdated = await _templateService.UpdateTemplateAsync(templatePostDTO);
            if (!isUpdated)
            {
                TempData["error"] = $"Please enter valid data";
                return View(model);
            }

            TempData["success"] = $"Template updated successfully";

            return RedirectToAction(nameof(Templates));
        }

        public async Task<IActionResult> DeleteTemplates(int id)
        {
            var isDeleted = await _templateService.DeleteTemplateAsync(id);

            if (isDeleted)
            {
                TempData["success"] = $"Template deleted successfully";
                return RedirectToAction(nameof(Templates));
            }

            TempData["error"] = $"Template deleted error";
            return RedirectToAction(nameof(Templates));
        }

        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var isDeleted = await _questionService.DeleteQuestionAsync(id);

            if (isDeleted)
            {
                TempData["success"] = $"Question deleted successfully";
                return RedirectToAction(nameof(Questionnaires));
            }

            TempData["error"] = $"Question deleted error";
            return RedirectToAction(nameof(Questionnaires));
        }

        public async Task<IActionResult> DeleteField(int id)
        {
            var isDeleted = await _fieldService.DeleteFieldAsync(id);

            if (isDeleted)
            {
                TempData["success"] = $"Field deleted successfully";
                return RedirectToAction(nameof(Fields));
            }

            TempData["error"] = $"Field deleted error";
            return RedirectToAction(nameof(Fields));
        }

        public async Task<IActionResult> DeleteForm(int id)
        {
            var isDeleted = await _formService.DeleteFormAsync(id);

            if (isDeleted)
            {
                TempData["success"] = $"Form deleted successfully";
                return RedirectToAction(nameof(Forms));
            }

            TempData["error"] = $"Form deleted error";
            return RedirectToAction(nameof(Forms));
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

        private async Task<List<TokenDTO>> ExtractTokensAsync(TemplateDTO template)
        {
            var tokens = new List<TokenDTO>();
            var questions = await _questionService.GetQuestionsAsync();
            // Define the pattern for matching strings between {{ and }}
            string pattern = @"\{\{(.+?)\}\}";
            MatchCollection matches = Regex.Matches(template.Text, pattern);

            // Extract and add matched strings to the list
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1)
                {
                    string extractedString = match.Groups[1].Value;
                    var question = questions.Where(x => string.Equals(x.Text.Replace(" ", ""), extractedString.Replace(" ", ""), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if (question is not null)
                    {
                        tokens.Add(new TokenDTO() { Name = extractedString, Questionid = question.Id});
                    }
                }
            }

            return tokens;
        }

        #endregion
    }
}
