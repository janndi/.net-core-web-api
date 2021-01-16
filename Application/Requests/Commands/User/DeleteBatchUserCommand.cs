using Domain.ResultTypes;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Requests.Commands
{
    public class DeleteBatchUserCommand : IRequest<BooleanResult>
    {
        public List<Guid> Ids { get; set; }

        public DeleteBatchUserCommand(List<Guid> ids)
        {
            Ids = ids;
        }
    }
}
