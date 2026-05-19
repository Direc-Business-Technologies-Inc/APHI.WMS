using Application.DataTransferObjects.Others.SAP;
using Shared.Libraries.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
namespace Application.UseCases.Repositories.Integration.Others;

public interface IDashboardItemsIntegration
{
    Task<DashboardItemsSAPDTO> GetDashboardItemsAsync();
}
