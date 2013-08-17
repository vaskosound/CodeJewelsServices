using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJewels.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CodeLanguage { get; set; }

        public ICollection<CodeJewel> CodeJewels { get; set; }

        public Category()
        {
            this.CodeJewels = new HashSet<CodeJewel>();
        }
    }
}
