namespace WillsPlatform.Web.Models
{
    public class Breadcrumb
    {
        public string Text { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public bool Active { get; set; }

        public Breadcrumb() { }

        //Want to show text with URL
        public Breadcrumb(string text, string controller, string action = "",bool active = true)
        {
            this.Text = text;
            this.Action = action;
            this.Controller = controller;
            this.Active = active;
        }

        //Want to show only text
        public Breadcrumb(string text, bool active = false)
        {
            this.Text = text;
            this.Active = active;
        }
    }
}
