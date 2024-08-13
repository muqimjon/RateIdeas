using RateIdeas.Application.Auths.Commands.Register;
using RateIdeas.Application.Auths.DTOs;

namespace RateIdeas.Application.Commons.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
        CreateMap<UpdateUserByIdCommand, User>();
        CreateMap<UserRegistrationDto, User>();
        CreateMap<User, UserResultDto>();
        CreateMap<User, UserResponseDto>();
        CreateMap<User, UserResultForPropDto>();
        CreateMap<RegisterCommand, UserRegistrationDto>();

        // Idea
        CreateMap<CreateIdeaCommand, Idea>();
        CreateMap<UpdateIdeaCommand, Idea>();
        CreateMap<Idea, IdeaResultDto>();
        CreateMap<Idea, IdeaResultForPropDto>();

        // Category
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<UpdateCategoryCommand, Category>();
        CreateMap<Category, CategoryResultDto>();
        CreateMap<Category, CategoryResultForPropDto>();

        // Saved idea
        CreateMap<CreateSavedIdeaCommand, SavedIdea>();
        CreateMap<UpdateSavedIdeaCommand, SavedIdea>();
        CreateMap<SavedIdea, SavedIdeaResultDto>();

        // Idea vote
        CreateMap<ToggleIdeaVoteCommand, IdeaVote>();
        CreateMap<IdeaVote, IdeaVoteResultDto>();

        // Comment
        CreateMap<CreateCommentCommand, Comment>();
        CreateMap<UpdateCommentCommand, Comment>();
        CreateMap<Comment, CommentResultDto>();

        // Comment vote
        CreateMap<CreateCommentVoteCommand, CommentVote>();
        CreateMap<UpdateCommentVoteCommand, CommentVote>();
        CreateMap<CommentVote, CommentVoteResultDto>();

        // Asset
        CreateMap<Asset, AssetResultDto>();
        CreateMap<UploadAssetCommand, Asset>();
    }
}