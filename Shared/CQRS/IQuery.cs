namespace Shared.CQRS;

/// <summary>
/// Интерфейс, представляющий запрос, который возвращает ответ типа <typeparamref name="TResponse"/>.
/// Наследует от <see cref="IRequest{TResponse}"/>.
/// </summary>
/// <typeparam name="TResponse">Тип ответа, который будет возвращен при выполнении запроса.</typeparam>
/// <remarks>Тип <typeparamref name="TResponse"/> должен быть совместим с принципом ковариантности.</remarks>
public interface IQuery<out TResponse> 
    : IRequest<TResponse>
{
    
}