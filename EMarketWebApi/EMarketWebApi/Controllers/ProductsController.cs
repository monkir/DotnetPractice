using EMarketWebApi.Db.Context;
using EMarketWebApi.Db.Entity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMarketWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private EMarketContext _db;
        public ProductsController(EMarketContext db)
        {
            _db = db;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            try
            {
                var productList = _db.Products.ToList();
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            try
            {
                var productObj = _db.Products.SingleOrDefault(x=>x.Id==id);
                if (productObj == null)
                {
                    return NotFound("Product not found");
                }
                return Ok(productObj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ProductsController>
        [HttpPost]
        public ActionResult Post([FromBody] Product value)
        {
            try
            {
                _db.Products.Add(value);
                int changes = _db.SaveChanges();
                if (changes > 0)
                {
                    return Ok(new {value.Id});
                }
                else
                {
                    return BadRequest(new { Message = "Product not added" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product value)
        {
            try
            {
                var exProduct = _db.Products.SingleOrDefault(x => x.Id == id);
                if(exProduct == null)
                {
                    return NotFound("Product not found");
                }
                exProduct.Name = value.Name;
                exProduct.Description = value.Description;
                exProduct.Price = value.Price;
                exProduct.ManufacturingDate = value.ManufacturingDate;
                exProduct.ExpireDate = value.ExpireDate;
                _db.Update(exProduct);
                int changes = _db.SaveChanges();
                if (changes > 0)
                {
                    return Ok(new { Message = "Product updated successfully" });
                }
                else
                {
                    return BadRequest(new { Message = "Product not updated" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var exProduct = _db.Products.SingleOrDefault(x => x.Id == id);
                if (exProduct == null)
                {
                    return NotFound("Product not found");
                }
                _db.Remove(exProduct);
                int changes = _db.SaveChanges();
                if (changes > 0)
                {
                    return Ok(new { Message = "Product deleted successfully" });
                }
                else
                {
                    return BadRequest(new { Message = "Product not deleted" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
