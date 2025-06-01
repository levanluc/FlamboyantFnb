using FlamboyantFnb.Domain.Entities;
using FlamboyantFnb.Domain.Interfaces.Repository;
using FlamboyantFnb.Domain.RequestModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlamboyantFnb.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(FnbDbContext context) : base(context)
        {
        }

        public Task<User> GetByUserNameAsync(string username)
        {
            return GetAll().Where(e => e.UserName == username).FirstOrDefaultAsync();
        }
    }
}
