using AutoMapper;
using Domain.Models.DTO;
using Infrastructure.Persistence.Repositories.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Commands
{
    public class UpdateUserCommand : IRequest<UserDTO>
    {
        public UpdateUserCommand()
        {
        }
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
        public bool Admin { get; set; }
    }
}
