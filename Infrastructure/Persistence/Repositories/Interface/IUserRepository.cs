using Domain.ResultTypes;
using Domain.SeedWork;
using Infrastructure.Persistence.Entities;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IS4ApiResponse> GenerateToken(string username, string password);

        Task<IS4ApiResponse> RefreshToken(string refreshToken);

        Task RevokeToken(string refreshToken);
    }
}
