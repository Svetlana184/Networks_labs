using System;
using System.Collections.Generic;
using LAB_1.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB_1;

public partial class SapunovaBooksContext : DbContext
{
    public SapunovaBooksContext()
    {
        Database.EnsureCreated();
    }

    public SapunovaBooksContext(DbContextOptions<SapunovaBooksContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<PublishingHouse> PublishingHouses { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=TEACHERPC;Initial Catalog=Sapunova_Books;User ID=user12;Password=user12;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.CodeAuthor).HasName("PK__Authors__B66BEF26E8DF9D1A");

            entity.Property(e => e.CodeAuthor).HasColumnName("Code_author");
            entity.Property(e => e.NameAuthor)
                .HasMaxLength(300)
                .HasColumnName("Name_author");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.CodeBook).HasName("PK__Books__62C11907AE37EC0E");

            entity.Property(e => e.CodeBook).HasColumnName("Code_book");
            entity.Property(e => e.CodeAuthor).HasColumnName("Code_author");
            entity.Property(e => e.CodePublish).HasColumnName("Code_publish");
            entity.Property(e => e.TitleBook)
                .HasMaxLength(150)
                .HasColumnName("Title_book");

            entity.HasOne(d => d.CodeAuthorNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.CodeAuthor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__Code_auth__3B75D760");

            entity.HasOne(d => d.CodePublishNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.CodePublish)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__Code_publ__3C69FB99");
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.CodeDelivery).HasName("PK__Deliveri__76AA043B0AE20F3D");

            entity.Property(e => e.CodeDelivery).HasColumnName("Code_delivery");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Inn)
                .HasMaxLength(20)
                .HasColumnName("INN");
            entity.Property(e => e.NameCompany)
                .HasMaxLength(100)
                .HasColumnName("Name_company");
            entity.Property(e => e.NameDelivery)
                .HasMaxLength(300)
                .HasColumnName("Name_delivery");
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<PublishingHouse>(entity =>
        {
            entity.HasKey(e => e.CodePublish).HasName("PK__Publishi__565A668CA79A4D6B");

            entity.ToTable("Publishing_house");

            entity.Property(e => e.CodePublish).HasColumnName("Code_publish");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Publish).HasMaxLength(100);
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.CodePurchase);

            entity.Property(e => e.CodePurchase).HasColumnName("Code_purchase");
            entity.Property(e => e.CodeBook).HasColumnName("Code_book");
            entity.Property(e => e.CodeDelivery).HasColumnName("Code_delivery");
            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.DateOrder)
                .HasColumnType("datetime")
                .HasColumnName("Date_order");
            entity.Property(e => e.TypePurchase)
                .HasMaxLength(10)
                .HasColumnName("Type_purchase");

            entity.HasOne(d => d.CodeBookNavigation).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.CodeBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Purchases__Code___412EB0B6");

            entity.HasOne(d => d.CodeDeliveryNavigation).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.CodeDelivery)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Purchases__Code___4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
