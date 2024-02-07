using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.DataTransferObjects
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }

        [Required]
        public string Password { get; set; }
     
    }
}
