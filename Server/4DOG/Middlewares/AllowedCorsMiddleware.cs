using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Middlewares
{
    public class AllowedCorsMiddleware
    {
        // המשתנה שומר את הבקשות מהלקוח
        private readonly RequestDelegate _next;       
        public AllowedCorsMiddleware(RequestDelegate next)// בנאי
        {
            _next = next;
        }

        // אני אקבל את הבקשה מהלקוח עם כל המידע של הבקשה והעביר אותה הלאה לשרת
        public async Task Invoke(HttpContext context)
        {
            //cors פתרון לשגיאת ה
            // אחראי לשמות השרתים שמותר להם לעבור דרך השרת
            context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { " * " });
            /*http://localhost:4200*/

            // אחראי לסוגי ה    שהשרת יכול לקבל 
            //*Headers
            // לא נשים אותו במערך
            context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { " * " });

            // אחראי לסוגי הבקשות שהשרת יכול לקבל
            // "GET" , "POST","DELETE" , "PUT"
            context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { " * " });

            await _next(context);
        }
    }
}
