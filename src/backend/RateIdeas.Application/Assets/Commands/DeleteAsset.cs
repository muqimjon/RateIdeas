namespace RateIdeas.Application.Assets.Commands;

public record DeleteAssetCommand : IRequest<bool>
{
    public DeleteAssetCommand(long? id)
    {
        Id = id;
    }
    public long? Id { get; set; }
}

public class DeleteAssetCommandHendler(IRepository<Asset> repository)
    : IRequestHandler<DeleteAssetCommand, bool>
{
    public async Task<bool> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id));

        if (entity is null)
            return false;

        string filePath = Path.Combine(PathHelper.WebRootPath, "Images", entity.FileName);

        if (!File.Exists(filePath))
            File.Delete(filePath);

        repository.Delete(entity);
        await repository.SaveAsync();

        return true;
    }
}
