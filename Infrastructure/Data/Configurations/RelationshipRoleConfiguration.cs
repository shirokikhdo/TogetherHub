namespace Infrastructure.Data.Configurations;

public class RelationshipRoleConfiguration 
    : IEntityTypeConfiguration<Relationship>
{
    public void Configure(EntityTypeBuilder<Relationship> builder)
    {
        builder.Property(item => item.Role)
            .HasConversion<string>();
    }
}