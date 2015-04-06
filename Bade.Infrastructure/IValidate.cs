namespace Bade.Infrastructure
{

    public interface IValidatable
    {
        bool IsValid { get; }

        ValidationErrors ValidationErrors { get; }
    }
}