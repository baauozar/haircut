using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;


namespace DataLayer
{
    public class Context:DbContext
    {

        public Context(DbContextOptions<Context> options)
           : base(options)
        {
        }


       
        public DbSet<BeautyCategory>? BeautyCategories { get; set; }
        public DbSet<BeautyItem>? BeautyItems { get; set; }
        public DbSet<BeautyCardInfo>? BeautyCardInfos { get; set; }
        public DbSet<BeautyServiesItem>? BeautyServiesItems { get; set; }
        public DbSet<BeautysServices>? BeautysServices { get; set; }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<Contact>? Contacts { get; set; }
        public DbSet<Faq>? Faqs { get; set; }
        public DbSet<HaircutMenuCategory>? HaircutMenuCategories { get; set; }
        public DbSet<HaircutMenuItem>? HaircutMenuItems { get; set; }

        public DbSet<HaircutServicesCategory>? HaircutServicesCategories { get; set; }
        public DbSet<HaircutService>? HaircutServices { get; set; }
        public DbSet<HaircutSupService>? HairCutSupServices { get; set; }
        public DbSet<HairCutTeammember>? HairCutTeammembers { get; set; }

    

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            // If you have any other one-to-one, many-to-many, or other relationships, configure them here as well.
            // For example, if there's a relationship between BeautysServices and another entity, 
            // you would add a similar configuration.

            base.OnModelCreating(modelBuilder);


        }
}
}
