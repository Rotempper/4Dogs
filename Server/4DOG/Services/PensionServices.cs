using _4DOG.Data;
using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _4DOG.Services
{
    public class PensionServices
    {
        // משתנה שמחזיר את כל הרשומות מהטבלה 
        private readonly _4DogsDBContext m_db;

        // בנאי 
        public PensionServices(_4DogsDBContext db) 
        {
            m_db = db;
        }

        // ----------  שליפת כל הנתונים -------------
        public List<PensionDTO> GetPensionServices()
        {
            var e = m_db.Pension.Include(i => i.Users).Select
                (ee => new PensionDTO()
                {
                    Id = ee.Id,
                    Users = ee.Users.FirstName,
                    UserId = ee.UserId,
                    NameBusiness = ee.NameBusiness,
                    Aaddress = ee.Aaddress,
                    PricePerForNight = ee.PricePerForNight,
                    About = ee.About,
                    Phone = ee.Phone,
                    CityId = ee.CityId
                }).ToList();

            e = m_db.Pension.Include(i => i.City).Select
             (ee => new PensionDTO()
             {
                 Id = ee.Id,
                 Users = ee.Users.FirstName,
                 UserId = ee.UserId,
                 NameBusiness = ee.NameBusiness,
                 city = ee.City.CityName,
                 Aaddress = ee.Aaddress,
                 PricePerForNight = ee.PricePerForNight,
                 About = ee.About,
                 Phone = ee.Phone,
                 CityId = ee.CityId
             }).ToList();

            return e;
        }

        // ---------- שליפת כל הנתונים ע"י מס מזהה -------------
        public PensionDTO GetPensionServicesId(int id)
        {
            return m_db.Pension.Where(Pension => Pension.Id == id).Select(e => new PensionDTO()
            {
                Id = e.Id,
                Users = e.Users.FirstName,
                UserId = e.UserId,
                NameBusiness = e.NameBusiness,
                city = e.City.CityName,
                Aaddress = e.Aaddress,
                PricePerForNight = e.PricePerForNight,
                About = e.About,
                Phone = e.Phone,
                CityId = e.CityId
            }).FirstOrDefault();
        }

        // שליפת נתוני פנסיון ע"י מ"ס מזהה למחיקה/עדכון
        public Pension getPensionForDelUpd(int id)
        {
            return m_db.Pension.Where(newPension => newPension.Id == id).FirstOrDefault();
        }

        // שליפת אובייקט פנסיון
        public Pension getPensionByUserId(int userid)
        {
            return m_db.Pension.Where(Pension => Pension.UserId == userid).FirstOrDefault();
        }

        // ------------ הוספת משתמש- פנסיון ----------------
        public bool AddPensionServices(PensionDTO user)
        {
            // מעתיק את הנתונים ממחלקת itemDTO וממפה אותם
            Pension newUser = new Pension();

            newUser.UserId = user.UserId;
            newUser.NameBusiness = user.NameBusiness;
            newUser.Aaddress = user.Aaddress;
            newUser.PricePerForNight = user.PricePerForNight;
            newUser.About = user.About;
            newUser.Phone = user.Phone;
            newUser.CityId = user.CityId;

            m_db.Pension.Add(newUser);
            int c = m_db.SaveChanges(); // שומר את מה שהכנסנו
            return c > 0;
        }

        // ------------- מחיקת משתמש - פנסיון --------------
        public ResponseDTO DeletPensionServices(int id)
        {
            /*  שליפת ישות מהמאגר הנתונים למחיקה */
            Pension PensionToDelete = getPensionForDelUpd(id);

            if (PensionToDelete == null) // קיימת במאגר הנתונים(ID) בדיקה האם הישות  
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"Entity with id: {id} Not found in Data base!"
                };
            }

            // Remove מחיקת הישות ממאגר הנתונים על ידי פונקציית 
            m_db.Pension.Remove(PensionToDelete);
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

        // ---------- עדכון משתמש - פנסיון  ------------
        public ResponseDTO UpdatePensionServices(int id, PensionDTO user)
        {
            Pension PensionFromDB = getPensionForDelUpd(id); // שולח ישות ממאגר הנתונים

            if (PensionFromDB == null)
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"Item {user.NameBusiness} With id {id} not found in DB"
                };
            }

            PensionFromDB.UserId = user.UserId;
            PensionFromDB.NameBusiness = user.NameBusiness;
            PensionFromDB.Aaddress = user.Aaddress;
            PensionFromDB.PricePerForNight = user.PricePerForNight;
            PensionFromDB.About = user.About;
            PensionFromDB.Phone = user.Phone;

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
