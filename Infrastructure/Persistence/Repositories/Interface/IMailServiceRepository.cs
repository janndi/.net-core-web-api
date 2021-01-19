using Domain.Models.DTO;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Interface
{
    public interface IMailServiceRepository
    {
        Task<bool> SendAsync(EmailDTO emailDTO);
    }
}
