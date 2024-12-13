using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace DataModel
{
    public class Species
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

        [Column("genusId")]
        public int GenusId { get; set; }

        [ForeignKey("GenusId")]
        [InverseProperty("Species")]
        public virtual Genus Genus { get; set; } = null!;
    }
}
