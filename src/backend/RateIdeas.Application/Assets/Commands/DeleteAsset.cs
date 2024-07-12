namespace RateIdeas.Application.Assets.Commands;

public record DeleteAssetCommand : IRequest<bool>
{
    public DeleteAssetCommand(long id)
    {
         Id = id;
    }
    public long Id { get; set; }
}

public class DeleteAssetCommandHendler(IRepository<Asset> repository) : IRequestHandler<DeleteAssetCommand, bool>
{
    public async Task<bool> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id.Equals(request.Id))
            ?? throw new NotFoundException($"The Asset is not found with id:{request.Id}");

        string path = Path.Combine(PathHelper.WebRootPath, "Images", entity.FileName);

        if (!File.Exists(path))
            File.Delete(path);

        repository.Delete(entity);
        await repository.SaveAsync();
        return true;
    }
}
