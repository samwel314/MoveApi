using System.ComponentModel.DataAnnotations;

namespace DeVCreed.Dtos
{
     public class GenreDto
     {
          [MaxLength(100)]
          [MinLength(1)]
          public string Name { get; set; } = null!; 
     }
}
