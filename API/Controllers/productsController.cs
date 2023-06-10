using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Website.Models;
using E_Website.Models.Data;
using E_Website.Models.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using NuGet.Packaging.Signing;

namespace E_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    

    public class productsController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly IMapper _mapper;
        private readonly string filePathUrl = @"C:\Users\RTX\Desktop\Angular and .Net\E-Website\Resourses\Images\Products";

        public productsController(ModelContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVM>>> GetProducts()
        {

            var products = await _context.Products.ToListAsync();
            var data = _mapper.Map<List<ProductVM>>(products);
            return Ok(data);
        }

        //[HttpGet("GetProductList")]
        //public async Task<ActionResult<IEnumerable<ProductVM>>> GetProductList()
        //{
        //    var products = await _context.Products.Select(x => new { x.productId,x.categoryId, x.name, x.price, x.quantity, x.description }).ToListAsync();
        //    return Ok(products);
        //}


        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVM>> Getproduct(string id)
        {
            var product = await _context.Products.FindAsync(id);

            var data = _mapper.Map<ProductVM>(product);

            if (product == null)
            {
                return NotFound();
            }

            return data;
        }


        // GET: api/products/5
        [HttpGet("GetProduct/{id}")]
        public async Task<ActionResult<ProductVM>> GetproductById(string id)
        {
            var product = await _context.Products.FindAsync(id);

            var data = _mapper.Map<ProductVM>(product);

            if (product == null)
            {
                return NotFound();
            }

            return data;
        }






        // PUT: api/products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putproduct(string id, [FromForm]product product)
        {
            var user = _context.Products.Where(x=>x.productId == id).Select(x=>x.image).FirstOrDefault();
            if (id != product.productId)
            {
                return BadRequest();
            }
            if(product.fileImg != null)
            {
                await RemoveFile(user);
                var image = await SaveImageFile(product.fileImg);
                product.image = image;
            }
            product.updatedAt = DateTime.Now.ToString();
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productExists(id))
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

        // POST: api/products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<product>> Postproduct([FromForm] product product)
        {
            if (product.fileImg != null)
            {
                var image = await SaveImageFile(product.fileImg);
                product.image = image;
            }
            else
            {
                product.image = "no-product-image.png";
            }


            product.productId = Guid.NewGuid().ToString();
            product.categoryId = product.categoryId;
            product.createdAt = DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
            _context.Products.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (productExists(product.productId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getproduct", new { id = product.productId }, product);
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]

        public async Task<IActionResult> Deleteproduct(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            if(product.image != null) {
                await RemoveFile(product.image);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [NonAction]
        public async Task<string> SaveImageFile(IFormFile file)
        {

            string imageName = Guid.NewGuid().ToString() + ".jpg";
            string path = Path.Combine(filePathUrl, imageName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            };
            return imageName;
        }

        private bool productExists(string id)
        {
            return _context.Products.Any(e => e.productId == id);
        }

        [NonAction]
        public async Task<Boolean> RemoveFile(string fileName)
        {
            string path = Path.Combine(filePathUrl, fileName);
            if (System.IO.File.Exists(path))
            {
                  System.IO.File.Delete(path);
                return true;
            }
            return false;

        }


    }
}
