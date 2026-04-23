using FluentValidation.Results;
using Ecommerce.Common;

namespace Ecommerce.BLL 
{
    public interface IErrorMapper
    {
        Dictionary<string, List<Errors>> MapError(ValidationResult validationResult);
    }
}
