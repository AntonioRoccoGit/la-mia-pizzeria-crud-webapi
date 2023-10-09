using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    [Table("category")]
    public class Category
    {
        [Column("id")]
        public int CategoryId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public List<PizzaItem> PizzaItems { get; set; }

        public Category() { }
    }
}
