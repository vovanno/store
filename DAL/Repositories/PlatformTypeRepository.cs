using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    internal sealed class PlatformTypeRepository : GenericRepository<PlatformType>, IPlatformRepository
    {
        public PlatformTypeRepository(IAppContext context) : base(context)
        {

        }
    }
}
