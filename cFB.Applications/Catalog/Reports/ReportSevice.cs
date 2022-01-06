using cFB.Data.EFs;
using cFB.Data.Entites;
using cFB.Utilities.AutoStrings;
using cFB.Utilities.Constants;
using cFB.ViewModels.Catalog.Reports;
using cFB.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cFB.Applications.Catalog.Reports
{
    public class ReportSevice : IReportSevice
    {
        private readonly cFBDbContext _context;

        public ReportSevice(cFBDbContext context)
        {
            _context = context;
        }

        //Create
        public async Task CreateReport(GetManageReportRequest request)
        {
            try
            {
                var reports = new Report()
                {
                    ReportId = AutoGenerate.ReportRandomID(request.AdministrativeDivisionID),
                    AdministrativeDivisionId = request.AdministrativeDivisionID,
                    FileReport = !string.IsNullOrEmpty(request.FileReport) ? request.FileReport : "",
                    DateReport = DateTime.Now,
                    PostId = request.PostID
                };

                _context.Reports.Add(reports);

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return;
            }
        }
        //List
        public async Task<PagedResult<GetReportViewModel>> GetAllReport(GetReportRequest request)
        {
            try
            {
                var query = from r in _context.Reports
                            join a in _context.AdministrativeDivisions
                            on r.AdministrativeDivisionId equals a.AdministrativeDivisionId
                            orderby r.DateReport descending
                            select new
                            {
                                a.AdministrativeDivisionId,
                                a.AdministrativeDivisionName,
                                r.DateReport,
                                r.FileReport,
                                r.ReportId,
                                r.PostId
                            };

                if (!string.IsNullOrEmpty(request.AdministrativeDivisionID))
                {
                    var rs = await _context.AdministrativeDivisions.Where(x => x.AdministrativeDivisionId == request.AdministrativeDivisionID).Select(x => x.ManagerId).FirstOrDefaultAsync();
                    if (rs == null && request.AdministrativeDivisionID != ShareContants.UserAdmin)
                    {
                        return null;
                    }
                    if (rs != ShareContants.UserAdmin || request.AdministrativeDivisionID != ShareContants.UserAdmin)
                    {
                        query = query.Where(x => x.AdministrativeDivisionId == request.AdministrativeDivisionID);
                    }
                }

                if (request.DateReport != null)
                {
                    query = query.Where(x => x.DateReport == request.DateReport);
                }

                if (!string.IsNullOrEmpty(request.PostID) || !string.IsNullOrEmpty(request.ReportID))
                {
                    query = query.Where(x => x.PostId.Contains(request.PostID) || x.ReportId.Contains(request.ReportID));
                }

                int totalRow = await query.CountAsync();
    
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize).Select(x => new GetReportViewModel()
                    {
                        ReportId = x.ReportId,
                        AdministrativeDivisionName = x.AdministrativeDivisionName,
                        Date = x.DateReport,
                        DateFill = ShareContants.Checkday(x.DateReport, "history"),
                        FileReport = x.FileReport,
                        PostId = x.PostId
                    }).ToListAsync();

                var pagedResult = new PagedResult<GetReportViewModel>()
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
