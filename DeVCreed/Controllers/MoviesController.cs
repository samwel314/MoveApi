using AutoMapper;
using DeVCreed.Data;
using DeVCreed.Data.Models;
using DeVCreed.Dtos;
using DeVCreed.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeVCreed.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class MoviesController : ControllerBase
     {
          private readonly IMapper _mapper; 
          private readonly IGenresService _genresService; 
          private readonly IMoviesService _moviesService;
          private  List<string> _allows = new List<string>
          {
               ".jpg"  , ".png" 
          };
          private long _max = 1048576;
     
 
          public MoviesController(IGenresService genresService, IMoviesService moviesService , IMapper mapper )
          {
               _genresService = genresService;
               _moviesService = moviesService;
               _mapper = mapper;
          }

          [HttpGet("GenreMovies/{id}")]
          // هنا احنا كررنا code لما نعمل servies هنرتاح 
          public  async  Task<IActionResult> GetGenreMovies(byte id )
          {
               var movies = await _moviesService.GetAll(id);

              var data =  _mapper.Map<IEnumerable<MovieDetailsDto>>(movies); 
               if (movies.Count() == 0 )
                    return NotFound("No Movies Found For This Genre");

               return Ok(data);  
          }

          [HttpGet]
          public async Task<IActionResult> GetAll ()
          {
               var movies = await _moviesService.GetAll();  

               return Ok(movies);  
          }
          [HttpGet ("{id}")]
          public  async Task <IActionResult> GetMovie ([FromRoute] int id )
          {
               var movie =await _moviesService.GetMovie(id);     
               if (movie == null)
                    return NotFound($"No Movie Found With This Id {id}"); 

               return Ok(new MovieDetailsDto
               {
                    Id = movie.Id,
                    Title = movie.Title,
                    Poster = movie.Poster,
                    GenreName = movie.Genre.Name,
                    StoreLine = movie.StoreLine,
                    Rate = movie.Rate,
                    GenreId = movie.Genre.Id,
                    Year = movie.Year,
               });   
               //---- //////- --------------- -- - --------------- -----------// ----
          }

          [HttpPost]  
          public async Task<IActionResult> Create ([FromForm] MovieDto Model )
          {
               // file => size , extions 
               if (Model.Poster == null)
                    return BadRequest("Uploade poster  ");

               if (!_allows.Contains(Path.GetExtension(Model.Poster.FileName.ToLower())) )
                    return BadRequest("This Type Of file not allowed we use png , jpg");    
                    
               if ( Model.Poster.Length > _max)
               {
                    return BadRequest("Max File Size is 1MB");
               }
               // هنا افضل من First 
               var Done = _genresService.IsFound (Model.GenreId);

               if (!Done)
                    return BadRequest("Invalid Genre Id") ; 

               using  var dataStrem = new MemoryStream ();   

               await Model.Poster.CopyToAsync (dataStrem);  // -- ><--//

               var movie = new Movie
               {
                    Title = Model.Title,
                    Year = Model.Year,
                    Poster = dataStrem.ToArray(),   
                    Rate = Model.Rate,
                    StoreLine = Model.StoreLine,
                    GenreId = Model.GenreId,
               };

               _moviesService.Add(movie);    

               return Ok(movie); 
          }


          [HttpDelete ("{id}")]
          
          public async Task<IActionResult> Delete (int id )
          {
               var movie = await _moviesService.GetMovie (id);

               if (movie == null)
                    return NotFound($"NO Movie With this  id {id}");

               _moviesService.Delete(movie); 
               return Ok(movie); 
          }

          [HttpPut ("{id}")]    
          public async Task<IActionResult> Update
               ([FromRoute] int id, [FromForm] MovieDto model)
          {
               var movie = await _moviesService.GetMovie(id);     

                    if (movie == null)
                         return NotFound($"NO Movie With this  id {id}");

                var Done = _genresService.IsFound(model.GenreId);

               if (!Done)
                    return BadRequest("Invalid Genre Id");


               if (model.Poster != null)
               {
                    if (!_allows.Contains(Path.GetExtension(model.Poster.FileName.ToLower())))
                         return BadRequest("This Type Of file not allowed we use png , jpg");

                    if (model.Poster.Length > _max)
                    {
                         return BadRequest("Max File Size is 1MB");
                    }
                    using var dataStrem = new MemoryStream();

                    await model.Poster.CopyToAsync(dataStrem);
                    movie.Poster = dataStrem.ToArray();
               }

               movie.Title = model.Title;
               movie.Year = model.Year;
               movie.Rate = model.Rate;
               movie.StoreLine = model.StoreLine;
               movie.GenreId = model.GenreId;

             _moviesService.Update(movie);
               return Ok(movie);
          }

     }
}
