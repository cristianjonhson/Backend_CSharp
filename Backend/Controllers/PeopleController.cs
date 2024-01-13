using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleService _peopleService;

        // Inyección de dependencias en el constructor
        public PeopleController([FromKeyedServices("peopleService")]IPeopleService peopleService)
        {
            // Se inyecta la instancia de IPeopleService en lugar de crear una nueva.
            _peopleService = peopleService;
        }

        // GET: api/People/all
        [HttpGet("all")]
        public ActionResult<List<People>> GetPeoples()
        {
            // Devuelve un ActionResult con un código de estado 200 OK y la lista de personas.
            return Ok(Repository.People);
        }

        // GET: api/People/{id}
        // Buscar una persona por ID
        [HttpGet("{id}")]
        public ActionResult<People> GetPeoplesById(int id)
        {
            // Busca la persona en la lista con el ID proporcionado.
            var people = Repository.People.FirstOrDefault(p => p.Id == id);

            // Verifica si se encontró la persona.
            if (people == null)
            {
                // Si no se encuentra, devuelve un ActionResult con un código de estado 404 Not Found.
                return NotFound();
            }

            // Si se encuentra, devuelve un ActionResult con un código de estado 200 OK y la persona.
            return Ok(people);
        }

        // GET: api/People/search/{search}
        // Buscar personas cuyo nombre contenga el valor proporcionado
        [HttpGet("search/{search}")]
        public ActionResult<List<People>> GetPeoples(string search)
        {
            // Filtra las personas cuyo nombre contiene la cadena proporcionada (ignorando mayúsculas y minúsculas).
            var filteredPeople = Repository.People
                .Where(p => p.Name.ToUpper().Contains(search.ToUpper()))
                .ToList();

            if (filteredPeople.Count == 0)
            {
                // Si no se encuentra ninguna persona, devuelve un ActionResult con un código de estado 404 Not Found
                // y un mensaje descriptivo.
                return Ok($"No se encontraron personas con el nombre que contenga '{search}'.");
            }
            else
            {
                var firstPerson = filteredPeople.First();

                // Verifica si el nombre de la primera persona es válido utilizando el servicio.
                var isNameValid = _peopleService.Validate(firstPerson);

                if (!isNameValid)
                {
                    // Si el nombre no es válido, devuelve un IActionResult con un código de estado 400 Bad Request.
                    return BadRequest("El nombre de la persona no es válido.");
                }

                // Si se encontraron personas y el nombre es válido, devuelve un ActionResult con un código de estado 200 OK y la lista de personas filtradas.
                return Ok(filteredPeople);
            }
        }
    }

    // Clase estática que actúa como una fuente de datos ficticia
    public class Repository
    {
        public static List<People> People = new List<People>()
        {
            new People() { Id = 1, Name="Cristian", Birthday = new DateTime(1994, 04, 22) },
            new People() { Id = 2, Name="Luis", Birthday = new DateTime(2000, 02, 03) }
        };
    }

    // Clase que representa a una persona
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}
