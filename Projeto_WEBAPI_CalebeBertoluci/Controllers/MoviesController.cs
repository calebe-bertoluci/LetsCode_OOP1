
    
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_WEBAPI_CalebeBertoluci.Dto;
//using Projeto_WEBAPI_CalebeBertoluci.Filters;
using Projeto_WEBAPI_CalebeBertoluci.Interfaces;
using Projeto_WEBAPI_CalebeBertoluci.Models;
using Projeto_WEBAPI_CalebeBertoluci.Repositories;
using System.Net.Mime;
using System.Text.Json;

namespace Projeto_WEBAPI_CalebeBertoluci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[CustomAsyncActionFilterController]
    public class MoviesController : ControllerBase, IBaseController<Movies, MoviesDto, MoviesPatchDto>
    {
        private readonly IBaseRepository<Movies> _repository;
        private readonly ILogger<MoviesController> _logger;
        private readonly IConfiguration _configuration;

        public MoviesController(IBaseRepository<Movies> repository, ILogger<MoviesController> logger,
            IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            _configuration = configuration;
        }

        private Movies UpdateMoviesModel(Movies newData, MoviesDto entity)
        {
            newData.Id = entity.Id;
            newData.Name = entity.Name;
            newData.Year = entity.Year;
            newData.ImdbRating = entity.ImdbRating;
            newData.MoviePoster = entity.MoviePoster;
            newData.Genres = entity.Genres;
            newData.Directors = entity.Directors;
            newData.Casts = entity.Casts;
            newData.SimilarMovie = entity.SimilarMovie;
            return newData;
        }
        
        //GET - PAGE
        [HttpGet]
        //[CookiesFilter]
        //[CustomActionFilterEndpoint]
        [Authorize("ValidateClaimModule")]
        public async Task<IActionResult> Get([FromQuery] int page, int maxResults)
        {
            var apikey = _configuration.GetValue<string>("AppInsightsIntrumentatioKey");
            Console.WriteLine(apikey);
            var movies = await _repository.Get(page, maxResults);
            Response.Cookies.Append("authToken", "123");
            return Ok(movies);
        }
        
        //GET - ID
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Movies), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var movie = await _repository.GetByKey(id);
            if (movie == null)
            {
                Console.WriteLine(movie.Id);
                return NotFound("Id Inexistente");
            }
            return Ok(movie);
        }
        
        //PUT - ID
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json, new[] { "application/xml", "text/plain" })]
        [ProducesResponseType(typeof(Movies), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Movies), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status415UnsupportedMediaType)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] MoviesDto entity)
        {
            var databaseMovies = await _repository.GetByKey(id);

            if (databaseMovies == null)
            {
                var movieToInsert = new Movies(entity.Id,entity.Name,entity.Year,entity.ImdbRating,entity.MoviePoster,entity.Genres,entity.Directors,entity.Casts,entity.SimilarMovie); // DAO
                var inserted = await _repository.Insert(movieToInsert);
                return Created(string.Empty, inserted);
            }

            databaseMovies = UpdateMoviesModel(databaseMovies, entity);

            var updated = await _repository.Update(databaseMovies);

            return Ok(updated);
        }
        
        //POST
        [HttpPost]
        //[AuthorizationFilter]
        public async Task<IActionResult> Post([FromBody] MoviesDto entity)
        {
            var gameToInsert = new Movies(entity.Id,entity.Name,entity.Year,entity.ImdbRating,entity.MoviePoster,entity.Genres,entity.Directors,entity.Casts,entity.SimilarMovie); // DAO
            var inserted = await _repository.Insert(gameToInsert);
            return Created(string.Empty, inserted);
        }
        
        //POST - BUSCA
        [HttpPost("busca")]
        public async Task<IActionResult> Post2([FromBody] MoviesDto entity)
        {
            var result = _repository.Get(1, 10).Result;
            var filtered = result.Where(item => item.Id > entity.Id);
            return Ok(filtered);
        }
        
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] MoviesPatchDto entity)
        {
            var databseMovies = await _repository.GetByKey(id);

            if (databseMovies == null)
            {
                return NoContent();
            }
            //_logger.Log(LogLevel.Information, $"Antes do update {JsonSerializer.Serialize(databaseGames)}. Data da atualização: {DateTime.Now.ToString("G")}");
            //_logger.LogInformation($"Antes do update {JsonSerializer.Serialize(databaseGames)}. Data da atualização: {DateTime.Now.ToString("G")}");
            //databseMovies.Platforms = entity.Platforms; <<<--- ESSENCIAL
            var updated = await _repository.Update(databseMovies);
            // Comunicação com o sistema externo
            // Nessa comunicação, da erro. 
            //_logger.Log(LogLevel.Information, $"Depois do update {JsonSerializer.Serialize(updated)}. Data da atualização: {DateTime.Now.ToString("G")}");
            //_logger.LogInformation($"Depois do update {JsonSerializer.Serialize(databaseGames)}. Data da atualização: {DateTime.Now.ToString("G")}");
            return Ok(updated);
        }
        
        //DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var databaseMovies = await _repository.GetByKey(id);

            if (databaseMovies == null)
            {
                return NoContent();
            }
            
            var deleted = await _repository.Delete(id);
            return Ok(deleted);
        }
        
        //POST - ID
        [HttpPost("{id}")]
        public async Task<IActionResult> RecuperaDados([FromRoute] int id)
        {
            var movie = await _repository.GetByKey(id);
            if (movie == null)
            {
                //throw new KeyNotFoundException($"Id: {id}");
                // Gambiarra pra retornar mensagem no NoContent
                //return Ok(new
                //{
                //    message = "Gambiarra pra retornar mensagem no NoContent",
                //    StatusCode = StatusCodes.Status204NoContent
                //});
                return NotFound("Id Inexistente");
            }
            return Ok(movie);
        }
        
        
        
        
    }
    

    
}