using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeVCreed.Data.Models
{
    public class Genre
    {
          [DatabaseGenerated(databaseGeneratedOption :DatabaseGeneratedOption.Identity)]
          public byte Id { get; set; }   // Id For This Table     

          [MaxLength(100)]
          public string Name { get; set; } = null!; 
          
    }
}
