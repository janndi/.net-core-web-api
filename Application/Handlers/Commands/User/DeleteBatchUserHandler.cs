using Application.Exceptions;
using Application.Requests.Commands;
using Domain.Models.Enums;
using Domain.ResultTypes;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.Repositories.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers
{
    public class DeleteBatchUserHandler : IRequestHandler<DeleteBatchUserCommand, BooleanResult>
    {
        private readonly IUserRepository _userRepository;

        public DeleteBatchUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BooleanResult> Handle(DeleteBatchUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<User> users = new List<User>();

                foreach (Guid id in request.Ids)
                {
                    var oUser = await _userRepository.GetByIdAsync(id);

                    if (oUser != null)
                    {
                        oUser.Status = (int)Status.Deleted;
                        users.Add(oUser);
                    }
                }

                _userRepository.UpdateRange(users);
                await _userRepository.SaveChangesAsync();

                return new BooleanResult
                {
                    Result = true,
                    Message = "Users successfully deleted."
                };
            }
            catch (Exception ex)
            {
                throw new ApiException(ErrorCodes.GeneralException, ex.Message.ToString());
            }
        }
    }
}
