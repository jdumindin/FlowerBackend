using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataModel;
using Flower.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Flower.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly FlowerDbContext _context;

        public SpeciesController(FlowerDbContext context)
        {
            _context = context;
        }

        // GET: api/Species
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Species>>> GetSpecies()
        {
            return await _context.Species.ToListAsync();
        }

        // GET: api/Species/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Species>> GetSpecies(int id)
        {
            var species = await _context.Species.FindAsync(id);

            if (species == null)
            {
                return NotFound();
            }

            return species;
        }

        // PUT: api/Species/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecies(int id, Species species)
        {
            if (id != species.Id)
            {
                return BadRequest();
            }

            _context.Entry(species).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeciesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Species
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Species>> PostSpecies(Species species)
        {
            _context.Species.Add(species);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecies", new { id = species.Id }, species);
        }

        // DELETE: api/Species/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecies(int id)
        {
            var species = await _context.Species.FindAsync(id);
            if (species == null)
            {
                return NotFound();
            }

            _context.Species.Remove(species);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpeciesExists(int id)
        {
            return _context.Species.Any(e => e.Id == id);
        }

        // GET: api/Species/genus-species/{genusId}
        [HttpGet("genus-species/{genusId}")]
        public async Task<ActionResult<IEnumerable<Species>>> GetSpeciesByGenus(int genusId)
        {
            var speciesList = await _context.Species
                                            .Where(s => s.GenusId == genusId)
                                            .ToListAsync();

            if (!speciesList.Any())
            {
                return NotFound($"No species found for GenusId {genusId}.");
            }

            return Ok(speciesList);
        }

        [HttpPost("Create-Species")]
        [Authorize]
        public async Task<IActionResult> CreateSpecies([FromBody] CreateSpeciesDTO speciesDto)
        {
            if (speciesDto == null || speciesDto.GenusId == 0)
            {
                return BadRequest("GenusId is required");
            }

            // Retrieve the Genus object using the GenusId
            var genus = await _context.Genuses.FindAsync(speciesDto.GenusId);
            if (genus == null)
            {
                return BadRequest("Genus not found");
            }

            // Create the Species object with the GenusId (no need to include the full Genus object)
            var species = new Species
            {
                ScientificName = speciesDto.ScientificName,
                ColloquialName = speciesDto.ColloquialName,
                GenusId = speciesDto.GenusId
            };

            // Add the Species to the database
            _context.Species.Add(species);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecies", new { id = species.Id }, species);
        }

    }
}
