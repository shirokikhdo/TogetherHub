namespace Shared.CQRS;

/// <summary>
/// Интерфейс, представляющий команду, которая возвращает ответ типа <typeparamref name="TResponse"/>.
/// Наследует от <see cref="IRequest{TResponse}"/>.
/// </summary>
/// <typeparam name="TResponse">Тип ответа, который будет возвращен при выполнении команды.</typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>
{
    
}

/// <summary>
/// Интерфейс, представляющий команду, которая не возвращает никакого значения.
/// Наследует от <see cref="IRequest{Unit}"/>.
/// </summary>
public interface ICommand : IRequest<Unit>
{

}