using Microsoft.EntityFrameworkCore;

namespace Kairos.Infrastructure.Persistence;

public sealed class KairosDbContext(DbContextOptions<KairosDbContext> options)
    : DbContext(options)
{
    public DbSet<StoredFitUpload> FitUploads => Set<StoredFitUpload>();
    public DbSet<StoredActivity> Activities => Set<StoredActivity>();

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

        var activity = modelBuilder.Entity<StoredActivity>();
        activity.ToTable("activities");
        activity.HasKey(value => value.Id);
        activity.Property(value => value.Id).HasColumnName("id");
        activity
            .Property(value => value.OwnerSubject)
            .HasColumnName("owner_subject")
            .HasMaxLength(255)
            .IsRequired();
        activity.Property(value => value.SourceUploadId).HasColumnName("source_upload_id");
        activity
            .Property(value => value.ActivityType)
            .HasColumnName("activity_type")
            .HasMaxLength(64)
            .IsRequired();
        activity.Property(value => value.StartUtc).HasColumnName("start_utc");
        activity.Property(value => value.EndUtc).HasColumnName("end_utc");
        activity
            .Property(value => value.SourceKind)
            .HasColumnName("source_kind")
            .HasMaxLength(64)
            .IsRequired();
        activity
            .Property(value => value.SourceProvider)
            .HasColumnName("source_provider")
            .HasMaxLength(64)
            .IsRequired();
        activity
            .Property(value => value.OriginalFileName)
            .HasColumnName("original_file_name")
            .HasMaxLength(255);
        activity
            .Property(value => value.ContentHashSha256)
            .HasColumnName("content_hash_sha256")
            .HasMaxLength(64)
            .IsFixedLength();
        activity.Property(value => value.ImportedAtUtc).HasColumnName("imported_at_utc");
        activity.Property(value => value.Document).HasColumnName("document").HasColumnType("jsonb");
        activity.HasIndex(value => value.SourceUploadId).IsUnique();
        activity.HasIndex(value => new { value.OwnerSubject, value.StartUtc });
        activity
            .HasOne<StoredFitUpload>()
            .WithOne()
            .HasForeignKey<StoredActivity>(value => value.SourceUploadId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
