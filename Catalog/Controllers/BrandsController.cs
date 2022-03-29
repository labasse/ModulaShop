#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Catalog.Data;
using Catalog.Models;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly CatalogContext _context;

        public BrandsController(CatalogContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the list of available brands.
        /// </summary>
        /// <response code="200">Brand list successfully returned.</response>
        [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrand()
        {
            return await _context.Brand.ToListAsync();
        }

        /// <summary>
        /// TODO : Documentation
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}/products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByBrand(int id)
        {
            if (!_context.Brand.Any(t => t.Id == id))
            {
                return BadRequest("No brand with this id");
            }
            return await _context.Product.Where(p => p.Brand.Id == id).ToListAsync();
        }

        /// <summary>
        /// Modifies brand information.
        /// </summary>
        /// <param name="id">id of brand to modify</param>
        /// <param name="brand">New brand information. Id content must match Id parameter.</param>
        /// <response code="204">Brand successfully modified</response>
        /// <response code="422">Id parameter does not match the Brand Id</response>
        /// <response code="404">No brand exists with the given Id</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int? id, Brand brand)
        {
            if (id != brand.Id)
            {
                return UnprocessableEntity("Id parameter does not match the Brand Id");
            }
            _context.Entry(brand).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
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

        /// <summary>
        /// Creates a new brand.
        /// </summary>
        /// <param name="brand">New brand information. Id field is ignored.</param>
        /// <response code="201">Brand successfully created</response>
        /// <response code="401">No authentication data provided</response>
        /// <response code="403">Credentials does not match Admin or Marketing role.</response>
        [ProducesResponseType(typeof(Brand), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _context.Brand.Add(brand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrand", new { id = brand.Id }, brand);
        }

        /// <summary>
        /// Deletes the brand.
        /// </summary>
        /// <param name="id">Id of brand to delete</param>
        /// <response code="401">No authentication data provided</response>
        /// <response code="403">The brand still have products associated and/or 
        /// credentials does not match Admin or Marketing role.</response>
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [Authorize(Roles = ShopRoles.AdminMarketing)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int? id)
        {
            var brand = await _context.Brand.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brand.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrandExists(int? id)
        {
            return _context.Brand.Any(e => e.Id == id);
        }
    }
}
