using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.Entities
{
    [Table("BarberShop")] // שם הטבלה בבסיס הנתונים
    public class BarberShop
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

        [Column("Aaddress")]
        public string Aaddress { get; set; }

        [Column("NameBarberShop")]
        public string NameBarberShop { get; set; }

        [Column("About")]
        public string About { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        [Column("CityId")]
        public int CityId { get; set; }

        // מפתח זר
        public virtual Users User { get; set; }
        public virtual City City { get; set; }
        
        // רשימה המקשרת בין המפתחות
        public virtual ICollection<Haircuts> ListHaircuts { get; set; }
        public virtual ICollection<TypesHaircut> ListTypesHaircut { get; set; }
    }
}
