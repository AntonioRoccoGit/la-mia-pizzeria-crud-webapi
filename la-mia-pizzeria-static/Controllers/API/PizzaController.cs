using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Interface;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.API;
using la_mia_pizzeria_static.Models.FormModel;
using la_mia_pizzeria_static.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace la_mia_pizzeria_static.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IRepository<PizzaItem> _efRepo;

        public PizzaController(IRepository<PizzaItem> efRepo)
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

        [HttpPost]
        public IActionResult Post([FromBody] PizzaItem item)
        {
            ApiModel model = new();
            model.Success = _efRepo.Add(item);
            if (!model.Success)
                return UnprocessableEntity();

            return Ok(model.Success);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PizzaFormModel model)
        {
            model.Pizza.PizzaItemId = id;

            if (model.IngredientsId?.Count() > 0)
            {
                model.Pizza.Ingredients = new();
                using (PizzaContext db = new())
                {
                    foreach (var ingredient in model.IngredientsId)
                    {
                        Ingredient toAdd = db.Ingredients.Find(ingredient);
                        if (toAdd != null)
                            model.Pizza.Ingredients.Add(toAdd);
                    }
                }
            }
            try
            {
                _efRepo.Update(model.Pizza);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }
    }
}
