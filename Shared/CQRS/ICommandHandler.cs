namespace Shared.CQRS;

/// <summary>
/// Интерфейс, представляющий обработчик команды, который принимает команду типа <typeparamref name="TCommand"/> 
/// и возвращает ответ типа <typeparamref name="TResponse"/>.
/// Наследует от <see cref="IRequestHandler{TCommand, TResponse}"/>.
/// </summary>
/// <typeparam name="TCommand">Тип команды, которая будет обрабатываться.</typeparam>
/// <typeparam name="TResponse">Тип ответа, который будет возвращен при выполнении команды.</typeparam>
/// <remarks>Тип <typeparamref name="TResponse"/> должен быть не null.</remarks>
public interface ICommandHandler<in TCommand, TResponse> 
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{
    
}

/// <summary>
/// Интерфейс, представляющий обработчик команды, который принимает команду типа <typeparamref name="TCommand"/> 
/// и не возвращает никакого значения.
/// Наследует от <see cref="ICommandHandler{TCommand, Unit}"/>.
/// </summary>
/// <typeparam name="TCommand">Тип команды, которая будет обрабатываться.</typeparam>
public interface ICommandHandler<in TCommand>
    : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand<Unit>
{

}