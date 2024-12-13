using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flower.DTO
{
    public class FlowerDTO
    {
        public int Id { get; set; }

        public string SpeciesName { get; set; } = null!;

        public string ColloquialName { get; set; } = null!;

        public required string GenusName { get; set; }
    }
}
