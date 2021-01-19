using Domain.ResultTypes;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Requests.Commands
{
    public class DeleteBatchUserCommand : IRequest<BooleanResult>
    {
        public string Ids { get; set; }

        public DeleteBatchUserCommand(string ids)
        {
            Ids = ids;
        }
    }
}
