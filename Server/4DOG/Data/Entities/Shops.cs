using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.Entities
{
    [Table("Shops")] // שם הטבלה בבסיס הנתונים
    public class Shops
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Aaddress")]
        public string Aaddress { get; set; }

        [Column("NameShop")]
        public string NameShop { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        [Column("CityId")]
        public int CityId { get; set; }

        // מפתח זר
        public virtual City City { get; set; }
    }
}
