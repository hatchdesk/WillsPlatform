using WillsPlatform.Application.DTOs;

namespace WillsPlatform.Web.Models.Manage
{
    public class FieldsViewModel
    {
        public IEnumerable<FieldDTO> Fields { get; set; } = new List<FieldDTO>();
    }
}
