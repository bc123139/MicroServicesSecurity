using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServer.Entities.Configuration
{
    public class RoleConfiguration :IEntityTypeConfiguration<IdentityRole>
    {

        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole
            {
                Name = "Àdmin",
                NormalizedName = "ADMIN",
                Id = "42c5448c-e9a9-4300-b84c-29b7e9aab7fd"
            },
            new IdentityRole
            {
                Name= "Visitor",
                NormalizedName = "VISITOR",
                Id= "0309d39b-fac2-4b25-b163-8a96db6ec720"
            }
            );
        }
    }
}
