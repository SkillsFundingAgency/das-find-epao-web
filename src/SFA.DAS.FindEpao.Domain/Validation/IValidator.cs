using System.Threading.Tasks;

namespace SFA.DAS.FindEpao.Domain.Validation
{
    public interface IValidator<in T>
    {
        Task<ValidationResult> ValidateAsync(T item);
    }
}
