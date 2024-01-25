using DeVCreed.Data;
using DeVCreed.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DeVCreed.Services
{
     public class GenresService : IGenresService
     {

          private readonly AppDbContext _context;

          public GenresService(AppDbContext context)
          {
               _context = context;
          }

          public async Task<Genre> Add(Genre genre)
          {
                await _context.Genres.AddAsync(genre); 
               _context.SaveChanges();
               return genre;  
          }

          public Genre Delete(Genre genre)
          {
               _context.Remove(genre);
              _context.SaveChanges();
               return genre; 
          }

          public async Task<IEnumerable<Genre>> GetAll()
          {
                var Data = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
               return Data;
          }

          public async Task<Genre> GetGenre(int Id)
          {
               var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == Id);
               return genre!; 
          }

          public bool IsFound(int id)
          {
              return    _context.Genres. Any(g => g.Id ==id);
          }

          public Genre Update(Genre genre)
          {
               _context.Genres.Update(genre);
               _context.SaveChanges();
               return genre;
          }
     }

}

