using Application.Requests.Commands;
using Domain.ResultTypes;
using Infrastructure.Persistence.Repositories.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers
{
    public class RevokeTokenHandler : IRequestHandler<RevokeTokenCommand, BooleanResult>
    {
        private readonly IUserRepository _userRepository;

        public RevokeTokenHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BooleanResult> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.RevokeToken(request.RefreshToken);

            return new BooleanResult
            {
                Result = true,
                Message = "Token Revoked"
            };
        }
    }
}
