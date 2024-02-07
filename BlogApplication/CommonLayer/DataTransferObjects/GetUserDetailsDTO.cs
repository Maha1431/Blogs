using System.Collections.Generic;

namespace CommonLayer.DataTransferObjects
{
    public class GetUserDetailsDTO
    {
        public string FullName { get; set; }      
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}   
