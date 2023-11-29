using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Web.Models.Manage
{
    public class AddQuestionViewModel
    {
        [Display(Name = "Text")]
        [Required(ErrorMessage = "Please enter the question.")]
        public string Text { get; set; } = string.Empty;

        [Display(Name = "Select Form:")]
        [Required(ErrorMessage = "Please select a form.")]
        public int FormId { get; set; }

        [Display(Name = "Select Field:")]
        [Required(ErrorMessage = "Please select a field.")]
        public int FieldId { get; set; }

        public List<SelectListItem>? Forms { get; set; }

        public List<SelectListItem>? Fields { get; set; }

        public FormDTO SelectedForm { get; set; } = new FormDTO();

        public FieldDTO SelectedField { get; set; } = new FieldDTO();
    }
}