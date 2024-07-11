namespace RateIdeas.Application.Users.Commands;

public record UpdateUserCommand : IRequest<int>
{
    public UpdateUserCommand(UpdateUserCommand command)
    {
        Id = command.Id;
        Image = command.Image;
        Email = command.Email;
        LastName = command.LastName;
        Password = command.Password;
        FirstName = command.FirstName;
        DateOfBirth = command.DateOfBirth;
    }

    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public IFormFile Image { get; set; } = default!;
}

public class UpdateUserCommandHandler(IMapper mapper,
    IRepository<User> repository) :
    IRequestHandler<UpdateUserCommand, int>
{
    public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"This User is not found by id: {request.Id} | User update");

        mapper.Map(request, entity);
        entity.DateOfBirth = TimeHelper.ToLocalize(request.DateOfBirth);

        repository.Update(entity);
        return await repository.SaveAsync();
    }
}