using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BeerRepository : IBeerRepository<Beer>
{
    private readonly StoreContext _storeContext;

    public BeerRepository(StoreContext storeContext)
    {
        _storeContext = storeContext;
    }

    public async Task<Beer> Add(Beer entity)
    {
        _storeContext.Beers.Add(entity);
        await _storeContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<Beer>> GetAll()
    {
        return await _storeContext.Beers.ToListAsync();
    }

    public async Task<Beer?> GetById(int id)
    {
        return await _storeContext.Beers.FindAsync(id);
    }

    public async Task Save()
    {
        await _storeContext.SaveChangesAsync();
    }

    public async Task<IActionResult> Update(int id, Beer entity)
    {
        var existingBeer = await _storeContext.Beers.FindAsync(id);

        if (existingBeer == null)
        {
            return new NotFoundResult();
        }

        // Actualiza las propiedades de la cerveza con los datos de la entidad
        existingBeer.BeerName = entity.BeerName;
        existingBeer.BeerDescription = entity.BeerDescription;
        existingBeer.BeerType = entity.BeerType;
        existingBeer.Alcohol = entity.Alcohol;
        existingBeer.Brand = entity.Brand;

        await _storeContext.SaveChangesAsync();

        return new NoContentResult();
    }
}
