using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet("all")]
        public List<People> GetPeoples() => Repository.People;
    }

    public class Repository
    {
        public static List<People> People = new List<People>(){

        new People() {
        Id = 1, Name="Cristian", Birthday = new DateTime(1994,04,22)
        }
};
    }

    public class People {
    

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    
    }

}
