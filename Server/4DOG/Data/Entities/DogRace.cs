using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.Entities
{
    [Table("DogRace")] // שם הטבלה בבסיס הנתונים
    public class DogRace
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("RaceName")]
        public string RaceName { get; set; }

        // רשימה המקשרת בין המפתחות
        public virtual ICollection<DogOwner> ListDogOwner { get; set; }
    }
}
