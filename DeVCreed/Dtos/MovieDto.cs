using System.ComponentModel.DataAnnotations;

namespace DeVCreed.Dtos
{
     public class MovieDto
     {
          [MaxLength(250)]
          [MinLength(1)]
          public string Title { get; set; } = null!;
          public int Year { get; set; }
          public double Rate { get; set; }
          [MaxLength(2500)]
          [MinLength(1)]
          public string StoreLine { get; set; } = null!;
          public IFormFile ? Poster { get; set; } = null;
          public byte GenreId { get; set; } // Fk 


     }
}
