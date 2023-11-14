using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.DTO
{
    public class ShopsDTO
    {
        public int Id { get; set; }
        public string Aaddress { get; set; }
        public string NameShop { get; set; }
        public string Phone { get; set; }
        public int CityId { get; set; }

        // עמודה חדשה למפתח זר
        public string City { get; set; }
    }
}
