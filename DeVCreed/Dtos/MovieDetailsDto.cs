using System.ComponentModel.DataAnnotations;

namespace DeVCreed.Dtos
{
     public class MovieDetailsDto
     {
          public int Id { get; set; }
          public string Title { get; set; } = null!;
          public int Year { get; set; }
          public double Rate { get; set; }
          public string StoreLine { get; set; } = null!;
          public byte[] Poster { get; set; } = null!;
          public string GenreName { get; set; } = null!;
          public byte GenreId { get; set; } // Fk


     }
}
