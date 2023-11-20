using Microsoft.AspNetCore.Mvc;
using System.Text;
using WillsPlatform.Application.DTOs;
using WillsPlatform.Application.Services;
using WillsPlatform.Web.Models.Forms;

namespace WillsPlatform.Web.Controllers
{
    public class FormController : Controller
    {
        private readonly IFormService _formService;

        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        public async Task<IActionResult> Create(int id)
        {
            var questions = await  _formService.GetFormQuestionsAsync(id);
            var questionModel = questions.Select(q => new QuestionViewModel()
            {
                Id = q.Id,
                Name = q.Name,
                FieldId = q.FieldId,
                Text = q.Text
            });
                     
            return View( new FormViewModel () {FormId = id, Questions = questionModel});
        }      

        [HttpPost]
        public async Task<IActionResult> Generate(FormViewModel model, IFormCollection collection)
        {
            try
            {
                //Fetch Template based on form.
                var template = await _formService.GetFormTemplateAsync(model.FormId);

                //Parse template tockens by user inputs.
                var templatecontent = ParseTemplate(template, collection);
                var parsedContent = BuildFormContent(templatecontent);
                return File(Encoding.UTF8.GetBytes(parsedContent), "application/vnd.ms-word", "Will.doc");
            }
            catch(Exception ex)
            {
                string exceptionMessage = ex.Message;
                return View();
            }
        }


        #region Helper Methods --
        private string ParseTemplate(TemplateDTO template, IFormCollection userInputs)
        {
            var content = template.Text ?? string.Empty;
            var tokens = template.Tokens ?? new List<TokenDTO>();

            foreach (var tok in tokens)
            {
                if (userInputs.ContainsKey(tok.Name))
                    content = content?.Replace(tok.Text, userInputs[tok.Name]);
            }
            
            return BuildFormContent(content);
        }

        private string BuildFormContent(string htmlTemplateContent)
        {
            //build the content for the dynamic Word document
            //in HTML alongwith some Office specific style properties. 
            var strBody = new StringBuilder();

            strBody.Append("<html " +
             "xmlns:o='urn:schemas-microsoft-com:office:office' " +
             "xmlns:w='urn:schemas-microsoft-com:office:word'" +
              "xmlns='http://www.w3.org/TR/REC-html40'>" +
              "<head><title>Time</title>");

            //The setting specifies document's view after it is downloaded as Print
            //instead of the default Web Layout
            strBody.Append("<!--[if gte mso 9]>" +
             "<xml>" +
             "<w:WordDocument>" +
             "<w:View>Print</w:View>" +
             "<w:Zoom>90</w:Zoom>" +
             "<w:DoNotOptimizeForBrowser/>" +
             "</w:WordDocument>" +
             "</xml>" +
             "<![endif]-->");

            strBody.Append("<style>" +
             "<!-- /* Style Definitions */" +
             "@page Section1" +
             "   {size:8.5in 11.0in; " +
             "   margin:1.0in 1.25in 1.0in 1.25in ; " +
             "   mso-header-margin:.5in; " +
             "   mso-footer-margin:.5in; mso-paper-source:0;}" +
             " div.Section1" +
             "   {page:Section1;}" +
             "-->" +
             "</style></head>");

            strBody.Append("<body lang=EN-US style='tab-interval:.5in'>" +
             "<div class=Section1>");
            strBody.Append(htmlTemplateContent ?? string.Empty);
            strBody.Append("</div></body></html>");

            return strBody.ToString();
        }

        #endregion
    }
}
