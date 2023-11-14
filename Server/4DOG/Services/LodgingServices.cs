using _4DOG.Data;
using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _4DOG.Services
{
    public class LodgingServices
    {
        private readonly _4DogsDBContext m_db;

        // בנאי 
        public LodgingServices(_4DogsDBContext db) 
        {
            m_db = db;
        }

        // ----------  שליפת כל הנתונים -------------
        public List<LodgingDTO> GetLodgingServices()
        {         
            var e = m_db.Lodging.Include(i => i.DogOwner).Select
            (ee => new LodgingDTO()
            {
                Id = ee.Id,
               DogOwnerUserId = ee.DogOwner.UserId,
                DogOwnerId = ee.DogOwnerId,
                StartDate = ee.StartDate,
                EndDate = ee.EndDate,
                PensionId = ee.PensionId,
                PensionName = ee.Pension.NameBusiness
            }).ToList();

            e = m_db.Lodging.Include(i => i.Pension).Select
                (ee => new LodgingDTO()
                {
                    Id = ee.Id,
                    DogOwnerUserId = ee.DogOwner.UserId,
                    DogOwnerId = ee.DogOwnerId,
                    StartDate = ee.StartDate,
                    EndDate = ee.EndDate,
                    PensionId = ee.PensionId,
                    PensionName = ee.Pension.NameBusiness
                }).ToList();

            // מיון תאריכים
            for (int i = 0; i < e.Count; i++)
            {
                for (int c = i; c < e.Count; c++)
                {
                    if (e[i].StartDate > e[c].StartDate)
                    {
                        LodgingDTO temp = e[i];
                        e[i] = e[c];
                        e[c] = temp;
                    }
                }
            }
            return e;        
        }

        // ---------- שליפת כל הנתונים ע"י מס מזהה -------------
        public LodgingDTO GetLodgingServicesId(int id)
        {
            return m_db.Lodging.Where(Lodging => Lodging.Id == id).Select(ee => new LodgingDTO()
            {
                Id = ee.Id,
                DogOwnerUserId = ee.DogOwner.UserId,
                DogOwnerId = ee.DogOwnerId,
                StartDate = ee.StartDate,
                EndDate = ee.EndDate,
                PensionId = ee.PensionId,
                PensionName = ee.Pension.NameBusiness
            }).FirstOrDefault();
        }
   

        // ---- שליפת פרטי הזמנת לינה ע"י מס מזהה משתמש ----
        public List<LodgingDTO> getLodgingByUserId(int id)
        {
           var ee = m_db.Lodging.Where(newLodging => newLodging.DogOwnerId == id).Select(ee => new LodgingDTO()
            {
                Id = ee.Id,
                DogOwnerUserId = ee.DogOwner.UserId,
                DogOwnerId = ee.DogOwnerId,
                StartDate = ee.StartDate,
                EndDate = ee.EndDate,
                PensionId = ee.PensionId,
                PensionName = ee.Pension.NameBusiness
            }).ToList();

            // מיון תאריכים
            for (int i = 0; i < ee.Count; i++)
            {
                for (int c = i; c < ee.Count; c++)
                {
                    if (ee[i].StartDate > ee[c].StartDate)
                    {
                        LodgingDTO temp = ee[i];
                        ee[i] = ee[c];
                        ee[c] = temp;
                    }
                }
            }
            return ee;
        }

        // ------------ הוספת משתמש - לינה ----------------
        public bool AddLodgingServices(LodgingDTO user)
        {
            // מעתיק את הנתונים ממחלקת itemDTO וממפה אותם
            Lodging newUser = new Lodging();

            newUser.DogOwnerId = user.DogOwnerId;
            newUser.StartDate = user.StartDate;
            newUser.EndDate = user.EndDate;
            newUser.PensionId = user.PensionId;
          
            m_db.Lodging.Add(newUser);
            int c = m_db.SaveChanges(); // שומר את מה שהכנסנו
            return c > 0;
        }

        // שליפת נתוני לינה ע"י מ"ס מזהה למחיקה/עדכון
        public Lodging getLodgingForDelUpd(int id)
        {
            return m_db.Lodging.Where(Lodging => Lodging.Id == id).FirstOrDefault();
        }

        // ------------- מחיקת משתמש - לינה --------------
        public ResponseDTO DeleteLodgingServices(int id)
        {
            /*  שליפת ישות מהמאגר הנתונים למחיקה */
            Lodging LodgingToDelete = getLodgingForDelUpd(id);

            if (LodgingToDelete == null) // קיימת במאגר הנתונים(ID) בדיקה האם הישות  
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"Entity with id: {id} Not found in Data base!"
                };
            }

            // Remove מחיקת הישות ממאגר הנתונים על ידי פונקציית 
            m_db.Lodging.Remove(LodgingToDelete);

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

        /* שליפת ההזמנות על פי DogownerId.UserId */
        public void GetLodgingOrderByUserId(int UserId)
        {
            List<LodgingDTO> ListLodgingOrder = GetLodgingServices(); //את כל ההזמנות שולף 

            //   *******
            for (int i = 0; i < ListLodgingOrder.Count; i++)
            {
                if(ListLodgingOrder[i].DogOwnerUserId == UserId)
                {
                    DeleteLodgingServices(ListLodgingOrder[i].Id);
                }
             
            }
        }



        // ------------- עדכון משתמש - לינה ----------------
        public ResponseDTO UpdateLodgingServices(int id, LodgingDTO user)
        {
            Lodging LodgingFromDB = getLodgingForDelUpd(id); // שולח ישות ממאגר הנתונים

            if (LodgingFromDB == null)
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"Item {user.DogOwnerId} With id {id} not found in DB"
                };
            }

            LodgingFromDB.DogOwnerId = user.DogOwnerId;
            LodgingFromDB.StartDate = user.StartDate;
            LodgingFromDB.EndDate = user.EndDate;
            LodgingFromDB.PensionId = user.PensionId;

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
