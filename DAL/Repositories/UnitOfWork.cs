using DAL.Interfaces;
using System.Threading.Tasks;
using DAL.Context;

namespace DAL.Repositories
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private ICommentRepository _commentRepository;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IManufacturerRepository _manufacturerRepository;
        private IOrderRepository _orderRepository;
        private readonly StoreContext _storeContext;

        public UnitOfWork(StoreContext context)
        {
            _storeContext = context;
        }

        public ICommentRepository CommentRepository =>
            _commentRepository ?? (_commentRepository = new CommentRepository(_storeContext));
        public IProductRepository ProductRepository => _productRepository ?? (_productRepository = new ProductRepository(_storeContext));
        public ICategoryRepository CategoryRepository => _categoryRepository ?? (_categoryRepository = new CategoryRepository(_storeContext));
        public IManufacturerRepository ManufacturerRepository => _manufacturerRepository ?? (_manufacturerRepository = new ManufacturerRepository(_storeContext));
        public IOrderRepository OrderRepository =>
            _orderRepository ?? (_orderRepository = new OrderRepository(_storeContext));

        public async Task Commit()
        {
            await _storeContext.SaveChangesAsync();
        }
    }
}
