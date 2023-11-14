using _4DOG.Data;
using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _4DOG.Services
{
    public class ShopsServices
    {
        // משתנה שמחזיר את כל הרשומות מהטבלה 
        private readonly _4DogsDBContext m_db;

        // בנאי 
        public ShopsServices(_4DogsDBContext db) 
        {
            m_db = db;
        }

        // ----------  שליפת כל הנתונים -------------
        public List<ShopsDTO> GetShopsServices()
        {
            var e = m_db.Shops.Include(i => i.City).Select
                (ee => new ShopsDTO()
                {
                    Id = ee.Id,
                    City = ee.City.CityName,
                    Aaddress = ee.Aaddress,
                    NameShop = ee.NameShop,
                    Phone = ee.Phone,
                    CityId = ee.CityId
                }).OrderBy(f => f.City).ToList(); // מיון ערים
            return e;
        }

       

        // ---------- שליפת כל הנתונים ע"י מס מזהה -------------
        public ShopsDTO GetShopsServicesId(int id)
        {
            return m_db.Shops.Where(Shops => Shops.Id == id).Select(e => new ShopsDTO()
            {
                Id = e.Id,
                City = e.City.CityName,
                Aaddress = e.Aaddress,
                NameShop = e.NameShop,
                Phone = e.Phone,
                CityId = e.CityId
            }).FirstOrDefault();
        }

        // ------------ הוספת משתמש - חנות ----------------
        public bool AddShopsServices(ShopsDTO user)
        {
            // מעתיק את הנתונים ממחלקת itemDTO וממפה אותם
            Shops newUser = new Shops();

            newUser.Aaddress = user.Aaddress;
            newUser.NameShop = user.NameShop;
            newUser.Phone = user.Phone;
            newUser.CityId = user.CityId;

            m_db.Shops.Add(newUser);
            int c = m_db.SaveChanges(); // שומר את מה שהכנסנו
            return c > 0;
        }

        // שליפת נתוני חנות ע"י מ"ס מזהה למחיקה/עדכון
        public Shops getShopsForDelUpd(int id)
        {
            return m_db.Shops.Where(newShops => newShops.Id == id).FirstOrDefault();
        }

        // ------------- מחיקת משתמש - חנות --------------
        public ResponseDTO DeleteShopsServices(int id)
        {
            /*  שליפת ישות מהמאגר הנתונים למחיקה */
            Shops ShopsToDelete = getShopsForDelUpd(id);

            if (ShopsToDelete == null) // קיימת במאגר הנתונים(ID) בדיקה האם הישות  
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"Entity with id: {id} Not found in Data base!"
                };
            }

            // Remove מחיקת הישות ממאגר הנתונים על ידי פונקציית 
            m_db.Shops.Remove(ShopsToDelete);

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

        // ---------- עדכון משתמש - חנות ------------
        public ResponseDTO UpdateShopsServices(int id, ShopsDTO user)
        {
            Shops ShopsFromDB = getShopsForDelUpd(id); // שולח ישות ממאגר הנתונים

            if (ShopsFromDB == null)
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"Item {user.NameShop} With id {id} not found in DB"
                };
            }

            ShopsFromDB.Aaddress = user.Aaddress;
            ShopsFromDB.NameShop = user.NameShop;
            ShopsFromDB.Phone = user.Phone;
            ShopsFromDB.CityId = user.CityId;

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
