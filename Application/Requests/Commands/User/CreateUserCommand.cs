using Domain.Models.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Commands
{
    public class CreateUserCommand : IRequest<UserDTO>
    {
        public CreateUserCommand()
        {
        }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
        public bool Admin { get; set; }

    }
}
