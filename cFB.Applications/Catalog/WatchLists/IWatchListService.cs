using cFB.ViewModels.Catalog.WatchLists;
using cFB.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cFB.Applications.Catalog.WatchLists
{
    public interface IWatchListService
    {
        //Check
        Task<bool> CheckExistInWatchListByID(string faceBookId, string administrativeDivision_Id);
        Task<bool> CheckExistInWatchList(string faceBookUrl, string administrativeDivision_Id);
        //Count
        Task<int> GetCountWatchList(string administrativeDivision_Id);
        //Create
        Task<bool> AddNewOrUpdateWatchList(GetWatchListCreateRequest request);
        //Update
        Task<bool> UpdateToWatchList(string FaceBookID, string FaceBookName, string FaceBookTypeId);
        Task<bool> Unfollow(string facebookId);
        Task<bool> Follow(string faceBookId);
        //List
        Task<GetWatchListViewModel> GetWatchListItemById(string faceBookId);
        Task<List<GetWatchListInPython>> GetAllWatchList(GetWatchListInPython request);
        Task<PagedResult<GetWatchListViewModel>> GetAllWatchListPagedResult(GetManageWatchListRequest request);
    }
}
