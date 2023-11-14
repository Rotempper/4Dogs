using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.Entities
{
    [Table("TrainingPackage")] // שם הטבלה בבסיס הנתונים
    public class TrainingPackage
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("TrainingName")]
        public string TrainingName { get; set; }

        [Column("Ddescription")]
        public string Ddescription { get; set; }

        [Column("Price")]
        public int Price { get; set; }

        [Column("TrainingId")]
        public int TrainingId { get; set; }
        public virtual Training Training { get; set; }

        // רשימה המקשרת בין המפתחות
        public virtual ICollection<DogTraining> ListDogTraining { get; set; }
       

    }
}
