namespace Shared.CQRS;

/// <summary>
/// Интерфейс, представляющий обработчик запросов типа <typeparamref name="TQuery"/>.
/// </summary>
/// <typeparam name="TQuery">Тип запроса, который обрабатывается. Должен реализовывать <see cref="IQuery{TResponse}"/>.</typeparam>
/// <typeparam name="TResponse">Тип ответа, который будет возвращен при обработке запроса. Не должен быть равен null.</typeparam>
/// <remarks>Этот интерфейс наследует от <see cref="IRequestHandler{TQuery, TResponse}"/> и обеспечивает обработку запросов с заданными типами.</remarks>
public interface IQueryHandler<in TQuery, TResponse> 
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
    
}