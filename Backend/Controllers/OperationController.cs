using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Backend.Controllers
{
    // Especificamos la ruta base para las acciones del controlador y que es un ApiController
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        // Definimos una acción HTTP GET que responde a la ruta base del controlador
        [HttpGet]
        public decimal OperationAmount(decimal a, decimal b)
        {
            // Realizamos una operación simple de suma y devolvemos el resultado
            return a + b;
        }

        [HttpPost]
        public decimal OperationRest(decimal a, decimal b)
        {
            // Realizamos una operación simple de resta y devolvemos el resultado
            return a - b;
        }

        [HttpPut]
        public decimal OperationMultify(decimal a, decimal b)
        {
            // Realizamos una operación simple de multiplicacion y devolvemos el resultado
            return a * b;
        }


        [HttpDelete]
        public decimal OperationDivide(decimal a, decimal b)
        {
            // Realizamos una operación simple de division y devolvemos el resultado
            return a / b;
        }
    }
}

