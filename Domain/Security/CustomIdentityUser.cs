namespace Domain.Security;

/// <summary>
/// Представляет пользовательскую сущность, расширяющую стандартную 
/// функциональность класса <see cref="IdentityUser"/>.
/// </summary>
public class CustomIdentityUser : IdentityUser
{
    /// <summary>
    /// Получает или задает полное имя пользователя.
    /// </summary>
    public string FullName { get; set; } = default!;

    /// <summary>
    /// Получает или задает информацию о пользователе.
    /// </summary>
    public string About { get; set; } = default!;

    public List<Relationship> Topics { get; set; } = new List<Relationship>();
}