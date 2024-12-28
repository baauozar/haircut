using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace DataLayer
{
    public class Context :IdentityDbContext<IdentityUser, IdentityRole, string>
    {

        public Context(DbContextOptions<Context> options)
           : base(options)
        {

        }


       
        public DbSet<BeautyCategory>? BeautyCategories { get; set; }
        public DbSet<BeautyItem>? BeautyItems { get; set; }
        public DbSet<BeautyCardInfo>? BeautyCardInfos { get; set; }
        public DbSet<BeautyServicesItem>? BeautyServiesItems { get; set; }
        public DbSet<BeautyService>? BeautysServices { get; set; }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<Contact>? Contacts { get; set; }
        public DbSet<Faq>? Faqs { get; set; }
        public DbSet<HaircutMenuCategory>? HaircutMenuCategories { get; set; }
        public DbSet<HaircutMenuItem>? HaircutMenuItems { get; set; }
        public DbSet<HairStyleSuggestion>? HairStyleSuggestions { get; set; }

        public DbSet<HaircutServicesCategory>? HaircutServicesCategories { get; set; }
        public DbSet<HaircutService>? HaircutServices { get; set; }
        public DbSet<HaircutSupService>? HairCutSupServices { get; set; }
    

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Custom table names for Identity
            modelBuilder.Entity<IdentityUser>(entity => {
                entity.ToTable("AspNetUsers");
            });

            modelBuilder.Entity<IdentityRole>(entity => {
                entity.ToTable("AspNetRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity => {
                entity.ToTable("AspNetUserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity => {
                entity.ToTable("AspNetUserLogins");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity => {
                entity.ToTable("AspNetUserRoles");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => {
                entity.ToTable("AspNetRoleClaims");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity => {
                entity.ToTable("AspNetUserTokens");
            });



            modelBuilder.Entity<BeautyCategory>().HasQueryFilter(bc => !bc.IsDeleted);
            modelBuilder.Entity<BeautyItem>().HasQueryFilter(bi => !bi.IsDeleted);

            modelBuilder.Entity<HaircutMenuCategory>().HasQueryFilter(hmc => !hmc.IsDeleted);
            modelBuilder.Entity<HaircutMenuItem>().HasQueryFilter(hmi => !hmi.IsDeleted);

            modelBuilder.Entity<HaircutServicesCategory>().HasQueryFilter(hsc => !hsc.IsDeleted);
            modelBuilder.Entity<HaircutService>().HasQueryFilter(hs => !hs.IsDeleted);
            modelBuilder.Entity<HaircutSupService>().HasQueryFilter(hcs => !hcs.IsDeleted);



            modelBuilder.Entity<BeautyCategory>()
          .HasMany(bc => bc.BeautyItems)
          .WithOne(bi => bi.BeautyCategory)   // The navigation property in BeautyItems pointing back to BeautyCategory
          .HasForeignKey(bi => bi.BeautyCategoryId)
          .OnDelete(DeleteBehavior.Cascade);

            // 2. HaircutMenuCategory to HaircutMenuItem (One-to-Many)
            modelBuilder.Entity<HaircutMenuCategory>()
                .HasMany(hmc => hmc.HaircutMenuItems)
                .WithOne(hmi => hmi.HaircutMenuCategory)  // The navigation property in HaircutMenuItem pointing back to HaircutMenuCategory
                .HasForeignKey(hmi => hmi.HaircutMenuCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // 3. HaircutServicesCategory to HaircutServices (One-to-Many)
            modelBuilder.Entity<HaircutServicesCategory>()
                .HasMany(hsc => hsc.HaircutServices)
                .WithOne(hs => hs.HaircutServicesCategory) // The navigation property in HaircutServices pointing back to HaircutServicesCategory
                .HasForeignKey(hs => hs.ServiceCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // 4. HaircutServices to HairCutSupServices (One-to-Many)
            modelBuilder.Entity<HaircutService>()
                .HasMany(hs => hs.HairCutSupServices)
                .WithOne(hss => hss.HaircutService)     // The navigation property in HairCutSupServices pointing back to HaircutServices
                .HasForeignKey(hss => hss.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // 5.Beauty services to Beauty items(one-to-one)
            modelBuilder.Entity<BeautyService>()
         .HasOne(b => b.BeautyServicesItem) // Each BeautyService belongs to one BeautyServicesItem
         .WithOne(i => i.BeautyService)    // Each BeautyServicesItem has one BeautyService
         .HasForeignKey<BeautyService>(b => b.BeautyServicesItemId) // Foreign key in BeautyService
         .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete

        }
    }
}
