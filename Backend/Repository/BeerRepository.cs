using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repository
{
    public class BeerRepository : IRepository<Beer>
    {
        public Task<Beer> Add(Beer entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Beer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Beer> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Update(int id, Beer entity)
        {
            throw new NotImplementedException();
        }
    }
}
