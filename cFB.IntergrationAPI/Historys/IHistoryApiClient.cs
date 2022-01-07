using cFB.ViewModels.Catalog.Historys;
using cFB.ViewModels.Common;
using System.Threading.Tasks;

namespace cFB.IntergrationAPI.Historys
{
    public interface IHistoryApiClient
    {
        Task<PagedResult<GetHistoryViewModel>> GetAllHistory(GetManagerHistoryRequest request);
        Task<PagedResult<GetHistoryClientViewModel>> GetAllHistoryClient(GetManagerHistoryClientRequest request);
        Task<bool> CreateHistoryClient(string AdministrativeDivisionID, string UserAgent, string IpAdress);
    }
}
