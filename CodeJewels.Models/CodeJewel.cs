using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJewels.Models
{
    public class CodeJewel
    {
        public int Id { get; set; }
        [Required]
        public string SourceCode { get; set; }
        public string AuthorEmail { get; set; }
        public int Rating { get; set; }
        public Category Category { get; set; }

    }
}
