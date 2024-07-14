using RateIdeas.Application.Assets.DTOs;

namespace RateIdeas.Application.Assets.Commands;

public record UploadAssetCommand : IRequest<AssetResultDto>
{
    public UploadAssetCommand(IFormFile formFile)
    {
           FormFile = formFile;
    }
    public IFormFile FormFile { get; set; } = default!;
}

#nullable disable
public class UploadAssetCommandHandler(IMapper mapper,
    IRepository<Asset> repository,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<UploadAssetCommand, AssetResultDto>
{
    public async Task<AssetResultDto> Handle(UploadAssetCommand request, CancellationToken cancellationToken)
    {
        var webRootPath = Path.Combine(PathHelper.WebRootPath, "Images");

        if (!Directory.Exists(webRootPath))
            Directory.CreateDirectory(webRootPath);

        var fileExtension = Path.GetExtension(request.FormFile.FileName);
        var fileName = $"{Guid.NewGuid():N}{fileExtension}";
        var filePath = Path.Combine(webRootPath, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await request.FormFile.CopyToAsync(fileStream, cancellationToken);
        }
        var imageUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/Images/{fileName}";

        var asset = new Asset()
        {
            FileName = fileName,
            FilePath = imageUrl,
        };

        await repository.InsertAsync(asset);
        await repository.SaveAsync();
        return mapper.Map<AssetResultDto>(asset);
    }
}
