// Servicio de cervezas
using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class BeerService : IBeerService
    {
        private readonly StoreContext _storeContext;

        // Constructor que recibe el contexto de la base de datos mediante inyección de dependencias
        public BeerService(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
            // Realiza una consulta para seleccionar todas las cervezas y proyecta los resultados en BeerDto
            var beerDtos = await _storeContext.Beers
                .Select(x => new BeerDto
                {
                    BeerId = x.BeerId,
                    BeerDescription = x.BeerDescription,
                    BeerName = x.BeerName,
                    BeerType = x.BeerType,
                    Alcohol = x.Alcohol,
                    BrandId = x.Brand.BrandId
                })
                .ToListAsync();

            // Retorna la lista de BeerDto
            return beerDtos;
        }

        public async Task<BeerDto> GetById(int id)
        {
            // Incluye la propiedad de navegación Brand al cargar la cerveza
            var beer = await _storeContext.Beers
                .Include(b => b.Brand)
                .FirstOrDefaultAsync(b => b.BeerId == id);

            // Si no se encuentra la cerveza, retorna null
            if (beer == null)
            {
                return null;
            }

            // Retorna un objeto BeerDto basado en la cerveza encontrada
            return new BeerDto()
            {
                BeerId = beer.BeerId,
                BeerDescription = beer.BeerDescription,
                BeerName = beer.BeerName,
                BeerType = beer.BeerType,
                Alcohol = beer.Alcohol,
                BrandId = beer.Brand != null ? beer.Brand.BrandId : 0
            };
        }

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            // Crea una nueva instancia de la clase Beer
            var beer = new Beer()
            {
                BeerDescription = beerInsertDto.BeerDescription,
                BeerName = beerInsertDto.BeerName,
                BeerType = beerInsertDto.BeerType,
                Alcohol = beerInsertDto.Alcohol,
            };

            // Busca la marca por su ID en la base de datos
            var brand = await _storeContext.Brands.FindAsync(beerInsertDto.BrandId);

            // Si la marca no se encuentra, retorna un resultado NotFound
            if (brand == null)
            {
                return null;
            }

            // Asigna la marca a la cerveza
            beer.Brand = brand;

            // Agrega la cerveza al contexto y guarda los cambios en la base de datos
            await _storeContext.Beers.AddAsync(beer);
            await _storeContext.SaveChangesAsync();

            // Retorna un objeto BeerDto basado en la cerveza creada
            return new BeerDto
            {
                BeerId = beer.BeerId,
                BeerDescription = beer.BeerDescription,
                BeerName = beer.BeerName,
                BeerType = beer.BeerType,
                Alcohol = beer.Alcohol,
                BrandId = beer.Brand.BrandId
            };
        }

        public async Task<IActionResult> UpdateBeer(int id, BeerUpdateDto beerUpdateDto)
        {
            // Busca la cerveza en la base de datos por su ID
            var beer = await _storeContext.Beers.FindAsync(id);

            // Si no se encuentra la cerveza, retorna un resultado NotFound
            if (beer == null)
            {
                return new NotFoundResult();
            }

            // Actualiza las propiedades de la cerveza con los datos del DTO
            beer.BeerName = beerUpdateDto.BeerName;
            beer.BeerDescription = beerUpdateDto.BeerDescription;
            beer.BeerType = beerUpdateDto.BeerType;
            beer.Alcohol = beerUpdateDto.Alcohol;

            // Verifica si BrandId es mayor que cero (o el valor predeterminado para long)
            if (beerUpdateDto.BrandId > 0)
            {
                var brand = await _storeContext.Brands.FindAsync(beerUpdateDto.BrandId);

                // Si la marca no se encuentra, retorna un resultado NotFound
                if (brand == null)
                {
                    return new NotFoundObjectResult("Marca no fue encontrada");
                }

                // Asigna la marca actualizada a la cerveza
                beer.Brand = brand;
            }

            // Guarda los cambios en la base de datos
            await _storeContext.SaveChangesAsync();

            // Retorna un resultado NoContent indicando que la actualización fue exitosa
            return new NoContentResult();
        }
    }
}