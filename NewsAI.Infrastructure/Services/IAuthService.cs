using NewsAI.Core.Entities;
using NewsAI.Core.Models.Auth.DTOs;

namespace NewsAI.Infrastructure.Services
{
    public interface IAuthService: ICommonService<User, CreateUserDto, >
    {
        
    }
}