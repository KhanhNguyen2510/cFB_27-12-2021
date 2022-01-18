using cFB.Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace cFB.Data.Configurations
{
    public class AdministrativeDivisionConfiguration : IEntityTypeConfiguration<AdministrativeDivision>
    {
        public void Configure(EntityTypeBuilder<AdministrativeDivision> builder)
        {
            builder.ToTable("AdministrativeDivision");
            builder.HasKey(x => x.AdministrativeDivisionId);
            builder.Property(x => x.AdministrativeDivisionId).HasColumnType("varchar(10)");
            builder.Property(x => x.AdministrativeDivisionName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.NumberPhone).HasColumnType("varchar(13)");
            builder.Property(x => x.Addrees).HasMaxLength(300);
            builder.Property(x => x.TimeOnline).HasDefaultValue(DateTime.Now).HasColumnType("datetime");
            builder.Property(x => x.Password).IsRequired().HasMaxLength(100);
            builder.HasOne(x => x.RoleManager).WithMany(x => x.AdministrativeDivision).HasForeignKey(x => x.ManagerId);
        }
    }
}
