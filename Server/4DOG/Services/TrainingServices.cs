using _4DOG.Data;
using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _4DOG.Services
{
    public class TrainingServices
    {
        // משתנה שמחזיר את כל הרשומות מהטבלה 
        private readonly _4DogsDBContext m_db;

        // בנאי 
        public TrainingServices(_4DogsDBContext db) 
        {
            m_db = db;
        }

        // ----------  שליפת כל הנתונים -------------
        public List<TrainingDTO> GetTrainingServices()
        {
            var e = m_db.Training.Include(i => i.User).Select
                (ee => new TrainingDTO()
                {
                    Id = ee.Id,
                    UserId = ee.UserId,
                    User = ee.User,
                    NameBusiness = ee.NameBusiness,
                    Aaddress = ee.Aaddress,
                    About = ee.About,
                    Phone = ee.Phone,
                    CityId = ee.CityId,
                    city = ee.City.CityName
                }).ToList();

             e = m_db.Training.Include(i => i.City).Select
                (ee => new TrainingDTO()
                {
                    Id = ee.Id,
                    UserId = ee.UserId,
                    User = ee.User,
                    city = ee.City.CityName,
                    NameBusiness = ee.NameBusiness,
                    Aaddress = ee.Aaddress,
                    About = ee.About,
                    Phone = ee.Phone,
                    CityId = ee.CityId
                }).ToList();
            return e;
        }

        // ---------- שליפת כל הנתונים ע"י מס מזהה -------------
        public TrainingDTO GetTrainingServicesId(int id)
        {
            return m_db.Training.Where(Training => Training.Id == id).Select(ee => new TrainingDTO()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                User = ee.User,
                city = ee.City.CityName,
                NameBusiness = ee.NameBusiness,
                Aaddress = ee.Aaddress,
                About = ee.About,
                Phone = ee.Phone,
                CityId = ee.CityId
            }).FirstOrDefault();
        }

        // שליפת אובייקט אילוף
        public Training getTrainingIdByUserIdObj(int userid)
        {
            return m_db.Training.Where(Training => Training.UserId == userid).FirstOrDefault();
        }

        // ---- שליפת פרטי הזמנת סוג אילוף ע"י מס מזהה משתמש ----
        public List<TrainingDTO> GetTrainingServicesIdData(int id)
        {
            return m_db.Training.Where(Training => Training.Id == id).Select(ee => new TrainingDTO()
            {
                city = ee.City.CityName,
                Aaddress = ee.Aaddress,
                Phone = ee.Phone,
            }).OrderBy(f => f.city).ToList(); // מיון ערים
        }


        // ---- שליפת מס מזהה הזמנת אילוף ע"י מס מזהה משתמש ----
        public int getTrainingIdByUserId(int userid)
        {
            Training training = m_db.Training.Where(newTraining => newTraining.UserId == userid).FirstOrDefault();
            return training.Id;
        }


        // ------------ הוספת משתמש - אילוף ----------------
        public bool AddTrainingServices(TrainingDTO user)
        {
            // מעתיק את הנתונים ממחלקת itemDTO וממפה אותם
            Training newUser = new Training();

            newUser.UserId = user.UserId;
            newUser.NameBusiness = user.NameBusiness;
            newUser.Aaddress = user.Aaddress;
            newUser.About = user.About;
            newUser.Phone = user.Phone;
            newUser.CityId = user.CityId;

            m_db.Training.Add(newUser);
            int c = m_db.SaveChanges(); // שומר את מה שהכנסנו
            return c > 0;
        }

        // שליפת נתוני אילוף ע"י מ"ס מזהה למחיקה/עדכון
        public Training getTrainingForDelUpd(int id)
        {
            return m_db.Training.Where(Training => Training.Id == id).FirstOrDefault();
        }

        // ------------- מחיקת משתמש - אילוף --------------
        public ResponseDTO DeleteTrainingServices(int id)
        {
            /*  שליפת ישות מהמאגר הנתונים למחיקה */
            Training TrainingToDelete = getTrainingForDelUpd(id);

            if (TrainingToDelete == null) // קיימת במאגר הנתונים(ID) בדיקה האם הישות  
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"Entity with id: {id} Not found in Data base!"
                };
            }

            // Remove מחיקת הישות ממאגר הנתונים על ידי פונקציית 
            m_db.Training.Remove(TrainingToDelete);
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

        // ---------- עדכון משתמש - אילוף -------------
        public ResponseDTO UpdateTrainingServices(int id, TrainingDTO user)
        {
            Training TrainingFromDB = getTrainingForDelUpd(id); // שולח ישות ממאגר הנתונים

            if (TrainingFromDB == null)
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"Item {user.UserId} With id {id} not found in DB"
                };
            }

            TrainingFromDB.UserId = user.UserId;
            TrainingFromDB.NameBusiness = user.NameBusiness;
            TrainingFromDB.Aaddress = user.Aaddress;
            TrainingFromDB.About = user.About;
            TrainingFromDB.Phone = user.Phone;
            TrainingFromDB.CityId = user.CityId;

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
