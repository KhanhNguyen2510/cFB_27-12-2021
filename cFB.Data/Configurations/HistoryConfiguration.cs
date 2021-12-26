using cFB.Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cFB.Data.Configurations
{
    public class HistoryConfiguration : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.ToTable("History");
            builder.HasKey(x => new { x.AdministrativeDivisionId, x.Time });
            builder.Property(x => x.StatusHistory).HasMaxLength(400);
            builder.Property(x => x.Event).IsRequired();
            builder.HasOne(x => x.AdministrativeDivisions).WithMany(x => x.Histories).HasForeignKey(x => x.AdministrativeDivisionId);
        }
    }
}
