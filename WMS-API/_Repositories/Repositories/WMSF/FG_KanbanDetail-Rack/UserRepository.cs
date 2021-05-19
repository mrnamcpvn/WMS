
using WMS_API._Repositories.Interfaces;
using WMS_API.Data;
using WMS_API.Models;

namespace WMS_API._Repositories.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {

        }
    }
}