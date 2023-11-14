using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.DTO
{
    public class LodgingDTO
    {
        public int Id { get; set; }
        public int DogOwnerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PensionId { get; set; }

        // עמודות למפתח זר
        public int DogOwnerUserId { get; set; }
        public string PensionName { get; set; }
    }
}
