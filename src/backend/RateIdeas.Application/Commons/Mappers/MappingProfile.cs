namespace RateIdeas.Application.Commons.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
        CreateMap<User, UserResultDto>();

        // Asset
        CreateMap<Asset, AssetResultDto>();
        CreateMap<UploadAssetCommand, Asset>();
    }
}