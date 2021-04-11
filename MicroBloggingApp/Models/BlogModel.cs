using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBloggingApp.Models
{
    public class BlogModel
    {
        public string Body
        {
            get;
            set;
        }

        public DateTime CreateDate
        {
            get;
            set;
        }

        [Required]
        public string Heading
        {
            get;
            set;
        }

        [Key]
        [Required]
        public int Id
        {
            get;
            set;
        }

        public UserModel User
        {
            get;
            set;
        }

        [ForeignKey("Email")]
        public string UserId
        {
            get;
            set;
        }

        public BlogModel()
        {
        }
    }
}
