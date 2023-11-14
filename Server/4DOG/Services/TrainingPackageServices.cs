using _4DOG.Data;
using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _4DOG.Services
{
    public class TrainingPackageServices
    {
        private readonly _4DogsDBContext m_db;

        // בנאי 
        public TrainingPackageServices(_4DogsDBContext db) 
        {
            m_db = db;
        }

        // ----------  שליפת כל הנתונים -------------
        public List<TrainingPackageDTO> GetTrainingPackageServices()
        {
            var e = m_db.TrainingPackage.IgnoreAutoIncludes().Select //  IgnoreAutoIncludes לשים לב לשינוי 
                (ee => new TrainingPackageDTO()
                {
                    Id = ee.Id,
                    TrainingName = ee.TrainingName,
                    Ddescription = ee.Ddescription,
                    Price = ee.Price,
                    TrainingId = ee.TrainingId,
                    NameBusiness = ee.Training.NameBusiness
                }).ToList();

            e = m_db.TrainingPackage.Include(i => i.Training).Select
              (ee => new TrainingPackageDTO()
              {
                  Id = ee.Id,
                  TrainingName = ee.TrainingName,
                  Ddescription = ee.Ddescription,
                  Price = ee.Price,
                  TrainingId = ee.TrainingId,
                  NameBusiness = ee.Training.NameBusiness
              }).ToList();
            return e;
        }

        // ---------- שליפת כל הנתונים ע"י מס מזהה -------------
        public TrainingPackageDTO GetTrainingPackageServicesId(int id)
        {
            return m_db.TrainingPackage.Where(TrainingPackage => TrainingPackage.Id == id).Select(e => new TrainingPackageDTO()
            {
                Id = e.Id,
                TrainingName = e.TrainingName,
                Ddescription = e.Ddescription,
                Price = e.Price,
                TrainingId = e.TrainingId,
                NameBusiness = e.Training.NameBusiness
            }).FirstOrDefault();
        }

        // שליפת אובייקט חבילת אילוף
        public TrainingPackage getTrainingPackageByTrainingId(int trainingId)
        {
            return m_db.TrainingPackage.Where(TrainingPackage => TrainingPackage.TrainingId == trainingId).FirstOrDefault();
        }

        // שליפת נתוני חבילת אילוף ע"י מ"ס מזהה למחיקה/עדכון
        public TrainingPackage getTrainingPackageForDelUpd(int id)
        {
            return m_db.TrainingPackage.Where(newTrainingPackage => newTrainingPackage.Id == id).FirstOrDefault();
        }

        // ------------ הוספה  ----------------
        public bool AddTrainingPackageServices(TrainingPackageDTO user)
        {
            // מעתיק את הנתונים ממחלקת itemDTO וממפה אותם
            TrainingPackage newUser = new TrainingPackage();

            newUser.TrainingName = user.TrainingName;
            newUser.Ddescription = user.Ddescription;
            newUser.Price = user.Price;
            newUser.TrainingId = user.TrainingId;
                 
            m_db.TrainingPackage.Add(newUser);
            int c = m_db.SaveChanges(); // שומר את מה שהכנסנו
            return c > 0;
        }

        // ----------- מחיקת משתמש - חבילת אילוף ------------
        public ResponseDTO DeleteTrainingPackageServices(int id)
        {
            /*  שליפת ישות מהמאגר הנתונים למחיקה */
            TrainingPackage TrainingPackageToDelete = getTrainingPackageForDelUpd(id);

            if (TrainingPackageToDelete == null) // קיימת במאגר הנתונים(ID) בדיקה האם הישות  
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"Entity with id: {id} Not found in Data base!"
                };
            }

            // Remove מחיקת הישות ממאגר הנתונים על ידי פונקציית 
            m_db.TrainingPackage.Remove(TrainingPackageToDelete);
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

        // ----------- עדכון משתמש - חבילת אילוף ------------
        public ResponseDTO UpdateTrainingPackageServices(int id, TrainingPackageDTO user) 
        {
            TrainingPackage TrainingPackageFromDB = getTrainingPackageForDelUpd(id); // שולח ישות ממאגר הנתונים

            if (TrainingPackageFromDB == null)
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"TrainingName {user.TrainingName} With id {id} not found in DB"
                };
            }

            TrainingPackageFromDB.TrainingName = user.TrainingName;
            TrainingPackageFromDB.Ddescription = user.Ddescription;
            TrainingPackageFromDB.Price = user.Price;
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
