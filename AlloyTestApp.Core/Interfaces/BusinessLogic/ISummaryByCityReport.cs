using AlloyTestApp.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlloyTestApp.Core.Interfaces.BusinessLogic
{
    public interface ISummaryByCityReport
    {
        Task<IEnumerable<SummaryByCityReportItem>> GetReportItemsAsync();
    }
}
