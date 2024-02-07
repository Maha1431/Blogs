using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccessLayer.Entities
{
    public partial class BlogApplicationContext : DbContext
    {
        public BlogApplicationContext()
        {
        }

        public BlogApplicationContext(DbContextOptions<BlogApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlogComments> BlogComments { get; set; }
        public virtual DbSet<Blogs> Blogs { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=PA-5CG144123X;Initial Catalog=BlogApplication;User ID=AzureAD/HindhujhaSridhar;Password=Hindhu17");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogComments>(entity =>
            {
                entity.Property(e => e.Comments)
                    .IsRequired()
                    .HasMaxLength(3000);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PostedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.BlogComments)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK__BlogComme__BlogI__3C69FB99");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BlogComments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BlogComme__UserI__3D5E1FD2");
            });

            modelBuilder.Entity<Blogs>(entity =>
            {
                entity.HasIndex(e => e.Title)
                    .HasName("idx_Title")
                    .IsUnique();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PostedDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Blogs__UserId__398D8EEE");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.EmailId)
                    .HasName("UQ__Users__7ED91ACE262E81E9")
                    .IsUnique();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Otp).HasColumnName("OTP");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
