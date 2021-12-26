using cFB.Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cFB.Data.Configurations
{
    public class SentimentLabelsConfiguration : IEntityTypeConfiguration<SentimentLabel>
    {
        public void Configure(EntityTypeBuilder<SentimentLabel> builder)
        {
            builder.ToTable("SentimentLabel");
            builder.Property(x => x.SentimentLabelId).HasColumnType("varchar(8)");
            builder.Property(x => x.SentimentLabelName).IsRequired().HasMaxLength(200);
        }
    }
}
