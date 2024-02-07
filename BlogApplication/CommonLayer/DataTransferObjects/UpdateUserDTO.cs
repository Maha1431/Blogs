﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.DataTransferObjects
{
    public class UpdateUserDTO  
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}

