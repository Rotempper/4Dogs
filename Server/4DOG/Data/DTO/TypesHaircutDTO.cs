using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.DTO
{
    public class TypesHaircutDTO
    {
        public int Id { get; set; }
        public string HaircutName { get; set; }
        public string Ddescription { get; set; }
        public int Price { get; set; }
        public int BarberShopId { get; set; }

        // עמודות למפתח זר
        public string BarberShopName { get; set; }

    }
}
