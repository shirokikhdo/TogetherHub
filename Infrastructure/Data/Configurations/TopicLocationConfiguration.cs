namespace Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Topic"/> для настройки свойств местоположения.
/// </summary>
public class TopicLocationConfiguration : IEntityTypeConfiguration<Topic>
{
    /// <summary>
    /// Настраивает свойства сущности <see cref="Topic"/> в контексте базы данных,
    /// включая вложенное свойство <see cref="Topic.Location"/>.
    /// </summary>
    /// <param name="builder">Строитель сущности для конфигурации.</param>
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.OwnsOne(
            topic => topic.Location, 
            location => 
            {
                location.Property(l => l.City).HasColumnName("City");
                location.Property(l => l.Street).HasColumnName("Street");
            });
    }
}