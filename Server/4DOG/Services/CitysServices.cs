using _4DOG.Data;
using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _4DOG.Services
{
    public class CitysServices
    {
        private readonly _4DogsDBContext m_db;

        // בנאי 
        public CitysServices(_4DogsDBContext db) 
        {
            m_db = db;
        }
       
        // ----------  שליפת כל הנתונים -------------
        public List<CitysDTO> GetCitysServices()
        {
            var e = m_db.Citys.IgnoreAutoIncludes().Select //  IgnoreAutoIncludes לשים לב לשינוי 
                (ee => new CitysDTO()
                {
                    Id = ee.Id,
                    CityName = ee.CityName
                }).OrderBy(f => f.CityName).ToList();
            return e;
        }

       

        // ---------- שליפת כל הנתונים ע"י מס מזהה -------------
        public CitysDTO GetCitysServicesId(int id)
        {
            return m_db.Citys.Where(Citys => Citys.Id == id).Select(e => new CitysDTO()
            {
                Id = e.Id,
                CityName = e.CityName
            }).FirstOrDefault();
        }

        // ------------ הוספה  ----------------
        public bool AddCitysServices(CitysDTO user)
        {
            // מעתיק את הנתונים ממחלקת itemDTO וממפה אותם
            City newUser = new City();
            newUser.CityName = user.CityName;

            m_db.Citys.Add(newUser);
            int c = m_db.SaveChanges(); // שומר את מה שהכנסנו
            return c > 0;
        }

        // שליפת נתוני ערים ע"י מס מזהה למחיקה/עדכון
        public City getCityForDelUpd(int id)
        {
            return m_db.Citys.Where(newCity => newCity.Id == id).FirstOrDefault();
        }

        // ------------- מחיקת נתוני ערים --------------
        public ResponseDTO DeleteCitysServices(int id)
        {
            /*  שליפת ישות מהמאגר הנתונים למחיקה */
            City CityToDelete = getCityForDelUpd(id);

            if (CityToDelete == null) // קיימת במאגר הנתונים(ID) בדיקה האם הישות  
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"City with id: {id} Not found in Data base!"
                };
            }

            // Remove מחיקת הישות ממאגר הנתונים על ידי פונקציית 
            m_db.Citys.Remove(CityToDelete);
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

        // ------------- עדכון משתמש - ערים ----------------
        public ResponseDTO UpdateCityServices(int id, CitysDTO user)
        {
            City CityFromDB = getCityForDelUpd(id); // שולח ישות ממאגר הנתונים

            if (CityFromDB == null)
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"City {user.CityName} With id {id} not found in DB"
                };
            }
            CityFromDB.CityName = user.CityName;
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
