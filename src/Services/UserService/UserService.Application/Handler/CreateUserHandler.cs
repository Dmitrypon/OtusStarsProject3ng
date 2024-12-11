using AutoMapper;
using MediatR;
using UserService.Domain.Entities;
using UserService.Infrastructure.Repository;
using UserService.Application.Models;
using UserService.Application.Models.Requests;
using UserService.Application.Models.Response;
using UserService.Application.Models.Results;
using ILogger = Serilog.ILogger;
using Serilog;

namespace UserService.Application.Handler;

public class CreateUserHandler : IRequestHandler<CreateUserRequestDto, CreateUserResponseDto>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public CreateUserHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = Log.Logger;
    }

    public async Task<CreateUserResponseDto> Handle(CreateUserRequestDto request, CancellationToken cancellationToken)
    {
        _logger.Information("Пришёл запрос CreateUserRequestDto");

        var createUserResponseDto = new CreateUserResponseDto();
        try
        {
            _logger.Information("Приступаю к исполнению запроса");
            var userEntity = _mapper.Map<User>(request);

            var newUser = await _repository.AddAsync(userEntity, cancellationToken);
            if (newUser == null)
            {
                _logger.Error("Не смогли отработать запрос CreateUserRequestDto, результат добавления нового юзера в BD равен null");
                createUserResponseDto.Result = CreateUserResultModel.Fail;
                return createUserResponseDto;
            }

            await _repository.SaveChangesAsync(cancellationToken);

            _logger.Information("Успешно отработали запрос CreateUserRequestDto");

            var userDto = _mapper.Map<UserDto>(newUser);
            createUserResponseDto.User = userDto;
            createUserResponseDto.Result = CreateUserResultModel.Success;
            return createUserResponseDto;
        }
        catch(Exception e)
        {
            _logger.Error(e, "Исключение при попытке отработать запрос CreateUserRequestDto");
            createUserResponseDto.Result = CreateUserResultModel.Fail;
            return createUserResponseDto;
        }
    }
}
