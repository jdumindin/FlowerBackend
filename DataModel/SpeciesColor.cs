using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataModel
{
    public class SpeciesColor
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("ColorId")]
        [InverseProperty("SpeciesColor")]
        public virtual Color Color { get; set; } = null!;

        [ForeignKey("SpeciesId")]
        [InverseProperty("SpeciesColor")]
        public virtual Species Species { get; set; } = null!;
    }
}
