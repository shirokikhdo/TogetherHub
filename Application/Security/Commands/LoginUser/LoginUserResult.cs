using Domain.Security.Dtos;

namespace Application.Security.Commands.LoginUser;

public record LoginUserResult(ResponseIdentityUserDto User);