using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shipping.Domain.Entities.ExtraOptionAggregate;

namespace Shipping.Dal.EntityConfigurations
{
    public class ExtraOptionEntityConfiguration : IEntityTypeConfiguration<ExtraOption>
    {
        public void Configure(EntityTypeBuilder<ExtraOption> builder)
        {
        }
    }
}
