using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.DTO
{
    public class TrainingPackageDTO
    {
        public int Id { get; set; }
        public string TrainingName { get; set; }
        public string Ddescription { get; set; }
        public int Price { get; set; }
        public int TrainingId { get; set; }
        public string  NameBusiness{ get; set; }
    }
}
