using Domain.ResultTypes;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Commands
{
    public class RefreshTokenCommand : IRequest<IS4ApiResponse>
    {
        [Required]
        public string RefreshToken { get; set; }

        public RefreshTokenCommand()
        {
        }
    }
}
