
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeJewels.Services.Models
{
    public class CodeJewelsModel
    {
        public int Id { get; set; }

        public string SourceCode { get; set; }

        public int Rating { get; set; }

        public string AuthorEmail { get; set; }

        public string CodeLanguage { get; set; }
    }
}