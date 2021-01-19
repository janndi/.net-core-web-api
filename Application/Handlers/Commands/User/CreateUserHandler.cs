using Application.Exceptions;
using Application.Requests.Commands;
using AutoMapper;
using Domain.Models.DTO;
using Domain.Models.Enums;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.Interface;

namespace Application.Handlers.CommandHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMailServiceRepository _mailServiceRepository;

        public CreateUserHandler(IUserRepository userRepository, IMapper mapper, IMailServiceRepository mailServiceRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _mailServiceRepository = mailServiceRepository;
        }

        public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = (await _userRepository.GetAllAsync(x => x.Email == request.Email && x.Status != 2)).FirstOrDefault();

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

                EmailDTO emailDTO = new EmailDTO
                {
                    To = userDTO.Email,
                    Subject = "User Creation",
                    Username = userDTO.Email 
                };

                await _mailServiceRepository.SendAsync(emailDTO);

                return userDTO;

            }
            catch(Exception ex)
            {
                throw new ApiException(ErrorCodes.GeneralException, ex.Message.ToString());
            }
        }
    }
}
