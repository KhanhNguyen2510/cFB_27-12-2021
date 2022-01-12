using cFB.Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace cFB.Data.Configurations
{
    public class ReportsConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Report");
            builder.HasKey(x => x.ReportId);
            builder.Property(x => x.DateReport).HasDefaultValue(DateTime.Now).HasColumnType("datetime");
            builder.Property(x => x.FileReport).HasDefaultValue("").HasMaxLength(500);
            builder.HasOne(x => x.AdministrativeDivision).WithMany(x => x.Reports).HasForeignKey(x => x.AdministrativeDivisionId);
            builder.HasOne(x => x.Posts).WithMany(x => x.Reports).HasForeignKey(x => x.PostId);
        }
    }
}
