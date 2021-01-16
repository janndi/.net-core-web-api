using Domain.ResultTypes;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Commands
{
    public class RevokeTokenCommand : IRequest<BooleanResult>
    {
        [Required]
        public string RefreshToken { get; set; }

        public RevokeTokenCommand()
        {
        }
    }
}
