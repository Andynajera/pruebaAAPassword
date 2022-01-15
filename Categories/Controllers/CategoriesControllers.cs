using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AAPASSWORD.Password.Categories;


namespace AAPASSWORD.Password.Categories.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesControllers : ControllerBase
    {
        private static List<CategoriesItem> Categories = new List<CategoriesItem>{
            new CategoriesItem { Id=1 ,Name="uno",Description="description1"},
            new CategoriesItem { Id=2 ,Name="dos",Description="description2"}
        };
        
    [HttpGet]
    public ActionResult<List<CategoriesItem>> Get(){
        return Ok(Categories);
    }

    [HttpGet]
    [Route("{Id}")]
    public ActionResult<CategoriesItem> Get (int Id){
        var categoriesItem = Categories.Find (x =>x.Id == Id);
        return categoriesItem == null ? NotFound() : Ok(categoriesItem);
    }

    [HttpPost]
    public ActionResult Post (CategoriesItem categoriesItem){
        var existingCategoriesItem = Categories.Find(x=>x.Id== categoriesItem.Id);
        if (existingCategoriesItem != null){
            return Conflict("ya existe esa categoria");
        } else {
            Categories.Add(categoriesItem);
            var resourceUrl = Request.Path.ToString()+ "/" + categoriesItem.Id;
            return Created(resourceUrl, categoriesItem);
        }
        }

     [HttpPut]
    public ActionResult Put (CategoriesItem categoriesItem){
        var existingCategoriesItem = Categories.Find(x=>x.Id== categoriesItem.Id);
        if (existingCategoriesItem == null){
            return Conflict("no existe esa categoria");
        } else {
            existingCategoriesItem.Name = categoriesItem.Name;
            return Ok();
        }
        }
     [HttpDelete]
    [Route("{Id}")]
    public ActionResult<CategoriesItem> Delete (int Id){
        var categoriesItem = Categories.Find (x =>x.Id == Id);
        if (categoriesItem == null){
            return NotFound();
        } else{
            Categories.Remove(categoriesItem);
            return NoContent();
        }
    }
    



    };
     
}
