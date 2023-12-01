using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Web.Models.Manage
{
    public class AddTemplateViewModel
    {
        [Display(Name = "Text")]
        public string Text { get; set; }
        public FormDTO SelectedForm { get; set; } = new FormDTO();
        [Display(Name = "Select form:")]
        [Required(ErrorMessage = "Please select a form.")]
        public int? FormId { get; set; }
        public List<SelectListItem>? Forms { get; set; }
    }
}
