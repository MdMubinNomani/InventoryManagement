using InventoryManagement.Repositories;

namespace InventoryManagement.Data
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        Task Save();

    }
}
