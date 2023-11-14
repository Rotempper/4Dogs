using _4DOG.Data;
using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _4DOG.Services
{
    public class BarberShopServices
    {
        private readonly _4DogsDBContext m_db;

        // בנאי 
        public BarberShopServices(_4DogsDBContext db) 
        {
            m_db = db;
        }

        // ----------  שליפת כל הנתונים -------------
        public List<BarberShopDTO> GetBarberShopServices()
        {
            var e = m_db.BarberShop.Include(i => i.User).Select
                (ee => new BarberShopDTO()
                {
                    Id = ee.Id,
                    User = ee.User.FirstName,
                    UserId = ee.UserId,
                    Aaddress = ee.Aaddress,
                    NameBarberShop = ee.NameBarberShop,
                    About = ee.About,
                    Phone = ee.Phone,
                    CityId = ee.CityId
                }).ToList();

            e = m_db.BarberShop.Include(i => i.City).Select
                 (ee => new BarberShopDTO()
                 {
                     Id = ee.Id,
                     User = ee.User.FirstName,
                     city = ee.City.CityName,
                     UserId = ee.UserId,
                     Aaddress = ee.Aaddress,
                     NameBarberShop = ee.NameBarberShop,
                     About = ee.About,
                     Phone = ee.Phone,
                     CityId = ee.CityId
                 }).ToList();
            return e;
        }

        // ---------- שליפת כל הנתונים ע"י מס מזהה -------------
        public BarberShopDTO GetBarberShopServicesId(int id)
        {
            return m_db.BarberShop.Where(BarberShop => BarberShop.Id == id).Select(e => new BarberShopDTO()
            {
                Id = e.Id,
                User = e.User.FirstName,
                city = e.City.CityName,
                UserId = e.UserId,
                Aaddress = e.Aaddress,
                NameBarberShop = e.NameBarberShop,
                About = e.About,
                Phone = e.Phone,
                CityId = e.CityId
            }).FirstOrDefault();
        }

        // ---------- שליפת מס מזהה מספרה ע"י מס מזהה של משתמש -------------
        public int getBarberShopIdByUserId(int userid)
        {
            BarberShop barberShop = m_db.BarberShop.Where(newBarberShop => newBarberShop.UserId == userid).FirstOrDefault();
            return barberShop.Id;
        }

        // שליפת נתוני מספרה ע"י מ"ס מזהה למחיקה/עדכון
        public BarberShop getBarberShopForDelUpd(int id)
        {
            return m_db.BarberShop.Where(newBarberShop => newBarberShop.Id == id).FirstOrDefault();
        }

        // שליפת אובייקט מספרה
        public BarberShop getBarberShopByUserIdObj(int userid)
        {
            return m_db.BarberShop.Where(BarberShop => BarberShop.UserId == userid).FirstOrDefault();
        }

        // ------------ הוספה  ----------------
        public bool AddBarberShopServices(BarberShopDTO user)
        {
            // מעתיק את הנתונים ממחלקת itemDTO וממפה אותם
            BarberShop newUser = new BarberShop();

            newUser.UserId = user.UserId;
            newUser.Aaddress = user.Aaddress;
            newUser.NameBarberShop = user.NameBarberShop;
            newUser.About = user.About;
            newUser.Phone = user.Phone;
            newUser.CityId = user.CityId;

            m_db.BarberShop.Add(newUser);
            int c = m_db.SaveChanges(); // שומר את מה שהכנסנו
            return c > 0;
        }

        // ------------- מחיקת נתוני מספרה --------------
        public ResponseDTO DeleteBarberShopServices(int id)
        {
            /*  שליפת ישות מהמאגר הנתונים למחיקה */
            BarberShop BarberShopToDelete = getBarberShopForDelUpd(id);

            if (BarberShopToDelete == null) // קיימת במאגר הנתונים(ID) בדיקה האם הישות  
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"BarberShop with id: {id} Not found in Data base!"
                };
            }

            // Remove מחיקת הישות ממאגר הנתונים על ידי פונקציית 
            m_db.BarberShop.Remove(BarberShopToDelete);

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

        // ------------- עדכון משתמש - מספרה ----------------
        public ResponseDTO UpdateBarberShopServices(int id, BarberShopDTO user)
        {
            BarberShop BarberShopFromDB = getBarberShopForDelUpd(id); // שולח ישות ממאגר הנתונים

            if (BarberShopFromDB == null)
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"BarberShop {user.UserId} With id {id} not found in DB"
                };
            }

            BarberShopFromDB.UserId = user.UserId;
            BarberShopFromDB.Aaddress = user.Aaddress;
            BarberShopFromDB.NameBarberShop = user.NameBarberShop;
            BarberShopFromDB.About = user.About;
            BarberShopFromDB.Phone = user.Phone;
            BarberShopFromDB.CityId = user.CityId;

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
