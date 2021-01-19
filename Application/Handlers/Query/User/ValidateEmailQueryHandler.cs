using Application.Exceptions;
using Application.Requests.Queries;
using Domain.Models.Enums;
using Domain.ResultTypes;
using Infrastructure.Persistence.Interface;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.QueryHandlers
{
    public class ValidateEmailQueryHandler : IRequestHandler<ValidateEmailQuery, BooleanResult>
    {
        private readonly IUserRepository userRepository;

        public ValidateEmailQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<BooleanResult> Handle(ValidateEmailQuery request, CancellationToken cancellationToken)
        {
            var email = (await userRepository.GetAllAsync(x => x.Email == request.Email)).FirstOrDefault();

            if (email != null)
                throw new ApiException(ErrorCodes.BadRequest, "Email Address still in use.");

            return new BooleanResult
            {
                Result = true,
                Message = $"Email address is available."
            };
        }
    }
}
