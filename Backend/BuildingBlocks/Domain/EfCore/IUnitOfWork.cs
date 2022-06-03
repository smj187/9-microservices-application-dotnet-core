namespace BuildingBlocks.Domain.EfCore
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        void Dispose();
    }
}
