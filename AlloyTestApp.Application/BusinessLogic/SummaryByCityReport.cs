using AlloyTestApp.Core.Interfaces.BusinessLogic;
using AlloyTestApp.Core.Interfaces.DataAccess;
using AlloyTestApp.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlloyTestApp.Application.BusinessLogic
{
    public class SummaryByCityReport : ISummaryByCityReport
    {
        private readonly ICustomersRepository _customersRepository;

        public SummaryByCityReport(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<IEnumerable<SummaryByCityReportItem>> GetReportItemsAsync()
        {
            var customers = await _customersRepository.GetListAsync();

            return customers
                .GroupBy(x => x.City)
                .Select(x => new SummaryByCityReportItem
                {
                    CityName = x.Key,
                    SummaryAmount = x.Sum(c => c.Amount)
                })
                .OrderByDescending(x => x.SummaryAmount);
        }
    }
}
