using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CommonLayer.DataTransferObjects
{
    public class AddUserDTO
    {
        [Required]
        [MinLength(3),MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        //[EmailAddress]
        public string EmailId { get; set; }

        [Required]
        [MinLength(8), MaxLength(16)]
        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }                              

    }
}
