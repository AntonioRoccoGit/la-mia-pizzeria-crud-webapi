using la_mia_pizzeria_static.ValidationAttributes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    [Table("pizza")]

    public class PizzaItem
    {
        [Column("id")]
        public int PizzaItemId { get; set; }

        [Column("name"), MaxLength(50, ErrorMessage ="Lunghezza massima 50 caratteri")]
        public string Name { get; set; }

        [Column("description"), MaxLength(255, ErrorMessage = "Lunghezza massima 255 caratteri"), MoreThenFiveWords()]
        public string Description { get; set; }

        [Column("thumbnail"), MaxLength(255, ErrorMessage = "Lunghezza massima 255 caratteri")]
        public string Thumbnail { get; set; }

        [Column("price")] 
        [Range(1, 100, ErrorMessage ="Inserisci un prezzo valido")]
        public double Price { get; set; }
      

        //Relations
        [Column("category_id")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }


        public List<Ingredient>? Ingredients { get; set; }


        public PizzaItem() { }
        public PizzaItem(string name, string description, string thumbnail, double price)
        {
            Name = name;
            Description = description;
            Thumbnail = thumbnail;
            Price = price;
        }


        public string GetStringPrice()
        {
            return Price.ToString("0.00");
        }
    }
}
