using FlamboyantFnb.Domain.Entities;
using FlamboyantFnb.Domain.RequestModel;
using FlamboyantFnb.Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlamboyantFnb.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(LoginReq req);
    }
}
