using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Shelter.Domain.Entities;
using Shelter.Domain.Constants;
using System.Security.Cryptography;
using System.Text;

namespace Shelter.Domain.Context
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }

        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Genus> Genus { get; set; }

        public BaseDbContext(IConfiguration configuration, DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            IEnumerable<EntityEntry<Entity>> datas = ChangeTracker
                .Entries<Entity>().Where(e =>
                    e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OperationClaim[] operationClaimSeeds =
           {
                new(1,OperationClaimConsts.Admin),
                new(2,OperationClaimConsts.User),
            };
            modelBuilder.Entity<OperationClaim>().HasData(operationClaimSeeds);



            User user = new()
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Admin",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Email = "b161210122@sakarya.edu.tr",
                Status = true,
                
            };

            byte[] passwordHash, passwordSalt;
            using HMACSHA512 hmac = new();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("sau"));
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            User[] userSeed = new[] { user };

            modelBuilder.Entity<User>().HasData(user);

            UserOperationClaim[] userOperationClaimSeedData = new UserOperationClaim[] { new(1, 1, 1) };
            modelBuilder.Entity<UserOperationClaim>().HasData(userOperationClaimSeedData);
        }
    }
}
