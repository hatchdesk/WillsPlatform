using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WillsPlatform.Application.Enumerations;

namespace WillsPlatform.Web.TagHelpers
{
    [HtmlTargetElement("question")]
    public class QuestionTagHelper : TagHelper
    {
        public int FieldType { get; set; }
        public string FieldName { get; set; } = string.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = GetTagName(FieldType);
            output.TagMode = TagMode.StartTagAndEndTag;
            if (output.TagName == "div")
            {

            }
            else 
            {
                output.Attributes.SetAttribute("name", FieldName);
                output.Attributes.SetAttribute("type", GetInputType(FieldType));
            }

            if (FieldType == FieldTypes.Dropdown.Value && output.TagName == "select")
            {
                var selectList = GetSelectList();
                foreach (var option in selectList)
                {
                    var optionTag = new TagBuilder("option");
                    optionTag.Attributes.Add("value", option.Value);
                    optionTag.InnerHtml.AppendHtml(option.Text);
                    output.Content.AppendHtml(optionTag);
                }
            }

            if (FieldType == FieldTypes.RadioButton.Value && output.TagName == "div")
            {
                var radioValues = new List<string> { "male", "female" };

                var divTag = new TagBuilder("div");

                foreach (var radioValue in radioValues)
                {
                    var formCheckDivTag = new TagBuilder("div");
                    formCheckDivTag.AddCssClass("form-check");

                    var radioTag = new TagBuilder("input");
                    radioTag.Attributes.Add("type", "radio");
                    radioTag.Attributes.Add("class", "form-check-input");
                    radioTag.Attributes.Add("id", "gridRadio" + radioValue);
                    radioTag.Attributes.Add("name", FieldName + "[" + radioValue + "]");
                    radioTag.Attributes.Add("value", radioValue);

                    var labelTag = new TagBuilder("label");
                    labelTag.AddCssClass("form-check-label");
                    labelTag.Attributes.Add("for", "gridRadio" + radioValue);
                    labelTag.InnerHtml.AppendHtml($" {radioValue} ");

                    formCheckDivTag.InnerHtml.AppendHtml(radioTag);
                    formCheckDivTag.InnerHtml.AppendHtml(labelTag);
                    divTag.InnerHtml.AppendHtml(formCheckDivTag);
                }
                output.Content.AppendHtml(divTag);
            }

            if (FieldType == FieldTypes.Checkbox.Value && output.TagName == "div")
            {
                var checkboxValues = new List<string> { "male", "female" };

                var divTag = new TagBuilder("div");
                divTag.AddCssClass("col-sm-10");

                foreach (var checkboxValue in checkboxValues)
                {
                    var formCheckDivTag = new TagBuilder("div");
                    formCheckDivTag.AddCssClass("form-check");

                    var checkboxTag = new TagBuilder("input");
                    checkboxTag.Attributes.Add("type", "checkbox");
                    checkboxTag.Attributes.Add("class", "form-check-input");
                    checkboxTag.Attributes.Add("id", "gridCheck" + checkboxValue);
                    checkboxTag.Attributes.Add("name", FieldName + "[" + checkboxValue + "]");
                    checkboxTag.Attributes.Add("value", checkboxValue);
                    checkboxTag.Attributes.Add("checked", "checked");

                    var labelTag = new TagBuilder("label");
                    labelTag.AddCssClass("form-check-label");
                    labelTag.Attributes.Add("for", "gridCheck" + checkboxValue);
                    labelTag.InnerHtml.AppendHtml($" {checkboxValue} ");

                    formCheckDivTag.InnerHtml.AppendHtml(checkboxTag);
                    formCheckDivTag.InnerHtml.AppendHtml(labelTag);
                    divTag.InnerHtml.AppendHtml(formCheckDivTag);
                }
                output.Content.AppendHtml(divTag);
            }

            else if (FieldType == FieldTypes.DateTime.Value && output.TagName == "input")
            {
                output.Attributes.Add("class", "datepicker");
                output.Attributes.Add("data-toggle", "datepicker");
                output.Attributes.Add("value", "");
                IncludeDatepickerScripts(output);
            }
        }

        private string GetTagName(int fieldType)
        {
            return fieldType switch
            {
                1 => "input",
                2 => "select",
                3 => "div",
                4 => "div",
                5 => "textarea",
                6 => "input",
                _ => "input",
            };
        }

        private string GetInputType(int fieldType)
        {
            return fieldType switch
            {
                1 => "text",
                2 => "select",
                3 => "radio",
                4 => "checkbox",
                5 => "text",
                6 => "datetime-local",
                _ => "text",
            };
        }

        private void IncludeDatepickerScripts(TagHelperOutput output)
        {
            var scriptTag = new TagBuilder("script");
            scriptTag.Attributes.Add("src", "https://code.jquery.com/jquery-3.6.4.min.js");
            output.Content.AppendHtml(scriptTag);

            var styleTag = new TagBuilder("link");
            styleTag.Attributes.Add("rel", "stylesheet");
            styleTag.Attributes.Add("href", "https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css");
            output.Content.AppendHtml(styleTag);

            var datepickerScriptTag = new TagBuilder("script");
            datepickerScriptTag.InnerHtml.AppendHtml("$(document).ready(function() { $('.datepicker').datepicker(); });");
            output.Content.AppendHtml(datepickerScriptTag);
        }

        private List<SelectListItem> GetSelectList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "Select Gender" },
                new SelectListItem { Value = "male", Text = "Male" },
                new SelectListItem { Value = "female", Text = "Female" },
            };
        }
    }
}
