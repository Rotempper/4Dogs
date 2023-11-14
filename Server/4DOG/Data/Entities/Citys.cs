using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.Entities
{
    [Table("Citys")] // שם הטבלה בבסיס הנתונים
    public class City
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("CityName")]
        public string CityName { get; set; }

        // רשימה המקשרת בין המפתחות
        public virtual ICollection<Shops> ListShop { get; set; }
        public virtual ICollection<BarberShop> ListBarberShop { get; set; }
        public virtual ICollection<Training> ListTraining { get; set; }
        public virtual ICollection<Pension> ListPension { get; set; }
    }
}
