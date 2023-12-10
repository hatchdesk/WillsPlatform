using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Web.Models.Manage
{
    public class EditQuestionViewModel : BaseViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Please enter the questions text")]
        [Required(ErrorMessage = "Please enter the questions.")]
        public string Text { get; set; }

        [Display(Name = "Select form:")]
        [Required(ErrorMessage = "Please select a form.")]
        public int? FormId { get; set; }

        [Display(Name = "Select field:")]
        [Required(ErrorMessage = "Please select a field.")]
        public int? FieldId { get; set; }
        public List<SelectListItem>? FormType { get; set; }
        public List<SelectListItem>? FieldType { get; set; }
        public FormDTO SelectedForm { get; set; } = new FormDTO();
        public FieldDTO SelectedField { get; set; } = new FieldDTO();
    }
}