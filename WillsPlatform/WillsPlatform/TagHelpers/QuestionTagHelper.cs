using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WillsPlatform.Web.TagHelpers
{
    [HtmlTargetElement("question")]
    public class QuestionTagHelper : TagHelper
    {
        public int fieldType { get; set; }
        public string fieldName { get; set; } = string.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = GetTagName(fieldType);
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.SetAttribute("name", fieldName);
            output.Attributes.SetAttribute("type", GetInputType(fieldType));
        }

        private string GetTagName(int fieldType)
        {
            return "input";
        }
        private string GetInputType(int fieldType)
        {
            return "text";
        }
    }
}
