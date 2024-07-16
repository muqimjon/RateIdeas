using RateIdeas.Application.Categories.Commands;
using RateIdeas.Application.Categories.Queries;
using RateIdeas.Application.Ideas.Commands;
using RateIdeas.Application.Ideas.Queries;

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


        // Category
        services.AddScoped<IRequestHandler<CreateCategoryCommand, CategoryResultDto>, CreateCategoryCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateCategoryCommand, CategoryResultDto>, UpdateCategoryCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteCategoryCommand, bool>, DeleteCategoryCommandHandler>();

        services.AddScoped<IRequestHandler<GetCategoryQuery, CategoryResultDto>, GetCategoryQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResultDto>>, GetAllCategoriesQueryHandler>();


        // Idea
        services.AddScoped<IRequestHandler<CreateIdeaCommand, IdeaResultDto>, CreateIdeaCommandHandler>();
        
        services.AddScoped<IRequestHandler<UpdateIdeaCommand, IdeaResultDto>,  UpdateIdeaCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteIdeaCommand, bool>, DeleteIdeaCommandHandler>();

        services.AddScoped<IRequestHandler<GetIdeaQuery, IdeaResultDto>,  GetIdeaQueryHandler>();
        services.AddScoped < IRequestHandler<GetAllIdeasQuery, IEnumerable<IdeaResultDto>>, GetAllIdeasQueryHandler>();


        // Asset
        services.AddScoped<IRequestHandler<UploadAssetCommand, AssetResultDto>, UploadAssetCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteAssetCommand, bool>, DeleteAssetCommandHendler>();



        return services;
    }
}