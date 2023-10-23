
using Microsoft.EntityFrameworkCore;
using Pertamina.Website_KPI.Domain.Entities;
using Pertamina.Website_KPI.Domain.Entities.DatabaseContext;

namespace Pertamina.Website_KPI.Application.Beranda;

public interface IRegionalServices
{
    Task<bool> Add(TbltRegional items);
    Task<bool> Update(TbltRegional items);
    Task<List<TbltRegional>> GetData();
}
public class RegionalServices : IRegionalServices
{
    private readonly Website_KPIContext _dbContext;

    public RegionalServices(Website_KPIContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> Add(TbltRegional items)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(TbltRegional items)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TbltRegional>> GetData()
    {
        try
        {
            var dta = await _dbContext.TbltRegional.ToListAsync();
            return dta;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
