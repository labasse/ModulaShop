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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductType()
        {
            return await _context.ProductType.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductType>> GetProductType(int? id)
        {
            var productType = await _context.ProductType.FindAsync(id);

            if (productType == null)
            {
                return NotFound();
            }

            return productType;
        }

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

        [HttpPost]
        public async Task<ActionResult<ProductType>> PostProductType(ProductType productType)
        {
            _context.ProductType.Add(productType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductType", new { id = productType.Id }, productType);
        }

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
