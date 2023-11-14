using _4DOG.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace _4DOG.Data
{
    public class _4DogsDBContext: DbContext
    {
        // בנאי המכיל יצירת קשר מול בסיס הנתונים והתחברות אוטומטית
        public _4DogsDBContext(DbContextOptions<_4DogsDBContext> options) : base(options)
        {
        }

        // פונק למפתחות המקשר בין יחיד לרבים
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // DogRace לטבלה DogOwner
            modelBuilder.Entity<DogRace>(entity =>
            {
                entity.HasMany<DogOwner>(A => A.ListDogOwner)
                .WithOne(B => B.DogRace)
                .HasForeignKey(C => C.DogRaceId);
            });

            // DogOwner לטבלה Users
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasMany<DogOwner>(A => A.ListDogOwner)
                .WithOne(B => B.Users)
                .HasForeignKey(C => C.UserId);

                // .OnDelete(DeleteBehavior.Cascade);
                //.OnDelete(DeleteBehavior.ClientCascade); 
            });

            // Users לטבלה Pension
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasMany<Pension>(A => A.ListPension)
                .WithOne(B => B.Users)
                .HasForeignKey(C => C.UserId);
            });

            // Pension לטבלה Citys
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasMany<Pension>(A => A.ListPension)
                .WithOne(B => B.City)
                .HasForeignKey(C => C.CityId);
            });

            // Shops לטבלה Citys
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasMany<Shops>(A => A.ListShop)
                .WithOne(B => B.City)
                .HasForeignKey(C => C.CityId);
            });

            // Barbershop לטבלה Users
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasMany<BarberShop>(A => A.ListBarberShop)
                .WithOne(B => B.User)
                .HasForeignKey(C => C.UserId);
            });

            // Barbershop לטבלה Citys
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasMany<BarberShop>(A => A.ListBarberShop)
                .WithOne(B => B.City)
                .HasForeignKey(C => C.CityId);
            });

            // Training לטבלה Users
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasMany<Training>(A => A.ListTraining)
                .WithOne(B => B.User)
                .HasForeignKey(C => C.UserId);
            });

            // Training לטבלה Citys
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasMany<Training>(A => A.ListTraining)
                .WithOne(B => B.City)
                .HasForeignKey(C => C.CityId);
            });

            // DogOwner לטבלה Lodging      
            modelBuilder.Entity<DogOwner>(entity =>
            {
                entity.HasMany<Lodging>(A => A.ListLodging)
                .WithOne(B => B.DogOwner)
                .HasForeignKey(C => C.DogOwnerId);
            });

            // Pension לטבלה Lodging
            modelBuilder.Entity<Pension>(entity =>
            {
                entity.HasMany<Lodging>(A => A.ListLodging)
                .WithOne(B => B.Pension)
                .HasForeignKey(C => C.PensionId);
            }); 

            // DogOwner לטבלה Haircuts
            modelBuilder.Entity<DogOwner>(entity =>
            {
                entity.HasMany<Haircuts>(A => A.ListHaircuts)
                .WithOne(B => B.DogOwner)
                .HasForeignKey(C => C.DogOwnerId);

               // .OnDelete(DeleteBehavior.Cascade);
                // .OnDelete(DeleteBehavior.ClientCascade);

            });

            // BarberShop לטבלה Haircuts
            modelBuilder.Entity<BarberShop>(entity =>
            {
                entity.HasMany<Haircuts>(A => A.ListHaircuts)
                .WithOne(B => B.BarberShop)
                .HasForeignKey(C => C.BarbershopId);
            });

            // TypesHaircut לטבלה Haircuts
            modelBuilder.Entity<TypesHaircut>(entity =>
            {
                entity.HasMany<Haircuts>(A => A.ListHaircuts)
                .WithOne(B => B.TypesHaircut)
                .HasForeignKey(C => C.TypesHaircutId);
            });

            // BarberShop לטבלה TypesHaircut
            modelBuilder.Entity<BarberShop>(entity =>
            {
                entity.HasMany<TypesHaircut>(A => A.ListTypesHaircut)
                .WithOne(B => B.BarberShop)
                .HasForeignKey(C => C.BarberShopId);
            });

            // DogOwner לטבלה DogTraining
            modelBuilder.Entity<DogOwner>(entity =>
            {
                entity.HasMany<DogTraining>(A => A.ListDogTraining)
                .WithOne(B => B.DogOwner)
                .HasForeignKey(C => C.DogOwnerId);
            });

            // Training לטבלה DogTraining
            modelBuilder.Entity<Training>(entity =>
            {
                entity.HasMany<DogTraining>(A => A.ListDogTraining)
                .WithOne(B => B.Training)
                .HasForeignKey(C => C.TrainingId);
            });

            // TrainingPackage לטבלה DogTraining
            modelBuilder.Entity<TrainingPackage>(entity =>
            {
                entity.HasMany<DogTraining>(A => A.ListDogTraining)
                .WithOne(B => B.TrainingPackage)
                .HasForeignKey(C => C.PackagId);
            });

            // Training לטבלה TrainingPackage
            modelBuilder.Entity<Training>(entity =>
            {
                entity.HasMany<TrainingPackage>(A => A.ListTrainingPackage)
                .WithOne(B => B.Training)
                .HasForeignKey(C => C.TrainingId);
            });
        }

        // מיפוי שמות הטבלאות - מקבל שורה מהטבלה
        public virtual DbSet<Users> Users { get; set; } 
        public virtual DbSet<DogOwner> DogOwner { get; set; }
        public virtual DbSet<DogRace> DogRace { get; set; }
        public virtual DbSet<City> Citys { get; set; }
        public virtual DbSet<TypesHaircut> TypesHaircut { get; set; }
        public virtual DbSet<TrainingPackage> TrainingPackage { get; set; }
        public virtual DbSet<Pension> Pension { get; set; } 
        public virtual DbSet<BarberShop> BarberShop { get; set; }
        public virtual DbSet<Training> Training { get; set; }
        public virtual DbSet<Shops> Shops { get; set; }
        public virtual DbSet<Lodging> Lodging { get; set; }
        public virtual DbSet<Haircuts> Haircuts { get; set; }
        public virtual DbSet<DogTraining> DogTraining { get; set; }      
    }
}
