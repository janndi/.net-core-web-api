using Application.Exceptions;
using Application.Requests.Commands;
using AutoMapper;
using Domain.Models.DTO;
using Domain.Models.Enums;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.Repositories.Interface;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = _userRepository.GetById(request.Id);
                user.FirstName = request.Firstname;
                user.LastName = request.Lastname;
                user.Email = request.Email;
                user.Password = request.Password;
                user.Status = request.Status;
                user.Admin = request.Admin;

                _userRepository.Update(user);

                await _userRepository.SaveChangesAsync();

                UserDTO userDTO = (await _userRepository.GetAllAsync(x => x.Email == user.Email)).Select(c => _mapper.Map<User, UserDTO>(c)).FirstOrDefault();

                return userDTO;
            }
            catch (Exception ex)
            {
                throw new ApiException(ErrorCodes.GeneralException, ex.Message.ToString());
            }
        }
    }
}
