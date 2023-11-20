using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillsPlatform.Application.DTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int FormId { get; set; }
        public int FieldId { get; set; }
    }
}
