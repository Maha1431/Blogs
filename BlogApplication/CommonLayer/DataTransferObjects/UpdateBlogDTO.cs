using System;

namespace CommonLayer.DataTransferObjects
{
    public class UpdateBlogDTO 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int UserId { get; set; }
    }
}
