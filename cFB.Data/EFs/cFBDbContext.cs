using cFB.Data.Configurations;
using cFB.Data.Entites;
using cFB.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace cFB.Data.EFs
{
    public class cFBDbContext : DbContext
    {
        public cFBDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdministrativeDivisionConfiguration());
            modelBuilder.ApplyConfiguration(new FaceBookTypeConfiguration());
            modelBuilder.ApplyConfiguration(new NewsLabelConfiguration());
            modelBuilder.ApplyConfiguration(new PostsConfiguration());
            modelBuilder.ApplyConfiguration(new SentimentLabelsConfiguration());
            modelBuilder.ApplyConfiguration(new WatchListsConfiguration());
            modelBuilder.ApplyConfiguration(new HistoryConfiguration());
            modelBuilder.ApplyConfiguration(new RoleManagersConfiguration());
            modelBuilder.ApplyConfiguration(new ReportsConfiguration());
            modelBuilder.ApplyConfiguration(new HistoryClientConfiguration());
            modelBuilder.Seed();
        }

        public DbSet<AdministrativeDivision> AdministrativeDivisions { get; set; }
        public DbSet<FaceBookType> FaceBookTypes { get; set; }
        public DbSet<NewsLabel> NewsLabels { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<SentimentLabel> SentimentLabels { get; set; }
        public DbSet<WatchList> WatchLists { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<RoleManager> RoleManagers { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<HistoryClient> HistoryClients { get; set; }
    }
}
