using System.ComponentModel.DataAnnotations;                    // Provides attributes for model validation
using System.ComponentModel.DataAnnotations.Schema;             // Provides attributes like [Table] to map classes to database tables

namespace WebAPISandwich.Model
{
    [Table("Sandwich")]                                         // Maps this class to the "Sandwich" table in the database
    public class Sandwich
    {
        [Key]                                                   // Id as the primary key of the table
        public int Id { get; set; }

        [Required]                                              // Name cannot be null
        public string? Name { get; set; }

        public double? Price { get; set; }                      // Price is optional (nullable), can store null if not provided
    }
}