using AppWeather.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppWeather.Data.Configurations
{
    public class HistoryConfiguration : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.ToTable("History");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.AppUser).WithMany(x => x.Histories).HasForeignKey(x => x.IdUser);
            builder.HasOne(x => x.City).WithMany(x => x.Histories).HasForeignKey(x => x.IdCity);
        }
    }
}
