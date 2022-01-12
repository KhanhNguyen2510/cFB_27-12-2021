using cFB.Data.Entites;
using cFB.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cFB.Data.Configurations
{
    class WatchListsConfiguration : IEntityTypeConfiguration<WatchList>
    {
        public void Configure(EntityTypeBuilder<WatchList> builder)
        {
            builder.ToTable("WatchList");
            builder.HasKey(x => x.FaceBookId);
            builder.Property(x => x.FaceBookId).HasMaxLength(100);
            builder.Property(x => x.FaceBookName).IsRequired().HasMaxLength(500);
            builder.Property(x => x.FaceBookUrl).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Status).HasDefaultValue(Status.Activate);
            builder.HasOne(x => x.AdministrativeDivisions).WithMany(x => x.WatchLists).HasForeignKey(x => x.AdministrativeDivisionId);
            builder.HasOne(x => x.FaceBookType).WithMany(x => x.WatchLists).HasForeignKey(x => x.FaceBookTypeId);
        }
    }
}
