using cFB.Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cFB.Data.Configurations
{
    public class RoleManagersConfiguration : IEntityTypeConfiguration<RoleManager>
    {
        public void Configure(EntityTypeBuilder<RoleManager> builder)
        {
            builder.ToTable("RoleManager");
            builder.HasKey(x => x.ManagerId);
            builder.Property(x => x.ManagerName).IsRequired();
        }
    }
}
