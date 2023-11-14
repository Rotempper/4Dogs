using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.DTO
{
    public class DogOwnerDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool DogGender { get; set; }
        public string DogSize { get; set; }
        public int DogRaceId { get; set; }
        public string DogName { get; set; }
        public bool Castrated { get; set; }

        // עמודות למפתח זר
        public string DogRace { get; set; }
        public string User { get; set; }
    }
}
