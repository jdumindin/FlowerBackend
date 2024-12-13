namespace Flower.DTO
{
    public class CreateSpeciesDTO
    {
        public required string ScientificName { get; set; }

        public required string ColloquialName { get; set; }

        public required int GenusId { get; set; }  // Foreign key to Genus
    }
}
