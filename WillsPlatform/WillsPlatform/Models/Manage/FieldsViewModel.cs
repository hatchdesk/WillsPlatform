using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Web.Models.Manage
{
    public class FieldsViewModel : BaseViewModel
    {
        public IEnumerable<FieldDTO> Fields { get; set; } = new List<FieldDTO>();
    }
}
