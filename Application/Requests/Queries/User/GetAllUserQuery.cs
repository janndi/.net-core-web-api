using Domain.Models.DTO;
using MediatR;
using System.Collections.Generic;

namespace Application.Requests.Queries
{
    public class GetAllUserQuery : IRequest<IEnumerable<UserDTO>>
    {
        public GetAllUserQuery()
        {
        }
    }
}