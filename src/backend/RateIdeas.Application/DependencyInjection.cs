using RateIdeas.Application.Users.Commands.UpdateRole;

namespace RateIdeas.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        PathHelper.WebRootPath = Path.GetFullPath("wwwroot");


        // Use metadata
        services.AddHttpContextAccessor()
            // Mapping objects
            .AddAutoMapper(typeof(MappingProfile))
            // Add MVC controllers
            .AddControllers()
            // Ignore cycle errors
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


        // User
        services.AddScoped<IRequestHandler<CreateUserCommand, UserResultDto>, CreateUserCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateUserCommand, UserResultDto>, UpdateUserCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateRoleCommand, bool>, UpdateRoleCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateUserByIdCommand, UserResultDto>, UpdateUserByIdCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteUserCommand, bool>, DeleteUserCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteUserByIdCommand, bool>, DeleteUserByIdCommandHandler>();

        services.AddScoped<IRequestHandler<GetUserQuery, UserResultDto>, GetUserQueryHandler>();
        services.AddScoped<IRequestHandler<GetUserByIdQuery, UserResultDto>, GetUserByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllUsersQuery, IEnumerable<UserResultDto>>, GetAllUsersQueryHandler>();


        // Category
        services.AddScoped<IRequestHandler<CreateCategoryCommand, CategoryResultDto>, CreateCategoryCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateCategoryCommand, CategoryResultDto>, UpdateCategoryCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteCategoryCommand, bool>, DeleteCategoryCommandHandler>();

        services.AddScoped<IRequestHandler<GetCategoryQuery, CategoryResultDto>, GetCategoryQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResultDto>>, GetAllCategoriesQueryHandler>();


        // Idea
        services.AddScoped<IRequestHandler<CreateIdeaCommand, IdeaResultDto>, CreateIdeaCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateIdeaCommand, IdeaResultDto>, UpdateIdeaCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteIdeaCommand, bool>, DeleteIdeaCommandHandler>();

        services.AddScoped<IRequestHandler<GetIdeaQuery, IdeaResultDto>, GetIdeaQueryHandler>();
        services.AddScoped<IRequestHandler<GetIdeasQuery, IEnumerable<IdeaResultDto>>, GetIdeasQueryHandler>();
        services.AddScoped<IRequestHandler<GetIdeasByUserIdQuery, IEnumerable<IdeaResultDto>>, GetIdeasByUserIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllIdeasQuery, IEnumerable<IdeaResultDto>>, GetAllIdeasQueryHandler>();


        // Saved idea
        services.AddScoped<IRequestHandler<CreateSavedIdeaCommand, SavedIdeaResultDto>, CreateSavedIdeaCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateSavedIdeaCommand, SavedIdeaResultDto>, UpdateSavedIdeaCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteSavedIdeaCommand, bool>, DeleteSavedIdeaCommandHandler>();

        services.AddScoped<IRequestHandler<GetSavedIdeaQuery, SavedIdeaResultDto>, GetSavedIdeaQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllSavedIdeasQuery, IEnumerable<SavedIdeaResultDto>>, GetAllSavedIdeasQueryHandler>();


        // Idea vote
        services.AddScoped<IRequestHandler<CreateIdeaVoteCommand, IdeaVoteResultDto>, CreateIdeaVoteCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateIdeaVoteCommand, IdeaVoteResultDto>, UpdateIdeaVoteCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteIdeaVoteCommand, bool>, DeleteIdeaVoteCommandHandler>();

        services.AddScoped<IRequestHandler<GetIdeaVoteQuery, IdeaVoteResultDto>, GetIdeaVoteQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllIdeaVotesQuery, IEnumerable<IdeaVoteResultDto>>, GetAllIdeaVotesQueryHandler>();


        // Comment
        services.AddScoped<IRequestHandler<CreateCommentCommand, CommentResultDto>, CreateCommentCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateCommentCommand, CommentResultDto>, UpdateCommentCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteCommentCommand, bool>, DeleteCommentCommandHandler>();

        services.AddScoped<IRequestHandler<GetCommentQuery, CommentResultDto>, GetCommentQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllCommentsQuery, IEnumerable<CommentResultDto>>, GetAllCommentsQueryHandler>();


        // Comment vote
        services.AddScoped<IRequestHandler<CreateCommentVoteCommand, CommentVoteResultDto>, CreateCommentVoteCommandHandler>();

        services.AddScoped<IRequestHandler<UpdateCommentVoteCommand, CommentVoteResultDto>, UpdateCommentVoteCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteCommentVoteCommand, bool>, DeleteCommentVoteCommandHandler>();

        services.AddScoped<IRequestHandler<GetCommentVoteQuery, CommentVoteResultDto>, GetCommentVoteQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllCommentVotesQuery, IEnumerable<CommentVoteResultDto>>, GetAllCommentVotesQueryHandler>();


        // Asset
        services.AddScoped<IRequestHandler<UploadAssetCommand, AssetResultDto>, UploadAssetCommandHandler>();

        services.AddScoped<IRequestHandler<DeleteAssetCommand, bool>, DeleteAssetCommandHendler>();


        // Auth
        services.AddScoped<IRequestHandler<GenerateTokenCommand, UserResponseDto>, GenerateTokenCommandHandler>();


        return services;
    }
}