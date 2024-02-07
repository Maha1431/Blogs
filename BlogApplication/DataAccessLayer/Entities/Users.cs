using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccessLayer.Entities
{
    public partial class Users
    {
        public Users()
        {
            BlogComments = new HashSet<BlogComments>();
            Blogs = new HashSet<Blogs>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Otp { get; set; }

        public virtual ICollection<BlogComments> BlogComments { get; set; }
        public virtual ICollection<Blogs> Blogs { get; set; }
    }
}
