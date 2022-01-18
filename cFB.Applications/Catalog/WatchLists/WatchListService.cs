using cFB.Applications.Catalog.Historys;
using cFB.Data.EFs;
using cFB.Data.Entites;
using cFB.Data.Enums;
using cFB.Utilities.AutoStrings;
using cFB.Utilities.Constants;
using cFB.ViewModels.Catalog.WatchLists;
using cFB.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cFB.Applications.Catalog.WatchLists
{
    public class WatchListService : IWatchListService
    {
        private readonly cFBDbContext _context;
        private readonly IHistorySevice _historySevice;
        public WatchListService(cFBDbContext context, IHistorySevice historySevice)
        {
            _context = context;
            _historySevice = historySevice;
        }
        //Check
        public async Task<bool> CheckExistInWatchList(string faceBookUrl, string administrativeDivision_Id)
        {
            try
            {
                var watchlist = await _context.WatchLists.FirstOrDefaultAsync(x => x.FaceBookUrl == faceBookUrl.TrimEnd('/') && x.AdministrativeDivisionId == administrativeDivision_Id);
                if (watchlist == null) return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> CheckExistInWatchListByID(string faceBookId, string administrativeDivision_Id)
        {
            try
            {
                var watchlist = await _context.WatchLists.FirstOrDefaultAsync(x => x.FaceBookId == faceBookId && x.AdministrativeDivisionId == administrativeDivision_Id);
                if (watchlist == null) return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Count
        public async Task<int> GetCountWatchList(string administrativeDivision_Id)
        {
            try
            {
                var data = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == administrativeDivision_Id).Select(x => x.ManagerId).FirstOrDefaultAsync();
                if (data == null && administrativeDivision_Id != ShareContants.UserAdmin)
                {
                    return 0;
                }
                else if (data == ShareContants.UserAdmin || administrativeDivision_Id == ShareContants.UserAdmin)
                {
                    return await _context.WatchLists.Where(x => x.Status == Status.Activate).CountAsync();
                }
                else
                {
                    return await _context.WatchLists.Where(x => x.Status == Status.Activate && x.AdministrativeDivisionId == administrativeDivision_Id).CountAsync();
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }
        //Create
        public async Task<bool> CreateToWatchList(GetWatchListCreateRequest request)
        {
            try
            {
                var watchList = new WatchList()
                {
                    FaceBookId = (request.FaceBookID != null) ? request.FaceBookID : AutoGenerate.WatchListRandomId(request.FaceBookUrl, request.AdministrativeDivisionID), // Nếu người dùng không tạo thì máy sẽ tự setup cho bạn
                    FaceBookName = (request.FaceBookName != null) ? request.FaceBookName : "",
                    FaceBookUrl = request.FaceBookUrl.TrimEnd('/'),
                    Status = (Status)(request.Status == null ? Status.Activate : request.Status),
                    AdministrativeDivisionId = request.AdministrativeDivisionID,
                    FaceBookTypeId = request.FaceBookTypeID
                };
                _context.WatchLists.Add(watchList);
                await _context.SaveChangesAsync();
                await _historySevice.CreateInHistory(request.AdministrativeDivisionID, Event.Create, $"Thêm vào đối tượng {request.FaceBookUrl} ");
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        //Update
        public async Task<bool> UpdateToWatchList(string FaceBookID, string FaceBookName, string FaceBookTypeId)
        {
            try
            {
                var watchlist = await _context.WatchLists.FirstOrDefaultAsync(x => x.FaceBookId == FaceBookID);

                if (watchlist == null) return false;

                watchlist.FaceBookName = (FaceBookName != null) ? FaceBookName : watchlist.FaceBookName;
                watchlist.FaceBookTypeId = (FaceBookTypeId != null) ? FaceBookTypeId : watchlist.FaceBookTypeId;

                await _context.SaveChangesAsync();
                await _historySevice.CreateInHistory(watchlist.AdministrativeDivisionId, Event.Update, $"Cập nhật thông tin theo dõi {watchlist.FaceBookUrl}");
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> AddNewOrUpdateWatchList(GetWatchListCreateRequest request)
        {
            //step 1: check ID exits in Post -> then update / add new
            //step 2: if add new -> check FacebookID exits in WatchList -> if not then add facebookID before add post
            try
            {
                if (await CheckExistInWatchList(request.FaceBookUrl, request.AdministrativeDivisionID))
                {
                    var data = await _context.WatchLists.Where(x => x.FaceBookUrl == request.FaceBookUrl && x.AdministrativeDivisionId == request.AdministrativeDivisionID).FirstOrDefaultAsync();
                    var update = await UpdateToWatchList(data.FaceBookId, request.FaceBookName, request.FaceBookTypeID);

                    if (update == true)
                        return true;
                }
                else
                {
                    var data = await CreateToWatchList(request);

                    if (data == true)
                        return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Follow(string faceBookId)
        {
            try
            {
                var watchlist = await _context.WatchLists.FirstOrDefaultAsync(x => x.FaceBookId == faceBookId);

                if (watchlist == null) return false;

                watchlist.Status = Status.Activate;

                await _context.SaveChangesAsync();

                await _historySevice.CreateInHistory(watchlist.AdministrativeDivisionId, Event.Update, $"Bật theo dõi {watchlist.FaceBookUrl}");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Unfollow(string facebookId)
        {
            try
            {
                var watchlist = await _context.WatchLists.FirstOrDefaultAsync(x => x.FaceBookId == facebookId);

                if (watchlist == null) return false;

                watchlist.Status = Status.Inactive;
                await _context.SaveChangesAsync();

                await _historySevice.CreateInHistory(watchlist.AdministrativeDivisionId, Event.Update, $"Tắt theo dõi {watchlist.FaceBookUrl}");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //List
        public async Task<GetWatchListViewModel> GetWatchListItemById(string faceBookId)
        {
            try
            {
                var query = await (from wl in _context.WatchLists
                                   join ft in _context.FaceBookTypes
                                   on wl.FaceBookTypeId equals ft.FaceBookTypeId
                                   join ad in _context.AdministrativeDivisions
                                   on wl.AdministrativeDivisionId equals ad.AdministrativeDivisionId
                                   where wl.FaceBookId == faceBookId
                                   select new
                                   {
                                       wl.FaceBookId,
                                       wl.FaceBookName,
                                       wl.FaceBookUrl,
                                       wl.FaceBookTypeId,
                                       ft.FaceBookTypeName,
                                       wl.Status,
                                       wl.AdministrativeDivisionId,
                                       ad.AdministrativeDivisionName
                                   }).FirstOrDefaultAsync();

                if (query == null) return null;

                var watchListViewModel = new GetWatchListViewModel()
                {
                    FaceBookID = query.FaceBookId,
                    FaceBookName = query.FaceBookName,
                    FaceBookUrl = query.FaceBookUrl,
                    FaceBookTypeID = query.FaceBookTypeId,
                    FaceBookTypeName = query.FaceBookTypeName,
                    Status = query.Status,
                    AdministrativeDivisionID = query.AdministrativeDivisionId,
                    AdministrativeDivisionName = query.AdministrativeDivisionName
                };
                return watchListViewModel;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<GetWatchListInPython>> GetAllWatchList(GetWatchListInPython request)  /// load các thông tin đã được cấp phép  
        {
            try
            {
                var query = from wl in _context.WatchLists
                            join ft in _context.FaceBookTypes
                            on wl.FaceBookTypeId equals ft.FaceBookTypeId
                            where wl.Status == Status.Activate
                            select new
                            {
                                wl.FaceBookId,
                                wl.FaceBookName,
                                wl.FaceBookUrl,
                                ft.FaceBookTypeId,
                                wl.AdministrativeDivisionId,
                                wl.Status
                            };
                if (query == null) return null;

                if (!string.IsNullOrEmpty(request.FaceBookTypeID))
                    query = query.Where(x => x.FaceBookTypeId == request.FaceBookTypeID);

                if (request.Status != null)
                    query = query.Where(x => x.Status == request.Status);

                if (!string.IsNullOrEmpty(request.AdministrativeDivisionID))
                {
                    var rs = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == request.AdministrativeDivisionID).Select(x => x.ManagerId).FirstOrDefaultAsync();
                    if (rs == null && request.AdministrativeDivisionID != ShareContants.UserAdmin)
                    {
                        return null;
                    }
                    if (rs != ShareContants.UserAdmin)
                    {
                        query = query.Where(x => x.AdministrativeDivisionId == request.AdministrativeDivisionID);
                    }
                }

                var result = await query.Select(x => new GetWatchListInPython()
                {
                    AdministrativeDivisionID = x.AdministrativeDivisionId,
                    FaceBookID = x.FaceBookId,
                    FaceBookTypeID = x.FaceBookTypeId,
                    FaceBookName = x.FaceBookName,
                    FaceBookUrl = x.FaceBookUrl,
                    Status = x.Status
                }).ToListAsync();

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<PagedResult<GetWatchListViewModel>> GetAllWatchListPagedResult(GetManageWatchListRequest request)
        {
            try
            {
                var query = from wl in _context.WatchLists
                            join ft in _context.FaceBookTypes
                            on wl.FaceBookTypeId equals ft.FaceBookTypeId
                            join ad in _context.AdministrativeDivisions
                            on wl.AdministrativeDivisionId equals ad.AdministrativeDivisionId
                            select new
                            {
                                wl.FaceBookId,
                                wl.FaceBookName,
                                wl.FaceBookUrl,
                                wl.FaceBookTypeId,
                                ft.FaceBookTypeName,
                                wl.Status,
                                wl.AdministrativeDivisionId,
                                ad.AdministrativeDivisionName
                            };

                if (!string.IsNullOrEmpty(request.FacebookTypeID))
                    query = query.Where(x => x.FaceBookTypeId == request.FacebookTypeID);

                if (request.Status != null)
                    query = query.Where(x => x.Status == request.Status);

                if (!string.IsNullOrEmpty(request.AdministrativeDivisionID))
                {
                    var rs = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == request.AdministrativeDivisionID).Select(x => x.ManagerId).FirstOrDefaultAsync();
                    if (rs == null && request.AdministrativeDivisionID != ShareContants.UserAdmin)
                    {
                        return null;
                    }
                    if (rs != ShareContants.UserAdmin)
                    {
                        query = query.Where(x => x.AdministrativeDivisionId == request.AdministrativeDivisionID);
                    }
                }

                int totalRow = await query.CountAsync();

                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
               .Take(request.PageSize).Select(x => new GetWatchListViewModel()
               {
                   FaceBookID = x.FaceBookId,
                   FaceBookName = x.FaceBookName,
                   FaceBookUrl = x.FaceBookUrl,
                   FaceBookTypeID = x.FaceBookTypeId,
                   FaceBookTypeName = x.FaceBookTypeName,
                   Status = x.Status,
                   AdministrativeDivisionID = x.AdministrativeDivisionId,
                   AdministrativeDivisionName = x.AdministrativeDivisionName
               }).ToListAsync();
                var pagedResult = new PagedResult<GetWatchListViewModel>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
                return pagedResult;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
