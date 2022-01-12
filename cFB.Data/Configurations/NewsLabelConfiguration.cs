using cFB.Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cFB.Data.Configurations
{
    public class NewsLabelConfiguration : IEntityTypeConfiguration<NewsLabel>
    {
        public void Configure(EntityTypeBuilder<NewsLabel> builder)
        {
            builder.ToTable("NewsLabel");
            builder.HasKey(x => x.NewsLabelId);
            builder.Property(x => x.NewsLabelId).HasColumnType("varchar(8)");
            builder.Property(x => x.NewsLabelName).IsRequired().HasMaxLength(100);
        }
    }
}
