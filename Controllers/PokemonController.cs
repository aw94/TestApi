using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.Annotations;
namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PokemonController : ControllerBase
    {

        private readonly ILogger<PokemonController> _logger;

        public PokemonController(ILogger<PokemonController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("GetResponse/{name}")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Returns internal server error on failed operation", typeof(ActionResult<PokemonCharacter>))]
        [SwaggerResponse(StatusCodes.Status200OK,"Returns successful pokemon character response",typeof(ActionResult<PokemonCharacter>))]
        public ActionResult<PokemonCharacter> GetResponse(string name)
        {
            try
            {
                HttpClient http = new HttpClient();
                _logger.LogInformation("Retrieving Pokemon info");
                var pokemonResponse = http.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{name}").ConfigureAwait(false).GetAwaiter().GetResult();
                JsonConvert.DeserializeObject<PokemonCharacter>(pokemonResponse);
                return Ok(pokemonResponse);
            }
            catch (Exception e)
            {
                _logger.LogWarning("Error message", e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
