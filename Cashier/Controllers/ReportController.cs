using InfrastructureSql.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cashier.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReportRepository _reportRepository;

        public ReportController(ILogger<ReportController> logger, 
            IReportRepository reportRepository)
        {
            _logger = logger;
            _reportRepository = reportRepository;
        }

        
        public IActionResult Index()
        {
            _logger.LogDebug("ReportController.Index");
            return View();
        }

        public async Task<IActionResult> GetReportData(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                _logger.LogDebug($"ReportController.GetReportData dateFrom={dateFrom} dateTo={dateTo}");

                if (dateFrom > dateTo)
                {
                    //return validation error
                }
                
                var reportData = await _reportRepository.GetOrdersReport(
                        User.Identity.Name,
                        dateFrom,
                        dateTo
                    );
                return Json(reportData);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ReportController.GetReportData", ex);
                throw;
            }

        }
    }
}
