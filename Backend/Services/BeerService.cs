using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Mvc;

public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
{
    private readonly IBeerRepository _beerRepository;

    public BeerService(IBeerRepository beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task<IEnumerable<BeerDto>> Get()
    {
        var beerEntities = await _beerRepository.GetAll();
        return beerEntities.Select(MapToDto);
    }

    public async Task<BeerDto> GetById(int id)
    {
        var beerEntity = await _beerRepository.GetById(id);
        return beerEntity != null ? MapToDto(beerEntity) : null;
    }

    public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
    {
        var beerEntity = MapToEntity(beerInsertDto);
        await _beerRepository.Add(beerEntity);
        return MapToDto(beerEntity);
    }

    public async Task<IActionResult> Update(int id, BeerUpdateDto beerUpdateDto)
    {
        var beerEntity = MapToEntity(beerUpdateDto);
        return await _beerRepository.Update(id, beerEntity);
    }

    // Método para mapear una entidad Beer a un DTO BeerDto
    private BeerDto MapToDto(Beer beerEntity)
    {
        return new BeerDto
        {
            BeerId = beerEntity.BeerId,
            BeerName = beerEntity.BeerName,
            BeerDescription = beerEntity.BeerDescription,
            BeerType = beerEntity.BeerType,
            Alcohol = beerEntity.Alcohol,
            BrandId = beerEntity.Brand != null ? beerEntity.Brand.BrandId : 0
        };
    }

    // Método para mapear un DTO BeerInsertDto a una entidad Beer
    private Beer MapToEntity(BeerInsertDto beerInsertDto)
    {
        return new Beer
        {
            BeerName = beerInsertDto.BeerName,
            BeerDescription = beerInsertDto.BeerDescription,
            BeerType = beerInsertDto.BeerType,
            Alcohol = beerInsertDto.Alcohol,
            Brand = beerInsertDto.BrandId // Puedes validar o manejar la marca según tus necesidades
        };
    }

    // Método para mapear un DTO BeerUpdateDto a una entidad Beer
    private Beer MapToEntity(BeerUpdateDto beerUpdateDto)
    {
        return new Beer
        {
            BeerName = beerUpdateDto.BeerName ?? string.Empty,
            BeerDescription = beerUpdateDto.BeerDescription ?? string.Empty,
            BeerType = beerUpdateDto.BeerType ?? string.Empty,
            Alcohol = beerUpdateDto.Alcohol,
            BrandId = beerUpdateDto.BrandId // Puedes validar o manejar la marca según tus necesidades
        };
    }
}
