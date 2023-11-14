using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.Entities
{
    [Table("Haircuts")] // שם הטבלה בבסיס הנתונים
    public class Haircuts
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("DogOwnerId")]
        public int DogOwnerId { get; set; }

        [Column("Ddate")]
        public DateTime Ddate { get; set; }

        [Column("Hhour")]
        public string Hhour { get; set; }

        [Column("BarbershopId")]
        public int BarbershopId { get; set; }

        [Column("TypesHaircutId")]
        public int TypesHaircutId { get; set; }

        // מפתח זר
        public virtual DogOwner DogOwner { get; set; }
        public virtual BarberShop BarberShop { get; set; }
        public virtual TypesHaircut TypesHaircut { get; set; }
    }
}
