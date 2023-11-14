using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.DTO
{
    public class PensionDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string NameBusiness { get; set; }
        public string Aaddress { get; set; }
        public int PricePerForNight { get; set; }
        public string About { get; set; }
        public string Phone { get; set; }
        public int CityId { get; set; }
        // עמודות למפתח זר
        public string Users { get; set; }
        public string city { get; set; }
    }
}
