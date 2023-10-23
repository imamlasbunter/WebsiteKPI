using Microsoft.EntityFrameworkCore;
using Pertamina.Website_KPI.Domain.Entities;
using Pertamina.Website_KPI.Domain.Entities.DatabaseContext;

namespace Pertamina.Website_KPI.Application.Beranda;

public interface IHeaderUpServices
{
    Task<bool> Add(TbltHeaderUp items);
    Task<bool> Update(TbltHeaderUp items);
    Task<List<TbltHeaderUp>> GetData();
    TbltHeaderUp GetById(int id);
}

public class HeaderUpServices : IHeaderUpServices
{
    private readonly Website_KPIContext _dbContext;

    public HeaderUpServices(Website_KPIContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Add(TbltHeaderUp items)
    {
        try
        {
            _ = await _dbContext.TbltHeaderUp.AddAsync(items);

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public TbltHeaderUp GetById(int id)
    {
        try
        {
            var dta = _dbContext.TbltHeaderUp.Single(m => m.Id.Equals(id));

            return dta;
        }
        catch (Exception ex)
        {
            return new TbltHeaderUp();
        }
    }

    public async Task<List<TbltHeaderUp>> GetData()
    {
        try
        {
            var dta = await _dbContext.TbltHeaderUp.ToListAsync();

            return dta;
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }

    public async Task<bool> Update(TbltHeaderUp items)
    {
        try
        {
            var existingItem = GetById(items.Id);

            existingItem.Title = items.Title;
            existingItem.Subtitle = items.Subtitle;
            existingItem.IsActive = items.IsActive;
            existingItem.UpdatedBy = "ADMIN";
            existingItem.UpdatedDate = DateTime.Now;

            _dbContext.Entry(existingItem).State = EntityState.Modified;
            _ = await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
