using cFB.Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cFB.Data.Configurations
{
    public class FaceBookTypeConfiguration : IEntityTypeConfiguration<FaceBookType>
    {
        public void Configure(EntityTypeBuilder<FaceBookType> builder)
        {
            builder.ToTable("FaceBookType");
            builder.HasKey(x => x.FaceBookTypeId);
            builder.Property(x => x.FaceBookTypeId).HasColumnType("varchar(8)");
            builder.Property(x => x.FaceBookTypeName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(500);
        }
    }
}
