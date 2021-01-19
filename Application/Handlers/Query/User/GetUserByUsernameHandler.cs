using Application.Exceptions;
using Application.Requests.Queries;
using AutoMapper;
using Domain.Models.DTO;
using Domain.Models.Enums;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.QueryHandlers
{
    public class GetUserByUsernameHandler : IRequestHandler<GetUserByUsernameQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByUsernameHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                UserDTO userDTO = _mapper.Map<User, UserDTO>(await _userRepository.GetByIdAsync(x => x.Email == request.Username));

                return userDTO;
            }
            catch (Exception ex)
            {
                throw new ApiException(ErrorCodes.GeneralException, ex.Message.ToString());
            }
        }
    }
}
