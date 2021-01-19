using Application.Helpers.RegexValidators;
using Domain.ResultTypes;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Queries
{
    public class ValidateEmailQuery : IRequest<BooleanResult>
    {
        [Required]
        [RegularExpression(RegularExpressionType.EmailAdd, ErrorMessage = RegexCustomError.EmailAdd)]
        [StringLength(maximumLength: 100)]
        public string Email { get; set; }

        public ValidateEmailQuery(string Email)
        {
            this.Email = Email;
        }
    }
}
