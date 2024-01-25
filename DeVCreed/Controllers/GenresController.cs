using DeVCreed.Data;
using DeVCreed.Data.Models;
using DeVCreed.Dtos;
using DeVCreed.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeVCreed.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class GenresController : ControllerBase
     {
       
          private readonly IGenresService _genresService; 
      

          public GenresController( IGenresService genresService)
          {
               _genresService = genresService;
          }

          [HttpGet] 
          public async Task<IActionResult> GetAll ()
          {
               // --- 
               var Data = await _genresService.GetAll();     
               
               return Ok(Data);    
          }
          [HttpPost]
          public  async Task<IActionResult> Create(GenreDto Model  )
          {
               var genre = new Genre()
               {
                    Name = Model.Name,  
               };

               genre =  await   _genresService.Add(genre);   
               return Ok(genre);   
          }

          // ------       
          [HttpPut (template: "{id}")]
          public async  Task<IActionResult> Update ( [FromRoute] int id , GenreDto dto )
          {
               var genre = await _genresService.GetGenre(id); 

               if (genre == null)
                    return NotFound("This Resource Not Found  ");

               genre.Name = dto.Name;

               genre =  _genresService.Update(genre);  
               return Ok(genre);
          }

          [HttpDelete ("{id}")]
          public async Task<IActionResult> Delete ([FromRoute] int id) 
          {
               //---? 
              var genre  = await  _genresService.GetGenre(id);
               if (genre == null)
                    return NotFound("This Resourse Not Found");

               genre =  _genresService.Delete(genre); 
               return Ok($"We Remove {genre.Name}");
 
          }

     }
}
