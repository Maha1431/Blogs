using System;

namespace CommonLayer.DataTransferObjects
{
    public class AddCommentDTO
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public string Comments { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
    