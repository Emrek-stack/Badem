namespace Bade.Lib.Configuration
{
    public interface IConfigReader
    {
        int ApplicationId { get; }

        string DefaultConnectionString { get; }

    }
}