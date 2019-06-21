using DAL.Interfaces;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private ICommentRepository _commentRepository;
        private IGameRepository _gameRepository;
        private IGenreRepository _genreRepository;
        private IPlatformRepository _platformRepository;
        private IPublisherRepository _publisherRepository;
        private ISubGenreRepository _subGenreRepository;
        private IOrderRepository _orderRepository;
        private readonly IAppContext _appContext;

        public UnitOfWork(IAppContext context)
        {
            _appContext = context;
        }

        public ICommentRepository CommentRepository =>
            _commentRepository ?? (_commentRepository = new CommentRepository(_appContext));
        public ISubGenreRepository SubGenreRepository =>
            _subGenreRepository ?? (_subGenreRepository = new SubGenresRepository(_appContext));
        public IGameRepository GameRepository => _gameRepository ?? (_gameRepository = new GameRepository(_appContext));
        public IGenreRepository GenreRepository => _genreRepository ?? (_genreRepository = new GenreRepository(_appContext));
        public IPlatformRepository PlatformRepository => _platformRepository ?? (_platformRepository = new PlatformTypeRepository(_appContext));
        public IPublisherRepository PublisherRepository => _publisherRepository ?? (_publisherRepository = new PublisherRepository(_appContext));
        public IOrderRepository OrderRepository =>
            _orderRepository ?? (_orderRepository = new OrderRepository(_appContext));

        public async Task Commit()
        {
            await _appContext.SaveChangesAsync();
        }
    }
}
