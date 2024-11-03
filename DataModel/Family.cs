using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataModel
{
    public class Family
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("scientificName")]
        [StringLength(50)]
        public string ScientificName { get; set; } = null!;

        [Column("colloquialName")]
        [StringLength(50)]
        public string ColloquialName { get; set; } = null!;

    }
}
