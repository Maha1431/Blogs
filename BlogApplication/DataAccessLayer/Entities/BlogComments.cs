using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccessLayer.Entities
{
    public partial class BlogComments
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int BlogId { get; set; }
        public int UserId { get; set; }

        public virtual Blogs Blog { get; set; }
        public virtual Users User { get; set; }
    }
}
