using Application.Requests.Commands;
using Domain.ResultTypes;
using Infrastructure.Persistence.Repositories.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, IS4ApiResponse>
    {
        private readonly IUserRepository _userRepository;

        public RefreshTokenHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IS4ApiResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            IS4ApiResponse identityResponse = await _userRepository.RefreshToken(request.RefreshToken);

            return identityResponse;
        }
    }
}
