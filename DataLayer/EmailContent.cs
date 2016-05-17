using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class EmailContent
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        [Display(Name = "Your Name")]
        public string ContactName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Your Email")]
        public string ContactEmail { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Your Contact Number")]
        public string ContactNumber { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }

    }
}
