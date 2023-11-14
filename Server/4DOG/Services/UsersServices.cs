using _4DOG.Data;
using _4DOG.Data.DTO;
using _4DOG.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _4DOG.Services
{
    public class UsersServices
    {
        // משתנה שמחזיר את כל הרשומות מהטבלה 
        private readonly _4DogsDBContext m_db;
        private readonly JwtService _m_jwtService; // Token
        private readonly HaircutsServices _HaircutsServices; // *******
        private readonly LodgingServices _LodgingServices; // *******
        private readonly DogTrainingServices _DogTrainingServices; // *******


        // בנאי 
        public UsersServices(_4DogsDBContext db, JwtService m_jwtService, HaircutsServices Haircuts_Services, LodgingServices Lodging_Services,
            DogTrainingServices DogTraining_Services) 
        {
            m_db = db;
            _m_jwtService = m_jwtService;
            _HaircutsServices = Haircuts_Services; // **********
            _LodgingServices = Lodging_Services;
            _DogTrainingServices = DogTraining_Services;


        }

        // ----------  שליפת מס מזהה וסוג משתמש -------------
        public string getToken(string id, string role)
        {
            return _m_jwtService.GenerateToken(id,role);
        }

        // ----------  שליפת כל הנתונים -------------
        public List<UsersDTO> GetUsersServices()
        {
            return m_db.Users.Select(e => new UsersDTO()
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Password = e.Password,
                Role = e.Role,
                Phone = e.Phone
            }).ToList();
        }

        // ---------- שליפת כל הנתונים ע"י מס מזהה -------------
        public UsersDTO GetUserServicesId(int id)
        {
            return m_db.Users.Where(Users => Users.Id == id).Select(e => new UsersDTO()
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Password = e.Password,
                Role = e.Role,
                Phone = e.Phone
            }).FirstOrDefault();
        }

        // ------ מקבל שם משתמש ע"י מס מזהה ------
        public UsersDTO getUserNameById(int id)
        {
            return m_db.Users.Where(newDogTraining => newDogTraining.Id == id).Select(ee => new UsersDTO()
            {
                FirstName = ee.FirstName
            }).FirstOrDefault();
        }


        // ------ מקבל מס מזהה של משתמש ע"י מייל ------
        public int getUserIdByEmail(string email)
        {
            Users User = m_db.Users.Where(ee => ee.Email == email).FirstOrDefault();
            return User.Id;
        }


        // ------ מקבל מייל ובודק אם קיים במערכת ------
        public bool checkEmailInSql(string Email)
        {
            int number = 0;
            try {
                number = m_db.Users.Where(ee => ee.Email == Email).Count(); //  יחזיר כמה מיילים כאלה יש 
               
                }
            //Users  יכנס רק במידה והטבלה  ריקה לחלוטין
            catch
            {
                return true; // לא קיים מייל כזה , אפשר להירשם לאתר
            }

            if(number > 0)
            {
                return false; // האיימל קיים בבסיס נתונים
            }
            return true; // לא קיים איימל כזה
        }


        //------------------- סיסמה מוצפנת ---------------------
        public UsersDTO GetUserServicesIdGWT(string email, string password)
        {
            string passwordAfterMD5 = GetMD5(password); 
           
            return m_db.Users.Where(user => user.Email.ToLower() == email.ToLower() && user.Password == passwordAfterMD5
           ).Select(e => new UsersDTO() //  שיניתי user.Password == passwordAfterMD5
            {                                                                          
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Password = e.Password,
                Role = e.Role,
                Phone = e.Phone
            }).FirstOrDefault();
        }
        private string GetMD5(string input) // מקבלת 123
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", ""); // 202cb962ac59075b964b07152d234b70 - מחזירה ערך מוצפן
            }
        }

        // ------------ הוספת משתמש ----------------
        public bool AddUserServices(UsersDTO user)
        {
            // מעתיק את הנתונים ממחלקת itemDTO וממפה אותם
            Users newUser = new Users();
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.Email = user.Email;
            newUser.Password = GetMD5(user.Password);
            newUser.Phone = user.Phone;
            newUser.Role = user.Role;

            m_db.Users.Add(newUser);
            int c = m_db.SaveChanges(); // שומר את מה שהכנסנו
            return c > 0;           
        }

        // שליפת נתוני משתמש ע"י מ"ס מזהה למחיקה/עדכון
        public Users getUserForDelUpd(int id)
        {
            return m_db.Users.Where(newUser => newUser.Id == id).FirstOrDefault();
        }

        // ------------- מחיקת משתמש --------------
        public ResponseDTO DeleteUserServices(int id)
        {
            //  שליפת ישות מהמאגר הנתונים למחיקה
            Users UserToDelete = getUserForDelUpd(id);

            if (UserToDelete == null) // קיימת במאגר הנתונים(ID) בדיקה האם הישות  
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"Entity with id: {id} Not found in Data base!"
                };
            }
            _HaircutsServices.GetHaircutsOrderByUserId(UserToDelete.Id); // **  
            _LodgingServices.GetLodgingOrderByUserId(UserToDelete.Id);
            _DogTrainingServices.GetHaircutsOrderByUserId(UserToDelete.Id); 

            // Remove מחיקת הישות ממאגר הנתונים על ידי פונקציית 
            m_db.Users.Remove(UserToDelete);
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

        // -------------עדכון משתמש ----------------
        public ResponseDTO UpdateUserServices(int id, UsersDTO user)
        {
            Users UserFromDB = getUserForDelUpd(id); // שולח ישות ממאגר הנתונים

            if (UserFromDB == null)
            {
                return new ResponseDTO()
                {
                    Status = StatosCode.Error,
                    StatusText = $"Item {user.FirstName} With id {id} not found in DB"
                };
            }

            UserFromDB.FirstName = user.FirstName;
            UserFromDB.LastName = user.LastName;
            UserFromDB.Email = user.Email;
            UserFromDB.Password = GetMD5(user.Password);
            UserFromDB.Phone = user.Phone;

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
