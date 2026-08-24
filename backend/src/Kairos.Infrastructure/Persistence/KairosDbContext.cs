using Microsoft.EntityFrameworkCore;

namespace Kairos.Infrastructure.Persistence;

public sealed class KairosDbContext(DbContextOptions<KairosDbContext> options)
    : DbContext(options)
{
    public DbSet<StoredFitUpload> FitUploads => Set<StoredFitUpload>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var upload = modelBuilder.Entity<StoredFitUpload>();
        upload.ToTable("fit_uploads");
        upload.HasKey(value => value.Id);
        upload.Property(value => value.Id).HasColumnName("id");
        upload
            .Property(value => value.OwnerSubject)
            .HasColumnName("owner_subject")
            .HasMaxLength(255)
            .IsRequired();
        upload
            .Property(value => value.OriginalFileName)
            .HasColumnName("original_file_name")
            .HasMaxLength(255)
            .IsRequired();
        upload
            .Property(value => value.ContentType)
            .HasColumnName("content_type")
            .HasMaxLength(100)
            .IsRequired();
        upload.Property(value => value.SizeBytes).HasColumnName("size_bytes");
        upload
            .Property(value => value.Sha256)
            .HasColumnName("sha256")
            .HasMaxLength(64)
            .IsFixedLength()
            .IsRequired();
        upload.Property(value => value.UploadedAtUtc).HasColumnName("uploaded_at_utc");
        upload
            .Property(value => value.Status)
            .HasColumnName("status")
            .HasMaxLength(32)
            .IsRequired();
        upload.Property(value => value.Content).HasColumnName("content").IsRequired();
        upload.HasIndex(value => new { value.OwnerSubject, value.UploadedAtUtc });
    }
}
