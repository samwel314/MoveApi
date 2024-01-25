using DeVCreed.Data.Models;

namespace DeVCreed.Services
{
     public interface IGenresService
     {
          Task<IEnumerable<Genre>> GetAll();
          Task<Genre> Add(Genre genre);
        Genre Update(Genre genre);
        Genre Delete(Genre genre);

          bool IsFound(int id); 
          Task<Genre> GetGenre(int Id); 

     }

}

