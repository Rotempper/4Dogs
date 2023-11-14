using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.Entities
{
    [Table("TypesHaircut")] // שם הטבלה בבסיס הנתונים
    public class TypesHaircut
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("HaircutName")]
        public string HaircutName { get; set; }

        [Column("Ddescription")]
        public string Ddescription { get; set; }

        [Column("Price")]
        public int Price { get; set; }

        [Column("BarberShopId")]
        public int BarberShopId { get; set; }
        
        // רשימה המקשרת בין המפתחות
        public virtual ICollection<Haircuts> ListHaircuts { get; set; }
        public virtual BarberShop BarberShop { get; set; }
    }
}
