using ImageStoreWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace ImageStoreWeb.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public static AppDbContext Create()
        {
            return new AppDbContext();

        }

        public AppDbContext() : base("defaultDB") { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public override Task<int> SaveChangesAsync()
        {
            try
            {
                SetUpdatedOn();
                return base.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                throw ex;
            }
        }
        public override int SaveChanges()
        {

            SetUpdatedOn();

            return base.SaveChanges();
        }

        private void SetUpdatedOn()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is EntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified));


            foreach (var entity in entities)
            {
                ((EntityBase)entity.Entity).UpdatedOn = DateTime.UtcNow;

            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>().Configure(
                x => x.HasMaxLength(100).IsRequired()
                );

            modelBuilder.Types<EntityBase>().Configure(c =>
            {
                c.Property(x => x.CreatedOn)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);
            });


            var user = modelBuilder.Entity<Client>().ToTable("ClientInfo");
            user.Property(x => x.Street).HasMaxLength(200).IsOptional();
            user.Property(x => x.City).IsOptional();
            user.Property(x => x.State).HasMaxLength(60).IsOptional();
            user.Property(x => x.Zip).HasMaxLength(10).IsOptional();
            user.Property(x => x.Country).IsOptional();
            user.Property(x => x.Phone).IsOptional();
            user.Property(x => x.Password).HasMaxLength(256);

            var image = modelBuilder.Entity<Image>().ToTable("Image");
            image.Property(x => x.ImagePath).HasMaxLength(256).IsOptional();
            image.Property(x => x.ImageContent).IsOptional();
            image.Property(x => x.ImageType).HasMaxLength(10);

            var token = modelBuilder.Entity<Token>().ToTable("Token");
            token.Property(x => x.AuthToken).HasMaxLength(1000);
            base.OnModelCreating(modelBuilder);


        }
    }
}