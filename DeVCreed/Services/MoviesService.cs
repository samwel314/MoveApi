using DeVCreed.Data;
using DeVCreed.Data.Models;
using DeVCreed.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DeVCreed.Services
{
     public class MoviesService : IMoviesService
     {
          private readonly AppDbContext _context;

          public MoviesService(AppDbContext context)
          {
               _context = context;
          }
          /*
           *     _context.Movies.Remove(movie);     

               _context.SaveChanges ();

           */
          public Movie Add(Movie movie)
          {
                _context.Movies.AddAsync(movie);
               _context.SaveChanges();
               return movie;  
          }

          public Movie Delete(Movie movie)
          {
               _context.Movies.Remove(movie);
               _context.SaveChanges();
               return movie;
          }
          public Movie Update(Movie movie)
          {
               _context.Movies.Update(movie);
               _context.SaveChanges();
               return movie;
          }
          public async Task<IEnumerable<Movie>> GetAll(int id = 0 )
          {
             return  await    _context.Movies
                    .OrderByDescending(m => m.Rate)
                   .Where(m=>m.GenreId == id || id == 0).ToListAsync();    
 
          }

          public  async Task<Movie > GetMovie(int id)
          {
             var data =  await _context.Movies.Include(m => m.Genre)
                    .FirstOrDefaultAsync(m => m.Id == id);
               return data!; 
          }
     }
}

