using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Controllers
{
  public class CategoryController : ControllerBase
  {
    private readonly StoreDataContext _context;

    public CategoryController(StoreDataContext context)
    {
      _context = context;
    }

    [HttpGet]
    [Route("v1/categories")]
    [ResponseCache(Duration = 60)]
    public IEnumerable<Category> Get()
    {
      return _context.Categories.AsNoTracking().ToList();
    }

    [HttpGet]
    [Route("v1/categories/{id}")]
    public Category Get(int id)
    {
      return _context.Categories.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
    }

    [HttpGet]
    [Route("v1/categories/{id}/products")]
    public IEnumerable<Product> GetProducts(int id)
    {
      return _context.Products.AsNoTracking().Where(x => x.CategoryId == id).ToList();
    }

    [HttpPost]
    [Route("v1/categories")]
    public Category Post([FromBody]Category category)
    {
      _context.Categories.Add(category);
      _context.SaveChanges();

      return category;
    }

    [HttpPut]
    [Route("v1/categories")]
    public Category Put([FromBody]Category category)
    {
      _context.Entry<Category>(category).State = EntityState.Modified;
      _context.SaveChanges();

      return category;
    }

    [HttpDelete]
    [Route("v1/categories")]
    public Category Delete([FromBody]Category category)
    {
      _context.Categories.Remove(category);
      _context.SaveChanges();

      return category;
    }
  }
}