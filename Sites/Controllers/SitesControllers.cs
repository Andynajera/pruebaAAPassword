using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AAPASSWORD.Password.Sites;


namespace AAPASSWORD.Password.Sites.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SitesControllers : ControllerBase
    {
        private static List<SiteItems> Categories = new List<SiteItems>{
            new SiteItems { Id=1 ,Name="Najera",UserName = "Andrea", Password = "123", IdCategory=2},
            new SiteItems { Id=2 ,Name="Andrea",UserName = "Najera", Password = "123", IdCategory=3},
        };
        
    [HttpGet]
    public ActionResult<List<SiteItems>> Get(){
        return Ok(Categories);
    }

    [HttpGet]
    [Route("{Id}")]
    public ActionResult<SiteItems> Get (int Id){
        var categoriesItem = Categories.Find (x =>x.Id == Id);
        return categoriesItem == null ? NotFound() : Ok(categoriesItem);
    }

    [HttpPost]
    public ActionResult Post (SiteItems categoriesItem){
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
    public ActionResult Put (SiteItems categoriesItem){
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
    public ActionResult<SiteItems> Delete (int Id){
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
