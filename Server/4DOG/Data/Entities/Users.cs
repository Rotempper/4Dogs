using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.Entities
{
    [Table("Users")] // שם הטבלה בבסיס הנתונים
    public class Users
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        [Column("Role")]
        public string Role { get; set; }

        // רשימה המקשרת בין המפתחות
        public virtual ICollection<Pension> ListPension { get; set; }
        public virtual ICollection<DogOwner> ListDogOwner { get; set; }
        public virtual ICollection<BarberShop> ListBarberShop { get; set; }
        public virtual ICollection<Training> ListTraining { get; set; }
    }
}
