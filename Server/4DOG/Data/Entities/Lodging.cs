using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.Entities
{
    [Table("Lodging")] // שם הטבלה בבסיס הנתונים
    public class Lodging
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("DogOwnerId")]
        public int DogOwnerId { get; set; }

        [Column("StartDate")]
        public DateTime StartDate { get; set; }

        [Column("EndDate")]
        public DateTime EndDate { get; set; }

        [Column("PensionId")]
        public int PensionId { get; set; }

        // מפתח זר
        public virtual DogOwner DogOwner { get; set; }
        public virtual Pension Pension { get; set; }
    }
}
