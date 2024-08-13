using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RateIdeas.Application.Auths.Commands.LogIn;

public record LogInCommand : IRequest<UserResponseDto>
{
    public LogInCommand(LogInCommand command)
    {
        Password = command.Password;
        EmailOrUserName = command.EmailOrUserName;
    }

    public string EmailOrUserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LogInCommandHandler(IMapper mapper,
    IRepository<User> userRepository,
    IConfiguration configuration) : IRequestHandler<LogInCommand, UserResponseDto>
{
    public async Task<UserResponseDto> Handle(LogInCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.SelectAsync(u => u.Email.Equals(request.EmailOrUserName)
            || u.UserName.Equals(request.EmailOrUserName))
            ?? throw new AuthorizationException("Email/UserName or password is invalid");

        bool verifiedPassword = SecurityHelper.Verify(user.PasswordHash, request.Password);

        if (!verifiedPassword)
            throw new AuthorizationException("Email/UserName or password is invalid");

        var mapped = mapper.Map<UserResponseDto>(user);
        mapped.Token = GenerateToken(user);

        return mapped;
    }

    private string GenerateToken(User user)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                 new("Id", user.Id.ToString()),
                 new(ClaimTypes.Email, user.Email),
                 new(ClaimTypes.Name, user.FirstName),
                 new(ClaimTypes.Surname, user.LastName),
                 new(ClaimTypes.Role, user.Role.ToString()),
            }),

            Expires = DateTime.UtcNow.AddHours(5),

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:Key"]!)),
                SecurityAlgorithms.HmacSha256Signature),
        };

        JwtSecurityTokenHandler tokenHandler = new();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}