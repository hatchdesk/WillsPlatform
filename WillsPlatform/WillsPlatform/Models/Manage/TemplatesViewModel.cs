using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Web.Models.Manage
{
    public class TemplatesViewModel : BaseViewModel
    {
        public IEnumerable<TemplateDTO> Templates { get; set; } = new List<TemplateDTO>();
    }
}
