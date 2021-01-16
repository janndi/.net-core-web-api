using AutoMapper;
using Application.Requests.Queries;
using Domain.Models.DTO;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.Repositories.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Domain.Models.Enums;

namespace Application.Handlers.QueryHandlers
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, IEnumerable<UserDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = (await _userRepository.GetAllAsync()).Select(c => _mapper.Map<User, UserDTO>(c));

                List<UserDTO> userDTOs = new List<UserDTO>();

                foreach (var user in users)
                    userDTOs.Add(user);

                return await Task.FromResult(userDTOs);
            }
            catch (Exception ex)
            {
                throw new ApiException(ErrorCodes.GeneralException, ex.Message.ToString());
            }
        }
    }
}