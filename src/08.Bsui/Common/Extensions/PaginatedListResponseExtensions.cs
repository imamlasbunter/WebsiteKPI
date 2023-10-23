using MudBlazor;
using Pertamina.Website_KPI.Shared.Common.Responses;

namespace Pertamina.Website_KPI.Bsui.Common.Extensions;
public static class PaginatedListResponseExtensions
{
    public static TableData<T> ToTableData<T>(this PaginatedListResponse<T> result)
    {
        return new TableData<T>() { TotalItems = result.TotalCount, Items = result.Items };
    }
}
