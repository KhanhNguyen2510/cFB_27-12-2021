using cFB.ViewModels.Catalog.Historys;
using cFB.ViewModels.Common;
using System.Threading.Tasks;

namespace cFB.IntergrationAPI.Historys
{
    public interface IHistoryApiClient
    {
        Task<PagedResult<GetHistoryViewModel>> GetAllHistory(GetManagerHistoryRequest request);
    }
}
