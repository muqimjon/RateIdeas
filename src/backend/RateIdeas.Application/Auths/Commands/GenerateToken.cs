using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RateIdeas.Application.Ideas.Commands;

public record GenerateTokenCommand : IRequest<UserResponseDto>
{
    public GenerateTokenCommand(GenerateTokenCommand command)
    {
        Password = command.Password;
        EmailOrUserName = command.EmailOrUserName;
    }
    public string EmailOrUserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class GenerateTokenCommandHandler(IMapper mapper,
    IRepository<User> userRepository,
    IConfiguration configuration) : IRequestHandler<GenerateTokenCommand, UserResponseDto>
{
    public async Task<UserResponseDto> Handle(GenerateTokenCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.SelectAsync(u => u.Email.Equals(request.EmailOrUserName))
            ?? await userRepository.SelectAsync(u => u.UserName.Equals(request.EmailOrUserName))
            ?? throw new AuthorizationException("Email/UserName or password is invalid");

        bool verifiedPassword = SecurityHelper.Verify(user.PasswordHash, request.Password);

        if (!verifiedPassword)
            throw new AuthorizationException("Email/UserName or password is invalid");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                 new("Id", user.Id.ToString()),
                 new(ClaimTypes.Email, user.Email),
                 new(ClaimTypes.Name, user.FirstName),
                 new(ClaimTypes.Surname, user.LastName),
                 new(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:Key"]!)),
                SecurityAlgorithms.HmacSha256Signature)
        };

        JwtSecurityTokenHandler tokenHandler = new();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var mapped = mapper.Map<UserResponseDto>(user);
        mapped.Token = tokenHandler.WriteToken(token);

        return mapped;
    }
}