using AutoMapper;
using Application.Requests.Queries;
using Domain.Models.DTO;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Handlers.QueryHandlers
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, IEnumerable<UserDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllUserQueryHandler(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<UserDTO>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string username = _httpContextAccessor.HttpContext.User.FindFirstValue("sub");

                var user = (await _userRepository.GetAllAsync(x => x.Email == username)).FirstOrDefault();

                var users = (await _userRepository.GetAllAsync()).Select(c => _mapper.Map<User, UserDTO>(c));

                if (!user.Admin )
                    users = (await _userRepository.GetAllAsync(x => x.Email == username)).Select(c => _mapper.Map<User, UserDTO>(c));

                List<UserDTO> userDTOs = new List<UserDTO>();

                foreach (var data in users)
                    userDTOs.Add(data);

                return await Task.FromResult(userDTOs);
            }
            catch (Exception ex)
            {
                throw new ApiException(ErrorCodes.GeneralException, ex.Message.ToString());
            }
        }
    }
}