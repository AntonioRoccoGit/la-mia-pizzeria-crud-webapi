using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Interface;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.FormModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILoggerMs _logger;
        public HomeController(ILoggerMs log)
        {
            this._logger = log;
        }
        public IActionResult Index()
        {
            List<PizzaItem> pizzas = new();
            using (PizzaContext db = new())
            {
                pizzas = db.Pizzas.ToList<PizzaItem>();
            }
            this._logger.Log("Accesso pagina home/index");
            return View(pizzas);
        }


        public IActionResult PizzaDetails(int id)
        {
            PizzaFormModel model = new PizzaFormModel();
            using (PizzaContext db = new())
            {
                PizzaItem? pizza = db.Pizzas.Include(p => p.Ingredients).FirstOrDefault(p => p.PizzaItemId == id);
                if (pizza == null)
                    return View("ProductNotFound");
                model.Pizza = pizza;
            }

            _logger.Log($"Visualizzati dettagli pizza id {model.Pizza.PizzaItemId}");
            return View(model);
        }

    }
}
