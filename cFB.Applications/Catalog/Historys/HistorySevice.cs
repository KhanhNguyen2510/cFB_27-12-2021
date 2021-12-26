using cFB.Data.EFs;
using cFB.Data.Entites;
using cFB.Data.Enums;
using cFB.Utilities.Constants;
using cFB.ViewModels.Catalog.Historys;
using cFB.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cFB.Applications.Catalog.Historys
{
    public class HistorySevice : IHistorySevice
    {
        private readonly cFBDbContext _context;
        public HistorySevice(cFBDbContext context)
        {
            _context = context;
        }
        //Create
        public async Task CreateInHistory(string AdministrativeDivisionId, Event Event, string StatusHistory)
        {
            try
            {
                var history = new History()
                {
                    AdministrativeDivisionId = AdministrativeDivisionId,
                    Time = DateTime.Now,
                    Event = Event,
                    StatusHistory = StatusHistory
                };

                _context.Histories.Add(history);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return;
            }

        }
        //List
        public async Task<PagedResult<GetHistoryViewModel>> GetAllHistory(GetManagerHistoryRequest request)
        {
            try
            {
                var query = from h in _context.Histories
                            join ad in _context.AdministrativeDivisions
                            on h.AdministrativeDivisionId equals ad.AdministrativeDivisionId
                            orderby h.Time descending
                            select new
                            {
                                h.AdministrativeDivisionId,
                                ad.AdministrativeDivisionName,
                                h.Time,
                                h.Event,
                                h.StatusHistory
                            };

                if (!string.IsNullOrEmpty(request.AdministrativeDivision_Id))
                {
                    if (!string.IsNullOrEmpty(request.AdministrativeDivision_Id))
                    {
                        var rs = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == request.AdministrativeDivision_Id).Select(x => x.ManagerId).FirstOrDefaultAsync();
                        if (rs == null && request.AdministrativeDivision_Id != ShareContants.UserAdmin)
                        {
                            return null;
                        }
                        if (rs != ShareContants.UserAdmin || request.AdministrativeDivision_Id != ShareContants.UserAdmin)
                        {
                            query = query.Where(x => x.AdministrativeDivisionId == request.AdministrativeDivision_Id);
                        }
                    }
                }

                if ((request.StartDate != null || request.EndDate != null))
                    query = query.Where(x => x.Time.Date >= request.StartDate.Value.Date && x.Time.Date <= request.EndDate.Value.Date);

                if (request.Event != null)
                    query = query.Where(x => x.Event == request.Event);

                int totalRow = await query.CountAsync();
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize).Select(x => new GetHistoryViewModel()
                    {
                        AdministrativeDivision_Id = x.AdministrativeDivisionId,
                        AdministrativeDivision_Name = x.AdministrativeDivisionName,
                        Time = x.Time,
                        Event = x.Event,
                        StatusHistory = x.StatusHistory
                    }).ToListAsync();

                var pagedResult = new PagedResult<GetHistoryViewModel>()
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
