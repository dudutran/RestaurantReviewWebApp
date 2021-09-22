using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ReviewApp.DataAccess.Entities
{
    public partial class ReviewDbContext : DbContext
    {
        public ReviewDbContext()
        {
        }

        public ReviewDbContext(DbContextOptions<ReviewDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<ReviewJoin> ReviewJoins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("Restaurant");

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<ReviewJoin>(entity =>
            {
                entity.ToTable("ReviewJoin");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ReviewJoins)
                    .HasForeignKey(d => d.CustomerId)
                    //.OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReviewJoi__Custo__6477ECF3");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.ReviewJoins)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReviewJoi__Resta__6383C8BA");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.ReviewJoins)
                    .HasForeignKey(d => d.ReviewId)
                    //.OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReviewJoi__Revie__628FA481");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
