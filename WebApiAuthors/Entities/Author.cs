using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiAuthors.Validators;

namespace WebApiAuthors.Entities
{
    public class Author
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(40, ErrorMessage = "The maximum size of characters for the field {0} is {1}.")]
        [FirstCharToUpper]
        [ExistsName]
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
