using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.Entities
{
    [Table("Training")] // שם הטבלה בבסיס הנתונים
    public class Training
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

        [Column("NameBusiness")]
        public string NameBusiness { get; set; }

        [Column("Aaddress")]
        public string Aaddress { get; set; }

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
        public virtual ICollection<DogTraining> ListDogTraining { get; set; }
        public virtual ICollection<TrainingPackage> ListTrainingPackage { get; set; }

    }
}
