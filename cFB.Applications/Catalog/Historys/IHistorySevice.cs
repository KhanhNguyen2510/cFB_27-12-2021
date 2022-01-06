using cFB.Data.Enums;
using cFB.ViewModels.Catalog.Historys;
using cFB.ViewModels.Common;
using System.Threading.Tasks;

namespace cFB.Applications.Catalog.Historys
{
    public interface IHistorySevice
    {
        //Create
        Task CreateInHistory(string AdministrativeDivisionId, Event Event, string StatusHistory);
        //List
        Task<PagedResult<GetHistoryViewModel>> GetAllHistory(GetManagerHistoryRequest request);
        Task<PagedResult<GetHistoryClientViewModel>> GetAllHistoryClient(GetManagerHistoryClientRequest request);
    }
}
