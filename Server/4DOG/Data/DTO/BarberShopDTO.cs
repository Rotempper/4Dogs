using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.DTO
{
    public class BarberShopDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Aaddress { get; set; }
        public string NameBarberShop { get; set; }
        public string About { get; set; }
        public string Phone { get; set; }
        public int CityId { get; set; }

        // עמודות למפתח זר
        public string User { get; set; }
        public string city { get; set; }
    }
}
