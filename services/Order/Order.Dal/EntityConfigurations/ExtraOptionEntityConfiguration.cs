using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities.ExtraOptionAggregate;

namespace Order.Dal.EntityConfigurations
{
    public class ExtraOptionEntityConfiguration : IEntityTypeConfiguration<ExtraOption>
    {
        public void Configure(EntityTypeBuilder<ExtraOption> builder)
        {
        }
    }
}
