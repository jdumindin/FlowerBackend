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

        [Column("genus")]
        [StringLength(50)]
        public string Genus { get; set; } = null!;

        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [ForeignKey("FamilyId")]
        [InverseProperty("Species")]
        public virtual Family Family { get; set; } = null!;

        [ForeignKey("ContinentId")]
        [InverseProperty("Species")]
        public virtual Continent Continent { get; set; } = null!;
    }
}
