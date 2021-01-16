using Application.Helpers.RegexValidators;
using Domain.ResultTypes;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Commands
{
    public class GenerateTokenCommand : IRequest<IS4ApiResponse>
    {
        [Required]
        [RegularExpression(RegularExpressionType.EmailAdd, ErrorMessage = RegexCustomError.EmailAdd)]
        [StringLength(maximumLength: 100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Password { get; set; }

        public GenerateTokenCommand()
        {
        }
    }
}
