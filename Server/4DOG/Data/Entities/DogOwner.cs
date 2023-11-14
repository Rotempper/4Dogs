using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.Entities
{
    [Table("DogOwner")] // שם הטבלה בבסיס הנתונים
    public class DogOwner
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

        [Column("DogGender")]
        public bool DogGender { get; set; }

        [Column("DogSize")]
        public string DogSize { get; set; }

        [Column("DogRaceId")]
        public int DogRaceId { get; set; }

        [Column("DogName")]
        public string DogName { get; set; }

        [Column("Castrated")]
        public bool Castrated { get; set; }        

        // מפתח זר המגיע מהטבלה המשנית
        public virtual DogRace DogRace { get; set; }
        public virtual Users Users { get; set; }

        // רשימה המקשרת בין המפתחות
        public virtual  ICollection<Lodging> ListLodging { get; set; }
        public virtual ICollection<Haircuts> ListHaircuts { get; set; }
        public virtual ICollection<DogTraining> ListDogTraining { get; set; }
    }
}
