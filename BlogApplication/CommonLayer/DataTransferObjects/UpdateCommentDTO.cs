using System;

namespace CommonLayer.DataTransferObjects
{
    public class UpdateCommentDTO
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public string Comments { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
