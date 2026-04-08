using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Mvc;

public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
{
    private IBeerRepository<Beer> _beerRepository;

    public BeerService(IBeerRepository<Beer> beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task<IEnumerable<BeerDto>> Get()
    { 
        var beers = await _beerRepository.GetAll();
        return beers.Select(b => new BeerDto()
        {
            BeerId = b.BeerId,
            BeerName = b.BeerName,
            BeerType = b.BeerType,
            BeerDescription = b.BeerDescription,
            Alcohol = b.Alcohol,
            BrandId = b.Brand?.BrandId
        }); 
    }

    public async Task<BeerDto?> GetById(int id)
    {
        var beer = await _beerRepository.GetById(id);

        if (beer != null) {
            var beerDto = new BeerDto()
            {
                BeerId = beer.BeerId,
                BeerName = beer.BeerName,
                BeerType = beer.BeerType,
                BeerDescription = beer.BeerDescription,
                Alcohol = beer.Alcohol,
                BrandId = beer.Brand?.BrandId
            };
        return beerDto;
        }
        return null;
    }

    public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
    {
        var beer = new Beer()
        {
            BeerName = beerInsertDto.BeerName,
            BeerDescription = beerInsertDto.BeerDescription,
            BeerType = beerInsertDto.BeerType,
            Alcohol = beerInsertDto.Alcohol,
        };

        await _beerRepository.Add(beer);
        await _beerRepository.Save();

        var beerDto = new BeerDto()
        {
            BeerId = beer.BeerId,
            BeerName = beer.BeerName,
            BeerType = beer.BeerType,
            BeerDescription = beer.BeerDescription,
            Alcohol = beer.Alcohol,
            BrandId = beer.Brand?.BrandId
        };

        return beerDto;
    }

    public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
    {
        var beerEntity = await _beerRepository.GetById(id);

        if (beerEntity != null)
        {
            beerEntity.BeerName = beerUpdateDto.BeerName;
            beerEntity.BeerDescription = beerUpdateDto.BeerDescription;
            beerEntity.BeerType = beerUpdateDto.BeerType;
            beerEntity.Alcohol = beerUpdateDto.Alcohol;

            _beerRepository.Update(beerEntity);
            await _beerRepository.Save();

            var beerDto = new BeerDto()
            {
                BeerId = beerEntity.BeerId,
                BeerName = beerEntity.BeerName,
                BeerType = beerEntity.BeerType,
                BeerDescription = beerEntity.BeerDescription,
                Alcohol = beerEntity.Alcohol,
                BrandId = beerEntity.Brand?.BrandId
            };

            return beerDto;
        }
        return null;
    }
}
