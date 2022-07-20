using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebrand.Application.ViewModels.CustomerView
{
    public class UpdateCustomerViewModel
    {

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The First Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]


        public string FName { get; set; }

        [Required(ErrorMessage = "The Last Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]

        public string LName { get; set; }

        [Required(ErrorMessage = "The E-mail is Required")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The BirthDate is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Birth Date")]
        public DateTime BirthDate { get; set; }
    }
}
