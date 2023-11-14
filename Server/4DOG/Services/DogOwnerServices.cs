using _4DOG.Data;
using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _4DOG.Services
{
    public class DogOwnerServices
    {
        // משתנה שמחזיר את כל הרשומות מהטבלה 
        private readonly _4DogsDBContext m_db;

        // בנאי 
        public DogOwnerServices(_4DogsDBContext db) 
        {
            m_db = db;
        }

        // ----------  שליפת כל הנתונים -------------
        public List<DogOwnerDTO> GetDogOwneServices()
        {
            var e = m_db.DogOwner.Include(i => i.DogRace).Select
                (ee => new DogOwnerDTO()
                {
                    Id = ee.Id,
                    User = ee.Users.FirstName,
                    DogRace = ee.DogRace.RaceName,
                    UserId = ee.UserId,
                    DogGender = ee.DogGender,
                    DogSize = ee.DogSize,
                    DogRaceId = ee.DogRaceId,
                    DogName = ee.DogName,
                    Castrated = ee.Castrated
                }).ToList();
      
            e = m_db.DogOwner.Include(i => i.Users).Select
                 (ee => new DogOwnerDTO()
                 {
                     Id = ee.Id,
                     User = ee.Users.FirstName,
                     UserId = ee.UserId,
                     DogGender = ee.DogGender,
                     DogSize = ee.DogSize,
                     DogRaceId = ee.DogRaceId,
                     DogName = ee.DogName,
                     Castrated = ee.Castrated,
                     DogRace = ee.DogRace.RaceName
                 }).ToList();
            return e;
        }

        // ---------- שליפת כל הנתונים ע"י מס מזהה -------------
        public DogOwnerDTO GetDogOwnerServicesId(int id)
        {    
            return m_db.DogOwner.Where(DogOwner => DogOwner.Id == id).Select(e => new DogOwnerDTO()
            {
                Id = e.Id,
                User = e.Users.FirstName,
                UserId = e.UserId,
                DogGender = e.DogGender,
                DogSize = e.DogSize,
                DogRaceId = e.DogRaceId,
                DogName = e.DogName,
                Castrated = e.Castrated,
                DogRace = e.DogRace.RaceName
         
            }).FirstOrDefault();         
        }

        // ------------ הוספה  ----------------
        public bool AddDogOwnerServices(DogOwnerDTO user)
        {
            // מעתיק את הנתונים ממחלקת itemDTO וממפה אותם
            DogOwner newUser = new DogOwner();
            newUser.UserId = user.UserId;
            newUser.DogGender = user.DogGender;
            newUser.DogSize = user.DogSize;
            newUser.DogRaceId = user.DogRaceId;
            newUser.DogName = user.DogName;
            newUser.Castrated = user.Castrated;

            m_db.DogOwner.Add(newUser);
            int c = m_db.SaveChanges(); // שומר את מה שהכנסנו
            return c > 0;
        }

        // שליפת נתוני בעל הכלב עי מס מזהה למחיקה/עדכון
        public DogOwner getDogOwnerForDelUpd(int id)
        {
            return m_db.DogOwner.Where(newDogOwner => newDogOwner.Id == id).FirstOrDefault();
        }

        // שליפת נתוני בעל הכלב עי מס מזהה המשתמש
        public DogOwner getDogOwnerByUserId(int userid)
        {
            return m_db.DogOwner.Where(newDogOwner => newDogOwner.UserId == userid).FirstOrDefault();
        }

        // ------------- מחיקת משתמש בעל הכלב --------------
        public ResponseDTO DeleteDogOwnerServices(int id)
        {
            /*  שליפת ישות מהמאגר הנתונים למחיקה */
            DogOwner DogOwnerToDelete = getDogOwnerForDelUpd(id);

            if (DogOwnerToDelete == null) // קיימת במאגר הנתונים(ID) בדיקה האם הישות  
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"DogOwner with id: {id} Not found in Data base!"
                };
            }

            // Remove מחיקת הישות ממאגר הנתונים על ידי פונקציית 
            m_db.DogOwner.Remove(DogOwnerToDelete);
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

        // ----------- עדכון משתמש - בעל הכלב -------------
        public ResponseDTO UpdateDogOwnerServices(int id, DogOwnerDTO user)
        {
            DogOwner DogOwnerFromDB = getDogOwnerForDelUpd(id); // שולח ישות ממאגר הנתונים

            if (DogOwnerFromDB == null)
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"DogOwner {user.UserId} With id {id} not found in DB"
                };
            }
            DogOwnerFromDB.UserId = user.UserId;
            DogOwnerFromDB.DogGender = user.DogGender;
            DogOwnerFromDB.DogSize = user.DogSize;
            DogOwnerFromDB.DogRaceId = user.DogRaceId;
            DogOwnerFromDB.DogName = user.DogName;
            DogOwnerFromDB.Castrated = user.Castrated;

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
