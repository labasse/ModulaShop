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
    [Route("api/product-types")]
    [ApiController]
    public class ProductTypesController : ControllerBase
    {
        private readonly CatalogContext _context;

        public ProductTypesController(CatalogContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the list of available product types.
        /// </summary>
        /// <response code="200">Type list successfully returned.</response>
        [ProducesResponseType(typeof(IEnumerable<ProductType>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductType()
        {
            return await _context.ProductType.ToListAsync();
        }

        /// <summary>
        /// Get the list of product for the given type.
        /// </summary>
        /// <param name="id">Type Id whose product are to be retreived</param>
        /// <response code="200">Product list successfully returned.</response>
        /// <response code="400">The given type does not exists.</response>
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}/products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByProductType(int? id)
        {
            if(!_context.ProductType.Any(t=>t.Id == id))
            {
                return BadRequest("No product type with this id");
            }
            return await _context.Product.Where(p=>p.Type.Id==id).ToListAsync();
        }

        /// <summary>
        /// TODO: Documentation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productType"></param>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductType(int? id, ProductType productType)
        {
            if (id != productType.Id)
            {
                return BadRequest();
            }

            _context.Entry(productType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypeExists(id))
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
        /// Creates the product type.
        /// </summary>
        /// <param name="productType">Product type to create. Id field is ignored.</param>
        /// <response code="401">No authentication data provided</response>
        /// <response code="403">Credentials does not match Admin or Marketing role.</response>
        /// <response code="201">Product type successfully created</response>
        [ProducesResponseType(typeof(ProductType), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [Authorize(Roles = ShopRoles.AdminMarketing)]
        [HttpPost]
        public async Task<ActionResult<ProductType>> PostProductType(ProductType productType)
        {
            _context.ProductType.Add(productType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductType", new { id = productType.Id }, productType);
        }

        /// <summary>
        /// Deletes the product type.
        /// </summary>
        /// <param name="id">Id of the type to delete</param>
        /// <response code="401">No authentication data provided</response>
        /// <response code="403">The type still have products associated and/or 
        /// credentials does not match Admin or Marketing role.</response>
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [Authorize(Roles = ShopRoles.AdminMarketing)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductType(int? id)
        {
            var productType = await _context.ProductType.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }
            _context.ProductType.Remove(productType);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ProductTypeExists(int? id)
        {
            return _context.ProductType.Any(e => e.Id == id);
        }
    }
}
