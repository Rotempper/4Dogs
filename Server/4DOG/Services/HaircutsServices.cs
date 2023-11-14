using _4DOG.Data;
using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _4DOG.Services
{
    public class HaircutsServices
    {
        // משתנה שמחזיר את כל הרשומות מהטבלה 
        private readonly _4DogsDBContext m_db;

        // בנאי 
        public HaircutsServices(_4DogsDBContext db) 
        {
            m_db = db;
        }

        // ----------  שליפת כל הנתונים -------------
        public List<HaircutsDTO> GetHaircutsServices()
        {
            var e = m_db.Haircuts.Include(i => i.DogOwner).Select
               (ee => new HaircutsDTO()
               {
                   Id = ee.Id,
                   DogOwnerUserId = ee.DogOwner.UserId,
                   DogOwnerId = ee.DogOwnerId,
                   Ddate = ee.Ddate,
                   Hhour = ee.Hhour,
                   BarbershopId = ee.BarbershopId,
                   BarberShopName = ee.BarberShop.NameBarberShop,
                   TypesHaircutId = ee.TypesHaircutId,
                   TypesHaircutName = ee.TypesHaircut.HaircutName
               }).ToList();

            e = m_db.Haircuts.Include(i => i.BarberShop).Select
            (ee => new HaircutsDTO()
            {
                Id = ee.Id,
                DogOwnerUserId = ee.DogOwner.UserId,
                DogOwnerId = ee.DogOwnerId,
                Ddate = ee.Ddate,
                Hhour = ee.Hhour,
                BarbershopId = ee.BarbershopId,
                BarberShopName = ee.BarberShop.NameBarberShop,
                TypesHaircutId = ee.TypesHaircutId,
                TypesHaircutName = ee.TypesHaircut.HaircutName
            }).ToList();
          
            e = m_db.Haircuts.Include(i => i.TypesHaircut).Select
            (ee => new HaircutsDTO()
            {
                Id = ee.Id,
                DogOwnerUserId = ee.DogOwner.UserId,
                DogOwnerId = ee.DogOwnerId,
                Ddate = ee.Ddate,
                Hhour = ee.Hhour,
                BarbershopId = ee.BarbershopId,
                BarberShopName = ee.BarberShop.NameBarberShop,
                TypesHaircutId = ee.TypesHaircutId,
                TypesHaircutName = ee.TypesHaircut.HaircutName
            }).ToList();

            // מיון תאריכים
            for(int i =0; i<e.Count; i++)
            {
                for(int c = i; c< e.Count; c++)
                {
                    if(e[i].Ddate > e[c].Ddate)
                    {
                        HaircutsDTO temp = e[i];
                        e[i] = e[c];
                        e[c] = temp;
                    }
                }
            }
            return e;
        }


        // ---- שליפת פרטי הזמנת סוג תספורת ע"י מס מזהה משתמש ----
        public List<HaircutsDTO> getHaircutsDTOByUserId(int id)
        {
            var e = m_db.Haircuts.Where(newHaircuts => newHaircuts.DogOwnerId == id).Select(ee => new HaircutsDTO()
            {
                Id = ee.Id,
                DogOwnerUserId = ee.DogOwner.UserId,
                DogOwnerId = ee.DogOwnerId,
                Ddate = ee.Ddate,
                Hhour = ee.Hhour,
                BarbershopId = ee.BarbershopId,
                BarberShopName = ee.BarberShop.NameBarberShop,
                TypesHaircutId = ee.TypesHaircutId,
                TypesHaircutName = ee.TypesHaircut.HaircutName
            }).ToList();

            // מיון תאריכים
            for (int i = 0; i < e.Count; i++)
            {
                for (int c = i; c < e.Count; c++)
                {
                    if (e[i].Ddate > e[c].Ddate)
                    {
                        HaircutsDTO temp = e[i];
                        e[i] = e[c];
                        e[c] = temp;
                    }
                }
            }
            return e;
        }

        // ---------- שליפת כל הנתונים ע"י מס מזהה -------------
        public HaircutsDTO GetHaircutsServicesId(int id)
        {
            return m_db.Haircuts.Where(Haircuts => Haircuts.Id == id).Select(ee => new HaircutsDTO()
            {
                Id = ee.Id,
                DogOwnerUserId = ee.DogOwner.UserId,
                DogOwnerId = ee.DogOwnerId,
                Ddate = ee.Ddate,
                Hhour = ee.Hhour,
                BarbershopId = ee.BarbershopId,
                BarberShopName = ee.BarberShop.NameBarberShop,
                TypesHaircutId = ee.TypesHaircutId,
                TypesHaircutName = ee.TypesHaircut.HaircutName
            }).FirstOrDefault();
        }

        // ------ הוספת קביעת שעה להזמנת תספורת ------
        public bool checkTime(HaircutsDTO obj)
        {
            // אם גדול מ 0 כלומר יש את הנתונים בבסיס הנתונים ואם זה 0 כלומר לא קיים בבסיס הנתונים והשעה פנויה
            DateTime ddate =DateTime.Parse(obj.Ddate.ToString("yyyy/MM/dd"));
            int c = m_db.Haircuts.Where(h => h.Hhour == obj.Hhour && h.Ddate == ddate && h.BarbershopId == obj.Id).Count();
            return c > 0;
        }

        // ------------ הוספת משתמש - תספורת ----------------
        public bool AddHaircutsServices(HaircutsDTO user)
        {
            // מעתיק את הנתונים ממחלקת itemDTO וממפה אותם
            Haircuts newUser = new Haircuts();
            newUser.DogOwnerId = user.DogOwnerId;
            newUser.Ddate = user.Ddate;
            newUser.Hhour = user.Hhour;
            newUser.BarbershopId = user.BarbershopId;
            newUser.TypesHaircutId = user.TypesHaircutId;

            m_db.Haircuts.Add(newUser);
            int c = m_db.SaveChanges(); // שומר את מה שהכנסנו
            return c > 0;
        }

        // שליפת נתוני תספורת ע"י מ"ס מזהה למחיקה/עדכון
        public Haircuts getHaircutsForDelUpd(int id)
        {
            return m_db.Haircuts.Where(Haircuts => Haircuts.Id == id).FirstOrDefault();
        }

        /* שליפת ההזמנות על פי DogownerId.UserId */
        public void  GetHaircutsOrderByUserId(int UserId)
        {
             List<HaircutsDTO> ListHaircutOrder  = GetHaircutsServices(); //את כל ההזמנות שולף 

         //   *******
            for (int i = 0; i < ListHaircutOrder.Count; i++)
            {
                if(ListHaircutOrder[i].DogOwnerUserId == UserId)
                {
                    DeleteHaircutsServices(ListHaircutOrder[i].Id);
                }
            }
        }

        // ------------- מחיקת משתמש - תספורת --------------
        public ResponseDTO DeleteHaircutsServices(int id)
        {
            /*  שליפת ישות מהמאגר הנתונים למחיקה */
            Haircuts HaircutsToDelete = getHaircutsForDelUpd(id);

            if (HaircutsToDelete == null) // קיימת במאגר הנתונים(ID) בדיקה האם הישות  
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"Entity with id: {id} Not found in Data base!"
                };
            }

            // Remove מחיקת הישות ממאגר הנתונים על ידי פונקציית 
            m_db.Haircuts.Remove(HaircutsToDelete);

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

        // --------- עדכון משתמש - תספורת  -----------
        public ResponseDTO UpdateHaircutsServices(int id, HaircutsDTO user)
        {
            Haircuts HaircutsFromDB = getHaircutsForDelUpd(id); // שולח ישות ממאגר הנתונים

            if (HaircutsFromDB == null)
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"Item {user.BarbershopId} With id {id} not found in DB"
                };
            }

            HaircutsFromDB.DogOwnerId = user.DogOwnerId;
            HaircutsFromDB.Ddate = user.Ddate;
            HaircutsFromDB.Hhour = user.Hhour;
            HaircutsFromDB.BarbershopId = user.BarbershopId;
            HaircutsFromDB.TypesHaircutId = user.TypesHaircutId;

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
