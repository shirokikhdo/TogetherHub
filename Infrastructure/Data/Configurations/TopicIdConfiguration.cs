namespace Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Topic"/> для использования с Entity Framework.
/// </summary>
public class TopicIdConfiguration : IEntityTypeConfiguration<Topic>
{
    /// <summary>
    /// Настраивает свойства сущности <see cref="Topic"/> в контексте базы данных.
    /// </summary>
    /// <param name="builder">Строитель сущности для конфигурации.</param>
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.Property(topic => topic.Id)
            .HasConversion(
                id => id.Value, 
                value => TopicId.Of(value));
    }
}