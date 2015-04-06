namespace Generator
{
    public interface IDbConnectionUser
    {
        IDbConnectionProvider ConnectionProvider { get; set; }
    }
}