using Domain.Models.DTO;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Queries
{
    public class GetUserByUsernameQuery : IRequest<UserDTO>
    {
        [Required]
        public string Username { get; private set; }

        public GetUserByUsernameQuery(string username)
        {
            Username = username;
        }
    }
}
