using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Interface;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.API;
using la_mia_pizzeria_static.Models.FormModel;
using la_mia_pizzeria_static.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace la_mia_pizzeria_static.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaApiController : ControllerBase
    {
        private readonly IRepository<PizzaItem> _efRepo;

        public PizzaApiController(IRepository<PizzaItem> efRepo)
        {
            _efRepo = efRepo;
        }


        //ROUTE API

        [HttpGet]
        public IActionResult GetPizzas()
        {
            ApiModel model = new();
            try
            {
                model.Data = _efRepo.GetAll();
                model.Success = true;
                return Ok(model);
            }
            catch (Exception ex)
            {

                model.Success = false;
                model.Message = ex.Message;
                return BadRequest(model);
            }
        }

        [HttpGet]
        public IActionResult GetPizza(int id = 0, string name = "")
        {
            ApiModel model = new();

            if (id != 0)
            {
                try
                {
                    model.Data = _efRepo.GetById(id);
                    model.Success = true;
                    if (model.Data == null)
                    {
                        model.Message = "Nessun item corrisponde all'id";
                        model.Success = false;
                    }
                    return Ok(model);
                }
                catch (Exception ex)
                {

                    model.Success = false;
                    model.Message = ex.Message;
                    return BadRequest(model);
                }
            }

            if (name != "")
            {
                try
                {
                    using (PizzaContext db = new())
                    {
                        model.Data = db.Pizzas.Where(p => p.Name.Contains(name)).FirstOrDefault();
                        model.Success = true;
                        return Ok(model);
                    }

                }
                catch (Exception ex)
                {
                    model.Success = false;
                    model.Message = ex.Message;
                    return BadRequest(model);
                }
            }

            model.Message = "Specificare un filtro di ricerca";
            return BadRequest(model);

        }

        [HttpPost, ActionName("Create")]
        public IActionResult Post([FromBody] PizzaFormModel form)
        {
            ApiModel model = new();
            if (form.IngredientsId.Count() < 0)
                return UnprocessableEntity();

            form.Pizza.Ingredients = new();
            using (PizzaContext db = new())
            {

                foreach (var id in form.IngredientsId)
                {
                    Ingredient toAdd = ((EntityFrameworkRepository<PizzaItem, PizzaContext>)_efRepo)._context.Ingredients.Find(id);
                    if (toAdd != null)
                        form.Pizza.Ingredients.Add(toAdd);
                }

                if (_efRepo.Add(form.Pizza))
                    return Ok();

                return BadRequest();

            }



        }

        [HttpPut("{id}"), ActionName("Edit")]
        public IActionResult Put(int id, [FromBody] PizzaFormModel model)
        {
            model.Pizza.PizzaItemId = id;

            model.Pizza.Ingredients = new();
            using (PizzaContext db = new())
            {
                PizzaItem pizzaToUpdate = db.Pizzas.Include(p => p.Ingredients).FirstOrDefault(p => p.PizzaItemId == id);

                if (pizzaToUpdate == null)
                    return BadRequest();


                if (model.IngredientsId?.Count() > 0)
                {
                    pizzaToUpdate.Ingredients?.Clear();
                    foreach (var ingredient in model.IngredientsId)
                    {
                        Ingredient toAdd = db.Ingredients.Find(ingredient);
                        if (toAdd != null)
                            pizzaToUpdate.Ingredients.Add(toAdd);
                    }

                }

                try
                {
                    EntityEntry<PizzaItem> updatePizza = db.Entry(pizzaToUpdate);
                    updatePizza.CurrentValues.SetValues(model.Pizza);
                    db.SaveChanges();
                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }

            }

        }
    }
}
