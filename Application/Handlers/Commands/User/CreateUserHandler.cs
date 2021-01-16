using Application.Exceptions;
using Application.Requests.Commands;
using AutoMapper;
using Domain.Models.DTO;
using Domain.Models.Enums;
using Infrastructure.Persistence.Repositories.Interface;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Persistence.Entities;

namespace Application.Handlers.CommandHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = (await _userRepository.GetAllAsync(x => x.Email == request.Email)).FirstOrDefault();

            if(user != null)
                throw new ApiException(ErrorCodes.BadRequest, "Email address has already been taken.");

            try
            {
                var newUser = new User
                {
                    FirstName = request.Firstname,
                    LastName = request.Lastname,
                    Email = request.Email,
                    Password = request.Password,
                    Status = request.Status,
                    Admin = request.Admin
                };

                _userRepository.Add(newUser);

                await _userRepository.SaveChangesAsync();

                UserDTO userDTO = (await _userRepository.GetAllAsync(x => x.Email == newUser.Email)).Select(c => _mapper.Map<User, UserDTO>(c)).FirstOrDefault();

                return userDTO;

            }
            catch(Exception ex)
            {
                throw new ApiException(ErrorCodes.GeneralException, ex.Message.ToString());
            }
        }
    }
}
