namespace BEService.Core.Interfaces.Services.SupportServices
{
    public interface IPasswordService
    {
        bool Check(string hash, string password);
        string Hash(string password);
    }
}
