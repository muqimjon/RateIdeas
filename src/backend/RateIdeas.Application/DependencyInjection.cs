using RateIdeas.Application.Assets.Commands;
using RateIdeas.Application.Assets.DTOs;

namespace RateIdeas.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        PathHelper.WebRootPath = Path.GetFullPath("wwwroot");

        services.AddAutoMapper(typeof(MappingProfile));
        services.AddHttpContextAccessor();


        // User
        services.AddScoped<IRequestHandler<CreateUserCommand, UserResultDto>, CreateUserCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateUserCommand, UserResultDto>, UpdateUserCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteUserCommand, bool>, DeleteUserCommandHandler>();

        services.AddScoped<IRequestHandler<GetUserQuery, UserResultDto>, GetUserQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllUsersQuery, IEnumerable<UserResultDto>>, GetAllUsersQueryHandler>();


        // Asset
        services.AddScoped<IRequestHandler<UploadAssetCommand, AssetResultDto>, UploadAssetCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteAssetCommand, bool>, DeleteAssetCommandHendler>();



        return services;
    }
}