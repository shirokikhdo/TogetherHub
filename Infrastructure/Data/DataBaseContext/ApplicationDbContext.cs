namespace Infrastructure.Data.DataBaseContext;

/// <summary>
/// Контекст базы данных для приложения, наследующий от <see cref="IdentityDbContext{TUser}"/>.
/// Управляет сущностями и их конфигурацией в базе данных.
/// </summary>
public class ApplicationDbContext 
    : IdentityDbContext<CustomIdentityUser>, IApplicationDbContext
{
    /// <summary>
    /// Получает набор сущностей <see cref="Topic"/> для работы с темами в базе данных.
    /// </summary>
    public DbSet<Topic> Topics => 
        Set<Topic>();

    public DbSet<Relationship> Relationships =>
        Set<Relationship>();

    public DbSet<Comment> Comments => 
        Set<Comment>();

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ApplicationDbContext"/> с указанными параметрами конфигурации.
    /// </summary>
    /// <param name="options">Параметры конфигурации для контекста базы данных.</param>
    public ApplicationDbContext(DbContextOptions options) 
        : base(options)
    {
        
    }

    /// <summary>
    /// Настраивает модель базы данных при создании контекста.
    /// Применяет все конфигурации из сборки, в которой находится данный контекст.
    /// </summary>
    /// <param name="modelBuilder">Строитель модели, используемый для настройки сущностей.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}