using Application.Exceptions;
using Application.Requests.Commands;
using Domain.Models.Enums;
using Domain.ResultTypes;
using Infrastructure.Persistence.Repositories.Interface;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, BooleanResult>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BooleanResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.Id);
                user.Status = (int)Status.Deleted;

                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();

                return new BooleanResult
                {
                    Result = true,
                    Message = $"User: {request.Id} successfully deleted."
                };
            }
            catch (Exception ex)
            {
                throw new ApiException(ErrorCodes.GeneralException, ex.Message.ToString());
            }
        }
    }
}
