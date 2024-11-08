using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductRepository _repository;

        public ProductController()
        {
            _repository = new ProductRepository();
        }

        // GET: api/Product
        [HttpGet]
        public IActionResult Get()
        {
            var products = _repository.Read() ?? new List<Product>(); // Asegúrate de que siempre es una lista
            return Ok(products);
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("El producto no puede ser nulo.");
            }

            _repository.Create(product);

            // Recuperar el producto recién creado desde la base de datos para asegurar que contiene todos los datos
            var createdProduct = new Product
            {
                Id = product.Id, // Asignado en el repositorio
                NameP = product.NameP,
                Price = product.Price,
                Stock = product.Stock
            };

            return Ok(createdProduct); // Devuelve el producto con todos los campos
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingProduct = _repository.Read().FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound(); // Devuelve 404 si el producto no existe
            }

            _repository.Delete(id);
            return Ok("Producto eliminado exitosamente.");
        }

        // GET: /Product/Edit/{id}
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var product = _repository.Read().FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); // Devuelve 404 si el producto no existe
            }

            return View(product); // Devuelve la vista de edición con el producto
        }
        // PUT: api/Product
        [HttpPut]
        public IActionResult Update([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(product);
                return Ok(product); // Devuelve el producto actualizado
            }

            return BadRequest(ModelState); // Devuelve los errores de validación
        }
        [HttpGet("/Product/List")]
        public IActionResult List()
        {
            var products = _repository.Read();
            return View(products); // Asegúrate de que el nombre coincida con el nombre de la vista
        }


        //// POST: /Product/Update
        //[HttpPost("Update")]
        //public IActionResult Update([FromBody] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _repository.Update(product);
        //        return RedirectToAction("Index"); // Redirige a la lista de productos
        //    }

        //    return View("Edit", product); // Vuelve a la vista de edición si hay un error
        //}

        // GET: /Product/Index
        [HttpGet("/Product/Index")]
        public IActionResult Index()
        {
            var products = _repository.Read();
            return View(products);
        }
        // GET: /Product/List
       

    }
}
