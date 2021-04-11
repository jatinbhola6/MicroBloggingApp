using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBloggingApp.VIewModels
{
    public class RegistrationModel
    {
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        [Required]
        [Key]
        public string Email
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        [RegularExpression("^[A-z0-9]{8,20}$", ErrorMessage = "Password must contain atleast an upper case alphabet, a lower case alphabet, a number and have length between 8 and 20 characters")]
        [Required]
        public string Password
        {
            get;
            set;
        }
    }
}
