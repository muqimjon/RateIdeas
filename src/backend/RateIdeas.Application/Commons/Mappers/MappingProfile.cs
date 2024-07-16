namespace RateIdeas.Application.Commons.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
        CreateMap<User, UserResultDto>();

        // Idea
        CreateMap<CreateIdeaCommand, Idea>();
        CreateMap<UpdateIdeaCommand, Idea>();
        CreateMap<Idea, IdeaResultDto>();

        // Category
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<UpdateCategoryCommand, Category>();
        CreateMap<Category, CategoryResultDto>();

        // Saved idea
        CreateMap<CreateSavedIdeaCommand, SavedIdea>();
        CreateMap<UpdateSavedIdeaCommand, SavedIdea>();
        CreateMap<SavedIdea, SavedIdeaResultDto>();

        // Idea vote
        CreateMap<CreateIdeaVoteCommand, IdeaVote>();
        CreateMap<UpdateIdeaVoteCommand, IdeaVote>();
        CreateMap<IdeaVote, IdeaVoteResultDto>();

        // Asset
        CreateMap<Asset, AssetResultDto>();
        CreateMap<UploadAssetCommand, Asset>();
    }
}