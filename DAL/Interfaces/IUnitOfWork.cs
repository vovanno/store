using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ICommentRepository CommentRepository { get; }
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IManufacturerRepository ManufacturerRepository { get; }
        IOrderRepository OrderRepository { get; }
        Task Commit();
    }
}
