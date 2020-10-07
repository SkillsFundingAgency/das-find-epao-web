namespace SFA.DAS.FindEpao.Domain.Interfaces
{
    public interface IGetApiRequest
    {
        string BuildGetUrl(string baseUrl);
    }
}
