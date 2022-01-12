using cFB.Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace cFB.Data.Configurations
{
    class HistoryClientConfiguration : IEntityTypeConfiguration<HistoryClient>
    {
        public void Configure(EntityTypeBuilder<HistoryClient> builder)
        {
            builder.ToTable("HistoryClient");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.IPAddress).HasMaxLength(100);
            builder.Property(x => x.NameMachine).HasMaxLength(100);
            builder.Property(x => x.Time).HasDefaultValue(DateTime.Now);
            builder.HasOne(x => x.AdministrativeDivisions).WithMany(x => x.HistoryClients).HasForeignKey(x => x.AdministrativeDivisionID);
        }
    }
}
