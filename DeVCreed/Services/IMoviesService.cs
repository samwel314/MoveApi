using DeVCreed.Data.Models;
using DeVCreed.Dtos;

namespace DeVCreed.Services
{
     public interface IMoviesService
     {
          Task<IEnumerable<Movie>> GetAll(int  id = 0 ); 

          Task<Movie> GetMovie( int id );
          //Delete
          Movie Add(Movie movie);
          Movie Delete(Movie movie);
          Movie Update(Movie movie);
     }
}

