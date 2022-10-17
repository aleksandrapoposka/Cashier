using Dapper;
using DataAccess.Data;
using Entities.Reports;
using InfrastructureSql.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureSql.Concrete
{
    public class ReportRepository : IReportRepository
    {
        private IDbConnection db;
        
        public ReportRepository(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<OrdersReport>> GetOrdersReport(string modifiedBy, DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                IEnumerable<OrdersReport> reportData = null;

                var parameters = new DynamicParameters();
                parameters.Add("ModifiedBy", modifiedBy, DbType.String);
                parameters.Add("DateFrom", dateFrom);
                parameters.Add("DateTo", dateTo);

                reportData = await db.QueryAsync<OrdersReport>("spGetOrdersReport", parameters, commandType: CommandType.StoredProcedure);
                return reportData;
            }
            catch (Exception ex)
            {

                throw ex;
            }          
           
        }
    }
}
