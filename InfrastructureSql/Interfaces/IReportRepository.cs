using Entities.Reports;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureSql.Interfaces
{
    public interface IReportRepository
    {
        public Task<IEnumerable<OrdersReport>> GetOrdersReport(string modifiedBy, DateTime? dateFrom , DateTime? dateTo);
    }
}
