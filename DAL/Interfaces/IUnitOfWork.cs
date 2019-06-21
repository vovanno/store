using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ICommentRepository CommentRepository { get; }
        IGameRepository GameRepository { get; }
        IGenreRepository GenreRepository { get; }
        IPlatformRepository PlatformRepository { get; }
        IPublisherRepository PublisherRepository { get; }
        ISubGenreRepository SubGenreRepository { get; }
        IOrderRepository OrderRepository { get; }
        Task Commit();
    }
}
