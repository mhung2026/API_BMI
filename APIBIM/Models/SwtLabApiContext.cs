using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIBIM.Models;

public partial class SwtLabApiContext : DbContext
{
    public SwtLabApiContext()
    {
    }

    public SwtLabApiContext(DbContextOptions<SwtLabApiContext> options)
        : base(options)
    {
    }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FA47C9D4D");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E6164122C16C3").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__users__F3DBC5724D5D65D6").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
