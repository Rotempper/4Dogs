using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4DOG.Data.Entities
{
    // הרשאות גישה לדפים ע"י סוג התחברות
    public static class Role
    {
        public const string Admin = "Admin";
        public const string BarberShopRole = "BarberShopRole";
        public const string DogOwnerRole = "DogOwnerRole";
        public const string PensionRole = "PensionRole";
        public const string TrainingRole = "TrainingRole";
    }
}
