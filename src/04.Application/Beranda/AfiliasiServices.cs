using Microsoft.EntityFrameworkCore;
using Pertamina.Website_KPI.Domain.Entities;
using Pertamina.Website_KPI.Domain.Entities.DatabaseContext;

namespace Pertamina.Website_KPI.Application.Beranda;

public interface IAfiliasiServices
{
    Task<List<TbltAfiliasi>> GetData();
}

public class AfiliasiServices : IAfiliasiServices
{
    //https://pdsi.pertamina.com/
    private readonly Website_KPIContext _dbContext;

    public AfiliasiServices(Website_KPIContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TbltAfiliasi>> GetData()
    {
        try
        {
            var dta = await _dbContext.TbltAfiliasi.ToListAsync();
            return dta;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
