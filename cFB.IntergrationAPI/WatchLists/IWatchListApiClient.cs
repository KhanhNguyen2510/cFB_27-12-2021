using cFB.Data.Entites;
using cFB.ViewModels.Catalog.WatchLists;
using cFB.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cFB.IntergrationAPI.WatchLists
{
    public interface IWatchListApiClient
    {
        Task<int> GetCountWatchList(string administrativeDivision_Id);
        Task<bool> AddNewOrUpdateWatchList(GetWatchListCreateRequest request);
        Task<bool> Update(string FaceBookID, string FaceBookName, string FaceBookTypeId);
        Task<List<WatchList>> GetAllWatchList(GetManageListWatchListPagingRequest request);
        Task<PagedResult<GetWatchListViewModel>> GetAllWatchListPagedResult(GetManageWatchListRequest request);
    }
}
