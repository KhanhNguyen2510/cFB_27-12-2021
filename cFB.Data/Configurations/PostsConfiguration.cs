using cFB.Data.Entites;
using cFB.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace cFB.Data.Configurations
{
    public class PostsConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");
            builder.HasKey(x => x.PostId);
            builder.Property(x => x.PostId).HasMaxLength(500);
            builder.Property(x => x.PostUrl).IsRequired().HasMaxLength(400);
            builder.Property(x => x.UserUrl).IsRequired().HasMaxLength(400);
            builder.Property(x => x.PostContent);
            builder.Property(x => x.Image);
            builder.Property(x => x.UploadTime).HasDefaultValue(DateTime.Now).HasColumnType("datetime");
            builder.Property(x => x.CrawledTime).HasDefaultValue(DateTime.Now).HasColumnType("datetime");
            builder.Property(x => x.TotalLikes).HasDefaultValue(0);
            builder.Property(x => x.TotalComment).HasDefaultValue(0);
            builder.Property(x => x.TotalShare).HasDefaultValue(0);
            builder.Property(x => x.FilePDF).HasMaxLength(500).HasDefaultValue("");
            builder.Property(x => x.Report).HasDefaultValue(Reported.UnReported);
            builder.HasOne(x => x.NewsLabel).WithMany(x => x.Posts).HasForeignKey(x => x.NewsLabelId);
            builder.HasOne(x => x.WatchList).WithMany(x => x.Posts).HasForeignKey(x => x.FaceBookId);
            builder.HasOne(x => x.SentimentLabel).WithMany(x => x.Posts).HasForeignKey(x => x.SentimentLabelId);
            builder.HasOne(x => x.AdministrativeDivisions).WithMany(x => x.Posts).HasForeignKey(x => x.AdministrativeDivisionId);

        }
    }
}
