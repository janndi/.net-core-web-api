using Domain.Models.DTO;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Queries
{
    public class GetSingleUserQuery : IRequest<UserDTO>
    {
        [Required]
        public Guid Id { get; private set; }

        public GetSingleUserQuery(Guid id)
        {
            Id = id;
        }
    }
}
