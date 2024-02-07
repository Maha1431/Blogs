using System;

namespace CommonLayer.DataTransferObjects
{
    public  class AddBlogDTO    
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostedDate { get; set; }      
    }
}
