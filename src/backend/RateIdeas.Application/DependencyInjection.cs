namespace RateIdeas.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        PathHelper.WebRootPath = Path.GetFullPath("wwwroot");

        services.AddAutoMapper(typeof(MappingProfile));
        services.AddHttpContextAccessor();


        // User
        services.AddScoped<IRequestHandler<CreateUserCommand, IdeaResultDto>, CreateUserCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateUserCommand, IdeaResultDto>, UpdateUserCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteUserCommand, bool>, DeleteUserCommandHandler>();

        services.AddScoped<IRequestHandler<GetUserQuery, IdeaResultDto>, GetUserQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllUsersQuery, IEnumerable<IdeaResultDto>>, GetAllUsersQueryHandler>();


        // Asset
        services.AddScoped<IRequestHandler<UploadAssetCommand, AssetResultDto>, UploadAssetCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteAssetCommand, bool>, DeleteAssetCommandHendler>();



        return services;
    }
}