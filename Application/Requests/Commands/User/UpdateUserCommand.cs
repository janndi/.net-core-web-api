using Domain.Models.DTO;
using MediatR;
using System;

namespace Application.Requests.Commands
{
    public class UpdateUserCommand : IRequest<UserDTO>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
        public bool Admin { get; set; }
        public UpdateUserCommand()
        {
        }
    }
}
