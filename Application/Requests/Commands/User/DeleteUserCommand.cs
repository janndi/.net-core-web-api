using Domain.ResultTypes;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Commands
{
    public class DeleteUserCommand : IRequest<BooleanResult>
    {
        [Required]
        public Guid Id { get; set; }
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
