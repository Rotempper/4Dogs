using _4DOG.Data;
using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _4DOG.Services
{
    public class DogRaceServices
    {
        private readonly _4DogsDBContext m_db;

        // בנאי 
        public DogRaceServices(_4DogsDBContext db) 
        {
            m_db = db;
        }

        // ----------  שליפת כל הנתונים -------------
        public List<DogRaceDTO> GetDogRaceServices()
        {
            var e = m_db.DogRace.IgnoreAutoIncludes().Select //  IgnoreAutoIncludes לשים לב לשינוי 
                (ee => new DogRaceDTO()
                {
                    Id = ee.Id,
                    RaceName = ee.RaceName
                }).ToList();
            return e;
        }

        // ---------- שליפת כל הנתונים ע"י מס מזהה -------------
        public DogRaceDTO GetDogRaceServicesId(int id)
        {
            return m_db.DogRace.Where(DogRace => DogRace.Id == id).Select(e => new DogRaceDTO()
            {
                Id = e.Id,
                RaceName = e.RaceName
            }).FirstOrDefault();
        }

        // ------------ הוספת גזע ----------------
        public bool AddDogRaceServices(DogRaceDTO user)
        {
            // מעתיק את הנתונים ממחלקת itemDTO וממפה אותם
            DogRace newUser = new DogRace();
            newUser.RaceName = user.RaceName;

            m_db.DogRace.Add(newUser);
            int c = m_db.SaveChanges(); // שומר את מה שהכנסנו
            return c > 0;
        }

        // שליפת נתוני גזע ע"י מ"ס מזהה למחיקה/עדכון
        public DogRace getDogRaceForDelUpd(int id)
        {
            return m_db.DogRace.Where(newDogRace => newDogRace.Id == id).FirstOrDefault();
        }

        // ------------- מחיקת משתמש - גזע --------------
        public ResponseDTO DeleteDogRaceServices(int id)
        {
            /*  שליפת ישות מהמאגר הנתונים למחיקה */
            DogRace DogRaceToDelete = getDogRaceForDelUpd(id);

            if (DogRaceToDelete == null) // קיימת במאגר הנתונים(ID) בדיקה האם הישות  
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"DogRace with id: {id} Not found in Data base!"
                };
            }

            // Remove מחיקת הישות ממאגר הנתונים על ידי פונקציית 
            m_db.DogRace.Remove(DogRaceToDelete);
            int c = m_db.SaveChanges();
            ResponseDTO response = new ResponseDTO();

            if (c > 0) // תגובות
            {
                response.Status = StatosCode.Success;
            }
            else
            {
                response.Status = StatosCode.Error;
            }
            return response;
        }

        // ------------ עדכון משתמש - גזע -------------
        public ResponseDTO UpdateDogRaceServices(int id, DogRaceDTO user)
        {
            DogRace DogRaceFromDB = getDogRaceForDelUpd(id); // שולח ישות ממאגר הנתונים

            if (DogRaceFromDB == null)
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"DogRace {user.RaceName} With id {id} not found in DB"
                };
            }

            DogRaceFromDB.RaceName = DogRaceFromDB.RaceName;
            int c = m_db.SaveChanges();
            ResponseDTO response = new ResponseDTO();

            if (c > 0)
            {
                response.Status = StatosCode.Success;
            }
            else
            {
                response.Status = StatosCode.Error; // אם לא קיים יחזיר הודעת שגיאה
                response.StatusText = $"ERROR";
            }
            return response;
        }
    }
}
