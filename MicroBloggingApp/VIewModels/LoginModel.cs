using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBloggingApp.VIewModels
{
    public class LoginModel
    {
        [Required]
        [Key]
        [DataType(DataType.EmailAddress)]
        public string email
        {
            get;
            set;
        }

        [Required]
        [RegularExpression(@"^[A-z0-9]{8,20}$", ErrorMessage = "Must contain an uppercase character, lower case character, number")]
        [DataType(DataType.Password)]
        public string password
        {
            get;
            set;
        }
    }
}
