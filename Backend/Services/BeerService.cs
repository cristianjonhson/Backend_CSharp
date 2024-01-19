using Backend.DTOs;

namespace Backend.Services
{
    public class BeerService : IBeerService
    {
        public Task<IEnumerable<BeerDto>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<BeerDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            throw new NotImplementedException();
        }

        public Task<BeerDto> UpdateBeer(int id, BeerUpdateDto beerUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
