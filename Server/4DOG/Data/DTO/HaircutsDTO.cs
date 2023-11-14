using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.DTO
{
    public class HaircutsDTO
    {
        public int Id { get; set; }
        public int DogOwnerId { get; set; }
        public DateTime Ddate { get; set; }
        public string Hhour { get; set; }
        public int BarbershopId { get; set; }
        public int TypesHaircutId { get; set; }

        // עמודות למפתח זר
        public int DogOwnerUserId { get; set; }
        public string BarberShopName { get; set; }
        public string TypesHaircutName { get; set; }
    }
}
