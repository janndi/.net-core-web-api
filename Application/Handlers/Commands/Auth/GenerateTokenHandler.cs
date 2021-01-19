using Application.Exceptions;
using Application.Requests.Commands;
using Domain.Models.Enums;
using Domain.ResultTypes;
using Infrastructure.Persistence.Interface;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers
{
    public class GenerateTokenHandler : IRequestHandler<GenerateTokenCommand, IS4ApiResponse>
    {
        private readonly IUserRepository _userRepository;

        public GenerateTokenHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IS4ApiResponse> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var user = (await _userRepository.GetAllAsync(x => x.Email == request.UserName && x.Password == request.Password)).FirstOrDefault();

            if (user == null)
                throw new ApiException(ErrorCodes.BadRequest, "User does not exist.");

            if (user.Status == (int)Status.Inactive)
                throw new ApiException(ErrorCodes.BadRequest, "Your account is In-Active. Please contact administrator");


            try
            {
                IS4ApiResponse identityResponse = await _userRepository.GenerateToken(request.UserName, request.Password);

                return identityResponse;
            }
            catch (Exception ex)
            {
                throw new ApiException(ErrorCodes.GeneralException, ex.Message.ToString());
            }
        }
    }
}
