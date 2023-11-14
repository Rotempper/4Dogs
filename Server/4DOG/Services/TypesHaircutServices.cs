using _4DOG.Data;
using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _4DOG.Services
{
    public class TypesHaircutServices
    {
        private readonly _4DogsDBContext m_db;

        // בנאי 
        public TypesHaircutServices(_4DogsDBContext db) 
        {
            m_db = db;
        }

        // ----------  שליפת כל הנתונים -------------
        public List<TypesHaircutDTO> GetTypesHaircutServices()
        {           
            var e = m_db.TypesHaircut.IgnoreAutoIncludes().Select //  IgnoreAutoIncludes לשים לב לשינוי 
                (ee => new TypesHaircutDTO()
                {
                    Id = ee.Id,
                    HaircutName = ee.HaircutName,
                    Ddescription = ee.Ddescription,
                    Price = ee.Price,
                    BarberShopId = ee.BarberShopId,
                    BarberShopName = ee.BarberShop.NameBarberShop
                }).ToList();

            e = m_db.TypesHaircut.Include(i => i.BarberShop).Select
                (ee => new TypesHaircutDTO()
                {
                    Id = ee.Id,
                    HaircutName = ee.HaircutName,
                    Ddescription = ee.Ddescription,
                    Price = ee.Price,
                    BarberShopId = ee.BarberShopId,
                    BarberShopName = ee.BarberShop.NameBarberShop
                }).ToList();
            return e;
        }

        // ---------- שליפת כל הנתונים ע"י מס מזהה -------------
        public TypesHaircutDTO GetTypesHaircutServicesId(int id)
        {
            return m_db.TypesHaircut.Where(TypesHaircut => TypesHaircut.Id == id).Select(e => new TypesHaircutDTO()
            {
                Id = e.Id,
                HaircutName = e.HaircutName,
                Ddescription = e.Ddescription,
                Price = e.Price,
                BarberShopId = e.BarberShopId,
                BarberShopName = e.BarberShop.NameBarberShop,               
            }).FirstOrDefault();
        }

        // שליפת אובייקט סוג תספורת
        public TypesHaircut getTypesHaircutByBarberShopId(int barberShopId)
        {
            return m_db.TypesHaircut.Where(TypesHaircut => TypesHaircut.BarberShopId == barberShopId).FirstOrDefault();
        }

        // שליפת סוג תספורת ע"י מס מזהה
        public TypesHaircut getTypesHaircutById1(int id)
        {
            return m_db.TypesHaircut.Where(TypesHaircut => TypesHaircut.Id == id).FirstOrDefault();
        }

        // שליפת סוג תספורת ע"י מס מזהה
        public int getTypesHaircutById2(int barberShopId)
        {
            TypesHaircut typesHaircut = m_db.TypesHaircut.Where(ee => ee.BarberShopId == barberShopId).FirstOrDefault();
            return typesHaircut.Id;
        }


        // ------------ הוספה  ----------------
        public bool AddTypesHaircutServices(TypesHaircutDTO user)
        {
            // מעתיק את הנתונים ממחלקת itemDTO וממפה אותם
            TypesHaircut newUser = new TypesHaircut();

            newUser.HaircutName = user.HaircutName;
            newUser.Ddescription = user.Ddescription;
            newUser.Price = user.Price;
            newUser.BarberShopId = user.BarberShopId;

            m_db.TypesHaircut.Add(newUser);
            int c = m_db.SaveChanges(); // שומר את מה שהכנסנו
            return c > 0;
        }

        // שליפת נתוני סוג תספורת ע"י מ"ס מזהה למחיקה/עדכון
        public TypesHaircut getTypesHaircutForDelUpd(int id)
        {
            return m_db.TypesHaircut.Where(newTypesHaircut => newTypesHaircut.Id == id).FirstOrDefault();
        }

        // ------------- מחיקת משתמש - סוג תספורת --------------
        public ResponseDTO DeleteTypesHaircutServices(int id)
        {
            /*  שליפת ישות מהמאגר הנתונים למחיקה */
            TypesHaircut TypesHaircutToDelete = getTypesHaircutForDelUpd(id);

            if (TypesHaircutToDelete == null) // קיימת במאגר הנתונים(ID) בדיקה האם הישות  
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"Entity with id: {id} Not found in Data base!"
                };
            }

            // Remove מחיקת הישות ממאגר הנתונים על ידי פונקציית 
            m_db.TypesHaircut.Remove(TypesHaircutToDelete);
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

        // ----------- עדכון משתמש - סוג תספורת -------------
        public ResponseDTO UpdateTypesHaircutServices(int id, TypesHaircutDTO user)
        {
            TypesHaircut TypesHaircutFromDB = getTypesHaircutForDelUpd(id); // שולח ישות ממאגר הנתונים

            if (TypesHaircutFromDB == null)
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"Hair cut {user.HaircutName} With id {id} not found in DB"
                };
            }
            TypesHaircutFromDB.HaircutName = user.HaircutName;
            TypesHaircutFromDB.Ddescription = user.Ddescription;
            TypesHaircutFromDB.Price = user.Price;
            
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
