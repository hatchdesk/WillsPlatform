using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Web.Models.Manage
{
    public class FormsViewModel
    {
        public IEnumerable<FormDTO> Forms { get; set; } = new List<FormDTO>();
    }
}
