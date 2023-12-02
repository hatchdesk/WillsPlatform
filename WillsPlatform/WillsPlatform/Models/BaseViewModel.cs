namespace WillsPlatform.Web.Models
{
    public class BaseViewModel
    {
        public string Heading { get; set; } = string.Empty;
        public List<Breadcrumb> Breadcrumbs { get; set; } = new List<Breadcrumb>();
    }
}
