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
using Catalog.Dtos;

namespace Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CatalogContext _context;

        public ProductsController(CatalogContext context)
        {
            _context = context;
        }

        /// <summary>
        /// TODO : Documentation
        /// </summary>
        /// <response code="200">List of all products</response>
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _context.Product.ToListAsync();
        }

        /// <summary>
        /// Gets information of a given product
        /// </summary>
        /// <param name="id">Identifier of the product to retreive</param>
        /// <response code="200">A product with the given id has been successfully found</response>
        /// <response code="404">No product found with the given ID</response>
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int? id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        /// <summary>
        /// TODO : Documentation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productDto">Information to update (Brand and Type are not alterable.)</param>
        /// <response code="400"></response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int? id, ProductUpdateDto productDto)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            productDto.CopyTo(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
        /// TODO : Documentation
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductCreateDto productDto)
        {
            var brand = await _context.Brand.FindAsync(productDto.BrandId);
            var type = await _context.ProductType.FindAsync(productDto.TypeId);

            if(brand==null || type==null)
            {
                return BadRequest("Brand or type not found");
            }
            var product = productDto.ToProduct(brand, type);
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        /// <summary>
        /// TODO : Documentation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int? id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
