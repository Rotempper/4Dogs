using _4DOG.Data;
using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _4DOG.Services
{
    public class DogTrainingServices
    {
        // משתנה שמחזיר את כל הרשומות מהטבלה 
        private readonly _4DogsDBContext m_db;

        // בנאי 
        public DogTrainingServices(_4DogsDBContext db) 
        {
            m_db = db;
        }

        // ----------  שליפת כל הנתונים -------------
        public List<DogTrainingDTO> GetDogTrainingServices()
        {
            var e = m_db.DogTraining.Include(i => i.DogOwner).Select
                (ee => new DogTrainingDTO()
                 {
                    Id = ee.Id,
                    DogOwnerUserId = ee.DogOwner.UserId,
                    DogOwnerId = ee.DogOwnerId,
                    StartDate = ee.StartDate,
                    EndDate = ee.EndDate,
                    TrainingId = ee.TrainingId,
                    TrainingName = ee.Training.NameBusiness,
                    PackagId = ee.PackagId,
                    TrainingPackageName = ee.TrainingPackage.TrainingName
                }).ToList();

            e = m_db.DogTraining.Include(i => i.Training).Select
                (ee => new DogTrainingDTO()
                {
                    Id = ee.Id,
                    DogOwnerUserId = ee.DogOwner.UserId,
                    DogOwnerId = ee.DogOwnerId,
                    StartDate = ee.StartDate,
                    EndDate = ee.EndDate,
                    TrainingId = ee.TrainingId,
                    TrainingName = ee.Training.NameBusiness,
                    PackagId = ee.PackagId,
                    TrainingPackageName = ee.TrainingPackage.TrainingName
                }).ToList();
            
            e = m_db.DogTraining.Include(i => i.TrainingPackage).Select
                (ee => new DogTrainingDTO()
                {
                    Id = ee.Id,
                    DogOwnerUserId = ee.DogOwner.UserId,
                    DogOwnerId = ee.DogOwnerId,
                    StartDate = ee.StartDate,
                    EndDate = ee.EndDate,
                    TrainingId = ee.TrainingId,
                    TrainingName = ee.Training.NameBusiness,
                    PackagId = ee.PackagId,
                    TrainingPackageName = ee.TrainingPackage.TrainingName
                }).ToList();

            // מיון תאריכים
            for (int i = 0; i < e.Count; i++)
            {
                for (int c = i; c < e.Count; c++)
                {
                    if (e[i].StartDate > e[c].StartDate)
                    {
                        DogTrainingDTO temp = e[i];
                        e[i] = e[c];
                        e[c] = temp;
                    }
                }
            }
            return e;
        }
        
        // ---------- שליפת כל הנתונים ע"י מס מזהה -------------
        public DogTrainingDTO GetDogTrainingServicesId(int id)
        {
            return m_db.DogTraining.Where(DogTraining => DogTraining.Id == id).Select(ee => new DogTrainingDTO()
            {
                Id = ee.Id,
                DogOwnerUserId = ee.DogOwner.UserId,
                DogOwnerId = ee.DogOwnerId,
                StartDate = ee.StartDate,
                EndDate = ee.EndDate,
                TrainingId = ee.TrainingId,
                TrainingName = ee.Training.NameBusiness,
                PackagId = ee.PackagId,
                TrainingPackageName = ee.TrainingPackage.TrainingName
            }).FirstOrDefault();
        }

        // ---------- הוספת משתמש - מאלף ------------
        public bool AddDogTrainingServices(DogTrainingDTO user)
        {
            // מעתיק את הנתונים ממחלקת itemDTO וממפה אותם
            DogTraining newUser = new DogTraining();

            newUser.DogOwnerId = user.DogOwnerId;
            newUser.StartDate = user.StartDate;
            newUser.EndDate = user.EndDate;
            newUser.TrainingId = user.TrainingId;
            newUser.PackagId = user.PackagId;

            m_db.DogTraining.Add(newUser);
            int c = m_db.SaveChanges(); // שומר את מה שהכנסנו
            return c > 0;
        }

        // שליפת נתוני מאלף ע"י מ"ס מזהה למחיקה/עדכון
        public DogTraining getDogTrainingForDelUpd(int id)
        {
            return m_db.DogTraining.Where(DogTraining => DogTraining.Id == id).FirstOrDefault();
        }


        // שליפת פרטי הזמנת מאלף ע"י מס מזהה של משתמש
        public List<DogTrainingDTO> gettDogTrainingByUserId(int DogOwnerid)
        {
            var e = m_db.DogTraining.Where(newDogTraining => newDogTraining.DogOwnerId == DogOwnerid).Select(ee => new DogTrainingDTO()
            {
                Id = ee.Id,
                DogOwnerUserId = ee.DogOwner.UserId,
                DogOwnerId = ee.DogOwnerId,
                StartDate = ee.StartDate,
                EndDate = ee.EndDate,
                TrainingId = ee.TrainingId,
                TrainingName = ee.Training.NameBusiness,
                PackagId = ee.PackagId,
                TrainingPackageName = ee.TrainingPackage.TrainingName
            }).ToList();

            // מיון תאריכים
            for (int i = 0; i < e.Count; i++)
            {
                for (int c = i; c < e.Count; c++)
                {
                    if (e[i].StartDate > e[c].StartDate)
                    {
                        DogTrainingDTO temp = e[i];
                        e[i] = e[c];
                        e[c] = temp;
                    }
                }
            }
            return e;
        }

        /* שליפת ההזמנות על פי DogownerId.UserId */
        public void GetHaircutsOrderByUserId(int UserId)
        {
            List<DogTrainingDTO> ListDogTrainingOrder = GetDogTrainingServices(); //את כל ההזמנות שולף 

            //   *******
            for (int i = 0; i < ListDogTrainingOrder.Count; i++)
            {
                if (ListDogTrainingOrder[i].DogOwnerUserId == UserId)
                {
                    DeleteDogTrainingServices(ListDogTrainingOrder[i].Id);
                }
            }
        }


        // ------------- מחיקת משתמש - מאלף --------------
        public ResponseDTO DeleteDogTrainingServices(int id)
        {
            /*  שליפת ישות מהמאגר הנתונים למחיקה */
            DogTraining DogTrainingToDelete = getDogTrainingForDelUpd(id);

            if (DogTrainingToDelete == null) // קיימת במאגר הנתונים(ID) בדיקה האם הישות  
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"DogTraining with id: {id} Not found in Data base!"
                };
            }

            // Remove מחיקת הישות ממאגר הנתונים על ידי פונקציית 
            m_db.DogTraining.Remove(DogTrainingToDelete);
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

        // ------------ עדכון משתמש - מאלף -------------
        public ResponseDTO UpdateDogTrainingServices(int id, DogTrainingDTO user)
        {
            DogTraining DogTrainingFromDB = getDogTrainingForDelUpd(id); // שולח ישות ממאגר הנתונים

            if (DogTrainingFromDB == null)
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"DogOwnerId {user.DogOwnerId} With id {id} not found in DB"
                };
            }

            DogTrainingFromDB.DogOwnerId = user.DogOwnerId;
            DogTrainingFromDB.StartDate = user.StartDate;
            DogTrainingFromDB.EndDate = user.EndDate;
            DogTrainingFromDB.TrainingId = user.TrainingId;
            DogTrainingFromDB.PackagId = user.PackagId;

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

